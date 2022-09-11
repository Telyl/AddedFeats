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
using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using AddedFeats.Utils;

namespace AddedFeats.Feats.AnimalFocus
{
    /// <summary>
    /// Creates a feat that does nothing but show up.
    /// </summary>
    public class FavoredAnimalFocusMonkey
    {
        private static readonly string FeatName = "FavoredAnimalFocusMonkey";
        private static readonly string FeatGuid = "fa0ba92e-6304-494f-be38-8e4a4a840e16";
        private static readonly string PetFeatGuid = "b462262d-73ee-4f85-a321-6c4a63de44cf";

        private static readonly string DisplayName = "FavoredMonkey.Name";
        private static readonly string Description = "FavoredMonkey.Description";

        private static readonly LogWrapper FeatLogger = LogWrapper.Get("FavoredAnimalFocus");

        
        public static BlueprintFeature Configure()
        {
            //Create a pet feature to use as the conditional information for the new buff.
            BlueprintFeature FavoredMonkeyFocusPetFeat = BasicFunctions.CreateBasicFeat(FeatName + "Pet", PetFeatGuid, "FavoredMonkey.Name", "FavoredMonkey.Description");

            //The magical new ability with it's new context and everything
            BlueprintBuff FavoredAnimalMonkeyBuff = BuffConfigurator.New("FavoredMonkey", "f5273925-3e0f-4b91-9bdc-26d39a674825")
                .SetDisplayName("FavoredMonkey.Name")
                .SetDescription("FavoredMonkey.Description")
                .SetIcon(AbilityRefs.AnimalAspectGorilla.Reference.Get().Icon)
                .AddContextStatBonus(StatType.SkillAthletics, 
                value: new ContextValue()
                {
                    ValueType = ContextValueType.Rank
                }, ModifierDescriptor.Enhancement)
                .AddContextRankConfig(
                    ContextRankConfigs.CharacterLevel()
                    .WithCustomProgression((7, 6), (15, 8), (16, 12)))
                .Configure();

            //Patch the existing AnimalFocusEffect to get it into our game.
            FeatureConfigurator.For(FeatureRefs.AnimalFocusMonkeyEffect)
            .AddFactContextActions(
                activated:
                    ActionsBuilder.New()
                        .Conditional(
                            ConditionsBuilder.New().HasFact(FavoredMonkeyFocusPetFeat).IsAnimalCompanion(),
                            ifTrue: ActionsBuilder.New().ApplyBuffPermanent(FavoredAnimalMonkeyBuff, isNotDispelable: true, asChild: true)),
                deactivated:
                    ActionsBuilder.New().RemoveBuff(FavoredAnimalMonkeyBuff, removeRank: false, toCaster: false))
            .Configure();

            //The feature we (the player) can select that adds our pet feature.
            return FeatureConfigurator.New(FeatName, FeatGuid)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetReapplyOnLevelUp(true)
                .SetHideInUI(true)
                .SetHideInCharacterSheetAndLevelUp(false)
                .SetHideNotAvailibleInUI(false)
                .AddFeatureToPet(FavoredMonkeyFocusPetFeat)
                .AddPrerequisiteFeature(FeatureRefs.HunterAnimalFocusFeature.Reference.Get())
                .Configure();
        }
    }
}

