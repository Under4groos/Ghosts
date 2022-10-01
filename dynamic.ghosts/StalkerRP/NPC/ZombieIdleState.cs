using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000EA RID: 234
	public class ZombieIdleState : NPCState<ZombieNPC>
	{
		// Token: 0x06000A0C RID: 2572 RVA: 0x0002B528 File Offset: 0x00029728
		public ZombieIdleState(ZombieNPC zombie) : base(zombie)
		{
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0002B541 File Offset: 0x00029741
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bIdle", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.TargetingComponent.Reset();
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0002B57F File Offset: 0x0002977F
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bIdle", false);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0002B597 File Offset: 0x00029797
		public override void Update(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FindTarget();
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0002B5A4 File Offset: 0x000297A4
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

		// Token: 0x06000A11 RID: 2577 RVA: 0x0002B621 File Offset: 0x00029821
		public override void OnCrit(DamageInfo info)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SM.SetState(this.Host.FakeDeathState);
		}

		// Token: 0x04000335 RID: 821
		private TimeSince timeSinceLastThreatCalc = 0f;
	}
}
