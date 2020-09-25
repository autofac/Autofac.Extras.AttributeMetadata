﻿// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Autofac.Extras.AttributeMetadata
{
    /// <summary>
    /// Provides an annotation to resolve constructor dependencies
    /// according to their registered key.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This attribute allows constructor dependencies to be resolved by key.
    /// By marking your dependencies with this attribute and associating
    /// an attribute filter with your type registration, you can be selective
    /// about which service registration should be used to provide the
    /// dependency.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// A simple example might be registration of a specific logger type to be
    /// used by a class. If many loggers are registered with their own key,
    /// the consumer can simply specify the key filter as an attribute to
    /// the constructor parameter.
    /// </para>
    /// <code lang="C#">
    /// public class Manager
    /// {
    ///   public Manager([WithKey("Manager")] ILogger logger)
    ///   {
    ///     // ...
    ///   }
    /// }
    /// </code>
    /// <para>
    /// The same thing can be done for enumerable:
    /// </para>
    /// <code lang="C#">
    /// public class SolutionExplorer
    /// {
    ///   public SolutionExplorer(
    ///     [WithKey("Solution")] IEnumerable&lt;IAdapter&gt; adapters,
    ///     [WithKey("Solution")] ILogger logger)
    ///   {
    ///     this.Adapters = adapters.ToList();
    ///     this.Logger = logger;
    ///   }
    /// }
    /// </code>
    /// <para>
    /// When registering your components, the associated key on the
    /// dependencies will be used. Be sure to specify the
    /// <see cref="AutofacAttributeExtensions.WithAttributeFilter{TLimit, TReflectionActivatorData, TStyle}" />
    /// extension on the type with the filtered constructor parameters.
    /// </para>
    /// <code lang="C#">
    /// var builder = new ContainerBuilder();
    /// builder.RegisterModule(new AttributedMetadataModule());
    ///
    /// // Register the components getting filtered with keys
    /// builder.RegisterType&lt;ConsoleLogger&gt;().Keyed&lt;ILogger&gt;(&quot;Solution&quot;);
    /// builder.RegisterType&lt;FileLogger&gt;().Keyed&lt;ILogger&gt;(&quot;Other&quot;);
    ///
    /// // Attach the filtering behavior to the component with the constructor
    /// builder.RegisterType&lt;SolutionExplorer&gt;().WithAttributeFilter();
    ///
    /// var container = builder.Build();
    ///
    /// // The resolved instance will have the appropriate services in place
    /// var explorer = container.Resolve&lt;SolutionExplorer&gt;();
    /// </code>
    /// </example>
    [SuppressMessage("Microsoft.Design", "CA1018:MarkAttributesWithAttributeUsage", Justification = "Allowing the inherited AttributeUsageAttribute to be used avoids accidental override or conflict at this level.")]
    [Obsolete("Use the Autofac.Features.AttributeFilters.KeyFilterAttribute from the core Autofac library instead.")]
    public sealed class WithKeyAttribute : ParameterFilterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WithKeyAttribute" /> class.
        /// </summary>
        /// <param name="key">
        /// The <paramref name="key"/> that the
        /// dependency should have in order to satisfy the parameter.
        /// </param>
        public WithKeyAttribute(object key) => Key = key;

        /// <summary>
        /// Gets the key the dependency is expected to have to satisfy the parameter.
        /// </summary>
        /// <value>
        /// The <see cref="object"/> corresponding to a registered service
        /// key on a component. Resolved components must be keyed with this value to
        /// satisfy the filter.
        /// </value>
        public object Key { get; }

        /// <summary>
        /// Resolves a constructor parameter based on keyed service requirements.
        /// </summary>
        /// <param name="parameter">The specific parameter being resolved that is marked with this attribute.</param>
        /// <param name="context">The component context under which the parameter is being resolved.</param>
        /// <returns>
        /// The instance of the object that should be used for the parameter value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="parameter" /> or <paramref name="context" /> is <see langword="null" />.
        /// </exception>
        public override object ResolveParameter(ParameterInfo parameter, IComponentContext context)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.TryResolveKeyed(Key, parameter.ParameterType, out var value);
            return value;
        }
    }
}
