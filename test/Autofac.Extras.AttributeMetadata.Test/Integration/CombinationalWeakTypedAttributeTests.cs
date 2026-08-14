// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;
using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.CombinationalWeakTypedAttributeScenario;
using Autofac.Integration.Mef;

namespace Autofac.Extras.AttributeMetadata.Test.Integration;

public class CombinationalWeakTypedAttributeTests
{
    [Fact]
    public void MetadataFromMultipleAttributes()
    {
        // Several separate weak-typed attributes combine into one strongly-typed metadata instance.
        var builder = new ContainerBuilder();
        builder.RegisterMetadataRegistrationSources();

        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .As<ICombinationalWeakTypedScenario>()
            .WithAttributedMetadata();

        var items = builder.Build().Resolve<IEnumerable<Lazy<ICombinationalWeakTypedScenario, ICombinationalWeakTypedScenarioMetadata>>>();

        Assert.Single(items);
        Assert.Single(items, p => p.Metadata.Name == "Hello");
        Assert.Single(items, p => p.Metadata.Age == 42);
    }
}
