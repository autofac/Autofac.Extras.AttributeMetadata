// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.ComponentModel.Composition;

namespace Autofac.Extras.AttributeMetadata.Test.Stubs;

/// <summary>
/// A metadata attribute that implements its metadata view directly. Implementing the view is what
/// lets the typed discovery overload, <see cref="MetadataHelper.GetMetadata{TMetadataType}(Type)"/>,
/// single this attribute out from any others on the same component.
/// </summary>
[MetadataAttribute]
[AttributeUsage(AttributeTargets.Class)]
public sealed class DataAndCountAttribute : Attribute, IDataAndCountView
{
    public DataAndCountAttribute(string data, int count)
    {
        Data = data;
        Count = count;
    }

    public int Count
    {
        get; private set;
    }

    public string Data
    {
        get; private set;
    }
}
