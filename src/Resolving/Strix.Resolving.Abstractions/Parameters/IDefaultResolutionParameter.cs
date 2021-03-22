using System;
using System.Reflection;

namespace Strix.Resolving.Abstractions.Parameters
{
    public interface IDefaultResolutionParameter : IResolutionParameter
    {
        Type ParameterType { get; }

        string ParameterName { get; }

        PreferringType Type { get; }

        bool IsMatch(ParameterInfo parameterInfo);
    }
}