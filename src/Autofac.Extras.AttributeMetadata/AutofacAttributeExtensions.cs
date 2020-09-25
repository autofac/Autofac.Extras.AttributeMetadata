// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Linq;
using Autofac.Builder;
using Autofac.Features.Scanning;

namespace Autofac.Extras.AttributeMetadata
{
    /// <summary>
    /// Extends registration syntax for attribute scenarios.
    /// </summary>
    public static class AutofacAttributeExtensions
    {
        /// <summary>
        /// This method can be invoked with the assembly scanner to register metadata that is declared loosely using
        /// attributes marked with the MetadataAttributeAttribute. All of the marked attributes are used together to create
        /// a common set of dictionary values that constitute the metadata on the type.
        /// </summary>
        /// <typeparam name="TLimit">The type of the registration limit.</typeparam>
        /// <typeparam name="TScanningActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TRegistrationStyle">Registration style type.</typeparam>
        /// <param name="builder">The registration builder containing registration data.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="builder" /> is <see langword="null" />.
        /// </exception>
        public static IRegistrationBuilder<TLimit, TScanningActivatorData, TRegistrationStyle>
            WithAttributedMetadata<TLimit, TScanningActivatorData, TRegistrationStyle>(this IRegistrationBuilder<TLimit, TScanningActivatorData, TRegistrationStyle> builder)
                where TScanningActivatorData : ScanningActivatorData
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.ActivatorData.ConfigurationActions.Add(
                (t, rb) => rb.WithMetadata(MetadataHelper.GetMetadata(t)));

            return builder;
        }

        /// <summary>
        /// This method can be invoked with the assembly scanner to register strongly typed metadata attributes. The
        /// attributes are scanned for one that is derived from the metadata interface.  If one is found, the metadata
        /// contents are extracted and registered with the instance registration.
        /// </summary>
        /// <typeparam name="TMetadata">Metadata type to search for.</typeparam>
        /// <param name="builder">The registration builder containing registration data.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="builder" /> is <see langword="null" />.
        /// </exception>
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle>
            WithAttributedMetadata<TMetadata>(this IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.ActivatorData.ConfigurationActions.Add(
                (t, rb) => rb.WithMetadata(MetadataHelper.GetMetadata<TMetadata>(t)));

            return builder;
        }

        /// <summary>
        /// Applies attribute-based filtering on constructor dependencies for use with attributes
        /// derived from the <see cref="ParameterFilterAttribute"/>.
        /// </summary>
        /// <typeparam name="TLimit">The type of the registration limit.</typeparam>
        /// <typeparam name="TReflectionActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TRegistrationStyle">Registration style type.</typeparam>
        /// <param name="builder">The registration builder containing registration data.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="builder" /> is <see langword="null" />.
        /// </exception>
        /// <remarks>
        /// <para>
        /// Apply this extension to component registrations that use attributes
        /// that derive from the <see cref="ParameterFilterAttribute"/>
        /// like the <see cref="WithMetadataAttribute"/>
        /// in their constructors. Doing so will allow the attribute-based filtering to occur. See
        /// <see cref="WithMetadataAttribute"/> for an
        /// example on how to use the filter and attribute together.
        /// </para>
        /// </remarks>
        /// <seealso cref="WithMetadataAttribute"/>
        [Obsolete("Use Autofac.Features.AttributeFilters.RegistrationExtensions.WithAttributeFiltering from the core Autofac library instead.")]
        public static IRegistrationBuilder<TLimit, TReflectionActivatorData, TRegistrationStyle>
            WithAttributeFilter<TLimit, TReflectionActivatorData, TRegistrationStyle>(
                this IRegistrationBuilder<TLimit, TReflectionActivatorData, TRegistrationStyle> builder)
            where TReflectionActivatorData : ReflectionActivatorData
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

#pragma warning disable CS0618
            return builder.WithParameter(
                (p, c) => p.GetCustomAttributes(true).OfType<ParameterFilterAttribute>().Any(),
                (p, c) =>
                {
                    var filter = p.GetCustomAttributes(true).OfType<ParameterFilterAttribute>().First();
                    return filter.ResolveParameter(p, c);
                });
#pragma warning restore CS0618
        }
    }
}
