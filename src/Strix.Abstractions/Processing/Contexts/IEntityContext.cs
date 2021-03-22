namespace Strix.Abstractions.Processing.Contexts
{
    public interface IEntityContext<TUpdate, TUpdateType, TEntity>
        where TUpdate : IUpdate<TUpdateType>
    {
        TUpdate Update { get; }

        TEntity Entity { get; }
    }
}