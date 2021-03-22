using System;
using System.Threading;

namespace Strix.Widgets.Abstractions.Scopes
{
    public interface IWidgetStateChangeScope<TWidgetState, TWidgetStateChangeType> :
        INotifyWidgetStateChanged<TWidgetStateChangeType>,
        IDisposable,
        IAsyncDisposable
    {
        TWidgetState State { get; }
        
        WidgetStateChangeScopeMode Mode { get; }

        IWidgetStateChangeScope<TWidgetState, TWidgetStateChangeType> AutoUpdate(TimeSpan updateInterval, CancellationToken cancellationToken = default);
    }
}