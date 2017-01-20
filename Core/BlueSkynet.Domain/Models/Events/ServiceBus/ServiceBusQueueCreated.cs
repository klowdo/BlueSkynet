using System;

namespace BlueSkynet.Domain.Models.Events.ServiceBus
{
    public class ServiceBusQueueCreated : Event
    {
        public readonly Guid Id;
        public readonly string Name;

        public ServiceBusQueueCreated(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}