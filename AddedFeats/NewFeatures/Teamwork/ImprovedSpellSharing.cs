using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using AddedFeats.Utils;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using AddedFeats.NewComponents;
using BlueprintCore.Actions.Builder;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using BlueprintCore.Actions.Builder.ContextEx;
using Kingmaker.Blueprints.Classes.Selection;

namespace AddedFeats.NewFeatures.Teamwork
{
    public class ImprovedSpellShare
    {
        private static readonly string FeatName = "ImprovedSpellShare";
        internal const string DisplayName = "ImprovedSpellShare.Name";
        private static readonly string Description = "ImprovedSpellShare.Description";
        public static void Configure()
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
                .Configure();
        }
    }
}