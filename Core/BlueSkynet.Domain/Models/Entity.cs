using System;

namespace BlueSkynet.Domain.Models
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
    }
}