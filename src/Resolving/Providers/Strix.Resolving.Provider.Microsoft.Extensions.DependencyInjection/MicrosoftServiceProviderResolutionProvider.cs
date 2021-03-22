using System;
using Strix.Resolving.Abstractions;
using Strix.Resolving.Abstractions.Parameters;

namespace Strix.Resolving.Provider.Microsoft.Extensions.DependencyInjection
{
    public class MicrosoftServiceProviderResolutionProvider : IResolutionProvider
    {
        private readonly IServiceProvider _serviceProvider;
    
        public MicrosoftServiceProviderResolutionProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    
        public object ResolveOptional(Type serviceType, params IResolutionParameter[] resolvingParameters)
        {
            return _serviceProvider.GetService(serviceType);
        }

        public bool CanResolve(Type serviceType, params IResolutionParameter[] resolvingParameters)
        {
            return ResolveOptional(serviceType, resolvingParameters) != null;
        }
    }
}