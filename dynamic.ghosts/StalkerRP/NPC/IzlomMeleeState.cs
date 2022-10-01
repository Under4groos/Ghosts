using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000B0 RID: 176
	public class IzlomMeleeState : NPCState<IzlomNPC>
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x000228E1 File Offset: 0x00020AE1
		private float meleeRange
		{
			get
			{
				return 120f;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x000228E8 File Offset: 0x00020AE8
		private float meleeCooldown
		{
			get
			{
				return 0.1f;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x000228EF File Offset: 0x00020AEF
		private float meleeDamage
		{
			get
			{
				return 60f;
			}
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x000228F6 File Offset: 0x00020AF6
		public IzlomMeleeState(IzlomNPC host) : base(host)
		{
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00022910 File Offset: 0x00020B10
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.hasAttacked = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.isAttacking = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastAttack = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Attacking, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.AttackType, 1);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAttackPos();
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00022981 File Offset: 0x00020B81
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Attacking, false);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0002299C File Offset: 0x00020B9C
		public override void Update(float deltaTime)
		{
			if (!this.Host.TargetingComponent.TryEnsureTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
				return;
			}
			if (this.timeSinceLastAttack < 0.1f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.FaceTarget();
			}
			if (this.isAttacking)
			{
				return;
			}
			bool canMelee = this.CanDoMelee();
			if (this.hasAttacked && this.timeSinceLastAttack > this.meleeCooldown && !canMelee)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.ChaseState);
				return;
			}
			if (!this.isAttacking && this.timeSinceLastAttack > this.meleeCooldown && canMelee)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.isAttacking = true;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SetAnimParameter(NPCAnimParameters.Attacking, true);
			}
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00022A91 File Offset: 0x00020C91
		public override void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData)
		{
			if (name.Equals("NPCAttackEventFinished"))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnFinishedAttack();
			}
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00022AAB File Offset: 0x00020CAB
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoMeleeAttack();
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00022AB8 File Offset: 0x00020CB8
		private void OnFinishedAttack()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.isAttacking = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastAttack = 0f;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00022ADB File Offset: 0x00020CDB
		private void DoMeleeAttack()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PerformTraceAttack(this.attackPos, this.meleeDamage, 50f, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAttackPos();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.hasAttacked = true;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00022B1C File Offset: 0x00020D1C
		private void SetAttackPos()
		{
			if (!base.Target.IsValid())
			{
				return;
			}
			Vector3 dir = (base.Target.WorldSpaceBounds.Center - this.Host.Position).Normal;
			Vector3 attackPos = this.Host.Position + dir * this.meleeRange;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.attackPos = attackPos;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00022B8C File Offset: 0x00020D8C
		public bool CanDoMelee()
		{
			return this.Host.TargetingComponent.DistanceToTarget <= this.meleeRange && this.Host.TargetingComponent.CanSeeTarget();
		}

		// Token: 0x0400028A RID: 650
		private TimeSince timeSinceLastAttack = 0f;

		// Token: 0x0400028B RID: 651
		private Vector3 attackPos;

		// Token: 0x0400028C RID: 652
		private bool hasAttacked;

		// Token: 0x0400028D RID: 653
		private bool isAttacking;
	}
}
