using System;
using Strix.Resolving.Abstractions.Parameters;

namespace Strix.Resolving.Abstractions.Parameters
{
    [Flags]
    public enum PreferringType
    {
        ParameterType = 1,
        ParameterName = 2,
    }
}