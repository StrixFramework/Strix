using System;
using System.Collections.Generic;
using Strix.Resolving.Abstractions.Parameters;

namespace Strix.Resolving.Abstractions
{
    public interface IResolver : IResolutionProvider
    {
        IEnumerable<IResolutionProvider> ResolutionProviders { get; }

        object Resolve(Type serviceType, params IResolutionParameter[] resolvingParameters);

        TService Resolve<TService>(params IResolutionParameter[] resolvingParameters);

        TService ResolveOptional<TService>(params IResolutionParameter[] resolvingParameters);
    }
}