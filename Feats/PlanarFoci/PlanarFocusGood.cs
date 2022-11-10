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
    public class PlanarFocusGood
    {
        private static readonly string FeatName = "PlanarFocusGood";
        private static readonly string DisplayName = "PlanarFocusGood.Name";
        private static readonly string Description = "PlanarFocusGood.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(FeatName);
        public static void ConfigureDisabled()
        {
            BuffConfigurator.New(FeatName + "AnimalBuff", Guids.PlanarFocusGoodAnimalBuff).Configure();
            FeatureConfigurator.New(FeatName + "Effect", Guids.PlanarFocusGoodEffect).Configure();
            BuffConfigurator.New(FeatName + "AnimalBuffEffect", Guids.PlanarFocusGoodAnimalBuffEffect).Configure();
            BuffConfigurator.New(FeatName + "Buff", Guids.PlanarFocusGoodBuff).Configure();
            ActivatableAbilityConfigurator.New(FeatName, Guids.PlanarFocusGood).Configure();
        }
        public static (BlueprintActivatableAbility, BlueprintBuff, BlueprintBuff) Configure()
        {
            /************************* Add Sacred Bonus *************************/

            BlueprintBuff GoodAnimalBuff = PlanarFunctions.CreatePlanarFocusAnimalBuff(
                FeatName+"AnimalBuff", Guids.PlanarFocusGoodAnimalBuff, DisplayName, Description, AbilityRefs.SacredNimbus.Reference.Get().Icon);


            BlueprintFeature GoodEffect = PlanarFunctions.CreatePlanarFocusEffect(
                FeatName+"Effect", Guids.PlanarFocusGoodEffect, DisplayName, GoodAnimalBuff);

            FeatureConfigurator.For(GoodEffect)
                .AddACBonusAgainstFactOwner(AlignmentComponent.Evil, 2, FeatureRefs.OutsiderType.Reference.Get(), descriptor: ModifierDescriptor.Sacred)
                .AddSavingThrowBonusAgainstFact(AlignmentComponent.Evil, FeatureRefs.OutsiderType.Reference.Get(), descriptor: ModifierDescriptor.Sacred, 2)
                .Configure();

            BlueprintBuff GoodAnimalBuffEffect = PlanarFunctions.CreatePlanarFocusBuffEffect(
                FeatName+"AnimalBuffEffect", Guids.PlanarFocusGoodAnimalBuffEffect, DisplayName, GoodEffect);

            BlueprintBuff GoodBuff = PlanarFunctions.CreatePlanarFocusBuff(
                FeatName+"Buff", Guids.PlanarFocusGoodBuff, DisplayName, AbilityRefs.SacredNimbus.Reference.Get().Icon, GoodEffect, GoodAnimalBuffEffect);

            BlueprintActivatableAbility PF_Good = PlanarFunctions.CreatePlanarFocusAbility(
                FeatName, Guids.PlanarFocusGood, DisplayName, Description, AbilityRefs.SacredNimbus.Reference.Get().Icon, GoodBuff);

            return (PF_Good, GoodBuff, GoodAnimalBuffEffect);
        }
    }
}

