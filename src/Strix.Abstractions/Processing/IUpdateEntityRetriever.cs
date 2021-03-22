namespace Strix.Abstractions.Processing
{
    public interface IUpdateEntityRetriever<TUpdate, TUpdateType>
        where TUpdate : IUpdate<TUpdateType>
    {
        TEntity RetrieveEntity<TEntity>(TUpdate update);
    }
}