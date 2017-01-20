using System;

namespace BlueSkynet.Domain.Models.Events.ServiceBus
{
    public class ServiceBusCreated : Event
    {
        public readonly Guid Id;
        public readonly string ConnectionString;

        public ServiceBusCreated(Guid id, string connectionString)
        {
            Id = id;
            ConnectionString = connectionString;
        }
    }
}