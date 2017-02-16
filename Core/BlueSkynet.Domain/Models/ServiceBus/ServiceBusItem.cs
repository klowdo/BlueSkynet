using BlueSkynet.Domain.Exceptions;
using BlueSkynet.Domain.Extentions;
using BlueSkynet.Domain.Models.ServiceBus.Events;
using BlueSkynet.Domain.Models.ServiceBus.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueSkynet.Domain.Models.ServiceBus
{
    public class ServiceBusItem : AggregateRoot
    {
        private Guid _id;
        private bool _activated;
        public readonly List<string> Queues = new List<string>();
        public readonly List<Topic> Topics = new List<Topic>();
        public override Guid Id => _id;

        public void Apply(ServiceBusCreated e)
        {
            _id = e.Id;
            _activated = true;
        }

        public void Apply(ServiceBusQueueRemoved e) =>
            Queues.Remove(e.Name);

        public void Apply(ServiceBusQueueCreated e) =>
            Queues.Add(e.Name);

        public void Apply(ServiceBusTopicCreated e) =>
            Topics.Add(new Topic(e.Name));

        public void Apply(ServiceBusTopicSubscriberCreated e)
        {
            var topic = Topics.SingleOrDefault(x => x.Name.Equals(e.TopicName));
            topic?.Subscriptions
                .Add(e.SubscriberName);
        }

        private bool TopicExist(string name)
        {
            name.ThrowIfNullOrBlank(nameof(name));
            return Topics.Exists(x => x.Name.Equals(name));
        }

        private bool SubscriberExist(string topicName, string subscriptionName)
        {
            topicName.ThrowIfNullOrBlank(nameof(topicName));
            subscriptionName.ThrowIfNullOrBlank(nameof(subscriptionName));
            return Topics.Exists(x => x.Name.Equals(topicName) &&
                                    x.Subscriptions.Any(y => y.Equals(subscriptionName)));
        }

        private bool QueueExist(string name)
        {
            name.ThrowIfNullOrBlank(nameof(name));
            return Queues.Contains(name);
        }

        public void Apply(ServiceBusDeactivated e)
        {
            _activated = false;
        }

        public void Deactivate()
        {
            if (!_activated) throw new InvalidOperationException("Item already Deactivated");
            ApplyChange(new ServiceBusDeactivated(_id));
        }

        public void ChangeName(string name)
        {
            name.ThrowIfNullOrBlank(nameof(name));
            ApplyChange(new ServiceBusRenamed(_id, name));
        }

        public void UpdateQueueCount(string name, int queueCount)
        {
            if (!QueueExist(name)) throw new NotFoundException($"Queue {name} does not exsist");
            queueCount.ThrowIfNegative(nameof(queueCount));
            ApplyChange(new ServiceBusQueueCountChange(_id, name, queueCount));
        }

        public void UpdateDeadLetterQueueCount(string name, int queueCount)
        {
            if (!QueueExist(name)) throw new NotFoundException($"Queue {name} does not exsist");
            if (queueCount < 0) throw new InvalidOperationException("Queue count can't be negative");
            ApplyChange(new ServiceBusDeadLetterQueueCountChanged(_id, name, queueCount));
        }

        public void AddQueue(string name)
        {
            if (QueueExist(name)) throw new InvalidOperationException("Queue already exist");
            ApplyChange(new ServiceBusQueueCreated(_id, name));
        }

        public void RemoveQueue(string name)
        {
            if (!QueueExist(name)) throw new NotFoundException($"Queue with name {name} does not exist ");
            ApplyChange(new ServiceBusQueueRemoved(_id, name));
        }

        public void AddTopic(string name)
        {
            if (TopicExist(name)) throw new InvalidOperationException("Topic already exist");
            ApplyChange(new ServiceBusTopicCreated(_id, name));
        }

        public void AddTopicSubscriber(string topicName, string subscriberName)
        {
            if (!TopicExist(topicName)) throw new NotFoundException("Topic does not exist");
            if (SubscriberExist(topicName, subscriberName)) throw new InvalidOperationException($"Subsciption: {subscriberName} Already exists on topic: {topicName}");
            ApplyChange(new ServiceBusTopicSubscriberCreated(_id, subscriberName, topicName));
        }

        public void UpdateTopicSubsciberQueueCount(string topic, string subscriber, int count)
        {
            if (!SubscriberExist(topic, subscriber)) throw new InvalidOperationException($"Subsciption: {subscriber} Does not exists on topic: {topic}");
            count.ThrowIfNegative(nameof(count));
            ApplyChange(new ServiceBusTopicSubscriberQueueCountChanged(_id, topic, subscriber, count));
        }

        public ServiceBusItem()
        {
        }

        public ServiceBusItem(Guid id, string connectionString, string name)
        {
            name.ThrowIfNullOrBlank(nameof(name));
            connectionString.ThrowIfNullOrBlank(nameof(connectionString));
            ApplyChange(new ServiceBusCreated(id, connectionString, name));
        }

        public void UpdateTopicSubsciberDeadLetterQueueCount(string topic, string subscriber, int count)
        {
            if (!SubscriberExist(topic, subscriber)) throw new InvalidOperationException($"Subsciption: {subscriber} Does not exists on topic: {topic}.");
            count.ThrowIfNegative(nameof(count));
            ApplyChange(new ServiceBusTopicSubscriberDeadLetterQueueCountChanged(_id, topic, subscriber, count));
        }

        public void RemoveTopic(string name) =>
            ApplyChange(new ServiceBusTopicRemoved(_id, name));

        public void RemoveTopicSubscriber(string topicName, string subscriberName) =>
            ApplyChange(new ServiceBusTopicSubscriberRemoved(_id, topicName, subscriberName));
    }
}