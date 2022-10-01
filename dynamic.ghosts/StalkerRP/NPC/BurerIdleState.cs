using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x02000084 RID: 132
	public class BurerIdleState : NPCState<BurerNPC>
	{
		// Token: 0x060005E6 RID: 1510 RVA: 0x0001BA98 File Offset: 0x00019C98
		public BurerIdleState(BurerNPC burer) : base(burer)
		{
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001BAB1 File Offset: 0x00019CB1
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasTarget", false);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001BADF File Offset: 0x00019CDF
		public override void OnStateExited()
		{
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001BAE1 File Offset: 0x00019CE1
		public override void Update(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FindTarget();
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001BAF0 File Offset: 0x00019CF0
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

		// Token: 0x04000216 RID: 534
		private TimeSince timeSinceLastThreatCalc = 0f;
	}
}
