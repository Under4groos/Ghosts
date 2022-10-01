using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000AF RID: 175
	public class IzlomIdleState : NPCState<IzlomNPC>
	{
		// Token: 0x0600079E RID: 1950 RVA: 0x0002283B File Offset: 0x00020A3B
		public IzlomIdleState(IzlomNPC host) : base(host)
		{
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00022854 File Offset: 0x00020A54
		public override void OnStateExited()
		{
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00022856 File Offset: 0x00020A56
		public override void Update(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FindTarget();
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00022864 File Offset: 0x00020A64
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

		// Token: 0x04000289 RID: 649
		private TimeSince timeSinceLastThreatCalc = 0f;
	}
}
