using System;
using System.Threading.Tasks;

namespace Strix.Widgets.Abstractions.Scopes
{
    public interface INotifyWidgetStateChanged<TWidgetStateChangeType>
    {
        void NotifyWidgetStateChanged(TWidgetStateChangeType widgetStateChangeType);
    }
    
    public interface INotifyWidgetStateChanged<TWidgetState, TWidgetStateChangeType> : INotifyWidgetStateChanged<TWidgetStateChangeType>
    {
        event EventHandler<NotifyWidgetStateChangedEventArgs<TWidgetState, TWidgetStateChangeType>> OnStateChanged;
    }

    public class NotifyWidgetStateChangedEventArgs<TWidgetState, TWidgetStateChangeType> : EventArgs
    {
        public WidgetStateChangeEntry<TWidgetState, TWidgetStateChangeType> Entry { get; }

        public NotifyWidgetStateChangedEventArgs(WidgetStateChangeEntry<TWidgetState, TWidgetStateChangeType> entry)
        {
            Entry = entry;
        }
    }
}