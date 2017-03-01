using BlueSkynet.Domain.Messages;
using BlueSkynet.Infrastructure.ReadModels.ServiceBus;

namespace BlueSkynet.Infrastructure.Factories
{
    public class QueueMessageFactory
    {
        public static UpdateServiceBusMessage CreateUpdateMessage(ServiceBusConnectionRm model) => new UpdateServiceBusMessage
        {
            ConnectionSting = model.ConnectionString,
            EntitiyId = model.Id
        };
    }
}