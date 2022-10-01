using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000BC RID: 188
	public class PseudoDogChargeState : NPCState<PseudoDogNPC>
	{
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x00024252 File Offset: 0x00022452
		private float PsyAttackCooldown
		{
			get
			{
				return 35f;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x00024259 File Offset: 0x00022459
		public float ForceChargeRange
		{
			get
			{
				return 300f;
			}
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00024260 File Offset: 0x00022460
		public PseudoDogChargeState(PseudoDogNPC host) : base(host)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Event.Register(this);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00024284 File Offset: 0x00022484
		~PseudoDogChargeState()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Event.Unregister(this);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x000242B8 File Offset: 0x000224B8
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, true);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00024306 File Offset: 0x00022506
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00024345 File Offset: 0x00022545
		private bool CanDoPsyAttack()
		{
			return !this.HasDonePsyAttack || this.TimeSinceLastPsyAttack > this.PsyAttackCooldown;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00024364 File Offset: 0x00022564
		public override void Update(float deltaTime)
		{
			if (!this.Host.TargetingComponent.TryEnsureTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Speed = this.Host.GetWishSpeed();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer.Target = base.Target.Position;
			if (this.Host.LeapState.CanDoLeap())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.LeapState);
				return;
			}
			if (this.Host.MeleeState.CanDoMelee())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.MeleeState);
				return;
			}
			if (this.CanDoPsyAttack())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TryRandomPsyAttack();
				return;
			}
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0002445C File Offset: 0x0002265C
		private void TryRandomPsyAttack()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceLastPsyAttack = 0f;
			if (this.Host.PsyWaveState.CanDoPsyWave() && Rand.Float(1f) > 0.85f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.HasDonePsyAttack = true;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.PsyWaveState);
			}
		}

		// Token: 0x040002AE RID: 686
		private TimeSince TimeSinceLastPsyAttack = 0f;

		// Token: 0x040002AF RID: 687
		private bool HasDonePsyAttack;
	}
}
