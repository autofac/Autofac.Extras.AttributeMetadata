using Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.WeakTypedMetadataAttributeScenario;
using Autofac.Integration.Mef;

namespace Autofac.Extras.AttributeMetadata.Test.ScenarioTypes.CombinationalWeakTypedAttributeScenario
{
    public class WeakTypeAttributedMetadataModule : AttributedMetadataModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterMetadataRegistrationSources();
            builder.RegisterType<WeakTypedScenario>().As<IWeakTypedScenario>();
            builder.RegisterType<CombinationalWeakTypedScenario>().As<ICombinationalWeakTypedScenario>();
        }
    }
}
