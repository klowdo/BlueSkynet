namespace BlueSkynet.Domain.Tests
{
    internal class BusTest
    {
        //[Theory, AutoFakeItEasyData]
        //public void Test(
        //    [Frozen]Fake<IHandlesFactory> handelsFactory,
        //    EventBus sut,
        //    ServiceBusCreated @event
        //    )
        //{
        //    var dataContext = new BullshitDb();
        //    handelsFactory.CallsTo(factory => factory.Create<ServiceBusCreated>())
        //        .Returns(new ServiceBusHandler(dataContext));

        //    sut.Publish(@event);

        //    var actual =
        //        dataContext.ServiceBuses.SingleOrDefault(x => x.Name.Equals(@event.Name));

        //    Assert.IsNotEmpty(dataContext.ServiceBuses);

        //    Assert.NotNull(actual);
        //}
    }
}