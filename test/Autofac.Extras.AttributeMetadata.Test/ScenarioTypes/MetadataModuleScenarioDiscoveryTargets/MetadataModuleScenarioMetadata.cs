namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.MetadataModuleScenarioDiscoveryTargets
{
    public class MetadataModuleScenarioMetadata : IMetadataModuleScenarioMetadata
    {
        public MetadataModuleScenarioMetadata(string name)
        {
            this.Name = name;
        }

        // IExportScenario4Metadata Members
        public string Name { get; private set; }
    }
}
