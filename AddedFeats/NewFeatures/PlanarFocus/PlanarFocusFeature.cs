using AddedFeats.NewFeatures.AnimalFocus;
using AddedFeats.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using UniRx;

namespace AddedFeats.NewFeatures.PlanarFocusFeature
{
    internal class PlanarFocusFeature
    {
        private static readonly string FeatName = "PlanarFocusFeature";
        internal const string DisplayName = "PlanarFocusFeature.Name";
        private static readonly string Description = "PlanarFocusFeature.Description";

        public static void Configure()
        {
            PlanarFocusAir.Configure();
            PlanarFocusChaos.Configure();
            PlanarFocusCold.Configure();
            PlanarFocusEarth.Configure();
            PlanarFocusEvil.Configure();
            PlanarFocusFire.Configure();
            PlanarFocusGood.Configure();
            PlanarFocusLawful.Configure();
            PlanarFocusShadow.Configure();
            PlanarFocusWater.Configure();

            FeatureConfigurator.New(FeatName, Guids.PlanarFocusFeature, FeatureGroup.Feat)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFacts(new() { Guids.PlanarFocusAirActivatableAbility, Guids.PlanarFocusChaosActivatableAbility, 
                    Guids.PlanarFocusColdActivatableAbility, Guids.PlanarFocusEarthActivatableAbility, 
                    Guids.PlanarFocusEvilActivatableAbility, Guids.PlanarFocusFireActivatableAbility,
                    Guids.PlanarFocusGoodActivatableAbility, Guids.PlanarFocusLawfulActivatableAbility,
                    Guids.PlanarFocusShadowActivatableAbility, Guids.PlanarFocusWaterActivatableAbility})
                .AddPrerequisiteFeature(FeatureRefs.HunterAnimalFocusFeature.Reference.Get())
                .AddRecommendationHasFeature(FeatureRefs.HunterAnimalFocusFeature.Reference.Get())
                .AddPrerequisiteStatValue(StatType.SkillLoreReligion, 5, group: Kingmaker.Blueprints.Classes.Prerequisites.Prerequisite.GroupType.Any)
                .AddPrerequisiteStatValue(StatType.SkillKnowledgeArcana, 5, group: Kingmaker.Blueprints.Classes.Prerequisites.Prerequisite.GroupType.Any)
                .AddPrerequisiteStatValue(StatType.SkillLoreNature, 5, group: Kingmaker.Blueprints.Classes.Prerequisites.Prerequisite.GroupType.Any)
                .AddPrerequisiteStatValue(StatType.SkillKnowledgeWorld, 5, group: Kingmaker.Blueprints.Classes.Prerequisites.Prerequisite.GroupType.Any)
                .Configure();
        }
    }
}
