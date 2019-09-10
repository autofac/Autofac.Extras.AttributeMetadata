// This software is part of the Autofac IoC container
// Copyright (c) 2007 - 2013 Autofac Contributors
// https://autofac.org
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

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
