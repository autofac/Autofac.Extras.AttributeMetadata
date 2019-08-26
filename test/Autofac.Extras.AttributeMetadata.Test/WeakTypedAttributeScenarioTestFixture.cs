using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes;
using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.WeakTypedMetadataAttributeScenario;
using Autofac.Integration.Mef;
using Xunit;

namespace Autofac.Extras.AttributeMetadata.Test
{
    public class WeakTypedAttributeScenarioTestFixture
    {
        [Fact]
        public void Validate_wireup_of_generic_attributes_to_strongly_typed_metadata_on_resolve()
        {
            // arrange
            var builder = new ContainerBuilder();
            builder.RegisterMetadataRegistrationSources();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .As<IWeakTypedScenario>()
                .WithAttributedMetadata();

            // act
            var items = builder.Build().Resolve<IEnumerable<Lazy<IWeakTypedScenario, IWeakTypedScenarioMetadata>>>();

            // assert
            Assert.Single(items);
            Assert.Single(items.Where(p => p.Metadata.Name == "Hello"));
        }
    }
}
