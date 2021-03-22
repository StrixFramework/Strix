using System;
using System.Collections.Generic;
using System.Linq;
using Strix.Widgets.Abstractions;

namespace Strix.Widgets
{
    public class WidgetFactory : IWidgetFactory
    {
        private readonly IList<object> _factories;

        public WidgetFactory(IList<object> factories)
        {
            _factories = factories;
        }
        
        public IWidgetController<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult> CreateWidget<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult>(
            TWidgetCreateOptions createOptions, TWidgetState initialState)
            where TWidget : IWidget<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult>
        {
            IWidgetFactory<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult> factory = _factories
                .OfType<IWidgetFactory<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult>>()
                .FirstOrDefault();

            if (factory is null)
            {
                throw new InvalidOperationException("There's no bla bla bla...");
            }

            IWidget<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult> widget = factory.CreateWidget();

            IWidgetController<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult>
                widgetController
                    = new WidgetController<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult>(
                        widget, initialState);

            widget.OnCreated(createOptions).GetAwaiter().GetResult();

            return widgetController;
        }
    }

    public class
        WidgetFactory<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult> : IWidgetFactory<
            TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult>
        where TWidget : IWidget<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult>
    {
        private readonly Func<TWidget> _widgetFactory;
        
        public WidgetFactory(
            Func<TWidget> widgetFactory)
        {
            _widgetFactory = widgetFactory;
        }
        
        public TWidget CreateWidget()
        {
            return _widgetFactory();
        }
    }
}