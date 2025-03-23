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
    internal class EvolvedCompanionScent
    {
        private static readonly string FeatName = "EvolvedCompanionScent";
        internal const string DisplayName = "EvolvedCompanionScent.Name";
        private static readonly string Description = "EvolvedCompanionScent.Description";

        public static void Configure()
        {
            FeatureConfigurator.New(FeatName + "Pet", Guids.EvolvedCompanionScentPet)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFacts(new() { FeatureRefs.ScentFeature.Reference.Get() })
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.EvolvedCompanionScent)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFeatureToPet(Guids.EvolvedCompanionScentPet, Kingmaker.Enums.PetType.AnimalCompanion)
                .Configure();
        }
    }
}
