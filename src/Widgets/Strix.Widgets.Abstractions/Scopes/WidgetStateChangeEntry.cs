namespace Strix.Widgets.Abstractions.Scopes
{
    public class WidgetStateChangeEntry<TWidgetState, TWidgetStateChangeType>
    {
        public TWidgetState State { get; set; }
        
        public TWidgetStateChangeType ChangeType { get; set; }
    }
}