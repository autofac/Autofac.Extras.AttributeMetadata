// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;
using Autofac.Extras.AttributeMetadata.Test.Stubs;
using Autofac.Integration.Mef;

namespace Autofac.Extras.AttributeMetadata.Test.Integration;

public class AssemblyScanningTests
{
    [Fact]
    public void MetadataFromCombinedAttributes()
    {
        // Two separate reflected attributes merge into a single metadata view.
        var items = ScanFor<ICombinedComponent, IDataAndCountView>();

        Assert.Single(items);
        Assert.Single(items, p => p.Metadata.Data == "Hello" && p.Metadata.Count == 42);
    }

    [Fact]
    public void MetadataFromReflectedAttribute()
    {
        var items = ScanFor<IReflectedComponent, IDataView>();

        Assert.Single(items);
        Assert.Single(items, p => p.Metadata.Data == "Hello");
    }

    [Fact]
    public void MetadataFromTypedAttribute()
    {
        // Because the attribute implements the view, the typed overload can pick it out by view type.
        var builder = new ContainerBuilder();
        builder.RegisterMetadataRegistrationSources();
        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .As<ITypedComponent>()
            .WithAttributedMetadata<IDataAndCountView>();

        var items = builder.Build().Resolve<IEnumerable<Lazy<ITypedComponent, IDataAndCountView>>>().ToList();

        Assert.Equal(2, items.Count);
        Assert.IsType<TypedComponent>(items.First(p => p.Metadata.Data == "Hello" && p.Metadata.Count == 42).Value);
        Assert.IsType<AlternateTypedComponent>(items.First(p => p.Metadata.Data == "Goodbye" && p.Metadata.Count == 24).Value);
    }

    private static List<Lazy<TService, TView>> ScanFor<TService, TView>()
        where TService : notnull
    {
        var builder = new ContainerBuilder();
        builder.RegisterMetadataRegistrationSources();
        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .As<TService>()
            .WithAttributedMetadata();

        return builder.Build().Resolve<IEnumerable<Lazy<TService, TView>>>().ToList();
    }
}
