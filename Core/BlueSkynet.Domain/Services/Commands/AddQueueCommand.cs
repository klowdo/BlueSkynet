using BlueSkynet.Domain.Bus;
using BlueSkynet.Domain.Extentions;
using BlueSkynet.Domain.Models.ServiceBus;
using BlueSkynet.Domain.Repository;
using System;

namespace BlueSkynet.Domain.Services.Commands
{
    public class AddQueueCommand : Command
    {
        public AddQueueCommand(Guid id, string queueName)
        {
            Id = id;
            QueueName = queueName;
        }

        public Guid Id { get; set; }
        public string QueueName { get; set; }
    }

    public class AddQueueCommandHandler : ICommand<AddQueueCommand>
    {
        private readonly IRepository<ServiceBusState> _repository;

        public AddQueueCommandHandler(IRepository<ServiceBusState> repository)
        {
            _repository = repository;
        }

        public void Execute(AddQueueCommand args)
        {
            args.ThrowIfNull(nameof(args));
            var state = _repository.GetById(args.Id);
            var item = ServiceBus.CreateFromState(state);
            item.AddQueue(args.QueueName);
            _repository.Save(item.State, item.State.Version);
        }
    }
}