using System;
using System.Runtime.CompilerServices;

namespace StalkerRP.NPC
{
	// Token: 0x020000D8 RID: 216
	public class PsyDogStaggerState : NPCState<PsyDogNPC>
	{
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x000289E1 File Offset: 0x00026BE1
		private float Cooldown
		{
			get
			{
				return 3f;
			}
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x000289E8 File Offset: 0x00026BE8
		public PsyDogStaggerState(PsyDogNPC host) : base(host)
		{
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x000289F1 File Offset: 0x00026BF1
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Stagger, true);
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00028A09 File Offset: 0x00026C09
		public override void OnStateExited()
		{
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00028A0C File Offset: 0x00026C0C
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

		// Token: 0x06000964 RID: 2404 RVA: 0x00028A79 File Offset: 0x00026C79
		public bool CanBeStaggered()
		{
			return !this.IsActive && !this.Host.DodgeState.IsActive && this.TimeSinceExited > this.Cooldown;
		}
	}
}
