using System;
using System.Runtime.CompilerServices;

namespace StalkerRP.NPC
{
	// Token: 0x020000CE RID: 206
	public class PsyDogCreatedState : NPCState<PsyDogNPC>
	{
		// Token: 0x060008F6 RID: 2294 RVA: 0x000272D1 File Offset: 0x000254D1
		public PsyDogCreatedState(PsyDogNPC host) : base(host)
		{
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x000272DC File Offset: 0x000254DC
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Falling, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasLanded = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FinishedJump = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.GroundEntity = null;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00027339 File Offset: 0x00025539
		public override void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData)
		{
			if (name.Equals("NPCJumpFinished"))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.FinishedJump = true;
				return;
			}
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00027358 File Offset: 0x00025558
		public override void Update(float deltaTime)
		{
			if (!this.HasLanded && this.Host.GroundEntity != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SetAnimParameter(NPCAnimParameters.Falling, false);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.HasLanded = true;
				return;
			}
			if (this.HasLanded && this.TimeSinceEntered > 0.5f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.ChargeState);
				return;
			}
		}

		// Token: 0x040002E3 RID: 739
		private bool HasLanded;

		// Token: 0x040002E4 RID: 740
		private bool FinishedJump;
	}
}
