// This software is part of the Autofac IoC container
// Copyright © 2013 Autofac Contributors
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
