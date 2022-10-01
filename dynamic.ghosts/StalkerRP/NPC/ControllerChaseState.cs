using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x02000093 RID: 147
	public class ControllerChaseState : NPCState<ControllerNPC>
	{
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x0001E5ED File Offset: 0x0001C7ED
		private float DamageThresholdForStagger
		{
			get
			{
				return 60f;
			}
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001E5F4 File Offset: 0x0001C7F4
		public ControllerChaseState(ControllerNPC host) : base(host)
		{
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001E620 File Offset: 0x0001C820
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Speed = this.Host.Resource.CrippledSpeed;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DamageTakenSinceEntered = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastLaugh = 8f;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001E69D File Offset: 0x0001C89D
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001E6C8 File Offset: 0x0001C8C8
		public override void Update(float deltaTime)
		{
			if (this.timeSinceLastLaugh > 10f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.PlaySound("controller.laugh");
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.timeSinceLastLaugh = 0f;
			}
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
			if (this.Host.DominateState.CanDominateTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.DominateState);
				return;
			}
			if (this.Host.TargetingComponent.DistanceToTarget < 180f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.MeleeState);
				return;
			}
			if (this.Host.PsyBoltState.CanDoPsyBolt())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.PsyBoltState);
				return;
			}
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001E840 File Offset: 0x0001CA40
		public override void TakeDamage(DamageInfo info)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DamageTakenSinceEntered += info.Damage;
			if (this.DamageTakenSinceEntered > this.DamageThresholdForStagger && this.Host.StaggerState.CanStagger())
			{
				this.Host.SM.SetState(this.Host.StaggerState);
			}
		}

		// Token: 0x04000242 RID: 578
		private float DamageTakenSinceEntered;

		// Token: 0x04000243 RID: 579
		private TimeSince timeSinceLastLaugh;

		// Token: 0x04000244 RID: 580
		private TimeSince timeSinceLastThreatCalc = 0f;

		// Token: 0x04000245 RID: 581
		private TimeSince timeSinceLastSeen = 0f;
	}
}
