// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;
using Autofac.Builder;
using Autofac.Extras.AttributeMetadata.Test.Stubs;
using Autofac.Features.Metadata;
using Autofac.Features.Scanning;
using Autofac.Integration.Mef;

namespace Autofac.Extras.AttributeMetadata.Test;

public class AutofacAttributeExtensionsTests
{
    [Fact]
    public void WithAttributedMetadata_AssemblyScanningBuilder()
    {
        // The single registration overload must not make the assembly scanning overload ambiguous.
        var builder = new ContainerBuilder();
        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .As<IReflectedComponent>()
            .WithAttributedMetadata();

        var item = builder.Build().Resolve<Meta<IReflectedComponent>>();

        Assert.Equal("Hello", item.Metadata["Data"]);
    }

    [Fact]
    public void WithAttributedMetadata_GenericTypeRegistration()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<ReflectedComponent>().As<IReflectedComponent>().WithAttributedMetadata();

        var item = builder.Build().Resolve<Meta<IReflectedComponent>>();

        Assert.Equal("Hello", item.Metadata["Data"]);
    }

    [Fact]
    public void WithAttributedMetadata_NonGenericTypeRegistration()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType(typeof(ReflectedComponent)).As<IReflectedComponent>().WithAttributedMetadata();

        var item = builder.Build().Resolve<Meta<IReflectedComponent>>();

        Assert.Equal("Hello", item.Metadata["Data"]);
    }

    [Fact]
    public void WithAttributedMetadata_NullScanningBuilder()
    {
        IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> builder = null!;

        Assert.Throws<ArgumentNullException>(() => { builder.WithAttributedMetadata(); });
    }

    [Fact]
    public void WithAttributedMetadata_NullSingleRegistrationBuilder()
    {
        IRegistrationBuilder<ReflectedComponent, ConcreteReflectionActivatorData, SingleRegistrationStyle> builder = null!;

        Assert.Throws<ArgumentNullException>(() => { builder.WithAttributedMetadata(); });
    }

    [Fact]
    public void WithAttributedMetadata_NullTypedViewBuilder()
    {
        IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> builder = null!;

        Assert.Throws<ArgumentNullException>(() => { builder.WithAttributedMetadata<IDataAndCountView>(); });
    }

    [Fact]
    public void WithAttributedMetadata_TypedViewAssemblyScanning()
    {
        var builder = new ContainerBuilder();
        builder.RegisterMetadataRegistrationSources();
        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .As<ITypedComponent>()
            .WithAttributedMetadata<IDataAndCountView>();

        var items = builder.Build().Resolve<IEnumerable<Lazy<ITypedComponent, IDataAndCountView>>>();

        Assert.Single(items, p => p.Metadata.Data == "Hello" && p.Metadata.Count == 42);
    }

    [Fact]
    public void WithAttributedMetadata_WithAttributedMetadataModule()
    {
        // The module skips keys that are already present, so metadata is applied once even though
        // both the module and the extension target the same registration.
        var builder = new ContainerBuilder();
        builder.RegisterModule<AttributedMetadataModule>();
        builder.RegisterType<ReflectedComponent>().As<IReflectedComponent>().WithAttributedMetadata();

        var item = builder.Build().Resolve<Meta<IReflectedComponent>>();

        Assert.Equal("Hello", item.Metadata["Data"]);
    }
}
