using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000E0 RID: 224
	public class SnorkMeleeState : NPCState<SnorkNPC>
	{
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x00029C45 File Offset: 0x00027E45
		private float AttackRange
		{
			get
			{
				return 100f;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x00029C4C File Offset: 0x00027E4C
		private float AggroRange
		{
			get
			{
				return 300f;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00029C53 File Offset: 0x00027E53
		private float Cooldown
		{
			get
			{
				return 6f;
			}
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00029C5A File Offset: 0x00027E5A
		public SnorkMeleeState(SnorkNPC snork) : base(snork)
		{
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00029C74 File Offset: 0x00027E74
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bRunning", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("eAttackType", 0);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.offset = (float)(Rand.Int(0, 1) * 2 - 1);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasAttacked = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasBegunAttack = false;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00029D08 File Offset: 0x00027F08
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bRunning", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00029D68 File Offset: 0x00027F68
		public override void Update(float deltaTime)
		{
			if (!this.Host.TargetingComponent.ValidateTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
				return;
			}
			if (this.TimeSinceEntered > 3f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.Steer = null;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.ChaseState);
				return;
			}
			Vector3 dir = (base.Target.Position - this.Host.Position).Normal;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer.Target = base.Target.Position - dir * 35f + dir.Cross(Vector3.Up) * this.offset * 20f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Speed = this.Host.GetWishSpeed();
			if (this.Host.TargetingComponent.DistanceToTarget < this.AttackRange && this.TimeSinceLastAttack > 2f && !this.HasBegunAttack)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DoAttackAnim();
				return;
			}
			if (this.HasAttacked)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.Steer = null;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.FlankState);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", false);
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00029F0F File Offset: 0x0002810F
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PerformTraceAttackOnTarget(this.Host.AttackDamage, 50f, this.AttackRange, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasAttacked = true;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00029F49 File Offset: 0x00028149
		private void DoAttackAnim()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceLastAttack = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasBegunAttack = true;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00029F84 File Offset: 0x00028184
		public bool CanDoMelee()
		{
			return this.TimeSinceExited >= this.Cooldown && this.Host.TargetingComponent.DistanceToTarget <= this.AggroRange && this.Host.TargetingComponent.CanSeeTarget();
		}

		// Token: 0x04000318 RID: 792
		private TimeSince TimeSinceLastAttack = 0f;

		// Token: 0x04000319 RID: 793
		private float offset;

		// Token: 0x0400031A RID: 794
		private bool HasAttacked;

		// Token: 0x0400031B RID: 795
		private bool HasBegunAttack;
	}
}
