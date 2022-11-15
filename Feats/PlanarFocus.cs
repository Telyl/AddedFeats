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
    /// <summary>
    /// Creates a feat that does nothing but show up.
    /// </summary>
    public class PlanarFocus
    {
        private static readonly string FeatName = "PlanarFocus";
        internal const string DisplayName = "PlanarFocus.Name";
        private static readonly string Description = "PlanarFocus.Description";

        private static readonly ModLogger Logger = Logging.GetLogger(FeatName);

        internal static void Configure()
        {
            try
            {
                if (Settings.IsEnabled(Guids.PlanarFocus))
                    ConfigureEnabled();
                else
                    ConfigureDisabled();
            }
            catch (Exception e)
            {
                Logger.LogException("PlanarFocus.Configure", e);
            }
        }

        private static void ConfigureDisabled()
        {
            FeatureConfigurator.New(FeatName, Guids.PlanarFocus).Configure();
            PlanarFoci.PlanarFocusAir.ConfigureDisabled();
            PlanarFoci.PlanarFocusChaotic.ConfigureDisabled();
            PlanarFoci.PlanarFocusCold.ConfigureDisabled();
            PlanarFoci.PlanarFocusEarth.ConfigureDisabled();
            PlanarFoci.PlanarFocusEvil.ConfigureDisabled();
            PlanarFoci.PlanarFocusFire.ConfigureDisabled();
            PlanarFoci.PlanarFocusGood.ConfigureDisabled();
            PlanarFoci.PlanarFocusLawful.ConfigureDisabled();
            PlanarFoci.PlanarFocusShadow.ConfigureDisabled();
            PlanarFoci.PlanarFocusWater.ConfigureDisabled();
        }

        public static void ConfigureEnabled()
        {
            (var PF_Air, var AirBuff, var AirAnimalBuffEffect) = PlanarFoci.PlanarFocusAir.Configure();
            (var PF_Chaotic, var ChaoticBuff, var ChaoticAnimalBuffEffect) = PlanarFoci.PlanarFocusChaotic.Configure();
            (var PF_Cold, var ColdBuff, var ColdAnimalBuffEffect) = PlanarFoci.PlanarFocusCold.Configure();
            (var PF_Earth, var EarthBuff, var EarthAnimalBuffEffect) = PlanarFoci.PlanarFocusEarth.Configure();
            (var PF_Evil, var EvilBuff, var EvilAnimalBuffEffect) = PlanarFoci.PlanarFocusEvil.Configure();
            (var PF_Fire, var FireBuff, var FireAnimalBuffEffect) = PlanarFoci.PlanarFocusFire.Configure();
            (var PF_Good, var GoodBuff, var GoodAnimalBuffEffect) = PlanarFoci.PlanarFocusGood.Configure();
            (var PF_Lawful, var LawfulBuff, var LawfulAnimalBuffEffect) = PlanarFoci.PlanarFocusLawful.Configure();
            (var PF_Shadow, var ShadowBuff, var ShadowAnimalBuffEffect) = PlanarFoci.PlanarFocusShadow.Configure();
            (var PF_Water, var WaterBuff, var WaterAnimalBuffEffect) = PlanarFoci.PlanarFocusWater.Configure();

            //Air, Chaotic, Cold, Earth, Evil, Fire, Good, Lawful, Shadow, Water.
            FeatureConfigurator.New(FeatName, Guids.PlanarFocus, FeatureGroup.Feat)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFacts(new() { PF_Air, PF_Chaotic, PF_Cold, PF_Earth, PF_Evil, PF_Fire, PF_Good, PF_Lawful, PF_Shadow, PF_Water })
                .AddPrerequisiteFeature(FeatureRefs.HunterAnimalFocusFeature.Reference.Get())
                .AddRecommendationHasFeature(FeatureRefs.HunterAnimalFocusFeature.Reference.Get())
                .AddPrerequisiteStatValue(StatType.SkillKnowledgeArcana, 5)
                .Configure();

            // This allows us to apply it to ourselves.
            BuffConfigurator.For(BuffRefs.HunterAnimalFocusForHimselfBuff.Reference.Get())
                .AddBuffExtraEffects(checkedBuff: AirBuff, extraEffectBuff: AirAnimalBuffEffect)
                .AddBuffExtraEffects(checkedBuff: ChaoticBuff, extraEffectBuff: ChaoticAnimalBuffEffect)
                .AddBuffExtraEffects(checkedBuff: ColdBuff, extraEffectBuff: ColdAnimalBuffEffect)
                .AddBuffExtraEffects(checkedBuff: EarthBuff, extraEffectBuff: EarthAnimalBuffEffect)
                .AddBuffExtraEffects(checkedBuff: EvilBuff, extraEffectBuff: EvilAnimalBuffEffect)
                .AddBuffExtraEffects(checkedBuff: GoodBuff, extraEffectBuff: GoodAnimalBuffEffect)
                .AddBuffExtraEffects(checkedBuff: FireBuff, extraEffectBuff: FireAnimalBuffEffect)
                .AddBuffExtraEffects(checkedBuff: LawfulBuff, extraEffectBuff: LawfulAnimalBuffEffect)
                .AddBuffExtraEffects(checkedBuff: ShadowBuff, extraEffectBuff: ShadowAnimalBuffEffect)
                .AddBuffExtraEffects(checkedBuff: WaterBuff, extraEffectBuff: WaterAnimalBuffEffect)
                .Configure();
        }
    }
}

