namespace BlueSkynet.Domain.Bus
{
    public interface IHandles<in T>
    {
        void Handle(T message);
    }
}