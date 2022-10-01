using System;
using System.Runtime.CompilerServices;

namespace StalkerRP.NPC
{
	// Token: 0x020000AD RID: 173
	public class FleshStaggerState : NPCState<FleshNPC>
	{
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x000225ED File Offset: 0x000207ED
		private float Cooldown
		{
			get
			{
				return 3f;
			}
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x000225F4 File Offset: 0x000207F4
		public FleshStaggerState(FleshNPC host) : base(host)
		{
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x000225FD File Offset: 0x000207FD
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Stagger, true);
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00022615 File Offset: 0x00020815
		public override void OnStateExited()
		{
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00022618 File Offset: 0x00020818
		public override void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData)
		{
			if (name.Equals("NPCEndStaggerEvent"))
			{
				if (this.Host.TargetingComponent.ValidateTarget())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.SM.SetState(this.Host.CircleState);
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
			}
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00022685 File Offset: 0x00020885
		public bool CanBeStaggered()
		{
			return !this.IsActive && this.TimeSinceExited > this.Cooldown;
		}
	}
}
