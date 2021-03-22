using Strix.Resolving.Abstractions.Parameters;

namespace Strix.Resolving.Parameters
{
    public abstract class ResolutionParameterBase : IResolutionParameter
    {
        public object Value { get; }

        protected ResolutionParameterBase(object value)
        {
            Value = value;
        }
    }
}