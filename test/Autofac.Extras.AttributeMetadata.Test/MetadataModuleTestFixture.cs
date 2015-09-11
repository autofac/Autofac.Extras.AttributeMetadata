using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes;
using Xunit;

namespace Autofac.Extras.AttributeMetadata.Test
{
    public class MetadataModuleTestFixture
    {
        [Fact]
        public void metadata_module_scenario_validate_registration_content()
        {
            // arrange
            var builder = new ContainerBuilder();

            builder.RegisterModule(new StrongTypedScenarioMetadataModule());

            // act
            var items = builder.Build().Resolve<IEnumerable<Lazy<IMetadataModuleScenario, IMetadataModuleScenarioMetadata>>>();

            // assert
            Assert.Equal(1, items.Where(p => p.Metadata.Name == "sid").Count());
            Assert.Equal(1, items.Where(p => p.Metadata.Name == "nancy").Count());
            Assert.Equal(1, items.Where(p => p.Metadata.Name == "the-cats").Count());

            // the following was not registered
            Assert.Equal(0, items.Where(p => p.Metadata.Name == "the-dogs").Count());
        }

        [Fact]
        public void metadata_module_scenario_using_typeof_registration()
        {
            // arrange
            var builder = new ContainerBuilder();

            builder.RegisterModule(new TypeOfScenarioMetadataModule());

            // act
            var items = builder.Build().Resolve<IEnumerable<Lazy<IMetadataModuleScenario, IMetadataModuleScenarioMetadata>>>();

            // assert
            Assert.Equal(1, items.Where(p => p.Metadata.Name == "sid").Count());
            Assert.Equal(1, items.Where(p => p.Metadata.Name == "nancy").Count());
            Assert.Equal(1, items.Where(p => p.Metadata.Name == "the-cats").Count());

            // the following was not registered
            Assert.Equal(0, items.Where(p => p.Metadata.Name == "the-dogs").Count());
        }
    }
}