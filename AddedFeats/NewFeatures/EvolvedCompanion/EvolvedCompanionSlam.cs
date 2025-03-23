using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddedFeats.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;

namespace AddedFeats.NewFeatures.EvolvedCompanion
{
    internal class EvolvedCompanionSlam
    {
        private static readonly string FeatName = "EvolvedCompanionSlam";
        internal const string DisplayName = "EvolvedCompanionSlam.Name";
        private static readonly string Description = "EvolvedCompanionSlam.Description";

        public static void Configure()
        {
            FeatureConfigurator.New(FeatName + "Pet", Guids.EvolvedCompanionSlamPet)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddAdditionalLimb(ItemWeaponRefs.Slam1d8.Cast<BlueprintItemWeaponReference>().Reference.Get())
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.EvolvedCompanionSlam)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFeatureToPet(Guids.EvolvedCompanionSlamPet, Kingmaker.Enums.PetType.AnimalCompanion)
                .Configure();
        }
    }
}
