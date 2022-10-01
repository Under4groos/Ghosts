using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000B2 RID: 178
	public class KarlikChaseState : NPCState<KarlikNPC>
	{
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x00022DBE File Offset: 0x00020FBE
		private float maxChaseDuration
		{
			get
			{
				return 3f;
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00022DC5 File Offset: 0x00020FC5
		public KarlikChaseState(KarlikNPC host) : base(host)
		{
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00022DDE File Offset: 0x00020FDE
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00022E0B File Offset: 0x0002100B
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00022E34 File Offset: 0x00021034
		public override void Update(float deltaTime)
		{
			if (this.timeSinceLastThreatCalc > 1f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.TargetingComponent.RecalculateThreatLevelsByDistance(false);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.timeSinceLastThreatCalc = 0f;
			}
			if (!this.Host.TargetingComponent.TryEnsureTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer.Target = base.Target.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Speed = this.Host.GetWishSpeed();
			if (this.TimeSinceEntered > this.maxChaseDuration)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.CircleState);
				return;
			}
			if (this.Host.TargetingComponent.DistanceToTarget < this.Host.MeleeState.AttackRange)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.MeleeState);
				return;
			}
		}

		// Token: 0x04000291 RID: 657
		private TimeSince timeSinceLastThreatCalc = 0f;
	}
}
