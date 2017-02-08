﻿using BlueSkynet.Domain.Bus;
using System;
using System.Collections.Generic;
using BlueSkynet.Domain.Models;
using BlueSkynet.Domain.Models.ServiceBus.Events;

namespace BlueSkynet.Domain
{
    public abstract class AggregateRoot
    {
        private readonly List<Event> _changes = new List<Event>();

        public abstract Guid Id { get; }
        public int Version { get; internal set; }

        public IEnumerable<Event> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        public void LoadsFromHistory(IEnumerable<Event> history)
        {
            foreach (var e in history) ApplyChange(e, false);
        }

        protected void ApplyChange(Event @event)
        {
            ApplyChange(@event, true);
        }

        // push atomic aggregate changes to local history for further processing (EventStore.SaveEvents)
        private void ApplyChange(Event @event, bool isNew)
        {
            ((dynamic)this).Apply((dynamic)@event);
            if (isNew) _changes.Add(@event);
            Version++;
        }

        public void Apply(Event e)
        {
            // no-op
        }
    }
}