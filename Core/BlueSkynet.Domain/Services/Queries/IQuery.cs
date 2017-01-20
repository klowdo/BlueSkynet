namespace BlueSkynet.Domain.Services.Queries
{
    public interface IQuery<in TQuery, out TResult>
    {
        TResult Execute(TQuery args);
    }
}