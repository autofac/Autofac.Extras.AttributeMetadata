// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;
using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes;
using Autofac.Integration.Mef;

namespace Autofac.Extras.AttributeMetadata.Test.Integration;

public class AssemblyScanningTests
{
    [Fact]
    public void MetadataFromMultipleWeakTypedAttributes()
    {
        // Several separate weak-typed attributes combine into one strongly-typed metadata view.
        var items = ScanFor<ICombinationalWeakTypedScenario, INameAndAgeMetadata>();

        Assert.Single(items);
        Assert.Single(items, p => p.Metadata.Name == "Hello" && p.Metadata.Age == 42);
    }

    [Fact]
    public void MetadataFromSingleWeakTypedAttribute()
    {
        var items = ScanFor<IWeakTypedScenario, INameMetadata>();

        Assert.Single(items);
        Assert.Single(items, p => p.Metadata.Name == "Hello");
    }

    [Fact]
    public void MetadataFromTypedAttribute()
    {
        // The attribute implements the metadata view itself, so the typed overload can find it.
        var builder = new ContainerBuilder();
        builder.RegisterMetadataRegistrationSources();
        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .As<IStrongTypedScenario>()
            .WithAttributedMetadata<INameAndAgeMetadata>();

        var items = builder.Build().Resolve<IEnumerable<Lazy<IStrongTypedScenario, INameAndAgeMetadata>>>().ToList();

        Assert.Equal(2, items.Count);
        Assert.IsType<StrongTypedScenario>(items.First(p => p.Metadata.Name == "Hello" && p.Metadata.Age == 42).Value);
        Assert.IsType<AlternateStrongTypedScenario>(items.First(p => p.Metadata.Name == "Goodbye" && p.Metadata.Age == 24).Value);
    }

    private static List<Lazy<TService, TMetadata>> ScanFor<TService, TMetadata>()
        where TService : notnull
    {
        var builder = new ContainerBuilder();
        builder.RegisterMetadataRegistrationSources();
        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .As<TService>()
            .WithAttributedMetadata();

        return builder.Build().Resolve<IEnumerable<Lazy<TService, TMetadata>>>().ToList();
    }
}
