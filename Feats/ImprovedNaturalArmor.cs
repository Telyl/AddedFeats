using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using AddedFeats.Utils;
using static UnityModManagerNet.UnityModManager.ModEntry;
using System;

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

        private static void ConfigureDisabled()
        {
            FeatureConfigurator.New(FeatName, Guids.ImprovedNaturalArmor).Configure();
        }

        private static void ConfigureEnabled()
        {
            FeatureConfigurator.New(FeatName, Guids.ImprovedNaturalArmor, FeatureGroup.Feat)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddStatBonus(Kingmaker.Enums.ModifierDescriptor.NaturalArmor, stat: StatType.AC, value: 1)
                .AddPrerequisiteClassLevel(CharacterClassRefs.AnimalCompanionClass.Reference.Get(), 1)
                .AddPrerequisiteFullStatValue(stat: StatType.Constitution, value: 13)
                .Configure();
        }
    }
}
