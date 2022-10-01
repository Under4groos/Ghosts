using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000C6 RID: 198
	public class PseudoDogPsyWaveState : NPCState<PseudoDogNPC>
	{
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x00025DD8 File Offset: 0x00023FD8
		private float Cooldown
		{
			get
			{
				return 3f;
			}
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00025DDF File Offset: 0x00023FDF
		public PseudoDogPsyWaveState(PseudoDogNPC host) : base(host)
		{
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00025DE8 File Offset: 0x00023FE8
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.AttackType, 2);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Attacking, true);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00025E16 File Offset: 0x00024016
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter(NPCAnimParameters.Attacking, false);
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00025E30 File Offset: 0x00024030
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

		// Token: 0x060008A1 RID: 2209 RVA: 0x00025E80 File Offset: 0x00024080
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ThrowPsyWave();
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00025E8D File Offset: 0x0002408D
		public override void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData)
		{
			if (name.Equals("NPCAttackEventFinished"))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.CircleState);
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00025EBC File Offset: 0x000240BC
		private void ThrowPsyWave()
		{
			PseudoDogPsyWaveEntity pseudoDogPsyWaveEntity = new PseudoDogPsyWaveEntity();
			pseudoDogPsyWaveEntity.Position = this.Host.WorldSpaceBounds.Center + Vector3.Up * 15f + this.Host.Rotation.Forward * 15f;
			pseudoDogPsyWaveEntity.Direction = (base.Target.Position - this.Host.Position).Normal;
			pseudoDogPsyWaveEntity.Speed = this.Host.TelekinesisPushSpeed;
			pseudoDogPsyWaveEntity.Radius = 25f;
			pseudoDogPsyWaveEntity.Damage = this.Host.TelekinesisPushDamage;
			pseudoDogPsyWaveEntity.Owner = this.Host;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pseudoDogPsyWaveEntity.Fire();
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00025F8C File Offset: 0x0002418C
		public bool CanDoPsyWave()
		{
			return base.Target.IsValid() && this.TimeSinceExited > this.Cooldown && this.Host.TargetingComponent.DistanceToTarget <= this.Host.TelekinesisPushRange;
		}
	}
}
