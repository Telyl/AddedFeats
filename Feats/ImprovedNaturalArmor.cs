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
    public class ImprovedNaturalArmor
    {
        private static readonly string FeatName = "ImprovedNaturalArmor";
        internal const string DisplayName = "ImprovedNaturalArmor.Name";
        private static readonly string Description = "ImprovedNaturalArmor.Description";

        private static readonly ModLogger Logger = Logging.GetLogger(FeatName);

        internal static void Configure()
        {
            try
            {
                if (Settings.IsEnabled(Guids.ImprovedNaturalArmor))
                    ConfigureEnabled();
                else
                    ConfigureDisabled();
            }
            catch (Exception e)
            {
                Logger.LogException("ImprovedNaturalArmor.Configure", e);
            }
        }
        
        private static BlueprintFeature CreatePCandPetImpArmor(string featname, string guid, BlueprintFeature prereq = null)
        {
            if(prereq != null) {
                return FeatureConfigurator.New(featname, guid, FeatureGroup.Feat)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddStatBonus(ModifierDescriptor.UntypedStackable, stat: StatType.AC, value: 1)
                .AddPrerequisiteClassLevel(CharacterClassRefs.AnimalCompanionClass.Reference.Get(), 1)
                .AddPrerequisiteFullStatValue(stat: StatType.Constitution, value: 13)
                .AddPrerequisiteFeature(prereq)
                .AddRecommendationHasClasses(new() { CharacterClassRefs.AnimalCompanionClass.Reference.Get() })
                .SetHideNotAvailibleInUI()
                .Configure();
            }
            else
            {
                return FeatureConfigurator.New(featname, guid, FeatureGroup.Feat)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddStatBonus(ModifierDescriptor.UntypedStackable, stat: StatType.AC, value: 1)
                .AddPrerequisiteClassLevel(CharacterClassRefs.AnimalCompanionClass.Reference.Get(), 1)
                .AddPrerequisiteFullStatValue(stat: StatType.Constitution, value: 13)
                .AddRecommendationHasClasses(new() { CharacterClassRefs.AnimalCompanionClass.Reference.Get(), CharacterClassRefs.AnimalClass.Reference.Get() })
                .SetHideNotAvailibleInUI()
                .Configure();
            }
            
        }
        private static void ConfigureDisabled()
        {
            FeatureConfigurator.New(FeatName, Guids.ImprovedNaturalArmor).Configure();
            FeatureConfigurator.New(FeatName + "2", Guids.ImprovedNaturalArmor2).Configure();
            FeatureConfigurator.New(FeatName + "3", Guids.ImprovedNaturalArmor3).Configure();
            FeatureConfigurator.New(FeatName + "4", Guids.ImprovedNaturalArmor4).Configure();
            FeatureConfigurator.New(FeatName + "5", Guids.ImprovedNaturalArmor5).Configure();
            FeatureConfigurator.New(FeatName + "6", Guids.ImprovedNaturalArmor6).Configure();
            FeatureConfigurator.New(FeatName + "7", Guids.ImprovedNaturalArmor7).Configure();
            FeatureConfigurator.New(FeatName + "8", Guids.ImprovedNaturalArmor8).Configure();
            FeatureConfigurator.New(FeatName + "9", Guids.ImprovedNaturalArmor9).Configure();
            FeatureConfigurator.New(FeatName + "10", Guids.ImprovedNaturalArmor10).Configure();
        }

        private static void ConfigureEnabled()
        {
            var INA1 = CreatePCandPetImpArmor(FeatName, Guids.ImprovedNaturalArmor);
            var INA2 = CreatePCandPetImpArmor(FeatName + "2", Guids.ImprovedNaturalArmor2, INA1);
            var INA3 = CreatePCandPetImpArmor(FeatName + "3", Guids.ImprovedNaturalArmor3, INA2);
            var INA4 = CreatePCandPetImpArmor(FeatName + "4", Guids.ImprovedNaturalArmor4, INA3);
            var INA5 = CreatePCandPetImpArmor(FeatName + "5", Guids.ImprovedNaturalArmor5, INA4);
            var INA6 = CreatePCandPetImpArmor(FeatName + "6", Guids.ImprovedNaturalArmor6, INA5);
            var INA7 = CreatePCandPetImpArmor(FeatName + "7", Guids.ImprovedNaturalArmor7, INA6);
            var INA8 = CreatePCandPetImpArmor(FeatName + "8", Guids.ImprovedNaturalArmor8, INA7);
            var INA9 = CreatePCandPetImpArmor(FeatName + "9", Guids.ImprovedNaturalArmor9, INA8);
            var INA10 = CreatePCandPetImpArmor(FeatName + "10", Guids.ImprovedNaturalArmor10, INA9);
        }
    }
}
