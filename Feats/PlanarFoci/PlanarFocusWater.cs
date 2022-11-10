using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using AddedFeats.Utils;
using static UnityModManagerNet.UnityModManager.ModEntry;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;

namespace AddedFeats.Feats.PlanarFoci
{
    /// <summary>
    /// Creates a feat that does nothing but show up.
    /// </summary>
    public class PlanarFocusWater
    {
        private static readonly string FeatName = "PlanarFocusWater";
        private static readonly string DisplayName = "PlanarFocusWater.Name";
        private static readonly string Description = "PlanarFocusWater.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(FeatName);
        public static void ConfigureDisabled()
        {
            BuffConfigurator.New(FeatName + "AnimalBuff", Guids.PlanarFocusWaterAnimalBuff).Configure();
            FeatureConfigurator.New(FeatName + "Effect", Guids.PlanarFocusWaterEffect).Configure();
            BuffConfigurator.New(FeatName + "AnimalBuffEffect", Guids.PlanarFocusWaterAnimalBuffEffect).Configure();
            BuffConfigurator.New(FeatName + "Buff", Guids.PlanarFocusWaterBuff).Configure();
            ActivatableAbilityConfigurator.New(FeatName, Guids.PlanarFocusWater).Configure();
        }
        public static (BlueprintActivatableAbility, BlueprintBuff, BlueprintBuff) Configure()
        {
            /************************* Add cmb immunity *************************/

            BlueprintBuff WaterAnimalBuff = PlanarFunctions.CreatePlanarFocusAnimalBuff(
                FeatName+"AnimalBuff", Guids.PlanarFocusWaterAnimalBuff, DisplayName, Description, AbilityRefs.Vanish.Reference.Get().Icon);


            BlueprintFeature WaterEffect = PlanarFunctions.CreatePlanarFocusEffect(
                FeatName+"Effect", Guids.PlanarFocusWaterEffect, DisplayName, WaterAnimalBuff);

            FeatureConfigurator.For(WaterEffect)
                .AddConditionImmunity(UnitCondition.ImmuneToCombatManeuvers)
                .Configure();

            BlueprintBuff WaterAnimalBuffEffect = PlanarFunctions.CreatePlanarFocusBuffEffect(
                FeatName+"AnimalBuffEffect", Guids.PlanarFocusWaterAnimalBuffEffect, DisplayName, WaterEffect);

            BlueprintBuff WaterBuff = PlanarFunctions.CreatePlanarFocusBuff(
                FeatName+"Buff", Guids.PlanarFocusWaterBuff, DisplayName, AbilityRefs.Vanish.Reference.Get().Icon, WaterEffect, WaterAnimalBuffEffect);

            BlueprintActivatableAbility PF_Water = PlanarFunctions.CreatePlanarFocusAbility(
                FeatName, Guids.PlanarFocusWater, DisplayName, Description, AbilityRefs.Vanish.Reference.Get().Icon, WaterBuff);

            return (PF_Water, WaterBuff, WaterAnimalBuffEffect);
        }
    }
}

