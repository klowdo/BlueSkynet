using BlueSkynet.Domain.Bus;

namespace BlueSkynet.Domain.Services
{
    public interface IHandlesFactory
    {
        IHandles<T> Create<T>();
    }
}