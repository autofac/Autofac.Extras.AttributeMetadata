// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.WeakTypedMetadataAttributeScenario
{
    /// <summary>
    /// This class demonstrates the ability to search for metadata based on metadata attributes instead of
    /// providing the metadata directly.
    /// </summary>
    public class WeakTypedScenarioMetadataModule : MetadataModule<IWeakTypedScenario, IWeakTypedScenarioMetadata>
    {
        private readonly bool _useGeneric;

        public WeakTypedScenarioMetadataModule(bool useGeneric)
        {
            _useGeneric = useGeneric;
        }

        public override void Register(IMetadataRegistrar<IWeakTypedScenario, IWeakTypedScenarioMetadata> registrar)
        {
            if (_useGeneric)
            {
                registrar.RegisterAttributedType<WeakTypedScenario>();
                return;
            }

            registrar.RegisterAttributedType(typeof(WeakTypedScenario));
        }
    }
}
