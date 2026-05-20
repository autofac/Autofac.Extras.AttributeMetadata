// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.CombinationalWeakTypedAttributeScenario;
using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.WeakTypedMetadataAttributeScenario;
using Xunit;

namespace Autofac.Extras.AttributeMetadata.Test
{
    public class WeakTypeAttributedMetadataModuleTestFixture
    {
        [Fact]
        public void Verify_automatic_scanning_with_the_attributed_metadata_module()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new WeakTypeAttributedMetadataModule());

            var container = builder.Build();

            var weakTyped = container.Resolve<Lazy<IWeakTypedScenario, IWeakTypedScenarioMetadata>>();

            Assert.Equal("Hello", weakTyped.Metadata.Name);
        }

        [Fact]
        public void Verify_automatic_scanning_with_the_multiple_attributions_by_the_module()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new WeakTypeAttributedMetadataModule());

            var container = builder.Build();

            var weakTyped = container.Resolve<Lazy<ICombinationalWeakTypedScenario, ICombinationalWeakTypedScenarioMetadata>>();

            Assert.Equal("Hello", weakTyped.Metadata.Name);
            Assert.Equal(42, weakTyped.Metadata.Age);
        }
    }
}
