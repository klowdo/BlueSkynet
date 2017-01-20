using System;

namespace BlueSkynet.Domain.Repository
{
    public interface IRepository<out T> where T : AggregateRoot, new()
    {
        void Save(AggregateRoot aggregate, int expectedVersion);

        T GetById(Guid id);
    }
}