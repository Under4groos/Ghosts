using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000A9 RID: 169
	public class FleshIdleState : NPCState<FleshNPC>
	{
		// Token: 0x0600076D RID: 1901 RVA: 0x00021DC8 File Offset: 0x0001FFC8
		public FleshIdleState(FleshNPC host) : base(host)
		{
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00021DE1 File Offset: 0x0001FFE1
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PlaySound(this.Host.GrowlSound);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00021DFF File Offset: 0x0001FFFF
		public override void Update(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FindTarget();
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00021E0C File Offset: 0x0002000C
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
				this.Host.SM.SetState(this.Host.CircleState);
				return;
			}
		}

		// Token: 0x0400027C RID: 636
		private TimeSince timeSinceLastThreatCalc = 0f;
	}
}
