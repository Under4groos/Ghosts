using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000B4 RID: 180
	public class KarlikIdleState : NPCState<KarlikNPC>
	{
		// Token: 0x060007C9 RID: 1993 RVA: 0x0002338B File Offset: 0x0002158B
		public KarlikIdleState(KarlikNPC host) : base(host)
		{
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x000233A4 File Offset: 0x000215A4
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PlaySound(this.Host.GrowlSound);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x000233C2 File Offset: 0x000215C2
		public override void Update(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FindTarget();
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x000233D0 File Offset: 0x000215D0
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

		// Token: 0x04000298 RID: 664
		private TimeSince timeSinceLastThreatCalc = 0f;
	}
}
