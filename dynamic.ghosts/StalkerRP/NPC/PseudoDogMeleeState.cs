using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000C2 RID: 194
	public class PseudoDogMeleeState : NPCState<PseudoDogNPC>
	{
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x00025315 File Offset: 0x00023515
		private float MeleeRange
		{
			get
			{
				return 100f;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x0002531C File Offset: 0x0002351C
		private float MeleeCooldown
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00025323 File Offset: 0x00023523
		public PseudoDogMeleeState(PseudoDogNPC host) : base(host)
		{
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0002533C File Offset: 0x0002353C
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasAttacked = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsAttacking = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceLastAttack = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.AttackType, 1);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Attacking, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAttackPos();
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x000253AD File Offset: 0x000235AD
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Attacking, false);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000253C8 File Offset: 0x000235C8
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
				if (this.IsAttacking)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.FaceTarget();
				bool canMelee = this.CanDoMelee();
				if (this.HasAttacked && this.TimeSinceLastAttack > this.MeleeCooldown && !canMelee)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.SM.SetState(this.Host.Damaged ? this.Host.FleeState : this.Host.ChargeState);
					return;
				}
				if (this.TimeSinceLastAttack > this.MeleeCooldown && canMelee)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.IsAttacking = true;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.SetAnimParameter(NPCAnimParameters.Attacking, true);
				}
				return;
			}
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00025520 File Offset: 0x00023720
		public override void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData)
		{
			if (name.Equals("NPCAttackEventFinished"))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnFinishedAttack();
			}
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0002553A File Offset: 0x0002373A
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoMeleeAttack();
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00025547 File Offset: 0x00023747
		private void OnFinishedAttack()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsAttacking = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceLastAttack = 0f;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0002556C File Offset: 0x0002376C
		private void DoMeleeAttack()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PerformTraceAttack(this.AttackPos, this.Host.AttackDamage, 50f, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAttackPos();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasAttacked = true;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x000255BC File Offset: 0x000237BC
		private void SetAttackPos()
		{
			Vector3 dir = (base.Target.WorldSpaceBounds.Center - this.Host.Position).Normal;
			Vector3 attackPos = this.Host.Position + dir * this.MeleeRange;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AttackPos = attackPos;
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0002561E File Offset: 0x0002381E
		public bool CanDoMelee()
		{
			return this.Host.TargetingComponent.DistanceToTarget <= this.MeleeRange && this.Host.TargetingComponent.CanSeeTarget();
		}

		// Token: 0x040002C0 RID: 704
		private TimeSince TimeSinceLastAttack = 0f;

		// Token: 0x040002C1 RID: 705
		private Vector3 AttackPos;

		// Token: 0x040002C2 RID: 706
		private bool HasAttacked;

		// Token: 0x040002C3 RID: 707
		private bool IsAttacking;
	}
}
