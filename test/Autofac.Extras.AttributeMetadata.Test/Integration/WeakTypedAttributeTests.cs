// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;
using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.WeakTypedMetadataAttributeScenario;
using Autofac.Integration.Mef;

namespace Autofac.Extras.AttributeMetadata.Test.Integration;

public class WeakTypedAttributeTests
{
    [Fact]
    public void MetadataFromWeakTypedAttributes()
    {
        var builder = new ContainerBuilder();
        builder.RegisterMetadataRegistrationSources();

        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .As<IWeakTypedScenario>()
            .WithAttributedMetadata();

        var items = builder.Build().Resolve<IEnumerable<Lazy<IWeakTypedScenario, IWeakTypedScenarioMetadata>>>();

        Assert.Single(items);
        Assert.Single(items, p => p.Metadata.Name == "Hello");
    }
}
