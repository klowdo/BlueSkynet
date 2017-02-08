using System;
using BlueSkynet.Domain.Data;
using BlueSkynet.Domain.Exceptions;
using BlueSkynet.Domain.Extentions;
using BlueSkynet.Infrastructure.ReadModels.ServiceBus;

namespace BlueSkynet.Infrastructure.Queries.ServiceBus
{
    public class ServiceBusByIdQuery : IQuery<ServiceBusByIdQueryArgs, ServiceBusItemListDto>
    {
        private readonly IDataContext _db;

        public ServiceBusByIdQuery(
            IDataContext db
            )
        {
            _db = db;
        }

        public ServiceBusItemListDto Execute(ServiceBusByIdQueryArgs args)
        {
            args.ThrowIfNull(nameof(args));
            var item = _db.GetCollection<ServiceBusItemListDto>()
                .FindById(args.Id);
            if (item.IsNull()) throw new NotFoundException($"ServiceBus With Id {args.Id} could not be found");
            return item;
        }
    }

    public class ServiceBusByIdQueryArgs
    {
        public ServiceBusByIdQueryArgs(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}