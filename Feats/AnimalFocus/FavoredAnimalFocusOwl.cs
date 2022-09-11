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
    public class FavoredAnimalFocusOwl
    {
        private static readonly string FeatName = "FavoredAnimalFocusOwl";
        private static readonly string FeatGuid = "4cfe9a7f-74d8-46d2-bc47-aafd383b30d6";
        private static readonly string PetFeatGuid = "b6096f3e-e95a-4700-b7fa-695a3f25f103";

        private static readonly string DisplayName = "FavoredOwl.Name";
        private static readonly string Description = "FavoredOwl.Description";

        private static readonly LogWrapper FeatLogger = LogWrapper.Get("FavoredAnimalFocus");

        
        public static BlueprintFeature Configure()
        {
            //Create a pet feature to use as the conditional information for the new buff.
            BlueprintFeature FavoredOwlFocusPetFeat = BasicFunctions.CreateBasicFeat(FeatName + "Pet", PetFeatGuid, "FavoredOwl.Name", "FavoredOwl.Description");

            //The magical new ability with it's new context and everything
            BlueprintBuff FavoredAnimalOwlBuff = BuffConfigurator.New("FavoredOwl", "725a9c22-a69f-41ee-9aed-cddd2b2af560")
                .SetDisplayName("FavoredOwl.Name")
                .SetDescription("FavoredOwl.Description")
                .SetIcon(AbilityRefs.OwlsWisdom.Reference.Get().Icon)
                .AddContextStatBonus(StatType.SkillStealth, 
                value: new ContextValue()
                {
                    ValueType = ContextValueType.Rank
                }, ModifierDescriptor.Enhancement)
                .AddContextRankConfig(
                    ContextRankConfigs.CharacterLevel()
                    .WithCustomProgression((7, 6), (15, 8), (16, 12)))
                .Configure();

            //Patch the existing AnimalFocusEffect to get it into our game.
            FeatureConfigurator.For(FeatureRefs.AnimalFocusOwlEffect)
            .AddFactContextActions(
                activated:
                    ActionsBuilder.New()
                        .Conditional(
                            ConditionsBuilder.New().HasFact(FavoredOwlFocusPetFeat).IsAnimalCompanion(),
                            ifTrue: ActionsBuilder.New().ApplyBuffPermanent(FavoredAnimalOwlBuff, isNotDispelable: true, asChild: true)),
                deactivated:
                    ActionsBuilder.New().RemoveBuff(FavoredAnimalOwlBuff, removeRank: false, toCaster: false))
            .Configure();

            //The feature we (the player) can select that adds our pet feature.
            return FeatureConfigurator.New(FeatName, FeatGuid)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetReapplyOnLevelUp(true)
                .SetHideInUI(true)
                .SetHideInCharacterSheetAndLevelUp(false)
                .SetHideNotAvailibleInUI(false)
                .AddFeatureToPet(FavoredOwlFocusPetFeat)
                .AddPrerequisiteFeature(FeatureRefs.HunterAnimalFocusFeature.Reference.Get())
                .Configure();
        }
    }
}

