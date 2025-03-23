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
    public class SpiritFocusBattle
    {
        private static readonly string FeatName = "SpiritFocusBattle";
        private static readonly string DisplayName = "SpiritFocusBattle.Name";
        private static readonly string Description = "SpiritFocusBattle.Description";
        
        public static (BlueprintAbility, BlueprintBuff) Configure(BlueprintAbilityResource spiritsgiftresource)
        {
            BlueprintBuff CustomBuff = BuffConfigurator.New(FeatName + "CustomBuff", Guids.CustomSpiritFocusBattleBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddStatBonus(ModifierDescriptor.Other, stat: StatType.AC, value: 2)
                .SetIcon(AbilityRefs.ShamanHexBattleWardAbility.Reference.Get().Icon)
                .SetStacking(StackingType.Replace)
                .SetFrequency(DurationRate.Rounds)
                .Configure();

            BlueprintBuff CompanionBattleBuff = BuffConfigurator.New(FeatName + "Buff", Guids.SpiritFocusBattleBuff)
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

            BlueprintAbility BattleActiveAbility = AbilityConfigurator.New(FeatName, Guids.SpiritFocusBattle)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.ShamanHexBattleWardAbility.Reference.Get().Icon)
                .AddActionPanelLogic(
                autoFillConditions:
                    ConditionsBuilder.New().CharacterClass(false, CharacterClassRefs.HunterClass.Reference.Get(), 0, true))
                .AddAbilityResourceLogic(isSpendResource: true, requiredResource: spiritsgiftresource, amount: 1)
                .AddAbilityResources(resource: spiritsgiftresource, restoreAmount: true)
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .ApplyBuff(CompanionBattleBuff, ContextDuration.Variable(ContextValues.Rank(), rate: DurationRate.Minutes)))
                .Configure();

            return (BattleActiveAbility, CompanionBattleBuff);

            
        }
    }
}

