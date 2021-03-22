namespace Strix.Widgets.Abstractions
{
    public interface IWidgetFactoryBuilder
    {
        IWidgetFactoryBuilder WithWidgetFactory<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult> (
            IWidgetFactory<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult> widgetFactory)
            where TWidget : IWidget<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult>;
        
        IWidgetFactory Build();
    }
}