using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000D2 RID: 210
	public class PsyDogIdleState : NPCState<PsyDogNPC>
	{
		// Token: 0x06000910 RID: 2320 RVA: 0x00027A2B File Offset: 0x00025C2B
		public PsyDogIdleState(PsyDogNPC host) : base(host)
		{
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00027A44 File Offset: 0x00025C44
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.DissipateIllusions();
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00027A56 File Offset: 0x00025C56
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PlaySound(this.Host.GrowlSound);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00027A74 File Offset: 0x00025C74
		public override void Update(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FindTarget();
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00027A84 File Offset: 0x00025C84
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

		// Token: 0x040002ED RID: 749
		private TimeSince timeSinceLastThreatCalc = 0f;
	}
}
