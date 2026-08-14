// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Autofac.Extras.AttributeMetadata.Test.Stubs;

/// <summary>
/// A second implementation of <see cref="ITypedComponent"/> with different metadata values, so that
/// resolving by metadata can be shown to select between candidates.
/// </summary>
[DataAndCount("Goodbye", 24)]
public class AlternateTypedComponent : ITypedComponent
{
}
