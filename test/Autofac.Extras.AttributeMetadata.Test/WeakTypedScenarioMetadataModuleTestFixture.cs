using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes;
using Xunit;

namespace Autofac.Extras.AttributeMetadata.Test
{
    public class WeakTypedScenarioMetadataModuleTestFixture
    {
        [Fact]
        public void verify_single_attribute_scan_results_from_test_fixture()
        {
            // arrange
            var builder = new ContainerBuilder();

            builder.RegisterModule(new WeakTypedScenarioMetadataModule(true));

            // act
            var items = builder.Build().Resolve<IEnumerable<Lazy<IWeakTypedScenario, IWeakTypedScenarioMetadata>>>();

            // assert
            Assert.Equal(1, items.Count());
            Assert.Equal(1, items.Where(p => p.Metadata.Name == "Hello").Count());
        }

        [Fact]
        public void verify_single_attribute_scan_results_from_test_fixture_using_non_generic()
        {
            // arrange
            var builder = new ContainerBuilder();

            builder.RegisterModule(new WeakTypedScenarioMetadataModule(false));

            // act
            var items = builder.Build().Resolve<IEnumerable<Lazy<IWeakTypedScenario, IWeakTypedScenarioMetadata>>>();

            // assert
            Assert.Equal(1, items.Count());
            Assert.Equal(1, items.Where(p => p.Metadata.Name == "Hello").Count());
        }
    }
}
