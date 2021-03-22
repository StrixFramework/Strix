using System;
using Strix.Resolving.Abstractions.Parameters;

namespace Strix.Resolving.Parameters
{
    public class AttributeResolutionParameter : ResolutionParameterBase, IAttributeResolutionParameter
    {
        public Type AttributeType { get; }

        public AttributeResolutionParameter(Type attributeType, object value) : base(value)
        {
            AttributeType = attributeType;
        }
    }
}