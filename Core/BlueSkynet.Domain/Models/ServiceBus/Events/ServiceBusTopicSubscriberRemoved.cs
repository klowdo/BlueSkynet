using System;

namespace BlueSkynet.Domain.Models.ServiceBus.Events
{
    public class ServiceBusTopicSubscriberRemoved : Event
    {
        public ServiceBusTopicSubscriberRemoved(Guid id, string topicName, string subscriverName)
        {
            Id = id;
            TopicName = topicName;
            SubscriverName = subscriverName;
        }

        public string TopicName { get; set; }
        public string SubscriverName { get; set; }
    }
}