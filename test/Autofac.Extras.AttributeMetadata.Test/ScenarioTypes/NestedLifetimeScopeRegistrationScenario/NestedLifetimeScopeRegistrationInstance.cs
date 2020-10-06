// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.NestedLifetimeScopeRegistrationScenario
{
    [NestedLifetimeScopeRegistrationMetadataAttribute(Value = "ParentLifetime")]
    public class NestedLifetimeScopeRegistrationInstance : ILifetimeScopeRegistrationInstance
    {
    }
}
