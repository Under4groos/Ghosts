using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x02000082 RID: 130
	public class BurerChaseState : NPCState<BurerNPC>
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0001B48C File Offset: 0x0001968C
		private float TelekinesisSearchRange
		{
			get
			{
				return 800f;
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0001B493 File Offset: 0x00019693
		public BurerChaseState(BurerNPC host) : base(host)
		{
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001B4AC File Offset: 0x000196AC
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasTarget", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Speed = this.Host.Resource.CrippledSpeed;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001B51A File Offset: 0x0001971A
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasTarget", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001B55C File Offset: 0x0001975C
		public override void Update(float deltaTime)
		{
			if (this.timeSinceLastThreatCalc > 1f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.TargetingComponent.RecalculateThreatLevelsByDistance(false);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.timeSinceLastThreatCalc = 0f;
			}
			if (!this.Host.TargetingComponent.TryEnsureTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer.Target = base.Target.Position;
			if (this.TimeSinceEntered < 1f)
			{
				return;
			}
			if (this.Host.TargetingComponent.DistanceToTarget < 180f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.MeleeState);
				return;
			}
			if (this.Host.TelekinesisState.CanDoAttack())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.TelekinesisState);
				return;
			}
			if (this.Host.PsyCrushState.CanDoCrushAttack())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.PsyCrushState);
				return;
			}
			if (this.Host.PsyWaveState.CanDoPsyWave())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.PsyWaveState);
				return;
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001B6E0 File Offset: 0x000198E0
		public override void TakeDamage(DamageInfo info)
		{
			if (!this.Host.ShieldState.CanDoShield())
			{
				return;
			}
			if (info.Flags.HasFlag(DamageFlags.Bullet))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.ShieldState);
			}
		}

		// Token: 0x04000212 RID: 530
		private TimeSince timeSinceLastThreatCalc = 0f;
	}
}
