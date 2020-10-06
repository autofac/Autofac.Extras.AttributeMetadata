// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.ComponentModel.Composition;

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.WeakTypedMetadataAttributeScenario
{
    [MetadataAttribute]
    public class WeakTypedScenarioMetadataAttribute : Attribute
    {
        public WeakTypedScenarioMetadataAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
