// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Autofac.Extras.AttributeMetadata.Test.Stubs;

/// <summary>
/// A metadata view whose properties line up with the keys <see cref="ProvidedDataAttribute"/>
/// hands back, rather than with any property on that attribute.
/// </summary>
public class ProvidedDataView
{
    public string? Key1
    {
        get; set;
    }

    public string? Key2
    {
        get; set;
    }
}
