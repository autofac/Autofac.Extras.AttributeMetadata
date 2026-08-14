// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Autofac.Core;
using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes;
using Autofac.Features.Metadata;

namespace Autofac.Extras.AttributeMetadata.Test;

public class AttributedMetadataModuleTests
{
    [Fact]
    public void AttachToComponentRegistration_NestedLifetimeScope()
    {
        // A child scope sees parent registrations wrapped in registrations that share the parent's
        // metadata dictionary, so the module must not attempt to add the same keys twice.
        var builder = new ContainerBuilder();
        builder.RegisterModule<AttributedMetadataModule>();
        builder.RegisterType<NestedScopeComponent>().As<INestedScopeComponent>();
        var container = builder.Build();

        using var lifetimeScope = container.BeginLifetimeScope(x => x.RegisterType<NestedScopeComponent>());
        var exception = Record.Exception(() => lifetimeScope.Resolve<Meta<INestedScopeComponent>>());

        Assert.Null(exception);
    }

    [Fact]
    public void AttachToComponentRegistration_NullRegistration()
    {
        var module = new AttachableMetadataModule();

        Assert.Throws<ArgumentNullException>(() => module.Attach(null!));
    }

    /// <summary>
    /// Exposes the protected attachment point so the argument guard can be exercised directly.
    /// </summary>
    private sealed class AttachableMetadataModule : AttributedMetadataModule
    {
        public void Attach(IComponentRegistration registration) => AttachToComponentRegistration(null!, registration);
    }

    private interface INestedScopeComponent
    {
    }

    [NameMetadata("ParentLifetime")]
    private sealed class NestedScopeComponent : INestedScopeComponent
    {
    }
}
