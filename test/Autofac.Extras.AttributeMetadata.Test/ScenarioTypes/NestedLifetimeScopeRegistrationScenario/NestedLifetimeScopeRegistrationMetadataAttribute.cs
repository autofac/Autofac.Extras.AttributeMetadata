using System;
using System.ComponentModel.Composition;

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.NestedLifetimeScopeRegistrationScenario
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class NestedLifetimeScopeRegistrationMetadataAttribute : Attribute
    {
        public string Value { get; set; }
    }
}