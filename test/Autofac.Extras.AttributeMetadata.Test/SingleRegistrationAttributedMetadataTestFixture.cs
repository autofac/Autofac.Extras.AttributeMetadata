// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;
using Autofac.Builder;
using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.WeakTypedMetadataAttributeScenario;
using Autofac.Features.Metadata;

namespace Autofac.Extras.AttributeMetadata.Test;

public class SingleRegistrationAttributedMetadataTestFixture
{
    [Fact]
    public void Validate_attributed_metadata_on_generic_type_registration()
    {
        // arrange
        var builder = new ContainerBuilder();
        builder.RegisterType<WeakTypedScenario>().As<IWeakTypedScenario>().WithAttributedMetadata();

        // act
        var item = builder.Build().Resolve<Meta<IWeakTypedScenario>>();

        // assert
        Assert.Equal("Hello", item.Metadata["Name"]);
    }

    [Fact]
    public void Validate_attributed_metadata_on_non_generic_type_registration()
    {
        // arrange
        var builder = new ContainerBuilder();
        builder.RegisterType(typeof(WeakTypedScenario)).As<IWeakTypedScenario>().WithAttributedMetadata();

        // act
        var item = builder.Build().Resolve<Meta<IWeakTypedScenario>>();

        // assert
        Assert.Equal("Hello", item.Metadata["Name"]);
    }

    [Fact]
    public void Validate_assembly_scanning_registration_still_resolves()
    {
        // arrange
        var builder = new ContainerBuilder();
        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .As<IWeakTypedScenario>()
            .WithAttributedMetadata();

        // act
        var item = builder.Build().Resolve<Meta<IWeakTypedScenario>>();

        // assert
        Assert.Equal("Hello", item.Metadata["Name"]);
    }

    [Fact]
    public void Validate_no_conflict_when_combined_with_attributed_metadata_module()
    {
        // arrange
        var builder = new ContainerBuilder();
        builder.RegisterModule<AttributedMetadataModule>();
        builder.RegisterType<WeakTypedScenario>().As<IWeakTypedScenario>().WithAttributedMetadata();

        // act
        var item = builder.Build().Resolve<Meta<IWeakTypedScenario>>();

        // assert
        Assert.Equal("Hello", item.Metadata["Name"]);
    }

    [Fact]
    public void Validate_null_builder_throws()
    {
        // arrange
        IRegistrationBuilder<WeakTypedScenario, ConcreteReflectionActivatorData, SingleRegistrationStyle> builder = null!;

        // act / assert
        Assert.Throws<ArgumentNullException>(() => { builder.WithAttributedMetadata(); });
    }
}
