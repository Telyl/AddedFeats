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

namespace AddedFeats.Feats.PlanarFoci
{
    /// <summary>
    /// Creates a feat that does nothing but show up.
    /// </summary>
    public class PlanarFocusChaotic
    {
        private static readonly string FeatName = "PlanarFocusChaotic";
        private static readonly string DisplayName = "PlanarFocusChaos.Name";
        private static readonly string Description = "PlanarFocusChaos.Description";
        
        public static (BlueprintActivatableAbility, BlueprintBuff, BlueprintBuff) Configure()
        { 
            /************************* Adds Fortification 25 *************************/
            BlueprintBuff ChaoticAnimalBuff = PlanarFunctions.CreatePlanarFocusAnimalBuff(
                FeatName+"AnimalBuff", Guids.PlanarFocusChaoticAnimalBuff, DisplayName, Description, AbilityRefs.ProtectionFromChaos.Reference.Get().Icon);

            BlueprintFeature ChaoticEffect = PlanarFunctions.CreatePlanarFocusEffect(
                FeatName+"Effect", Guids.PlanarFocusChaoticEffect, DisplayName, ChaoticAnimalBuff);
            FeatureConfigurator.For(ChaoticEffect)
                .AddFortification(25)
                .Configure();

            BlueprintBuff ChaoticAnimalBuffEffect = PlanarFunctions.CreatePlanarFocusBuffEffect(
                FeatName+"AnimalBuffEffect", Guids.PlanarFocusChaoticAnimalBuffEffect, DisplayName, ChaoticEffect);

            BlueprintBuff ChaoticBuff = PlanarFunctions.CreatePlanarFocusBuff(
                FeatName+"Buff", Guids.PlanarFocusChaoticBuff, DisplayName, AbilityRefs.WindsOfVengeance.Reference.Get().Icon, ChaoticEffect, ChaoticAnimalBuffEffect);

            BlueprintActivatableAbility PF_Chaotic = PlanarFunctions.CreatePlanarFocusAbility(
                FeatName, Guids.PlanarFocusChaotic, DisplayName, Description, AbilityRefs.ProtectionFromChaos.Reference.Get().Icon, ChaoticBuff);

            return (PF_Chaotic, ChaoticBuff, ChaoticAnimalBuffEffect);
        }
    }
}

