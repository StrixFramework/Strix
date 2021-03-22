using System;
using System.Collections.Generic;
using Strix.Resolving.Abstractions;
using Strix.Resolving.Abstractions.Parameters;

namespace Strix.Resolving
{
    public class Resolver : IResolver
    {
        public IEnumerable<IResolutionProvider> ResolutionProviders { get; }

        public Resolver(IEnumerable<IResolutionProvider> resolutionProviders)
        {
            ResolutionProviders = resolutionProviders;
        }

        public object Resolve(Type serviceType, params IResolutionParameter[] resolvingParameters)
        {
            object? result = ResolveOptional(serviceType, resolvingParameters);

            if (result == null)
            {
                throw new InvalidOperationException($"There's no registered service with specified type. Type: {serviceType}");

            }

            return result;
        }

        public object? ResolveOptional(Type serviceType, params IResolutionParameter[] resolvingParameters)
        {
            object objectInstance = null;

            foreach (IResolutionProvider resolutionProvider in ResolutionProviders)
            {
                if (resolutionProvider.CanResolve(serviceType, resolvingParameters))
                {
                    objectInstance = resolutionProvider.ResolveOptional(serviceType, resolvingParameters);

                    if (objectInstance != null)
                        break;
                }
            }

            return objectInstance;
        }

        public TService Resolve<TService>(params IResolutionParameter[] resolvingParameters)
        {
            return (TService) Resolve(typeof(TService), resolvingParameters);
        }

        public TService ResolveOptional<TService>(params IResolutionParameter[] resolvingParameters)
        {
            return (TService) ResolveOptional(typeof(TService), resolvingParameters);
        }

        public bool CanResolve(Type serviceType, params IResolutionParameter[] resolvingParameters)
        {
            foreach (IResolutionProvider resolutionProvider in ResolutionProviders)
            {
                if (resolutionProvider.CanResolve(serviceType, resolvingParameters))
                {
                    return true;
                }
            }

            return false;
        }
    }
}