// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Autofac.Extras.AttributeMetadata.Test.Stubs;
using Autofac.Features.Metadata;

namespace Autofac.Extras.AttributeMetadata.Test.Integration;

public class MetadataProviderTests
{
    [Fact]
    public void MetadataFromProviderTypedView()
    {
        var container = BuildContainer();

        var withMetadata = container.Resolve<Meta<IProvidedComponent, ProvidedDataView>>();

        Assert.NotNull(withMetadata);
        Assert.NotNull(withMetadata.Metadata);
        Assert.Equal("Value1", withMetadata.Metadata.Key1);
        Assert.Equal("Value2", withMetadata.Metadata.Key2);
    }

    [Fact]
    public void MetadataFromProviderUntypedView()
    {
        var container = BuildContainer();

        var withMetadata = container.Resolve<Meta<IProvidedComponent>>();

        Assert.NotNull(withMetadata);
        Assert.Equal("Value1", withMetadata.Metadata.FirstOrDefault(kv => kv.Key == "Key1").Value);
        Assert.Equal("Value2", withMetadata.Metadata.FirstOrDefault(kv => kv.Key == "Key2").Value);
    }

    private static IContainer BuildContainer()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule<AttributedMetadataModule>();
        builder.RegisterType<ProvidedComponent>().As<IProvidedComponent>();

        return builder.Build();
    }
}
