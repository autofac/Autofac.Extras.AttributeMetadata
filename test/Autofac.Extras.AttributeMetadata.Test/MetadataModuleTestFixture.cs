using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes;
using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.MetadataModuleScenarioDiscoveryTargets;
using Xunit;

namespace Autofac.Extras.AttributeMetadata.Test
{
    public class MetadataModuleTestFixture
    {
        [Fact]
        public void Metadata_module_scenario_validate_registration_content()
        {
            // arrange
            var builder = new ContainerBuilder();

            builder.RegisterModule(new StrongTypedScenarioMetadataModule());

            // act
            var items = builder.Build().Resolve<IEnumerable<Lazy<IMetadataModuleScenario, IMetadataModuleScenarioMetadata>>>();

            // assert
            Assert.Single(items.Where(p => p.Metadata.Name == "sid"));
            Assert.Single(items.Where(p => p.Metadata.Name == "nancy"));
            Assert.Single(items.Where(p => p.Metadata.Name == "the-cats"));

            // the following was not registered
            Assert.Empty(items.Where(p => p.Metadata.Name == "the-dogs"));
        }

        [Fact]
        public void Metadata_module_scenario_using_typeof_registration()
        {
            // arrange
            var builder = new ContainerBuilder();

            builder.RegisterModule(new TypeOfScenarioMetadataModule());

            // act
            var items = builder.Build().Resolve<IEnumerable<Lazy<IMetadataModuleScenario, IMetadataModuleScenarioMetadata>>>();

            // assert
            Assert.Single(items.Where(p => p.Metadata.Name == "sid"));
            Assert.Single(items.Where(p => p.Metadata.Name == "nancy"));
            Assert.Single(items.Where(p => p.Metadata.Name == "the-cats"));

            // the following was not registered
            Assert.Empty(items.Where(p => p.Metadata.Name == "the-dogs"));
        }
    }
}