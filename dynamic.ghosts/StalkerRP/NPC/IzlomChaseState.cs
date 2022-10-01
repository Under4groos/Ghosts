using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000AE RID: 174
	public class IzlomChaseState : NPCState<IzlomNPC>
	{
		// Token: 0x0600079A RID: 1946 RVA: 0x000226A4 File Offset: 0x000208A4
		public IzlomChaseState(IzlomNPC host) : base(host)
		{
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x000226C0 File Offset: 0x000208C0
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, true);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0002270E File Offset: 0x0002090E
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00022750 File Offset: 0x00020950
		public override void Update(float deltaTime)
		{
			if (this.timeSinceLastThreatCalc > 1f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.TargetingComponent.RecalculateThreatLevelsByDistance(false);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.timeSinceLastThreatCalc = 0f;
			}
			if (!this.Host.TargetingComponent.TryEnsureTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Speed = this.Host.GetWishSpeed();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer.Target = base.Target.Position;
			if (this.Host.MeleeState.CanDoMelee())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.MeleeState);
				return;
			}
		}

		// Token: 0x04000288 RID: 648
		private TimeSince timeSinceLastThreatCalc = 0f;
	}
}
