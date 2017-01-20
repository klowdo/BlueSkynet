using BlueSkynet.Domain.Bus;
using BlueSkynet.Domain.Data;
using BlueSkynet.Domain.Models.Events.ServiceBus;
using BlueSkynet.Domain.Models.ServiceBus;
using System.Linq;

namespace BlueSkynet.Domain.Handlers
{
    public class ServiceBusHandler : IHandles<ServiceBusCreated>, IHandles<ServiceBusQueueCreated>
    {
        private readonly IDataContext _data;

        public ServiceBusHandler(IDataContext data)
        {
            _data = data;
        }

        public void Handle(ServiceBusCreated message)
        {
            _data.ServiceBuses.Add(new ServiceBusDto(
                id: message.Id,
                connectionString: message.ConnectionString)
                 );
        }

        public void Handle(ServiceBusQueueCreated message)
        {
            var servicebus = _data.ServiceBuses.SingleOrDefault(x => x.Id == message.Id);
            servicebus.Queues.Add(new Queue(message.Name));
        }
    }
}