using System;

namespace BlueSkynet.Domain.Models.ServiceBus.Events
{
    public class ServiceBusTopicSubscriberCreated : Event
    {
        public ServiceBusTopicSubscriberCreated(Guid id, string subscriberName, string topicName)
        {
            Id = id;
            SubscriberName = subscriberName;
            TopicName = topicName;
        }

        public string SubscriberName { get; set; }
        public string TopicName { get; set; }
    }
}