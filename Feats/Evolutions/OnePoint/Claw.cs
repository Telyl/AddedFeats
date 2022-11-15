using System;
using AddedFeats.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.BasicEx;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using static UnityModManagerNet.UnityModManager.ModEntry;


namespace AddedFeats.Feats
{
    public class Claw
    {
        private static readonly string FeatName = "Claw";
        internal const string DisplayName = "Claw.Name";
        private static readonly string Description = "Claw.Description";

        private static readonly ModLogger Logger = Logging.GetLogger(FeatName);

        public static void ConfigureDisabled()
        {
            FeatureConfigurator.New(FeatName, Guids.Claw).Configure();
            FeatureConfigurator.New(FeatName + "Pet", Guids.ClawPet).Configure();
        }

        public static BlueprintFeature ConfigureEnabled()
        {
            var PetFeat = FeatureConfigurator.New(FeatName + "Pet", Guids.ClawPet)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddAdditionalLimb(ItemWeaponRefs.Claw1d4.Reference.Get())
                .AddAdditionalLimb(ItemWeaponRefs.Claw1d4.Reference.Get())
                .Configure();

            // Add prereqs for conforming to a particular form, per rules.
            var Feat = FeatureConfigurator.New(FeatName, Guids.Claw)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFeatureToPet(PetFeat)
                .AddPrerequisiteFeaturesFromList(new() { FeatureRefs.AnimalCompanionFeatureDog.Reference.Get(),
                FeatureRefs.AnimalCompanionFeatureTriceratops.Reference.Get(),
                FeatureRefs.AnimalCompanionFeatureTriceratops_PreorderBonus.Reference.Get(),
                FeatureRefs.AnimalCompanionFeatureBoar.Reference.Get(),
                FeatureRefs.AnimalCompanionFeatureWolf.Reference.Get(),
                FeatureRefs.AnimalCompanionFeatureMonitor.Reference.Get() }, amount: 1)
                .Configure();

            return Feat;
        }
    }
}
