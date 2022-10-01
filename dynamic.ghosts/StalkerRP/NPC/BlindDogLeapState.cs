using System;
using System.Runtime.CompilerServices;

namespace StalkerRP.NPC
{
	// Token: 0x0200009F RID: 159
	public class BlindDogLeapState : NPCState<BlindDogNPC>
	{
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x00020889 File Offset: 0x0001EA89
		private float MeleeRange
		{
			get
			{
				return 200f;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x00020890 File Offset: 0x0001EA90
		private float Cooldown
		{
			get
			{
				return 20f;
			}
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00020897 File Offset: 0x0001EA97
		public BlindDogLeapState(BlindDogNPC host) : base(host)
		{
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x000208A0 File Offset: 0x0001EAA0
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.AttackType, 0);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Attacking, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAttackPos();
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x000208D9 File Offset: 0x0001EAD9
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Attacking, false);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x000208F4 File Offset: 0x0001EAF4
		public override void Update(float deltaTime)
		{
			float oldZ = this.Host.Velocity.z;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Velocity = (this.Host.Rotation.Forward * 300f).WithZ(oldZ);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0002094B File Offset: 0x0001EB4B
		public override void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData)
		{
			if (name.Equals("NPCAttackEventFinished"))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnFinishedAttack();
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00020965 File Offset: 0x0001EB65
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoMeleeAttack();
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00020974 File Offset: 0x0001EB74
		private void OnFinishedAttack()
		{
			if (this.Host.Damaged)
			{
				this.Host.SM.SetState(this.Host.FleeState);
				return;
			}
			if (this.Host.Target != null)
			{
				this.Host.SM.SetState(this.Host.ChargeState);
				return;
			}
			this.Host.SM.SetState(this.Host.IdleState);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x000209EE File Offset: 0x0001EBEE
		private void DoMeleeAttack()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PerformTraceAttack(this.AttackPos, this.Host.AttackDamage, 50f, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAttackPos();
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00020A28 File Offset: 0x0001EC28
		private void SetAttackPos()
		{
			Vector3 dir = (base.Target.WorldSpaceBounds.Center - this.Host.Position).Normal;
			Vector3 attackPos = this.Host.Position + dir * this.MeleeRange;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AttackPos = attackPos;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00020A8C File Offset: 0x0001EC8C
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

		// Token: 0x04000265 RID: 613
		private Vector3 AttackPos;
	}
}
