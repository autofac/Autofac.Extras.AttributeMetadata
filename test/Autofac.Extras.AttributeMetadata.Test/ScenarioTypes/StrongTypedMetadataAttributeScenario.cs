using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes
{
    public interface IStrongTypedScenarioMetadata
    {
        string Name { get; }

        int Age { get; }
    }

    public interface IStrongTypedScenario
    {
    }

    [MetadataAttribute]
    public class StrongTypedScenarioMetadataAttribute : Attribute, IStrongTypedScenarioMetadata
    {
        public StrongTypedScenarioMetadataAttribute(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public string Name { get; private set; }

        public int Age { get; private set; }
    }

    [StrongTypedScenarioMetadata("Hello", 42)]
    public class StrongTypedScenario : IStrongTypedScenario
    {
    }

    [StrongTypedScenarioMetadata("Goodbye", 24)]
    public class AlternateStrongTypedScenario : IStrongTypedScenario
    {
    }
}
