// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Autofac.Extras.AttributeMetadata.Test.Stubs;

/// <summary>
/// Metadata handed over as an object rather than discovered on an attribute. This is what the
/// programmatic registration path on <see cref="MetadataModule{TInterface, TMetadata}"/> supplies.
/// </summary>
public class DataView : IDataView
{
    public DataView(string data)
    {
        Data = data;
    }

    public string Data
    {
        get; private set;
    }
}
