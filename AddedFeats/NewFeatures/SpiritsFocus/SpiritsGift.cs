    using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using AddedFeats.Utils;
using static UnityModManagerNet.UnityModManager.ModEntry;
using System;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using Kingmaker.Blueprints;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using AddedFeats.Feats.SpiritFocus;

namespace AddedFeats.NewFeatures.SpiritFocus
{
    internal class SpiritsGift
    {
        private static readonly string FeatName = "SpiritsGift";
        internal const string DisplayName = "SpiritsGift.Name";
        private static readonly string Description = "SpiritsGift.Description";
        public static void Configure()
        {
            BlueprintAbilityResource spiritsgiftresource = AbilityResourceConfigurator.New(FeatName + "Resource", Guids.SpiritsGiftResource)
                .SetMaxAmount(ResourceAmountBuilder.New(1).Build())
                .SetMin(1)
                .Configure();

            var (BattleActiveAbility, CompanionBattleBuff) = SpiritFocusBattle.Configure(spiritsgiftresource);
            var (BonesActiveAbility, CompanionBonesBuff) = SpiritFocusBones.Configure(spiritsgiftresource);
            var (FlameActiveAbility, CompanionFlameBuff) = SpiritFocusFlame.Configure(spiritsgiftresource);
            var (FrostActiveAbility, CompanionFrostBuff) = SpiritFocusFrost.Configure(spiritsgiftresource);
            var (LifeActiveAbility, CompanionLifeBuff) = SpiritFocusLife.Configure(spiritsgiftresource);
            var (LoreActiveAbility, CompanionLoreBuff) = SpiritFocusLore.Configure(spiritsgiftresource);
            var (MammothActiveAbility, CompanionMammothBuff) = SpiritFocusMammoth.Configure(spiritsgiftresource);
            var (NatureActiveAbility, CompanionNatureBuff) = SpiritFocusNature.Configure(spiritsgiftresource);
            var (SlumsActiveAbility, CompanionSlumsBuff) = SpiritFocusSlums.Configure(spiritsgiftresource);
            var (StoneActiveAbility, CompanionStoneBuff) = SpiritFocusStone.Configure(spiritsgiftresource);
            var (WavesActiveAbility, CompanionWavesBuff) = SpiritFocusWaves.Configure(spiritsgiftresource);
            var (WindActiveAbility, CompanionWindBuff) = SpiritFocusWind.Configure(spiritsgiftresource);

            // The buffs should be the correct buffs that it wants to have.
            BlueprintBuff SpiritsGiftCompanionBuff = BuffConfigurator.New(FeatName + "CompanionBuff", Guids.SpiritsGiftCompanionBuff)
                .AddShareBuffsWithPet(type: PetType.AnimalCompanion, buffs: new() { CompanionBattleBuff, CompanionBonesBuff, CompanionFlameBuff, CompanionFrostBuff,
                    CompanionLifeBuff,
                    CompanionLoreBuff,
                    CompanionMammothBuff,
                    CompanionNatureBuff,
                    CompanionSlumsBuff,
                    CompanionStoneBuff,
                    CompanionWavesBuff,
                    CompanionWindBuff
                })
                .SetStacking(StackingType.Replace)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .Configure();

            BlueprintFeature SpiritsGiftCompanion = FeatureConfigurator.New(FeatName + "Companion", Guids.SpiritsGiftCompanion)
                .SetHideInCharacterSheetAndLevelUp()
                .SetHideInUI()
                .AddBuffExtraEffects(checkedBuff: CompanionBattleBuff, extraEffectBuff: SpiritsGiftCompanionBuff)
                .AddBuffExtraEffects(checkedBuff: CompanionBonesBuff, extraEffectBuff: SpiritsGiftCompanionBuff)
                .AddBuffExtraEffects(checkedBuff: CompanionFlameBuff, extraEffectBuff: SpiritsGiftCompanionBuff)
                .AddBuffExtraEffects(checkedBuff: CompanionFrostBuff, extraEffectBuff: SpiritsGiftCompanionBuff)
                .AddBuffExtraEffects(checkedBuff: CompanionLifeBuff, extraEffectBuff: SpiritsGiftCompanionBuff)
                .AddBuffExtraEffects(checkedBuff: CompanionLoreBuff, extraEffectBuff: SpiritsGiftCompanionBuff)
                .AddBuffExtraEffects(checkedBuff: CompanionMammothBuff, extraEffectBuff: SpiritsGiftCompanionBuff)
                .AddBuffExtraEffects(checkedBuff: CompanionNatureBuff, extraEffectBuff: SpiritsGiftCompanionBuff)
                .AddBuffExtraEffects(checkedBuff: CompanionSlumsBuff, extraEffectBuff: SpiritsGiftCompanionBuff)
                .AddBuffExtraEffects(checkedBuff: CompanionStoneBuff, extraEffectBuff: SpiritsGiftCompanionBuff)
                .AddBuffExtraEffects(checkedBuff: CompanionWavesBuff, extraEffectBuff: SpiritsGiftCompanionBuff)
                .AddBuffExtraEffects(checkedBuff: CompanionWindBuff, extraEffectBuff: SpiritsGiftCompanionBuff)
                .Configure();


            var SpiritsGiftAbility = AbilityConfigurator.New(FeatName + "Ability", Guids.SpiritsGiftAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.ShamanWanderingHexAbility.Reference.Get().Icon)
                .AddAbilityVariants(new() {
                    BattleActiveAbility.ToReference<BlueprintAbilityReference>(),
                    BonesActiveAbility.ToReference<BlueprintAbilityReference>(),
                    FlameActiveAbility.ToReference<BlueprintAbilityReference>(),
                    FrostActiveAbility.ToReference<BlueprintAbilityReference>(),
                    LifeActiveAbility.ToReference<BlueprintAbilityReference>(),
                    LoreActiveAbility.ToReference<BlueprintAbilityReference>(),
                    MammothActiveAbility.ToReference<BlueprintAbilityReference>(),
                    NatureActiveAbility.ToReference<BlueprintAbilityReference>(),
                    SlumsActiveAbility.ToReference<BlueprintAbilityReference>(),
                    StoneActiveAbility.ToReference<BlueprintAbilityReference>(),
                    WavesActiveAbility.ToReference<BlueprintAbilityReference>(),
                    WindActiveAbility.ToReference<BlueprintAbilityReference>()
                })
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.SpiritsGift, FeatureGroup.Feat)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddPrerequisitePet()
                .AddAbilityResources(amount: 0, resource: spiritsgiftresource, restoreAmount: true)
                .AddFacts(new() { SpiritsGiftCompanion, SpiritsGiftAbility })
                .Configure();
        }
    }
}

