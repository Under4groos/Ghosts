using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000E5 RID: 229
	public class TushkanoMeleeState : NPCState<TushkanoNPC>
	{
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x0002A8FD File Offset: 0x00028AFD
		private float meleeRange
		{
			get
			{
				return 90f;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x0002A904 File Offset: 0x00028B04
		private float meleeCooldown
		{
			get
			{
				return 0.5f;
			}
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0002A90B File Offset: 0x00028B0B
		public TushkanoMeleeState(TushkanoNPC host) : base(host)
		{
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0002A924 File Offset: 0x00028B24
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.hasAttacked = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.isAttacking = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastAttack = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Attacking, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.AttackType, 1);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAttackPos();
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0002A995 File Offset: 0x00028B95
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Attacking, false);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0002A9B0 File Offset: 0x00028BB0
		public override void Update(float deltaTime)
		{
			if (!this.Host.TargetingComponent.ValidateTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.TargetingComponent.RecalculateThreatLevelsByDistance(false);
				if (this.Host.TargetingComponent.ValidateTarget())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.SM.SetState(this.Host.Damaged ? this.Host.FleeState : this.Host.ChaseState);
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
				return;
			}
			else
			{
				if (this.timeSinceLastAttack < 0.1f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.FaceTarget();
				}
				if (this.isAttacking)
				{
					return;
				}
				bool canMelee = this.CanDoMelee();
				if (this.hasAttacked && this.timeSinceLastAttack > this.meleeCooldown && !canMelee)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.SM.SetState(this.Host.Damaged ? this.Host.FleeState : this.Host.ChaseState);
					return;
				}
				if (!this.isAttacking && this.timeSinceLastAttack > this.meleeCooldown && canMelee)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.isAttacking = true;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.SetAnimParameter(NPCAnimParameters.Attacking, true);
				}
				return;
			}
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0002AB25 File Offset: 0x00028D25
		public override void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData)
		{
			if (name.Equals("NPCAttackEventFinished"))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnFinishedAttack();
			}
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0002AB3F File Offset: 0x00028D3F
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoMeleeAttack();
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0002AB4C File Offset: 0x00028D4C
		private void OnFinishedAttack()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.isAttacking = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastAttack = 0f;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0002AB70 File Offset: 0x00028D70
		private void DoMeleeAttack()
		{
			if (!this.Host.TargetingComponent.ValidateTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnFinishedAttack();
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PerformTraceAttack(this.attackPos, this.Host.AttackDamage, 50f, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAttackPos();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.hasAttacked = true;
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0002ABE0 File Offset: 0x00028DE0
		private void SetAttackPos()
		{
			Vector3 dir = (base.Target.WorldSpaceBounds.Center - this.Host.Position).Normal;
			Vector3 attackPos = this.Host.Position + dir * this.meleeRange;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.attackPos = attackPos;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0002AC42 File Offset: 0x00028E42
		public bool CanDoMelee()
		{
			return this.Host.TargetingComponent.DistanceToTarget <= this.meleeRange && this.Host.TargetingComponent.CanSeeTarget();
		}

		// Token: 0x04000327 RID: 807
		private TimeSince timeSinceLastAttack = 0f;

		// Token: 0x04000328 RID: 808
		private Vector3 attackPos;

		// Token: 0x04000329 RID: 809
		private bool hasAttacked;

		// Token: 0x0400032A RID: 810
		private bool isAttacking;
	}
}
