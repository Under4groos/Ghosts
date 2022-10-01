using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox;
using SandboxEditor;
using StalkerRP.Debug;

namespace StalkerRP.NPC
{
	// Token: 0x020000C4 RID: 196
	[DebugSpawnable(Name = "Pseudodog", Category = "Mutants")]
	[HammerEntity]
	[Category("Mutants")]
	[EditorModel("mmodels/stalker/monsters/pseudodog/pseudodog.vmdl", "white", "white")]
	[Spawnable]
	[NPC]
	public class PseudoDogNPC : NPCBase
	{
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x0002567B File Offset: 0x0002387B
		public float AttackDamage
		{
			get
			{
				return 20f;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x00025682 File Offset: 0x00023882
		public float LeapAttackDamage
		{
			get
			{
				return 30f;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x00025689 File Offset: 0x00023889
		public float MaxGroupFormDistance
		{
			get
			{
				return 3000f;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x00025690 File Offset: 0x00023890
		public float TelekinesisPushDamage
		{
			get
			{
				return 50f;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x00025697 File Offset: 0x00023897
		public float TelekinesisPushRange
		{
			get
			{
				return 1400f;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x0002569E File Offset: 0x0002389E
		public float TelekinesisPushSpeed
		{
			get
			{
				return 500f;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x000256A5 File Offset: 0x000238A5
		public float TelekinesisThrowDamage
		{
			get
			{
				return 10f;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x000256AC File Offset: 0x000238AC
		public string GrowlSound
		{
			get
			{
				return "pdog.aggro";
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x000256B3 File Offset: 0x000238B3
		protected override float PainSoundDelay
		{
			get
			{
				return 1.5f;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x000256BA File Offset: 0x000238BA
		protected override float IdleSoundMinDelay
		{
			get
			{
				return 5f;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x000256C1 File Offset: 0x000238C1
		protected override float IdleSoundMaxDelay
		{
			get
			{
				return 8f;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x000256C8 File Offset: 0x000238C8
		protected override bool EnableIdleSounds
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x000256CB File Offset: 0x000238CB
		protected override float FootStepVolume
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x000256D2 File Offset: 0x000238D2
		public override Capsule PhysicsCapsule
		{
			get
			{
				return Capsule.FromHeightAndRadius(32f, 20f);
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x000256E3 File Offset: 0x000238E3
		protected override string NPCAssetID
		{
			get
			{
				return "pseudodog";
			}
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x000256EC File Offset: 0x000238EC
		protected override void PostSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EyeLocalPosition = Vector3.Up * 20f + Vector3.Forward * 35f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Movement = new PseudoDogMovement(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleState = new PseudoDogIdleState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StaggerState = new PseudoDogStaggerState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ChargeState = new PseudoDogChargeState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FleeState = new PseudoDogFleeState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MeleeState = new PseudoDogMeleeState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LeapState = new PseuodoDogLeapState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DodgeState = new PseudoDogDodgeState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CircleState = new PseudoDogCircleState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PsyWaveState = new PseudoDogPsyWaveState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Scale = Rand.Float(0.55f, 0.92f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Health *= this.Scale;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FindGroup();
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00025824 File Offset: 0x00023A24
		private void FindGroup()
		{
			foreach (PseudoDogNPC dog in Entity.All.OfType<PseudoDogNPC>())
			{
				if (this.Position.Distance(dog.Position) < this.MaxGroupFormDistance)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					dog.JoinGroup(this);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.PseudoDogGroup = dog.PseudoDogGroup;
					return;
				}
			}
			foreach (BlindDogNPC dog2 in Entity.All.OfType<BlindDogNPC>())
			{
				if (this.Position.Distance(dog2.Position) < this.MaxGroupFormDistance)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					dog2.JoinGroup(this);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.PseudoDogGroup = dog2.BlindDogGroup;
					return;
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PseudoDogGroup = new BlindDogGroup();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PseudoDogGroup.SetLeader(this);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00025948 File Offset: 0x00023B48
		public void JoinGroup(PseudoDogNPC dog)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PseudoDogGroup.AddMember(dog);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0002595B File Offset: 0x00023B5B
		public override void SetToIdleState()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00025973 File Offset: 0x00023B73
		public override float GetWishSpeed()
		{
			if (!base.Damaged)
			{
				return base.Resource.RunSpeed;
			}
			return base.Resource.CrippledSpeed;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00025994 File Offset: 0x00023B94
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

		// Token: 0x06000889 RID: 2185 RVA: 0x00025ABA File Offset: 0x00023CBA
		protected override void OnBecomeDamaged(DamageInfo info)
		{
			if (!this.FleeState.IsActive && !this.StaggerState.IsActive)
			{
				base.SM.SetState(this.FleeState);
			}
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00025AE7 File Offset: 0x00023CE7
		public override void OnKilled()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BlindDogGroup pseudoDogGroup = this.PseudoDogGroup;
			if (pseudoDogGroup != null)
			{
				pseudoDogGroup.OnMemberDied(this);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnKilled();
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00025B0B File Offset: 0x00023D0B
		public override void OnTargetAcquired(Entity target)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BlindDogGroup pseudoDogGroup = this.PseudoDogGroup;
			if (pseudoDogGroup == null)
			{
				return;
			}
			pseudoDogGroup.OnMemberAcquireTarget(target);
		}

		// Token: 0x040002C4 RID: 708
		public PseudoDogIdleState IdleState;

		// Token: 0x040002C5 RID: 709
		public PseudoDogStaggerState StaggerState;

		// Token: 0x040002C6 RID: 710
		public PseudoDogChargeState ChargeState;

		// Token: 0x040002C7 RID: 711
		public PseudoDogFleeState FleeState;

		// Token: 0x040002C8 RID: 712
		public PseudoDogMeleeState MeleeState;

		// Token: 0x040002C9 RID: 713
		public PseuodoDogLeapState LeapState;

		// Token: 0x040002CA RID: 714
		public PseudoDogDodgeState DodgeState;

		// Token: 0x040002CB RID: 715
		public PseudoDogCircleState CircleState;

		// Token: 0x040002CC RID: 716
		public PseudoDogPsyWaveState PsyWaveState;

		// Token: 0x040002CD RID: 717
		public BlindDogGroup PseudoDogGroup;
	}
}
