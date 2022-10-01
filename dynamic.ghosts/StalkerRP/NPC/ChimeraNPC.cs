using System;
using System.Runtime.CompilerServices;
using Sandbox;
using SandboxEditor;
using StalkerRP.Debug;

namespace StalkerRP.NPC
{
	// Token: 0x02000091 RID: 145
	[DebugSpawnable(Name = "Chimera", Category = "Mutants")]
	[Title("Chimera")]
	[HammerEntity]
	[Category("Mutants")]
	[EditorModel("models/stalker/monsters/chimera/chimera.vmdl", "white", "white")]
	[Spawnable]
	[NPC]
	public class ChimeraNPC : NPCBase
	{
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x0001E359 File Offset: 0x0001C559
		public float MaxLeapRange
		{
			get
			{
				return 1000f;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0001E360 File Offset: 0x0001C560
		public float MinLeapRange
		{
			get
			{
				return 350f;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x0001E367 File Offset: 0x0001C567
		public float LeapDamage
		{
			get
			{
				return 100f;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x0001E36E File Offset: 0x0001C56E
		public string LeapHitSound
		{
			get
			{
				return "chimera.leaphit";
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0001E375 File Offset: 0x0001C575
		public string LittleIdleSound
		{
			get
			{
				return "chimera.little.idle";
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x0001E37C File Offset: 0x0001C57C
		public string LittlePainSound
		{
			get
			{
				return "chimera.little.pain";
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x0001E383 File Offset: 0x0001C583
		public string LittleDeathSound
		{
			get
			{
				return "chimera.little.death";
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0001E38A File Offset: 0x0001C58A
		public string GrowlSound
		{
			get
			{
				return "chimera.growl";
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x0001E391 File Offset: 0x0001C591
		protected override float PainSoundDelay
		{
			get
			{
				return 1.5f;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0001E398 File Offset: 0x0001C598
		protected override float IdleSoundMinDelay
		{
			get
			{
				return 5f;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x0001E39F File Offset: 0x0001C59F
		protected override float IdleSoundMaxDelay
		{
			get
			{
				return 8f;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0001E3A6 File Offset: 0x0001C5A6
		protected override bool EnableIdleSounds
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0001E3A9 File Offset: 0x0001C5A9
		protected override float FootStepVolume
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0001E3B0 File Offset: 0x0001C5B0
		public override Capsule PhysicsCapsule
		{
			get
			{
				return Capsule.FromHeightAndRadius(16f, 24f);
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0001E3C1 File Offset: 0x0001C5C1
		protected override string NPCAssetID
		{
			get
			{
				return "chimera";
			}
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001E3C8 File Offset: 0x0001C5C8
		protected override void PostSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Scale = 0.9f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EyeLocalPosition = Vector3.Up * 20f + Vector3.Forward * 35f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Movement = new NPCMovementSlowTurn(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleState = new ChimeraIdleState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ChaseState = new ChimeraChaseState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LeapState = new ChimeraLeapState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001E46D File Offset: 0x0001C66D
		public override void SetToIdleState()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001E488 File Offset: 0x0001C688
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
			base.SetAnimParameter("fTargetDirectionDotProduct", base.GetAnimParameterFloat("fTargetDirectionDotProduct").LerpTo(dot, Time.Delta * 10f, true));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetAnimParameter("fMoveSpeed", this.Velocity.WithZ(0f).Length);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			string name = "bTurning";
			NPCMovementSlowTurn npcmovementSlowTurn = this.Movement as NPCMovementSlowTurn;
			base.SetAnimParameter(name, npcmovementSlowTurn != null && npcmovementSlowTurn.IsStationaryTurning);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001E57A File Offset: 0x0001C77A
		public override void EmitDeathSound()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EmitDeathSound();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromWorld(this.LittleDeathSound, this.Position);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001E59E File Offset: 0x0001C79E
		public override void EmitIdleSound()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EmitIdleSound();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromEntity(this.LittleIdleSound, this);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001E5BD File Offset: 0x0001C7BD
		public override void EmitPainSound()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EmitPainSound();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromEntity(this.LittlePainSound, this);
		}

		// Token: 0x0400023F RID: 575
		public ChimeraIdleState IdleState;

		// Token: 0x04000240 RID: 576
		public ChimeraChaseState ChaseState;

		// Token: 0x04000241 RID: 577
		public ChimeraLeapState LeapState;
	}
}
