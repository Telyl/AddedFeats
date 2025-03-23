using BlueprintCore.Blueprints.References;
using AddedFeats.Utils;
using static UnityModManagerNet.UnityModManager.ModEntry;
using System;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using Kingmaker.Blueprints.Classes;
using static Kingmaker.UI.EnterNameDialogue;

namespace AddedFeats.NewHomebrew
{
    internal class MythicAnimalFocus
    {
        private static readonly string FeatName = "MythicAnimalFocus";
        internal const string DisplayName = "MythicAnimalFocus.Name";
        private static readonly string Description = "MythicAnimalFocus.Description";
        public static void Configure()
        {
            FeatureConfigurator.New(FeatName, Guids.MythicAnimalFocus, FeatureGroup.MythicAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddIncreaseActivatableAbilityGroupSize(Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityGroup.HunterAnimalFocus)
                .SetHideInUI(false)
                .SetHideInCharacterSheetAndLevelUp(false)
                .SetIsClassFeature(true)
                .SetReapplyOnLevelUp(false)
                .SetRanks(1)
                .SetIcon(FeatureRefs.HunterAnimalFocus.Reference.Get().Icon)
                .AddPrerequisiteFeature(FeatureRefs.HunterAnimalFocusFeature.Reference.Get())
                .AddRecommendationHasFeature(FeatureRefs.HunterAnimalFocusFeature.Reference.Get())
                .Configure();

        }
    }
}