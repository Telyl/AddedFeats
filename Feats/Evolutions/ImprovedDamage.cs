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
    public class ImprovedDamage
    {
        private static readonly string FeatName = "ImprovedDamage";
        internal const string DisplayName = "ImprovedDamage.Name";
        private static readonly string Description = "ImprovedDamage.Description";

        private static readonly ModLogger Logger = Logging.GetLogger(FeatName);

        internal static void Configure()
        {
            try
            {
                if (Settings.IsEnabled(Guids.ImprovedDamage))
                    ConfigureEnabled();
                else
                    ConfigureDisabled();
            }
            catch (Exception e)
            {
                Logger.LogException("ImprovedDamage.Configure", e);
            }
        }

        private static void ConfigureDisabled()
        {
            FeatureConfigurator.New(FeatName, Guids.ImprovedDamage).Configure();
        }

        private static void ConfigureEnabled()
        {
            var PetFeat = FeatureConfigurator.New(FeatName + "Pet", Guids.ImprovedDamagePet)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddIncreaseDiceSizeOnAttack(additionalSize: 1, categories: new WeaponCategory[] { WeaponCategory.Bite, WeaponCategory.Claw, WeaponCategory.Gore, WeaponCategory.Sting, WeaponCategory.Tail, WeaponCategory.Talon, WeaponCategory.Slam, WeaponCategory.Slam, WeaponCategory.Hoof })
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.ImprovedDamage, FeatureGroup.Feat)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFeatureToPet(PetFeat)
                .AddPrerequisitePet()
                .Configure();
        }
    }
}
