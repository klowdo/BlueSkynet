using BlueSkynet.Domain.Bus;
using BlueSkynet.Domain.Models;
using BlueSkynet.Domain.Services;

namespace BlueSkynet.Domain.EventStore
{
    public class EventBus : ICommandSender, IEventPublisher
    {
        private readonly IHandlesFactory _handlesFactory;

        public EventBus(IHandlesFactory handlesFactory)
        {
            _handlesFactory = handlesFactory;
        }

        public void Send<T>(T command) where T : Command
        {
            var handler = _handlesFactory.Create<T>();
            handler.Execute(command);
        }

        public void Publish<T>(T @event) where T : Event
        {
            var handelers = _handlesFactory.Get<T>();
            foreach (var handeler in handelers)
            {
                handeler.Handle(@event);
            }
        }
    }
}