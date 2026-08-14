// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.ComponentModel.Composition;

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes;

// Implements the metadata view directly, which is what lets the typed WithAttributedMetadata
// overload find it.
[MetadataAttribute]
[AttributeUsage(AttributeTargets.Class)]
public sealed class NameAndAgeMetadataAttribute : Attribute, INameAndAgeMetadata
{
    public NameAndAgeMetadataAttribute(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public int Age
    {
        get; private set;
    }

    public string Name
    {
        get; private set;
    }
}
