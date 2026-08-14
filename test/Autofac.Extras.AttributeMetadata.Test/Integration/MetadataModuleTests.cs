// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes;

namespace Autofac.Extras.AttributeMetadata.Test.Integration;

public class MetadataModuleTests
{
    [Fact]
    public void MetadataFromAttributedTypeGeneric()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule(new AttributedTypeModule(true));

        var items = builder.Build().Resolve<IEnumerable<Lazy<IWeakTypedScenario, INameMetadata>>>();

        Assert.Single(items);
        Assert.Single(items, p => p.Metadata.Name == "Hello");
    }

    [Fact]
    public void MetadataFromAttributedTypeNonGeneric()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule(new AttributedTypeModule(false));

        var items = builder.Build().Resolve<IEnumerable<Lazy<IWeakTypedScenario, INameMetadata>>>();

        Assert.Single(items);
        Assert.Single(items, p => p.Metadata.Name == "Hello");
    }

    [Fact]
    public void MetadataFromGenericRegistration()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule(new GenericRegistrationModule());

        var items = builder.Build().Resolve<IEnumerable<Lazy<IMetadataModuleScenario, INameMetadata>>>();

        Assert.Single(items, p => p.Metadata.Name == "sid");
        Assert.Single(items, p => p.Metadata.Name == "nancy");
        Assert.Single(items, p => p.Metadata.Name == "the-cats");
        Assert.DoesNotContain(items, p => p.Metadata.Name == "the-dogs");
    }

    [Fact]
    public void MetadataFromTypeOfRegistration()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule(new TypeOfRegistrationModule());

        var items = builder.Build().Resolve<IEnumerable<Lazy<IMetadataModuleScenario, INameMetadata>>>();

        Assert.Single(items, p => p.Metadata.Name == "sid");
        Assert.Single(items, p => p.Metadata.Name == "nancy");
        Assert.Single(items, p => p.Metadata.Name == "the-cats");
        Assert.DoesNotContain(items, p => p.Metadata.Name == "the-dogs");
    }

    /// <summary>
    /// Finds metadata by scanning the registered type's attributes rather than supplying it.
    /// </summary>
    private sealed class AttributedTypeModule : MetadataModule<IWeakTypedScenario, INameMetadata>
    {
        private readonly bool _useGeneric;

        public AttributedTypeModule(bool useGeneric)
        {
            _useGeneric = useGeneric;
        }

        public override void Register(IMetadataRegistrar<IWeakTypedScenario, INameMetadata> registrar)
        {
            ArgumentNullException.ThrowIfNull(registrar);

            if (_useGeneric)
            {
                registrar.RegisterAttributedType<WeakTypedScenario>();
                return;
            }

            registrar.RegisterAttributedType(typeof(WeakTypedScenario));
        }
    }

    /// <summary>
    /// Supplies metadata programmatically, which allows non-compile-time wireup. The same component
    /// is registered more than once with different metadata.
    /// </summary>
    private sealed class GenericRegistrationModule : MetadataModule<IMetadataModuleScenario, INameMetadata>
    {
        public override void Register(IMetadataRegistrar<IMetadataModuleScenario, INameMetadata> registrar)
        {
            ArgumentNullException.ThrowIfNull(registrar);

            registrar.RegisterType<MetadataModuleScenario>(new NameMetadata("sid"));
            registrar.RegisterType<MetadataModuleScenario>(new NameMetadata("nancy"));
            registrar.RegisterType<MetadataModuleScenarioAlternate>(new NameMetadata("the-cats"));
        }
    }

    private sealed class TypeOfRegistrationModule : MetadataModule<IMetadataModuleScenario, INameMetadata>
    {
        public override void Register(IMetadataRegistrar<IMetadataModuleScenario, INameMetadata> registrar)
        {
            ArgumentNullException.ThrowIfNull(registrar);

            registrar.RegisterType(typeof(MetadataModuleScenario), new NameMetadata("sid"));
            registrar.RegisterType(typeof(MetadataModuleScenario), new NameMetadata("nancy"));
            registrar.RegisterType(typeof(MetadataModuleScenarioAlternate), new NameMetadata("the-cats"));
        }
    }
}
