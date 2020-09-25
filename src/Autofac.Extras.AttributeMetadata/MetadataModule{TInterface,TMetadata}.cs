// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Autofac.Builder;
using Autofac.Integration.Mef;

namespace Autofac.Extras.AttributeMetadata
{
    /// <summary>
    /// Provides a mechanism to separate metadata registrations from compile-time attributes.
    /// </summary>
    /// <typeparam name="TInterface">Interface used on concrete types of metadata decorated instances.</typeparam>
    /// <typeparam name="TMetadata">Strongly typed metadata definition.</typeparam>
    public abstract class MetadataModule<TInterface, TMetadata> : Module, IMetadataRegistrar<TInterface, TMetadata>
    {
        /// <summary>
        /// Gets the builder used to build an <see cref="IContainer"/> from component registrations.
        /// </summary>
        public ContainerBuilder ContainerBuilder { get; private set; }

        /// <summary>
        /// Override this method to execute metadata registration operations.
        /// </summary>
        /// <param name="registrar">Wrapped metadata registry interface.</param>
        public abstract void Register(IMetadataRegistrar<TInterface, TMetadata> registrar);

        /// <summary>
        /// Registers provided metadata on the declared type.
        /// </summary>
        /// <typeparam name="TInstance">Concrete instance type.</typeparam>
        /// <param name="metadata">Metadata instance.</param>
        /// <returns>
        /// The registration for continued configuration.
        /// </returns>
        public IRegistrationBuilder<TInstance, ConcreteReflectionActivatorData, SingleRegistrationStyle>
            RegisterType<TInstance>(TMetadata metadata)
            where TInstance : TInterface =>
            ContainerBuilder.RegisterType<TInstance>().As<TInterface>().WithMetadata(
                MetadataHelper.GetProperties(metadata, typeof(TInstance)));

        /// <summary>
        /// Registers the provided concrete instance and scans it for generic metadata attribute data.
        /// </summary>
        /// <typeparam name="TInstance">Concrete instance type.</typeparam>
        /// <returns>
        /// The registration for continued configuration.
        /// </returns>
        public IRegistrationBuilder<TInstance, ConcreteReflectionActivatorData, SingleRegistrationStyle>
            RegisterAttributedType<TInstance>()
            where TInstance : TInterface =>
            ContainerBuilder.RegisterType<TInstance>().As<TInterface>().WithMetadata(
                MetadataHelper.GetMetadata(typeof(TInstance)));

        /// <summary>
        /// registers provided metadata on the declared type.
        /// </summary>
        /// <param name="instanceType">Type of the instance.</param>
        /// <param name="metadata">The metadata.</param>
        /// <returns>
        /// The registration for continued configuration.
        /// </returns>
        public IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> RegisterType(
            Type instanceType, TMetadata metadata) =>
            ContainerBuilder.RegisterType(instanceType).As<TInterface>().WithMetadata(
                MetadataHelper.GetProperties(metadata, instanceType));

        /// <summary>
        /// Registers the provided concrete instance type and scans it for generate metadata data.
        /// </summary>
        /// <param name="instanceType">Type of the instance.</param>
        /// <returns>
        /// The registration for continued configuration.
        /// </returns>
        public IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle>
            RegisterAttributedType(Type instanceType) =>
            ContainerBuilder.RegisterType(instanceType).As<TInterface>().WithMetadata(
                MetadataHelper.GetMetadata(instanceType));

        /// <summary>
        /// Standard module method being overridden and sealed to provide wrapped metadata registration.
        /// </summary>
        /// <param name="builder">Container builder.</param>
        protected sealed override void Load(ContainerBuilder builder)
        {
            ContainerBuilder = builder;
            builder.RegisterMetadataRegistrationSources();
            Register(this);
        }
    }
}
