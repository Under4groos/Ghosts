using System;
using System.Runtime.CompilerServices;
using Sandbox;
using SandboxEditor;
using StalkerRP.Debug;

namespace StalkerRP.NPC
{
	// Token: 0x02000080 RID: 128
	[DebugSpawnable(Name = "Boar", Category = "Mutants")]
	[Title("Boar")]
	[HammerEntity]
	[Category("Mutants")]
	[EditorModel("models/stalker/monsters/boar/boar.vmdl", "white", "white")]
	[Spawnable]
	[NPC]
	public class BoarNPC : NPCBase
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x0001B0D3 File Offset: 0x000192D3
		public float AlphaSearchRadius
		{
			get
			{
				return 1500f;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0001B0DA File Offset: 0x000192DA
		public float AttackDamage
		{
			get
			{
				return 30f;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0001B0E1 File Offset: 0x000192E1
		public string GrowlSound
		{
			get
			{
				return "boar.aggro";
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0001B0E8 File Offset: 0x000192E8
		protected override float PainSoundDelay
		{
			get
			{
				return 1.5f;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0001B0EF File Offset: 0x000192EF
		protected override float IdleSoundMinDelay
		{
			get
			{
				return 5f;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0001B0F6 File Offset: 0x000192F6
		protected override float IdleSoundMaxDelay
		{
			get
			{
				return 8f;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0001B0FD File Offset: 0x000192FD
		protected override bool EnableIdleSounds
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x0001B100 File Offset: 0x00019300
		protected override float FootStepVolume
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0001B107 File Offset: 0x00019307
		public override Capsule PhysicsCapsule
		{
			get
			{
				return Capsule.FromHeightAndRadius(60f, 30f);
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x0001B118 File Offset: 0x00019318
		protected override string NPCAssetID
		{
			get
			{
				return "boar";
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0001B120 File Offset: 0x00019320
		protected override void PostSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EyeLocalPosition = Vector3.Up * 20f + Vector3.Forward * 35f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Movement = new BoarMovement(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleState = new BoarIdleState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ChargeState = new BoarChargeState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CircleState = new BoarCircleState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FleeState = new BoarFleeState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MeleeState = new BoarMeleeState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LungeState = new BoarLungeState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StaggerState = new BoarStaggerState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0001B1F9 File Offset: 0x000193F9
		public override void SetToIdleState()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0001B211 File Offset: 0x00019411
		public override float GetWishSpeed()
		{
			if (!base.Damaged)
			{
				return base.Resource.RunSpeed;
			}
			return base.Resource.CrippledSpeed;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001B234 File Offset: 0x00019434
		protected override void Tick()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Tick();
			Vector3 dir = this.InputVelocity.Normal;
			float dot = this.Rotation.Left.Dot(dir);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			dot = MathF.Acos(dot);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			dot /= 3.1415927f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			dot *= 2f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			dot -= 1f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetAnimParameter(NPCAnimParameters.TargetDirectionDotProduct, base.GetAnimParameterFloat("fTargetDirectionDotProduct").LerpTo(dot, Time.Delta * 10f, true));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetAnimParameter(NPCAnimParameters.MoveSpeed, this.Velocity.WithZ(0f).Length);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetAnimParameter(NPCAnimParameters.SpeedFraction, this.Velocity.WithZ(0f).Length / base.Speed);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			string turning = NPCAnimParameters.Turning;
			NPCMovementSlowTurn st = this.Movement as NPCMovementSlowTurn;
			base.SetAnimParameter(turning, st != null && st.IsStationaryTurning);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0001B35C File Offset: 0x0001955C
		public override void OnNPCCrit(DamageInfo info)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnNPCCrit(info);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimesHitInHead++;
			if ((this.TimesHitInHead > this.TimesHitInHeadStaggerLimit || info.Damage > 100f) && this.StaggerState.CanBeStaggered())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.SM.SetState(this.StaggerState);
			}
		}

		// Token: 0x04000209 RID: 521
		public BoarIdleState IdleState;

		// Token: 0x0400020A RID: 522
		public BoarChargeState ChargeState;

		// Token: 0x0400020B RID: 523
		public BoarCircleState CircleState;

		// Token: 0x0400020C RID: 524
		public BoarFleeState FleeState;

		// Token: 0x0400020D RID: 525
		public BoarMeleeState MeleeState;

		// Token: 0x0400020E RID: 526
		public BoarLungeState LungeState;

		// Token: 0x0400020F RID: 527
		public BoarStaggerState StaggerState;

		// Token: 0x04000210 RID: 528
		private int TimesHitInHead;

		// Token: 0x04000211 RID: 529
		private int TimesHitInHeadStaggerLimit = 3;
	}
}
