// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.CombinationalWeakTypedAttributeScenario;
using Autofac.Integration.Mef;
using Xunit;

namespace Autofac.Extras.AttributeMetadata.Test
{
    public class CombinationalWeakTypedAttributeScenarioTestFixture
    {
        /// <summary>
        /// This is a test that demonstrates the ability to combine multiple weak-typed attributes to
        /// constitute a single strongly-typed metadata instance resolved from the container.
        /// </summary>
        [Fact]
        public void Validate_wireup_of_generic_attributes_to_strongly_typed_metadata_on_resolve()
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
            Assert.Single(items);
            Assert.Single(items.Where(p => p.Metadata.Name == "Hello"));
            Assert.Single(items.Where(p => p.Metadata.Age == 42));
        }
    }
}
