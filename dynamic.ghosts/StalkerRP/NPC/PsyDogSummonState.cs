using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000D9 RID: 217
	public class PsyDogSummonState : NPCState<PsyDogNPC>
	{
		// Token: 0x06000965 RID: 2405 RVA: 0x00028AAC File Offset: 0x00026CAC
		public PsyDogSummonState(PsyDogNPC host) : base(host)
		{
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00028AC8 File Offset: 0x00026CC8
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.AttackType, 3);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Attacking, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IllusionSummonTime = 1f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IllusionSummonDelay = this.IllusionSummonTime / (float)this.Host.MaxIllusions;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceLastSummon = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasSummoned = false;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00028B50 File Offset: 0x00026D50
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Attacking, false);
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00028B68 File Offset: 0x00026D68
		public override void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData)
		{
			if (name.Equals("NPCAttackEventFinished"))
			{
				if (this.Host.Target != null)
				{
					this.Host.SM.SetState(this.Host.CircleState);
					return;
				}
				this.Host.SM.SetState(this.Host.IdleState);
			}
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00028BC8 File Offset: 0x00026DC8
		public override void Update(float deltaTime)
		{
			if (!this.Host.CanCreateIllusion())
			{
				return;
			}
			if (this.TimeSinceLastSummon > this.IllusionSummonDelay || !this.HasSummoned)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.CreateIllusion();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TimeSinceLastSummon = 0f;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.HasSummoned = true;
			}
		}

		// Token: 0x04000302 RID: 770
		private float IllusionSummonTime;

		// Token: 0x04000303 RID: 771
		private float IllusionSummonDelay;

		// Token: 0x04000304 RID: 772
		private TimeSince TimeSinceLastSummon = 0f;

		// Token: 0x04000305 RID: 773
		private bool HasSummoned;
	}
}
