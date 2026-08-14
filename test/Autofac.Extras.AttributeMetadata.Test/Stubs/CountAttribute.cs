// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.ComponentModel.Composition;

namespace Autofac.Extras.AttributeMetadata.Test.Stubs;

/// <summary>
/// A second reflected metadata attribute, so that two independent attributes contributing separate
/// pairs to the same component can be observed combining into one metadata view.
/// </summary>
[MetadataAttribute]
[AttributeUsage(AttributeTargets.Class)]
public sealed class CountAttribute : Attribute
{
    public CountAttribute(int count)
    {
        Count = count;
    }

    public int Count
    {
        get; private set;
    }
}
