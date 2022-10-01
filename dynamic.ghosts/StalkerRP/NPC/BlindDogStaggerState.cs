using System;
using System.Runtime.CompilerServices;

namespace StalkerRP.NPC
{
	// Token: 0x020000A3 RID: 163
	public class BlindDogStaggerState : NPCState<BlindDogNPC>
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x00021358 File Offset: 0x0001F558
		private float Cooldown
		{
			get
			{
				return 3f;
			}
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0002135F File Offset: 0x0001F55F
		public BlindDogStaggerState(BlindDogNPC host) : base(host)
		{
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00021368 File Offset: 0x0001F568
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Stagger, true);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00021380 File Offset: 0x0001F580
		public override void OnStateExited()
		{
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00021384 File Offset: 0x0001F584
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

		// Token: 0x0600074C RID: 1868 RVA: 0x000213F1 File Offset: 0x0001F5F1
		public bool CanBeStaggered()
		{
			return !this.IsActive && this.TimeSinceExited > this.Cooldown;
		}
	}
}
