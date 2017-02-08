using System;
using BlueSkynet.Domain.Data;
using BlueSkynet.Domain.Models;

namespace BlueSkynet.Infrastructure.ReadModels
{
    public abstract class ReadModelBase<T> where T : Entity
    {
        protected readonly IDataCollection<T> Collection;

        protected ReadModelBase(IDataContext db)
        {
            Collection = db.GetCollection<T>();
        }

        protected T Find(Guid id) => Collection.FindById(id);

        protected void Update(T item) => Collection.Update(item);

        protected void Insert(T item) => Collection.Insert(item);
    }
}