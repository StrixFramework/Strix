namespace Strix.Abstractions.Processing.Contexts
{
    public interface IEntityContextFactory<TUpdate, TUpdateType, TEntity> where TUpdate : IUpdate<TUpdateType>
    {
        IEntityContext<TUpdate, TUpdateType, TEntity> Create(TUpdate update, TEntity entity);
    }
}