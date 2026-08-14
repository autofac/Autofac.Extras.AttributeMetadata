// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Autofac.Extras.AttributeMetadata.Test.Stubs;

/// <summary>
/// A metadata view holding a single value. This is the <c>TMetadata</c> in
/// <c>Lazy&lt;T, TMetadata&gt;</c>, which is how metadata is read back out of the container.
/// </summary>
public interface IDataView
{
    string Data
    {
        get;
    }
}
