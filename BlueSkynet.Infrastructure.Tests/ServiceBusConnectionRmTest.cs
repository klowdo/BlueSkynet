using BlueSkynet.Domain.Models.ServiceBus.Events;
using BlueSkynet.Infrastructure.ReadModels.ServiceBus;
using BlueSkynet.TestUtilities;
using NUnit.Framework;
using System;

namespace BlueSkynet.Infrastructure.Tests
{
    public class ServiceBusConnectionRmTest : ReadModelTest<ServiceBusConnectionRm, ServiveBusConnectionView>
    {
        [Theory, AutoFakeItEasyData]
        public void When_ServiceBusCreatedEvent_Create_ReadModelEntity(Guid id, string name, string connectionstring)
        {
            Given(message: "New inital state");
            When(new ServiceBusCreated(id, connectionstring, name));
            Then(id, rm => rm.Name.Equals(name) && rm.ConnectionString.Equals(connectionstring));
        }

        [Theory, AutoFakeItEasyData]
        public void When_ServiceBusRenamedEvent_Rename_ReadModelEntity(Guid id, string name, string connectionstring, string newName)
        {
            Given(message: "Existing changename ");
            When(new ServiceBusCreated(id, connectionstring, name), new ServiceBusRenamed(id, newName));
            Then(id, rm => rm.Name.Equals(newName) && rm.ConnectionString.Equals(connectionstring));
        }
    }
}