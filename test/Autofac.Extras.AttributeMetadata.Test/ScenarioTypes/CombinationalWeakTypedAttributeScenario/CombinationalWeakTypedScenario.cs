using System;

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.CombinationalWeakTypedAttributeScenario
{
    [CombinationalWeakNameMetadata("Hello")]
    [CombinationalWeakAgeMetadata(42)]
    public class CombinationalWeakTypedScenario : ICombinationalWeakTypedScenario
    {
    }
}
