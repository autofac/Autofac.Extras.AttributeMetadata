// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.ComponentModel.Composition;

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.CombinationalWeakTypedAttributeScenario
{
    [MetadataAttribute]
    public class CombinationalWeakAgeMetadataAttribute : Attribute
    {
        public CombinationalWeakAgeMetadataAttribute(int age)
        {
            Age = age;
        }

        public int Age { get; private set; }
    }
}
