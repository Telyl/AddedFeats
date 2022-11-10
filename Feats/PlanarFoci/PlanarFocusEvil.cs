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
    public class PlanarFocusEvil
    {
        private static readonly string FeatName = "PlanarFocusEvil";
        private static readonly string DisplayName = "PlanarFocusEvil.Name";
        private static readonly string Description = "PlanarFocusEvil.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(FeatName);
        public static void ConfigureDisabled()
        {
            BuffConfigurator.New(FeatName + "AnimalBuff", Guids.PlanarFocusEvilAnimalBuff).Configure();
            FeatureConfigurator.New(FeatName + "Effect", Guids.PlanarFocusEvilEffect).Configure();
            BuffConfigurator.New(FeatName + "AnimalBuffEffect", Guids.PlanarFocusEvilAnimalBuffEffect).Configure();
            BuffConfigurator.New(FeatName + "Buff", Guids.PlanarFocusEvilBuff).Configure();
            ActivatableAbilityConfigurator.New(FeatName, Guids.PlanarFocusEvil).Configure();
        }
        public static (BlueprintActivatableAbility, BlueprintBuff, BlueprintBuff) Configure()
        {
            /************************* +1 profane bonus to AC. This bonus increases to +2. *************************/

            BlueprintBuff EvilAnimalBuff = PlanarFunctions.CreatePlanarFocusAnimalBuff(
                FeatName+"AnimalBuff", Guids.PlanarFocusEvilAnimalBuff, DisplayName, Description, AbilityRefs.ProfaneNimbus.Reference.Get().Icon);


            BlueprintFeature EvilEffect = PlanarFunctions.CreatePlanarFocusEffect(
                FeatName+"Effect", Guids.PlanarFocusEvilEffect, DisplayName, EvilAnimalBuff);

            FeatureConfigurator.For(EvilEffect)
                .AddACBonusAgainstFactOwner(AlignmentComponent.Good, 2, FeatureRefs.OutsiderType.Reference.Get(), descriptor: ModifierDescriptor.Profane)
                .AddSavingThrowBonusAgainstFact(AlignmentComponent.Good, FeatureRefs.OutsiderType.Reference.Get(), descriptor: ModifierDescriptor.Profane, 2)
                .Configure();

            BlueprintBuff EvilAnimalBuffEffect = PlanarFunctions.CreatePlanarFocusBuffEffect(
                FeatName+"AnimalBuffEffect", Guids.PlanarFocusEvilAnimalBuffEffect, DisplayName, EvilEffect);

            BlueprintBuff EvilBuff = PlanarFunctions.CreatePlanarFocusBuff(
                FeatName+"Buff", Guids.PlanarFocusEvilBuff, DisplayName, AbilityRefs.ProfaneNimbus.Reference.Get().Icon, EvilEffect, EvilAnimalBuffEffect);

            BlueprintActivatableAbility PF_Evil = PlanarFunctions.CreatePlanarFocusAbility(
                FeatName, Guids.PlanarFocusEvil, DisplayName, Description, AbilityRefs.ProfaneNimbus.Reference.Get().Icon, EvilBuff);

            return (PF_Evil, EvilBuff, EvilAnimalBuffEffect);

        }
    }
}

