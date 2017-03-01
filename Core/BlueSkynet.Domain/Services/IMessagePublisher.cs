using System.Collections.Generic;
using BlueSkynet.Domain.Messages;

namespace BlueSkynet.Domain.Services
{
    public interface IMessagePublisher
    {
        void Send(QueueMessage message);

        void Send(IEnumerable<QueueMessage> messages);

        void Send(TopicMessage message);

        void Send(IEnumerable<TopicMessage> messages);
    }
}