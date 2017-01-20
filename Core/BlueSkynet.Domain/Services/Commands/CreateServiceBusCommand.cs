using BlueSkynet.Domain.Bus;

namespace BlueSkynet.Domain.Services.Commands
{
    public class CreateServiceBusCommand : Command
    {
        public readonly string ConnectionString;

        public CreateServiceBusCommand(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }

    //public class CreateServiceBusCommand : ICommand<CreateServiceBusCommandArgs>
    //{
    //    private readonly IRepository<Models.Events.ServiceBus.ServiceBus> _serviceBusRepository;

    //    public CreateServiceBusCommand(IRepository<Models.Events.ServiceBus.ServiceBus> serviceBusRepository)
    //    {
    //        _serviceBusRepository = serviceBusRepository;
    //    }

    //    public void Execute(CreateServiceBusCommandArgs args)
    //    {
    //        args.ThrowIfNull(nameof(args));
    //        var item = new ServiceBus(args.ConnectionString);
    //        _serviceBusRepository.Save(item, 0);
    //    }
    //}

    //public class CreateServiceBusCommandArgs
    //{
    //    public CreateServiceBusCommandArgs(string connectionString)
    //    {
    //        ConnectionString = connectionString;
    //    }

    //    public string ConnectionString { get; set; }
    //}
}