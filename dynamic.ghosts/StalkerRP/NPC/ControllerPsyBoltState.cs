using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x02000098 RID: 152
	public class ControllerPsyBoltState : NPCState<ControllerNPC>
	{
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0001F7A3 File Offset: 0x0001D9A3
		private float Cooldown
		{
			get
			{
				return 3f;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0001F7AA File Offset: 0x0001D9AA
		private float ZoomTime
		{
			get
			{
				return 1.4f;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0001F7B1 File Offset: 0x0001D9B1
		private float TargetFOV
		{
			get
			{
				return 60f;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x0001F7B8 File Offset: 0x0001D9B8
		private float DamageThresholdForStagger
		{
			get
			{
				return 60f;
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001F7BF File Offset: 0x0001D9BF
		public ControllerPsyBoltState(ControllerNPC host) : base(host)
		{
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001F7C8 File Offset: 0x0001D9C8
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("eAttackType", 1);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PsyBoltTarget = base.Target;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsDoingPsyAttack = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DamageTakenSinceEntered = 0f;
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001F83F File Offset: 0x0001DA3F
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", false);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0001F858 File Offset: 0x0001DA58
		public override void Update(float deltaTime)
		{
			if (!this.Host.TargetingComponent.ValidateTarget(this.PsyBoltTarget))
			{
				Player p = this.PsyBoltTarget as Player;
				if (p != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.CancelPsyBolt(To.Single(p));
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
				return;
			}
			if (!this.CanFinishPsyBolt())
			{
				Player p2 = this.PsyBoltTarget as Player;
				if (p2 != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.CancelPsyBolt(To.Single(p2));
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.ChaseState);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.FaceTarget();
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0001F921 File Offset: 0x0001DB21
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			if (intData == 0)
			{
				this.BeginPsyBoltAttack();
			}
			if (intData == 1)
			{
				this.BeginZoomInEffect();
			}
			if (intData == 2)
			{
				this.FinishPsyBolt();
			}
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0001F940 File Offset: 0x0001DB40
		public override void TakeDamage(DamageInfo info)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DamageTakenSinceEntered += info.Damage;
			if (this.DamageTakenSinceEntered > this.DamageThresholdForStagger && !this.IsDoingPsyAttack && this.Host.StaggerState.CanStagger())
			{
				Player p = this.PsyBoltTarget as Player;
				if (p != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.CancelPsyBolt(To.Single(p));
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.StaggerState);
			}
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0001F9D4 File Offset: 0x0001DBD4
		private void BeginPsyBoltAttack()
		{
			Player p = this.PsyBoltTarget as Player;
			if (p != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.DoPsyBoltFX(To.Single(p));
			}
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001FA08 File Offset: 0x0001DC08
		private void BeginZoomInEffect()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsDoingPsyAttack = true;
			StalkerPlayer player = base.Target as StalkerPlayer;
			if (player != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.DoZoomInEffect(player, this.ZoomTime, this.TargetFOV);
			}
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001FA50 File Offset: 0x0001DC50
		private void FinishPsyBolt()
		{
			PsyHealthComponent psyHealth;
			if (base.Target.Components.TryGet<PsyHealthComponent>(out psyHealth, false))
			{
				psyHealth.TakeDamage(this.Host.PsyBoltDamage);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SM.SetState(this.Host.ChaseState);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001FAA3 File Offset: 0x0001DCA3
		private bool CanFinishPsyBolt()
		{
			return this.IsDoingPsyAttack || this.Host.CanSee(this.PsyBoltTarget, true);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0001FAC8 File Offset: 0x0001DCC8
		public bool CanDoPsyBolt()
		{
			return this.TimeSinceExited > this.Cooldown && this.Host.TargetingComponent.DistanceToTarget >= this.Host.PsyBoltMinRange && this.Host.TargetingComponent.DistanceToTarget <= this.Host.PsyBoltMaxRange;
		}

		// Token: 0x04000257 RID: 599
		private Entity PsyBoltTarget;

		// Token: 0x04000258 RID: 600
		private bool IsDoingPsyAttack;

		// Token: 0x04000259 RID: 601
		private float DamageTakenSinceEntered;
	}
}
