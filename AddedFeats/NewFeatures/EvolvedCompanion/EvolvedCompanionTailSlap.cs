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
    internal class EvolvedCompanionTailSlap
    {
        private static readonly string FeatName = "EvolvedCompanionTailSlap";
        internal const string DisplayName = "EvolvedCompanionTailSlap.Name";
        private static readonly string Description = "EvolvedCompanionTailSlap.Description";

        public static void Configure()
        {
            FeatureConfigurator.New(FeatName + "Pet", Guids.EvolvedCompanionTailSlapPet)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddSecondaryAttacks(ItemWeaponRefs.Tail1d6.Reference.Get())
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.EvolvedCompanionTailSlap)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFeatureToPet(Guids.EvolvedCompanionTailSlapPet, Kingmaker.Enums.PetType.AnimalCompanion)
                .AddPrerequisiteFeature(Guids.EvolvedCompanionTail)
                .Configure();
        }
    }
}
