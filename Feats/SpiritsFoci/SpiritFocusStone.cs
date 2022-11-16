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
using static UnityModManagerNet.UnityModManager.ModEntry;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;

namespace AddedFeats.Feats.SpiritFoci
{
    public class SpiritFocusStone
    {
        private static readonly string FeatName = "SpiritFocusStone";
        private static readonly string DisplayName = "SpiritFocusStone.Name";
        private static readonly string Description = "SpiritFocusStone.Description";
        
        private static readonly ModLogger Logger = Logging.GetLogger(FeatName);
        public static void ConfigureDisabled()
        {
            BuffConfigurator.New(FeatName + "Buff", Guids.SpiritFocusStoneBuff).Configure();
            BuffConfigurator.New(FeatName + "CustomBuff", Guids.CustomSpiritFocusStoneBuff).Configure();
            ActivatableAbilityConfigurator.New(FeatName, Guids.SpiritFocusStone).Configure();
        }

        public static (BlueprintAbility, BlueprintBuff) Configure(BlueprintAbilityResource spiritsgiftresource)
        {
            BlueprintBuff CustomBuff = BuffConfigurator.New(FeatName + "CustomBuff", Guids.CustomSpiritFocusStoneBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddDamageResistancePhysical(value: 5, material: PhysicalDamageMaterial.Adamantite, bypassedByMaterial: true)
                .SetIcon(AbilityRefs.Stoneskin.Reference.Get().Icon)
                .SetStacking(StackingType.Replace)
                .SetFrequency(DurationRate.Rounds)
                .Configure();

            BlueprintBuff CompanionBuff = BuffConfigurator.New(FeatName + "Buff", Guids.SpiritFocusStoneBuff)
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

            BlueprintAbility ActiveAbility = AbilityConfigurator.New(FeatName, Guids.SpiritFocusStone)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.Stoneskin.Reference.Get().Icon)
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

