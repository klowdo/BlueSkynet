using System;

namespace BlueSkynet.Domain.Models.ServiceBus.Models
{
    public class ServiceBusDto : Entity
    {
        public string Name;

        public ServiceBusDto(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}