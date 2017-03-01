using System;

namespace BlueSkynet.Domain.Models
{
    public class DomainBase<T> where T : AggregateRoot, new()
    {
        public T State { get; }
        protected Guid Id => State.Id;

        public DomainBase()
        {
            State = new T();
        }

        protected void ApplyChange(Event @event)
        {
            State.ApplyChange(@event);
        }
    }
}