using System;

namespace BlueSkynet.Domain.Models.ServiceBus.Events
{
    public class ServiceBusQueueCountChange : Event
    {
        public ServiceBusQueueCountChange(Guid id, string name, int count)
        {
            Id = id;
            Name = name;
            Count = count;
        }

        public string Name { get; set; }
        public int Count { get; set; }
    }
}