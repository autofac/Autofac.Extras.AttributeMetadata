// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.CombinationalWeakTypedAttributeScenario
{
    [CombinationalWeakNameMetadata("Hello")]
    [CombinationalWeakAgeMetadata(42)]
    public class CombinationalWeakTypedScenario : ICombinationalWeakTypedScenario
    {
    }
}
