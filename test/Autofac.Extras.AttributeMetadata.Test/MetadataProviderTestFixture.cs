using System.Linq;
using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes;
using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.MetadataProviderScenarioTypes;
using Autofac.Features.Metadata;
using Xunit;

namespace Autofac.Extras.AttributeMetadata.Test
{
    public class MetadataProviderTestFixture
    {
        [Fact]
        public void Load_provided_into_weak_metadata()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AttributedMetadataModule>();
            builder.RegisterType<MetadataProviderScenario>()
                .As<IMetadataProviderScenario>();

            var container = builder.Build();
            var withMetadata = container.Resolve<Meta<IMetadataProviderScenario>>();

            Assert.NotNull(withMetadata);
            var value1 = withMetadata.Metadata.Where(kv => kv.Key == "Key1").FirstOrDefault();
            var value2 = withMetadata.Metadata.Where(kv => kv.Key == "Key2").FirstOrDefault();

            Assert.Equal("Value1", value1.Value);
            Assert.Equal("Value2", value2.Value);
        }

        [Fact]
        public void Load_provided_into_strong_metadata()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AttributedMetadataModule>();
            builder.RegisterType<MetadataProviderScenario>()
                .As<IMetadataProviderScenario>();

            var container = builder.Build();
            var withMetadata = container.Resolve<Meta<IMetadataProviderScenario, ProvidedMetadata>>();

            Assert.NotNull(withMetadata);
            Assert.NotNull(withMetadata.Metadata);
            Assert.Equal("Value1", withMetadata.Metadata.Key1);
            Assert.Equal("Value2", withMetadata.Metadata.Key2);
        }
    }
}
