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
    /// Creates the Favored Animal Focus - Bat logic and feature.
    /// </summary>
    internal class AnimalFocusBat
    {
        private static readonly string FeatName = "FavoredAnimalFocusBat";
        private static readonly string DisplayName = "FavoredBat.Name";
        private static readonly string Description = "FavoredBat.Description";

        private static readonly string FavoredAnimalFocusCharacter = Guids.FavoredAnimalFocusBat;
        private static readonly string FavoredAnimalFocusPet = Guids.FavoredAnimalFocusBatPet;
        private static readonly string FavoredAnimalFocusBuff = Guids.FavoredAnimalFocusBatBuff;
        private static readonly StatType FavoredAnimalFocusStat = StatType.Constitution;
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
                .AddBlindsense(20.Feet(), false)
                .Configure();

            //Patch the existing AnimalFocusEffect to get it into our game.
            FeatureConfigurator.For(Guids.AnimalFocusBatFeatureEffect)
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

