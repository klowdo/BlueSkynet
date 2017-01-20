using System;

namespace BlueSkynet.Domain.Models.ServiceBus
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
    }
}