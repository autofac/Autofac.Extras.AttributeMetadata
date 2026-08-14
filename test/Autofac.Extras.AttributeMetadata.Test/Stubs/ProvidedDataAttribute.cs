// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.ComponentModel.Composition;

namespace Autofac.Extras.AttributeMetadata.Test.Stubs;

/// <summary>
/// A metadata attribute that computes its own dictionary through <see cref="IMetadataProvider"/>
/// instead of having its properties reflected, so the keys it supplies need not correspond to any
/// property on it.
/// </summary>
[MetadataAttribute]
[AttributeUsage(AttributeTargets.Class)]
public sealed class ProvidedDataAttribute : Attribute, IMetadataProvider
{
    public IDictionary<string, object?> GetMetadata(Type targetType)
    {
        return new Dictionary<string, object?>()
        {
            { "Key1", "Value1" },
            { "Key2", "Value2" },
        };
    }
}
