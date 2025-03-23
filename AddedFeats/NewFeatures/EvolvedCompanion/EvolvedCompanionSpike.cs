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
    internal class EvolvedCompanionSpike
    {
        private static readonly string FeatName = "EvolvedCompanionSpike";
        internal const string DisplayName = "EvolvedCompanionSpike.Name";
        private static readonly string Description = "EvolvedCompanionSpike.Description";

        public static void Configure()
        {
            FeatureConfigurator.New(FeatName + "Pet", Guids.EvolvedCompanionSpikePet)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddAdditionalLimb(ItemWeaponRefs.Spike1d4.Cast<BlueprintItemWeaponReference>().Reference.Get())
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.EvolvedCompanionSpike)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFeatureToPet(Guids.EvolvedCompanionSpikePet, Kingmaker.Enums.PetType.AnimalCompanion)
                .AddPrerequisiteFeature(Guids.EvolvedCompanionTail)
                .Configure();
        }
    }
}
