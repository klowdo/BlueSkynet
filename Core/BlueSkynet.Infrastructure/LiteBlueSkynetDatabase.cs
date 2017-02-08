using BlueSkynet.Domain.Data;
using BlueSkynet.Domain.Models;
using LiteDB;
using System;
using System.IO;

namespace BlueSkynet.Infrastructure
{
    public class LiteBlueSkynetDatabase : IDataContext, IDisposable
    {
        private readonly LiteDatabase _db;

        public LiteBlueSkynetDatabase(string connectionname)
        {
            _db = new LiteDatabase(connectionname);
        }

        public LiteBlueSkynetDatabase(Stream stream)
        {
            _db = new LiteDatabase(stream);
        }

        public IDataCollection<T> GetCollection<T>() where T : Entity =>
            new LiteBlueSkynetCollection<T>(_db.GetCollection<T>(typeof(T).Name));

        public void Dispose() =>
           _db.Dispose();
    }
}