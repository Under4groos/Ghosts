using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x02000076 RID: 118
	public class BloodSuckerIdleState : NPCState<BloodSuckerNPC>
	{
		// Token: 0x06000562 RID: 1378 RVA: 0x00019900 File Offset: 0x00017B00
		public BloodSuckerIdleState(BloodSuckerNPC bloodSucker) : base(bloodSucker)
		{
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0001991C File Offset: 0x00017B1C
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasTarget", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.EndFade();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.TargetingComponent.Reset();
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0001997A File Offset: 0x00017B7A
		public override void OnStateExited()
		{
			if (this.Host.EmitAggroSoundOnLeavingIdle)
			{
				this.Host.EmitAggroSound();
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00019994 File Offset: 0x00017B94
		public override void Update(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FindTarget();
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000199A4 File Offset: 0x00017BA4
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
				this.Host.SM.SetState(this.Host.CircleState);
				return;
			}
		}

		// Token: 0x040001F1 RID: 497
		private TimeSince timeSinceLastThreatCalc = 0f;
	}
}
