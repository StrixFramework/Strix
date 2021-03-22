using Strix.Abstractions.Processing;

namespace Strix.Abstractions.Builders
{
    public interface IBotBuilder<TUpdate, TUpdateType> : IBuilder<IBot<TUpdate, TUpdateType>>
        where TUpdate : IUpdate<TUpdateType>
    {
        IBotBuilder<TUpdate, TUpdateType> WithEntityProcessor<TEntity>(IEntityProcessor<TUpdate, TUpdateType, TEntity> entityProcessor);
    }
}