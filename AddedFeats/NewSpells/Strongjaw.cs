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

namespace AddedFeats.NewSpells
{
    internal class StrongJaw
    {
        private static readonly string SpellName = "StrongJaw";
        internal const string DisplayName = "StrongJaw.Name";
        private static readonly string Description = "StrongJaw.Description";
        public static void Configure()
        {
            var StrongJawBuff = BuffConfigurator.New(SpellName + "Buff", Guids.StrongJawSpellBuff)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(BuffRefs.WitchHexBeastsGiftBiteBuff.Reference.Get().Icon)
                .SetFrequency(DurationRate.Rounds)
                //.AddWeaponSizeChange(sizeCategoryChange: 2)
                .AddIncreaseDiceSizeOnAttack(additionalSize: 2, categories: new WeaponCategory[] { WeaponCategory.Bite, WeaponCategory.Claw, WeaponCategory.Gore, WeaponCategory.Sting, WeaponCategory.Tail, WeaponCategory.Talon, WeaponCategory.Slam, WeaponCategory.Slam, WeaponCategory.Hoof })
                .Configure();

            AbilityConfigurator.NewSpell(SpellName, Guids.StrongJawSpell, SpellSchool.Transmutation, canSpecialize: true, SpellDescriptor.None)
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
                .AddToSpellLists(level: 3, SpellList.Ranger, SpellList.Hunter)
                .AddToSpellLists(level: 4, SpellList.Druid)
                .AddContextRankConfig(ContextRankConfigs.CasterLevel())
                .AddAbilityEffectRunAction(
                    actions: ActionsBuilder.New()
                        .ApplyBuff(StrongJawBuff, ContextDuration.Variable(ContextValues.Rank(), rate: DurationRate.Minutes)))
                .Configure();
        }
    }
}