using BlueSkynet.Domain.Models.ServiceBus.Events;
using BlueSkynet.Domain.Models.ServiceBus.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueSkynet.Domain.Models.ServiceBus
{
    public class ServiceBusState : AggregateRoot

    {
        private Guid _id;
        public bool Activated;
        public readonly List<string> Queues = new List<string>();
        public readonly List<Topic> Topics = new List<Topic>();
        public override Guid Id => _id;

        public void Apply(ServiceBusCreated e)
        {
            _id = e.Id;
            Activated = true;
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

        public void Apply(ServiceBusDeactivated e)
        {
            Activated = false;
        }
    }
}