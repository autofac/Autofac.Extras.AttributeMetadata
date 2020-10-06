﻿// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Autofac.Features.Metadata;

namespace Autofac.Extras.AttributeMetadata
{
    /// <summary>
    /// Provides an annotation to filter constructor dependencies
    /// according to their specified metadata.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This attribute allows constructor dependencies to be filtered by metadata.
    /// By marking your dependencies with this attribute and associating
    /// an attribute filter with your type registration, you can be selective
    /// about which service registration should be used to provide the
    /// dependency.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// A simple example might be registration of a specific logger type to be
    /// used by a class. If many loggers are registered with the <c>LoggerName</c>
    /// metadata, the consumer can simply specify the filter as an attribute to
    /// the constructor parameter.
    /// </para>
    /// <code lang="C#">
    /// public class Manager
    /// {
    ///   public Manager([WithMetadata("LoggerName", "Manager")] ILogger logger)
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
    ///     [WithMetadata("Target", "Solution")] IEnumerable&lt;IAdapter&gt; adapters,
    ///     [WithMetadata("LoggerName", "Solution")] ILogger logger)
    ///   {
    ///     this.Adapters = adapters.ToList();
    ///     this.Logger = logger;
    ///   }
    /// }
    /// </code>
    /// <para>
    /// When registering your components, the associated metadata on the
    /// dependencies will be used. Be sure to specify the
    /// <see cref="AutofacAttributeExtensions.WithAttributeFilter{TLimit, TReflectionActivatorData, TStyle}" />
    /// extension on the type with the filtered constructor parameters.
    /// </para>
    /// <code lang="C#">
    /// var builder = new ContainerBuilder();
    /// builder.RegisterModule(new AttributedMetadataModule());
    ///
    /// // Attach metadata to the components getting filtered
    /// builder.RegisterType&lt;ConsoleLogger&gt;().WithMetadata(&quot;LoggerName&quot;, &quot;Solution&quot;).As&lt;ILogger&gt;();
    /// builder.RegisterType&lt;FileLogger&gt;().WithMetadata(&quot;LoggerName&quot;, &quot;Other&quot;).As&lt;ILogger&gt;();
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
    [Obsolete("Use the Autofac.Features.AttributeFilters.MetadataFilterAttribute from the core Autofac library instead.")]
    public sealed class WithMetadataAttribute : ParameterFilterAttribute
    {
        /// <summary>
        /// Reference to the <see cref="FilterOne{T}"/>
        /// method used in creating a closed generic reference during registration.
        /// </summary>
        private static readonly MethodInfo FilterOneInfo = typeof(WithMetadataAttribute).GetMethod("FilterOne", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.InvokeMethod);

        /// <summary>
        /// Reference to the <see cref="FilterAll{T}"/>
        /// method used in creating a closed generic reference during registration.
        /// </summary>
        private static readonly MethodInfo FilterAllInfo = typeof(WithMetadataAttribute).GetMethod("FilterAll", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.InvokeMethod);

        /// <summary>
        /// Initializes a new instance of the <see cref="WithMetadataAttribute"/> class,
        /// specifying the <paramref name="key"/> and <paramref name="value"/> that the
        /// dependency should have in order to satisfy the parameter.
        /// </summary>
        /// <param name="key">
        /// The key the dependency is expected to have to satisfy the parameter.
        /// </param>
        /// <param name="value">
        /// The value the dependency is expected to have to satisfy the parameter.
        /// </param>
        public WithMetadataAttribute(string key, object value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Gets the key the dependency is expected to have to satisfy the parameter.
        /// </summary>
        /// <value>
        /// The <see cref="string"/> corresponding to a registered metadata
        /// key on a component. Resolved components must have this metadata key to
        /// satisfy the filter.
        /// </value>
        public string Key { get; }

        /// <summary>
        /// Gets the value the dependency is expected to have to satisfy the parameter.
        /// </summary>
        /// <value>
        /// The <see cref="object"/> corresponding to a registered metadata
        /// value on a component. Resolved components must have the metadata
        /// <see cref="Key"/> with
        /// this value to satisfy the filter.
        /// </value>
        public object Value { get; }

        /// <summary>
        /// Resolves a constructor parameter based on metadata requirements.
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

            // GetElementType currently is the effective equivalent of "Determine if the type
            // is in IEnumerable and if it is, get the type being enumerated." This doesn't support
            // the other relationship types like Lazy<T>, Func<T>, etc. If we need to add that,
            // this is the place to do it.
            var elementType = GetElementType(parameter.ParameterType);
            var hasMany = elementType != parameter.ParameterType;

            return hasMany
                ? FilterAllInfo.MakeGenericMethod(elementType).Invoke(null, new[] { context, Key, Value })
                : FilterOneInfo.MakeGenericMethod(elementType).Invoke(null, new[] { context, Key, Value });
        }

        private static Type GetElementType(Type type) =>
            type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                ? type.GetGenericArguments()[0]
                : type;

        // Using Lazy<T> to ensure components that aren't actually used won't get activated.
        [SuppressMessage("IDE0051", "IDE0051", Justification = "Method is consumed via reflection in static member variable in this class.")]
        private static T FilterOne<T>(IComponentContext context, string metadataKey, object metadataValue) =>
            context.Resolve<IEnumerable<Meta<Lazy<T>>>>()
                .Where(m => m.Metadata.ContainsKey(metadataKey) && metadataValue.Equals(m.Metadata[metadataKey]))
                .Select(m => m.Value.Value)
                .FirstOrDefault();

        // Using Lazy<T> to ensure components that aren't actually used won't get activated.
        [SuppressMessage("IDE0051", "IDE0051", Justification = "Method is consumed via reflection in static member variable in this class.")]
        private static IEnumerable<T> FilterAll<T>(IComponentContext context, string metadataKey, object metadataValue) =>
            context.Resolve<IEnumerable<Meta<Lazy<T>>>>()
                .Where(m => m.Metadata.ContainsKey(metadataKey) && metadataValue.Equals(m.Metadata[metadataKey]))
                .Select(m => m.Value.Value)
                .ToArray();
    }
}
