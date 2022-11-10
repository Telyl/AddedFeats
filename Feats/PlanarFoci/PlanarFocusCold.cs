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

namespace AddedFeats.Feats.PlanarFoci
{
    /// <summary>
    /// Creates a feat that does nothing but show up.
    /// </summary>
    public class PlanarFocusCold
    {
        private static readonly string FeatName = "PlanarFocusCold";
        private static readonly string DisplayName = "PlanarFocusCold.Name";
        private static readonly string Description = "PlanarFocusCold.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(FeatName);
        public static void ConfigureDisabled()
        {
            BuffConfigurator.New(FeatName + "AnimalBuff", Guids.PlanarFocusColdAnimalBuff).Configure();
            FeatureConfigurator.New(FeatName + "Effect", Guids.PlanarFocusColdEffect).Configure();
            BuffConfigurator.New(FeatName + "AnimalBuffEffect", Guids.PlanarFocusColdAnimalBuffEffect).Configure();
            BuffConfigurator.New(FeatName + "Buff", Guids.PlanarFocusColdBuff).Configure();
            ActivatableAbilityConfigurator.New(FeatName, Guids.PlanarFocusCold).Configure();
        }
        public static (BlueprintActivatableAbility, BlueprintBuff, BlueprintBuff) Configure()
        {
            /************************* Add thorns 1d4 *************************/
            BlueprintBuff ColdAnimalBuff = PlanarFunctions.CreatePlanarFocusAnimalBuff(
                FeatName+"AnimalBuff", Guids.PlanarFocusColdAnimalBuff, DisplayName, Description, AbilityRefs.ProtectionFromCold.Reference.Get().Icon);


            BlueprintFeature ColdEffect = PlanarFunctions.CreatePlanarFocusEffect(
                FeatName+"Effect", Guids.PlanarFocusColdEffect, DisplayName, ColdAnimalBuff);

            FeatureConfigurator.For(ColdEffect)
                .AddContextRankConfig(
                    ContextRankConfigs
                    .CharacterLevel()
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

            BlueprintBuff ColdAnimalBuffEffect = PlanarFunctions.CreatePlanarFocusBuffEffect(
                FeatName+"AnimalBuffEffect", Guids.PlanarFocusColdAnimalBuffEffect, DisplayName, ColdEffect);

            BlueprintBuff ColdBuff = PlanarFunctions.CreatePlanarFocusBuff(
                FeatName+"Buff", Guids.PlanarFocusColdBuff, DisplayName, AbilityRefs.ProtectionFromCold.Reference.Get().Icon, ColdEffect, ColdAnimalBuffEffect);

            BlueprintActivatableAbility PF_Cold = PlanarFunctions.CreatePlanarFocusAbility(
                FeatName, Guids.PlanarFocusCold, DisplayName, Description, AbilityRefs.ProtectionFromCold.Reference.Get().Icon, ColdBuff);

            return (PF_Cold, ColdBuff, ColdAnimalBuffEffect);
        }
    }
}

