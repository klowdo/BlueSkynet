using System;

namespace BlueSkynet.Domain.Models.ServiceBus.Events
{
    public class ServiceBusRenamed : Event
    {
        public readonly string Name;

        public ServiceBusRenamed(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}