using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.Enums;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using BlueprintCore.Actions.Builder.BasicEx;
using AddedFeats.NewComponents;
using AddedFeats.Utils;
using AddedFeats.Feats;

namespace AddedFeats.Feats
{
    /// <summary>
    /// Creates a feat that does nothing but show up.
    /// </summary>
    public class PlanarFocus
    {
        private static readonly string FeatName = "PlanarFocus";
        private static readonly string DisplayName = "PlanarFocus.Name";
        private static readonly string Description = "PlanarFocus.Description";
        
        public static void Configure()
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

