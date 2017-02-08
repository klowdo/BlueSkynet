using BlueSkynet.Domain.Data;
using BlueSkynet.Domain.Extentions;
using BlueSkynet.Infrastructure.ReadModels.ServiceBus;
using System.Collections.Generic;
using System.Linq;

namespace BlueSkynet.Infrastructure.Queries.ServiceBus
{
    public class ServiceBusQuery : IQuery<EmptyArgs, IEnumerable<ServiceBusItemListDto>>
    {
        private readonly IDataContext _db;

        public ServiceBusQuery(IDataContext db)
        {
            _db = db;
        }

        public IEnumerable<ServiceBusItemListDto> Execute(EmptyArgs args)
        {
            args.ThrowIfNull(nameof(args));

            var items = _db.GetCollection<ServiceBusItemListDto>()
                .FindAll()
                .ToList();
            return items;
        }
    }
}