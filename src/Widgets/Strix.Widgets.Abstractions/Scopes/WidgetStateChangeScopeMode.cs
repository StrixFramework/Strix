using System;
using System.Threading;

namespace Strix.Widgets.Abstractions.Scopes
{
    [Flags]
    public enum WidgetStateChangeScopeMode
    {
        Automatical = 1,
        OnDispose = 2,
        All = Automatical | OnDispose
    }
}