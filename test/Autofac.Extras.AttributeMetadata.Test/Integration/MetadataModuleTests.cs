// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.MetadataModuleScenarioDiscoveryTargets;
using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.WeakTypedMetadataAttributeScenario;

namespace Autofac.Extras.AttributeMetadata.Test.Integration;

public class MetadataModuleTests
{
    [Fact]
    public void MetadataFromAttributedTypeGeneric()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule(new WeakTypedScenarioMetadataModule(true));

        var items = builder.Build().Resolve<IEnumerable<Lazy<IWeakTypedScenario, IWeakTypedScenarioMetadata>>>();

        Assert.Single(items);
        Assert.Single(items, p => p.Metadata.Name == "Hello");
    }

    [Fact]
    public void MetadataFromAttributedTypeNonGeneric()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule(new WeakTypedScenarioMetadataModule(false));

        var items = builder.Build().Resolve<IEnumerable<Lazy<IWeakTypedScenario, IWeakTypedScenarioMetadata>>>();

        Assert.Single(items);
        Assert.Single(items, p => p.Metadata.Name == "Hello");
    }

    [Fact]
    public void MetadataFromGenericRegistration()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule(new StrongTypedScenarioMetadataModule());

        var items = builder.Build().Resolve<IEnumerable<Lazy<IMetadataModuleScenario, IMetadataModuleScenarioMetadata>>>();

        Assert.Single(items, p => p.Metadata.Name == "sid");
        Assert.Single(items, p => p.Metadata.Name == "nancy");
        Assert.Single(items, p => p.Metadata.Name == "the-cats");
        Assert.DoesNotContain(items, p => p.Metadata.Name == "the-dogs");
    }

    [Fact]
    public void MetadataFromTypeOfRegistration()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule(new TypeOfScenarioMetadataModule());

        var items = builder.Build().Resolve<IEnumerable<Lazy<IMetadataModuleScenario, IMetadataModuleScenarioMetadata>>>();

        Assert.Single(items, p => p.Metadata.Name == "sid");
        Assert.Single(items, p => p.Metadata.Name == "nancy");
        Assert.Single(items, p => p.Metadata.Name == "the-cats");
        Assert.DoesNotContain(items, p => p.Metadata.Name == "the-dogs");
    }
}
