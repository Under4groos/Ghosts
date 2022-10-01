using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000C8 RID: 200
	public class GiantChaseState : NPCState<GiantNPC>
	{
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x000260A8 File Offset: 0x000242A8
		private float AttackHaltDuration
		{
			get
			{
				return 1.3f;
			}
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x000260B0 File Offset: 0x000242B0
		public GiantChaseState(GiantNPC host) : base(host)
		{
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00026100 File Offset: 0x00024300
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("eAttackType", 0);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsAttacking = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PlaySound(this.Host.AggroSound);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00026176 File Offset: 0x00024376
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x000261B5 File Offset: 0x000243B5
		private float breathingSoundDelayRange
		{
			get
			{
				return 2f;
			}
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x000261BC File Offset: 0x000243BC
		public override void Update(float deltaTime)
		{
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
			if (this.timeSinceLastBreathingSound > this.breathingSoundDelay)
			{
				this.DoBreathingSound();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Speed = this.Host.GetWishSpeed();
			if (this.TimeSinceLastAttack > this.AttackHaltDuration)
			{
				this.SetSteerTarget();
			}
			if (this.Host.TargetingComponent.CanSeeTarget())
			{
				if (this.Host.StompState.CanStomp())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.SM.SetState(this.Host.StompState);
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TryAttack();
			}
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x000262D6 File Offset: 0x000244D6
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoAttack();
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x000262E4 File Offset: 0x000244E4
		private void SetSteerTarget()
		{
			Vector3 targetPos = this.Host.Target.Position;
			Vector3 dir = (this.Host.Position - targetPos).Normal;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer.Target = targetPos + dir * this.Host.AttackRange * 0.8f;
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00026354 File Offset: 0x00024554
		private void TryAttack()
		{
			if (this.Host.TargetingComponent.DistanceToTarget < this.Host.AttackRange)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SetAnimParameter("bAttacking", true);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.IsAttacking = true;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TimeSinceLastAttack = 0f;
				if (!this.IsAttacking)
				{
					this.AttackRotation = Rotation.LookAt((base.Target.Position - this.Host.Position).WithZ(0f).Normal);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.FaceTarget();
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsAttacking = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", false);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00026430 File Offset: 0x00024630
		private void DoAttack()
		{
			Vector3 endPos = (this.Host.Target.WorldSpaceBounds.Center - this.Host.EyePosition).Normal * this.Host.AttackRange * 1.1f + this.Host.EyePosition;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PerformTraceAttack(endPos, this.Host.AttackDamage, this.Host.AttackForce, 0f);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x000264C8 File Offset: 0x000246C8
		private void DoBreathingSound()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PlaySound(this.Host.BreathingSounds);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastBreathingSound = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.breathingSoundDelay = Rand.Float(1f, this.breathingSoundDelayRange);
		}

		// Token: 0x040002D3 RID: 723
		private bool IsAttacking;

		// Token: 0x040002D4 RID: 724
		private TimeSince TimeSinceLastAttack = 0f;

		// Token: 0x040002D5 RID: 725
		private Rotation AttackRotation;

		// Token: 0x040002D6 RID: 726
		private TimeSince timeSinceLastThreatCalc = 0f;

		// Token: 0x040002D7 RID: 727
		private TimeSince timeSinceLastBreathingSound = 0f;

		// Token: 0x040002D8 RID: 728
		private float breathingSoundDelay = 1f;
	}
}
