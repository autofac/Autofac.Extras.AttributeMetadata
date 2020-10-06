// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.ComponentModel.Composition;

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.NestedLifetimeScopeRegistrationScenario
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class NestedLifetimeScopeRegistrationMetadataAttribute : Attribute
    {
        public string Value { get; set; }
    }
}
