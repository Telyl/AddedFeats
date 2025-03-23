using BlueprintCore.Blueprints.References;
using Kingmaker.UnitLogic.Mechanics;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes.Spells;
using AddedFeats.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.Enums;
using Kingmaker.EntitySystem.Stats;

namespace AddedFeats.NewSpells
{
    internal class Atavism
    {
        private static readonly string SpellName = "Atavism";
        internal const string DisplayName = "Atavism.Name";
        private static readonly string Description = "Atavism.Description";

        public static void Configure()
        {
            var AtavismBuff = BuffConfigurator.New(SpellName + "Buff", Guids.AtavismSpellBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(BuffRefs.WitchHexBeastsGiftBiteBuff.Reference.Get().Icon)
                .SetFrequency(DurationRate.Rounds)
                .AddStatBonus(ModifierDescriptor.Other, stat: StatType.AdditionalAttackBonus, value: 2)
                .AddStatBonus(ModifierDescriptor.Other, stat: StatType.AdditionalDamage, value: 2)
                .AddStatBonus(ModifierDescriptor.Other, stat: StatType.AdditionalCMD, value: 4)
                .AddStatBonus(ModifierDescriptor.Other, stat: StatType.AC, value: 4)
                .AddStatBonus(ModifierDescriptor.Other, stat: StatType.HitPoints, value: 10)
                .AddForbidSpellCasting()
                .AddCondition(Kingmaker.UnitLogic.UnitCondition.StealthForbidden)
                //.AddFactContextActions()
                .Configure();

            AbilityConfigurator.NewSpell(SpellName, Guids.AtavismSpell, SpellSchool.Transmutation, canSpecialize: true, SpellDescriptor.None)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetLocalizedDuration(Duration.MinutePerLevel)
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
                .AddToSpellLists(level: 4, SpellList.Druid, SpellList.Hunter)
                .AddContextRankConfig(ContextRankConfigs.CasterLevel())
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .ApplyBuff(AtavismBuff, ContextDuration.Variable(ContextValues.Rank(), rate: DurationRate.Minutes)))
                .AddAbilityTargetHasFact(checkedFacts: new() { FeatureRefs.AnimalType.Reference.Get() })
                .Configure();
        }
    }
}