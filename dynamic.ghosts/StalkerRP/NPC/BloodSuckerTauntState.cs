using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x02000078 RID: 120
	public class BloodSuckerTauntState : NPCState<BloodSuckerNPC>
	{
		// Token: 0x06000586 RID: 1414 RVA: 0x00019FF8 File Offset: 0x000181F8
		public BloodSuckerTauntState(BloodSuckerNPC bloodSucker) : base(bloodSucker)
		{
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0001A001 File Offset: 0x00018201
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bTaunting", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.EndFade();
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0001A03A File Offset: 0x0001823A
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bTaunting", false);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0001A054 File Offset: 0x00018254
		public override void Update(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.FaceTarget();
			if (this.TimeSinceEntered > 1.8f)
			{
				if (this.Host.TargetingComponent.ValidateTarget())
				{
					this.Host.SM.SetState(this.Host.CircleState);
					return;
				}
				this.Host.SM.SetState(this.Host.IdleState);
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0001A0CC File Offset: 0x000182CC
		public override void TakeDamage(DamageInfo info)
		{
			if (this.Host.TargetingComponent.ValidateTarget())
			{
				this.Host.SM.SetState(this.Host.CircleState);
				return;
			}
			this.Host.SM.SetState(this.Host.IdleState);
		}
	}
}
