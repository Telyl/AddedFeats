using System;
using AddedFeats.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using static UnityModManagerNet.UnityModManager.ModEntry;


namespace AddedFeats.Feats
{
    public class ImprovedNaturalAttack
    {
        private static readonly string FeatName = "ImprovedNaturalAttack";
        internal const string DisplayName = "ImprovedNaturalAttack.Name";
        private static readonly string Description = "ImprovedNaturalAttack.Description";

        private static readonly ModLogger Logger = Logging.GetLogger(FeatName);

        internal static void Configure()
        {
            try
            {
                if (Settings.IsEnabled(Guids.ImprovedNaturalAttack))
                    ConfigureEnabled();
                else
                    ConfigureDisabled();
            }
            catch (Exception e)
            {
                Logger.LogException("ImprovedNaturalAttack.Configure", e);
            }
        }

        private static void ConfigureDisabled()
        {
            FeatureConfigurator.New(FeatName, Guids.ImprovedNaturalAttack).Configure();
        }

        private static void ConfigureEnabled()
        {
            FeatureConfigurator.New(FeatName, Guids.ImprovedNaturalAttack, FeatureGroup.Feat)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddIncreaseDiceSizeOnAttack(additionalSize: 1, categories: new WeaponCategory[] { WeaponCategory.Bite, WeaponCategory.Claw, WeaponCategory.Gore, WeaponCategory.Sting, WeaponCategory.Tail, WeaponCategory.Talon, WeaponCategory.Slam, WeaponCategory.Slam, WeaponCategory.Hoof })
                .AddPrerequisiteClassLevel(CharacterClassRefs.AnimalCompanionClass.Reference.Get(), 1)
                .AddPrerequisiteFullStatValue(stat: StatType.BaseAttackBonus, value: 4)
                .Configure();
        }
    }
}
