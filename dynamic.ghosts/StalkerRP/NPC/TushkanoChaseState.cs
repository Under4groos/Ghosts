using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000E7 RID: 231
	public class TushkanoChaseState : NPCState<TushkanoNPC>
	{
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x0002AF25 File Offset: 0x00029125
		private float maxChaseDuration
		{
			get
			{
				return 10f;
			}
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0002AF2C File Offset: 0x0002912C
		public TushkanoChaseState(TushkanoNPC host) : base(host)
		{
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0002AF45 File Offset: 0x00029145
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0002AF72 File Offset: 0x00029172
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0002AF9C File Offset: 0x0002919C
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
				this.Host.SM.SetState(this.Host.FleeState);
				return;
			}
			if (this.Host.MeleeState.CanDoMelee())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.MeleeState);
				return;
			}
		}

		// Token: 0x04000330 RID: 816
		private TimeSince timeSinceLastThreatCalc = 0f;
	}
}
