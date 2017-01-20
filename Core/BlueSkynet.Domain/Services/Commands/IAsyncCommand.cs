using System.Threading.Tasks;

namespace BlueSkynet.Domain.Services.Commands
{
    public interface IAsyncCommand<in TArgs>
    {
        Task ExecuteAsync(TArgs args);
    }
}