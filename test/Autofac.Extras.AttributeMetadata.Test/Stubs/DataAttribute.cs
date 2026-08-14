// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.ComponentModel.Composition;

namespace Autofac.Extras.AttributeMetadata.Test.Stubs;

/// <summary>
/// A metadata attribute whose publicly readable properties are reflected into the metadata
/// dictionary. It carries no meaning beyond the value it holds, and it does not implement any
/// metadata view, so it is only found by the untyped discovery overload of
/// <see cref="MetadataHelper.GetMetadata(Type)"/>.
/// </summary>
[MetadataAttribute]
[AttributeUsage(AttributeTargets.Class)]
public sealed class DataAttribute : Attribute
{
    public DataAttribute(string data)
    {
        Data = data;
    }

    public string Data
    {
        get; private set;
    }
}
