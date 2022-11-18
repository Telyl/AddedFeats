using BlueprintCore.Utils;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.RuleSystem;
using Kingmaker.ResourceLinks;
using Kingmaker;

using System;
using JetBrains.Annotations;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Controllers;
using Kingmaker.Controllers.Combat;
using Kingmaker.Designers;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Settings;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Utility;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.Enums;
using Kingmaker.Blueprints.Facts;
using static UnityModManagerNet.UnityModManager.ModEntry;
using AddedFeats.Utils;
using Kingmaker.UnitLogic.Buffs;
using Newtonsoft.Json;
using Kingmaker.ElementsSystem;
using BlueprintCore.Conditions.Builder;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Components.Base;

namespace AddedFeats.NewComponents
{
    [TypeId("cb6f3ba3cc1f45909e5bae27177c1668")]
    public class AddSpellSharing : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCanApplyBuff>, IRulebookHandler<RuleCanApplyBuff>, ISubscriber, IInitiatorRulebookSubscriber, IResourcesHolder
    {
        private static readonly ModLogger Logger = Logging.GetLogger("ImprovedSpellSharing");
        private static BlueprintUnitFact _spellSharing;
        private static BlueprintUnitFact SpellSharing
        {
            get
            {
                _spellSharing ??= BlueprintTool.Get<BlueprintUnitFact>(Guids.ImprovedSpellSharing);
                return _spellSharing;
            }
        }
        public void OnEventDidTrigger(RuleCanApplyBuff evt)
        {

        }

        public void OnEventAboutToTrigger(RuleCanApplyBuff evt)
        {
            UnitEntityData caster = evt.Reason.Caster;
            UnitEntityData target = evt.Reason.Context.MainTarget.Unit;
            UnitEntityData casterpet = caster.GetPet(PetType.AnimalCompanion);

            // If anything doesn't look correct, get out.
            if (caster == null || target == null || casterpet == null) return;

            // Check if the caster and target are the same.
            if (caster == target)
            {
                // Only operate on spells.
                if (evt.Blueprint.IsFromSpell)
                {
                    // Check if pet has the teamwork feat.
                    if (caster.Descriptor.HasFact(SpellSharing) && casterpet.Descriptor.HasFact(SpellSharing)){
                        TimeSpan? timestamp = evt.Duration;
                        if (timestamp.HasValue)
                        {
                            TimeSpan TS = timestamp.Value;
                            TimeSpan half = new TimeSpan(TS.Ticks / 2);
                            evt.Duration = half;
                        }
                        casterpet.Descriptor.AddBuff(evt.Blueprint, casterpet, evt.Duration, null);
                    }
                }
            }


            
            /*
            BlueprintBuff buff = evt.Blueprint;
            var target = evt.GetRuleTarget();
            if (buff != null)
            {
                if (Context.MaybeCaster
                if target.Pets
                target.Descriptor.AddBuff(buff, target, evt.Duration, null);
            }
            */

            
        }
    }
}

