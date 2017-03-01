using BlueSkynet.Domain.Data;
using BlueSkynet.Domain.Exceptions;
using BlueSkynet.Domain.Extentions;
using BlueSkynet.Infrastructure.ReadModels.ServiceBus;

namespace BlueSkynet.Infrastructure.Queries.ServiceBus
{
    public class ServiveBusDetailedViewQueryById : IQuery<IdQueryArgs, ServiceBusDetailedItemRm>
    {
        private readonly IDataContext _db;

        public ServiveBusDetailedViewQueryById(IDataContext db)
        {
            _db = db;
        }

        public ServiceBusDetailedItemRm Execute(IdQueryArgs args)
        {
            args.ThrowIfNull(nameof(args));
            var result = _db.GetCollection<ServiceBusDetailedItemRm>().FindById(args.Id);
            if (result.IsNull()) throw new NotFoundException($"Could not find model with id: {args.Id}");
            return result;
        }
    }
}