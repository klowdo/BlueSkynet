using BlueSkynet.Domain.Models.ServiceBus;
using BlueSkynet.Domain.Models.ServiceBus.Events;
using BlueSkynet.TestUtilities;
using NUnit.Framework;
using System;
using System.Linq;

namespace BlueSkynet.Domain.Tests.Models
{
    internal class ServiceBusItemTest
    {
        [Test]
        public void Apply_NonValidNameAndConectionstring_ThrowsArgumentException()
        {
            Assert.That(() => new ServiceBusItem(Guid.NewGuid(), null, null), Throws.ArgumentNullException.With.Message.Contain("name"));
            Assert.That(() => new ServiceBusItem(Guid.NewGuid(), null, "Test"), Throws.ArgumentNullException.With.Message.Contain("connectionString"));
        }

        [Theory, AutoFakeItEasyData]
        public void Apply_ValidNameAndConectionstring_SetsId(
            Guid id, string value)
        {
            var sut = new ServiceBusItem(id, value, value);
            Assert.IsTrue(sut.GetUncommittedChanges().Any(x => x is ServiceBusCreated));
            Assert.That(id, Is.EqualTo(sut.Id));
        }

        [Theory, AutoFakeItEasyData]
        public void Dactivate_AlreadyDeactivated_ThrowsInvalidOperationException(

            Guid id, string value)
        {
            var sut = new ServiceBusItem(id, value, value);
            sut.Deactivate();
            Assert.That(() => sut.Deactivate(), Throws.InvalidOperationException);
        }

        [Theory, AutoFakeItEasyData]
        public void ChangeName_NullName_ThrowsArgumentException(
           Guid id, string value)
        {
            var sut = new ServiceBusItem(id, value, value);
            Assert.That(() => sut.ChangeName(null), Throws.ArgumentNullException.With.Message.Contain("name"));
            Assert.That(() => sut.ChangeName(""), Throws.ArgumentException.With.Message.Contain("name"));
        }

        [Theory, AutoFakeItEasyData]
        public void ChangeName_ValidName_AppliesEvent(
           Guid id, string value)
        {
            var sut = new ServiceBusItem(id, value, value);

            sut.ChangeName(value);

            sut.GetUncommittedChanges().AssertContainsEvent(typeof(ServiceBusRenamed));
        }
    }
}