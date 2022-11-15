using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using AddedFeats.Utils;
using static UnityModManagerNet.UnityModManager.ModEntry;
using System;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Alignments;
using BlueprintCore.Conditions.Builder;

namespace AddedFeats.Feats
{
    public class VampiricCompanion
    {
        private static readonly string FeatName = "VampiricCompanion";
        internal const string DisplayName = "VampiricCompanion.Name";
        private static readonly string Description = "VampiricCompanion.Description";

        private static readonly ModLogger Logger = Logging.GetLogger(FeatName);

        internal static void Configure()
        {
            try
            {
                if (Settings.IsEnabled(Guids.VampiricCompanionFeat))
                    ConfigureEnabled();
                else
                    ConfigureDisabled();
            }
            catch (Exception e)
            {
                Logger.LogException("VampiricCompanion.Configure", e);
            }
        }
        private static void ConfigureDisabled()
        {
            FeatureConfigurator.New(FeatName, Guids.VampiricCompanionFeat).Configure();
        }

        private static void ConfigureEnabled()
        {  
            FeatureConfigurator.New(FeatName, Guids.VampiricCompanionFeat, FeatureGroup.Feat)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFeatureToPet(FeatureRefs.FastHealing.Reference.Get())
                .AddFeatureToPet(FeatureRefs.UndeadType.Reference.Get())
                .AddPrerequisiteFeature(RaceRefs.DhampirRace.Reference.Get())
                .AddPrerequisitePet()
                .AddPrerequisiteAlignment(AlignmentMaskType.Evil | AlignmentMaskType.TrueNeutral | AlignmentMaskType.ChaoticNeutral | AlignmentMaskType.LawfulNeutral)
                .AddPrerequisiteCharacterLevel(10)
                .Configure();
        }
    }
}
