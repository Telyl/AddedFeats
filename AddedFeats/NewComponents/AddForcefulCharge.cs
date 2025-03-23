using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.ResourceLinks;
using Kingmaker;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace AddedFeats.NewComponents
{
    [TypeId("2a364f2990da40148a22ee61fcad0201")]
    public class AddForcefulCharge : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleAttackWithWeapon>, IRulebookHandler<RuleAttackWithWeapon>, ISubscriber, IInitiatorRulebookSubscriber, IResourcesHolder
    {
        public void OnEventAboutToTrigger(RuleAttackWithWeapon evt)
        {
        }

        public void OnEventDidTrigger(RuleAttackWithWeapon evt)
        {
            if (evt.IsCharge && evt.AttackRoll.IsHit)
            {
                RuleCombatManeuver newevt = Game.Instance.Rulebook.TriggerEvent<RuleCombatManeuver>(new RuleCombatManeuver(evt.Initiator, evt.GetRuleTarget(), CombatManeuver.BullRush));
            }
        }
    }
}