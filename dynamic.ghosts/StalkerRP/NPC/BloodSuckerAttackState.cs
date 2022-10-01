using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x02000074 RID: 116
	public class BloodSuckerAttackState : NPCState<BloodSuckerNPC>
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x00018FD5 File Offset: 0x000171D5
		private float AttackRange
		{
			get
			{
				return 175f;
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00018FDC File Offset: 0x000171DC
		public BloodSuckerAttackState(BloodSuckerNPC bloodSucker) : base(bloodSucker)
		{
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00018FF8 File Offset: 0x000171F8
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.offset = (float)(Rand.Int(0, 1) * 2 - 1);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasAttacked = false;
			if (Rand.Int(3) == 3)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.EmitAggroSound();
			}
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00019058 File Offset: 0x00017258
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttackLeft", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttackRight", false);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x000190B8 File Offset: 0x000172B8
		public override void Update(float deltaTime)
		{
			if (!this.Host.TargetingComponent.TryEnsureTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.TauntState);
				return;
			}
			if (this.TimeSinceEntered > 3f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.CircleState);
				return;
			}
			if (this.Host.TargetingComponent.DistanceToTarget < this.Host.UncloakRange && this.Host.IsFading)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.EndFade();
			}
			Vector3 dir = (base.Target.Position - this.Host.Position).Normal;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer.Target = base.Target.Position - dir * 35f + dir.Cross(Vector3.Up) * this.offset * 20f;
			if (this.Host.TargetingComponent.DistanceToTarget < this.AttackRange && this.TimeSinceLastAttack > 2f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DoAttackAnim();
				return;
			}
			if (this.TimeSinceLastAttack < 2f && this.TimeSinceLastAttack > 1f && this.HasAttacked)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.CircleState);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttackLeft", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttackRight", false);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x000192A4 File Offset: 0x000174A4
		private void DoAttackAnim()
		{
			if ((this.Host.Position - base.Target.Position).Normal.Dot(this.Host.Velocity.Cross(Vector3.Up)) < 0f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SetAnimParameter("bAttackLeft", true);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SetAnimParameter("bAttacking", true);
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SetAnimParameter("bAttackRight", true);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SetAnimParameter("bAttacking", true);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceLastAttack = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.attackPos = this.Host.Position + (base.Target.Position - this.Host.Position).Normal * this.AttackRange + Vector3.Up * 40f;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x000193C7 File Offset: 0x000175C7
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PerformTraceAttackOnTarget(this.Host.AttackDamage, 50f, this.AttackRange, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasAttacked = true;
		}

		// Token: 0x040001E7 RID: 487
		private TimeSince TimeSinceLastAttack = 0f;

		// Token: 0x040001E8 RID: 488
		private float offset;

		// Token: 0x040001E9 RID: 489
		private bool HasAttacked;

		// Token: 0x040001EA RID: 490
		private Vector3 attackPos;
	}
}
