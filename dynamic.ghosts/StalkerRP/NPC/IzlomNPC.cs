using System;
using System.Runtime.CompilerServices;
using Sandbox;
using SandboxEditor;
using StalkerRP.Debug;

namespace StalkerRP.NPC
{
	// Token: 0x020000B1 RID: 177
	[DebugSpawnable(Name = "Izlom", Category = "Mutants")]
	[Title("Fracture")]
	[HammerEntity]
	[Category("Mutants")]
	[EditorModel("models/stalker/monsters/izlom/izlom.vmdl", "white", "white")]
	[Spawnable]
	[NPC]
	public class IzlomNPC : NPCBase
	{
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x00022BBD File Offset: 0x00020DBD
		protected override float PainSoundDelay
		{
			get
			{
				return 1.5f;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00022BC4 File Offset: 0x00020DC4
		protected override float IdleSoundMinDelay
		{
			get
			{
				return 3f;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x00022BCB File Offset: 0x00020DCB
		protected override float IdleSoundMaxDelay
		{
			get
			{
				return 8f;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00022BD2 File Offset: 0x00020DD2
		protected override bool EnableIdleSounds
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x00022BD5 File Offset: 0x00020DD5
		protected override float FootStepVolume
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00022BDC File Offset: 0x00020DDC
		public override Capsule PhysicsCapsule
		{
			get
			{
				return Capsule.FromHeightAndRadius(72f, 28f);
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x00022BED File Offset: 0x00020DED
		protected override string NPCAssetID
		{
			get
			{
				return "izlom";
			}
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00022BF4 File Offset: 0x00020DF4
		protected override void PostSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EyeLocalPosition = Vector3.Up * 50f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleState = new IzlomIdleState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ChaseState = new IzlomChaseState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MeleeState = new IzlomMeleeState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Movement = new NPCMovementSlowTurn(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00022C75 File Offset: 0x00020E75
		public override void SetToIdleState()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00022C90 File Offset: 0x00020E90
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

		// Token: 0x0400028E RID: 654
		public IzlomIdleState IdleState;

		// Token: 0x0400028F RID: 655
		public IzlomChaseState ChaseState;

		// Token: 0x04000290 RID: 656
		public IzlomMeleeState MeleeState;
	}
}
