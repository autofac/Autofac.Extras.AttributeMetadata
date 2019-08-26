using System;
using System.ComponentModel.Composition;

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.CombinationalWeakTypedAttributeScenario
{
    [MetadataAttribute]
    public class CombinationalWeakNameMetadataAttribute : Attribute
    {
        public string Name { get; private set; }

        public CombinationalWeakNameMetadataAttribute(string name)
        {
            Name = name;
        }
    }
}
