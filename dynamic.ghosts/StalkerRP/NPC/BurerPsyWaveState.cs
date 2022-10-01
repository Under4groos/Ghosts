using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x02000089 RID: 137
	public class BurerPsyWaveState : NPCState<BurerNPC>
	{
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x0001CCAC File Offset: 0x0001AEAC
		private float Cooldown
		{
			get
			{
				return 5f;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x0001CCB3 File Offset: 0x0001AEB3
		private float PsyWaveSpeed
		{
			get
			{
				return 500f;
			}
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001CCBA File Offset: 0x0001AEBA
		public BurerPsyWaveState(BurerNPC host) : base(host)
		{
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001CCC4 File Offset: 0x0001AEC4
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("eAttackType", 1);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("ePsychicType", 0);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001CD3A File Offset: 0x0001AF3A
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", false);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001CD54 File Offset: 0x0001AF54
		public override void Update(float deltaTime)
		{
			if (!this.Host.TargetingComponent.ValidateTarget())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.FaceTarget();
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001CDA4 File Offset: 0x0001AFA4
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ThrowPsyWave();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SM.SetState(this.Host.ChaseState);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001CDD4 File Offset: 0x0001AFD4
		private void ThrowPsyWave()
		{
			BurerPsyWaveEntity burerPsyWaveEntity = new BurerPsyWaveEntity();
			burerPsyWaveEntity.Position = this.Host.WorldSpaceBounds.Center;
			burerPsyWaveEntity.Direction = (base.Target.Position - this.Host.Position).Normal;
			burerPsyWaveEntity.Speed = this.Host.TelekinesisPushSpeed;
			burerPsyWaveEntity.Radius = 25f;
			burerPsyWaveEntity.Damage = this.Host.TelekinesisPushDamage;
			burerPsyWaveEntity.Owner = this.Host;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			burerPsyWaveEntity.Fire();
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001CE6C File Offset: 0x0001B06C
		public bool CanDoPsyWave()
		{
			return base.Target.IsValid() && this.TimeSinceExited > this.Cooldown && this.Host.TargetingComponent.DistanceToTarget <= this.Host.TelekinesisPushRange;
		}
	}
}
