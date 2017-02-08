using System;

namespace BlueSkynet.Domain.Models.ServiceBus.Events
{
    public class ServiceBusDeactivated : Event
    {
        public ServiceBusDeactivated(Guid id)
        {
            Id = id;
        }
    }
}