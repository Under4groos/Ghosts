using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x02000096 RID: 150
	public class ControllerMeleeState : NPCState<ControllerNPC>
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0001EBB9 File Offset: 0x0001CDB9
		private float AttackRange
		{
			get
			{
				return 90f;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0001EBC0 File Offset: 0x0001CDC0
		private float AttackDamage
		{
			get
			{
				return 30f;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x0001EBC7 File Offset: 0x0001CDC7
		private float StandingAttackTime
		{
			get
			{
				return 1.5f;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x0001EBCE File Offset: 0x0001CDCE
		private float AttackDelay
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x0001EBD5 File Offset: 0x0001CDD5
		private float LeashRange
		{
			get
			{
				return 400f;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x0001EBDC File Offset: 0x0001CDDC
		private float DamageThresholdForStagger
		{
			get
			{
				return 40f;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x0001EBE3 File Offset: 0x0001CDE3
		[Description("If its been this many seconds and we haven't attacked, go back to chase state.")]
		private float NoAttackTimeLimit
		{
			get
			{
				return 3f;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x0001EBEA File Offset: 0x0001CDEA
		private float TimeSinceCanAttack
		{
			get
			{
				return this.TimeSinceLastAttack - this.AttackDelay;
			}
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001EBFE File Offset: 0x0001CDFE
		public ControllerMeleeState(ControllerNPC host) : base(host)
		{
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0001EC28 File Offset: 0x0001CE28
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasTarget", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("eAttackType", 0);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Speed = this.Host.Resource.RunSpeed;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DamageTakenSinceEntered = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsAttacking = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceLastAttack = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.NumAttacks = 0;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001ECEC File Offset: 0x0001CEEC
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasTarget", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001ED4C File Offset: 0x0001CF4C
		public override void Update(float deltaTime)
		{
			if (!this.Host.TargetingComponent.ValidateTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.TargetingComponent.RecalculateThreatLevelsByDistance(false);
				if (!this.Host.TargetingComponent.ValidateTarget())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.SM.SetState(this.Host.IdleState);
					return;
				}
			}
			if (this.TimeSinceCanAttack > this.NoAttackTimeLimit)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.ChaseState);
				return;
			}
			float distance = this.Host.TargetingComponent.DistanceToTarget;
			if (distance > this.LeashRange)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.ChaseState);
				return;
			}
			if (this.timeSinceLastThreatCalc > 3f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.TargetingComponent.RecalculateThreatLevelsByDistance(false);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.timeSinceLastThreatCalc = 0f;
			}
			if (distance < this.AttackRange || this.IsAttacking)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.Steer.Target = this.Host.Position;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SetAnimParameter("bHasPath", false);
				if (!this.IsAttacking)
				{
					this.Host.FaceTarget();
				}
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.Steer.Target = base.Target.Position;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SetAnimParameter("bHasPath", true);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SetAnimParameter("bRunning", true);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SetAnimParameter("bAttacking", false);
			}
			if (this.CanAttackTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.BeginAttack();
				return;
			}
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001EF30 File Offset: 0x0001D130
		public override void TakeDamage(DamageInfo info)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DamageTakenSinceEntered += info.Damage;
			if (this.DamageTakenSinceEntered > this.DamageThresholdForStagger && this.Host.StaggerState.CanStagger())
			{
				this.Host.SM.SetState(this.Host.StaggerState);
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001EF91 File Offset: 0x0001D191
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoTraceAttack(intData);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001EFA0 File Offset: 0x0001D1A0
		private bool CanAttackTarget()
		{
			return !this.IsAttacking && ((this.TimeSinceLastAttack >= this.AttackDelay + this.StandingAttackTime || this.TimeSinceEntered < 1f) && this.Host.TargetingComponent.DistanceToTarget <= this.AttackRange);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001F000 File Offset: 0x0001D200
		private void BeginAttack()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceLastAttack = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AttackLocation = base.Target.Position + Vector3.Up * 40f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsAttacking = true;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001F070 File Offset: 0x0001D270
		private void DoTraceAttack(int attackNumber)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PerformTraceAttack(this.AttackLocation, this.AttackDamage, 50f, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.NumAttacks++;
			if (attackNumber > 1)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.IsAttacking = false;
			}
		}

		// Token: 0x04000248 RID: 584
		private bool IsAttacking;

		// Token: 0x04000249 RID: 585
		private TimeSince TimeSinceLastAttack = 0f;

		// Token: 0x0400024A RID: 586
		private Vector3 AttackLocation;

		// Token: 0x0400024B RID: 587
		private float DamageTakenSinceEntered;

		// Token: 0x0400024C RID: 588
		private int NumAttacks;

		// Token: 0x0400024D RID: 589
		private TimeSince timeSinceLastThreatCalc = 0f;
	}
}
