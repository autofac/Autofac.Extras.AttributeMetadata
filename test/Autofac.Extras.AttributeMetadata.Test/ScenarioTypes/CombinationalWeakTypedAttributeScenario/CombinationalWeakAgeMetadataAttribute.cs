using System;
using System.ComponentModel.Composition;

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.CombinationalWeakTypedAttributeScenario
{
    [MetadataAttribute]
    public class CombinationalWeakAgeMetadataAttribute : Attribute
    {
        public int Age { get; private set; }

        public CombinationalWeakAgeMetadataAttribute(int age)
        {
            Age = age;
        }
    }
}
