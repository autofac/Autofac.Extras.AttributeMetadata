// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Autofac.Extras.AttributeMetadata.Test.Stubs;

namespace Autofac.Extras.AttributeMetadata.Test.Integration;

public class MetadataModuleTests
{
    [Fact]
    public void MetadataFromAttributedTypeGeneric()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule(new AttributedTypeModule(true));

        var items = builder.Build().Resolve<IEnumerable<Lazy<IReflectedComponent, IDataView>>>();

        Assert.Single(items);
        Assert.Single(items, p => p.Metadata.Data == "Hello");
    }

    [Fact]
    public void MetadataFromAttributedTypeNonGeneric()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule(new AttributedTypeModule(false));

        var items = builder.Build().Resolve<IEnumerable<Lazy<IReflectedComponent, IDataView>>>();

        Assert.Single(items);
        Assert.Single(items, p => p.Metadata.Data == "Hello");
    }

    [Fact]
    public void MetadataFromSuppliedMetadataGeneric()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule(new SuppliedMetadataModule());

        var items = builder.Build().Resolve<IEnumerable<Lazy<IProgrammaticComponent, IDataView>>>();

        Assert.Single(items, p => p.Metadata.Data == "sid");
        Assert.Single(items, p => p.Metadata.Data == "nancy");
        Assert.Single(items, p => p.Metadata.Data == "the-cats");
        Assert.DoesNotContain(items, p => p.Metadata.Data == "the-dogs");
    }

    [Fact]
    public void MetadataFromSuppliedMetadataNonGeneric()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule(new SuppliedMetadataTypeOfModule());

        var items = builder.Build().Resolve<IEnumerable<Lazy<IProgrammaticComponent, IDataView>>>();

        Assert.Single(items, p => p.Metadata.Data == "sid");
        Assert.Single(items, p => p.Metadata.Data == "nancy");
        Assert.Single(items, p => p.Metadata.Data == "the-cats");
        Assert.DoesNotContain(items, p => p.Metadata.Data == "the-dogs");
    }

    /// <summary>
    /// Finds metadata by scanning the registered type's attributes rather than supplying it.
    /// </summary>
    private sealed class AttributedTypeModule : MetadataModule<IReflectedComponent, IDataView>
    {
        private readonly bool _useGeneric;

        public AttributedTypeModule(bool useGeneric)
        {
            _useGeneric = useGeneric;
        }

        public override void Register(IMetadataRegistrar<IReflectedComponent, IDataView> registrar)
        {
            ArgumentNullException.ThrowIfNull(registrar);

            if (_useGeneric)
            {
                registrar.RegisterAttributedType<ReflectedComponent>();
                return;
            }

            registrar.RegisterAttributedType(typeof(ReflectedComponent));
        }
    }

    /// <summary>
    /// Supplies metadata directly, which allows wireup that is not fixed at compile time. The same
    /// component is registered more than once with differing metadata.
    /// </summary>
    private sealed class SuppliedMetadataModule : MetadataModule<IProgrammaticComponent, IDataView>
    {
        public override void Register(IMetadataRegistrar<IProgrammaticComponent, IDataView> registrar)
        {
            ArgumentNullException.ThrowIfNull(registrar);

            registrar.RegisterType<ProgrammaticComponent>(new DataView("sid"));
            registrar.RegisterType<ProgrammaticComponent>(new DataView("nancy"));
            registrar.RegisterType<AlternateProgrammaticComponent>(new DataView("the-cats"));
        }
    }

    /// <summary>
    /// The same supplied-metadata registrations expressed through the non-generic registrar overload.
    /// </summary>
    private sealed class SuppliedMetadataTypeOfModule : MetadataModule<IProgrammaticComponent, IDataView>
    {
        public override void Register(IMetadataRegistrar<IProgrammaticComponent, IDataView> registrar)
        {
            ArgumentNullException.ThrowIfNull(registrar);

            registrar.RegisterType(typeof(ProgrammaticComponent), new DataView("sid"));
            registrar.RegisterType(typeof(ProgrammaticComponent), new DataView("nancy"));
            registrar.RegisterType(typeof(AlternateProgrammaticComponent), new DataView("the-cats"));
        }
    }
}
