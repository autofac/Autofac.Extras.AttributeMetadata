// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;
using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.StrongTypedMetadataAttributeScenario;
using Autofac.Integration.Mef;

namespace Autofac.Extras.AttributeMetadata.Test.Integration;

public class StrongTypedAttributeTests
{
    [Fact]
    public void MetadataFromTypedAttributes()
    {
        var builder = new ContainerBuilder();
        builder.RegisterMetadataRegistrationSources();

        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .As<IStrongTypedScenario>()
            .WithAttributedMetadata<IStrongTypedScenarioMetadata>();

        var items = builder.Build().Resolve<IEnumerable<Lazy<IStrongTypedScenario, IStrongTypedScenarioMetadata>>>().ToList();

        Assert.Equal(2, items.Count);
        Assert.Single(items, p => p.Metadata.Name == "Hello" && p.Metadata.Age == 42);
        Assert.Single(items, p => p.Metadata.Name == "Goodbye" && p.Metadata.Age == 24);

        Assert.IsType<StrongTypedScenario>(items.First(p => p.Metadata.Name == "Hello" && p.Metadata.Age == 42).Value);
        Assert.IsType<AlternateStrongTypedScenario>(items.First(p => p.Metadata.Name == "Goodbye" && p.Metadata.Age == 24).Value);
    }
}
