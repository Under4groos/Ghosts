using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x0200007E RID: 126
	public class BoarMeleeState : NPCState<BoarNPC>
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0001AD51 File Offset: 0x00018F51
		private float MeleeRange
		{
			get
			{
				return 100f;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0001AD58 File Offset: 0x00018F58
		private float MeleeCooldown
		{
			get
			{
				return 0.5f;
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001AD5F File Offset: 0x00018F5F
		public BoarMeleeState(BoarNPC host) : base(host)
		{
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0001AD78 File Offset: 0x00018F78
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasAttacked = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsAttacking = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceLastAttack = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Attacking, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.AttackType, 1);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAttackPos();
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001ADE9 File Offset: 0x00018FE9
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Attacking, false);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0001AE04 File Offset: 0x00019004
		public override void Update(float deltaTime)
		{
			if (!this.Host.TargetingComponent.ValidateTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.TargetingComponent.RecalculateThreatLevelsByDistance(false);
				if (this.Host.TargetingComponent.ValidateTarget())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.SM.SetState(this.Host.Damaged ? this.Host.FleeState : this.Host.CircleState);
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
				return;
			}
			else
			{
				if (this.TimeSinceLastAttack < 0.1f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.FaceTarget();
				}
				if (this.IsAttacking)
				{
					return;
				}
				bool canMelee = this.CanDoMelee();
				if (this.HasAttacked && this.TimeSinceLastAttack > this.MeleeCooldown && !canMelee)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.SM.SetState(this.Host.Damaged ? this.Host.FleeState : this.Host.CircleState);
					return;
				}
				if (!this.IsAttacking && this.TimeSinceLastAttack > this.MeleeCooldown && canMelee)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.IsAttacking = true;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.SetAnimParameter(NPCAnimParameters.Attacking, true);
				}
				return;
			}
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0001AF79 File Offset: 0x00019179
		public override void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData)
		{
			if (name.Equals("NPCAttackEventFinished"))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnFinishedAttack();
			}
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0001AF93 File Offset: 0x00019193
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoMeleeAttack();
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0001AFA0 File Offset: 0x000191A0
		private void OnFinishedAttack()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsAttacking = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceLastAttack = 0f;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0001AFC4 File Offset: 0x000191C4
		private void DoMeleeAttack()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PerformTraceAttack(this.AttackPos, this.Host.AttackDamage, 50f, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAttackPos();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasAttacked = true;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0001B014 File Offset: 0x00019214
		private void SetAttackPos()
		{
			Vector3 dir = (base.Target.WorldSpaceBounds.Center - this.Host.Position).Normal;
			Vector3 attackPos = this.Host.Position + dir * this.MeleeRange;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AttackPos = attackPos;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001B076 File Offset: 0x00019276
		public bool CanDoMelee()
		{
			return this.Host.TargetingComponent.DistanceToTarget <= this.MeleeRange && this.Host.TargetingComponent.CanSeeTarget();
		}

		// Token: 0x04000205 RID: 517
		private TimeSince TimeSinceLastAttack = 0f;

		// Token: 0x04000206 RID: 518
		private Vector3 AttackPos;

		// Token: 0x04000207 RID: 519
		private bool HasAttacked;

		// Token: 0x04000208 RID: 520
		private bool IsAttacking;
	}
}
