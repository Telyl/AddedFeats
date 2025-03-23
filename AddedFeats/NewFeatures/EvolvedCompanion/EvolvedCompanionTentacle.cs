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
    internal class EvolvedCompanionTentacle
    {
        private static readonly string FeatName = "EvolvedCompanionTentacle";
        internal const string DisplayName = "EvolvedCompanionTentacle.Name";
        private static readonly string Description = "EvolvedCompanionTentacle.Description";

        public static void Configure()
        {
            FeatureConfigurator.New(FeatName + "Pet", Guids.EvolvedCompanionTentaclePet)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddSecondaryAttacks(ItemWeaponRefs.Tentacle1d4.Reference.Get())
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.EvolvedCompanionTentacle)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFeatureToPet(Guids.EvolvedCompanionTentaclePet, Kingmaker.Enums.PetType.AnimalCompanion)
                .Configure();
        }
    }
}
