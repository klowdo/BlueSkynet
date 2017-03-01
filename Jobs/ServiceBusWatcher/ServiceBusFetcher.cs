using BlueSkynet.Domain.Services;
using BlueSkynet.Infrastructure.Factories;
using BlueSkynet.Infrastructure.Queries;
using BlueSkynet.Infrastructure.Queries.ServiceBus;
using BlueSkynet.Infrastructure.ReadModels.ServiceBus;
using System.Collections.Generic;
using System.Linq;

namespace ServiceBusWatcher
{
    public class ServiceBusFetcher
    {
        private readonly IMessagePublisher _messagePublisher;
        private readonly IQuery<EmptyArgs, IEnumerable<ServiceBusConnectionRm>> _serviceBusConnectionQuery;

        public ServiceBusFetcher(
            IMessagePublisher messagePublisher,
            IQuery<EmptyArgs, IEnumerable<ServiceBusConnectionRm>> serviceBusConnectionQuery
            )
        {
            _messagePublisher = messagePublisher;
            _serviceBusConnectionQuery = serviceBusConnectionQuery;
        }

        public void FetchServiceBusesAndPublishUpdateRequest()
        {
            var entities = _serviceBusConnectionQuery.Execute(new EmptyArgs());
            var messages = entities.Select(QueueMessageFactory.CreateUpdateMessage);
            _messagePublisher.Send(messages);
        }
    }
}