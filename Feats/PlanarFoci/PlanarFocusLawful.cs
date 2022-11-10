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
    public class PlanarFocusLawful
    {
        private static readonly string FeatName = "PlanarFocusLawful";
        private static readonly string DisplayName = "PlanarFocusLawful.Name";
        private static readonly string Description = "PlanarFocusLawful.Description";
        private static readonly ModLogger Logger = Logging.GetLogger(FeatName);
        public static void ConfigureDisabled()
        {
            BuffConfigurator.New(FeatName + "AnimalBuff", Guids.PlanarFocusLawfulAnimalBuff).Configure();
            FeatureConfigurator.New(FeatName + "Effect", Guids.PlanarFocusLawfulEffect).Configure();
            BuffConfigurator.New(FeatName + "AnimalBuffEffect", Guids.PlanarFocusLawfulAnimalBuffEffect).Configure();
            BuffConfigurator.New(FeatName + "Buff", Guids.PlanarFocusLawfulBuff).Configure();
            ActivatableAbilityConfigurator.New(FeatName, Guids.PlanarFocusLawful).Configure();
        }
        public static (BlueprintActivatableAbility, BlueprintBuff, BlueprintBuff) Configure()
        {
            /************************* Add Immunity Polymorph *************************/

            BlueprintBuff LawfulAnimalBuff = PlanarFunctions.CreatePlanarFocusAnimalBuff(
                FeatName+"AnimalBuff", Guids.PlanarFocusLawfulAnimalBuff, DisplayName, Description, AbilityRefs.ProtectionFromLaw.Reference.Get().Icon);


            BlueprintFeature LawfulEffect = PlanarFunctions.CreatePlanarFocusEffect(
                FeatName+"Effect", Guids.PlanarFocusLawfulEffect, DisplayName, LawfulAnimalBuff);

            FeatureConfigurator.For(LawfulEffect)
                .AddSpellImmunityToSpellDescriptor(descriptor: SpellDescriptor.Polymorph)
                .Configure();

            BlueprintBuff LawfulAnimalBuffEffect = PlanarFunctions.CreatePlanarFocusBuffEffect(
                FeatName+"AnimalBuffEffect", Guids.PlanarFocusLawfulAnimalBuffEffect, DisplayName, LawfulEffect);

            BlueprintBuff LawfulBuff = PlanarFunctions.CreatePlanarFocusBuff(
                FeatName+"Buff", Guids.PlanarFocusLawfulBuff, DisplayName, AbilityRefs.ProtectionFromLaw.Reference.Get().Icon, LawfulEffect, LawfulAnimalBuffEffect);


            BlueprintActivatableAbility PF_Lawful = PlanarFunctions.CreatePlanarFocusAbility(
                FeatName, Guids.PlanarFocusLawful, DisplayName, Description, AbilityRefs.ProtectionFromLaw.Reference.Get().Icon, LawfulBuff);

            return (PF_Lawful, LawfulBuff, LawfulAnimalBuffEffect);
        }
    }
}

