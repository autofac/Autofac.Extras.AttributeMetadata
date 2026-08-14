// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Autofac.Extras.AttributeMetadata.Test.Stubs;

/// <summary>
/// A component carrying two separate reflected metadata attributes, whose pairs are expected to
/// merge into one metadata view.
/// </summary>
[Data("Hello")]
[Count(42)]
public class CombinedComponent : ICombinedComponent
{
}
