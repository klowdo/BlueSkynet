using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueSkynet.Domain.Services
{
    public interface IServiceBusProvider
    {
        Task<IEnumerable<string>> GetQueuesAsync();

        Task<string> GetQueueAsync();
    }
}