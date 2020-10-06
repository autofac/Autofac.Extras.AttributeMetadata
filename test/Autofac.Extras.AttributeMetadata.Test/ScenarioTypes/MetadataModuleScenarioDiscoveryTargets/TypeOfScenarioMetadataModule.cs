// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.MetadataModuleScenarioDiscoveryTargets
{
    public class TypeOfScenarioMetadataModule : MetadataModule<IMetadataModuleScenario, IMetadataModuleScenarioMetadata>
    {
        public override void Register(IMetadataRegistrar<IMetadataModuleScenario, IMetadataModuleScenarioMetadata> registrar)
        {
            registrar.RegisterType(typeof(MetadataModuleScenario), new MetadataModuleScenarioMetadata("sid"));
            registrar.RegisterType(typeof(MetadataModuleScenario), new MetadataModuleScenarioMetadata("nancy"));

            // in addition, we'll register an additional metadata variant of the alternate scenario 4 type
            registrar.RegisterType(typeof(MetadataModuleScenarioAlternate), new MetadataModuleScenarioMetadata("the-cats"));
        }
    }
}
