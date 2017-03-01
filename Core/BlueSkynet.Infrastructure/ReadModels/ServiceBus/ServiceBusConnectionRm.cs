using BlueSkynet.Domain.Bus;
using BlueSkynet.Domain.Data;
using BlueSkynet.Domain.Models;
using BlueSkynet.Domain.Models.ServiceBus.Events;

namespace BlueSkynet.Infrastructure.ReadModels.ServiceBus
{
    public class ServiceBusConnectionRm : Entity
    {
        public ServiceBusConnectionRm()
        {
        }

        public ServiceBusConnectionRm(string name, string connectionString)
        {
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