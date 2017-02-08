using BlueSkynet.TestUtilities;
using NUnit.Framework;
using System;
using System.Linq;
using BlueSkynet.Domain.Models.ServiceBus.Events;
using BlueSkynet.Infrastructure.ReadModels.ServiceBus;

namespace BlueSkynet.Application.Tests.ReadModel.ServiceBus
{
    internal class ServiceBusDetailedItemRmTest
    {
        [Test, AutoFakeItEasyData]
        public void Handle_NameChangeServiceBusEvent_ChangesNameOnEntity(

         Guid id,
         string name,
         string newName
         )
        {
            using (var db = FakeDb.CreateInMemoryDatabase())
            {
                var sut = new ServiceBusDetailedItemView(db);

                sut.Handle(new ServiceBusCreated(id, "", name));
                sut.Handle(new ServiceBusRenamed(id, newName));

                var collection = db.GetCollection<ServiceBusDetailedItemRm>();
                var actual = collection.FindById(id);
                Assert.That(actual.Name, Is.EqualTo(newName));
            }
        }

        [Test, AutoFakeItEasyData]
        public void Handle_NewServiceBusEvent_CreatesNewEnitiyWithName(
            Guid id,
            string name
            )
        {
            using (var db = FakeDb.CreateInMemoryDatabase())
            {
                var sut = new ServiceBusDetailedItemView(db);
                sut.Handle(new ServiceBusCreated(id, "", name));

                var collection = db.GetCollection<ServiceBusDetailedItemRm>();
                var actual = collection.FindById(id);
                Assert.That(actual.Name, Is.EqualTo(name));
            }
        }

        [Test, AutoFakeItEasyData]
        public void Handle_NewServiceBusQuwuwEvent_CreateQueue(
          Guid id,
          string name,
          string serviceBusName
          )
        {
            using (var db = FakeDb.CreateInMemoryDatabase())
            {
                var sut = new ServiceBusDetailedItemView(db);
                sut.Handle(new ServiceBusCreated(id, "", name));
                sut.Handle(new ServiceBusQueueCreated(id, serviceBusName));

                var collection = db.GetCollection<ServiceBusDetailedItemRm>();
                var actual = collection.FindById(id);
                Assert.That(actual.Queues.First().Name, Is.EqualTo(serviceBusName));
                Assert.That(actual.Queues.First().MessageCount, Is.EqualTo(0));
                Assert.That(actual.Queues.First().DeadLetterMessateCount, Is.EqualTo(0));
            }
        }
    }
}