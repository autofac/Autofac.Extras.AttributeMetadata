using System;
using System.Linq;

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.StrongTypedMetadataAttributeScenario
{
    [StrongTypedScenarioMetadata("Hello", 42)]
    public class StrongTypedScenario : IStrongTypedScenario
    {
    }
}
