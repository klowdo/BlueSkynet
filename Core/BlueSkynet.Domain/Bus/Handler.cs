using BlueSkynet.Domain.Models;

namespace BlueSkynet.Domain.Bus
{
    public interface IHandles<in T> where T : Event
    {
        void Handle(T message);
    }
}