using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000A0 RID: 160
	public class BlindDogMeleeState : NPCState<BlindDogNPC>
	{
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x00020B55 File Offset: 0x0001ED55
		private float MeleeRange
		{
			get
			{
				return 100f;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x00020B5C File Offset: 0x0001ED5C
		private float MeleeCooldown
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00020B63 File Offset: 0x0001ED63
		public BlindDogMeleeState(BlindDogNPC host) : base(host)
		{
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00020B7C File Offset: 0x0001ED7C
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

		// Token: 0x06000722 RID: 1826 RVA: 0x00020BED File Offset: 0x0001EDED
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Attacking, false);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00020C08 File Offset: 0x0001EE08
		public override void Update(float deltaTime)
		{
			if (!this.Host.TargetingComponent.TryEnsureTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.Damaged ? this.Host.FleeState : this.Host.IdleState);
				return;
			}
			if (this.IsAttacking)
			{
				return;
			}
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
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00020D04 File Offset: 0x0001EF04
		public override void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData)
		{
			if (name.Equals("NPCAttackEventFinished"))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnFinishedAttack();
			}
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00020D1E File Offset: 0x0001EF1E
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoMeleeAttack();
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00020D2B File Offset: 0x0001EF2B
		private void OnFinishedAttack()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsAttacking = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceLastAttack = 0f;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00020D50 File Offset: 0x0001EF50
		private void DoMeleeAttack()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PerformTraceAttack(this.AttackPos, this.Host.AttackDamage, 50f, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAttackPos();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasAttacked = true;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00020DA0 File Offset: 0x0001EFA0
		private void SetAttackPos()
		{
			Vector3 dir = (base.Target.WorldSpaceBounds.Center - this.Host.Position).Normal;
			Vector3 attackPos = this.Host.Position + dir * this.MeleeRange;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AttackPos = attackPos;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00020E02 File Offset: 0x0001F002
		public bool CanDoMelee()
		{
			return this.Host.TargetingComponent.DistanceToTarget <= this.MeleeRange && this.Host.TargetingComponent.CanSeeTarget();
		}

		// Token: 0x04000266 RID: 614
		private TimeSince TimeSinceLastAttack = 0f;

		// Token: 0x04000267 RID: 615
		private Vector3 AttackPos;

		// Token: 0x04000268 RID: 616
		private bool HasAttacked;

		// Token: 0x04000269 RID: 617
		private bool IsAttacking;
	}
}
