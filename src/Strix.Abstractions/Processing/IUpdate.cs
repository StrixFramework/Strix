namespace Strix.Abstractions.Processing
{
    public interface IUpdate<TUpdateType>
    {
        TUpdateType Type { get; }
    }
}