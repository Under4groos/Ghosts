using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000C0 RID: 192
	public class PseudoDogIdleState : NPCState<PseudoDogNPC>
	{
		// Token: 0x06000851 RID: 2129 RVA: 0x00024EA2 File Offset: 0x000230A2
		public PseudoDogIdleState(PseudoDogNPC host) : base(host)
		{
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00024EBB File Offset: 0x000230BB
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PlaySound(this.Host.GrowlSound);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00024ED9 File Offset: 0x000230D9
		public override void Update(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FindTarget();
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00024EE8 File Offset: 0x000230E8
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

		// Token: 0x040002BC RID: 700
		private TimeSince timeSinceLastThreatCalc = 0f;
	}
}
