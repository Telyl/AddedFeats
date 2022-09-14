using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics;

namespace AddedFeats.Utils
{
    /// <summary>
    /// The Planar Focus building blocks
    /// </summary>
    public class PlanarFunctions
    {
        public static BlueprintActivatableAbility CreatePlanarFocusAbility(string abilityname, string abilityguid, string displayname, string description, UnityEngine.Sprite Icon, BlueprintBuff bpbuff)
        {
            return ActivatableAbilityConfigurator.New(abilityname, abilityguid)
                .SetDisplayName(displayname)
                .SetDescription(description)
                .SetIcon(Icon)
                .AddActionPanelLogic(
                autoFillConditions:
                    ConditionsBuilder.New().CharacterClass(false, CharacterClassRefs.HunterClass.Reference.Get(), 0, true))
                .SetAllowNonContextActions(false)
                .SetBuff(bpbuff)
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
                .SetActivationType(AbilityActivationType.Immediately)
                .SetActivateWithUnitCommand(UnitCommand.CommandType.Free)
                .SetActivateOnUnitAction(AbilityActivateOnUnitActionType.Attack)
                .Configure();
        }

        /* This is perfectly fine and should NEVER change */
        public static BlueprintBuff CreatePlanarFocusAnimalBuff(string animalbuffname, string animalbuffguid, string displayname, string description, UnityEngine.Sprite Icon)
        {
            return BuffConfigurator.New(animalbuffname, animalbuffguid)
                .SetDisplayName(displayname)
                .SetDescription(description)
                .SetIcon(Icon)
                .SetIsClassFeature(false)
                .SetRanks(0)
                .SetStacking(StackingType.Replace)
                .SetAllowNonContextActions(false)
                .SetFlags(BlueprintBuff.Flags.StayOnDeath)
                .Configure();
        }

        public static BlueprintBuff CreatePlanarFocusBuff(string buffname, string buffguid, string displayname, UnityEngine.Sprite Icon, BlueprintFeature animalfeature, BlueprintBuff buffeffect)
        {
            return BuffConfigurator.New(buffname, buffguid)
                .SetDisplayName(displayname)
                .SetIcon(Icon)
                .AddFeatureToPet(animalfeature, PetType.AnimalCompanion)
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
                                    .ApplyBuffPermanent(buffeffect, isNotDispelable: false, asChild: true)
                            .Conditional(
                                ConditionsBuilder.New().IsPetDead(),
                                ifTrue: ActionsBuilder.New()
                                    .ApplyBuffPermanent(buffeffect, isNotDispelable: false, asChild: true)
                                ))
                        )
                .SetFrequency(DurationRate.Rounds)
                .Configure();

        }

        /* Adds the Effect. This will be customized later. */
        public static BlueprintFeature CreatePlanarFocusEffect(string animalfeaturename, string animalfeatureguid, string displayname, BlueprintBuff animalbuff)
        {
            return FeatureConfigurator.New(animalfeaturename, animalfeatureguid)
                .SetDisplayName(displayname)
                .SetHideInUI(true)
                .SetHideInCharacterSheetAndLevelUp(true)
                .SetHideNotAvailibleInUI(false)
                .SetRanks(1)
                .SetReapplyOnLevelUp(true)
                .SetIsClassFeature(true)
                .AddFactContextActions(
                    activated:
                        ActionsBuilder.New()
                            .ApplyBuffPermanent(animalbuff, asChild: true, isFromSpell: false, isNotDispelable: false, toCaster: false, sameDuration: false),
                    deactivated:
                        ActionsBuilder.New()
                            .RemoveBuff(animalbuff, removeRank: false, toCaster: false))
                .Configure();
        }

        public static BlueprintBuff CreatePlanarFocusBuffEffect(string buffeffectname, string buffeffectguid, string displayname, BlueprintFeature effect)
        {
            return BuffConfigurator.New(buffeffectname, buffeffectguid)
                .SetDisplayName(displayname)
                .AddFacts(new() { effect })
                .SetFlags(BlueprintBuff.Flags.HiddenInUi, BlueprintBuff.Flags.StayOnDeath)
                .Configure();
        }

    }
}

