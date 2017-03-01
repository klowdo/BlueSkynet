using System;

namespace BlueSkynet.Domain.Models
{
    public class DomainBase<T> where T : AggregateRoot, new()
    {
        public T State { get; private set; }
        protected Guid Id => State.Id;

        public DomainBase()
        {
            State = new T();
        }

        protected void CreateFromState(T state) =>
            State = state;

        protected void ApplyChange(Event @event)
        {
            State.ApplyChange(@event);
        }
    }
}