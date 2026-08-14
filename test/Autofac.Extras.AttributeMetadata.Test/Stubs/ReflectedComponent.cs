// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Autofac.Extras.AttributeMetadata.Test.Stubs;

/// <summary>
/// A component carrying a single reflected metadata attribute, which is the simplest
/// attribute-driven case.
/// </summary>
[Data("Hello")]
public class ReflectedComponent : IReflectedComponent
{
}
