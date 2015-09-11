using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes;
using Autofac.Integration.Mef;
using Xunit;

namespace Autofac.Extras.AttributeMetadata.Test
{
    public class CombinationalWeakTypedAttributeScenarioTestFixture
    {
        /// <summary>
        /// This is a test that demonstrates the ability to combine multiple weak-typed attributes to
        /// constitute a single strongly-typed metadata instance resolved from the container
        /// </summary>
        [Fact]
        public void validate_wireup_of_generic_attributes_to_strongly_typed_metadata_on_resolve()
        {
            // arrange
            var builder = new ContainerBuilder();
            builder.RegisterMetadataRegistrationSources();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .As<ICombinationalWeakTypedScenario>()
                .WithAttributedMetadata();

            // act
            var items = builder.Build().Resolve<IEnumerable<Lazy<ICombinationalWeakTypedScenario, ICombinationalWeakTypedScenarioMetadata>>>();

            // assert
            Assert.Equal(1, items.Count());
            Assert.Equal(1, items.Where(p => p.Metadata.Name == "Hello").Count());
            Assert.Equal(1, items.Where(p => p.Metadata.Age == 42).Count());
        }
    }
}
