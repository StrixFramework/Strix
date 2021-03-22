using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using Strix.Widgets.Abstractions;
using Strix.Widgets.Abstractions.Scopes;
using Strix.Widgets.Extensions;
using Strix.Widgets.Scopes;
using Strix.Widgets.Utils;

//Auto-reload state
        //Manual and automatical change realizations
        //Propertyless widgets. e.g. without result, creating options, etc. WidgetBase<...>
        //disposables, cancellationtokens, 


namespace Strix.Widgets
{
    public class WidgetController<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult> :
        IWidgetController<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult>
    where TWidget : IWidget<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult>
    {
        public IEnumerable<WidgetStateChangeEntry<TWidgetState, TWidgetStateChangeType>> Changes => _changes.AsEnumerable();

        private readonly List<WidgetStateChangeEntry<TWidgetState, TWidgetStateChangeType>> _changes;
        private readonly Channel<WidgetStateChangeEntry<TWidgetState, TWidgetStateChangeType>> _stateUpdateChannel;
        private readonly TWidgetState _state;
        private readonly Semaphore _waiterSemaphore;
        public WidgetController(IWidget<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult> widget, TWidgetState initialState)
        {
            _state = initialState;
            
            _stateUpdateChannel = Channel.CreateBounded<WidgetStateChangeEntry<TWidgetState, TWidgetStateChangeType>>(
                new BoundedChannelOptions(1)
                {
                    FullMode = BoundedChannelFullMode.Wait
                });
            _waiterSemaphore = new Semaphore(0, 1);

            _stateUpdateChannel.Reader.ReadUntilCancelledAsync(CancellationToken.None, async (entry, _) =>
            {
                try
                {
                    await widget.OnUpdated(entry.State.Copy(), entry.ChangeType.Copy());
                    _changes.Add(entry.Copy());
                }
                catch { }
                finally
                {
                    _waiterSemaphore.Release();
                }
            });

            _changes = new List<WidgetStateChangeEntry<TWidgetState, TWidgetStateChangeType>>();
        }
        
        
        public IWidgetStateChangeScope<TWidgetState, TWidgetStateChangeType> BeginStateChangeScope(WidgetStateChangeScopeMode stateChangeScopeMode = WidgetStateChangeScopeMode.All)
        {
            return new WidgetStateChangeScope<TWidgetState, TWidgetStateChangeType>(
                _state, 
                stateChangeScopeMode, 
                _stateUpdateChannel,
                _waiterSemaphore);
        }
    }
}