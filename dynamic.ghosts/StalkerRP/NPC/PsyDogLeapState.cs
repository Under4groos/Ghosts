using System;
using System.Runtime.CompilerServices;

namespace StalkerRP.NPC
{
	// Token: 0x020000D5 RID: 213
	public class PsyDogLeapState : NPCState<PsyDogNPC>
	{
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x00027D56 File Offset: 0x00025F56
		private float MeleeRange
		{
			get
			{
				return 350f;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x00027D5D File Offset: 0x00025F5D
		private float Cooldown
		{
			get
			{
				return 20f;
			}
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00027D64 File Offset: 0x00025F64
		public PsyDogLeapState(PsyDogNPC host) : base(host)
		{
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00027D70 File Offset: 0x00025F70
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasJumped = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasLanded = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.AttackType, 0);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Attacking, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAttackPos();
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00027DCC File Offset: 0x00025FCC
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Attacking, false);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00027DE4 File Offset: 0x00025FE4
		public override void Update(float deltaTime)
		{
			if (!this.HasJumped)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.Velocity = this.Host.Rotation.Forward * this.Host.GetWishSpeed();
			}
			if (!this.HasLanded && this.HasJumped && this.Host.GroundEntity != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.HasLanded = true;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SetAnimParameter(NPCAnimParameters.OnGround, true);
				return;
			}
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00027E6C File Offset: 0x0002606C
		public override void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData)
		{
			if (name.Equals("NPCJumpEvent"))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DoJump();
				return;
			}
			if (name.Equals("NPCJumpFinished"))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnLanded();
				return;
			}
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00027EA0 File Offset: 0x000260A0
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoMeleeAttack();
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00027EB0 File Offset: 0x000260B0
		private void DoJump()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.GroundEntity = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.OnGround, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Velocity += this.Host.Rotation.Forward * 150f + Vector3.Up * 220f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Position += Vector3.Up * 2f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasJumped = true;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00027F66 File Offset: 0x00026166
		private void OnLanded()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SM.SetState(this.Host.Damaged ? this.Host.FleeState : this.Host.ChargeState);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00027FA2 File Offset: 0x000261A2
		private void DoMeleeAttack()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PerformTraceAttack(this.AttackPos, this.Host.AttackDamage, 50f, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAttackPos();
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00027FDC File Offset: 0x000261DC
		private void SetAttackPos()
		{
			Vector3 dir = (base.Target.WorldSpaceBounds.Center - this.Host.Position).Normal;
			Vector3 attackPos = this.Host.Position + dir * this.MeleeRange;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AttackPos = attackPos;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00028040 File Offset: 0x00026240
		public bool CanDoLeap()
		{
			if (this.HasEntered && this.TimeSinceExited < this.Cooldown)
			{
				return false;
			}
			if (this.Host.TargetingComponent.DistanceToTarget > this.MeleeRange)
			{
				return false;
			}
			if (!this.Host.TargetingComponent.CanSeeTarget())
			{
				return false;
			}
			Vector3 dir = (base.Target.Position.WithZ(0f) - this.Host.Position.WithZ(0f)).Normal;
			Vector3 lookDir = this.Host.Rotation.Forward.WithZ(0f);
			return dir.Dot(lookDir) >= 0.8f;
		}

		// Token: 0x040002F0 RID: 752
		private Vector3 AttackPos;

		// Token: 0x040002F1 RID: 753
		private bool HasJumped;

		// Token: 0x040002F2 RID: 754
		private bool HasLanded;
	}
}
