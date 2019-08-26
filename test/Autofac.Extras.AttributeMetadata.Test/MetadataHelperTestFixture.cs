using System.Linq;
using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes;
using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.CombinationalWeakTypedAttributeScenario;
using Xunit;

namespace Autofac.Extras.AttributeMetadata.Test
{
    public class MetadataHelperTestFixture
    {
        [Fact]
        public void Scan_multiple_attributes_into_one_enumerable_set()
        {
            var metadata = MetadataHelper.GetMetadata(typeof(CombinationalWeakTypedScenario));

            Assert.Equal(2, metadata.Count());
            Assert.Equal("Hello", metadata.Where(p => p.Key == "Name").FirstOrDefault().Value);
            Assert.Equal(42, metadata.Where(p => p.Key == "Age").FirstOrDefault().Value);
        }

        [Fact]
        public void Scan_single_attribute_into_an_enumerable_set()
        {
            var metadata = MetadataHelper.GetMetadata(typeof(WeakTypedScenario));

            Assert.Single(metadata);
            Assert.Equal("Hello", metadata.Where(p => p.Key == "Name").FirstOrDefault().Value);
        }

        [Fact]
        public void Scan_strongly_typed_attribute_into_an_enumerable_set()
        {
            var metadata = MetadataHelper.GetMetadata<IStrongTypedScenarioMetadata>(typeof(StrongTypedScenario));

            Assert.Equal(2, metadata.Count());
            Assert.Equal("Hello", metadata.Where(p => p.Key == "Name").FirstOrDefault().Value);
            Assert.Equal(42, metadata.Where(p => p.Key == "Age").FirstOrDefault().Value);
        }

        [Fact]
        public void Verify_that_unfound_strong_typed_attribute_results_in_empty_property_set()
        {
            var metadata = MetadataHelper.GetMetadata<IMetadataModuleScenarioMetadata>(typeof(MetadataModuleScenario));

            Assert.Empty(metadata);
        }

        [Fact]
        public void Verify_that_unfound_weakly_typed_attribute_results_in_empty_property_set()
        {
            var metadata = MetadataHelper.GetMetadata(typeof(MetadataModuleScenario));

            Assert.Empty(metadata);
        }
    }
}
