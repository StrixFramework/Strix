using System;
using System.Linq;
using System.Reflection;
using Strix.Resolving.Abstractions.Parameters;
using Strix.Resolving.Parameters;

namespace Strix.Resolving
{
    public static class ResolutionParametersExtensions
    {
        #region Common

        #region With Predicates

        public static IResolutionParameter Parameter(
            this IResolutionParameter[] resolutionParameters, Func<IResolutionParameter, bool> predicate)
        {
            return resolutionParameters.First(predicate);
        }

        public static IResolutionParameter OptionalParameter(
            this IResolutionParameter[] resolutionParameters,
            Func<IResolutionParameter, bool> predicate)
        {
            return resolutionParameters.FirstOrDefault(predicate);
        }

        public static bool HasOptionalParameter(
            this IResolutionParameter[] resolutionParameters,
            Func<IResolutionParameter, bool> predicate,
            out IResolutionParameter resolutionParameter)
        {
            return (resolutionParameter = OptionalParameter(resolutionParameters, predicate)) != null;
        }

        #endregion

        #region Without Predicates

        public static IResolutionParameter Parameter(
            this IResolutionParameter[] resolutionParameters)
        {
            return resolutionParameters.First();
        }

        public static IResolutionParameter OptionalParameter(
            this IResolutionParameter[] resolutionParameters)
        {
            return resolutionParameters.FirstOrDefault();
        }

        public static bool HasOptionalParameter(
            this IResolutionParameter[] resolutionParameters,
            out IResolutionParameter resolutionParameter)
        {
            return (resolutionParameter = OptionalParameter(resolutionParameters)) != null;
        }

        #endregion

        #endregion

        #region Attribute-related

        #region With Predicates

        public static AttributeResolutionParameter AttributeParameter<TAttribute>(
            this IResolutionParameter[] resolutionParameters,
            Func<TAttribute, bool> predicate)
            where TAttribute : Attribute
        {
            return resolutionParameters
                .OfType<AttributeResolutionParameter>()
                .First(_ => _.Value is TAttribute attribute && predicate(attribute));
        }

        public static AttributeResolutionParameter OptionalAttributeParameter<TAttribute>(
            this IResolutionParameter[] resolutionParameters,
            Func<TAttribute, bool> predicate)
            where TAttribute : Attribute
        {
            return resolutionParameters
                .OfType<AttributeResolutionParameter>()
                .FirstOrDefault(_ => _.Value is TAttribute attribute && predicate(attribute));
        }

        public static bool HasOptionalAttributeParameter<TAttribute>(
            this IResolutionParameter[] resolutionParameters,
            Func<TAttribute, bool> predicate,
            out AttributeResolutionParameter attributeResolutionParameter)
            where TAttribute : Attribute
        {
            return (attributeResolutionParameter =
                OptionalAttributeParameter<TAttribute>(resolutionParameters, predicate)) != null;
        }

        #endregion

        #region Without Predicates

        public static AttributeResolutionParameter AttributeParameter<TAttribute>(
            this IResolutionParameter[] resolutionParameters)
            where TAttribute : Attribute
        {
            return resolutionParameters.OfType<AttributeResolutionParameter>().First();
        }

        public static AttributeResolutionParameter OptionalAttributeParameter<TAttribute>(
            this IResolutionParameter[] resolutionParameters)
        {
            return resolutionParameters.OfType<AttributeResolutionParameter>().FirstOrDefault();
        }

        public static bool HasOptionalAttributeParameter<TAttribute>(
            this IResolutionParameter[] resolutionParameters,
            out AttributeResolutionParameter attributeResolutionParameter)
        {
            return (attributeResolutionParameter = OptionalAttributeParameter<TAttribute>(resolutionParameters)) !=
                   null;
        }

        #endregion

        #endregion

        #region Type and name-related

        #region With Predicates

        public static DefaultResolutionParameter DefaultParameter(
            this IResolutionParameter[] resolutionParameters,
            Func<DefaultResolutionParameter, bool> predicate,
            ParameterInfo parameterInfo)
        {
            return resolutionParameters.OfType<DefaultResolutionParameter>()
                .First(_ => _.IsMatch(parameterInfo) && predicate(_));
        }

        public static DefaultResolutionParameter OptionalDefaultParameter(
            this IResolutionParameter[] resolutionParameters,
            Func<DefaultResolutionParameter, bool> predicate,
            ParameterInfo parameterInfo)
        {
            return resolutionParameters.OfType<DefaultResolutionParameter>()
                .FirstOrDefault(_ => _.IsMatch(parameterInfo) && predicate(_));
        }

        public static bool HasOptionalDefaultParameter(
            this IResolutionParameter[] resolutionParameters,
            Func<DefaultResolutionParameter, bool> predicate,
            ParameterInfo parameterInfo,
            out DefaultResolutionParameter defaultResolutionParameter)
        {
            return (defaultResolutionParameter = resolutionParameters.OfType<DefaultResolutionParameter>()
                .FirstOrDefault(_ => _.IsMatch(parameterInfo) && predicate(_))) != null;
        }

        #endregion

        #region Without Predicates

        public static DefaultResolutionParameter DefaultParameter(
            this IResolutionParameter[] resolutionParameters,
            ParameterInfo parameterInfo)
        {
            return resolutionParameters.OfType<DefaultResolutionParameter>()
                .First(_ => _.IsMatch(parameterInfo));
        }

        public static DefaultResolutionParameter OptionalDefaultParameter(
            this IResolutionParameter[] resolutionParameters,
            ParameterInfo parameterInfo)
        {
            return resolutionParameters.OfType<DefaultResolutionParameter>()
                .FirstOrDefault(_ => _.IsMatch(parameterInfo));
        }

        public static bool HasOptionalDefaultParameter(
            this IResolutionParameter[] resolutionParameters,
            ParameterInfo parameterInfo,
            out DefaultResolutionParameter defaultResolutionParameter)
        {
            return (defaultResolutionParameter = resolutionParameters.OfType<DefaultResolutionParameter>()
                .FirstOrDefault(_ => _.IsMatch(parameterInfo))) != null;
        }

        #endregion

        #endregion
    }
}