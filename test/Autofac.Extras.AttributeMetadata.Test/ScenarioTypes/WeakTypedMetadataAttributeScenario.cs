using System;
using System.ComponentModel.Composition;

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes
{
    public interface IWeakTypedScenarioMetadata
    {
        string Name { get; }
    }

    public interface IWeakTypedScenario
    {
    }

    [MetadataAttribute]
    public class WeakTypedScenarioMetadataAttribute : Attribute
    {
        public string Name { get; private set; }

        public WeakTypedScenarioMetadataAttribute(string name)
        {
            Name = name;
        }
    }

    [WeakTypedScenarioMetadata("Hello")]
    public class WeakTypedScenario : IWeakTypedScenario
    {
    }
}