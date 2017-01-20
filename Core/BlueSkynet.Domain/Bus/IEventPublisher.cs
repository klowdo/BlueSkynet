using BlueSkynet.Domain.Models.Events;

namespace BlueSkynet.Domain.Bus
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : Event;
    }
}