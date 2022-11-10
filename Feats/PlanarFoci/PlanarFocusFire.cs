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
    public class PlanarFocusFire
    {
        private static readonly string FeatName = "PlanarFocusFire";
        private static readonly string DisplayName = "PlanarFocusFire.Name";
        private static readonly string Description = "PlanarFocusFire.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(FeatName);
        public static void ConfigureDisabled()
        {
            BuffConfigurator.New(FeatName + "AnimalBuff", Guids.PlanarFocusFireAnimalBuff).Configure();
            FeatureConfigurator.New(FeatName + "Effect", Guids.PlanarFocusFireEffect).Configure();
            BuffConfigurator.New(FeatName + "AnimalBuffEffect", Guids.PlanarFocusFireAnimalBuffEffect).Configure();
            BuffConfigurator.New(FeatName + "Buff", Guids.PlanarFocusFireBuff).Configure();
            ActivatableAbilityConfigurator.New(FeatName, Guids.PlanarFocusFire).Configure();
        }
        public static (BlueprintActivatableAbility, BlueprintBuff, BlueprintBuff) Configure()
        {
            /************************* Add Fire 1d6 *************************/

            BlueprintBuff FireAnimalBuff = PlanarFunctions.CreatePlanarFocusAnimalBuff(
                FeatName+"AnimalBuff", Guids.PlanarFocusFireAnimalBuff, DisplayName, Description, AbilityRefs.ResistFire.Reference.Get().Icon);


            BlueprintFeature FireEffect = PlanarFunctions.CreatePlanarFocusEffect(
                FeatName+"Effect", Guids.PlanarFocusFireEffect , DisplayName, FireAnimalBuff);
       
            FeatureConfigurator.For(FireEffect)
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
                .Configure();

            BlueprintBuff FireAnimalBuffEffect = PlanarFunctions.CreatePlanarFocusBuffEffect(
                FeatName+"AnimalBuffEffect", Guids.PlanarFocusFireAnimalBuffEffect, DisplayName, FireEffect);

            BlueprintBuff FireBuff = PlanarFunctions.CreatePlanarFocusBuff(
                FeatName+"Buff", Guids.PlanarFocusFireBuff, DisplayName, AbilityRefs.ResistFire.Reference.Get().Icon, FireEffect, FireAnimalBuffEffect);

            BlueprintActivatableAbility PF_Fire = PlanarFunctions.CreatePlanarFocusAbility(
                FeatName, Guids.PlanarFocusFire, DisplayName, Description, AbilityRefs.ResistFire.Reference.Get().Icon, FireBuff);

            return (PF_Fire, FireBuff, FireAnimalBuffEffect);
        }
    }
}

