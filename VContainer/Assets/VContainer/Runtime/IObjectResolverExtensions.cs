using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VContainer
{
    public static class IObjectResolverExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Resolve<T>(this IObjectResolver resolver, bool isOptional = false) =>
            (T)resolver.Resolve(typeof(T), isOptional);

        // Using from CodeGen
        [Preserve]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object ResolveNonGeneric(this IObjectResolver resolve, Type type, bool isOptional = false) =>
            resolve.Resolve(type, isOptional);

        public static object ResolveOrParameter(
            this IObjectResolver resolver,
            Type parameterType,
            string parameterName,
            IReadOnlyList<IInjectParameter> parameters,
            bool isOptional = false)
        {
            if (parameters != null)
            {
                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < parameters.Count; i++)
                {
                    var parameter = parameters[i];
                    if (parameter.Match(parameterType, parameterName))
                    {
                        return parameter.Value;
                    }
                }
            }
            return resolver.Resolve(parameterType, isOptional);
        }
    }
}