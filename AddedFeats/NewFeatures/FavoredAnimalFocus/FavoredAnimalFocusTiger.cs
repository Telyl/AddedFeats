using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Enums;
using Kingmaker.UnitLogic;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Utility;
using BlueprintCore.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using AddedFeats.Utils;
using System;
using System.Runtime.CompilerServices;
using Kingmaker.Tutorial;
using Kingmaker.UI.ServiceWindow.CharacterScreen;

namespace AddedFeats.NewFeatures.FavoredAnimalFocus
{
    /// <summary>
    /// Creates the Favored Animal Focus - Bear logic and feature.
    /// </summary>
    internal class AnimalFocusTiger
    {
        private static readonly string FeatName = "FavoredAnimalFocusTiger";
        private static readonly string DisplayName = "FavoredTiger.Name";
        private static readonly string Description = "FavoredTiger.Description";

        private static readonly string FavoredAnimalFocusCharacter = Guids.FavoredAnimalFocusTiger;
        private static readonly string FavoredAnimalFocusPet = Guids.FavoredAnimalFocusTigerPet;
        private static readonly string FavoredAnimalFocusBuff = Guids.FavoredAnimalFocusTigerBuff;
        private static readonly StatType FavoredAnimalFocusStat = StatType.Dexterity;
        private static readonly ModifierDescriptor FavoredAnimalFocusDescriptor = ModifierDescriptor.Inherent;

        public static BlueprintFeature Configure()
        {
            //Create a pet feature to use as the conditional information for the new buff.
            FeatureConfigurator.New(FeatName + "Pet", FavoredAnimalFocusPet)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetHideInUI(true)
                .SetHideInCharacterSheetAndLevelUp(true)
                .SetHideNotAvailibleInUI(false)
                .Configure(); 

            //The magical new ability with it's new context and everything
            BlueprintBuff FavoredAnimalBuff = BuffConfigurator.New(FeatName + "Buff", FavoredAnimalFocusBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddContextStatBonus(FavoredAnimalFocusStat, 
                value: new ContextValue()
                {
                    ValueType = ContextValueType.Rank
                }, FavoredAnimalFocusDescriptor)
                .AddContextRankConfig(
                    ContextRankConfigs.CharacterLevel()
                    .WithCustomProgression((7, 4), (15, 6), (16, 8)))
                .Configure();

            //Patch the existing AnimalFocusEffect to get it into our game.
            FeatureConfigurator.For(FeatureRefs.AnimalFocusBearEffect)
            .AddFactContextActions(
                activated:
                    ActionsBuilder.New()
                        .Conditional(
                            ConditionsBuilder.New().HasFact(FavoredAnimalFocusPet).IsAnimalCompanion(),
                            ifTrue: ActionsBuilder.New().ApplyBuffPermanent(FavoredAnimalBuff, isNotDispelable: true, asChild: true)),
                deactivated:
                    ActionsBuilder.New().RemoveBuff(FavoredAnimalBuff, removeRank: false, toCaster: false))
            .Configure();

            //The feature we (the player) can select that adds our pet feature.
            return FeatureConfigurator.New(FeatName, FavoredAnimalFocusCharacter)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetReapplyOnLevelUp(true)
                .SetHideInUI(true)
                .SetHideInCharacterSheetAndLevelUp(false)
                .SetHideNotAvailibleInUI(false)
                .AddFeatureToPet(FavoredAnimalFocusPet)
                .AddPrerequisiteFeature(FeatureRefs.HunterAnimalFocusFeature.Reference.Get())
                .Configure();
        }
    }
}

