// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Autofac.Builder;
using Autofac.Features.Scanning;

namespace Autofac.Extras.AttributeMetadata;

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
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

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
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.ActivatorData.ConfigurationActions.Add(
            (t, rb) => rb.WithMetadata(MetadataHelper.GetMetadata<TMetadata>(t)));

        return builder;
    }
}
