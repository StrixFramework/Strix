using System;
using Strix.Resolving.Abstractions;
using Strix.Resolving.Abstractions.Parameters;

namespace Strix.Resolving
{
    public class CustomResolutionProvider : IResolutionProvider
    {
        private readonly Func<Type, IResolutionParameter[], object> _serviceResolutionFunc;
        private readonly Func<Type, IResolutionParameter[], bool> _serviceExistenceCheckerFunc;

        public CustomResolutionProvider(
            Func<Type, IResolutionParameter[], object> serviceResolutionFunc,
            Func<Type, IResolutionParameter[], bool> serviceExistenceCheckerFunc)
        {
            _serviceResolutionFunc = serviceResolutionFunc;
            _serviceExistenceCheckerFunc = serviceExistenceCheckerFunc;
        }

        public object ResolveOptional(Type serviceType, params IResolutionParameter[] resolvingParameters)
        {
            return _serviceResolutionFunc(serviceType, resolvingParameters);
        }

        public bool CanResolve(Type serviceType, params IResolutionParameter[] resolvingParameters)
        {
            return _serviceExistenceCheckerFunc(serviceType, resolvingParameters);
        }
    }
}