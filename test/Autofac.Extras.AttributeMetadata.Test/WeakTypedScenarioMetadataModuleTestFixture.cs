// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.WeakTypedMetadataAttributeScenario;
using Xunit;

namespace Autofac.Extras.AttributeMetadata.Test
{
    public class WeakTypedScenarioMetadataModuleTestFixture
    {
        [Fact]
        public void Verify_single_attribute_scan_results_from_test_fixture()
        {
            // arrange
            var builder = new ContainerBuilder();

            builder.RegisterModule(new WeakTypedScenarioMetadataModule(true));

            // act
            var items = builder.Build().Resolve<IEnumerable<Lazy<IWeakTypedScenario, IWeakTypedScenarioMetadata>>>();

            // assert
            Assert.Single(items);
            Assert.Single(items.Where(p => p.Metadata.Name == "Hello"));
        }

        [Fact]
        public void Verify_single_attribute_scan_results_from_test_fixture_using_non_generic()
        {
            // arrange
            var builder = new ContainerBuilder();

            builder.RegisterModule(new WeakTypedScenarioMetadataModule(false));

            // act
            var items = builder.Build().Resolve<IEnumerable<Lazy<IWeakTypedScenario, IWeakTypedScenarioMetadata>>>();

            // assert
            Assert.Single(items);
            Assert.Single(items.Where(p => p.Metadata.Name == "Hello"));
        }
    }
}
