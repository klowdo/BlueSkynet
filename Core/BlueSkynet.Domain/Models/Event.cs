using BlueSkynet.Domain.Bus;

namespace BlueSkynet.Domain.Models
{
    public class Event : Entity, Message
    {
        public int Version;
    }
}