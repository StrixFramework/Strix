using System;

namespace Strix.Resolving.Abstractions.Parameters
{
    public interface IAttributeResolutionParameter : IResolutionParameter
    {
        Type AttributeType { get; }
    }
}