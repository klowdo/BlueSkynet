using BlueSkynet.Domain.Data;
using BlueSkynet.Domain.Extentions;
using BlueSkynet.Infrastructure.ReadModels.ServiceBus;
using System.Collections.Generic;

namespace BlueSkynet.Infrastructure.Queries.ServiceBus
{
    internal class ServiceBusConnectionQuery : IQuery<EmptyArgs, IEnumerable<ServiceBusConnectionRm>>
    {
        private readonly IDataContext _db;

        public ServiceBusConnectionQuery(IDataContext db)
        {
            _db = db;
        }

        public IEnumerable<ServiceBusConnectionRm> Execute(EmptyArgs args)
        {
            args.ThrowIfNull(nameof(args));
            var collection = _db.GetCollection<ServiceBusConnectionRm>();
            return collection.FindAll();
        }
    }
}