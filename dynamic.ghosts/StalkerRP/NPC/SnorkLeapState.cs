using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000DF RID: 223
	public class SnorkLeapState : NPCState<SnorkNPC>
	{
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x00029721 File Offset: 0x00027921
		private float Cooldown
		{
			get
			{
				return 5.5f;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x00029728 File Offset: 0x00027928
		private float LeapZOffset
		{
			get
			{
				return 110f;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x0002972F File Offset: 0x0002792F
		private float AttackRange
		{
			get
			{
				return 100f;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x00029736 File Offset: 0x00027936
		private float LandDelay
		{
			get
			{
				if (!this.HardLanding)
				{
					return 0.5f;
				}
				return 1.2f;
			}
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0002974B File Offset: 0x0002794B
		public SnorkLeapState(SnorkNPC host) : base(host)
		{
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00029774 File Offset: 0x00027974
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("eAttackType", 1);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Velocity *= 0.2f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasLeaped = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasLanded = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasAttacked = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HardLanding = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetPos = this.Host.Target.WorldSpaceBounds.Center + default(Vector3).WithZ(this.LeapZOffset);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0002984A File Offset: 0x00027A4A
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bLanded", false);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00029878 File Offset: 0x00027A78
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LeapTowardsTarget();
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00029888 File Offset: 0x00027A88
		public override void Update(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("fLandSpeed", this.Host.Velocity.Length);
			if (!this.HasLeaped)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.FaceTarget();
				return;
			}
			if (!this.HasLanded)
			{
				if (this.Host.GroundEntity != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.SetAnimParameter("bLanded", true);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.HasLanded = true;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.HardLanding = (this.Host.Velocity.Length > 300f);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.LandingVelocity = this.Host.Velocity.Length;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.timeSinceLanded = 0f;
					return;
				}
				if (!this.HasAttacked)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.TryAttack();
				}
			}
			if (this.HardLanding)
			{
				float oldZ = this.Host.Velocity.z;
				Vector3 landVel = (this.Host.Rotation.Forward * this.LandingVelocity).WithZ(oldZ);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.Velocity = landVel.LerpTo((this.Host.Rotation.Forward * 120f).WithZ(oldZ), this.timeSinceLanded * 2f);
			}
			if (!this.HasLanded || this.timeSinceLanded <= this.LandDelay)
			{
				return;
			}
			if (this.Host.TargetingComponent.ValidateTarget())
			{
				this.Host.SM.SetState(this.Host.FlankState);
				return;
			}
			this.Host.SM.SetState(this.Host.IdleState);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00029A8C File Offset: 0x00027C8C
		public bool CanLeap()
		{
			return base.Target.IsValid() && this.timeSinceLastLeap >= this.Cooldown && this.Host.TargetingComponent.CanSeeTarget() && this.Host.TargetingComponent.DistanceToTarget < this.Host.MaxLeapRange && this.Host.TargetingComponent.DistanceToTarget > this.Host.MinLeapRange;
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00029B0D File Offset: 0x00027D0D
		public void SetLeapCooldown(float n)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastLeap = this.Cooldown - n;
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00029B28 File Offset: 0x00027D28
		private void LeapTowardsTarget()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.GroundEntity = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Position += Vector3.Up * 10f;
			Vector3 tpos = this.TargetPos;
			Vector3 dir = (tpos - this.Host.Position).Normal;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Velocity = dir * this.Host.Position.Distance(tpos) * 1.7f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasLeaped = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastLeap = 0f;
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00029BE8 File Offset: 0x00027DE8
		private void TryAttack()
		{
			if (this.Host.TargetingComponent.DistanceToTarget < this.AttackRange)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.PerformTraceAttackOnTarget(this.Host.LeapDamage, 50f, this.AttackRange, 0f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.HasAttacked = true;
			}
		}

		// Token: 0x04000310 RID: 784
		private bool HasLeaped;

		// Token: 0x04000311 RID: 785
		private bool HasLanded;

		// Token: 0x04000312 RID: 786
		private bool HardLanding;

		// Token: 0x04000313 RID: 787
		private bool HasAttacked;

		// Token: 0x04000314 RID: 788
		private float LandingVelocity;

		// Token: 0x04000315 RID: 789
		private Vector3 TargetPos;

		// Token: 0x04000316 RID: 790
		private TimeSince timeSinceLastLeap = 0f;

		// Token: 0x04000317 RID: 791
		private TimeSince timeSinceLanded = 0f;
	}
}
