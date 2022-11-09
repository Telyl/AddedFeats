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
using AddedFeats.Feats;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;
using Kingmaker.UnitLogic.Abilities;

namespace AddedFeats.NewSpells
{
    /// <summary>
    /// Creates a feat that does nothing but show up.
    /// </summary>
    public class StrongJaw
    {
        private static readonly string SpellName = "StrongJaw";
        private static readonly string DisplayName = "StrongJaw.Name";
        private static readonly string Description = "StrongJaw.Description";
        
        public static void Configure()
        {

            var StrongJawBuff = BuffConfigurator.New(SpellName + "Buff", Guids.StrongJawSpellBuff) 
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(BuffRefs.WitchHexBeastsGiftBiteBuff.Reference.Get().Icon)
                .SetFrequency(DurationRate.Rounds)
                .AddWeaponSizeChange(sizeCategoryChange: 2)
                .Configure();

            AbilityConfigurator.NewSpell(SpellName, Guids.StrongJawSpell, SpellSchool.Transmutation, canSpecialize: true, SpellDescriptor.None)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.WitchHexBeastsGiftClawAbility.Reference.Get().Icon)
                .SetRange(AbilityRange.Close)
                .SetSpellResistance()
                .SetActionType(CommandType.Standard)
                .AllowTargeting(friends: true)
                .SetAvailableMetamagic(
                    Metamagic.CompletelyNormal,
                    Metamagic.Quicken,
                    Metamagic.Reach,
                    Metamagic.Extend,
                    Metamagic.Heighten)
                .AddToSpellLists(level: 3, SpellList.Ranger, SpellList.Hunter)
                .AddToSpellLists(level: 4, SpellList.Druid)
                .AddContextRankConfig(ContextRankConfigs.CasterLevel())
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .ApplyBuff(StrongJawBuff, ContextDuration.Variable(ContextValues.Rank(), rate: DurationRate.Minutes)))
                .AddAbilityTargetHasFact(checkedFacts: new() { FeatureRefs.AnimalType.Reference.Get() } )
                .Configure();
        }
    }
}

