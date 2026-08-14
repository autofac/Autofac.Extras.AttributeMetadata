// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes;

namespace Autofac.Extras.AttributeMetadata.Test;

public class MetadataModuleTests
{
    [Fact]
    public void ContainerBuilder_BeforeLoad()
    {
        var module = new UnloadedMetadataModule();

        Assert.Null(module.ContainerBuilder);
    }

    [Fact]
    public void RegisterAttributedType_NoContainerBuilder()
    {
        var module = new UnloadedMetadataModule();

        Assert.Throws<InvalidOperationException>(() => { module.RegisterAttributedType<MetadataModuleScenario>(); });
    }

    [Fact]
    public void RegisterAttributedType_NoContainerBuilderNonGeneric()
    {
        var module = new UnloadedMetadataModule();

        Assert.Throws<InvalidOperationException>(() => { module.RegisterAttributedType(typeof(MetadataModuleScenario)); });
    }

    [Fact]
    public void RegisterType_NoContainerBuilder()
    {
        var module = new UnloadedMetadataModule();

        Assert.Throws<InvalidOperationException>(() => { module.RegisterType<MetadataModuleScenario>(new NameMetadata("sid")); });
    }

    [Fact]
    public void RegisterType_NoContainerBuilderNonGeneric()
    {
        var module = new UnloadedMetadataModule();

        Assert.Throws<InvalidOperationException>(() => { module.RegisterType(typeof(MetadataModuleScenario), new NameMetadata("sid")); });
    }

    [Fact]
    public void RegisterType_NullInstanceType()
    {
        var module = new UnloadedMetadataModule();

        Assert.Throws<ArgumentNullException>(() => { module.RegisterType(null!, new NameMetadata("sid")); });
    }

    [Fact]
    public void RegisterType_NullMetadata()
    {
        var module = new UnloadedMetadataModule();

        Assert.Throws<ArgumentNullException>(() => { module.RegisterType<MetadataModuleScenario>(null!); });
    }

    [Fact]
    public void RegisterType_NullMetadataNonGeneric()
    {
        var module = new UnloadedMetadataModule();

        Assert.Throws<ArgumentNullException>(() => { module.RegisterType(typeof(MetadataModuleScenario), null!); });
    }

    /// <summary>
    /// A module that is never registered in a container, so the builder it hands to registrar
    /// callers is still unset.
    /// </summary>
    private sealed class UnloadedMetadataModule : MetadataModule<IMetadataModuleScenario, INameMetadata>
    {
        public override void Register(IMetadataRegistrar<IMetadataModuleScenario, INameMetadata> registrar)
        {
        }
    }
}
