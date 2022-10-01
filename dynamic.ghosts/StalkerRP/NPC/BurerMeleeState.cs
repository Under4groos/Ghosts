using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x02000085 RID: 133
	public class BurerMeleeState : NPCState<BurerNPC>
	{
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0001BB6D File Offset: 0x00019D6D
		private float AttackRange
		{
			get
			{
				return 90f;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x0001BB74 File Offset: 0x00019D74
		private float AttackDamage
		{
			get
			{
				return 35f;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x0001BB7B File Offset: 0x00019D7B
		private float StandingAttackTime
		{
			get
			{
				return 1.5f;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x0001BB82 File Offset: 0x00019D82
		private float AttackDelay
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0001BB89 File Offset: 0x00019D89
		private float LeashRange
		{
			get
			{
				return 400f;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0001BB90 File Offset: 0x00019D90
		private float DamageTakenShieldThreshold
		{
			get
			{
				return 40f;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0001BB97 File Offset: 0x00019D97
		[Description("If its been this many seconds and we haven't attacked, go back to chase state.")]
		private float NoAttackTimeLimit
		{
			get
			{
				return 3f;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0001BB9E File Offset: 0x00019D9E
		private float TimeSinceCanAttack
		{
			get
			{
				return this.TimeSinceLastAttack - this.AttackDelay;
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001BBB2 File Offset: 0x00019DB2
		public BurerMeleeState(BurerNPC host) : base(host)
		{
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001BBDC File Offset: 0x00019DDC
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bRunning", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasTarget", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("eAttackType", 0);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Speed = this.Host.Resource.RunSpeed;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsAttacking = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DamageTakenSinceEntered = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceLastAttack = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.NumAttacks = 0;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001BCB4 File Offset: 0x00019EB4
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bRunning", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasTarget", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001BD2C File Offset: 0x00019F2C
		public override void Update(float deltaTime)
		{
			if (!this.Host.TargetingComponent.TryEnsureTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
				return;
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

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001BEE8 File Offset: 0x0001A0E8
		public override void TakeDamage(DamageInfo info)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DamageTakenSinceEntered += info.Damage;
			if (this.NumAttacks > 0)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.FleeState);
				return;
			}
			if (this.DamageTakenSinceEntered < this.DamageTakenShieldThreshold || this.IsAttacking)
			{
				return;
			}
			if (this.Host.ShieldState.CanDoShield())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.ShieldState);
				return;
			}
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001BF82 File Offset: 0x0001A182
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoTraceAttack(intData);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001BF90 File Offset: 0x0001A190
		private bool CanAttackTarget()
		{
			return !this.IsAttacking && ((this.TimeSinceLastAttack >= this.AttackDelay + this.StandingAttackTime || this.TimeSinceEntered < 1f) && this.Host.TargetingComponent.DistanceToTarget <= this.AttackRange);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001BFF0 File Offset: 0x0001A1F0
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

		// Token: 0x060005FB RID: 1531 RVA: 0x0001C060 File Offset: 0x0001A260
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

		// Token: 0x04000217 RID: 535
		private bool IsAttacking;

		// Token: 0x04000218 RID: 536
		private TimeSince TimeSinceLastAttack = 0f;

		// Token: 0x04000219 RID: 537
		private Vector3 AttackLocation;

		// Token: 0x0400021A RID: 538
		private float DamageTakenSinceEntered;

		// Token: 0x0400021B RID: 539
		private int NumAttacks;

		// Token: 0x0400021C RID: 540
		private TimeSince timeSinceLastThreatCalc = 0f;
	}
}
