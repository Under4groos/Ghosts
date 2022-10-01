using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x02000083 RID: 131
	public class BurerFleeState : NPCState<BurerNPC>
	{
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0001B739 File Offset: 0x00019939
		private float DamageTakenShieldThreshold
		{
			get
			{
				return 60f;
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001B740 File Offset: 0x00019940
		public BurerFleeState(BurerNPC host) : base(host)
		{
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001B75C File Offset: 0x0001995C
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CoverFinder = new CoverFinder();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bRunning", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasTarget", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Speed = this.Host.Resource.RunSpeed;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TryFindCoverPath();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DamageTakenSincEntered = 0f;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001B80C File Offset: 0x00019A0C
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasTarget", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bRunning", false);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001B85C File Offset: 0x00019A5C
		public override void Update(float deltaTime)
		{
			if (!this.Host.TargetingComponent.ValidateTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
				return;
			}
			if (this.Host.Position.AlmostEqual(this.Host.Steer.Target, 15f))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.ChaseState);
				return;
			}
			if (this.Host.CanSee(base.Target, false))
			{
				this.TimeSinceCouldSeeTarget = 0f;
			}
			if (this.TimeSinceCouldSeeTarget > 0.8f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.ChaseState);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer.DebugDrawPath();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer.Target = this.CoverFinder.Path.Last<Vector3>();
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001B978 File Offset: 0x00019B78
		private void TryFindCoverPath()
		{
			Vector3 dir = (base.Target != null) ? (this.Host.Position - base.Target.Position).Normal : this.Host.Rotation.Backward;
			if (this.CoverFinder.FindCoverInDirection(this.Host.WorldSpaceBounds.Center, dir, 600f))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.Steer.OverridePath(this.CoverFinder.Path);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SM.SetState(this.Host.ChaseState);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001BA30 File Offset: 0x00019C30
		public override void TakeDamage(DamageInfo info)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DamageTakenSincEntered += info.Damage;
			if (this.DamageTakenSincEntered < this.DamageTakenShieldThreshold)
			{
				return;
			}
			if (this.Host.ShieldState.CanDoShield())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.ShieldState);
				return;
			}
		}

		// Token: 0x04000213 RID: 531
		private CoverFinder CoverFinder;

		// Token: 0x04000214 RID: 532
		private TimeSince TimeSinceCouldSeeTarget = 0f;

		// Token: 0x04000215 RID: 533
		private float DamageTakenSincEntered;
	}
}
