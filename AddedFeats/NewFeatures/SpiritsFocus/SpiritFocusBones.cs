using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.References;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using AddedFeats.Utils;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using Kingmaker.Enums;

namespace AddedFeats.Feats.SpiritFocus
{
    /// <summary>
    /// Creates a feat that does nothing but show up.
    /// </summary>
    public class SpiritFocusBones
    {
        private static readonly string FeatName = "SpiritFocusBones";
        private static readonly string DisplayName = "SpiritFocusBones.Name";
        private static readonly string Description = "SpiritFocusBones.Description";
        
        public static (BlueprintAbility, BlueprintBuff) Configure(BlueprintAbilityResource spiritsgiftresource)
        {
            BlueprintBuff CustomBuff = BuffConfigurator.New(FeatName + "CustomBuff", Guids.CustomSpiritFocusBonesBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddConcealment(descriptor: ConcealmentDescriptor.Blur, concealment: Concealment.Partial, checkDistance: false, checkWeaponRangeType: false)
                .AddFormationACBonus(bonus: 1)
                .SetIcon(AbilityRefs.BoneExplosion.Reference.Get().Icon)
                .SetStacking(StackingType.Replace)
                .SetFrequency(DurationRate.Rounds)
                .Configure();

            BlueprintBuff CompanionBonesBuff = BuffConfigurator.New(FeatName + "Buff", Guids.SpiritFocusBonesBuff)
               .SetDisplayName(DisplayName)
               .SetDescription(Description)
               .SetFlags(BlueprintBuff.Flags.HiddenInUi)
               .AddContextRankConfig(ContextRankConfigs.CharacterLevel())
               .AddFactContextActions(
                   activated:
                       ActionsBuilder.New()
                           .Conditional(
                               ConditionsBuilder.New().IsAnimalCompanion(),
                               ifTrue: ActionsBuilder.New()
                               .ApplyBuff(buff: CustomBuff, ContextDuration.Variable(ContextValues.Rank(), rate: DurationRate.Minutes)))
                       )
               .Configure();

            BlueprintAbility BonesActiveAbility = AbilityConfigurator.New(FeatName, Guids.SpiritFocusBones)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.BoneExplosion.Reference.Get().Icon)
                .AddActionPanelLogic(
                autoFillConditions:
                    ConditionsBuilder.New().CharacterClass(false, CharacterClassRefs.HunterClass.Reference.Get(), 0, true))
                .AddAbilityResourceLogic(isSpendResource: true, requiredResource: spiritsgiftresource, amount: 1)
                .AddAbilityResources(resource: spiritsgiftresource, restoreAmount: true)
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .ApplyBuff(CompanionBonesBuff, ContextDuration.Variable(ContextValues.Rank(), rate: DurationRate.Minutes)))
                .Configure();

            return (BonesActiveAbility, CompanionBonesBuff);
        }
    }
}

