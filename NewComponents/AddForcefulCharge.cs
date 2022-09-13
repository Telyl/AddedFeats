using BlueprintCore.Utils;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.RuleSystem;
using Kingmaker;

namespace AddedFeats.NewComponents
{
    [TypeId("2a864f2990da40148a22ee61fcad0202")]
    public class AddForcefulCharge : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleAttackWithWeapon>, IRulebookHandler<RuleAttackWithWeapon>
    {
        private static readonly LogWrapper ComponentLogger = LogWrapper.Get("AddForcefulCharge");

        public void OnEventAboutToTrigger(RuleAttackWithWeapon evt)
        {
        }

        public void OnEventDidTrigger(RuleAttackWithWeapon evt)
        {
            if (evt.IsCharge && evt.AttackRoll.IsHit) {
                RuleCombatManeuver newevt = Game.Instance.Rulebook.TriggerEvent<RuleCombatManeuver>(new RuleCombatManeuver(evt.Initiator, evt.GetRuleTarget(), CombatManeuver.BullRush));
            }
        }
    }
}
