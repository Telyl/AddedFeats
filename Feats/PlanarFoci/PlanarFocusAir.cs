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
    public class PlanarFocusAir
    {
        private static readonly string FeatName = "PlanarFocusAir";
        private static readonly string DisplayName = "PlanarFocusAir.Name";
        private static readonly string Description = "PlanarFocusAir.Description";
        
        private static readonly ModLogger Logger = Logging.GetLogger(FeatName);
        public static void ConfigureDisabled()
        {
            BuffConfigurator.New(FeatName + "AnimalBuff", Guids.PlanarFocusAirAnimalBuff).Configure();
            FeatureConfigurator.New(FeatName + "Effect", Guids.PlanarFocusAirEffect).Configure();
            BuffConfigurator.New(FeatName + "AnimalBuffEffect", Guids.PlanarFocusAirAnimalBuffEffect).Configure();
            BuffConfigurator.New(FeatName + "Buff", Guids.PlanarFocusAirBuff).Configure();
            ActivatableAbilityConfigurator.New(FeatName, Guids.PlanarFocusAir).Configure();
        }

        public static (BlueprintActivatableAbility, BlueprintBuff, BlueprintBuff) Configure()
        {
            /************************* Adds ground immunity *************************/
            BlueprintBuff AirAnimalBuff = PlanarFunctions.CreatePlanarFocusAnimalBuff(
                FeatName+"AnimalBuff", Guids.PlanarFocusAirAnimalBuff, DisplayName, Description, AbilityRefs.WindsOfVengeance.Reference.Get().Icon);

            BlueprintFeature AirEffect = PlanarFunctions.CreatePlanarFocusEffect(
                FeatName+"Effect", Guids.PlanarFocusAirEffect, DisplayName, AirAnimalBuff);

            FeatureConfigurator.For(AirEffect)
                .AddSpellImmunityToSpellDescriptor(null, descriptor: SpellDescriptor.Ground)
                .AddBuffDescriptorImmunity(descriptor: SpellDescriptor.Ground)
                .AddConditionImmunity(UnitCondition.DifficultTerrain)
                .Configure();

            BlueprintBuff AirAnimalBuffEffect = PlanarFunctions.CreatePlanarFocusBuffEffect(
                FeatName+"AnimalBuffEffect",Guids.PlanarFocusAirAnimalBuffEffect, DisplayName, AirEffect);

            BlueprintBuff AirBuff = PlanarFunctions.CreatePlanarFocusBuff(
                FeatName+"Buff", Guids.PlanarFocusAirBuff, DisplayName, AbilityRefs.WindsOfVengeance.Reference.Get().Icon, AirEffect, AirAnimalBuffEffect);

            BlueprintActivatableAbility PF_Air = PlanarFunctions.CreatePlanarFocusAbility(
                FeatName, Guids.PlanarFocusAir, DisplayName, Description, AbilityRefs.WindsOfVengeance.Reference.Get().Icon, AirBuff);

            return (PF_Air, AirBuff, AirAnimalBuffEffect);
        }
    }
}

