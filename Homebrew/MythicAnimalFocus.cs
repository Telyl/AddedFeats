using BlueprintCore.Blueprints.References;
using Kingmaker.UnitLogic.Mechanics;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes.Spells;
using AddedFeats.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;
using Kingmaker.UnitLogic.Abilities;
using static UnityModManagerNet.UnityModManager.ModEntry;
using System;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using Kingmaker.Blueprints.Classes;

namespace AddedFeats.Homebrew
{
    /// <summary>
    /// Adds the Strong Jaw spell that only effects creatures of AnimalType.
    /// </summary>
    public class MythicAnimalFocus
    {
        private static readonly string FeatName = "MythicAnimalFocus";
        internal const string DisplayName = "MythicAnimalFocus.Name";
        private static readonly string Description = "MythicAnimalFocus.Description";

        private static readonly ModLogger Logger = Logging.GetLogger(FeatName);

        internal static void Configure()
        {
            try
            {
                if (Settings.IsEnabled(Guids.MythicAnimalFocus))
                    ConfigureEnabled();
                else
                    ConfigureDisabled();
            }
            catch (Exception e)
            {
                Logger.LogException("MythicAnimalFocus.Configure", e);
            }
        }

        private static void ConfigureDisabled()
        {
            FeatureConfigurator.New(FeatName, Guids.MythicAnimalFocus).Configure();
        }

        public static void ConfigureEnabled()
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
                .Configure();
                
        }
    }
}

