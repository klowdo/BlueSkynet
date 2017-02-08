using System.Collections.Generic;

namespace BlueSkynet.Domain.Models.ServiceBus.Models
{
    public class Topic
    {
        public Topic()
        {
        }

        public Topic(string name)
        {
            Name = name;
            Subscriptions = new List<string>();
        }

        public string Name { get; set; }
        public IList<string> Subscriptions { get; set; }
    }
}