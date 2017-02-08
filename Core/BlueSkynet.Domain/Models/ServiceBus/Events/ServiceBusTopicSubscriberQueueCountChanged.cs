using System;

namespace BlueSkynet.Domain.Models.ServiceBus.Events
{
    public class ServiceBusTopicSubscriberQueueCountChanged : Event
    {
        public ServiceBusTopicSubscriberQueueCountChanged(
            Guid id,
            string topicName,
            string subscriberName,
            int queueCount)
        {
            Id = id;
            TopicName = topicName;
            SubscriberName = subscriberName;
            QueueCount = queueCount;
        }

        public string TopicName { get; set; }
        public string SubscriberName { get; set; }
        public int QueueCount { get; set; }
    }
}