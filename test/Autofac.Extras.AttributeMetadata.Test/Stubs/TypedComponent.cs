// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Autofac.Extras.AttributeMetadata.Test.Stubs;

/// <summary>
/// A component whose metadata attribute implements the metadata view, so it can be discovered by
/// view type rather than by taking every metadata attribute present.
/// </summary>
[DataAndCount("Hello", 42)]
public class TypedComponent : ITypedComponent
{
}
