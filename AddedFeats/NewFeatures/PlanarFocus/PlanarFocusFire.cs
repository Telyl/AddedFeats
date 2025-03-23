using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddedFeats.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Conditions.Builder;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Commands.Base;
using System.Drawing;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics;
using BlueprintCore.Actions.Builder.ContextEx;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Utility;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using BlueprintCore.Utils;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Blueprints.Facts;
using BlueprintCore.Utils.Types;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using TabletopTweaks.Core.NewComponents;

namespace AddedFeats.NewFeatures.AnimalFocus
{
    internal class PlanarFocusFire
    {
        private static readonly string FeatName = "PlanarFocusFire";
        private static readonly string DisplayName = "PlanarFocusFire.Name";
        private static readonly string Description = "PlanarFocusFire.Description";

        private static readonly string FocusAnimalBuff = Guids.PlanarFocusFireBuff;
        private static readonly string FocusAnimalFeatureEffect = Guids.PlanarFocusFireFeatureEffect;
        private static readonly string FocusAnimalBuffEffect = Guids.PlanarFocusFireBuffEffect;
        private static readonly string FocusAnimalBuffCharacter = Guids.PlanarFocusFireBuffCharacter;
        private static readonly string FocusActivatableAbility = Guids.PlanarFocusFireActivatableAbility;

        private static readonly UnityEngine.Sprite icon = AbilityRefs.FireBlastAbility.Reference.Get().Icon;
        public static void Configure()
        {
            //Create AnimalBuff
            BuffConfigurator.New(FeatName + "Buff", FocusAnimalBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(icon)
                .SetIsClassFeature(false)
                .SetRanks(0)
                .SetStacking(StackingType.Replace)
                .SetAllowNonContextActions(false)
                .SetFlags(BlueprintBuff.Flags.StayOnDeath)
                .Configure();

            //Create FeatureEffect
            FeatureConfigurator.New(FeatName + "FeatureEffect", FocusAnimalFeatureEffect)
                .SetDisplayName(DisplayName)
                .SetHideInUI(true)
                .SetHideInCharacterSheetAndLevelUp(true)
                .SetHideNotAvailibleInUI(false)
                .SetRanks(1)
                .SetReapplyOnLevelUp(true)
                .SetIsClassFeature(true)

                //This is where we add the effect.
                .AddContextRankConfig(
                    ContextRankConfigs
                    .CharacterLevel()
                    .WithStartPlusDivStepProgression(4, 4, false))
                .AddComponent<AddAdditionalWeaponDamage>(c => {
                    c.Value = new ContextDiceValue()
                    {
                        DiceType = DiceType.D6,
                        DiceCountValue = new ContextValue()
                        {
                            ValueType = ContextValueType.Rank
                        },
                        BonusValue = 0
                    };
                    c.DamageType = new DamageTypeDescription()
                    {
                        Type = DamageType.Energy,
                        Energy = DamageEnergyType.Fire
                    };
                    c.CheckWeaponCatergoy = false;
                })

                .AddFactContextActions(
                    activated:
                        ActionsBuilder.New()
                            .ApplyBuffPermanent(FocusAnimalBuff, asChild: true, isFromSpell: false, isNotDispelable: false, toCaster: false, sameDuration: false),
                    deactivated:
                        ActionsBuilder.New()
                            .RemoveBuff(FocusAnimalBuff, removeRank: false, toCaster: false))
                .Configure();

            //Create AnimalBuffEffect
            BuffConfigurator.New(FeatName + "BuffEffect", FocusAnimalBuffEffect)
                .SetDisplayName(DisplayName)
                .AddFacts(new() { FocusAnimalFeatureEffect })
                .SetFlags(BlueprintBuff.Flags.HiddenInUi, BlueprintBuff.Flags.StayOnDeath)
                .Configure();

            //Create BuffCharacter
            BuffConfigurator.New(FeatName + "BuffCharacter", FocusAnimalBuffCharacter)
                .SetDisplayName(DisplayName)
                .AddFeatureToPet(FocusAnimalFeatureEffect, PetType.AnimalCompanion)
                .SetIsClassFeature(false)
                .SetRanks(0)
                .SetAllowNonContextActions(false)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi, BlueprintBuff.Flags.StayOnDeath)
                .AddFactContextActions(
                    activated:
                        ActionsBuilder.New()
                            .Conditional(
                                ConditionsBuilder.New().CasterHasFact(FeatureRefs.ForesterAnimalFocusFeature.Reference.Get()),
                                ifTrue: ActionsBuilder.New()
                                    .ApplyBuffPermanent(FocusAnimalBuffEffect, isNotDispelable: false, asChild: true)
                            .Conditional(
                                ConditionsBuilder.New().IsPetDead(),
                                ifTrue: ActionsBuilder.New()
                                    .ApplyBuffPermanent(FocusAnimalBuffEffect, isNotDispelable: false, asChild: true)
                                ))
                        )   
                .SetFrequency(DurationRate.Rounds)
                .Configure();

            //Create Activitable Ability

            ActivatableAbilityConfigurator.New(FeatName + "ActivatableAbility", FocusActivatableAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(icon)
                .AddActionPanelLogic(
                autoFillConditions:
                    ConditionsBuilder.New().CharacterClass(false, CharacterClassRefs.HunterClass.Reference.Get(), 0, true))
                .SetWeightInGroup(1)
                .SetAllowNonContextActions(false)
                .SetBuff(FocusAnimalBuffCharacter)
                .SetGroup(ActivatableAbilityGroup.HunterAnimalFocus)
                .SetIsOnByDefault(false)
                .SetDeactivateIfCombatEnded(false)
                .SetDeactivateAfterFirstRound(false)
                .SetDeactivateImmediately(true)
                .SetIsTargeted(false)
                .SetDeactivateIfOwnerDisabled(false)
                .SetDeactivateIfOwnerUnconscious(false)
                .SetOnlyInCombat(false)
                .SetDoNotTurnOffOnRest(false)
                .SetHiddenInUI(true)
                .SetActivationType(AbilityActivationType.Immediately)
                .SetActivateWithUnitCommand(UnitCommand.CommandType.Free)
                .SetActivateOnUnitAction(AbilityActivateOnUnitActionType.Attack)
                .Configure();

            ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.AnimalFocusHunterActivatable)
                .EditComponent<ActivatableAbilityVariants>(
                    c => c.m_Variants =
                        CommonTool.Append(
                        c.m_Variants, BlueprintTool.GetRef<BlueprintActivatableAbilityReference>(FocusActivatableAbility)))
                .Configure();

            BuffConfigurator.For(BuffRefs.HunterAnimalFocusForHimselfBuff.Reference.Get())
                .AddBuffExtraEffects(checkedBuff: FocusAnimalBuffCharacter, extraEffectBuff: FocusAnimalBuffEffect)
                .Configure();
        }
    }
}
