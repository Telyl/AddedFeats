using AddedFeats.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes;

namespace AddedFeats.NewArchetypes
{
    internal class Stalker
    {
        private static readonly string ArchetypeName = "Stalker";
        internal const string DisplayName = "Stalker.Name";
        private static readonly string Description = "Stalker.Description";
        
        internal const string TacticsDisplayName = "StalkerTactics.Name";
        private static readonly string TacticsDescription = "StalkerTactics.Description";
        public static void Configure()
        {
            var StalkerTactics = FeatureConfigurator.New("StalkerTactics", Guids.StalkerTactics)
                .SetDisplayName(TacticsDisplayName)
                .SetDescription(TacticsDescription)
                .SetIsClassFeature(true)
                .SetReapplyOnLevelUp(true)
                .AddFeatureToPet(FeatureRefs.SneakAttack.ToString())
                .AddFeatureToPet(FeatureRefs.DebilitatingInjury.ToString())
                .Configure();



            ArchetypeConfigurator.New(ArchetypeName, Guids.Stalker, BlueprintTool.Get<BlueprintCharacterClass>(CharacterClassRefs.HunterClass.ToString()))
                .SetLocalizedName(DisplayName)
                .SetLocalizedDescription(Description)
                .SetRemoveSpellbook(false)
                .SetReplaceClassSkills(false)
                .SetReplaceStartingEquipment(false)
                .AddToRemoveFeatures(6, FeatureSelectionRefs.HunterTeamworkFeatSelection.ToString())
                .AddToRemoveFeatures(12, FeatureSelectionRefs.HunterTeamworkFeatSelection.ToString())
                .AddToRemoveFeatures(18, FeatureSelectionRefs.HunterTeamworkFeatSelection.ToString())
                .AddToAddFeatures(2, FeatureRefs.SneakAttack.ToString())
                .AddToAddFeatures(5, FeatureRefs.SneakAttack.ToString())
                .AddToAddFeatures(8, FeatureRefs.SneakAttack.ToString())
                .AddToAddFeatures(9, StalkerTactics)
                .AddToAddFeatures(11, FeatureRefs.SneakAttack.ToString())
                .AddToAddFeatures(14, FeatureRefs.SneakAttack.ToString())
                .AddToAddFeatures(17, FeatureRefs.SneakAttack.ToString())
                .Configure();
        }
    }
}
