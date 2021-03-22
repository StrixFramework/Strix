namespace Strix.Abstractions.Builders
{
    public interface IBuilder<TBuildEntity>
    {
        TBuildEntity Build();
    }
}