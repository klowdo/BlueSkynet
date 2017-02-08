using BlueSkynet.Domain.Models;

namespace BlueSkynet.Domain.Bus
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : Event;
    }
}