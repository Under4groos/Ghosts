using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x02000090 RID: 144
	public class ChimeraLeapState : ChimeraState
	{
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x0001DF01 File Offset: 0x0001C101
		private float Cooldown
		{
			get
			{
				return 2f;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x0001DF08 File Offset: 0x0001C108
		private float LeapZOffset
		{
			get
			{
				return 65f;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x0001DF0F File Offset: 0x0001C10F
		private float AttackRange
		{
			get
			{
				return 100f;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x0001DF16 File Offset: 0x0001C116
		private float LandDelay
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001DF1D File Offset: 0x0001C11D
		public ChimeraLeapState(ChimeraNPC host) : base(host)
		{
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001DF38 File Offset: 0x0001C138
		public override void OnStateEntered()
		{
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
			this.TargetPos = this.Host.Target.WorldSpaceBounds.Center + default(Vector3).WithZ(this.LeapZOffset);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001DFEC File Offset: 0x0001C1EC
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bLanded", false);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001E01A File Offset: 0x0001C21A
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LeapTowardsTarget();
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001E028 File Offset: 0x0001C228
		public override void Update(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", false);
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
			if (!this.HasLanded || this.timeSinceLanded <= this.LandDelay)
			{
				return;
			}
			if (this.Host.TargetingComponent.ValidateTarget())
			{
				this.Host.SM.SetState(this.Host.ChaseState);
				return;
			}
			this.Host.SM.SetState(this.Host.IdleState);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001E148 File Offset: 0x0001C348
		public bool CanLeap()
		{
			if (!base.Target.IsValid())
			{
				return false;
			}
			if (this.TimeSinceExited < this.Cooldown)
			{
				return false;
			}
			if (!this.Host.TargetingComponent.CanSeeTarget())
			{
				return false;
			}
			Vector3 dir = (base.Target.Position.WithZ(0f) - this.Host.Position.WithZ(0f)).Normal;
			Vector3 lookDir = this.Host.Rotation.Forward.WithZ(0f);
			return dir.Dot(lookDir) >= 0.8f && this.Host.TargetingComponent.DistanceToTarget < this.Host.MaxLeapRange && this.Host.TargetingComponent.DistanceToTarget > this.Host.MinLeapRange;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001E23C File Offset: 0x0001C43C
		private void LeapTowardsTarget()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.GroundEntity = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Position += Vector3.Up * 10f;
			Vector3 dir = (this.TargetPos - this.Host.Position).Normal;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Velocity = dir * this.Host.TargetingComponent.DistanceToTarget * 2.3f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasLeaped = true;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001E2E0 File Offset: 0x0001C4E0
		private void TryAttack()
		{
			if (this.Host.TargetingComponent.DistanceToTarget < this.AttackRange)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.PerformTraceAttackOnTarget(this.Host.LeapDamage, 50f, this.AttackRange, 0f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.PlaySound(this.Host.LeapHitSound);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.HasAttacked = true;
			}
		}

		// Token: 0x04000239 RID: 569
		private bool HasLeaped;

		// Token: 0x0400023A RID: 570
		private bool HasLanded;

		// Token: 0x0400023B RID: 571
		private bool HasAttacked;

		// Token: 0x0400023C RID: 572
		private float LandingVelocity;

		// Token: 0x0400023D RID: 573
		private Vector3 TargetPos;

		// Token: 0x0400023E RID: 574
		private TimeSince timeSinceLanded = 0f;
	}
}
