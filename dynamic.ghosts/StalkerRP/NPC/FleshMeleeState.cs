using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000AA RID: 170
	public class FleshMeleeState : NPCState<FleshNPC>
	{
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x00021E89 File Offset: 0x00020089
		private float meleeRange
		{
			get
			{
				return 100f;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x00021E90 File Offset: 0x00020090
		private float meleeCooldown
		{
			get
			{
				return 0.5f;
			}
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00021E97 File Offset: 0x00020097
		public FleshMeleeState(FleshNPC host) : base(host)
		{
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00021EB0 File Offset: 0x000200B0
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

		// Token: 0x06000775 RID: 1909 RVA: 0x00021F21 File Offset: 0x00020121
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Attacking, false);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00021F3C File Offset: 0x0002013C
		public override void Update(float deltaTime)
		{
			if (!this.Host.TargetingComponent.TryEnsureTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.Damaged ? this.Host.FleeState : this.Host.IdleState);
				return;
			}
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
				this.Host.SM.SetState(this.Host.Damaged ? this.Host.FleeState : this.Host.CircleState);
				return;
			}
			if (!this.isAttacking && this.timeSinceLastAttack > this.meleeCooldown && canMelee)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.isAttacking = true;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SetAnimParameter(NPCAnimParameters.Attacking, true);
			}
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00022065 File Offset: 0x00020265
		public override void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData)
		{
			if (name.Equals("NPCAttackEventFinished"))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnFinishedAttack();
			}
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0002207F File Offset: 0x0002027F
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoMeleeAttack();
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0002208C File Offset: 0x0002028C
		private void OnFinishedAttack()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.isAttacking = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastAttack = 0f;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x000220B0 File Offset: 0x000202B0
		private void DoMeleeAttack()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PerformTraceAttack(this.attackPos, this.Host.AttackDamage, 50f, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAttackPos();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.hasAttacked = true;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00022100 File Offset: 0x00020300
		private void SetAttackPos()
		{
			Vector3 dir = (base.Target.WorldSpaceBounds.Center - this.Host.Position).Normal;
			Vector3 attackPos = this.Host.Position + dir * this.meleeRange;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.attackPos = attackPos;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00022162 File Offset: 0x00020362
		public bool CanDoMelee()
		{
			return this.Host.TargetingComponent.DistanceToTarget <= this.meleeRange && this.Host.TargetingComponent.CanSeeTarget();
		}

		// Token: 0x0400027D RID: 637
		private TimeSince timeSinceLastAttack = 0f;

		// Token: 0x0400027E RID: 638
		private Vector3 attackPos;

		// Token: 0x0400027F RID: 639
		private bool hasAttacked;

		// Token: 0x04000280 RID: 640
		private bool isAttacking;
	}
}
