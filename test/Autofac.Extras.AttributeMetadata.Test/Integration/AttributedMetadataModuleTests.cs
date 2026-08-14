// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes;
using Autofac.Integration.Mef;

namespace Autofac.Extras.AttributeMetadata.Test.Integration;

public class AttributedMetadataModuleTests
{
    [Fact]
    public void MetadataFromMultipleAttributes()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule(new ScanningModule());

        var weakTyped = builder.Build().Resolve<Lazy<ICombinationalWeakTypedScenario, INameAndAgeMetadata>>();

        Assert.Equal("Hello", weakTyped.Metadata.Name);
        Assert.Equal(42, weakTyped.Metadata.Age);
    }

    [Fact]
    public void MetadataFromSingleAttribute()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule(new ScanningModule());

        var weakTyped = builder.Build().Resolve<Lazy<IWeakTypedScenario, INameMetadata>>();

        Assert.Equal("Hello", weakTyped.Metadata.Name);
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
            builder.RegisterType<WeakTypedScenario>().As<IWeakTypedScenario>();
            builder.RegisterType<CombinationalWeakTypedScenario>().As<ICombinationalWeakTypedScenario>();
        }
    }
}
