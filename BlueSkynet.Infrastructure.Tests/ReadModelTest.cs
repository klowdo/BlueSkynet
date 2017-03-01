using BlueSkynet.Domain.Bus;
using BlueSkynet.Domain.Data;
using BlueSkynet.Domain.Models;
using BlueSkynet.Infrastructure.ReadModels;
using FakeItEasy;
using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;

namespace BlueSkynet.Infrastructure.Tests
{
    public abstract class ReadModelTest<TEntity, THandler> where THandler : ReadModelBase<TEntity> where TEntity : Entity, new()
    {
        private IDataContext _db;
        private Stream _dbStream;
        private THandler _handeler;

        protected void Given(TEntity entity = null, string message = null)
        {
            _dbStream = new MemoryStream();
            _db = new LiteBlueSkynetDatabase(_dbStream);
            _handeler = A.Fake<THandler>(options => options.WithArgumentsForConstructor(new[] { _db }));
        }

        protected void When(params Event[] events)
        {
            foreach (var @event in events)
            {
                var genericHandeler = typeof(IHandles<>);
                var specific = genericHandeler.MakeGenericType(@event.GetType());
                var handleMethod = specific.GetMethod("Handle",
                    BindingFlags.Instance | BindingFlags.Public);
                handleMethod.Invoke(_handeler, new object[] { @event });
            }
        }

        protected void Then(Guid id, Func<TEntity, bool> predicate)
        {
            Assert.IsNotEmpty(_db.GetCollection<TEntity>().FindAll());
            var entity = _db.GetCollection<TEntity>().FindById(id);
            Assert.NotNull(entity);
            Assert.IsTrue(predicate.Invoke(entity));
            _dbStream.Dispose();
        }
    }
}