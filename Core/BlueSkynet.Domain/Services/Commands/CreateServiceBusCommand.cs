using BlueSkynet.Domain.Bus;
using BlueSkynet.Domain.Models.ServiceBus;
using BlueSkynet.Domain.Repository;
using System;

namespace BlueSkynet.Domain.Services.Commands
{
    public class CreateServiceBusCommand : Command
    {
        public readonly Guid Id;
        public readonly string ConnectionString;
        public readonly string Name;

        public CreateServiceBusCommand(Guid id, string connectionString, string name)
        {
            Id = id;
            ConnectionString = connectionString;
            Name = name;
        }
    }

    public class CreateServiceBusCommandHandler : ICommand<CreateServiceBusCommand>
    {
        private readonly IRepository<ServiceBusItem> _repository;

        public CreateServiceBusCommandHandler(IRepository<ServiceBusItem> repository)
        {
            _repository = repository;
        }

        public void Execute(CreateServiceBusCommand message)
        {
            var item = new ServiceBusItem(
                id: message.Id,
                connectionString: message.ConnectionString,
                name: message.Name);
            _repository.Save(item, 0);
        }
    }

    //public class CreateServiceBusCommand : ICommand<CreateServiceBusCommandArgs>
    //{
    //    private readonly IRepository<Models.Events.ServiceBusItem.ServiceBusItem> _serviceBusRepository;

    //    public CreateServiceBusCommand(IRepository<Models.Events.ServiceBusItem.ServiceBusItem> serviceBusRepository)
    //    {
    //        _serviceBusRepository = serviceBusRepository;
    //    }

    //    public void Execute(CreateServiceBusCommandArgs args)
    //    {
    //        args.ThrowIfNull(nameof(args));
    //        var item = new ServiceBusItem(args.Name);
    //        _serviceBusRepository.Save(item, 0);
    //    }
    //}

    //public class CreateServiceBusCommandArgs
    //{
    //    public CreateServiceBusCommandArgs(string connectionString)
    //    {
    //        Name = connectionString;
    //    }

    //    public string Name { get; set; }
    //}
}