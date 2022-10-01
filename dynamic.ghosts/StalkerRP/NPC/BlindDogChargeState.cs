using System;
using System.Runtime.CompilerServices;

namespace StalkerRP.NPC
{
	// Token: 0x0200009A RID: 154
	public class BlindDogChargeState : NPCState<BlindDogNPC>
	{
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0001FBC6 File Offset: 0x0001DDC6
		public float ForceChargeRange
		{
			get
			{
				return 300f;
			}
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001FBCD File Offset: 0x0001DDCD
		public BlindDogChargeState(BlindDogNPC host) : base(host)
		{
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001FBD8 File Offset: 0x0001DDD8
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, true);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001FC26 File Offset: 0x0001DE26
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.HasPath, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Moving, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001FC68 File Offset: 0x0001DE68
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
		}
	}
}
