// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Autofac.Extras.AttributeMetadata.Test.Stubs;
using Autofac.Integration.Mef;

namespace Autofac.Extras.AttributeMetadata.Test.Integration;

public class AttributedMetadataModuleTests
{
    [Fact]
    public void MetadataFromCombinedAttributes()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule(new ScanningModule());

        var component = builder.Build().Resolve<Lazy<ICombinedComponent, IDataAndCountView>>();

        Assert.Equal("Hello", component.Metadata.Data);
        Assert.Equal(42, component.Metadata.Count);
    }

    [Fact]
    public void MetadataFromReflectedAttribute()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule(new ScanningModule());

        var component = builder.Build().Resolve<Lazy<IReflectedComponent, IDataView>>();

        Assert.Equal("Hello", component.Metadata.Data);
    }

    /// <summary>
    /// Registers components without declaring any metadata, leaving the base module to pick the
    /// metadata up off their attributes.
    /// </summary>
    private sealed class ScanningModule : AttributedMetadataModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterMetadataRegistrationSources();
            builder.RegisterType<ReflectedComponent>().As<IReflectedComponent>();
            builder.RegisterType<CombinedComponent>().As<ICombinedComponent>();
        }
    }
}
