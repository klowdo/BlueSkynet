using System;

namespace BlueSkynet.Domain.Messages
{
    public class UpdateServiceBusMessage : QueueMessage
    {
        public const string QueueName = nameof(UpdateServiceBusMessage);

        public Guid EntitiyId { get; set; }
        public string ConnectionSting { get; set; }
    }
}