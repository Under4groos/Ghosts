using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000DE RID: 222
	public class SnorkIdleState : NPCState<SnorkNPC>
	{
		// Token: 0x06000994 RID: 2452 RVA: 0x0002967E File Offset: 0x0002787E
		public SnorkIdleState(SnorkNPC host) : base(host)
		{
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00029697 File Offset: 0x00027897
		public override void Update(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FindTarget();
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x000296A4 File Offset: 0x000278A4
		private void FindTarget()
		{
			if (this.timeSinceLastThreatCalc > 0.3f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.TargetingComponent.RecalculateThreatLevelsByDistance(false);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.timeSinceLastThreatCalc = 0f;
			}
			if (this.Host.TargetingComponent.ValidateTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.ChaseState);
				return;
			}
		}

		// Token: 0x0400030F RID: 783
		private TimeSince timeSinceLastThreatCalc = 0f;
	}
}
