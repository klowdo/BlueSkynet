using System.Collections.Generic;

namespace BlueSkynet.Domain.Models.ServiceBus
{
    public class Topic
    {
        public string Name { get; set; }
        public IEnumerable<Subscription> Subscriptions { get; set; }
    }
}