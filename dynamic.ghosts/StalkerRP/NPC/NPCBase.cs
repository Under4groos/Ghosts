using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Debug.settings;
using Sandbox.Internal;
using StalkerRP.Debug;

namespace StalkerRP.NPC
{
	// Token: 0x020000F1 RID: 241
	[Description("This is the base NPC class that all stalker npcs will inherit from. It should be capable of navigation and movement, basic target selection, and not much else.")]
	public abstract class NPCBase : AnimatedEntity, IBoltable
	{
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0002C8A0 File Offset: 0x0002AAA0
		[Description("This is the current target of the NPC. Could be a player, a prop, a barricade, another NPC, whatever.")]
		public Entity Target
		{
			get
			{
				TargetingComponent targetingComponent = this.TargetingComponent;
				if (targetingComponent == null)
				{
					return null;
				}
				TargetComponent target = targetingComponent.Target;
				if (target == null)
				{
					return null;
				}
				return target.Entity;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x0002C8BE File Offset: 0x0002AABE
		[Description("The capsule used to create the physics object for the NPC.")]
		public virtual Capsule PhysicsCapsule
		{
			get
			{
				return Capsule.FromHeightAndRadius(72f, 28f);
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x0002C8CF File Offset: 0x0002AACF
		public float DamagedThreshold
		{
			get
			{
				return 0.3f;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x0002C8D6 File Offset: 0x0002AAD6
		// (set) Token: 0x06000A5F RID: 2655 RVA: 0x0002C8DE File Offset: 0x0002AADE
		public bool Damaged { get; set; }

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x0002C8E7 File Offset: 0x0002AAE7
		// (set) Token: 0x06000A61 RID: 2657 RVA: 0x0002C8EF File Offset: 0x0002AAEF
		[DefaultValue(40f)]
		[Description("The speed at which the entity will move - should it have a valid NavSteer.")]
		public float Speed { get; set; } = 40f;

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x0002C8F8 File Offset: 0x0002AAF8
		protected virtual float PainSoundDelay
		{
			get
			{
				return 0.5f;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x0002C8FF File Offset: 0x0002AAFF
		protected virtual bool EnableIdleSounds
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x0002C902 File Offset: 0x0002AB02
		// (set) Token: 0x06000A65 RID: 2661 RVA: 0x0002C90A File Offset: 0x0002AB0A
		[DefaultValue(3.5f)]
		protected virtual float IdleSoundDelay { get; set; } = 3.5f;

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x0002C913 File Offset: 0x0002AB13
		protected virtual float IdleSoundMinDelay
		{
			get
			{
				return 9f;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x0002C91A File Offset: 0x0002AB1A
		protected virtual float IdleSoundMaxDelay
		{
			get
			{
				return 14f;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x0002C921 File Offset: 0x0002AB21
		protected virtual float FootStepVolume
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x0002C928 File Offset: 0x0002AB28
		[BindComponent]
		public NPCStateMachine SM
		{
			get
			{
				return this.Components.Get<NPCStateMachine>(false);
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x0002C936 File Offset: 0x0002AB36
		[BindComponent]
		public TargetingComponent TargetingComponent
		{
			get
			{
				return this.Components.Get<TargetingComponent>(false);
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x0002C944 File Offset: 0x0002AB44
		[BindComponent]
		public TargetComponent TargetComponent
		{
			get
			{
				return this.Components.Get<TargetComponent>(false);
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x0002C952 File Offset: 0x0002AB52
		// (set) Token: 0x06000A6D RID: 2669 RVA: 0x0002C960 File Offset: 0x0002AB60
		[Property]
		[Net]
		[DefaultValue(true)]
		public unsafe bool Active
		{
			get
			{
				return *this._repback__Active.GetValue();
			}
			set
			{
				this._repback__Active.SetValue(value);
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x0002C96F File Offset: 0x0002AB6F
		// (set) Token: 0x06000A6F RID: 2671 RVA: 0x0002C97D File Offset: 0x0002AB7D
		[Property]
		[Net]
		[DefaultValue(false)]
		public unsafe bool Hidden
		{
			get
			{
				return *this._repback__Hidden.GetValue();
			}
			set
			{
				this._repback__Hidden.SetValue(value);
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000A70 RID: 2672
		protected abstract string NPCAssetID { get; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x0002C98C File Offset: 0x0002AB8C
		public NPCResource Resource
		{
			get
			{
				return StalkerResource.Get<NPCResource>(this.NPCAssetID);
			}
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0002C99C File Offset: 0x0002AB9C
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("npc");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("solid");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Components.Create<NPCStateMachine>(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Components.Create<TargetingComponent>(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Components.Create<TargetComponent>(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.LifeState = LifeState.Alive;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetupFromAsset();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SurroundingBoundsMode = SurroundingBoundsType.Hitboxes;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableHitboxes = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PhysicsEnabled = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Movement = new NPCMovementBase(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.GenericFollowState = new GenericFollowState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.GenericFollowPathState = new GenericFollowPathState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetupPhysicsFromCapsule(PhysicsMotionType.Keyframed, this.PhysicsCapsule);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PostSpawn();
			if (this.Hidden)
			{
				this.Hide();
			}
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0002CAB0 File Offset: 0x0002ACB0
		protected virtual void SetupFromAsset()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetModel(this.Resource.Model);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Health = this.Resource.MaxHealth;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetingComponent.LoadFromAsset(this.Resource);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetComponent.LoadFromAsset(this.Resource);
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0002CB15 File Offset: 0x0002AD15
		protected override void OnDestroy()
		{
			if (base.IsServer)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				NPCStateMachine sm = this.SM;
				if (sm == null)
				{
					return;
				}
				sm.OnDestroyed();
			}
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0002CB34 File Offset: 0x0002AD34
		protected virtual void PostSpawn()
		{
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0002CB38 File Offset: 0x0002AD38
		[Event.Tick.ServerAttribute]
		protected virtual void Tick()
		{
			if (!this.Active)
			{
				return;
			}
			if (base.LifeState != LifeState.Alive)
			{
				return;
			}
			float delta = Time.Delta;
			if (this.EnableIdleSounds && this.TimeSinceLastIdleSound > this.IdleSoundDelay)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TimeSinceLastIdleSound = 0f;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.IdleSoundDelay = Rand.Float(this.IdleSoundMinDelay, this.IdleSoundMaxDelay);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.EmitIdleSound();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.InputVelocity = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Movement.Update(delta);
			if (DebugCommands.stalker_debug_npc_disable_thinking)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			NPCStateMachine sm = this.SM;
			if (sm == null)
			{
				return;
			}
			sm.Update(delta);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0002CBF8 File Offset: 0x0002ADF8
		public void FaceTarget()
		{
			if (!this.Target.IsValid())
			{
				return;
			}
			Rotation targetRotation = Rotation.LookAt((this.Target.Position - this.Position).WithZ(0f).Normal, Vector3.Up);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Rotation = Rotation.Lerp(this.Rotation, targetRotation, 5f * Time.Delta * 5f, true);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0002CC72 File Offset: 0x0002AE72
		public virtual float GetWishSpeed()
		{
			return this.Resource.RunSpeed;
		}

		// Token: 0x06000A79 RID: 2681
		public abstract void SetToIdleState();

		// Token: 0x06000A7A RID: 2682 RVA: 0x0002CC7F File Offset: 0x0002AE7F
		[Description("Tells the creature to attack a target. By default, it just sets the active threat and then forces the NPC back to its idle state.")]
		public void AttackTarget(Entity target)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetingComponent.TrySetTarget(target);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetToIdleState();
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0002CC9E File Offset: 0x0002AE9E
		[Description("Request to follow something. If the NPC is already targeting another creature, the request will be ignored.")]
		public void RequestFollowTarget(Entity ent)
		{
			if (this.Target.IsValid())
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.GenericFollowState.SetFollowTarget(ent);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SM.SetState(this.GenericFollowState);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0002CCD5 File Offset: 0x0002AED5
		public virtual void ForceFollowTarget(Entity ent)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.GenericFollowState.SetFollowTarget(ent);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SM.SetState(this.GenericFollowState);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0002CD00 File Offset: 0x0002AF00
		public override void OnAnimEventFootstep(Vector3 pos, int foot, float volume)
		{
			if (base.LifeState != LifeState.Alive)
			{
				return;
			}
			if (!base.IsClient)
			{
				return;
			}
			Vector3 vector = pos + Vector3.Down * 20f;
			Trace trace = Trace.Ray(pos, vector).Radius(1f);
			Entity entity = this;
			bool flag = true;
			TraceResult tr = trace.Ignore(entity, flag).Run();
			if (!tr.Hit)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			tr.Surface.DoFootstep(this, tr, foot, volume * this.FootStepVolume);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002CD8C File Offset: 0x0002AF8C
		public virtual void OnNPCCrit(DamageInfo info)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			NPCStateMachine sm = this.SM;
			if (sm != null)
			{
				sm.OnCrit(info);
			}
			if (info.Flags.HasFlag(DamageFlags.Bullet))
			{
				Particles particles = Particles.Create("particles/stalker/weapons/bullet_headshot.vpcf", info.Position);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				particles.SetPosition(1, info.Force.Normal);
			}
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0002CDF4 File Offset: 0x0002AFF4
		[Description("This is called by NPC base when it takes damage. It provides the modified damage info struct.")]
		protected virtual void OnTakeDamage(DamageInfo info)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			NPCStateMachine sm = this.SM;
			if (sm == null)
			{
				return;
			}
			sm.TakeDamage(info);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0002CE0C File Offset: 0x0002B00C
		public override void TakeDamage(DamageInfo info)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceLastIdleSound = 0f;
			if (base.GetHitboxGroup(info.HitboxIndex) == 1)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				info.Damage *= this.Resource.CriticalMultiplier;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnNPCCrit(info);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetingComponent.OnDamageTaken(info);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.lastDamageTaken = info;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.TakeDamage(info);
			if (base.LifeState != LifeState.Alive)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnTakeDamage(info);
			if (base.Health < this.Resource.MaxHealth * this.DamagedThreshold)
			{
				if (!this.Damaged)
				{
					this.OnBecomeDamaged(info);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Damaged = true;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.SetAnimParameter(NPCAnimParameters.Damaged, this.Damaged);
			}
			if (info.Damage > 1f && this.TimeSinceLastPainSound > this.PainSoundDelay)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TimeSinceLastPainSound = 0f;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.EmitPainSound();
			}
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0002CF32 File Offset: 0x0002B132
		[Description("Called when the creature becomes 'damaged'. When Health drops below DefaultHealth * DamagedThreshold.")]
		protected virtual void OnBecomeDamaged(DamageInfo info)
		{
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0002CF34 File Offset: 0x0002B134
		public override void OnKilled()
		{
			if (base.LifeState != LifeState.Alive)
			{
				return;
			}
			if ((this.lastDamageTaken.Flags & DamageFlags.PhysicsImpact) != DamageFlags.Generic && this.lastDamageTaken.Damage > 100f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Gib();
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			NPCStateMachine sm = this.SM;
			if (sm != null)
			{
				sm.OnKilled();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoNPCDeath();
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0002CF9C File Offset: 0x0002B19C
		protected virtual void DoNPCDeath()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.LifeState = LifeState.Dead;
			if (this.HasGibbed)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.EmitDeathSound();
			if (this.Resource.ShouldCreateRagdollOnDeath)
			{
				DeathRagdoll rag = this.CreateDeathRagdollAndRemove();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TransferDamageEffect(rag);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Delete();
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0002CFF5 File Offset: 0x0002B1F5
		public virtual void EmitDeathSound()
		{
			if (!string.IsNullOrWhiteSpace(this.Resource.DeathSounds))
			{
				Sound.FromWorld(this.Resource.DeathSounds, this.Position);
			}
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0002D020 File Offset: 0x0002B220
		public virtual void EmitIdleSound()
		{
			if (!string.IsNullOrWhiteSpace(this.Resource.IdleSounds))
			{
				Sound.FromEntity(this.Resource.IdleSounds, this);
			}
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0002D046 File Offset: 0x0002B246
		public virtual void EmitPainSound()
		{
			if (!string.IsNullOrWhiteSpace(this.Resource.PainSounds))
			{
				Sound.FromEntity(this.Resource.PainSounds, this);
			}
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0002D06C File Offset: 0x0002B26C
		public DeathRagdoll CreateDeathRagdollAndRemove()
		{
			DeathRagdoll deathRagdoll = new DeathRagdoll();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			deathRagdoll.CopyFrom(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			deathRagdoll.TakeDamage(this.lastDamageTaken);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Delete();
			return deathRagdoll;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0002D09B File Offset: 0x0002B29B
		public virtual void OnTargetAcquired(Entity target)
		{
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0002D09D File Offset: 0x0002B29D
		[Description("We hook into the Generic anim event in order to have animations trigger certain actions. In this case, things such as attacks, etc.")]
		public override void OnAnimEventGeneric(string name, int intData, float floatData, Vector3 vectorData, string stringData)
		{
			if (name == this.AttackEventName)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnAnimAttackEvent(intData, floatData, vectorData, stringData);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			NPCStateMachine sm = this.SM;
			if (sm == null)
			{
				return;
			}
			sm.OnAnimEventGeneric(name, intData, floatData, vectorData, stringData);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0002D0DA File Offset: 0x0002B2DA
		public virtual void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			NPCStateMachine sm = this.SM;
			if (sm == null)
			{
				return;
			}
			sm.OnAnimAttackEvent(intData, floatDta, vectorData, stringData);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0002D0F6 File Offset: 0x0002B2F6
		protected virtual void OnAttackHit(DamageInfo info, TraceResult traceResult)
		{
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0002D0F8 File Offset: 0x0002B2F8
		public bool PerformTraceAttackOnTarget(float damage = 50f, float force = 50f, float range = 80f, float deltaDelay = 0f)
		{
			if (!this.Target.IsValid())
			{
				return false;
			}
			Vector3 dir = (this.Target.WorldSpaceBounds.Center - this.Position).Normal;
			Vector3 attackPos = this.Position + dir * range;
			return this.PerformTraceAttack(attackPos, damage, force, deltaDelay);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0002D15C File Offset: 0x0002B35C
		public bool PerformTraceAttack(Vector3 endPos, float damage = 50f, float force = 50f, float deltaDelay = 0f)
		{
			Vector3 position = this.Position;
			Trace trace = Trace.Ray(position, endPos);
			Entity entity = this;
			bool flag = true;
			TraceResult tr = trace.Ignore(entity, flag).EntitiesOnly().WithAnyTags(new string[]
			{
				"player",
				"npc"
			}).Radius(5f).UseHitboxes(true).Run();
			if (!tr.Entity.IsValid())
			{
				return false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			tr.Surface.DoBulletImpact(tr);
			DamageInfo damageInfo = default(DamageInfo);
			damageInfo = damageInfo.WithFlag(DamageFlags.Slash);
			damageInfo = damageInfo.UsingTraceResult(tr);
			damageInfo = damageInfo.WithAttacker(this, null);
			damageInfo = damageInfo.WithForce(tr.Direction * force);
			damageInfo = damageInfo.WithPosition(tr.EndPosition);
			DamageInfo info = damageInfo.WithHitbox((int)this.Resource.LimbWeightResource.GetRandomLimb());
			RuntimeHelpers.EnsureSufficientExecutionStack();
			info.Damage = damage;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			tr.Entity.TakeDamage(info);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnAttackHit(info, tr);
			return true;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0002D280 File Offset: 0x0002B480
		public bool CanSee(Entity target, bool expensive = false)
		{
			Vector3 tpos = target.WorldSpaceBounds.Center;
			if (!expensive)
			{
				return this.CanSee(tpos);
			}
			Vector3 eyepos = target.EyePosition;
			return this.CanSee(eyepos) || this.CanSee(tpos) || this.CanSee(tpos + (eyepos - tpos) * 0.5f);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0002D2E4 File Offset: 0x0002B4E4
		public bool CanSee(Vector3 point)
		{
			Vector3 eyePosition = base.EyePosition;
			return Trace.Ray(eyePosition, point).WorldOnly().Radius(1f).Run().EndPosition.AlmostEqual(point, 5f);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0002D331 File Offset: 0x0002B531
		public void Gib()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.GibEffect(this.Position);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Delete();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasGibbed = true;
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0002D35C File Offset: 0x0002B55C
		[ClientRpc]
		public virtual void GibEffect(Vector3 pos)
		{
			if (!this.GibEffect__RpcProxy(pos, null))
			{
				return;
			}
			int numGibs = Rand.Int(12, 22);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ClientsideGib.DoGibEffect(pos, numGibs, 300f, 1500f, 20f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create("particles/stalker/anomalies/grav_gore_crush.vpcf", pos);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0002D3B4 File Offset: 0x0002B5B4
		public void OnHitByBolt(BoltProjectile bolt)
		{
			if (ServerSettings.stalker_setting_bolt_mode)
			{
				this.Gib();
				return;
			}
			this.TakeDamage(DamageInfo.Generic(1f).WithAttacker(bolt.Owner, null).WithFlag(DamageFlags.PhysicsImpact));
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0002D3FB File Offset: 0x0002B5FB
		[Input(Name = "Hide")]
		public void Hide()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableDrawing = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableAllCollisions = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableHitboxes = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnHidden();
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0002D42C File Offset: 0x0002B62C
		protected virtual void OnHidden()
		{
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0002D42E File Offset: 0x0002B62E
		protected virtual void OnRevealed()
		{
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0002D430 File Offset: 0x0002B630
		[Input(Name = "Reveal")]
		public void Reveal()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableDrawing = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableAllCollisions = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableHitboxes = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnRevealed();
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0002D461 File Offset: 0x0002B661
		[Input(Name = "Set Active")]
		public void SetActive(bool active)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Active = active;
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0002D46F File Offset: 0x0002B66F
		[Input(Name = "Set Target")]
		public void TrySetTarget(Entity target)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetingComponent.TrySetTarget(target);
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0002D483 File Offset: 0x0002B683
		[Input(Name = "Set Target and Activate")]
		public void TrySetTargetAndActivate(Entity target)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetActive(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetingComponent.TrySetTarget(target);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0002D4A4 File Offset: 0x0002B6A4
		[Input(Name = "Follow Path")]
		public void FollowPath(string pathEntity)
		{
			Entity path = Entity.FindByName(pathEntity, null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SM.SetState(this.GenericFollowPathState);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.GenericFollowPathState.SetPath(path as GenericPathEntity);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0002D4E8 File Offset: 0x0002B6E8
		protected bool GibEffect__RpcProxy(Vector3 pos, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("GibEffect", new object[]
				{
					pos
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-1459005357, this))
			{
				if (!NetRead.IsSupported(pos))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] GibEffect is not allowed to use Vector3 for the parameter 'pos'!");
					return false;
				}
				writer.Write<Vector3>(pos);
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0002D57C File Offset: 0x0002B77C
		public virtual void GibEffect(To toTarget, Vector3 pos)
		{
			this.GibEffect__RpcProxy(pos, new To?(toTarget));
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0002D58C File Offset: 0x0002B78C
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id == -1459005357)
			{
				Vector3 __pos = read.ReadData<Vector3>(default(Vector3));
				if (!Prediction.WasPredicted("GibEffect", new object[]
				{
					__pos
				}))
				{
					this.GibEffect(__pos);
				}
				return;
			}
			base.OnCallRemoteProcedure(id, read);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0002D5DD File Offset: 0x0002B7DD
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarUnmanaged<bool>>(ref this._repback__Active, "Active", false, false);
			builder.Register<VarUnmanaged<bool>>(ref this._repback__Hidden, "Hidden", false, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x04000342 RID: 834
		private TimeSince TimeSinceLastPainSound = 0f;

		// Token: 0x04000343 RID: 835
		private TimeSince TimeSinceLastIdleSound = 0f;

		// Token: 0x04000344 RID: 836
		public TimeSince TimeSinceLastStuck = 0f;

		// Token: 0x04000345 RID: 837
		public bool IsStuck;

		// Token: 0x04000346 RID: 838
		public NavSteer Steer;

		// Token: 0x04000347 RID: 839
		public Vector3 InputVelocity;

		// Token: 0x04000348 RID: 840
		public NPCMovementBase Movement;

		// Token: 0x04000349 RID: 841
		public GenericFollowState GenericFollowState;

		// Token: 0x0400034A RID: 842
		public GenericFollowPathState GenericFollowPathState;

		// Token: 0x0400034B RID: 843
		private DamageInfo lastDamageTaken;

		// Token: 0x0400034C RID: 844
		protected string AttackEventName = "NPCAttackEvent";

		// Token: 0x0400034D RID: 845
		private bool HasGibbed;

		// Token: 0x0400034E RID: 846
		private VarUnmanaged<bool> _repback__Active = new VarUnmanaged<bool>(true);

		// Token: 0x0400034F RID: 847
		private VarUnmanaged<bool> _repback__Hidden = new VarUnmanaged<bool>(false);
	}
}
