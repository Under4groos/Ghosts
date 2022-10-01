using System;
using System.Runtime.CompilerServices;

namespace StalkerRP.NPC
{
	// Token: 0x020000C1 RID: 193
	public class PseuodoDogLeapState : NPCState<PseudoDogNPC>
	{
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x00024F65 File Offset: 0x00023165
		private float MeleeRange
		{
			get
			{
				return 350f;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x00024F6C File Offset: 0x0002316C
		private float Cooldown
		{
			get
			{
				return 20f;
			}
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00024F73 File Offset: 0x00023173
		public PseuodoDogLeapState(PseudoDogNPC host) : base(host)
		{
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00024F7C File Offset: 0x0002317C
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

		// Token: 0x06000859 RID: 2137 RVA: 0x00024FD8 File Offset: 0x000231D8
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Attacking, false);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00024FF0 File Offset: 0x000231F0
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

		// Token: 0x0600085B RID: 2139 RVA: 0x00025078 File Offset: 0x00023278
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

		// Token: 0x0600085C RID: 2140 RVA: 0x000250AC File Offset: 0x000232AC
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoMeleeAttack();
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x000250BC File Offset: 0x000232BC
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

		// Token: 0x0600085E RID: 2142 RVA: 0x00025172 File Offset: 0x00023372
		private void OnLanded()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SM.SetState(this.Host.Damaged ? this.Host.FleeState : this.Host.ChargeState);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x000251AE File Offset: 0x000233AE
		private void DoMeleeAttack()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PerformTraceAttack(this.AttackPos, this.Host.AttackDamage, 50f, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAttackPos();
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x000251E8 File Offset: 0x000233E8
		private void SetAttackPos()
		{
			Vector3 dir = (base.Target.WorldSpaceBounds.Center - this.Host.Position).Normal;
			Vector3 attackPos = this.Host.Position + dir * this.MeleeRange;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AttackPos = attackPos;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0002524C File Offset: 0x0002344C
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

		// Token: 0x040002BD RID: 701
		private Vector3 AttackPos;

		// Token: 0x040002BE RID: 702
		private bool HasJumped;

		// Token: 0x040002BF RID: 703
		private bool HasLanded;
	}
}
