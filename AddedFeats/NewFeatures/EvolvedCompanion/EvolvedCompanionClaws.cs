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
    internal class EvolvedCompanionClaws
    {
        private static readonly string FeatName = "EvolvedCompanionClaws";
        internal const string DisplayName = "EvolvedCompanionClaws.Name";
        private static readonly string Description = "EvolvedCompanionClaws.Description";

        public static void Configure()
        {
            FeatureConfigurator.New(FeatName + "Pet", Guids.EvolvedCompanionClawsPet)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddSecondaryAttacks(ItemWeaponRefs.Claw1d4.Reference.Get())
                .AddSecondaryAttacks(ItemWeaponRefs.Claw1d4.Reference.Get())
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.EvolvedCompanionClaws)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFeatureToPet(Guids.EvolvedCompanionClawsPet, Kingmaker.Enums.PetType.AnimalCompanion)
                .Configure();
        }
    }
}
