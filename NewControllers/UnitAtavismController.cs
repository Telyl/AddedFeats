using System;
using Kingmaker;
using Kingmaker.Blueprints.Root;
using Kingmaker.Controllers;
using Kingmaker.Controllers.Units;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Commands;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Parts;
using TurnBased.Controllers;

namespace AddedFeats.NewControllers
{
	// Token: 0x020016DB RID: 5851
	public class UnitAtavismController : BaseUnitController
	{
		// Token: 0x0600A47B RID: 42107 RVA: 0x002ADFA4 File Offset: 0x002AC1A4
		public override void TickOnUnit(UnitEntityData unit)
		{
			if (CombatController.IsInTurnBasedCombat())
			{
				if (unit.IsCurrentUnit())
				{
					UnitPartConfusion unitPartConfusion = unit.Get<UnitPartConfusion>();
					if (unitPartConfusion && unitPartConfusion.RoundStartTime < Game.Instance.TimeController.GameTime)
					{
						UnitCommand cmd = unitPartConfusion.Cmd;
						if (cmd != null)
						{
							cmd.Interrupt(true);
						}
						unitPartConfusion.RoundStartTime = TimeSpan.Zero;
					}
				}
				else if (!CombatController.IsPassing() || unit.IsInCombat)
				{
					return;
				}
			}
			if (unit.Descriptor.State.HasCondition(UnitCondition.Confusion) || unit.Descriptor.State.HasCondition(UnitCondition.AttackNearest))
			{
				UnitConfusionController.TickConfusion(unit);
				return;
			}
			unit.Remove<UnitPartConfusion>();
		}

		// Token: 0x0600A47C RID: 42108 RVA: 0x002AE050 File Offset: 0x002AC250
		private static void TickConfusion(UnitEntityData unit)
		{
			UnitPartConfusion part = unit.Ensure<UnitPartConfusion>();
			bool flag = !unit.CombatState.HasCooldownForCommand(UnitCommand.CommandType.Standard);
			if (Game.Instance.TimeController.GameTime - part.RoundStartTime > UnitConfusionController.RoundDuration && flag)
			{
				
				part.State = ConfusionState.AttackNearest;
				part.ReleaseControl();

				EventBus.RaiseEvent<IConfusionRollResultHandler>(delegate (IConfusionRollResultHandler x)
				{
					x.HandleConfusionRollResult(unit, part.State);
				}, true);
				part.RoundStartTime = Game.Instance.TimeController.GameTime;
				UnitCommand cmd = part.Cmd;
				if (cmd != null)
				{
					cmd.Interrupt(true);
				}
				part.Cmd = null;
			}
			if (part.Cmd == null && unit.Descriptor.State.CanAct && part.State != ConfusionState.ActNormally)
			{
				if (flag)
				{
					switch (part.State)
					{
						case ConfusionState.AttackNearest:
							part.Cmd = UnitConfusionController.AttackNearest(part);
							break;
						default:
							throw new ArgumentOutOfRangeException();
					}
				}
				else
				{
					part.Cmd = UnitConfusionController.DoNothing(part);
				}
				if (part.Cmd != null)
				{
					part.Owner.Unit.Commands.Run(part.Cmd);
				}
			}
		}

		// Token: 0x0600A47D RID: 42109 RVA: 0x002AE2AD File Offset: 0x002AC4AD
		private static UnitCommand DoNothing(UnitPartConfusion part)
		{
			return new UnitDoNothing();
		}

		// Token: 0x0600A47E RID: 42110 RVA: 0x002AE2B4 File Offset: 0x002AC4B4
		private static UnitCommand SelfHarm(UnitPartConfusion part)
		{
			return new UnitSelfHarm();
		}

		// Token: 0x0600A47F RID: 42111 RVA: 0x002AE2BC File Offset: 0x002AC4BC
		private static UnitCommand AttackNearest(UnitPartConfusion part)
		{
			UnitEntityData unitEntityData = null;
			foreach (UnitGroupMemory.UnitInfo unitInfo in part.Owner.Unit.Memory.UnitsList)
			{
				UnitEntityData unit = unitInfo.Unit;
				if (!(unit == part.Owner.Unit) && (unitEntityData == null || part.Owner.Unit.SqrDistanceTo(unit) < part.Owner.Unit.SqrDistanceTo(unitEntityData)))
				{
					unitEntityData = unit;
				}
			}
			foreach (UnitEntityData unitEntityData2 in part.Owner.Unit.Group)
			{
				if (!(unitEntityData2 == part.Owner.Unit) && (unitEntityData == null || part.Owner.Unit.SqrDistanceTo(unitEntityData2) < part.Owner.Unit.SqrDistanceTo(unitEntityData)))
				{
					unitEntityData = unitEntityData2;
				}
			}
			if (unitEntityData == null)
			{
				return UnitConfusionController.DoNothing(part);
			}
			Ability ability = part.Owner.Abilities.GetAbility(BlueprintRoot.Instance.SystemMechanics.ChargeAbility);
			if (ability != null && ability.Data.IsAvailable && ability.Data.CanTarget(unitEntityData))
			{
				return new UnitUseAbility(ability.Data, unitEntityData);
			}
			return UnitAttack.CreateAttackCommand(part.Owner.Unit, unitEntityData);
		}

		// Token: 0x040069C9 RID: 27081
		private static readonly TimeSpan RoundDuration = 6f.Seconds();
	}
}
