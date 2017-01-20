using BlueSkynet.Domain.Models.ServiceBus;
using System.Collections.Generic;

namespace BlueSkynet.Domain.Data
{
    public interface IDataContext
    {
        IList<ServiceBusDto> ServiceBuses { get; }
    }

    public class BullshitDb : IDataContext
    {
        public BullshitDb()
        {
            ServiceBuses = new List<ServiceBusDto>();
        }

        public IList<ServiceBusDto> ServiceBuses { get; }
    }
}