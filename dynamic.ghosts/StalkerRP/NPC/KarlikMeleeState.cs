using System;
using System.Runtime.CompilerServices;

namespace StalkerRP.NPC
{
	// Token: 0x020000B5 RID: 181
	public class KarlikMeleeState : NPCState<KarlikNPC>
	{
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x0002344D File Offset: 0x0002164D
		public float AttackRange
		{
			get
			{
				return 130f;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x00023454 File Offset: 0x00021654
		private float attackDamage
		{
			get
			{
				return 30f;
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0002345B File Offset: 0x0002165B
		public KarlikMeleeState(KarlikNPC host) : base(host)
		{
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00023464 File Offset: 0x00021664
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Attacking, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAttackPos();
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00023487 File Offset: 0x00021687
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Attacking, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x000234B0 File Offset: 0x000216B0
		public override void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData)
		{
			if (name.Equals("NPCAttackEventFinished"))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnFinishedAttack();
			}
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x000234CA File Offset: 0x000216CA
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoMeleeAttack();
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x000234D8 File Offset: 0x000216D8
		private void SetAttackPos()
		{
			Vector3 dir = (base.Target.WorldSpaceBounds.Center - this.Host.Position).Normal;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.attackPos = this.Host.Position + dir * this.AttackRange;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00023538 File Offset: 0x00021738
		private void DoMeleeAttack()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PerformTraceAttack(this.attackPos, this.attackDamage, 50f, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAttackPos();
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0002356C File Offset: 0x0002176C
		private void OnFinishedAttack()
		{
			if (this.Host.TargetingComponent.ValidateTarget())
			{
				this.Host.SM.SetState(this.Host.CircleState);
				return;
			}
			this.Host.SM.SetState(this.Host.IdleState);
		}

		// Token: 0x04000299 RID: 665
		private Vector3 attackPos;
	}
}
