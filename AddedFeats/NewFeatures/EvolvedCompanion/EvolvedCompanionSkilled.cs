using System;
using AddedFeats.Utils;
using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using static UnityModManagerNet.UnityModManager.ModEntry;


namespace AddedFeats.NewFeatures.EvolvedCompanion
{
    public class EvolvedCompanionSkilled
    {
        private static readonly string FeatName = "EvolvedCompanionSkilled";
        internal const string DisplayName = "EvolvedCompanionSkilled.Name";
        internal const string AthleticsDisplayName = "EvolvedCompanionSkilledAthletics.Name";
        internal const string MobilityDisplayName = "EvolvedCompanionSkilledMobility.Name";
        internal const string TrickeryDisplayName = "EvolvedCompanionSkilledTrickery.Name";
        internal const string StealthDisplayName = "EvolvedCompanionSkilledStealth.Name";
        internal const string ArcanaDisplayName = "EvolvedCompanionSkilledArcana.Name";
        internal const string WorldDisplayName = "EvolvedCompanionSkilledWorld.Name";
        internal const string NatureDisplayName = "EvolvedCompanionSkilledNature.Name";
        internal const string ReligionDisplayName = "EvolvedCompanionSkilledReligion.Name";
        internal const string PerceptionDisplayName = "EvolvedCompanionSkilledPerception.Name";
        internal const string PersuasionDisplayName = "EvolvedCompanionSkilledPersuasion.Name";
        internal const string UMDDisplayName = "EvolvedCompanionSkilledUMD.Name";
        private static readonly string Description = "EvolvedCompanionSkilled.Description";

        public static BlueprintFeature CreatePCPetFeats(string displayname, string statname, string guidpet, string guidpc, StatType skill)
        {
            var petfeat = FeatureConfigurator.New(FeatName + statname + "Pet", guidpet)
                    .SetDisplayName(displayname)
                    .SetDescription(Description)
                    .AddStatBonus(descriptor: ModifierDescriptor.Racial, stat: skill, value: 8)
                    .Configure();

            return FeatureConfigurator.New(FeatName + statname, guidpc)
                .SetDisplayName(displayname)
                .SetDescription(Description)
                .AddFeatureToPet(petfeat)
                .Configure();
        }
        public static void Configure()
        {

            BlueprintFeature athleticsfeat = CreatePCPetFeats(AthleticsDisplayName, "Athletics", Guids.EvolvedCompanionSkilledAthleticsPet, Guids.EvolvedCompanionSkilledAthleticsFeat, StatType.SkillAthletics);
            BlueprintFeature mobilityfeat = CreatePCPetFeats(MobilityDisplayName, "Mobility", Guids.EvolvedCompanionSkilledMobilityPet, Guids.EvolvedCompanionSkilledMobilityFeat, StatType.SkillMobility);
            BlueprintFeature trickeryfeat = CreatePCPetFeats(TrickeryDisplayName, "Trickery", Guids.EvolvedCompanionSkilledTrickeryPet, Guids.EvolvedCompanionSkilledTrickeryFeat, StatType.SkillThievery);
            BlueprintFeature stealthfeat = CreatePCPetFeats(StealthDisplayName, "Stealth", Guids.EvolvedCompanionSkilledStealthPet, Guids.EvolvedCompanionSkilledStealthFeat, StatType.SkillStealth);
            BlueprintFeature arcanafeat = CreatePCPetFeats(ArcanaDisplayName, "Arcana", Guids.EvolvedCompanionSkilledArcanaPet, Guids.EvolvedCompanionSkilledArcanaFeat, StatType.SkillKnowledgeArcana);
            BlueprintFeature worldfeat = CreatePCPetFeats(WorldDisplayName, "World", Guids.EvolvedCompanionSkilledWorldPet, Guids.EvolvedCompanionSkilledWorldFeat, StatType.SkillKnowledgeWorld);
            BlueprintFeature naturefeat = CreatePCPetFeats(NatureDisplayName, "Nature", Guids.EvolvedCompanionSkilledNaturePet, Guids.EvolvedCompanionSkilledNatureFeat, StatType.SkillLoreNature);
            BlueprintFeature religionfeat = CreatePCPetFeats(ReligionDisplayName, "Religion", Guids.EvolvedCompanionSkilledReligionPet, Guids.EvolvedCompanionSkilledReligionFeat, StatType.SkillLoreReligion);
            BlueprintFeature perceptionfeat = CreatePCPetFeats(PerceptionDisplayName, "Perception", Guids.EvolvedCompanionSkilledPerceptionPet, Guids.EvolvedCompanionSkilledPerceptionFeat, StatType.SkillPerception);
            BlueprintFeature persuasionfeat = CreatePCPetFeats(PersuasionDisplayName, "Persuasion", Guids.EvolvedCompanionSkilledPersuasionPet, Guids.EvolvedCompanionSkilledPersuasionFeat, StatType.SkillPersuasion);
            BlueprintFeature umdfeat = CreatePCPetFeats(UMDDisplayName, "UMD", Guids.EvolvedCompanionSkilledUMDPet, Guids.EvolvedCompanionSkilledUMDFeat, StatType.SkillUseMagicDevice);

            FeatureSelectionConfigurator.New(FeatName, Guids.EvolvedCompanionSkilled)
               .SetDisplayName(DisplayName)
               .SetDescription(Description)
               .AddToAllFeatures(athleticsfeat, mobilityfeat, trickeryfeat, stealthfeat, arcanafeat, worldfeat, naturefeat,
               religionfeat, perceptionfeat, persuasionfeat, umdfeat)
               .Configure();
        }


    }
}
