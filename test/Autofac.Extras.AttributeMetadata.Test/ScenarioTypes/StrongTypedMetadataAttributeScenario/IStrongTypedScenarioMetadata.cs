using System;
using System.Linq;

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.StrongTypedMetadataAttributeScenario
{
    public interface IStrongTypedScenarioMetadata
    {
        int Age { get; }

        string Name { get; }
    }
}
