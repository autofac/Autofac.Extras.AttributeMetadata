using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Autofac.Extras.AttributeMetadata;

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.MetadataProviderScenarioTypes
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class ProvidedMetadataAttribute : Attribute, IMetadataProvider
    {
        public IDictionary<string, object> GetMetadata(Type targetType)
        {
            return new Dictionary<string, object>()
            {
                { "Key1", "Value1" },
                { "Key2", "Value2" },
            };
        }
    }
}
