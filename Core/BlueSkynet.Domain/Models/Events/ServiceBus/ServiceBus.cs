using System;

namespace BlueSkynet.Domain.Models.Events.ServiceBus
{
    public class ServiceBus : AggregateRoot
    {
        private Guid _id;
        private string _name;
        private string _connectionString;

        public void Apply(ServiceBusCreated e)
        {
            _id = e.Id;
            _connectionString = e.ConnectionString;
        }

        public void Apply(ServiceBusQueueCreated e)
        {
            _name = e.Name;
        }

        public ServiceBus(Guid id, string connectionString)
        {
            ApplyChange(new ServiceBusCreated(id, connectionString));
        }

        public override Guid Id => _id;

        public void CreateQueue(string name)
        {
            ApplyChange(new ServiceBusQueueCreated(_id, name));
        }
    }
}