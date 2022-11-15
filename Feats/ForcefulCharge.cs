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
using Kingmaker.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using AddedFeats.NewComponents;
using AddedFeats.Feats;
using static UnityModManagerNet.UnityModManager.ModEntry;
using System;

namespace AddedFeats.Feats
{
    /// <summary>
    /// Creates a feat that does nothing but show up.
    /// </summary>
    public class ForcefulCharge
    {
        private static readonly string FeatName = "ForcefulCharge";
        private static readonly string ImprovedFeatName = "ImprovedForcefulCharge";

        internal const string DisplayName = "ForcefulCharge.Name";
        private static readonly string Description = "ForcefulCharge.Description";
        internal const string ImprovedDisplayName = "ImprovedForcefulCharge.Name";
        private static readonly string ImprovedDescription = "ImprovedForcefulCharge.Description";

        private static readonly ModLogger Logger = Logging.GetLogger(FeatName);

        internal static void Configure()
        {
            try
            {
                if (Settings.IsEnabled(Guids.PlanarFocus))
                    ConfigureEnabled();
                else
                    ConfigureDisabled();
            }
            catch (Exception e)
            {
                Logger.LogException("ForcefulCharge.Configure", e);
            }
        }

        private static void ConfigureDisabled()
        {
            FeatureConfigurator.New(FeatName, Guids.ForcefulCharge).Configure();
            FeatureConfigurator.New(ImprovedFeatName, Guids.ImprovedForcefulCharge).Configure();
            BuffConfigurator.New(FeatName + "Effect", Guids.ForcefulChargeEffect).Configure();
            BuffConfigurator.New(FeatName + "Buff", Guids.ForcefulChargeBuff).Configure();
            BuffConfigurator.New(ImprovedFeatName + "Effect", Guids.ImprovedForcefulChargeEffect).Configure();
            ActivatableAbilityConfigurator.New(FeatName + "Ability", Guids.ForcefulChargeAbility).Configure();
        }

        public static void ConfigureEnabled()
        {

            BlueprintBuff ForcefulChargeEffect = BuffConfigurator.New(FeatName + "Effect", Guids.ForcefulChargeEffect)
                .AddComponent<AddForcefulCharge>()
                .Configure();

            BlueprintBuff ImprovedForcefulChargeEffect = BuffConfigurator.New(ImprovedFeatName + "Effect", Guids.ImprovedForcefulChargeEffect)
                .AddComponent<AddImprovedForcefulCharge>()
                .Configure();

            BlueprintBuff ForcefulChargeBuff = BuffConfigurator.New(FeatName + "Buff", Guids.ForcefulChargeBuff)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .Configure();

            BlueprintActivatableAbility ForcefulChargeToggleAbility = ActivatableAbilityConfigurator.New(FeatName + "Ability", Guids.ForcefulChargeAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetBuff(ForcefulChargeBuff)
                .SetIcon(AbilityRefs.BullRushAction.Reference.Get().Icon)
                .AddHideFeatureInInspect()
                .SetGroup(ActivatableAbilityGroup.CombatStyle)
                .SetActivationType(AbilityActivationType.Immediately)
                .SetIsOnByDefault(true)
                .SetOnlyInCombat(false)
                .SetDeactivateImmediately(false)
                .SetDoNotTurnOffOnRest(false)
                .SetDeactivateIfOwnerUnconscious(false)
                .SetDeactivateIfOwnerDisabled(false)
                .SetIsTargeted(false)
                .SetActivateWithUnitCommand(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Swift)
                .SetActivateOnUnitAction(AbilityActivateOnUnitActionType.Attack)
                .Configure();

            //Create a feature for forceful charge
            BlueprintFeature ForcefulChargeFeat = BasicFunctions.CreateBasicFeat(FeatName, Guids.ForcefulCharge, DisplayName, Description);

            FeatureConfigurator.For(ForcefulChargeFeat)
                .SetGroups(FeatureGroup.Feat)
                .SetIcon(AbilityRefs.ChargeAbility.Reference.Get().Icon)
                .AddFacts(new() { ForcefulChargeToggleAbility } )
                .AddPrerequisiteFeature(FeatureRefs.ImprovedBullRush.Reference.Get())
                .AddPrerequisiteFeature(FeatureRefs.PowerAttackFeature.Reference.Get())
                .AddPrerequisiteClassLevel(CharacterClassRefs.AnimalCompanionClass.Reference.Get(), 1)
                .Configure();

            BlueprintFeature ImprovedForcefulChargeFeat = BasicFunctions.CreateBasicFeat(ImprovedFeatName, Guids.ImprovedForcefulCharge,
                ImprovedDisplayName, ImprovedDescription);

            FeatureConfigurator.For(ImprovedForcefulChargeFeat)
                .SetGroups(FeatureGroup.Feat)
                .SetIcon(AbilityRefs.ChargeAbility.Reference.Get().Icon)
                .AddRecommendationHasFeature(FeatureRefs.AnimalCompanionArchetypeBullyFeature.Reference.Get())
                .AddPrerequisiteFeature(FeatureRefs.ImprovedBullRush.Reference.Get())
                .AddPrerequisiteFeature(FeatureRefs.PowerAttackFeature.Reference.Get())
                .AddPrerequisiteFeature(ForcefulChargeFeat)
                .AddPrerequisiteClassLevel(CharacterClassRefs.AnimalCompanionClass.Reference.Get(), 1)
                .Configure();


            BuffConfigurator.For(ForcefulChargeBuff)
                .AddFactContextActions(
                    activated:
                        ActionsBuilder.New()
                        .Conditional(
                            ConditionsBuilder.New().HasFact(ImprovedForcefulChargeFeat),
                            ifTrue: ActionsBuilder.New().ApplyBuffPermanent(ImprovedForcefulChargeEffect),
                            ifFalse: ActionsBuilder.New().ApplyBuffPermanent(ForcefulChargeEffect)))
                .Configure();


        }
    }
}

