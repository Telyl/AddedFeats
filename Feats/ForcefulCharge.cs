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

namespace AddedFeats.Feats
{
    /// <summary>
    /// Creates a feat that does nothing but show up.
    /// </summary>
    public class ForcefulCharge
    {
        private static readonly string FeatName = "ForcefulChargeFeat";
        private static readonly string FeatGuid = "b7874cf6-0a94-4685-a295-f2467b6c5428";
        private static readonly string AbilityName = "ForcefulChargeAbility";
        private static readonly string AbilityGuid = "d8ec6b3f-c687-493a-b5d9-2b31715b5fd2";
        private static readonly string BuffName = "ForcefulChargeBuff";
        private static readonly string BuffGuid = "508b7605-623c-468e-af65-7bac9cc36af7";

        private static readonly string EffectName = "ForcefulChargeEffect";
        private static readonly string EffectGuid = "508b7605-623c-468e-bbbb-7b32ccc36af7";

        private static readonly string ImprovedEffectName = "ImprovedForcefulChargeEffect";
        private static readonly string ImprovedEffectGuid = "5ccb7545-623c-468e-bbbb-7b32ccc36af7";

        private static readonly string DisplayName = "ForcefulCharge.Name";
        private static readonly string Description = "ForcefulCharge.Description";

        private static readonly LogWrapper FeatLogger = LogWrapper.Get("FavoredAnimalFocus");

        
        public static void Configure()
        {

            BlueprintBuff ForcefulChargeEffect = BuffConfigurator.New(EffectName, EffectGuid)
                .AddComponent<AddForcefulCharge>()
                .Configure();

            BlueprintBuff ImprovedForcefulChargeEffect = BuffConfigurator.New(ImprovedEffectName, ImprovedEffectGuid)
                .AddComponent<AddImprovedForcefulCharge>()
                .Configure();

            BlueprintBuff ForcefulChargeBuff = BuffConfigurator.New(BuffName, BuffGuid)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .Configure();

            BlueprintActivatableAbility ForcefulChargeToggleAbility = ActivatableAbilityConfigurator.New(AbilityName, AbilityGuid)
                .SetDisplayName(DisplayName)
                .SetDescription("ForcefulChargeAbility.Description")
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
            BlueprintFeature ForcefulChargeFeat = BasicFunctions.CreateBasicFeat(FeatName, FeatGuid, DisplayName, Description);

            FeatureConfigurator.For(ForcefulChargeFeat)
                .SetGroups(FeatureGroup.Feat)
                .SetIcon(AbilityRefs.ChargeAbility.Reference.Get().Icon)
                .AddFacts(new() { ForcefulChargeToggleAbility } )
                .AddPrerequisiteFeature(FeatureRefs.ImprovedBullRush.Reference.Get())
                .AddPrerequisiteFeature(FeatureRefs.PowerAttackFeature.Reference.Get())
                .AddPrerequisiteClassLevel(CharacterClassRefs.AnimalCompanionClass.Reference.Get(), 1)
                .Configure();

            BlueprintFeature ImprovedForcefulChargeFeat = BasicFunctions.CreateBasicFeat("ImprovedForcefulChargeFeat", "c6374cf6-0b94-4386-a281-f1167b6c5428" , "ImprovedForcefulCharge.Name", "ImprovedForcefulCharge.Description");
            FeatureConfigurator.For(ImprovedForcefulChargeFeat)
                .SetGroups(FeatureGroup.Feat)
                .SetIcon(AbilityRefs.ChargeAbility.Reference.Get().Icon)
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

