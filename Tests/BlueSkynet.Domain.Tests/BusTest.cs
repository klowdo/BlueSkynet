using BlueSkynet.Domain.Data;
using BlueSkynet.Domain.EventStore;
using BlueSkynet.Domain.Handlers;
using BlueSkynet.Domain.Models.Events.ServiceBus;
using BlueSkynet.Domain.Services;
using FakeItEasy;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using System.Linq;

namespace BlueSkynet.Domain.Tests
{
    internal class BusTest
    {
        [Theory, AutoFakeItEasyData]
        public void Test(
            [Frozen]Fake<IHandlesFactory> handelsFactory,
            EventBus sut,
            ServiceBusCreated @event
            )
        {
            var dataContext = new BullshitDb();
            handelsFactory.CallsTo(factory => factory.Create<ServiceBusCreated>())
                .Returns(new ServiceBusHandler(dataContext));

            sut.Publish(@event);

            var actual =
                dataContext.ServiceBuses.SingleOrDefault(x => x.ConnectionString.Equals(@event.ConnectionString));

            Assert.IsNotEmpty(dataContext.ServiceBuses);

            Assert.NotNull(actual);
        }
    }
}