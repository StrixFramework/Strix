using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Strix.Widgets.Abstractions.Scopes;
using Strix.Widgets.Utils;

namespace Strix.Widgets.Scopes
{
    public class WidgetStateChangeScope<TWidgetState, TWidgetStateChangeType> : IWidgetStateChangeScope<TWidgetState, TWidgetStateChangeType>
    {
        public TWidgetState State { get; private set; }
        
        public WidgetStateChangeScopeMode Mode { get; }

        private readonly Channel<WidgetStateChangeEntry<TWidgetState, TWidgetStateChangeType>> _stateUpdateChannel;
        private readonly Semaphore _waiter;

        private readonly CancellationTokenSource _autoUpdateTokenSource;
        
        private readonly bool _notifyOnDispose;
        
        public WidgetStateChangeScope(
            TWidgetState state, 
            WidgetStateChangeScopeMode mode,
            Channel<WidgetStateChangeEntry<TWidgetState, TWidgetStateChangeType>> stateUpdateChannel, 
            Semaphore waiter)
        {
            Mode = mode;
            State = state.Copy();
            _stateUpdateChannel = stateUpdateChannel;
            _waiter = waiter;

            _notifyOnDispose = mode.HasFlag(WidgetStateChangeScopeMode.OnDispose);

            if (mode.HasFlag(WidgetStateChangeScopeMode.Automatical)
            && State is INotifyWidgetStateChanged<TWidgetState, TWidgetStateChangeType> notifyWidgetStateChanged)
            {
                notifyWidgetStateChanged.OnStateChanged += NotifyWidgetStateChangedHandler;
            }

            _autoUpdateTokenSource = new CancellationTokenSource();
        }

        private void NotifyWidgetStateChangedHandler(object _, NotifyWidgetStateChangedEventArgs<TWidgetState, TWidgetStateChangeType> args)
        {
            NotifyWidgetStateChanged(args.Entry.ChangeType);
        }
        
        public virtual void NotifyWidgetStateChanged(TWidgetStateChangeType widgetStateChangeType)
        {
            _stateUpdateChannel.Writer.WaitToWriteAsync().AsTask().Wait();

            _stateUpdateChannel.Writer.WriteAsync(new WidgetStateChangeEntry<TWidgetState, TWidgetStateChangeType>()
            {
                State = State,
                ChangeType = widgetStateChangeType
            }).AsTask().Wait();

            _waiter.WaitOne();
        }

        public virtual IWidgetStateChangeScope<TWidgetState, TWidgetStateChangeType> AutoUpdate(TimeSpan updateInterval, CancellationToken cancellationToken = default)
        {
            CancellationTokenSource autoUpdateCancellationTokenSource = 
                CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _autoUpdateTokenSource.Token);

            CancellationToken autoUpdateCancellationToken = autoUpdateCancellationTokenSource.Token;
            
            Task.Factory.StartNew(async () =>
            {
                while (!autoUpdateCancellationToken.IsCancellationRequested)
                {
                    NotifyWidgetStateChanged(default);
                    await Task.Delay(updateInterval, autoUpdateCancellationToken);
                }
            }, autoUpdateCancellationToken);

            return this;
        }

        public void Dispose()
        {
            if (!_autoUpdateTokenSource.IsCancellationRequested)
            {
                _autoUpdateTokenSource.Cancel();
            }

            if (_notifyOnDispose)
            {
                NotifyWidgetStateChanged(default);
            }
            
            State = default;
            
            if (Mode.HasFlag(WidgetStateChangeScopeMode.Automatical)
                && State is INotifyWidgetStateChanged<TWidgetState, TWidgetStateChangeType> notifyWidgetStateChanged)
            {
                notifyWidgetStateChanged.OnStateChanged -= NotifyWidgetStateChangedHandler;
            }
            
        }

        public ValueTask DisposeAsync()
        {
            return new ValueTask(Task.Factory.StartNew(Dispose));
        }
    }
}