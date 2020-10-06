// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.StrongTypedMetadataAttributeScenario
{
    [MetadataAttribute]
    public class StrongTypedScenarioMetadataAttribute : Attribute, IStrongTypedScenarioMetadata
    {
        public StrongTypedScenarioMetadataAttribute(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public string Name { get; private set; }

        public int Age { get; private set; }
    }
}
