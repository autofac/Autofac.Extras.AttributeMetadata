using System;

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.NestedLifetimeScopeRegistrationScenario
{
    [NestedLifetimeScopeRegistrationMetadataAttribute(Value = "ParentLifetime")]
    public class NestedLifetimeScopeRegistrationInstance : ILifetimeScopeRegistrationInstance
    {
    }
}