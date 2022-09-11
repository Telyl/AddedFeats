using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.Enums;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.Blueprints;
using System.Collections.Generic;
using Kingmaker.UnitLogic;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using System.Linq;
using BlueprintCore.Utils;
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
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.Blueprints.Items.Ecnchantments;
using AddedFeats.NewComponents;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums.Damage;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;

namespace AddedFeats.Feats
{
    /// <summary>
    /// Creates a feat that does nothing but show up.
    /// </summary>
    public class PlanarFocus
    {
        private static readonly string FeatName = "PlanarFocus";
        private static readonly string FeatGuid = "3d64ed44-42f3-4a8f-b7ae-a927a45af820";
        private static readonly string DisplayName = "PlanarFocus.Name";
        private static readonly string Description = "PlanarFocus.Description";
        static BlueprintActivatableAbility CreatePlanarFocusAbility(string abilityname, string abilityguid, string displayname, string description, UnityEngine.Sprite Icon, BlueprintBuff bpbuff)
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
        static BlueprintBuff CreatePlanarFocusAnimalBuff(string animalbuffname, string animalbuffguid, string displayname, string description, UnityEngine.Sprite Icon)
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

        static BlueprintBuff CreatePlanarFocusBuff(string buffname, string buffguid, string displayname, UnityEngine.Sprite Icon, BlueprintFeature animalfeature, BlueprintBuff buffeffect)
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
        static BlueprintFeature CreatePlanarFocusEffect(string animalfeaturename, string animalfeatureguid, string displayname, BlueprintBuff animalbuff)
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
                            .ApplyBuffPermanent(animalbuff, asChild: true, isFromSpell: false, isNotDispelable:false, toCaster: false, sameDuration: false),
                    deactivated:
                        ActionsBuilder.New()
                            .RemoveBuff(animalbuff, removeRank: false, toCaster: false))
                .Configure();
        }

        static BlueprintBuff CreatePlanarFocusBuffEffect(string buffeffectname, string buffeffectguid, string displayname, BlueprintFeature effect)
        {
            return BuffConfigurator.New(buffeffectname, buffeffectguid)
                .SetDisplayName(displayname)
                .AddFacts(new() { effect })
                .SetFlags(BlueprintBuff.Flags.HiddenInUi, BlueprintBuff.Flags.StayOnDeath)
                .Configure();
        }

        public static void Configure()
        {

            /************************* Adds Airborne feature *************************/
            BlueprintBuff AirAnimalBuff = CreatePlanarFocusAnimalBuff(
                "PlanarFocusAirAnimalBuff", "56722f70-03e5-4272-9aa3-115382ae869f", "PlanarFocusAir.Name", "PlanarFocusAir.Description", AbilityRefs.WindsOfVengeance.Reference.Get().Icon);

            BlueprintFeature AirEffect = CreatePlanarFocusEffect(
                "PlanarFocusAirEffect", "06c88e20-b823-44b9-b15f-e69fc4c6a556", "PlanarFocusAir.Name", AirAnimalBuff);

            FeatureConfigurator.For(AirEffect)
                .AddSpellImmunityToSpellDescriptor(null, descriptor: SpellDescriptor.Ground)
                .AddBuffDescriptorImmunity(descriptor: SpellDescriptor.Ground)
                .AddConditionImmunity(UnitCondition.DifficultTerrain)
                .Configure();

            BlueprintBuff AirAnimalBuffEffect = CreatePlanarFocusBuffEffect(
                "PlanarFocusAirAnimalBuffEffect", "e55cc808-4cf2-4272-88f5-bda65d0417fc", "PlanarFocusAir.Name", AirEffect);

            BlueprintBuff AirBuff = CreatePlanarFocusBuff(
                "PlanarFocusAirBuff", "31d9d157-fb70-4997-a0b3-56f9026d1c40", "PlanarFocusAir.Name", AbilityRefs.WindsOfVengeance.Reference.Get().Icon, AirEffect, AirAnimalBuffEffect);

            BlueprintActivatableAbility PF_Air = CreatePlanarFocusAbility(
                "PlanarFocusAir", "31c9d157-fb70-4997-a0b3-56f9026d1c40", "PlanarFocusAir.Name", "PlanarFocusAir.Description", AbilityRefs.WindsOfVengeance.Reference.Get().Icon, AirBuff);


            /************************* Adds Fortification 25 *************************/
            BlueprintBuff ChaoticAnimalBuff = CreatePlanarFocusAnimalBuff(
                "PlanarFocusChaoticAnimalBuff", "fd904798-952b-45aa-b594-0841156bdc7a", "PlanarFocusChaos.Name", "PlanarFocusChaos.Description", AbilityRefs.ProtectionFromChaos.Reference.Get().Icon);

            BlueprintFeature ChaoticEffect = CreatePlanarFocusEffect(
                "PlanarFocusChaoticEffect", "b5741066-de62-447c-8ba5-e265aa4e1292", "PlanarFocusChaos.Name", ChaoticAnimalBuff);
            FeatureConfigurator.For(ChaoticEffect)
                .AddFortification(25)
                .Configure();

            BlueprintBuff ChaoticAnimalBuffEffect = CreatePlanarFocusBuffEffect(
                "PlanarFocusChaoticAnimalBuffEffect", "502cba69-f94a-4399-b37f-3577ad8018a2", "PlanarFocusChaos.Name", ChaoticEffect);

            BlueprintBuff ChaoticBuff = CreatePlanarFocusBuff(
                "PlanarFocusChaoticBuff", "db903e19-7586-45a5-ac05-3cb8b410f039", "PlanarFocusChaos.Name", AbilityRefs.WindsOfVengeance.Reference.Get().Icon, ChaoticEffect, ChaoticAnimalBuffEffect);

            BlueprintActivatableAbility PF_Chaotic = CreatePlanarFocusAbility(
                "PlanarFocusChaos", "4828c37e-3d59-4358-a042-b9fa4fb7bb72", "PlanarFocusChaos.Name", "PlanarFocusChaos.Description", AbilityRefs.ProtectionFromChaos.Reference.Get().Icon, ChaoticBuff);


            /************************* Add thorns 1d4 *************************/
            BlueprintBuff ColdAnimalBuff = CreatePlanarFocusAnimalBuff(
                "PlanarFocusColdAnimalBuff", "958b8209-4ece-4233-b592-87c3997fc073", "PlanarFocusCold.Name", "PlanarFocusCold.Description", AbilityRefs.ProtectionFromCold.Reference.Get().Icon);


            BlueprintFeature ColdEffect = CreatePlanarFocusEffect(
                "PlanarFocusColdEffect", "818f4cdb-2939-4824-90cc-118269fe5203", "PlanarFocusCold.Name", ColdAnimalBuff);

            FeatureConfigurator.For(ColdEffect)
                .AddContextRankConfig(
                    ContextRankConfigs
                    .ClassLevel(new string[] { CharacterClassRefs.HunterClass.ToString() }, false, AbilityRankType.Default, 20, 0)
                    .WithStartPlusDivStepProgression(4, 0, false))
                .AddTargetAttackRollTrigger(onlyHit: true, onlyMelee: true, doNotPassAttackRoll: true,
                    actionsOnAttacker:
                        ActionsBuilder.New()
                            .DealDamage(new DamageTypeDescription()
                            {
                                Type = DamageType.Energy,
                                Common = new DamageTypeDescription.CommomData(),
                                Physical = new DamageTypeDescription.PhysicalData(),
                                Energy = DamageEnergyType.Cold
                            },
                            new ContextDiceValue()
                            {
                                DiceType = DiceType.D4,
                                DiceCountValue = new ContextValue()
                                {
                                    ValueType = ContextValueType.Rank
                                },
                                BonusValue = new ContextValue(),
                            }, halfIfSaved: false, isAoE: false))
                .Configure();

            BlueprintBuff ColdAnimalBuffEffect = CreatePlanarFocusBuffEffect(
                "PlanarFocusColdAnimalBuffEffect", "775bb39b-8d48-40d9-bed7-55f2411575dd", "PlanarFocusCold.Name", ColdEffect);

            BlueprintBuff ColdBuff = CreatePlanarFocusBuff(
                "PlanarFocusColdBuff", "f47e182b-f8d3-4477-b78e-075fc8205892", "PlanarFocusCold.Name", AbilityRefs.ProtectionFromCold.Reference.Get().Icon, ColdEffect, ColdAnimalBuffEffect);

            BlueprintActivatableAbility PF_Cold = CreatePlanarFocusAbility("PlanarFocusCold",
                                                                          "cc239df8-dfbf-49d2-86ca-ea462717ab4d",
                                                                          "PlanarFocusCold.Name",
                                                                          "PlanarFocusCold.Description",
                                                                          AbilityRefs.ProtectionFromCold.Reference.Get().Icon,
                                                                          ColdBuff);


            /************************* Adds +2 Nat armor & Bullrush *************************/

            BlueprintBuff EarthAnimalBuff = CreatePlanarFocusAnimalBuff(
                "PlanarFocusEarthAnimalBuff", "b0f6b4b8-3dc7-4197-95ad-b242db6651bd", "PlanarFocusEarth.Name", "PlanarFocusEarth.Description", AbilityRefs.Stoneskin.Reference.Get().Icon);


            BlueprintFeature EarthEffect = CreatePlanarFocusEffect(
                "PlanarFocusEarthEffect", "6ebaa1dc-a6d2-48f9-81fa-80eb1d822b6e", "PlanarFocusEarth.Name", EarthAnimalBuff);

            FeatureConfigurator.For(EarthEffect)
                .AddStatBonus(descriptor: ModifierDescriptor.NaturalArmorEnhancement, stat: StatType.AC, value: 2)
                .AddManeuverBonus(2, descriptor: ModifierDescriptor.Other, false, CombatManeuver.BullRush)
                .AddManeuverDefenceBonus(2, descriptor: ModifierDescriptor.Other, CombatManeuver.BullRush)
                .Configure();

            BlueprintBuff EarthAnimalBuffEffect = CreatePlanarFocusBuffEffect(
                "PlanarFocusEarthAnimalBuffEffect", "9d3fbbc5-0609-4fe3-9cd2-53991c10620d", "PlanarFocusEarth.Name", EarthEffect);

            BlueprintBuff EarthBuff = CreatePlanarFocusBuff(
                "PlanarFocusEarthBuff", "7119f97b-110a-4001-b770-8b5eb08799e5", "PlanarFocusEarth.Name", AbilityRefs.Stoneskin.Reference.Get().Icon, EarthEffect, EarthAnimalBuffEffect);


            BlueprintActivatableAbility PF_Earth = CreatePlanarFocusAbility("PlanarFocusEarth",
                                                                          "548da62f-f01a-420c-9528-e8bee75a7d34",
                                                                          "PlanarFocusEarth.Name",
                                                                          "PlanarFocusEarth.Description",
                                                                          AbilityRefs.Stoneskin.Reference.Get().Icon,
                                                                          EarthBuff);

            /************************* +1 profane bonus to AC. This bonus increases to +2. *************************/

            BlueprintBuff EvilAnimalBuff = CreatePlanarFocusAnimalBuff(
                "PlanarFocusEvilAnimalBuff", "856c95f9-05a9-4dd6-89b3-6f2ebd51eef2", "PlanarFocusEvil.Name", "PlanarFocusEvil.Description", AbilityRefs.ProfaneNimbus.Reference.Get().Icon);


            BlueprintFeature EvilEffect = CreatePlanarFocusEffect(
                "PlanarFocusEvilEffect", "a54b8dd2-d0cc-44d3-bd6e-068274031a3b", "PlanarFocusEvil.Name", EvilAnimalBuff);

            FeatureConfigurator.For(EvilEffect)
                .AddACBonusAgainstFactOwner(AlignmentComponent.Good, 2, FeatureRefs.OutsiderType.Reference.Get(), descriptor: ModifierDescriptor.Profane)
                .AddSavingThrowBonusAgainstFact(AlignmentComponent.Good, FeatureRefs.OutsiderType.Reference.Get(), descriptor: ModifierDescriptor.Profane, 2)
                .Configure();

            BlueprintBuff EvilAnimalBuffEffect = CreatePlanarFocusBuffEffect(
                "PlanarFocusEvilAnimalBuffEffect", "b81875c8-313b-473f-a16c-be34c011b579", "PlanarFocusEvil.Name", EvilEffect);

            BlueprintBuff EvilBuff = CreatePlanarFocusBuff(
                "PlanarFocusEvilBuff", "2d294f73-f191-4309-b103-7a3eecc505b0", "PlanarFocusEvil.Name", AbilityRefs.ProfaneNimbus.Reference.Get().Icon, EvilEffect, EvilAnimalBuffEffect);

            BlueprintActivatableAbility PF_Evil = CreatePlanarFocusAbility("PlanarFocusEvil",
                                                                          "db542378-f53b-4d5c-ada2-461cea97a6b3",
                                                                          "PlanarFocusEvil.Name",
                                                                          "PlanarFocusEvil.Description",
                                                                          AbilityRefs.ProfaneNimbus.Reference.Get().Icon,
                                                                          EvilBuff);

            /************************* Add thorns 1d6 *************************/

            BlueprintBuff FireAnimalBuff = CreatePlanarFocusAnimalBuff(
                "PlanarFocusFireAnimalBuff", "595d616f-73ac-4da7-aa3b-402eeb53ee79", "PlanarFocusFire.Name", "PlanarFocusFire.Description", AbilityRefs.ResistFire.Reference.Get().Icon);


            BlueprintFeature FireEffect = CreatePlanarFocusEffect(
                "PlanarFocusFireEffect", "9899c88c-2e31-425b-946e-829e8666387e", "PlanarFocusFire.Name", FireAnimalBuff);
       
            FeatureConfigurator.For(FireEffect)
                .AddContextRankConfig(
                    ContextRankConfigs
                    .ClassLevel(new string[] { CharacterClassRefs.HunterClass.ToString() }, false, AbilityRankType.Default, 20, 0)
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
                .Configure();

            BlueprintBuff FireAnimalBuffEffect = CreatePlanarFocusBuffEffect(
                "PlanarFocusFireAnimalBuffEffect", "d2864f28-e5f9-4421-ad0f-c64f63124906", "PlanarFocusFire.Name", FireEffect);

            BlueprintBuff FireBuff = CreatePlanarFocusBuff(
                "PlanarFocusFireBuff", "456b7bc9-600b-4ac6-8a57-3f5d6061753d", "PlanarFocusFire.Name", AbilityRefs.ResistFire.Reference.Get().Icon, FireEffect, FireAnimalBuffEffect);

            BlueprintActivatableAbility PF_Fire = CreatePlanarFocusAbility("PlanarFocusFire",
                                                                          "33628596-101f-4c2d-bcf8-e6814ddda153",
                                                                          "PlanarFocusFire.Name",
                                                                          "PlanarFocusFire.Description",
                                                                          AbilityRefs.ResistFire.Reference.Get().Icon,
                                                                          FireBuff);

            /************************* Add Sacred Bonus *************************/

            BlueprintBuff GoodAnimalBuff = CreatePlanarFocusAnimalBuff(
                "PlanarFocusGoodAnimalBuff", "16500163-506d-4043-9ed9-05fd0ec902dd", "PlanarFocusGood.Name", "PlanarFocusGood.Description", AbilityRefs.SacredNimbus.Reference.Get().Icon);


            BlueprintFeature GoodEffect = CreatePlanarFocusEffect(
                "PlanarFocusGoodEffect", "ab623544-9f58-4065-a969-7b080b9745fe", "PlanarFocusGood.Name", GoodAnimalBuff);

            FeatureConfigurator.For(GoodEffect)
                .AddACBonusAgainstFactOwner(AlignmentComponent.Evil, 2, FeatureRefs.OutsiderType.Reference.Get(), descriptor: ModifierDescriptor.Sacred)
                .AddSavingThrowBonusAgainstFact(AlignmentComponent.Evil, FeatureRefs.OutsiderType.Reference.Get(), descriptor: ModifierDescriptor.Sacred, 2)
                .Configure();

            BlueprintBuff GoodAnimalBuffEffect = CreatePlanarFocusBuffEffect(
                "PlanarFocusGoodAnimalBuffEffect", "0b264ada-ab69-4e83-b146-51a2cb8d5365", "PlanarFocusGood.Name", GoodEffect);

            BlueprintBuff GoodBuff = CreatePlanarFocusBuff(
                "PlanarFocusGoodBuff", "dd28cb24-248f-4db5-b633-8d4d5e9fcd97", "PlanarFocusGood.Name", AbilityRefs.SacredNimbus.Reference.Get().Icon, GoodEffect, GoodAnimalBuffEffect);

            BlueprintActivatableAbility PF_Good = CreatePlanarFocusAbility("PlanarFocusGood",
                                                                          "dc063444-4fdf-45e2-95dd-6c656be7eea0",
                                                                          "PlanarFocusGood.Name",
                                                                          "PlanarFocusGood.Description",
                                                                          AbilityRefs.SacredNimbus.Reference.Get().Icon,
                                                                          GoodBuff);


            /************************* Add Immunity Polymorph *************************/

            BlueprintBuff LawfulAnimalBuff = CreatePlanarFocusAnimalBuff(
                "PlanarFocusLawfulAnimalBuff", "984434d3-991b-4f80-a522-3ffc949f2906", "PlanarFocusLawful.Name", "PlanarFocusLawful.Description", AbilityRefs.ProtectionFromLaw.Reference.Get().Icon);


            BlueprintFeature LawfulEffect = CreatePlanarFocusEffect(
                "PlanarFocusLawfulEffect", "0963ac0b-d3a5-4609-9755-1c85208309a9", "PlanarFocusLawful.Name", LawfulAnimalBuff);

            FeatureConfigurator.For(LawfulEffect)
                .AddSpellImmunityToSpellDescriptor(descriptor: SpellDescriptor.Polymorph)
                .Configure();

            BlueprintBuff LawfulAnimalBuffEffect = CreatePlanarFocusBuffEffect(
                "PlanarFocusLawfulAnimalBuffEffect", "92b8a842-5176-4d3d-9fb9-7a3a9100d80d", "PlanarFocusLawful.Name", LawfulEffect);

            BlueprintBuff LawfulBuff = CreatePlanarFocusBuff(
                "PlanarFocusLawfulBuff", "d8cef07b-85f8-416b-80e9-da08e98f14f3", "PlanarFocusLawful.Name", AbilityRefs.ProtectionFromLaw.Reference.Get().Icon, LawfulEffect, LawfulAnimalBuffEffect);


            BlueprintActivatableAbility PF_Lawful = CreatePlanarFocusAbility("PlanarFocusLawful",
                                                                          "9db5c2d0-1910-4da6-ac3c-a6c57d64fd7c",
                                                                          "PlanarFocusLawful.Name",
                                                                          "PlanarFocusLawful.Description",
                                                                          AbilityRefs.ProtectionFromLaw.Reference.Get().Icon,
                                                                          LawfulBuff);

            /************************* Add skill bonuses *************************/

            BlueprintBuff ShadowAnimalBuff = CreatePlanarFocusAnimalBuff(
                "PlanarFocusShadowAnimalBuff", "275b8ffc-475a-41a7-aeab-f41379cb2c73", "PlanarFocusShadow.Name", "PlanarFocusShadow.Description", AbilityRefs.MirrorImage.Reference.Get().Icon);


            BlueprintFeature ShadowEffect = CreatePlanarFocusEffect(
                "PlanarFocusShadowEffect", "b1269d4f-1ae6-4d8e-8257-03fa1fc86fdb", "PlanarFocusShadow.Name", ShadowAnimalBuff);

            FeatureConfigurator.For(ShadowEffect)
                .AddStatBonus(descriptor: ModifierDescriptor.Other, stat: StatType.SkillStealth, value: 5)
                .AddStatBonus(descriptor: ModifierDescriptor.Other, stat: StatType.SkillThievery, value: 5)
                .Configure();

            BlueprintBuff ShadowAnimalBuffEffect = CreatePlanarFocusBuffEffect(
                "PlanarFocusShadowAnimalBuffEffect", "0c40850d-e56d-4cbe-98af-536287e3d742", "PlanarFocusShadow.Name", ShadowEffect);

            BlueprintBuff ShadowBuff = CreatePlanarFocusBuff(
                "PlanarFocusShadowBuff", "caa05043-9cea-48e3-94ea-01e223abfdfb", "PlanarFocusShadow.Name", AbilityRefs.MirrorImage.Reference.Get().Icon, ShadowEffect, ShadowAnimalBuffEffect);

            BlueprintActivatableAbility PF_Shadow = CreatePlanarFocusAbility("PlanarFocusShadow",
                                                                          "4389de8f-407c-43fe-88fe-3acbd3de4d79",
                                                                          "PlanarFocusShadow.Name",
                                                                          "PlanarFocusShadow.Description",
                                                                          AbilityRefs.MirrorImage.Reference.Get().Icon,
                                                                          ShadowBuff);
            /************************* Add cmb immunity *************************/

            BlueprintBuff WaterAnimalBuff = CreatePlanarFocusAnimalBuff(
                "PlanarFocusWaterAnimalBuff", "736fd667-b68a-4e65-bee2-78cd55f3c27f", "PlanarFocusWater.Name", "PlanarFocusWater.Description", AbilityRefs.Vanish.Reference.Get().Icon);


            BlueprintFeature WaterEffect = CreatePlanarFocusEffect(
                "PlanarFocusWaterEffect", "4ca8f08f-124d-4cd6-a04c-8164192f0d87", "PlanarFocusWater.Name", WaterAnimalBuff);

            FeatureConfigurator.For(WaterEffect)
                .AddConditionImmunity(UnitCondition.ImmuneToCombatManeuvers)
                .Configure();

            BlueprintBuff WaterAnimalBuffEffect = CreatePlanarFocusBuffEffect(
                "PlanarFocusWaterAnimalBuffEffect", "6c912f1a-16e6-4870-b0d8-a3cb881bbfaa", "PlanarFocusWater.Name", WaterEffect);

            BlueprintBuff WaterBuff = CreatePlanarFocusBuff(
                "PlanarFocusWaterBuff", "b47bab02-098a-4b50-a244-dd4e62711658", "PlanarFocusWater.Name", AbilityRefs.Vanish.Reference.Get().Icon, WaterEffect, WaterAnimalBuffEffect);



            BlueprintActivatableAbility PF_Water = CreatePlanarFocusAbility("PlanarFocusWater",
                                                                          "b1b7951d-6c39-4b4b-9848-907436cacb1f",
                                                                          "PlanarFocusWater.Name",
                                                                          "PlanarFocusWater.Description",
                                                                          AbilityRefs.Vanish.Reference.Get().Icon,
                                                                          WaterBuff);

            //Air, Chaotic, Cold, Earth, Evil, Fire, Good, Lawful, Shadow, Water.
            FeatureConfigurator.New(FeatName, FeatGuid, FeatureGroup.Feat)
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

