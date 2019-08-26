namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes
{
    public class MetadataModuleScenario : IMetadataModuleScenario
    {
    }

    public class MetadataModuleScenarioAlternate : IMetadataModuleScenario
    {
    }

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
