// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.CombinationalWeakTypedAttributeScenario;
using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.WeakTypedMetadataAttributeScenario;

namespace Autofac.Extras.AttributeMetadata.Test.Integration;

public class AttributedMetadataModuleTests
{
    [Fact]
    public void MetadataFromMultipleAttributes()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule(new WeakTypeAttributedMetadataModule());

        var weakTyped = builder.Build().Resolve<Lazy<ICombinationalWeakTypedScenario, ICombinationalWeakTypedScenarioMetadata>>();

        Assert.Equal("Hello", weakTyped.Metadata.Name);
        Assert.Equal(42, weakTyped.Metadata.Age);
    }

    [Fact]
    public void MetadataFromSingleAttribute()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule(new WeakTypeAttributedMetadataModule());

        var weakTyped = builder.Build().Resolve<Lazy<IWeakTypedScenario, IWeakTypedScenarioMetadata>>();

        Assert.Equal("Hello", weakTyped.Metadata.Name);
    }
}
