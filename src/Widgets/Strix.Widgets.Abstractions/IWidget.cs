using System;
using System.Threading.Tasks;

namespace Strix.Widgets.Abstractions
{
    public interface IWidget<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult>
    where TWidget : IWidget<TWidget, TWidgetCreateOptions, TWidgetState, TWidgetStateChangeType, TWidgetResult>
    {
        Task OnCreated(TWidgetCreateOptions createOptions);

        Task OnUpdated(TWidgetState state, TWidgetStateChangeType stateChangeType);

        Task<TWidgetResult> OnCompleted(TWidgetCreateOptions createOptions, TWidgetState state);
    }
}