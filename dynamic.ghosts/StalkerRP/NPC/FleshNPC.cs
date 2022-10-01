using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox;
using StalkerRP.Debug;

namespace StalkerRP.NPC
{
	// Token: 0x020000AC RID: 172
	[DebugSpawnable(Name = "Flesh", Category = "Mutants")]
	[Title("Flesh")]
	[Category("Mutants")]
	[EditorModel("models/stalker/monsters/flesh/flesh.vmdl", "white", "white")]
	[Spawnable]
	[NPC]
	public class FleshNPC : NPCBase
	{
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x000221BF File Offset: 0x000203BF
		public float AttackDamage
		{
			get
			{
				return 30f;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x000221C6 File Offset: 0x000203C6
		public string GrowlSound
		{
			get
			{
				return "flesh.aggro";
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x000221CD File Offset: 0x000203CD
		protected override float PainSoundDelay
		{
			get
			{
				return 1.5f;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x000221D4 File Offset: 0x000203D4
		protected override float IdleSoundMinDelay
		{
			get
			{
				return 5f;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x000221DB File Offset: 0x000203DB
		protected override float IdleSoundMaxDelay
		{
			get
			{
				return 8f;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x000221E2 File Offset: 0x000203E2
		protected override bool EnableIdleSounds
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x000221E5 File Offset: 0x000203E5
		protected override float FootStepVolume
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x000221EC File Offset: 0x000203EC
		public override Capsule PhysicsCapsule
		{
			get
			{
				return Capsule.FromHeightAndRadius(16f, 24f);
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x000221FD File Offset: 0x000203FD
		protected override string NPCAssetID
		{
			get
			{
				return "flesh";
			}
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00022204 File Offset: 0x00020404
		protected override void PostSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EyeLocalPosition = Vector3.Up * 20f + Vector3.Forward * 35f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Movement = new FleshMovement(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleState = new FleshIdleState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ChargeState = new FleshChargeState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CircleState = new FleshCircleState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MeleeState = new FleshMeleeState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FleeState = new FleshFleeState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StaggerState = new FleshStaggerState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.JoinNearbyAlpha();
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x000222D7 File Offset: 0x000204D7
		public override void SetToIdleState()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x000222EF File Offset: 0x000204EF
		public override float GetWishSpeed()
		{
			if (!base.Damaged)
			{
				return base.Resource.RunSpeed;
			}
			return base.Resource.CrippledSpeed;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00022310 File Offset: 0x00020510
		public void JoinNearbyAlpha()
		{
			using (IEnumerator<FleshAlphaNPC> enumerator = Entity.All.OfType<FleshAlphaNPC>().GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					FleshAlphaNPC fleshAlphaNPC = enumerator.Current;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					fleshAlphaNPC.RecruitFlesh(this);
				}
			}
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00022368 File Offset: 0x00020568
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

		// Token: 0x06000791 RID: 1937 RVA: 0x00022490 File Offset: 0x00020690
		protected override void OnTakeDamage(DamageInfo info)
		{
			if (info.Damage <= 0f)
			{
				return;
			}
			if (!this.StaggerState.CanBeStaggered())
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.damageTakenSinceLastStagger += info.Damage;
			if (base.GetHitboxGroup(info.HitboxIndex) == 18)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.SetAnimParameter(NPCAnimParameters.StaggerType, 2);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.SM.SetState(this.StaggerState);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.damageTakenSinceLastStagger = 0f;
				return;
			}
			if (base.GetHitboxGroup(info.HitboxIndex) == 1)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.SetAnimParameter(NPCAnimParameters.StaggerType, 0);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.SM.SetState(this.StaggerState);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.damageTakenSinceLastStagger = 0f;
				return;
			}
			if (this.damageTakenSinceLastStagger > 80f && base.GetHitboxGroup(info.HitboxIndex) == 2)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.SetAnimParameter(NPCAnimParameters.StaggerType, 1);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.SM.SetState(this.StaggerState);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.damageTakenSinceLastStagger = 0f;
			}
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x000225B8 File Offset: 0x000207B8
		protected override void OnBecomeDamaged(DamageInfo info)
		{
			if (!this.FleeState.IsActive && !this.StaggerState.IsActive)
			{
				base.SM.SetState(this.FleeState);
			}
		}

		// Token: 0x04000281 RID: 641
		public FleshIdleState IdleState;

		// Token: 0x04000282 RID: 642
		public FleshCircleState CircleState;

		// Token: 0x04000283 RID: 643
		public FleshChargeState ChargeState;

		// Token: 0x04000284 RID: 644
		public FleshFleeState FleeState;

		// Token: 0x04000285 RID: 645
		public FleshStaggerState StaggerState;

		// Token: 0x04000286 RID: 646
		public FleshMeleeState MeleeState;

		// Token: 0x04000287 RID: 647
		private float damageTakenSinceLastStagger;
	}
}
