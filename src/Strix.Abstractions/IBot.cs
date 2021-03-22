using System.Collections.Generic;
using System.Threading.Tasks;
using Strix.Abstractions.Processing;

namespace Strix.Abstractions
{
    public interface IBot<TUpdate, TUpdateType>
        where TUpdate : IUpdate<TUpdateType>
    {
        IUpdateEntityRetriever<TUpdate, TUpdateType> UpdateEntityRetriever { get; }

        IEnumerable<IEntityProcessor<TUpdate, TUpdateType>> GetEntityProcessors();

        Task Process(TUpdate update);
    }
}