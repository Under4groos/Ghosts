using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000E8 RID: 232
	public class ZombieChaseState : NPCState<ZombieNPC>
	{
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x0002B0BB File Offset: 0x000292BB
		private float AttackDelay
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x0002B0C2 File Offset: 0x000292C2
		private bool IsAttacking
		{
			get
			{
				return this.TimeSinceLastAttack <= this.StandingAttackTime;
			}
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0002B0DA File Offset: 0x000292DA
		public ZombieChaseState(ZombieNPC zombie) : base(zombie)
		{
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0002B10E File Offset: 0x0002930E
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PlayAlertSound();
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0002B14B File Offset: 0x0002934B
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", false);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0002B174 File Offset: 0x00029374
		public override void Update(float deltaTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Speed = this.Host.GetWishSpeed();
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
			if (this.Host.TargetingComponent.DistanceToTarget < this.Host.AttackRange || this.IsAttacking)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.Steer.Target = this.Host.Position;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SetAnimParameter("bHasPath", false);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.FaceTarget();
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.Steer.Target = base.Target.Position;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SetAnimParameter("bHasPath", true);
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

		// Token: 0x06000A02 RID: 2562 RVA: 0x0002B2D9 File Offset: 0x000294D9
		public override void OnCrit(DamageInfo info)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SM.SetState(this.Host.FakeDeathState);
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0002B2FB File Offset: 0x000294FB
		public override void TakeDamage(DamageInfo info)
		{
			if (!this.Host.Aggravated)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.Aggravated = true;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SetAnimParameter("bAggrevated", true);
			}
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0002B331 File Offset: 0x00029531
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoTraceAttack();
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0002B340 File Offset: 0x00029540
		private bool CanAttackTarget()
		{
			return !this.IsAttacking && (this.TimeSinceLastAttack >= this.AttackDelay + this.StandingAttackTime && this.Host.TargetingComponent.DistanceToTarget <= this.Host.AttackRange);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0002B394 File Offset: 0x00029594
		private void BeginAttack()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceLastAttack = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AttackLocation = base.Target.Position + Vector3.Up * 40f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PlayAttackSound();
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0002B408 File Offset: 0x00029608
		private void DoTraceAttack()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PerformTraceAttack(this.AttackLocation, this.Host.AttackDamage, 50f, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", false);
		}

		// Token: 0x04000331 RID: 817
		private float StandingAttackTime = 1.5f;

		// Token: 0x04000332 RID: 818
		private TimeSince TimeSinceLastAttack = 0f;

		// Token: 0x04000333 RID: 819
		private Vector3 AttackLocation;

		// Token: 0x04000334 RID: 820
		private TimeSince timeSinceLastThreatCalc = 10f;
	}
}
