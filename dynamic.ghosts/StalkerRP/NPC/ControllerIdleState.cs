using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x02000095 RID: 149
	public class ControllerIdleState : NPCState<ControllerNPC>
	{
		// Token: 0x060006A3 RID: 1699 RVA: 0x0001EACE File Offset: 0x0001CCCE
		public ControllerIdleState(ControllerNPC host) : base(host)
		{
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001EAE7 File Offset: 0x0001CCE7
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasTarget", false);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001EB15 File Offset: 0x0001CD15
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PlaySound("controller.alert");
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001EB2D File Offset: 0x0001CD2D
		public override void Update(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FindTarget();
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001EB3C File Offset: 0x0001CD3C
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

		// Token: 0x04000247 RID: 583
		private TimeSince timeSinceLastThreatCalc = 0f;
	}
}
