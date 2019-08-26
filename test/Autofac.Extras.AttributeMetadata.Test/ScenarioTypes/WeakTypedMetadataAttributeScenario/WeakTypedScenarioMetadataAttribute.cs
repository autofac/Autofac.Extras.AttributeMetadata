using System;
using System.ComponentModel.Composition;

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.WeakTypedMetadataAttributeScenario
{
    [MetadataAttribute]
    public class WeakTypedScenarioMetadataAttribute : Attribute
    {
        public string Name { get; private set; }

        public WeakTypedScenarioMetadataAttribute(string name)
        {
            Name = name;
        }
    }
}