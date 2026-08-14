// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.ComponentModel.Composition;

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes;

[MetadataAttribute]
[AttributeUsage(AttributeTargets.Class)]
public sealed class AgeMetadataAttribute : Attribute
{
    public AgeMetadataAttribute(int age)
    {
        Age = age;
    }

    public int Age
    {
        get; private set;
    }
}
