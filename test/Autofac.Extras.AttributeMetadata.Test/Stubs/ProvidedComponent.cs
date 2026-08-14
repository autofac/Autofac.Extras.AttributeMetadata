// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Autofac.Extras.AttributeMetadata.Test.Stubs;

/// <summary>
/// A component whose metadata comes from an attribute that supplies its own dictionary rather than
/// exposing properties to be reflected.
/// </summary>
[ProvidedData]
public class ProvidedComponent : IProvidedComponent
{
}
