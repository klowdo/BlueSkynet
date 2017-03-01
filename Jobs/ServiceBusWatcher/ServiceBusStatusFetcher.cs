using BlueSkynet.Domain.Messages;
using BlueSkynet.Domain.Services.Commands;
using BlueSkynet.Infrastructure.Queries;
using BlueSkynet.Infrastructure.Queries.ServiceBus;
using BlueSkynet.Infrastructure.ReadModels.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBusWatcher
{
    internal class ServiceBusStatusFetcher
    {
        private readonly IQuery<IdQueryArgs, ServiceBusDetailedItemRm> _serviceBusQuery;
        private readonly ICommand<AddQueueCommand> _addQueueCommand;

        public ServiceBusStatusFetcher(
            IQuery<IdQueryArgs, ServiceBusDetailedItemRm> serviceBusQuery,
            ICommand<AddQueueCommand> addQueueCommand)
        {
            _serviceBusQuery = serviceBusQuery;
            _addQueueCommand = addQueueCommand;
        }

        public async Task FetchSatusForServiceBus([ServiceBusTrigger(UpdateServiceBusMessage.QueueName)] BrokeredMessage message)
        {
            var body = message.GetBody<UpdateServiceBusMessage>();
            var manager = NamespaceManager.CreateFromConnectionString(body.ConnectionSting);
            var queues = await manager.GetQueuesAsync();
            foreach (var queueDescription in queues)
            {
                var queue = manager.GetQueue(queueDescription.Path);
                var rm = _serviceBusQuery.Execute(new IdQueryArgs(body.EntitiyId));
                if (!rm.Queues.Any(x => x.Name.Equals(queue.Path)))
                    _addQueueCommand.Execute(new AddQueueCommand(body.EntitiyId, queue.Path));
            }
        }
    }
}