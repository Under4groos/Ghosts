using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000CC RID: 204
	public class PsyDogChargeState : NPCState<PsyDogNPC>
	{
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x00026D1C File Offset: 0x00024F1C
		private float PsyAttackCooldown
		{
			get
			{
				return 20f;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x00026D23 File Offset: 0x00024F23
		public float ForceChargeRange
		{
			get
			{
				return 300f;
			}
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00026D2A File Offset: 0x00024F2A
		public PsyDogChargeState(PsyDogNPC host) : base(host)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Event.Register(this);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00026D50 File Offset: 0x00024F50
		~PsyDogChargeState()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Event.Unregister(this);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00026D84 File Offset: 0x00024F84
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, true);
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00026DD2 File Offset: 0x00024FD2
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00026E11 File Offset: 0x00025011
		private bool CanDoPsyAttack()
		{
			return !this.HasDonePsyAttack || this.TimeSinceLastPsyAttack > this.PsyAttackCooldown;
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00026E30 File Offset: 0x00025030
		private bool ShouldFlee()
		{
			return this.Host.IllusionGroup.IsEmpty();
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00026E44 File Offset: 0x00025044
		public override void Update(float deltaTime)
		{
			if (!this.Host.TargetingComponent.TryEnsureTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
				return;
			}
			if (this.ShouldFlee())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.FleeState);
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
		}

		// Token: 0x040002DE RID: 734
		private TimeSince TimeSinceLastPsyAttack = 0f;

		// Token: 0x040002DF RID: 735
		private bool HasDonePsyAttack;
	}
}
