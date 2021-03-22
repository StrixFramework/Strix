namespace Strix.Widgets.Abstractions
{
    public interface IWidgetFactory
    {
        IWidgetController<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult>
            CreateWidget<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult>
            (TWidgetCreateOptions createOptions, TWidgetState initialState)
            where TWidget : IWidget<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult>;

    }
    
    public interface IWidgetFactory<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult>
        where TWidget : IWidget<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult>
    {
        TWidget CreateWidget();
    }
}