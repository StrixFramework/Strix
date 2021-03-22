using System;
using Strix.Widgets.Abstractions.Scopes;
using Strix.Widgets.Utils;

namespace Strix.Widgets.Scopes
{
    public abstract class NotifyWidgetStateChangedBase<TWidgetStateChangeType> : INotifyWidgetStateChanged<TWidgetStateChangeType>
    {
        public abstract void NotifyWidgetStateChanged(TWidgetStateChangeType widgetStateChangeType);
    }
    
    public abstract class NotifyWidgetStateChangedBase<TWidgetState, TWidgetStateChangeType> : 
        NotifyWidgetStateChangedBase<TWidgetStateChangeType>,
        INotifyWidgetStateChanged<TWidgetState, TWidgetStateChangeType>
        where TWidgetState : INotifyWidgetStateChanged<TWidgetState, TWidgetStateChangeType>
    {
        public override void NotifyWidgetStateChanged(TWidgetStateChangeType widgetStateChangeType)
        {
            if (this is TWidgetState state)
            {
                OnStateChanged?.Invoke(this, new NotifyWidgetStateChangedEventArgs<TWidgetState, TWidgetStateChangeType>(
                    new WidgetStateChangeEntry<TWidgetState, TWidgetStateChangeType>()
                    {
                        State = state.Copy(),
                        ChangeType = widgetStateChangeType
                    }));
            }
        }
        
        public event EventHandler<NotifyWidgetStateChangedEventArgs<TWidgetState, TWidgetStateChangeType>> OnStateChanged;
    }
}