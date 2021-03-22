using System.Threading.Tasks;

namespace Strix.Abstractions.Builders
{
    public interface IAsyncBuilder<TEntity>
    {
        Task<TEntity> BuildAsync();
    }
}