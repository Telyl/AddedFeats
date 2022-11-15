using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using AddedFeats.Utils;
using static UnityModManagerNet.UnityModManager.ModEntry;
using System;
using Kingmaker.Enums;

namespace AddedFeats.Feats
{
    public class EvolutionImprovedNaturalArmor
    {
        private static readonly string FeatName = "EvolutionImprovedNaturalArmor";
        internal const string DisplayName = "EvolutionImprovedNaturalArmor.Name";
        private static readonly string Description = "EvolutionImprovedNaturalArmor.Description";

        private static readonly ModLogger Logger = Logging.GetLogger(FeatName);

        public static void ConfigureDisabled()
        {
            FeatureConfigurator.New(FeatName +"1", Guids.EvolutionImprovedNaturalArmor1).Configure();
            FeatureConfigurator.New(FeatName + "1Pet", Guids.EvolutionImprovedNaturalArmorPet1).Configure();
            FeatureConfigurator.New(FeatName + "2", Guids.EvolutionImprovedNaturalArmor2).Configure();
            FeatureConfigurator.New(FeatName + "2Pet", Guids.EvolutionImprovedNaturalArmorPet2).Configure();
            FeatureConfigurator.New(FeatName + "3", Guids.EvolutionImprovedNaturalArmor3).Configure();
            FeatureConfigurator.New(FeatName + "3Pet", Guids.EvolutionImprovedNaturalArmorPet3).Configure();
            FeatureConfigurator.New(FeatName + "4", Guids.EvolutionImprovedNaturalArmor4).Configure();
            FeatureConfigurator.New(FeatName + "4Pet", Guids.EvolutionImprovedNaturalArmorPet4).Configure();
            FeatureConfigurator.New(FeatName + "5", Guids.EvolutionImprovedNaturalArmor5).Configure();
            FeatureConfigurator.New(FeatName + "5Pet", Guids.EvolutionImprovedNaturalArmorPet5).Configure();
        }

        public static BlueprintFeature CreatePcandPetNatArmor(string featname, string guidpet, string guidpc, BlueprintFeature prereq = null, int levelreq = 1)
        {

            var petfeat = FeatureConfigurator.New(featname + "Pet", guidpet)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddStatBonus(descriptor: ModifierDescriptor.UntypedStackable, stat: StatType.AC, value: 2)
                .Configure();

            if (prereq != null)
            {
                return FeatureConfigurator.New(featname, guidpc)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFeatureToPet(petfeat)
                .AddPrerequisiteFeature(prereq)
                .SetHideNotAvailibleInUI()
                .AddPrerequisiteCharacterLevel(levelreq)
                .Configure();
            }
            else
            {
                return FeatureConfigurator.New(featname, guidpc)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFeatureToPet(petfeat)
                .Configure();
            }
        }

        public static BlueprintFeature[] ConfigureEnabled()
        {
            BlueprintFeature feat1 = CreatePcandPetNatArmor(FeatName + "1", Guids.EvolutionImprovedNaturalArmorPet1, Guids.EvolutionImprovedNaturalArmor1);
            BlueprintFeature feat2 = CreatePcandPetNatArmor(FeatName + "2", Guids.EvolutionImprovedNaturalArmorPet2, Guids.EvolutionImprovedNaturalArmor2, feat1, 5);
            BlueprintFeature feat3 = CreatePcandPetNatArmor(FeatName + "3", Guids.EvolutionImprovedNaturalArmorPet3, Guids.EvolutionImprovedNaturalArmor3, feat2, 10);
            BlueprintFeature feat4 = CreatePcandPetNatArmor(FeatName + "4", Guids.EvolutionImprovedNaturalArmorPet4, Guids.EvolutionImprovedNaturalArmor4, feat3, 15);
            BlueprintFeature feat5 = CreatePcandPetNatArmor(FeatName + "5", Guids.EvolutionImprovedNaturalArmorPet5, Guids.EvolutionImprovedNaturalArmor5, feat4, 20);

            BlueprintFeature[] features = { feat1, feat2, feat3, feat4, feat5 };

            return features;
        }
    }
}
