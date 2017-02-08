using System;
using System.Collections.Generic;
using BlueSkynet.Domain.Bus;
using BlueSkynet.Domain.Data;
using BlueSkynet.Domain.Models;
using BlueSkynet.Domain.Models.ServiceBus.Events;

namespace BlueSkynet.Infrastructure.ReadModels.ServiceBus
{
    public class ServiceBusDetailedItemRm : Entity
    {
        public ServiceBusDetailedItemRm()
        {
        }

        public ServiceBusDetailedItemRm(Guid id, string name)
        {
            Name = name;
            Id = id;
            Queues = new List<ServiceBusQueue>();
        }

        public string Name { get; set; }
        public IList<ServiceBusQueue> Queues { get; set; }
    }

    public class ServiceBusDetailedItemView :
        ReadModelBase<ServiceBusDetailedItemRm>,
        IHandles<ServiceBusCreated>,
        IHandles<ServiceBusRenamed>,
        IHandles<ServiceBusQueueCreated>
    {
        public ServiceBusDetailedItemView(IDataContext db) : base(db)
        {
        }

        public void Handle(ServiceBusCreated message)
        {
            Insert(new ServiceBusDetailedItemRm(
                             name: message.Name,
                             id: message.Id
                             ));
        }

        public void Handle(ServiceBusRenamed message)
        {
            var item = Find(message.Id);
            item.Name = message.Name;
            Update(item);
        }

        public void Handle(ServiceBusQueueCreated message)
        {
            var item = Find(message.Id);
            item.Queues.Add(new ServiceBusQueue
            {
                Name = message.Name
            });
            Update(item);
        }
    }
}