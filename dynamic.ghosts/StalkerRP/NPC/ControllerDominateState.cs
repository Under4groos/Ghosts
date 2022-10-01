using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x02000094 RID: 148
	public class ControllerDominateState : NPCState<ControllerNPC>
	{
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0001E8A1 File Offset: 0x0001CAA1
		private float CoolDown
		{
			get
			{
				return 6f;
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001E8A8 File Offset: 0x0001CAA8
		public ControllerDominateState(ControllerNPC host) : base(host)
		{
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001E8B4 File Offset: 0x0001CAB4
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("eAttackType", 2);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DomTarget = (base.Target as NPCBase);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PlaySound("controller.control");
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001E92A File Offset: 0x0001CB2A
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", false);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001E942 File Offset: 0x0001CB42
		public override void Update(float deltaTime)
		{
			if (!this.Host.TargetingComponent.ValidateTarget(this.DomTarget))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
				return;
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001E97D File Offset: 0x0001CB7D
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			if (this.CanFinishDominate())
			{
				this.DominateTarget();
			}
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001E98D File Offset: 0x0001CB8D
		private void DominateTarget()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.DominateTarget(this.DomTarget);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SM.SetState(this.Host.IdleState);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001E9C8 File Offset: 0x0001CBC8
		private bool CanFinishDominate()
		{
			return this.DomTarget.IsValid() && this.Host.CanSee(this.DomTarget, false) && !(this.DomTarget is IPsyCreature) && !this.DomTarget.Tags.Has(this.Host.ControllerDominateTag);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001EA2C File Offset: 0x0001CC2C
		public bool CanDominateTarget()
		{
			if (!base.Target.IsValid())
			{
				return false;
			}
			if (this.Host.DominatedCreaturesCount >= this.Host.MaxDominatedTargets)
			{
				return false;
			}
			if (this.TimeSinceExited < this.CoolDown && this.HasEntered)
			{
				return false;
			}
			Entity target = base.Target;
			return target is NPCBase && !(target is IPsyCreature) && !base.Target.Tags.Has(this.Host.ControllerDominateTag) && this.Host.CanSee(base.Target, false);
		}

		// Token: 0x04000246 RID: 582
		private NPCBase DomTarget;
	}
}
