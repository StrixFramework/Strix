using System.Threading.Tasks;
using Strix.Abstractions.Processing.Contexts;

namespace Strix.Abstractions.Processing
{
    public interface IEntityProcessor<TUpdate, TUpdateType> : IIdentifiable<string>
        where TUpdate : IUpdate<TUpdateType>
    {
        TUpdateType SupportedUpdateTypes { get; }
    }
    
    public interface IEntityProcessor<TUpdate, TUpdateType, TEntity> : IEntityProcessor<TUpdate, TUpdateType>
        where TUpdate : IUpdate<TUpdateType>
    {
        IEntityContextFactory<TUpdate, TUpdateType, TEntity> EntityContextFactory { get; } 

        Task Process(TUpdate update, TEntity entity);
    }
}