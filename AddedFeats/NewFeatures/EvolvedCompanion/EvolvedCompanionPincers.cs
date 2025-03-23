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
    internal class EvolvedCompanionPincers
    {
        private static readonly string FeatName = "EvolvedCompanionPincers";
        internal const string DisplayName = "EvolvedCompanionPincers.Name";
        private static readonly string Description = "EvolvedCompanionPincers.Description";

        public static void Configure()
        {
            FeatureConfigurator.New(FeatName + "Pet", Guids.EvolvedCompanionPincersPet)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddSecondaryAttacks(ItemWeaponRefs.Pincers1d6.Reference.Get())
                .AddSecondaryAttacks(ItemWeaponRefs.Pincers1d6.Reference.Get())
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.EvolvedCompanionPincers)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFeatureToPet(Guids.EvolvedCompanionPincersPet, Kingmaker.Enums.PetType.AnimalCompanion)
                .Configure();
        }
    }
}
