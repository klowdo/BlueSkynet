using BlueSkynet.Domain.Exceptions;
using BlueSkynet.Domain.Models.ServiceBus;
using BlueSkynet.Domain.Models.ServiceBus.Events;
using BlueSkynet.TestUtilities;
using NUnit.Framework;

namespace BlueSkynet.Domain.Tests.Models
{
    internal class SertviceBusItemTopicTest
    {
        [Test]
        public void When_Try_Add_Topic_With_Null_Or_empty_Name_Throw_Exception()
        {
            var sut = new ServiceBus();
            Assert.That(() => sut.AddTopic(""), Throws.ArgumentException);
            Assert.That(() => sut.AddTopic(null), Throws.ArgumentNullException);
        }

        [Test]
        public void When_Try_Add_Topic_With_Valid_Name_Applies_ServiceBusEventCreated()
        {
            var sut = new ServiceBus();

            sut.AddTopic("ValidName");

            sut.GetUncommittedChanges().AssertContainsEvent(typeof(ServiceBusTopicCreated));
        }

        [Test]
        public void When_Try_Add_Topic_With_Valid_Name_Adds_Topic_To_List()
        {
            const string validName = "ValidName";
            var sut = new ServiceBus();

            sut.AddTopic(validName);

            Assert.IsTrue(sut.Topics.Exists(x => x.Name.Equals(validName)));
        }

        [Test]
        public void When_Try_Add_Topic_With_Same_Name_Trows_InvalidOperationException()
        {
            const string validName = "ValidName";
            var sut = new ServiceBus();
            sut.AddTopic(validName);

            Assert.That(() => sut.AddTopic(validName), Throws.InvalidOperationException);
        }

        [Test]
        public void When_Try_Add_Topic_Subscriber_That_Not_Exsist_Throws_NotFoundException()
        {
            const string validName = "ValidName";
            var sut = new ServiceBus();

            Assert.That(() => sut.AddTopicSubscriber(validName, validName), Throws.TypeOf<NotFoundException>());
        }

        [Test]
        public void When_Try_Add_Topic_Subscriber_Not_Valid_Name_Throws_Exception()
        {
            const string validName = "ValidName";
            var sut = new ServiceBus();
            sut.AddTopic(validName);

            Assert.That(() => sut.AddTopicSubscriber(validName, ""), Throws.ArgumentException);
            Assert.That(() => sut.AddTopicSubscriber(validName, null), Throws.ArgumentNullException);
        }

        [Test]
        public void When_Try_Add_Topic_Subscriber_Valid_Name_Applies_Event()
        {
            const string validName = "ValidName";
            var sut = new ServiceBus();
            sut.AddTopic(validName);

            sut.AddTopicSubscriber(validName, validName);

            sut.GetUncommittedChanges().AssertContainsEvent(typeof(ServiceBusTopicSubscriberCreated));
        }

        [Test]
        public void When_Try_Add_Topic_Subscriber_Valid_Name_That_Already_Exsist_Throws_InvadlidOperationException()
        {
            const string validName = "ValidName";
            var sut = new ServiceBus();
            sut.AddTopic(validName);

            sut.AddTopicSubscriber(validName, validName);

            Assert.That(() => sut.AddTopicSubscriber(validName, validName), Throws.InvalidOperationException);
        }

        [Test]
        public void When_Try_Update_Topic_Subscriber_Queue_Count_With_Negative_Throw_InvalidOperation()
        {
            const string validName = "ValidName";
            var sut = new ServiceBus();
            sut.AddTopic(validName);
            sut.AddTopicSubscriber(validName, validName);

            Assert.That(() => sut.UpdateTopicSubsciberQueueCount(validName, validName, -3), Throws.InvalidOperationException);
        }

        [Test]
        public void When_Try_Update_Topic_Subscriber_DeadLetter_Queue_Count_With_Negative_Throw_InvalidOperation()
        {
            const string validName = "ValidName";
            var sut = new ServiceBus();
            sut.AddTopic(validName);
            sut.AddTopicSubscriber(validName, validName);

            Assert.That(() => sut.UpdateTopicSubsciberDeadLetterQueueCount(validName, validName, -3), Throws.InvalidOperationException);
        }

        [Test]
        public void When_Try_Update_Topic_Subscriber_Queue_Count_With_Valid_Applies_Event()
        {
            const string validName = "ValidName";
            var sut = new ServiceBus();
            sut.AddTopic(validName);
            sut.AddTopicSubscriber(validName, validName);

            sut.UpdateTopicSubsciberQueueCount(validName, validName, 3);

            sut.GetUncommittedChanges().AssertContainsEvent(typeof(ServiceBusTopicSubscriberQueueCountChanged));
        }

        [Test]
        public void When_Try_Update_Topic_Subscriber_DeadLetter_Queue_Count_With_Valid_Applies_Event()
        {
            const string validName = "ValidName";
            var sut = new ServiceBus();
            sut.AddTopic(validName);
            sut.AddTopicSubscriber(validName, validName);

            sut.UpdateTopicSubsciberQueueCount(validName, validName, 3);

            sut.GetUncommittedChanges().AssertContainsEvent(typeof(ServiceBusTopicSubscriberQueueCountChanged));
        }

        [Test]
        public void When_try_Removing_Topic_AppliesEvent()
        {
            const string validName = "ValidName";
            var sut = new ServiceBus();

            sut.RemoveTopic(validName);

            sut.GetUncommittedChanges().AssertContainsEvent(typeof(ServiceBusTopicRemoved));
        }

        [Test]
        public void When_try_Removing_Topic_Subscription_AppliesEvent()
        {
            const string validName = "ValidName";
            var sut = new ServiceBus();

            sut.RemoveTopicSubscriber(validName, validName);

            sut.GetUncommittedChanges().AssertContainsEvent(typeof(ServiceBusTopicSubscriberRemoved));
        }
    }
}