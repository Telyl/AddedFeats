using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Enums;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Utils.Types;
using AddedFeats.Utils;
using System;
using static UnityModManagerNet.UnityModManager.ModEntry;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;

namespace AddedFeats.NewFeatures
{
    public class TotemBeast
    {
        private static readonly string FeatName = "TotemBeast";
        internal const string DisplayName = "TotemBeastSelection.Name";
        private static readonly string Description = "TotemBeastSelection.Description";
        public static void Configure()
        {
            #region bull,bear,tiger
            BlueprintBuff bullbuff = BuffConfigurator.New(FeatName + "BullBuff", Guids.TotemBeastBullBuff)
                .SetDisplayName("TotemBeastBull.Name")
                .SetDescription("TotemBeastBull.Description")
                .SetIcon(BuffRefs.AnimalFocusBullBuff.Reference.Get().Icon).Configure();
            BlueprintBuff bearbuff = BuffConfigurator.New(FeatName + "BearBuff", Guids.TotemBeastBearBuff)
                .SetDisplayName("TotemBeastBear.Name")
                .SetDescription("TotemBeastBear.Description")
                .SetIcon(BuffRefs.BearsEnduranceBuff.Reference.Get().Icon).Configure();
            BlueprintBuff tigerbuff = BuffConfigurator.New(FeatName + "TigerBuff", Guids.TotemBeastTigerBuff)
                .SetDisplayName("TotemBeastTiger.Name")
                .SetDescription("TotemBeastTiger.Description")
                .SetIcon(BuffRefs.AnimalFocusTigerBuff.Reference.Get().Icon).Configure();
            (BlueprintBuff, Kingmaker.EntitySystem.Stats.StatType)[] statbuffs = { (bullbuff, StatType.Strength), (bearbuff, StatType.Constitution), (tigerbuff, StatType.Dexterity) };
            foreach (var (buff, stat) in statbuffs)
            {
                BuffConfigurator.For(buff)
                .AddContextStatBonus(stat,
                value: new ContextValue()
                {
                    ValueType = ContextValueType.Rank
                }, ModifierDescriptor.Inherent)
                .AddContextRankConfig(
                    ContextRankConfigs.CharacterLevel(min: 0, max: 20)
                    .WithCustomProgression((7, 2), (14, 4), (20, 6)))
                .Configure();
            }
            #endregion
            #region falcon,owl,monkey
            BlueprintBuff falconbuff = BuffConfigurator.New(FeatName + "FalconBuff", Guids.TotemBeastFalconBuff)
                .SetDisplayName("TotemBeastFalcon.Name")
                .SetDescription("TotemBeastFalcon.Description")
                .SetIcon(BuffRefs.AnimalFocusFalconBuff.Reference.Get().Icon).Configure();
            BlueprintBuff owlbuff = BuffConfigurator.New(FeatName + "OwlBuff", Guids.TotemBeastOwlBuff)
                .SetDisplayName("TotemBeastOwl.Name")
                .SetDescription("TotemBeastOwl.Description")
                .SetIcon(BuffRefs.AnimalFocusOwlBuff.Reference.Get().Icon).Configure();
            BlueprintBuff monkeybuff = BuffConfigurator.New(FeatName + "MonkeyBuff", Guids.TotemBeastMonkeyBuff)
                .SetDisplayName("TotemBeastMonkey.Name")
                .SetDescription("TotemBeastMonkey.Description")
                .SetIcon(BuffRefs.AnimalFocusMonkeyBuff.Reference.Get().Icon).Configure();
            (BlueprintBuff, Kingmaker.EntitySystem.Stats.StatType)[] skillbuffs = { (falconbuff, StatType.SkillPerception), (owlbuff, StatType.SkillStealth), (monkeybuff, StatType.SkillAthletics) };
            foreach (var (buff, stat) in skillbuffs)
            {
                BuffConfigurator.For(buff)
                .AddContextStatBonus(stat,
                value: new ContextValue()
                {
                    ValueType = ContextValueType.Rank
                }, ModifierDescriptor.Inherent)
                .AddContextRankConfig(
                    ContextRankConfigs.CharacterLevel(min: 0, max: 20)
                    .WithCustomProgression((7, 4), (14, 6), (20, 8)))
                .Configure();
            }
            #endregion
            #region stag, mouse
            BlueprintBuff stagbuff = BuffConfigurator.New(FeatName + "StagBuff", Guids.TotemBeastStagBuff)
                .SetDisplayName("TotemBeastStag.Name")
                .SetDescription("TotemBeastStag.Description")
                .AddContextStatBonus(StatType.Speed,
                value: new ContextValue()
                {
                    ValueType = ContextValueType.Rank
                }, ModifierDescriptor.Inherent)
                .AddContextRankConfig(
                    ContextRankConfigs.CharacterLevel(min: 0, max: 20)
                    .WithCustomProgression((7, 5), (14, 10), (20, 20))
                    )
                .SetIcon(BuffRefs.AnimalFocusStagBuff.Reference.Get().Icon).Configure();
            
            BlueprintBuff mousebuff = BuffConfigurator.New(FeatName + "MouseBuff", Guids.TotemBeastMouseBuff)
                .SetDisplayName("TotemBeastMouse.Name")
                .SetDescription("TotemBeastMouse.Description")
                .AddFacts(new() { FeatureRefs.Evasion.Reference.Get() })
                .SetIcon(BuffRefs.AnimalFocusMouseBuff.Reference.Get().Icon).Configure();
            #endregion
            // These are the feats we add for totem beast to our pet.
            var focbullpet = FeatureConfigurator.New(FeatName + "BullPet", Guids.TotemBeastBullPet)
                .SetDisplayName("TotemBeastBull.Name")
                .SetDescription("TotemBeastBull.Description")
                .SetIcon(AbilityRefs.BullsStrength.Reference.Get().Icon)
                .AddFacts(new() { bullbuff })
                .SetReapplyOnLevelUp()
                .Configure();
            var focbearpet = FeatureConfigurator.New(FeatName + "BearPet", Guids.TotemBeastBearPet)
                .SetDisplayName("TotemBeastBear.Name")
                .SetDescription("TotemBeastBear.Description")
                .SetIcon(AbilityRefs.BearsEndurance.Reference.Get().Icon)
                .AddFacts(new() { bearbuff })
                .SetReapplyOnLevelUp()
                .Configure();
            var foctigerpet = FeatureConfigurator.New(FeatName + "TigerPet", Guids.TotemBeastTigerPet)
                .SetDisplayName("TotemBeastTiger.Name")
                .SetDescription("TotemBeastTiger.Description")
                .SetIcon(AbilityRefs.CatsGrace.Reference.Get().Icon)
                .AddFacts(new() { tigerbuff })
                .SetReapplyOnLevelUp()
                .Configure();

            var focfalconpet = FeatureConfigurator.New(FeatName + "FalconPet", Guids.TotemBeastFalconPet)
                .SetDisplayName("TotemBeastFalcon.Name")
                .SetDescription("TotemBeastFalcon.Description")
                .SetIcon(AbilityRefs.AspectOfTheFalcon.Reference.Get().Icon)
                .AddFacts(new() { falconbuff })
                .SetReapplyOnLevelUp()
                .Configure();
            var focstagpet = FeatureConfigurator.New(FeatName + "StagPet", Guids.TotemBeastStagPet)
                .SetDisplayName("TotemBeastStag.Name")
                .SetDescription("TotemBeastStag.Description")
                .SetIcon(AbilityRefs.AspectOfTheStag.Reference.Get().Icon)
                .AddFacts(new() { stagbuff })
                .SetReapplyOnLevelUp()
                .Configure();
            var focmousepet = FeatureConfigurator.New(FeatName + "MousePet", Guids.TotemBeastMousePet)
                .SetDisplayName("TotemBeastMouse.Name")
                .SetDescription("TotemBeastMouse.Description")
                .SetIcon(AbilityRefs.AnimalAspectRacoon.Reference.Get().Icon)
                .AddFacts(new() { mousebuff })
                .SetReapplyOnLevelUp()
                .Configure();
            var focowlpet = FeatureConfigurator.New(FeatName + "OwlPet", Guids.TotemBeastOwlPet)
                .SetDisplayName("TotemBeastOwl.Name")
                .SetDescription("TotemBeastOwl.Description")
                .SetIcon(AbilityRefs.OwlsWisdom.Reference.Get().Icon)
                .AddFacts(new() { owlbuff })
                .SetReapplyOnLevelUp()
                .Configure();
            var focmonkeypet = FeatureConfigurator.New(FeatName + "MonkeyPet", Guids.TotemBeastMonkeyPet)
                .SetDisplayName("TotemBeastMonkey.Name")
                .SetDescription("TotemBeastMonkey.Description")
                .SetIcon(AbilityRefs.AnimalAspectGorilla.Reference.Get().Icon)
                .AddFacts(new() { monkeybuff })
                .SetReapplyOnLevelUp()
                .Configure();

            //We are creating feats for the selection to apply the feat to our pet. There has to be a better way to do this. Madness ensues.
            var fbull = FeatureConfigurator.New(FeatName + "Bull", Guids.TotemBeastBull)
                .SetDisplayName("TotemBeastBull.Name")
                .SetDescription("TotemBeastBull.Description")
                .AddFeatureToPet(focbullpet).Configure();
            var fbear = FeatureConfigurator.New(FeatName + "Bear", Guids.TotemBeastBear)
                .SetDisplayName("TotemBeastBear.Name")
                .SetDescription("TotemBeastBear.Description")
                .AddFeatureToPet(focbearpet).Configure();
            var ftiger = FeatureConfigurator.New(FeatName + "Tiger", Guids.TotemBeastTiger)
                .SetDisplayName("TotemBeastTiger.Name")
                .SetDescription("TotemBeastTiger.Description")
                .AddFeatureToPet(foctigerpet).Configure();
            var ffalcon = FeatureConfigurator.New(FeatName + "Falcon", Guids.TotemBeastFalcon)
                .SetDisplayName("TotemBeastFalcon.Name")
                .SetDescription("TotemBeastFalcon.Description")
                .AddFeatureToPet(focfalconpet).Configure();
            var fstag = FeatureConfigurator.New(FeatName + "Stag", Guids.TotemBeastStag)
                .SetDisplayName("TotemBeastStag.Name")
                .SetDescription("TotemBeastStag.Description")
                .AddFeatureToPet(focstagpet).Configure();
            var fmouse = FeatureConfigurator.New(FeatName + "Mouse", Guids.TotemBeastMouse)
                .SetDisplayName("TotemBeastMouse.Name")
                .SetDescription("TotemBeastMouse.Description")
                .AddFeatureToPet(focmousepet).Configure();
            var fowl = FeatureConfigurator.New(FeatName + "Owl", Guids.TotemBeastOwl)
                .SetDisplayName("TotemBeastOwl.Name")
                .SetDescription("TotemBeastOwl.Description")
                .AddFeatureToPet(focowlpet).Configure();
            var fmonkey = FeatureConfigurator.New(FeatName + "Monkey", Guids.TotemBeastMonkey)
                .SetDisplayName("TotemBeastMonkey.Name")
                .SetDescription("TotemBeastMonkey.Description")
                .AddFeatureToPet(focmonkeypet).Configure();          

            var selection = FeatureSelectionConfigurator.New(FeatName + "Selection", Guids.TotemBeastSelection)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddToAllFeatures(fbull, fbear, ftiger, ffalcon, fstag, fmouse, fowl, fmonkey)
                .SetHideInUI(true)
                .SetHideInCharacterSheetAndLevelUp(true)
                .SetHideNotAvailibleInUI(false)
                .AddPrerequisitePet()
                .AddPrerequisiteFullStatValue(stat: StatType.Wisdom, value: 13)
                .Configure(); 

            var basicFeatSelectionGuid = "247a4068-296e-8be4-2890-143f451b4b45";
            FeatureSelectionConfigurator.For(basicFeatSelectionGuid)
                .AddToAllFeatures(selection)
                .Configure(); 
        }
    }
}

