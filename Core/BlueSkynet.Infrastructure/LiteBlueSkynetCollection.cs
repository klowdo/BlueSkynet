using BlueSkynet.Domain.Data;
using BlueSkynet.Domain.Models;
using LiteDB;
using System;
using System.Collections.Generic;

namespace BlueSkynet.Infrastructure
{
    public class LiteBlueSkynetCollection<T> : IDataCollection<T> where T : Entity
    {
        private readonly LiteCollection<T> _collection;

        public LiteBlueSkynetCollection(LiteCollection<T> collection)
        {
            _collection = collection;
        }

        public T FindById(Guid id) => _collection.FindById(id);

        public void Update(T entity) => _collection.Update(entity.Id, entity);

        public void Insert(T entity) => _collection.Insert(entity.Id, entity);

        public void Insert(IEnumerable<T> entity) => _collection.Insert(entity);

        public IEnumerable<T> FindAll() => _collection.FindAll();
    }
}