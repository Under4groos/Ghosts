using System;
using System.Runtime.CompilerServices;

namespace StalkerRP.NPC
{
	// Token: 0x020000E9 RID: 233
	public class ZombieFakeDeathState : NPCState<ZombieNPC>
	{
		// Token: 0x06000A08 RID: 2568 RVA: 0x0002B457 File Offset: 0x00029657
		public ZombieFakeDeathState(ZombieNPC zombie) : base(zombie)
		{
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0002B460 File Offset: 0x00029660
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bFakingDeath", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0002B489 File Offset: 0x00029689
		public override void OnStateExited()
		{
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0002B48C File Offset: 0x0002968C
		public override void Update(float timeDelta)
		{
			if (this.TimeSinceEntered > this.Host.FakeDeathTime && this.Host.GetAnimParameterBool("bFakingDeath"))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SetAnimParameter("bFakingDeath", false);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.PlayReviveSound();
				return;
			}
			if (this.TimeSinceEntered > this.Host.FakeDeathTime + 3.2f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
			}
		}
	}
}
