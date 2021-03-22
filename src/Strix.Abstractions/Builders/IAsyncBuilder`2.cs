using System.Threading.Tasks;

namespace Strix.Abstractions.Builders
{
    public interface IAsyncBuilder<TEntity, TEntityBuildOptions>
    {
        Task<TEntity> BuildAsync(TEntityBuildOptions buildOptions);
    }
}