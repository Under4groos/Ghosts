using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox;
using SandboxEditor;
using StalkerRP.Debug;

namespace StalkerRP.NPC
{
	// Token: 0x020000A2 RID: 162
	[DebugSpawnable(Name = "Blind Dog", Category = "Mutants")]
	[Title("Blind Dog")]
	[HammerEntity]
	[Category("Mutants")]
	[EditorModel("models/stalker/monsters/dog/blind_dog.vmdl", "white", "white")]
	[Spawnable]
	[NPC]
	public class BlindDogNPC : NPCBase
	{
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x00020E5F File Offset: 0x0001F05F
		public float AttackDamage
		{
			get
			{
				return 20f;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x00020E66 File Offset: 0x0001F066
		public float LeapAttackDamage
		{
			get
			{
				return 30f;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x00020E6D File Offset: 0x0001F06D
		public float MaxGroupFormDistance
		{
			get
			{
				return 3000f;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x00020E74 File Offset: 0x0001F074
		public virtual string PanicSound
		{
			get
			{
				return "bdog.panic";
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x00020E7B File Offset: 0x0001F07B
		public virtual string GrowlSound
		{
			get
			{
				return "bdog.growl";
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x00020E82 File Offset: 0x0001F082
		protected override float PainSoundDelay
		{
			get
			{
				return 1.5f;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x00020E89 File Offset: 0x0001F089
		protected override float IdleSoundMinDelay
		{
			get
			{
				return 5f;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x00020E90 File Offset: 0x0001F090
		protected override float IdleSoundMaxDelay
		{
			get
			{
				return 8f;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x00020E97 File Offset: 0x0001F097
		protected override bool EnableIdleSounds
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x00020E9A File Offset: 0x0001F09A
		protected override float FootStepVolume
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x00020EA1 File Offset: 0x0001F0A1
		public override Capsule PhysicsCapsule
		{
			get
			{
				return Capsule.FromHeightAndRadius(32f, 20f);
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x00020EB2 File Offset: 0x0001F0B2
		protected override string NPCAssetID
		{
			get
			{
				return "dog";
			}
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00020EBC File Offset: 0x0001F0BC
		protected override void PostSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EyeLocalPosition = Vector3.Up * 20f + Vector3.Forward * 35f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Movement = new BlindDogMovement(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleState = new BlindDogIdleState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StaggerState = new BlindDogStaggerState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ChargeState = new BlindDogChargeState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FleeState = new BlindDogFleeState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MeleeState = new BlindDogMeleeState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LeapState = new BlindDogLeapState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CircleState = new BlindDogCircleState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetMaterialGroup(Rand.Int(base.MaterialGroupCount) - 1);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Scale = Rand.Float(0.55f, 0.95f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Health *= this.Scale;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FindGroup();
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00020FEC File Offset: 0x0001F1EC
		protected virtual void FindGroup()
		{
			foreach (BlindDogNPC dog in Entity.All.OfType<BlindDogNPC>())
			{
				if (this.Position.Distance(dog.Position) < this.MaxGroupFormDistance)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					dog.JoinGroup(this);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.BlindDogGroup = dog.BlindDogGroup;
					return;
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.BlindDogGroup = new BlindDogGroup();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.BlindDogGroup.SetLeader(this);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00021094 File Offset: 0x0001F294
		public virtual void JoinGroup(NPCBase dog)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.BlindDogGroup.AddMember(dog);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x000210A7 File Offset: 0x0001F2A7
		public override void SetToIdleState()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x000210BF File Offset: 0x0001F2BF
		public override float GetWishSpeed()
		{
			if (!base.Damaged)
			{
				return base.Resource.RunSpeed;
			}
			return base.Resource.CrippledSpeed;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x000210E0 File Offset: 0x0001F2E0
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

		// Token: 0x06000742 RID: 1858 RVA: 0x00021208 File Offset: 0x0001F408
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

		// Token: 0x06000743 RID: 1859 RVA: 0x000212E7 File Offset: 0x0001F4E7
		protected override void OnBecomeDamaged(DamageInfo info)
		{
			if (!this.FleeState.IsActive && !this.StaggerState.IsActive)
			{
				base.SM.SetState(this.FleeState);
			}
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00021314 File Offset: 0x0001F514
		public override void OnKilled()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BlindDogGroup blindDogGroup = this.BlindDogGroup;
			if (blindDogGroup != null)
			{
				blindDogGroup.OnMemberDied(this);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnKilled();
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00021338 File Offset: 0x0001F538
		public override void OnTargetAcquired(Entity target)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BlindDogGroup blindDogGroup = this.BlindDogGroup;
			if (blindDogGroup == null)
			{
				return;
			}
			blindDogGroup.OnMemberAcquireTarget(target);
		}

		// Token: 0x0400026A RID: 618
		public BlindDogIdleState IdleState;

		// Token: 0x0400026B RID: 619
		public BlindDogStaggerState StaggerState;

		// Token: 0x0400026C RID: 620
		public BlindDogChargeState ChargeState;

		// Token: 0x0400026D RID: 621
		public BlindDogFleeState FleeState;

		// Token: 0x0400026E RID: 622
		public BlindDogMeleeState MeleeState;

		// Token: 0x0400026F RID: 623
		public BlindDogLeapState LeapState;

		// Token: 0x04000270 RID: 624
		public BlindDogCircleState CircleState;

		// Token: 0x04000271 RID: 625
		public BlindDogGroup BlindDogGroup;

		// Token: 0x04000272 RID: 626
		private float damageTakenSinceLastStagger;
	}
}
