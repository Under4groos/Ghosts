using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x0200008A RID: 138
	public class BurerShieldState : NPCState<BurerNPC>
	{
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0001CEBD File Offset: 0x0001B0BD
		private float MinShieldDuration
		{
			get
			{
				return 4f;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x0001CEC4 File Offset: 0x0001B0C4
		private float CoolDown
		{
			get
			{
				return 2f;
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001CECB File Offset: 0x0001B0CB
		public BurerShieldState(BurerNPC host) : base(host)
		{
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001CED4 File Offset: 0x0001B0D4
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bShielding", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ShieldDuration = this.MinShieldDuration;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001CEFD File Offset: 0x0001B0FD
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bShielding", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.HitboxSet = "default";
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001CF2A File Offset: 0x0001B12A
		public override void TakeDamage(DamageInfo info)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ShieldDuration = MathF.Max(this.TimeSinceEntered + 1f, this.MinShieldDuration);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001CF54 File Offset: 0x0001B154
		public override void Update(float deltaTime)
		{
			if (this.TimeSinceEntered > 0.2f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.HitboxSet = "psyshield";
			}
			if (!this.Host.TargetingComponent.ValidateTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
				return;
			}
			if (this.TimeSinceEntered > this.ShieldDuration)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.ChaseState);
			}
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001CFEE File Offset: 0x0001B1EE
		public bool CanDoShield()
		{
			return this.TimeSinceExited >= this.CoolDown;
		}

		// Token: 0x04000231 RID: 561
		private float ShieldDuration;
	}
}
