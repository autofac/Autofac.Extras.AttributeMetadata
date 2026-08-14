// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Autofac.Extras.AttributeMetadata.Test.Stubs;

/// <summary>
/// A second bare implementation of <see cref="IProgrammaticComponent"/>, so a module can be shown
/// registering more than one component against the same service with differing metadata.
/// </summary>
public class AlternateProgrammaticComponent : IProgrammaticComponent
{
}
