using BlueSkynet.Domain.Bus;
using BlueSkynet.Domain.Data;
using BlueSkynet.Domain.Models;
using BlueSkynet.Domain.Models.ServiceBus.Events;
using System;

namespace BlueSkynet.Infrastructure.ReadModels.ServiceBus
{
    public class ServiceBusConnectionRm : Entity
    {
        public ServiceBusConnectionRm()
        {
        }

        public ServiceBusConnectionRm(Guid id, string name, string connectionString)
        {
            Id = id;
            Name = name;
            ConnectionString = connectionString;
        }

        public string Name { get; set; }
        public string ConnectionString { get; set; }
    }

    public class ServiveBusConnectionView :
        ReadModelBase<ServiceBusConnectionRm>,
        IHandles<ServiceBusCreated>,
        IHandles<ServiceBusRenamed>
    {
        public ServiveBusConnectionView(IDataContext db) : base(db)
        {
        }

        public void Handle(ServiceBusCreated message)
        {
            Insert(new ServiceBusConnectionRm(
                id: message.Id,
                name: message.Name,
                connectionString: message.ConnectionString
                ));
        }

        public void Handle(ServiceBusRenamed message)
        {
            var item = Find(message.Id);
            item.Name = message.Name;
            Update(item);
        }
    }
}