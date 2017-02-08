using System;

namespace BlueSkynet.Domain.Models.ServiceBus.Events
{
    public class ServiceBusQueueCreated : Event
    {
        public readonly string Name;

        public ServiceBusQueueCreated(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}