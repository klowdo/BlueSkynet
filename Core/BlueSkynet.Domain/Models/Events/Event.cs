using BlueSkynet.Domain.Bus;

namespace BlueSkynet.Domain.Models.Events
{
    public class Event : Message
    {
        public int Version;
    }
}