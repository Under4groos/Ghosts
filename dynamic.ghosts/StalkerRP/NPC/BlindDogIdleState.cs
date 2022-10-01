using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x0200009E RID: 158
	public class BlindDogIdleState : NPCState<BlindDogNPC>
	{
		// Token: 0x0600070E RID: 1806 RVA: 0x000207C7 File Offset: 0x0001E9C7
		public BlindDogIdleState(BlindDogNPC host) : base(host)
		{
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x000207E0 File Offset: 0x0001E9E0
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PlaySound(this.Host.GrowlSound);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x000207FE File Offset: 0x0001E9FE
		public override void Update(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FindTarget();
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0002080C File Offset: 0x0001EA0C
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

		// Token: 0x04000264 RID: 612
		private TimeSince timeSinceLastThreatCalc = 0f;
	}
}
