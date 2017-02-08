using BlueSkynet.Domain.Exceptions;
using BlueSkynet.TestUtilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSkynet.Infrastructure.Queries.ServiceBus;
using BlueSkynet.Infrastructure.ReadModels.ServiceBus;

namespace BlueSkynet.Application.Tests.Queries
{
    internal class ServiceBusByIdQueryTest
    {
        [Theory, AutoFakeItEasyData]
        public void Execute_OneValidAmongOthers_ReturnsOne(
            IEnumerable<ServiceBusItemListDto> data
            )
        {
            var id = data.First().Id;
            var name = data.First().Name;
            using (var db = FakeDb.CreateInMemoryDatabase())
            {
                var collection = db.GetCollection<ServiceBusItemListDto>();
                collection.Insert(data);
                var sut = new ServiceBusByIdQuery(db);

                var actual = sut.Execute(new ServiceBusByIdQueryArgs(id));
                Assert.That(actual.Name, Is.EqualTo(name));
            }
        }

        [Theory, AutoFakeItEasyData]
        public void Execute_DoesNotExsist_TrowsNotFoundException(
            IEnumerable<ServiceBusItemListDto> data
            )
        {
            using (var db = FakeDb.CreateInMemoryDatabase())
            {
                var collection = db.GetCollection<ServiceBusItemListDto>();
                collection.Insert(data);
                var sut = new ServiceBusByIdQuery(db);

                Assert.That(() => sut.Execute(new ServiceBusByIdQueryArgs(Guid.NewGuid())), Throws.Exception.TypeOf<NotFoundException>()); ;
            }
        }
    }
}