// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Autofac.Builder;

namespace Autofac.Extras.AttributeMetadata
{
    /// <summary>
    /// Provides a mechanism to separate metadata registrations from compile-time attributes.
    /// </summary>
    /// <typeparam name="TInterface">Interface used on concrete types of metadata decorated instances.</typeparam>
    /// <typeparam name="TMetadata">Strongly typed metadata definition.</typeparam>
    public interface IMetadataRegistrar<in TInterface, in TMetadata>
    {
        /// <summary>
        /// Gets the builder used to build an <see cref="IContainer"/> from component registrations.
        /// </summary>
        ContainerBuilder ContainerBuilder { get; }

        /// <summary>
        /// Registers provided metadata on the declared type.
        /// </summary>
        /// <typeparam name="TInstance">Concrete instance type.</typeparam>
        /// <param name="metadata">Metadata instance.</param>
        /// <returns>
        /// The registration for continued configuration.
        /// </returns>
        IRegistrationBuilder<TInstance, ConcreteReflectionActivatorData, SingleRegistrationStyle> RegisterType<TInstance>(TMetadata metadata)
            where TInstance : TInterface;

        /// <summary>
        /// Registers provided metadata on the declared type.
        /// </summary>
        /// <param name="instanceType">Type of the instance.</param>
        /// <param name="metadata">The metadata.</param>
        /// <returns>
        /// The registration for continued configuration.
        /// </returns>
        IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> RegisterType(Type instanceType, TMetadata metadata);

        /// <summary>
        /// Registers the provided concrete instance and scans it for generic MetadataAttribute data.
        /// </summary>
        /// <typeparam name="TInstance">Concrete instance type.</typeparam>
        /// <returns>
        /// The registration for continued configuration.
        /// </returns>
        IRegistrationBuilder<TInstance, ConcreteReflectionActivatorData, SingleRegistrationStyle> RegisterAttributedType<TInstance>()
            where TInstance : TInterface;

        /// <summary>
        /// Registers the provided concrete instance type and scans it for generate metadata data.
        /// </summary>
        /// <param name="instanceType">Type of the instance.</param>
        /// <returns>
        /// The registration for continued configuration.
        /// </returns>
        IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> RegisterAttributedType(Type instanceType);
    }
}
