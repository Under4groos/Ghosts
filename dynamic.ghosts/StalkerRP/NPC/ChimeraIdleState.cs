using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x0200008F RID: 143
	public class ChimeraIdleState : ChimeraState
	{
		// Token: 0x0600066D RID: 1645 RVA: 0x0001DE3D File Offset: 0x0001C03D
		public ChimeraIdleState(ChimeraNPC host) : base(host)
		{
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001DE56 File Offset: 0x0001C056
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PlaySound(this.Host.GrowlSound);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001DE74 File Offset: 0x0001C074
		public override void Update(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FindTarget();
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001DE84 File Offset: 0x0001C084
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

		// Token: 0x04000238 RID: 568
		private TimeSince timeSinceLastThreatCalc = 0f;
	}
}
