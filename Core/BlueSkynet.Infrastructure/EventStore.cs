using BlueSkynet.Domain.Bus;
using BlueSkynet.Domain.Exceptions;
using BlueSkynet.Domain.Models;
using BlueSkynet.Domain.Services;
using Microsoft.WindowsAzure.Storage.Table;
using Streamstone;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueSkynet.Infrastructure
{
    public class EventStore : IEventStore
    {
        private readonly CloudTable _table;
        private readonly IEventPublisher _publisher;
        private readonly ISerializer _serializer;

        public EventStore(
            CloudTable table,
            IEventPublisher publisher,
            ISerializer serializer)
        {
            _publisher = publisher;
            _serializer = serializer;
            _table = table;
        }

        public void SaveEvents(Guid aggregateId, Event[] events, int expectedVersion)
        {
            var i = expectedVersion;

            // iterate through current aggregate events increasing version with each processed event
            foreach (var @event in events)
            {
                i++;
                @event.Version = i;
            }

            var paritionKey = aggregateId.ToString("D");
            var partition = new Partition(_table, paritionKey);

            var existent = Stream.TryOpen(partition);
            var stream = existent.Found
                ? existent.Stream
                : new Stream(partition);

            if (stream.Version != expectedVersion)
                throw new ConcurrencyException();

            try
            {
                Stream.Write(stream, events.Select(ToEventData).ToArray());
            }
            catch (ConcurrencyConflictException e)
            {
                throw new ConcurrencyException();
            }

            foreach (var @event in events)
            {
                // publish current event to the bus for further processing by subscribers
                _publisher.Publish(@event);
            }
        }

        // collect all processed events for given aggregate and return them as a list
        // used to build up an aggregate from its history (Domain.LoadsFromHistory)
        public List<Event> GetEventsForAggregate(Guid aggregateId)
        {
            var paritionKey = aggregateId.ToString("D");
            var partition = new Partition(_table, paritionKey);

            if (!Stream.Exists(partition))
            {
                throw new AggregateNotFoundException();
            }

            return Stream.Read<EventEntity>(partition).Events.Select(ToEvent).ToList();
        }

        private Event ToEvent(EventEntity e)
        {
            return (Event)_serializer.DeserializeObject(e.Data, Type.GetType(e.Type));
        }

        private EventData ToEventData(Event e)
        {
            var id = Guid.NewGuid();

            var properties = new
            {
                Id = id,
                Type = e.GetType().FullName,
                Data = _serializer.SerializeObject(e)
            };

            return new EventData(EventId.From(id), EventProperties.From(properties));
        }

        private class EventEntity : TableEntity
        {
            public string Type { get; set; }
            public string Data { get; set; }
        }
    }
}