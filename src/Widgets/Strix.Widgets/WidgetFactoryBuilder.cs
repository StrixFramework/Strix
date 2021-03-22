using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using Strix.Widgets.Abstractions;

namespace Strix.Widgets
{
    public class WidgetFactoryBuilder : IWidgetFactoryBuilder
    {
        private readonly IList<object> _factories;

        public WidgetFactoryBuilder()
        {
            _factories = new List<object>();
        }
        
        public IWidgetFactoryBuilder WithWidgetFactory<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult>(
            IWidgetFactory<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult> widgetFactory)
        where TWidget : IWidget<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult>
        {
            _factories.Add(widgetFactory ?? throw new ArgumentNullException(nameof(widgetFactory)));

            return this;
        }

        public IWidgetFactory Build()
        {
            return new WidgetFactory(_factories);
        }
    }
}