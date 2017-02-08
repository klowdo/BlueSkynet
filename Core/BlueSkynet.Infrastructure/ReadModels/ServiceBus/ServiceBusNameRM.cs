using System;
using BlueSkynet.Domain.Bus;
using BlueSkynet.Domain.Data;
using BlueSkynet.Domain.Models;
using BlueSkynet.Domain.Models.ServiceBus.Events;

namespace BlueSkynet.Infrastructure.ReadModels.ServiceBus
{
    public class ServiceBusItemListDto : Entity
    {
        public ServiceBusItemListDto()
        {
        }

        public ServiceBusItemListDto(string name, Guid id)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; set; }
    }

    public class ServiceBusItemListView :
        ReadModelBase<ServiceBusItemListDto>,
        IHandles<ServiceBusCreated>,
        IHandles<ServiceBusRenamed>
    {
        public ServiceBusItemListView(IDataContext db) : base(db)
        {
        }

        public void Handle(ServiceBusCreated message)
        {
            Insert(new ServiceBusItemListDto(
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
    }
}