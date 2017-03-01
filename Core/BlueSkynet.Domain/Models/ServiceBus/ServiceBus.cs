using BlueSkynet.Domain.Exceptions;
using BlueSkynet.Domain.Extentions;
using BlueSkynet.Domain.Models.ServiceBus.Events;
using System;
using System.Linq;

namespace BlueSkynet.Domain.Models.ServiceBus
{
    public class ServiceBus : DomainBase<ServiceBusState>
    {
        public ServiceBus()
        {
        }

        public ServiceBus(Guid id, string connectionString, string name)
        {
            name.ThrowIfNullOrBlank(nameof(name));
            connectionString.ThrowIfNullOrBlank(nameof(connectionString));
            ApplyChange(new ServiceBusCreated(id, connectionString, name));
        }

        private bool TopicExist(string name)
        {
            name.ThrowIfNullOrBlank(nameof(name));
            return State.Topics.Exists(x => x.Name.Equals(name));
        }

        private bool SubscriberExist(string topicName, string subscriptionName)
        {
            topicName.ThrowIfNullOrBlank(nameof(topicName));
            subscriptionName.ThrowIfNullOrBlank(nameof(subscriptionName));
            return State.Topics.Exists(x => x.Name.Equals(topicName) &&
                                    x.Subscriptions.Any(y => y.Equals(subscriptionName)));
        }

        private bool QueueExist(string name)
        {
            name.ThrowIfNullOrBlank(nameof(name));
            return State.Queues.Contains(name);
        }

        public void Deactivate()
        {
            if (!State.Activated) throw new InvalidOperationException("Item already Deactivated");
            ApplyChange(new ServiceBusDeactivated(Id));
        }

        public void ChangeName(string name)
        {
            name.ThrowIfNullOrBlank(nameof(name));
            ApplyChange(new ServiceBusRenamed(Id, name));
        }

        public void UpdateQueueCount(string name, int queueCount)
        {
            if (!QueueExist(name)) throw new NotFoundException($"Queue {name} does not exsist");
            queueCount.ThrowIfNegative(nameof(queueCount));
            ApplyChange(new ServiceBusQueueCountChange(Id, name, queueCount));
        }

        public void UpdateDeadLetterQueueCount(string name, int queueCount)
        {
            if (!QueueExist(name)) throw new NotFoundException($"Queue {name} does not exsist");
            if (queueCount < 0) throw new InvalidOperationException("Queue count can't be negative");
            ApplyChange(new ServiceBusDeadLetterQueueCountChanged(Id, name, queueCount));
        }

        public void AddQueue(string name)
        {
            if (QueueExist(name)) throw new InvalidOperationException("Queue already exist");
            ApplyChange(new ServiceBusQueueCreated(Id, name));
        }

        public void RemoveQueue(string name)
        {
            if (!QueueExist(name)) throw new NotFoundException($"Queue with name {name} does not exist ");
            ApplyChange(new ServiceBusQueueRemoved(Id, name));
        }

        public void AddTopic(string name)
        {
            if (TopicExist(name)) throw new InvalidOperationException("Topic already exist");
            ApplyChange(new ServiceBusTopicCreated(Id, name));
        }

        public void AddTopicSubscriber(string topicName, string subscriberName)
        {
            if (!TopicExist(topicName)) throw new NotFoundException("Topic does not exist");
            if (SubscriberExist(topicName, subscriberName)) throw new InvalidOperationException($"Subsciption: {subscriberName} Already exists on topic: {topicName}");
            ApplyChange(new ServiceBusTopicSubscriberCreated(Id, subscriberName, topicName));
        }

        public void UpdateTopicSubsciberQueueCount(string topic, string subscriber, int count)
        {
            if (!SubscriberExist(topic, subscriber)) throw new InvalidOperationException($"Subsciption: {subscriber} Does not exists on topic: {topic}");
            count.ThrowIfNegative(nameof(count));
            ApplyChange(new ServiceBusTopicSubscriberQueueCountChanged(Id, topic, subscriber, count));
        }

        public void UpdateTopicSubsciberDeadLetterQueueCount(string topic, string subscriber, int count)
        {
            if (!SubscriberExist(topic, subscriber)) throw new InvalidOperationException($"Subsciption: {subscriber} Does not exists on topic: {topic}.");
            count.ThrowIfNegative(nameof(count));
            ApplyChange(new ServiceBusTopicSubscriberDeadLetterQueueCountChanged(Id, topic, subscriber, count));
        }

        public void RemoveTopic(string name) =>
            ApplyChange(new ServiceBusTopicRemoved(Id, name));

        public void RemoveTopicSubscriber(string topicName, string subscriberName) =>
            ApplyChange(new ServiceBusTopicSubscriberRemoved(Id, topicName, subscriberName));
    }
}