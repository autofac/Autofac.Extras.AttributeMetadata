// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Autofac.Extras.AttributeMetadata.Test.Stubs;

/// <summary>
/// The service exposed by <see cref="ReflectedComponent"/>. Each component family here has its own
/// service interface so that assembly scanning in one test cannot pick up another test's components.
/// </summary>
public interface IReflectedComponent
{
}
