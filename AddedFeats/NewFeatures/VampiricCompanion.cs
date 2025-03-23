using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using AddedFeats.Utils;
using Kingmaker.UnitLogic.Alignments;

namespace AddedFeats.NewFeatures
{
    public class VampiricCompanion
    {
        private static readonly string FeatName = "VampiricCompanion";
        internal const string DisplayName = "VampiricCompanion.Name";
        private static readonly string Description = "VampiricCompanion.Description";
        public static void Configure()
        {
            FeatureConfigurator.New(FeatName, Guids.VampiricCompanion, FeatureGroup.Feat)
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