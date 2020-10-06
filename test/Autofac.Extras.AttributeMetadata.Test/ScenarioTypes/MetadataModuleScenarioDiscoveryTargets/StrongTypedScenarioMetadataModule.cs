﻿// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.MetadataModuleScenarioDiscoveryTargets
{
    /// <summary>
    /// This class demonstrates programmatic or non-attribute based discovery of metadata types.  This could also be
    /// used to provide variable, non-compile time wireup to the registration of objects.
    /// </summary>
    public class StrongTypedScenarioMetadataModule : MetadataModule<IMetadataModuleScenario, IMetadataModuleScenarioMetadata>
    {
        public override void Register(IMetadataRegistrar<IMetadataModuleScenario, IMetadataModuleScenarioMetadata> registrar)
        {
            registrar.RegisterType<MetadataModuleScenario>(new MetadataModuleScenarioMetadata("sid"));
            registrar.RegisterType<MetadataModuleScenario>(new MetadataModuleScenarioMetadata("nancy"));

            // in addition, we'll register an additional metadata variant of the alternate scenario 4 type
            registrar.RegisterType<MetadataModuleScenarioAlternate>(new MetadataModuleScenarioMetadata("the-cats"));
        }
    }
}
