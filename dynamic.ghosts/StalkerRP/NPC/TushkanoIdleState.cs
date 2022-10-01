using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000E4 RID: 228
	public class TushkanoIdleState : NPCState<TushkanoNPC>
	{
		// Token: 0x060009D4 RID: 2516 RVA: 0x0002A83B File Offset: 0x00028A3B
		public TushkanoIdleState(TushkanoNPC host) : base(host)
		{
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0002A854 File Offset: 0x00028A54
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PlaySound(this.Host.GrowlSound);
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0002A872 File Offset: 0x00028A72
		public override void Update(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FindTarget();
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0002A880 File Offset: 0x00028A80
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

		// Token: 0x04000326 RID: 806
		private TimeSince timeSinceLastThreatCalc = 0f;
	}
}
