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
using Kingmaker.EntitySystem.Stats;

namespace AddedFeats.Feats.SpiritFocus
{
    public class SpiritFocusMammoth
    {
        private static readonly string FeatName = "SpiritFocusMammoth";
        private static readonly string DisplayName = "SpiritFocusMammoth.Name";
        private static readonly string Description = "SpiritFocusMammoth.Description";
        public static (BlueprintAbility, BlueprintBuff) Configure(BlueprintAbilityResource spiritsgiftresource)
        {
            BlueprintBuff CustomBuff = BuffConfigurator.New(FeatName + "CustomBuff", Guids.CustomSpiritFocusMammothBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddStatBonus(ModifierDescriptor.Other, stat: StatType.Strength, value: 2)
                .SetIcon(AbilityRefs.ArmySummonMammoth.Reference.Get().Icon)
                .SetStacking(StackingType.Replace)
                .SetFrequency(DurationRate.Rounds)
                .Configure();

            BlueprintBuff CompanionBuff = BuffConfigurator.New(FeatName + "Buff", Guids.SpiritFocusMammothBuff)
               .SetDisplayName(DisplayName)
               .SetDescription(Description)
               .SetFlags(BlueprintBuff.Flags.HiddenInUi)
               .AddContextRankConfig(ContextRankConfigs.CharacterLevel())
               .AddFactContextActions(
                   activated:
                       ActionsBuilder.New()
                           .Conditional(
                               ConditionsBuilder.New().IsAnimalCompanion(),
                               ifTrue: ActionsBuilder.New().ApplyBuff(buff: CustomBuff, ContextDuration.Variable(ContextValues.Rank(), rate: DurationRate.Minutes)))
                       )
               .Configure();

            BlueprintAbility ActiveAbility = AbilityConfigurator.New(FeatName, Guids.SpiritFocusMammoth)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.ArmySummonMammoth.Reference.Get().Icon)
                .AddActionPanelLogic(
                autoFillConditions:
                    ConditionsBuilder.New().CharacterClass(false, CharacterClassRefs.HunterClass.Reference.Get(), 0, true))
                .AddAbilityResourceLogic(isSpendResource: true, requiredResource: spiritsgiftresource, amount: 1)
                .AddAbilityResources(resource: spiritsgiftresource, restoreAmount: true)
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .ApplyBuff(CompanionBuff, ContextDuration.Variable(ContextValues.Rank(), rate: DurationRate.Minutes)))
                .Configure();

            return (ActiveAbility, CompanionBuff);
        }
    }
}

