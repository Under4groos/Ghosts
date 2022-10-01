using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000C9 RID: 201
	public class GiantIdleState : NPCState<GiantNPC>
	{
		// Token: 0x060008B6 RID: 2230 RVA: 0x00026521 File Offset: 0x00024721
		public GiantIdleState(GiantNPC host) : base(host)
		{
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0002653A File Offset: 0x0002473A
		public override void OnStateEntered()
		{
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0002653C File Offset: 0x0002473C
		public override void OnStateExited()
		{
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0002653E File Offset: 0x0002473E
		public override void Update(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FindTarget();
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0002654C File Offset: 0x0002474C
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

		// Token: 0x040002D9 RID: 729
		private TimeSince timeSinceLastThreatCalc = 0f;
	}
}
