// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.ComponentModel.Composition;

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes;

[MetadataAttribute]
[AttributeUsage(AttributeTargets.Class)]
public sealed class NameMetadataAttribute : Attribute
{
    public NameMetadataAttribute(string name)
    {
        Name = name;
    }

    public string Name
    {
        get; private set;
    }
}
