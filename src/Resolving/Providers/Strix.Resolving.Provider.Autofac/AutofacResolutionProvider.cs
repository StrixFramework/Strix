using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autofac;
using Autofac.Core;
using Strix.Resolving.Abstractions;
using Strix.Resolving.Abstractions.Parameters;

namespace Strix.Resolving.Provider.Autofac
{
    public class AutofacResolutionProvider : IResolutionProvider
    {
        private readonly IContainer _container;
    
        public AutofacResolutionProvider(IContainer container)
        {
            _container = container;
        }
    
        public object ResolveOptional(Type serviceType, params IResolutionParameter[] resolvingParameters)
        {
            return _container.ResolveOptional(serviceType, ConvertParameters(resolvingParameters));
        }

        public bool CanResolve(Type serviceType, params IResolutionParameter[] resolvingParameters)
        {
            return _container.IsRegistered(serviceType);
        }

        private IEnumerable<Parameter> ConvertParameters(IResolutionParameter[] resolutionParameters)
        {
            foreach (IResolutionParameter resolutionParameter in resolutionParameters)
            {
                yield return resolutionParameter switch
                {
                    IAttributeResolutionParameter attributeResolutionParameter => new TypedParameter(
                        attributeResolutionParameter.AttributeType, attributeResolutionParameter.Value),
                    IDefaultResolutionParameter defaultResolutionParameter => ConvertDefaultResolutionParameter(
                        defaultResolutionParameter)
                };
            }
        }

        private Parameter ConvertDefaultResolutionParameter(IDefaultResolutionParameter defaultResolutionParameter)
        {
            return defaultResolutionParameter.Type switch
            {
                PreferringType.ParameterName => new NamedParameter(defaultResolutionParameter.ParameterName, defaultResolutionParameter.Value),
                PreferringType.ParameterType => new TypedParameter(defaultResolutionParameter.ParameterType, defaultResolutionParameter.Value)
            };
        }
    }
}