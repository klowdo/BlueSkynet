using BlueSkynet.Domain.Exceptions;
using BlueSkynet.Domain.Models.ServiceBus;
using BlueSkynet.Domain.Models.ServiceBus.Events;
using BlueSkynet.TestUtilities;
using NUnit.Framework;
using System;

namespace BlueSkynet.Domain.Tests.Models
{
    internal class ServiceBusItemQueueTest
    {
        [Theory, AutoFakeItEasyData]
        public void When_Try_To_AddQueue_With_Empty_NameOrNull__ArgumentException_Is_Thrown(
          ServiceBus sut)
        {
            Assert.That(() => sut.AddQueue(""), Throws.ArgumentException.With.Message.Contain("name"));
            Assert.That(() => sut.AddQueue(null), Throws.ArgumentNullException.With.Message.Contain("name"));
        }

        [Theory, AutoFakeItEasyData]
        public void When_Try_To_Add_Queue_That_Alredy_Exsist_InvalidOperationExceptionn_Is_Thrown(
           Guid id, string value)
        {
            var sut = new ServiceBus(id, value, value);
            sut.AddQueue(value);
            Assert.That(() => sut.AddQueue(value), Throws.InvalidOperationException);
        }

        [Theory, AutoFakeItEasyData]
        public void When_Try_To_Update_QueueCount_And_Queue_Does_Not_Exsist_Throw_NotFoundException(
          string name)
        {
            var sut = new ServiceBus();

            Assert.That(() => sut.UpdateDeadLetterQueueCount(name, 1), Throws.TypeOf<NotFoundException>());
        }

        [Test]
        public void When_Try_To_Update_QueueCount_And_Queue_Count_IsNegative_Throw_InvalidOperationException()
        {
            const string name = "MyQUEUE";
            var sut = new ServiceBus();
            sut.AddQueue(name);

            Assert.That(() => sut.UpdateQueueCount(name, -3), Throws.InvalidOperationException.With.Message.Contain("negative"));
            Assert.That(() => sut.UpdateDeadLetterQueueCount(name, -3), Throws.InvalidOperationException.With.Message.Contain("negative"));
        }

        [Test]
        public void When_Try_To_Update_QueueCount_MustApplyEvent()
        {
            const string name = "MyQUEUE";
            var sut = new ServiceBus();
            sut.AddQueue(name);
            sut.UpdateQueueCount(name, 3);
            sut.State.GetUncommittedChanges().AssertContainsEvent(typeof(ServiceBusQueueCountChange));
        }

        [Test]
        public void When_Try_To_Update_DeadLetterQueueCount_MustApplyEvent()
        {
            const string name = "MyQUEUE";
            var sut = new ServiceBus();
            sut.AddQueue(name);
            sut.UpdateDeadLetterQueueCount(name, 3);
            sut.State.GetUncommittedChanges().AssertContainsEvent(typeof(ServiceBusDeadLetterQueueCountChanged));
        }

        [Test]
        public void When_Try_To_RemoveQueue_QueueNameDoesNotExsist_Throws_NotFound()
        {
            const string name = "MyQUEUE";
            var sut = new ServiceBus();

            Assert.That(() => sut.RemoveQueue(name), Throws.TypeOf<NotFoundException>());
        }

        [Test]
        public void When_Try_To_RemoveQueue_Queue_Does_Not_Exsist_After()
        {
            const string name = "MyQUEUE";
            var sut = new ServiceBus();
            sut.AddQueue(name);

            sut.RemoveQueue(name);

            sut.State.GetUncommittedChanges().AssertContainsEvent(typeof(ServiceBusQueueRemoved));
        }
    }
}