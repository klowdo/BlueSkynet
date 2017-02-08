using System;

namespace BlueSkynet.Domain.Models.ServiceBus.Events
{
    public class ServiceBusQueueRemoved : Event
    {
        public ServiceBusQueueRemoved(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; set; }
    }
}