using System;

namespace BlueSkynet.Domain.Models.ServiceBus.Events
{
    public class ServiceBusCreated : Event
    {
        public readonly string Name;

        public ServiceBusCreated(Guid id, string connectionString, string name)
        {
            Id = id;
            ConnectionString = connectionString;
            Name = name;
        }

        public string ConnectionString { get; set; }
    }
}