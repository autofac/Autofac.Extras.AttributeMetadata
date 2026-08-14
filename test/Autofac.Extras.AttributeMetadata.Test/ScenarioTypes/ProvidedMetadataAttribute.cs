// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.ComponentModel.Composition;

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes;

// Supplies its own metadata rather than having its properties reflected over.
[MetadataAttribute]
[AttributeUsage(AttributeTargets.Class)]
public sealed class ProvidedMetadataAttribute : Attribute, IMetadataProvider
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
