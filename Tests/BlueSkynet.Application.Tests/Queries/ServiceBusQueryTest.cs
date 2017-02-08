using BlueSkynet.TestUtilities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using BlueSkynet.Infrastructure.Queries;
using BlueSkynet.Infrastructure.Queries.ServiceBus;
using BlueSkynet.Infrastructure.ReadModels.ServiceBus;

namespace BlueSkynet.Application.Tests.Queries
{
    internal class ServiceBusQueryTest
    {
        [Theory, AutoFakeItEasyData]
        public void Execurte_ThreeServiceBusExsist_returnsThreeServiceBus(
            IEnumerable<ServiceBusItemListDto> data)
        {
            using (var db = FakeDb.CreateInMemoryDatabase())
            {
                Assert.IsNotEmpty(data);
                db.GetCollection<ServiceBusItemListDto>()
                    .Insert(data);

                var sut = new ServiceBusQuery(db);
                var actual = sut.Execute(new EmptyArgs());
                Assert.That(actual.Count(), Is.EqualTo(3));
            }
        }

        [Theory, AutoFakeItEasyData]
        public void Execurte_NoServiceBusExsist_returnEmpty()
        {
            using (var db = FakeDb.CreateInMemoryDatabase())
            {
                db.GetCollection<ServiceBusItemListDto>()
                    .Insert(new List<ServiceBusItemListDto>());

                var sut = new ServiceBusQuery(db);
                var actual = sut.Execute(new EmptyArgs());
                Assert.IsEmpty(actual);
            }
        }
    }
}