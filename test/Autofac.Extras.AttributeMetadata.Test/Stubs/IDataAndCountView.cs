// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Autofac.Extras.AttributeMetadata.Test.Stubs;

/// <summary>
/// A metadata view holding two values, so that several separate metadata pairs can be observed
/// arriving as a single view.
/// </summary>
public interface IDataAndCountView
{
    int Count
    {
        get;
    }

    string Data
    {
        get;
    }
}
