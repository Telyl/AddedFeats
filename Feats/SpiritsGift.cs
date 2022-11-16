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

namespace AddedFeats.Feats
{
    public class SpiritsGift
    {
        private static readonly string FeatName = "SpiritsGift";
        internal const string DisplayName = "SpiritsGift.Name";
        private static readonly string Description = "SpiritsGift.Description";

        private static readonly ModLogger Logger = Logging.GetLogger(FeatName);

        internal static void Configure()
        {
            try
            {
                if (Settings.IsEnabled(Guids.SpiritsGift))
                    ConfigureEnabled();
                else
                    ConfigureDisabled();
            }
            catch (Exception e)
            {
                Logger.LogException("SpiritsGift.Configure", e);
            }
        }

        private static void ConfigureDisabled()
        {
            FeatureConfigurator.New(FeatName, Guids.SpiritsGift).Configure();
            FeatureConfigurator.New(FeatName + "Resource", Guids.SpiritsGiftResource).Configure();
            FeatureConfigurator.New(FeatName + "CompanionSpirit", Guids.SpiritsGiftCompanion).Configure();
            SpiritFoci.SpiritFocusBattle.ConfigureDisabled();
            SpiritFoci.SpiritFocusBones.ConfigureDisabled();
            SpiritFoci.SpiritFocusFlame.ConfigureDisabled();
            SpiritFoci.SpiritFocusFrost.ConfigureDisabled();
        }

        public static void ConfigureEnabled()
        {
            BlueprintAbilityResource spiritsgiftresource = AbilityResourceConfigurator.New(FeatName + "Resource", Guids.SpiritsGiftResource)
                .SetMaxAmount(ResourceAmountBuilder.New(1).Build())
                .SetMin(1)
                .Configure();

            var (BattleActiveAbility, CompanionBattleBuff) = SpiritFoci.SpiritFocusBattle.Configure(spiritsgiftresource);
            var (BonesActiveAbility, CompanionBonesBuff) = SpiritFoci.SpiritFocusBones.Configure(spiritsgiftresource);
            var (FlameActiveAbility, CompanionFlameBuff) = SpiritFoci.SpiritFocusFlame.Configure(spiritsgiftresource);
            var (FrostActiveAbility, CompanionFrostBuff) = SpiritFoci.SpiritFocusFrost.Configure(spiritsgiftresource);
            var (LifeActiveAbility, CompanionLifeBuff) = SpiritFoci.SpiritFocusLife.Configure(spiritsgiftresource);
            var (LoreActiveAbility, CompanionLoreBuff) = SpiritFoci.SpiritFocusLore.Configure(spiritsgiftresource);
            var (MammothActiveAbility, CompanionMammothBuff) = SpiritFoci.SpiritFocusMammoth.Configure(spiritsgiftresource);
            var (NatureActiveAbility, CompanionNatureBuff) = SpiritFoci.SpiritFocusNature.Configure(spiritsgiftresource);
            var (SlumsActiveAbility, CompanionSlumsBuff) = SpiritFoci.SpiritFocusSlums.Configure(spiritsgiftresource);
            var (StoneActiveAbility, CompanionStoneBuff) = SpiritFoci.SpiritFocusStone.Configure(spiritsgiftresource);
            var (WavesActiveAbility, CompanionWavesBuff) = SpiritFoci.SpiritFocusWaves.Configure(spiritsgiftresource);
            var (WindActiveAbility, CompanionWindBuff) = SpiritFoci.SpiritFocusWind.Configure(spiritsgiftresource);

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

