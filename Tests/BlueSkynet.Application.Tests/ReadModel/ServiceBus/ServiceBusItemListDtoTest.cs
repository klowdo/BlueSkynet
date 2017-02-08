using BlueSkynet.Domain.Models.ServiceBus.Events;
using BlueSkynet.TestUtilities;
using NUnit.Framework;
using System;
using BlueSkynet.Infrastructure.ReadModels.ServiceBus;

namespace BlueSkynet.Application.Tests.ReadModel.ServiceBus
{
    internal class ServiceBusItemListDtoTest
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
                var sut = new ServiceBusItemListView(db);

                sut.Handle(new ServiceBusCreated(id, "", name));
                sut.Handle(new ServiceBusRenamed(id, newName));

                var collection = db.GetCollection<ServiceBusItemListDto>();
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
                var sut = new ServiceBusItemListView(db);
                sut.Handle(new ServiceBusCreated(id, "", name));

                var collection = db.GetCollection<ServiceBusItemListDto>();
                var actual = collection.FindById(id);
                Assert.That(actual.Name, Is.EqualTo(name));
            }
        }
    }
}