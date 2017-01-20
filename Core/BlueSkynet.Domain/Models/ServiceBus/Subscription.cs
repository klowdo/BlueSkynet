using System.Collections.Generic;

namespace BlueSkynet.Domain.Models.ServiceBus
{
    public class Subscription : BaseQueue
    {
        public IEnumerable<Filter> Filters { get; set; }
    }
}