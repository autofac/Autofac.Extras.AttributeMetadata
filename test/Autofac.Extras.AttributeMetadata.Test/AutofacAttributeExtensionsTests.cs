// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;
using Autofac.Builder;
using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes;
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
            .As<IWeakTypedScenario>()
            .WithAttributedMetadata();

        var item = builder.Build().Resolve<Meta<IWeakTypedScenario>>();

        Assert.Equal("Hello", item.Metadata["Name"]);
    }

    [Fact]
    public void WithAttributedMetadata_GenericTypeRegistration()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<WeakTypedScenario>().As<IWeakTypedScenario>().WithAttributedMetadata();

        var item = builder.Build().Resolve<Meta<IWeakTypedScenario>>();

        Assert.Equal("Hello", item.Metadata["Name"]);
    }

    [Fact]
    public void WithAttributedMetadata_NonGenericTypeRegistration()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType(typeof(WeakTypedScenario)).As<IWeakTypedScenario>().WithAttributedMetadata();

        var item = builder.Build().Resolve<Meta<IWeakTypedScenario>>();

        Assert.Equal("Hello", item.Metadata["Name"]);
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
        IRegistrationBuilder<WeakTypedScenario, ConcreteReflectionActivatorData, SingleRegistrationStyle> builder = null!;

        Assert.Throws<ArgumentNullException>(() => { builder.WithAttributedMetadata(); });
    }

    [Fact]
    public void WithAttributedMetadata_NullTypedMetadataBuilder()
    {
        IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> builder = null!;

        Assert.Throws<ArgumentNullException>(() => { builder.WithAttributedMetadata<INameAndAgeMetadata>(); });
    }

    [Fact]
    public void WithAttributedMetadata_TypedMetadataAssemblyScanning()
    {
        var builder = new ContainerBuilder();
        builder.RegisterMetadataRegistrationSources();
        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .As<IStrongTypedScenario>()
            .WithAttributedMetadata<INameAndAgeMetadata>();

        var items = builder.Build().Resolve<IEnumerable<Lazy<IStrongTypedScenario, INameAndAgeMetadata>>>();

        Assert.Single(items, p => p.Metadata.Name == "Hello" && p.Metadata.Age == 42);
    }

    [Fact]
    public void WithAttributedMetadata_WithAttributedMetadataModule()
    {
        // The module skips keys that are already present, so metadata is applied once even though
        // both the module and the extension target the same registration.
        var builder = new ContainerBuilder();
        builder.RegisterModule<AttributedMetadataModule>();
        builder.RegisterType<WeakTypedScenario>().As<IWeakTypedScenario>().WithAttributedMetadata();

        var item = builder.Build().Resolve<Meta<IWeakTypedScenario>>();

        Assert.Equal("Hello", item.Metadata["Name"]);
    }
}
