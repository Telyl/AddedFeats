using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddedFeats.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.Enums.Damage;

namespace AddedFeats.NewFeatures.EvolvedCompanion
{
    internal class EvolvedCompanionMagicAttacks
    {
        private static readonly string FeatName = "EvolvedCompanionMagicAttacks";
        internal const string DisplayName = "EvolvedCompanionMagicAttacks.Name";
        private static readonly string Description = "EvolvedCompanionMagicAttacks.Description";

        public static void Configure()
        {
            FeatureConfigurator.New(FeatName + "Pet", Guids.EvolvedCompanionMagicAttacksPet)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddOutgoingPhysicalDamageProperty(addMagic: true, material: PhysicalDamageMaterial.Adamantite, 
                    reality: DamageRealityType.Ghost, alignment: DamageAlignment.Good, naturalAttacks: true)
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.EvolvedCompanionMagicAttacks)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFeatureToPet(Guids.EvolvedCompanionMagicAttacksPet, Kingmaker.Enums.PetType.AnimalCompanion)
                .Configure();
        }
    }
}
