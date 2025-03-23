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
    internal class EvolvedCompanionTail
    {
        private static readonly string FeatName = "EvolvedCompanionTail";
        internal const string DisplayName = "EvolvedCompanionTail.Name";
        private static readonly string Description = "EvolvedCompanionTail.Description";

        public static void Configure()
        {
            FeatureConfigurator.New(FeatName + "Pet", Guids.EvolvedCompanionTailPet)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddStatBonus(descriptor:Kingmaker.Enums.ModifierDescriptor.Racial, false, stat: Kingmaker.EntitySystem.Stats.StatType.SkillMobility, value: 2)
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.EvolvedCompanionTail)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFeatureToPet(Guids.EvolvedCompanionTailPet, Kingmaker.Enums.PetType.AnimalCompanion)
                .Configure();
        }
    }
}
