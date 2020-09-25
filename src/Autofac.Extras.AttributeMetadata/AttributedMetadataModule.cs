// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Autofac.Core;
using Autofac.Core.Registration;

namespace Autofac.Extras.AttributeMetadata
{
    /// <summary>
    /// this module will scan all registrations for metadata and associate them if found.
    /// </summary>
    public class AttributedMetadataModule : Module
    {
        /// <summary>
        /// Override to attach module-specific functionality to a
        /// component registration.
        /// </summary>
        /// <remarks>This method will be called for all existing <i>and future</i> component
        /// registrations - ordering is not important.</remarks>
        /// <param name="componentRegistry">The component registry builder.</param>
        /// <param name="registration">The registration to attach functionality to.</param>
        protected override void AttachToComponentRegistration(IComponentRegistryBuilder componentRegistry, IComponentRegistration registration)
        {
            if (registration == null) throw new ArgumentNullException(nameof(registration));

            foreach (var property in MetadataHelper.GetMetadata(registration.Activator.LimitType))
            {
                if (!registration.Metadata.ContainsKey(property.Key))
                {
                    registration.Metadata.Add(property);
                }
            }
        }
    }
}
