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
    internal class EvolvedCompanionBite
    {
        private static readonly string FeatName = "EvolvedCompanionBite";
        internal const string DisplayName = "EvolvedCompanionBite.Name";
        private static readonly string Description = "EvolvedCompanionBite.Description";

        public static void Configure()
        {
            FeatureConfigurator.New(FeatName + "Pet", Guids.EvolvedCompanionBitePet)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddAdditionalLimb(ItemWeaponRefs.Bite1d6.Cast<BlueprintItemWeaponReference>().Reference.Get())
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.EvolvedCompanionBite)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFeatureToPet(Guids.EvolvedCompanionBitePet, Kingmaker.Enums.PetType.AnimalCompanion)
                .Configure();
        }
    }
}
