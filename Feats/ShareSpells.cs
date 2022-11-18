using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using AddedFeats.Utils;
using static UnityModManagerNet.UnityModManager.ModEntry;
using System;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Alignments;
using BlueprintCore.Conditions.Builder;
using static Kingmaker.UnitLogic.FactLogic.AddMechanicsFeature;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using AddedFeats.NewComponents;
using BlueprintCore.Actions.Builder;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Conditions.Builder.BasicEx;
using Kingmaker.Blueprints.Classes.Selection;

namespace AddedFeats.Feats
{
    public class ShareSpells
    {
        private static readonly string FeatName = "ShareSpells";
        internal const string DisplayName = "ShareSpells.Name";
        private static readonly string Description = "ShareSpells.Description";

        private static readonly ModLogger Logger = Logging.GetLogger(FeatName);

        internal static void Configure()
        {
            try
            {
                if (Settings.IsEnabled(Guids.ImprovedSpellSharing))
                    ConfigureEnabled();
                else
                    ConfigureDisabled();
            }
            catch (Exception e)
            {
                Logger.LogException("ShareSpells.Configure", e);
            }
        }
        private static void ConfigureDisabled()
        {
            FeatureConfigurator.New(FeatName, Guids.ImprovedSpellSharing).Configure();
            ActivatableAbilityConfigurator.New(FeatName + "ActiveAbility", Guids.ImprovedSpellSharingActiveAbility).Configure();
            BuffConfigurator.New(FeatName + "BuffCheck", Guids.ImprovedSpellSharingBuffCheck).Configure();
            BuffConfigurator.New(FeatName + "BuffEffect", Guids.ImprovedSpellSharingBuffEffect).Configure();
            BuffConfigurator.New(FeatName + "Buff", Guids.ImprovedSpellSharingBuff).Configure();
        }

        private static void ConfigureEnabled()
        {
            BlueprintBuff ShareSpellsBuffCheck = BuffConfigurator.New(FeatName + "BuffCheck", Guids.ImprovedSpellSharingBuffCheck)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .Configure();

            BlueprintBuff ShareSpellsBuffEffect = BuffConfigurator.New(FeatName + "BuffEffect", Guids.ImprovedSpellSharingBuffEffect)
                .AddComponent<AddSpellSharing>()
                .SetIcon(BuffRefs.Erastil_Buff.Reference.Get().Icon)
                .Configure();

            BlueprintBuff ShareSpellsBuff = BuffConfigurator.New(FeatName + "Buff", Guids.ImprovedSpellSharingBuff)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .AddFactContextActions(
                    activated: ActionsBuilder.New().ApplyBuffPermanent(ShareSpellsBuffEffect).ApplyBuffPermanent(ShareSpellsBuffCheck),
                    deactivated: ActionsBuilder.New().RemoveBuff(ShareSpellsBuffEffect).RemoveBuff(ShareSpellsBuffCheck))
                .Configure();

            var ShareSpellsAbility = ActivatableAbilityConfigurator.New(FeatName + "ActiveAbility", Guids.ImprovedSpellSharingActiveAbility)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(BuffRefs.Erastil_Buff.Reference.Get().Icon)
                .SetBuff(ShareSpellsBuff)
                .Configure();

            FeatureConfigurator.New(FeatName, Guids.ImprovedSpellSharing, FeatureGroup.Feat, FeatureGroup.CombatFeat, FeatureGroup.TeamworkFeat)
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(BuffRefs.Erastil_Buff.Reference.Get().Icon)
                .AddFeatureTagsComponent(FeatureTag.Magic | FeatureTag.Teamwork)
                .AddRecommendationHasFeature(FeatureRefs.HunterTactics.ToString())
                .AddRecommendationHasFeature(FeatureRefs.CavalierTacticianFeature.ToString())
                .AddAsTeamworkFeat(Guids.ImprovedSpellSharingCavalier,
                Guids.ImprovedSpellSharingVanguardBuff, 
                Guids.ImprovedSpellSharingVanguardAbility,
                Guids.ImprovedSpellSharingRagerBuff,
                Guids.ImprovedSpellSharingRagerArea,
                Guids.ImprovedSpellSharingRagerAreaBuff,
                Guids.ImprovedSpellSharingRagerToggleBuff,
                Guids.ImprovedSpellSharingRagerToggle
                )
                //Logic goes here for prereqs!
                .AddFacts(new() { ShareSpellsAbility })
                .Configure(delayed: true);
        }
    }
}
