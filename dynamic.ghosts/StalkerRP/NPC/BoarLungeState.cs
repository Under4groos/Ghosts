using System;
using System.Runtime.CompilerServices;

namespace StalkerRP.NPC
{
	// Token: 0x0200007D RID: 125
	public class BoarLungeState : NPCState<BoarNPC>
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0001A9CD File Offset: 0x00018BCD
		private float MeleeRange
		{
			get
			{
				return 150f;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0001A9D4 File Offset: 0x00018BD4
		private float LungeRange
		{
			get
			{
				return 300f;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0001A9DB File Offset: 0x00018BDB
		private float MeleeCooldown
		{
			get
			{
				return 10f;
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001A9E2 File Offset: 0x00018BE2
		public BoarLungeState(BoarNPC host) : base(host)
		{
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001A9EC File Offset: 0x00018BEC
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasAttacked = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = new NavSteer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.AttackType, 0);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Attacking, true);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001AA46 File Offset: 0x00018C46
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Attacking, false);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001AA60 File Offset: 0x00018C60
		public override void Update(float deltaTime)
		{
			if (!this.Host.TargetingComponent.TryEnsureTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Speed = this.Host.GetWishSpeed();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer.Target = this.Host.Position + this.Host.Rotation.Forward * 100f;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001AAFD File Offset: 0x00018CFD
		public override void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData)
		{
			if (name.Equals("NPCAttackEventFinished"))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnFinishedAttack();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnAnimEventGeneric(name, intData, floatData, vectorData, stringData);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001AB29 File Offset: 0x00018D29
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoMeleeAttack();
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0001AB38 File Offset: 0x00018D38
		private void OnFinishedAttack()
		{
			if (this.Host.TargetingComponent.ValidateTarget())
			{
				this.Host.SM.SetState(this.Host.CircleState);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.TargetingComponent.RecalculateThreatLevelsByDistance(false);
			if (this.Host.TargetingComponent.ValidateTarget())
			{
				this.Host.SM.SetState(this.Host.CircleState);
				return;
			}
			this.Host.SM.SetState(this.Host.IdleState);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001ABD4 File Offset: 0x00018DD4
		private void DoMeleeAttack()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAttackPos();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.PerformTraceAttack(this.AttackPos, this.Host.AttackDamage, 50f, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasAttacked = true;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001AC24 File Offset: 0x00018E24
		private void SetAttackPos()
		{
			Vector3 dir = (base.Target.WorldSpaceBounds.Center - this.Host.Position).Normal;
			Vector3 attackPos = this.Host.Position + dir * this.MeleeRange;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AttackPos = attackPos;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0001AC88 File Offset: 0x00018E88
		public bool CanDoLunge()
		{
			if (this.TimeSinceExited < this.MeleeCooldown && this.HasAttacked)
			{
				return false;
			}
			if (this.Host.TargetingComponent.DistanceToTarget > this.LungeRange)
			{
				return false;
			}
			if (!this.Host.TargetingComponent.CanSeeTarget())
			{
				return false;
			}
			Vector3 dir = (base.Target.Position.WithZ(0f) - this.Host.Position.WithZ(0f)).Normal;
			Vector3 lookDir = this.Host.Rotation.Forward.WithZ(0f);
			return dir.Dot(lookDir) >= 0.95f;
		}

		// Token: 0x04000203 RID: 515
		private Vector3 AttackPos;

		// Token: 0x04000204 RID: 516
		private bool HasAttacked;
	}
}
