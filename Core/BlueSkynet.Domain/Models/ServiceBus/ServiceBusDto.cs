using System;
using System.Collections.Generic;

namespace BlueSkynet.Domain.Models.ServiceBus
{
    public class ServiceBusDto : Entity
    {
        public string ConnectionString;

        public ServiceBusDto(Guid id, string connectionString)
        {
            Id = id;
            ConnectionString = connectionString;
        }

        public IList<Queue> Queues { get; set; }
        public IList<Topic> Topics { get; set; }
    }
}