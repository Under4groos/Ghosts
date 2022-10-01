using System;
using System.Runtime.CompilerServices;

namespace StalkerRP.NPC
{
	// Token: 0x020000C7 RID: 199
	public class PseudoDogStaggerState : NPCState<PseudoDogNPC>
	{
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x00025FDD File Offset: 0x000241DD
		private float Cooldown
		{
			get
			{
				return 3f;
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00025FE4 File Offset: 0x000241E4
		public PseudoDogStaggerState(PseudoDogNPC host) : base(host)
		{
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00025FED File Offset: 0x000241ED
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Stagger, true);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00026005 File Offset: 0x00024205
		public override void OnStateExited()
		{
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00026008 File Offset: 0x00024208
		public override void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData)
		{
			if (name.Equals("NPCEndStaggerEvent"))
			{
				if (this.Host.TargetingComponent.ValidateTarget())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.SM.SetState(this.Host.FleeState);
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
			}
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00026075 File Offset: 0x00024275
		public bool CanBeStaggered()
		{
			return !this.IsActive && !this.Host.DodgeState.IsActive && this.TimeSinceExited > this.Cooldown;
		}
	}
}
