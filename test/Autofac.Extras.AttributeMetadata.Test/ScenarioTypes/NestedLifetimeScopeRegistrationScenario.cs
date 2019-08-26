using System;
using System.ComponentModel.Composition;

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class NestedLifetimeScopeRegistrationMetadataAttribute : Attribute
    {
        public string Value { get; set; }
    }

    public interface ILifetimeScopeRegistrationInstance
    {
    }

    [NestedLifetimeScopeRegistrationMetadataAttribute(Value = "ParentLifetime")]
    public class NestedLifetimeScopeRegistrationInstance : ILifetimeScopeRegistrationInstance
    {
    }
}