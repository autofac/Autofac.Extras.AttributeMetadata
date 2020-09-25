// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Reflection;

namespace Autofac.Extras.AttributeMetadata
{
    /// <summary>
    /// Base attribute class for marking constructor parameters and enabling
    /// filtering by attributed criteria.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Implementations of this attribute can be used to mark constructor parameters
    /// so filtering can be done based on registered service data. For example, the
    /// <see cref="WithMetadataAttribute"/> allows constructor
    /// parameters to be filtered by registered metadata criteria and the
    /// <see cref="WithKeyAttribute"/> allows constructor
    /// parameters to be filtered by a keyed service registration.
    /// </para>
    /// <para>
    /// If a type uses these attributes, it should be registered with Autofac
    /// using the
    /// <see cref="AutofacAttributeExtensions.WithAttributeFilter{TLimit, TReflectionActivatorData, TStyle}" />
    /// extension to enable the behavior.
    /// </para>
    /// <para>
    /// For specific attribute usage examples, see the attribute documentation.
    /// </para>
    /// </remarks>
    /// <seealso cref="WithMetadataAttribute"/>
    /// <seealso cref="WithKeyAttribute"/>
    [AttributeUsage(AttributeTargets.Parameter)]
    [Obsolete("Use the Autofac.Features.AttributeFilters.ParameterFilterAttribute from the core Autofac library instead.")]
    public abstract class ParameterFilterAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterFilterAttribute"/> class.
        /// </summary>
        protected ParameterFilterAttribute()
        {
        }

        /// <summary>
        /// Implemented in derived classes to resolve a specific parameter marked with
        /// this attribute.
        /// </summary>
        /// <param name="parameter">The specific parameter being resolved that is marked with this attribute.</param>
        /// <param name="context">The component context under which the parameter is being resolved.</param>
        /// <returns>The instance of the object that should be used for the parameter value.</returns>
        public abstract object ResolveParameter(ParameterInfo parameter, IComponentContext context);
    }
}
