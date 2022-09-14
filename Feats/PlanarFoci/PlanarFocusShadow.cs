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

namespace AddedFeats.Feats.PlanarFoci
{
    /// <summary>
    /// Creates a feat that does nothing but show up.
    /// </summary>
    public class PlanarFocusShadow
    {
        private static readonly string FeatName = "PlanarFocusShadow";
        private static readonly string DisplayName = "PlanarFocusShadow.Name";
        private static readonly string Description = "PlanarFocusShadow.Description";
        
        public static (BlueprintActivatableAbility, BlueprintBuff, BlueprintBuff) Configure()
        {
            /************************* Add skill bonuses *************************/

            BlueprintBuff ShadowAnimalBuff = PlanarFunctions.CreatePlanarFocusAnimalBuff(
                FeatName+"AnimalBuff", Guids.PlanarFocusShadowAnimalBuff, DisplayName, Description, AbilityRefs.MirrorImage.Reference.Get().Icon);


            BlueprintFeature ShadowEffect = PlanarFunctions.CreatePlanarFocusEffect(
                FeatName+"Effect", Guids.PlanarFocusShadowEffect, DisplayName, ShadowAnimalBuff);

            FeatureConfigurator.For(ShadowEffect)
                .AddStatBonus(descriptor: ModifierDescriptor.Other, stat: StatType.SkillStealth, value: 5)
                .AddStatBonus(descriptor: ModifierDescriptor.Other, stat: StatType.SkillThievery, value: 5)
                .Configure();

            BlueprintBuff ShadowAnimalBuffEffect = PlanarFunctions.CreatePlanarFocusBuffEffect(
                FeatName+"AnimalBuffEffect", Guids.PlanarFocusShadowAnimalBuffEffect , DisplayName, ShadowEffect);

            BlueprintBuff ShadowBuff = PlanarFunctions.CreatePlanarFocusBuff(
                FeatName+"Buff", Guids.PlanarFocusShadowBuff, DisplayName, AbilityRefs.MirrorImage.Reference.Get().Icon, ShadowEffect, ShadowAnimalBuffEffect);

            BlueprintActivatableAbility PF_Shadow = PlanarFunctions.CreatePlanarFocusAbility(
                FeatName, Guids.PlanarFocusShadow, DisplayName, Description, AbilityRefs.MirrorImage.Reference.Get().Icon, ShadowBuff);

            return (PF_Shadow, ShadowBuff, ShadowAnimalBuffEffect);
        }
    }
}

