using System;

namespace BlueSkynet.Domain.Models.ServiceBus.Events
{
    public class ServiceBusTopicRemoved : Event
    {
        public ServiceBusTopicRemoved(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; set; }
    }
}