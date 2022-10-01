using System;
using System.Runtime.CompilerServices;

namespace StalkerRP.NPC
{
	// Token: 0x02000081 RID: 129
	public class BoarStaggerState : NPCState<BoarNPC>
	{
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0001B3D6 File Offset: 0x000195D6
		private float Cooldown
		{
			get
			{
				return 3f;
			}
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0001B3DD File Offset: 0x000195DD
		public BoarStaggerState(BoarNPC host) : base(host)
		{
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0001B3E6 File Offset: 0x000195E6
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Stagger, true);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0001B3FE File Offset: 0x000195FE
		public override void OnStateExited()
		{
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001B400 File Offset: 0x00019600
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

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001B46D File Offset: 0x0001966D
		public bool CanBeStaggered()
		{
			return !this.IsActive && this.TimeSinceExited > this.Cooldown;
		}
	}
}
