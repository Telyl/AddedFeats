using AddedFeats.Utils;
using Kingmaker.EntitySystem.Stats;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;

namespace AddedFeats.NewFeatures.EvolvedCompanion
{
    public class EvolvedCompanion
    {
        private static readonly string FeatName = "EvolvedCompanion";
        internal const string DisplayName = "EvolvedCompanion.Name";
        private static readonly string Description = "EvolvedCompanion.Description";
        public static void Configure()
        {
            EvolvedCompanionBite.Configure();
            EvolvedCompanionClaws.Configure();
            EvolvedCompanionMagicAttacks.Configure();
            EvolvedCompanionPincers.Configure();
            EvolvedCompanionResistance.Configure();
            EvolvedCompanionScent.Configure();
            EvolvedCompanionSkilled.Configure();
            EvolvedCompanionSlam.Configure();         
            EvolvedCompanionSpike.Configure();
            EvolvedCompanionTail.Configure();
            EvolvedCompanionTailSlap.Configure();
            EvolvedCompanionTentacle.Configure();

            var selection = FeatureSelectionConfigurator.New(FeatName, Guids.EvolvedCompanion)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddToAllFeatures(Guids.EvolvedCompanionClaws, Guids.EvolvedCompanionBite,
                Guids.EvolvedCompanionMagicAttacks,
                Guids.EvolvedCompanionPincers, Guids.EvolvedCompanionScent, Guids.EvolvedCompanionSkilled,
                Guids.EvolvedCompanionSlam, Guids.EvolvedCompanionSpike, Guids.EvolvedCompanionTail,
                Guids.EvolvedCompanionTailSlap, Guids.EvolvedCompanionTentacle)
                .AddPrerequisiteFullStatValue(stat: StatType.Charisma, value: 13)
                .AddPrerequisitePet()
                .SetHideInUI(true)
                .SetHideInCharacterSheetAndLevelUp(true)
                .SetHideNotAvailibleInUI(false)
                .Configure();

            var basicFeatSelectionGuid = "247a4068-296e-8be4-2890-143f451b4b45";
            FeatureSelectionConfigurator.For(basicFeatSelectionGuid)
                .AddToAllFeatures(selection)
                .Configure();
        }
    }
}