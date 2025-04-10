﻿using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.ResourceLinks;
using Kingmaker;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace AddedFeats.NewComponents
{
    [TypeId("1a344f2340da40347a11ee61fcad7223")]
    public class AddImprovedForcefulCharge : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleAttackWithWeapon>, IRulebookHandler<RuleAttackWithWeapon>, ISubscriber, IInitiatorRulebookSubscriber, IResourcesHolder
    {
        public void OnEventAboutToTrigger(RuleAttackWithWeapon evt)
        {
        }

        public void OnEventDidTrigger(RuleAttackWithWeapon evt)
        {
            if (evt.IsCharge && evt.AttackRoll.IsHit)
            {
                RuleCombatManeuver newevt = Game.Instance.Rulebook.TriggerEvent<RuleCombatManeuver>(new RuleCombatManeuver(evt.Initiator, evt.GetRuleTarget(), CombatManeuver.BullRush));
                if (newevt.Success && ((newevt.InitiatorCMValue - newevt.TargetCMD) >= 5))
                {
                    Game.Instance.Rulebook.TriggerEvent<RuleCombatManeuver>(new RuleCombatManeuver(evt.Initiator, evt.GetRuleTarget(), CombatManeuver.Trip));
                }
            }
        }
    }
}