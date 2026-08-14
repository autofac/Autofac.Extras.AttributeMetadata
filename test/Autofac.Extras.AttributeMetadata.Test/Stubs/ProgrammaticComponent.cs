// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Autofac.Extras.AttributeMetadata.Test.Stubs;

/// <summary>
/// A component with no metadata attributes at all. Its metadata is supplied at registration time by
/// a <see cref="MetadataModule{TInterface, TMetadata}"/>, which is why it is deliberately bare.
/// </summary>
public class ProgrammaticComponent : IProgrammaticComponent
{
}
