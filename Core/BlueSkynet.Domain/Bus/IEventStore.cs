using System;
using System.Collections.Generic;
using BlueSkynet.Domain.Models;
using BlueSkynet.Domain.Models.ServiceBus.Events;

namespace BlueSkynet.Domain.Bus
{
    public interface IEventStore
    {
        void SaveEvents(Guid aggregateId, Event[] events, int expectedVersion);

        List<Event> GetEventsForAggregate(Guid aggregateId);
    }
}