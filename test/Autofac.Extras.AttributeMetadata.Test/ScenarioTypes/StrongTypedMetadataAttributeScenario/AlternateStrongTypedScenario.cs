using System;
using System.Linq;

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.StrongTypedMetadataAttributeScenario
{
    [StrongTypedScenarioMetadata("Goodbye", 24)]
    public class AlternateStrongTypedScenario : IStrongTypedScenario
    {
    }
}
