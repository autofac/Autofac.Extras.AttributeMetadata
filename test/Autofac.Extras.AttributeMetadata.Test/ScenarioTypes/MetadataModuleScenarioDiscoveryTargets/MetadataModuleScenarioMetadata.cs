// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.MetadataModuleScenarioDiscoveryTargets
{
    public class MetadataModuleScenarioMetadata : IMetadataModuleScenarioMetadata
    {
        public MetadataModuleScenarioMetadata(string name)
        {
            Name = name;
        }

        // IExportScenario4Metadata Members
        public string Name { get; private set; }
    }
}
