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
    internal class EvolvedCompanionResistance
    {
        private static readonly string FeatName = "EvolvedCompanionResistance";
        internal const string DisplayName = "EvolvedCompanionResistance.Name";
        private static readonly string Description = "EvolvedCompanionResistance.Description";

        public static void Configure()
        {
            FeatureConfigurator.New(FeatName + "Pet", Guids.EvolvedCompanionResistancePet)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFacts(new() { FeatureRefs.ScentFeature.Reference.Get() })
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.EvolvedCompanionResistance)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFeatureToPet(Guids.EvolvedCompanionResistancePet, Kingmaker.Enums.PetType.AnimalCompanion)
                .Configure();
        }
    }
}
