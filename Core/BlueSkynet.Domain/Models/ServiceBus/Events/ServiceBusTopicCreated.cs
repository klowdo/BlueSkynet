using System;

namespace BlueSkynet.Domain.Models.ServiceBus.Events
{
    public class ServiceBusTopicCreated : Event
    {
        public ServiceBusTopicCreated(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; set; }
    }
}