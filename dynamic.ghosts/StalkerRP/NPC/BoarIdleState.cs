using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x0200007C RID: 124
	public class BoarIdleState : NPCState<BoarNPC>
	{
		// Token: 0x060005A0 RID: 1440 RVA: 0x0001A90A File Offset: 0x00018B0A
		public BoarIdleState(BoarNPC host) : base(host)
		{
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001A923 File Offset: 0x00018B23
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PlaySound(this.Host.GrowlSound);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001A941 File Offset: 0x00018B41
		public override void Update(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FindTarget();
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0001A950 File Offset: 0x00018B50
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

		// Token: 0x04000202 RID: 514
		private TimeSince timeSinceLastThreatCalc = 0f;
	}
}
