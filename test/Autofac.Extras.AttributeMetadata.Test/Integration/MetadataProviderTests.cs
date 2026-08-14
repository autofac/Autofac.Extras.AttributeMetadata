// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes;
using Autofac.Features.Metadata;

namespace Autofac.Extras.AttributeMetadata.Test.Integration;

public class MetadataProviderTests
{
    [Fact]
    public void MetadataFromProviderStrongTyped()
    {
        var container = BuildContainer();

        var withMetadata = container.Resolve<Meta<IMetadataProviderScenario, ProvidedMetadata>>();

        Assert.NotNull(withMetadata);
        Assert.NotNull(withMetadata.Metadata);
        Assert.Equal("Value1", withMetadata.Metadata.Key1);
        Assert.Equal("Value2", withMetadata.Metadata.Key2);
    }

    [Fact]
    public void MetadataFromProviderWeakTyped()
    {
        var container = BuildContainer();

        var withMetadata = container.Resolve<Meta<IMetadataProviderScenario>>();

        Assert.NotNull(withMetadata);
        Assert.Equal("Value1", withMetadata.Metadata.FirstOrDefault(kv => kv.Key == "Key1").Value);
        Assert.Equal("Value2", withMetadata.Metadata.FirstOrDefault(kv => kv.Key == "Key2").Value);
    }

    private static IContainer BuildContainer()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule<AttributedMetadataModule>();
        builder.RegisterType<MetadataProviderScenario>().As<IMetadataProviderScenario>();

        return builder.Build();
    }
}
