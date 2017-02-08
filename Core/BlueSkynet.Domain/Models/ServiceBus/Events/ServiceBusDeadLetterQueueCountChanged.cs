using System;

namespace BlueSkynet.Domain.Models.ServiceBus.Events
{
    public class ServiceBusDeadLetterQueueCountChanged : Event
    {
        public ServiceBusDeadLetterQueueCountChanged(Guid id, string name, int count)
        {
            Id = id;
            Name = name;
            Count = count;
        }

        public string Name { get; set; }
        public int Count { get; set; }
    }
}