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
using Kingmaker.Enums.Damage;

namespace AddedFeats.Feats.SpiritFocus
{
    public class SpiritFocusWind
    {
        private static readonly string FeatName = "SpiritFocusWind";
        private static readonly string DisplayName = "SpiritFocusWind.Name";
        private static readonly string Description = "SpiritFocusWind.Description";
        
        public static (BlueprintAbility, BlueprintBuff) Configure(BlueprintAbilityResource spiritsgiftresource)
        {
            BlueprintBuff CustomBuff = BuffConfigurator.New(FeatName + "CustomBuff", Guids.CustomSpiritFocusWindBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddDamageResistanceEnergy(type: DamageEnergyType.Electricity, value: 10)
                .SetIcon(AbilityRefs.ResistElectricity.Reference.Get().Icon)
                .SetStacking(StackingType.Replace)
                .SetFrequency(DurationRate.Rounds)
                .Configure();

            BlueprintBuff CompanionBuff = BuffConfigurator.New(FeatName + "Buff", Guids.SpiritFocusWindBuff)
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

            BlueprintAbility ActiveAbility = AbilityConfigurator.New(FeatName, Guids.SpiritFocusWind)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.ResistElectricity.Reference.Get().Icon)
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

