using System;

namespace BlueSkynet.Infrastructure.Queries
{
    public class IdQueryArgs
    {
        public IdQueryArgs(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}