using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000D6 RID: 214
	public class PsyDogMeleeState : NPCState<PsyDogNPC>
	{
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x00028109 File Offset: 0x00026309
		private float MeleeRange
		{
			get
			{
				return 100f;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x00028110 File Offset: 0x00026310
		private float MeleeCooldown
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00028117 File Offset: 0x00026317
		public PsyDogMeleeState(PsyDogNPC host) : base(host)
		{
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00028130 File Offset: 0x00026330
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasAttacked = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsAttacking = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceLastAttack = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.AttackType, 1);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Attacking, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAttackPos();
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x000281A1 File Offset: 0x000263A1
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Attacking, false);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x000281BC File Offset: 0x000263BC
		public override void Update(float deltaTime)
		{
			if (!this.Host.TargetingComponent.ValidateTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.TargetingComponent.RecalculateThreatLevelsByDistance(false);
				if (this.Host.TargetingComponent.ValidateTarget())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.SM.SetState(this.Host.Damaged ? this.Host.FleeState : this.Host.CircleState);
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
				return;
			}
			else
			{
				if (this.IsAttacking)
				{
					return;
				}
				bool canMelee = this.CanDoMelee();
				if (this.HasAttacked && this.TimeSinceLastAttack > this.MeleeCooldown && !canMelee)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.SM.SetState(this.Host.Damaged ? this.Host.FleeState : this.Host.ChargeState);
					return;
				}
				if (this.TimeSinceLastAttack > this.MeleeCooldown && canMelee)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.IsAttacking = true;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.SetAnimParameter(NPCAnimParameters.Attacking, true);
				}
				return;
			}
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00028304 File Offset: 0x00026504
		public override void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData)
		{
			if (name.Equals("NPCAttackEventFinished"))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnFinishedAttack();
			}
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0002831E File Offset: 0x0002651E
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoMeleeAttack();
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0002832B File Offset: 0x0002652B
		private void OnFinishedAttack()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsAttacking = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceLastAttack = 0f;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00028350 File Offset: 0x00026550
		private void DoMeleeAttack()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PerformTraceAttack(this.AttackPos, this.Host.AttackDamage, 50f, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAttackPos();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasAttacked = true;
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x000283A0 File Offset: 0x000265A0
		private void SetAttackPos()
		{
			Vector3 dir = (base.Target.WorldSpaceBounds.Center - this.Host.Position).Normal;
			Vector3 attackPos = this.Host.Position + dir * this.MeleeRange;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AttackPos = attackPos;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00028402 File Offset: 0x00026602
		public bool CanDoMelee()
		{
			return this.Host.TargetingComponent.DistanceToTarget <= this.MeleeRange && this.Host.TargetingComponent.CanSeeTarget();
		}

		// Token: 0x040002F3 RID: 755
		private TimeSince TimeSinceLastAttack = 0f;

		// Token: 0x040002F4 RID: 756
		private Vector3 AttackPos;

		// Token: 0x040002F5 RID: 757
		private bool HasAttacked;

		// Token: 0x040002F6 RID: 758
		private bool IsAttacking;
	}
}
