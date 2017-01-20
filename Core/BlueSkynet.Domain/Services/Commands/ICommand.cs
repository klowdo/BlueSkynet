namespace BlueSkynet.Domain.Services.Commands
{
    public interface ICommand<in TArgs>
    {
        void Execute(TArgs args);
    }
}