// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.NestedLifetimeScopeRegistrationScenario;
using Autofac.Features.Metadata;
using Xunit;

namespace Autofac.Extras.AttributeMetadata.Test
{
    public class AttributeMetadataModuleTestFixture
    {
        [Fact]
        public void Does_not_throw_in_nested_lifetimeScope_builders()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AttributedMetadataModule>();
            builder.RegisterType<NestedLifetimeScopeRegistrationInstance>().As<ILifetimeScopeRegistrationInstance>();
            var container = builder.Build();

            using (var lifetimeScope = container.BeginLifetimeScope(x => builder.RegisterType<int>()))
            {
                var ex = Record.Exception(() => lifetimeScope.Resolve<Meta<ILifetimeScopeRegistrationInstance, NestedLifetimeScopeRegistrationMetadataAttribute>>());
                Assert.Null(ex);
            }
        }
    }
}
