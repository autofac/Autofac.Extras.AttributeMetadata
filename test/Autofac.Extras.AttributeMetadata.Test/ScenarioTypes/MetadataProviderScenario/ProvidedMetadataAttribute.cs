// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

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
