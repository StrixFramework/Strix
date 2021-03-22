namespace Strix.Abstractions.Builders
{
    public interface IBuilder<TEntity, TEntityBuildOptions>
    {
        TEntity Build(TEntityBuildOptions buildOptions);
    }
}