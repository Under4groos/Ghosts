using System;
using System.Runtime.CompilerServices;
using Sandbox;
using SandboxEditor;
using StalkerRP.Debug;
using StalkerRP.PostProcessing;

namespace StalkerRP.NPC
{
	// Token: 0x020000D7 RID: 215
	[DebugSpawnable(Name = "Psydog", Category = "Mutants")]
	[Title("Psydog")]
	[HammerEntity]
	[Category("Mutants")]
	[EditorModel("models/stalker/monsters/pseudodog/pseudodog.vmdl", "white", "white")]
	[Spawnable]
	[NPC]
	public class PsyDogNPC : NPCBase, IPsyCreature
	{
		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x00028433 File Offset: 0x00026633
		public float AttackDamage
		{
			get
			{
				return 40f;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x0002843A File Offset: 0x0002663A
		public int CurrentIllusions
		{
			get
			{
				return this.IllusionGroup.NumIllusions();
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x00028447 File Offset: 0x00026647
		public string GrowlSound
		{
			get
			{
				return "pdog.aggro";
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x0002844E File Offset: 0x0002664E
		protected override float PainSoundDelay
		{
			get
			{
				return 1.5f;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x00028455 File Offset: 0x00026655
		protected override float IdleSoundMinDelay
		{
			get
			{
				return 5f;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x0002845C File Offset: 0x0002665C
		protected override float IdleSoundMaxDelay
		{
			get
			{
				return 8f;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x00028463 File Offset: 0x00026663
		protected override bool EnableIdleSounds
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x00028466 File Offset: 0x00026666
		protected override float FootStepVolume
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x0002846D File Offset: 0x0002666D
		public override Capsule PhysicsCapsule
		{
			get
			{
				return Capsule.FromHeightAndRadius(32f, 20f);
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x0002847E File Offset: 0x0002667E
		protected virtual BaseNPCState StartingState
		{
			get
			{
				return this.IdleState;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x00028486 File Offset: 0x00026686
		protected override string NPCAssetID
		{
			get
			{
				return "psydog";
			}
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00028490 File Offset: 0x00026690
		protected override void PostSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EyeLocalPosition = Vector3.Up * 20f + Vector3.Forward * 35f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Movement = new PseudoDogMovement(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ChargeState = new PsyDogChargeState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CircleState = new PsyDogCircleState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DodgeState = new PsyDogDodgeState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FleeState = new PsyDogFleeState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MeleeState = new PsyDogMeleeState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LeapState = new PsyDogLeapState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StaggerState = new PsyDogStaggerState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleState = new PsyDogIdleState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SummonState = new PsyDogSummonState(this);
			if (!this.IsIllusion())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.IllusionGroup = new PsyDogIllusionGroup();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.IllusionGroup.SetLeader(this);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.StartingState);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Scale = 0.75f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetMaterialGroup(1);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x000285D0 File Offset: 0x000267D0
		public override void ClientSpawn()
		{
			if (!this.IsIllusion())
			{
				PostProcessEmitterComponent postProcessEmitterComponent = this.Components.Create<PostProcessEmitterComponent>(true);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				postProcessEmitterComponent.SetUpEmitter(EffectsPostProcessManager.STALKER_PSY_PSYDOG_COLOR, 700f, 0f, "psy_aura_loop", 0.1f, 0.5f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				postProcessEmitterComponent.AddSaturation(0.75f);
			}
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00028629 File Offset: 0x00026829
		public override float GetWishSpeed()
		{
			if (!base.Damaged)
			{
				return base.Resource.RunSpeed;
			}
			return base.Resource.CrippledSpeed;
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0002864A File Offset: 0x0002684A
		public virtual bool IsIllusion()
		{
			return false;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0002864D File Offset: 0x0002684D
		public override void SetToIdleState()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00028665 File Offset: 0x00026865
		public override void OnKilled()
		{
			if (!this.IsIllusion() && this.IllusionGroup.Leader == this)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.IllusionGroup.OnLeaderDeath();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnKilled();
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00028698 File Offset: 0x00026898
		public void DissipateIllusions()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			PsyDogIllusionGroup illusionGroup = this.IllusionGroup;
			if (illusionGroup == null)
			{
				return;
			}
			illusionGroup.DissipateIllusions();
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x000286AF File Offset: 0x000268AF
		protected override void OnDestroy()
		{
			if (!this.IsIllusion() && this.IllusionGroup != null && this.IllusionGroup.Leader == this)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DissipateIllusions();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnDestroy();
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x000286E5 File Offset: 0x000268E5
		public bool ShouldCreateIllusions()
		{
			return !this.IsIllusion() && this.IllusionGroup.IsEmpty();
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x000286FC File Offset: 0x000268FC
		public bool CanCreateIllusion()
		{
			return !this.IsIllusion() && this.IllusionGroup.NumIllusions() < this.MaxIllusions;
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0002871C File Offset: 0x0002691C
		private Vector3 GetIllusionCreatedPosition()
		{
			Vector3 targPos = base.WorldSpaceBounds.Center + Vector3.Up * 50f + Vector3.Random * 90f;
			Vector3 center = base.WorldSpaceBounds.Center;
			Trace trace = Trace.Ray(center, targPos).Radius(1f);
			Entity entity = this;
			bool flag = true;
			TraceResult tr = trace.Ignore(entity, flag).WorldOnly().Run();
			if (tr.Hit)
			{
				return tr.EndPosition - tr.Normal * 20f;
			}
			return targPos;
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x000287D4 File Offset: 0x000269D4
		private Vector3 GetIllusionCreatedVelocity()
		{
			return this.Rotation.Forward * 350f + Vector3.Up * 40f;
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0002880D File Offset: 0x00026A0D
		public override void OnTargetAcquired(Entity target)
		{
			if (!this.IsIllusion() && this.IllusionGroup.Leader == this)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.IllusionGroup.OnLeaderAcquiredTarget(target);
			}
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00028838 File Offset: 0x00026A38
		public void CreateIllusion()
		{
			PsyDogIllusionNPC ill = new PsyDogIllusionNPC();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ill.Position = this.GetIllusionCreatedPosition();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ill.Velocity = this.GetIllusionCreatedVelocity();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ill.Rotation = this.Rotation;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ill.DoIllusionCreatedEffect();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ill.IllusionGroup = this.IllusionGroup;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IllusionGroup.AddMember(ill);
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x000288AC File Offset: 0x00026AAC
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

		// Token: 0x040002F7 RID: 759
		public int MaxIllusions = 5;

		// Token: 0x040002F8 RID: 760
		public PsyDogChargeState ChargeState;

		// Token: 0x040002F9 RID: 761
		public PsyDogCircleState CircleState;

		// Token: 0x040002FA RID: 762
		public PsyDogDodgeState DodgeState;

		// Token: 0x040002FB RID: 763
		public PsyDogFleeState FleeState;

		// Token: 0x040002FC RID: 764
		public PsyDogMeleeState MeleeState;

		// Token: 0x040002FD RID: 765
		public PsyDogLeapState LeapState;

		// Token: 0x040002FE RID: 766
		public PsyDogStaggerState StaggerState;

		// Token: 0x040002FF RID: 767
		public PsyDogIdleState IdleState;

		// Token: 0x04000300 RID: 768
		public PsyDogSummonState SummonState;

		// Token: 0x04000301 RID: 769
		public PsyDogIllusionGroup IllusionGroup;
	}
}
