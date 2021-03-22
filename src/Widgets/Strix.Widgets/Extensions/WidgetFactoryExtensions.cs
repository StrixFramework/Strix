using System;
using Strix.Widgets.Abstractions;

namespace Strix.Widgets.Extensions
{
    public static class WidgetFactoryExtensions
    {
        public static IWidgetFactoryBuilder WithWidgetFactory<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType,
            TWidgetResult>(
            this IWidgetFactoryBuilder widgetFactoryBuilder,
            Func<TWidget> widgetFactory)
            where TWidget : IWidget<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult>
        {
            return widgetFactoryBuilder.WithWidgetFactory(
                new WidgetFactory<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult>(
                    widgetFactory));
        }

    }
}