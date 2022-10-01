using System;
using System.Runtime.CompilerServices;
using Sandbox;
using SandboxEditor;
using StalkerRP.Debug;

namespace StalkerRP.NPC
{
	// Token: 0x020000E1 RID: 225
	[DebugSpawnable(Name = "Snork", Category = "Mutants")]
	[Title("Snork")]
	[HammerEntity]
	[Category("Mutants")]
	[EditorModel("models/stalker/monsters/snork/snork.vmdl", "white", "white")]
	[Spawnable]
	[NPC]
	public class SnorkNPC : NPCBase
	{
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x00029FD5 File Offset: 0x000281D5
		public float WalkSpeed
		{
			get
			{
				return 70f;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x00029FDC File Offset: 0x000281DC
		public float RunSpeedCrippled
		{
			get
			{
				return 110f;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x00029FE3 File Offset: 0x000281E3
		public float MaxLeapRange
		{
			get
			{
				return 630f;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00029FEA File Offset: 0x000281EA
		public float MinLeapRange
		{
			get
			{
				return 400f;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x00029FF1 File Offset: 0x000281F1
		public float LeapDamage
		{
			get
			{
				return 60f;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00029FF8 File Offset: 0x000281F8
		public float AttackDamage
		{
			get
			{
				return 30f;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x00029FFF File Offset: 0x000281FF
		public string AggroSound
		{
			get
			{
				return "snork.aggro";
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x0002A006 File Offset: 0x00028206
		protected override float PainSoundDelay
		{
			get
			{
				return 1.5f;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x0002A00D File Offset: 0x0002820D
		protected override float IdleSoundMinDelay
		{
			get
			{
				return 5f;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x0002A014 File Offset: 0x00028214
		protected override float IdleSoundMaxDelay
		{
			get
			{
				return 8f;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0002A01B File Offset: 0x0002821B
		protected override bool EnableIdleSounds
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x0002A01E File Offset: 0x0002821E
		protected override float FootStepVolume
		{
			get
			{
				return 0.5f;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x0002A025 File Offset: 0x00028225
		public override Capsule PhysicsCapsule
		{
			get
			{
				return Capsule.FromHeightAndRadius(12f, 22f);
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x0002A036 File Offset: 0x00028236
		protected override string NPCAssetID
		{
			get
			{
				return "snork";
			}
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0002A040 File Offset: 0x00028240
		protected override void PostSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Movement = new NPCMovementDragTurn(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EyeLocalPosition = Vector3.Up * 20f + Vector3.Forward * 35f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleState = new SnorkIdleState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ChaseState = new SnorkChaseState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LeapState = new SnorkLeapState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FlankState = new SnorkFlankState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MeleeState = new SnorkMeleeState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Scale = 0.9f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0002A107 File Offset: 0x00028307
		public override float GetWishSpeed()
		{
			if (!base.Damaged)
			{
				return base.Resource.RunSpeed;
			}
			return this.RunSpeedCrippled;
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0002A123 File Offset: 0x00028323
		public override void SetToIdleState()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0002A13C File Offset: 0x0002833C
		protected override void Tick()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Tick();
			if (base.Target.IsValid())
			{
				Vector3 dir = (base.Target.Position - this.Position).Normal;
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
				base.SetAnimParameter("fTargetDirectionDotProduct", dot);
				return;
			}
			base.SetAnimParameter("fTargetDirectionDotProduct", 0);
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0002A1E9 File Offset: 0x000283E9
		public override void TakeDamage(DamageInfo info)
		{
			if (this.LeapState.IsActive)
			{
				info.Damage *= 2f;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.TakeDamage(info);
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0002A217 File Offset: 0x00028417
		public override void OnTargetAcquired(Entity target)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LeapState.SetLeapCooldown(2f);
		}

		// Token: 0x0400031C RID: 796
		public SnorkIdleState IdleState;

		// Token: 0x0400031D RID: 797
		public SnorkChaseState ChaseState;

		// Token: 0x0400031E RID: 798
		public SnorkLeapState LeapState;

		// Token: 0x0400031F RID: 799
		public SnorkFlankState FlankState;

		// Token: 0x04000320 RID: 800
		public SnorkMeleeState MeleeState;
	}
}
