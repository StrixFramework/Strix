using System;
using Strix.Resolving.Abstractions.Parameters;

namespace Strix.Resolving.Abstractions
{
    public interface IResolutionProvider
    {
        object ResolveOptional(Type serviceType, params IResolutionParameter[] resolvingParameters);

        bool CanResolve(Type serviceType, params IResolutionParameter[] resolvingParameters);
    }
}