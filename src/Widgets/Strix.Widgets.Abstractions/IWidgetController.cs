using System.Collections;
using System.Collections.Generic;
using Strix.Widgets.Abstractions.Scopes;

namespace Strix.Widgets.Abstractions
{
    public interface IWidgetController<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult>
    where TWidget : IWidget<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult>
    {
        IEnumerable<WidgetStateChangeEntry<TWidgetState, TWidgetStateChangeType>> Changes { get; }

        IWidgetStateChangeScope<TWidgetState, TWidgetStateChangeType> BeginStateChangeScope(
            WidgetStateChangeScopeMode stateChangeScopeMode = WidgetStateChangeScopeMode.All);
    }
}