using System;
using System.Reflection;
using Strix.Resolving.Abstractions.Parameters;

namespace Strix.Resolving.Parameters
{
    public class DefaultResolutionParameter : ResolutionParameterBase, IDefaultResolutionParameter
    {
        public Type ParameterType { get; }

        public string ParameterName { get; }

        public PreferringType Type { get; }
        
        public DefaultResolutionParameter(
            Type parameterType,
            object value, string parameterName = null) : base(value)
        {
            ParameterType = parameterType;
            ParameterName = parameterName;
            Type = PreferringType.ParameterType;
        }

        public DefaultResolutionParameter(
            string parameterName,
            object value, Type parameterType = null) : base(value)
        {
            ParameterType = parameterType;
            ParameterName = parameterName;
            Type = PreferringType.ParameterName;
        }

        public bool IsMatch(ParameterInfo parameterInfo)
        {
            return Type switch
            {
                PreferringType.ParameterType => parameterInfo.ParameterType == ParameterType,
                PreferringType.ParameterName => string.Compare(parameterInfo.Name, ParameterName, StringComparison.Ordinal) == 0,
                // PreferringType.Both => parameterInfo.ParameterType == ParameterType &&
                //                        string.Compare(parameterInfo.Name, ParameterName, StringComparison.Ordinal) == 0,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}