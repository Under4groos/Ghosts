using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000DD RID: 221
	public class SnorkFlankState : NPCState<SnorkNPC>
	{
		// Token: 0x0600098F RID: 2447 RVA: 0x0002933A File Offset: 0x0002753A
		public SnorkFlankState(SnorkNPC host) : base(host)
		{
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00029354 File Offset: 0x00027554
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
			this.Host.Speed = this.Host.Resource.RunSpeed;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TryFindCoverPath();
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x000293DD File Offset: 0x000275DD
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bRunning", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", false);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0002940C File Offset: 0x0002760C
		public override void Update(float deltaTime)
		{
			if (!this.Host.TargetingComponent.TryEnsureTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Speed = this.Host.GetWishSpeed();
			if (this.Host.IsStuck)
			{
				this.TryFindCoverPath();
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
			if (this.TimeSinceCouldSeeTarget > 1.5f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.ChaseState);
				return;
			}
			if (this.TimeSinceEntered > 2f && this.Host.MeleeState.CanDoMelee())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.MeleeState);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer.Target = ((this.CoverFinder.Path.Count > 0) ? this.CoverFinder.Path.Last<Vector3>() : (this.Host.Rotation.Forward + this.Host.Position));
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x000295C0 File Offset: 0x000277C0
		private void TryFindCoverPath()
		{
			if (base.Target == null)
			{
				Vector3 backward = this.Host.Rotation.Backward;
			}
			else
			{
				Vector3 normal = (this.Host.Position - base.Target.Position).Normal;
			}
			if (this.CoverFinder.FindCoverFromTarget(base.Target, this.Host.Position, 800f, -5f, 8, 360f))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.Steer.OverridePath(this.CoverFinder.Path);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SM.SetState(this.Host.ChaseState);
		}

		// Token: 0x0400030D RID: 781
		private CoverFinder CoverFinder;

		// Token: 0x0400030E RID: 782
		private TimeSince TimeSinceCouldSeeTarget = 0f;
	}
}
