using BlueSkynet.Domain.Models;
using System;
using System.Collections.Generic;

namespace BlueSkynet.Domain.Data
{
    public interface IDataContext
    {
        IDataCollection<T> GetCollection<T>() where T : Entity;
    }

    public interface IDataCollection<T> where T : Entity
    {
        T FindById(Guid id);

        void Update(T entity);

        void Insert(T entity);

        void Insert(IEnumerable<T> entity);

        IEnumerable<T> FindAll();
    }
}