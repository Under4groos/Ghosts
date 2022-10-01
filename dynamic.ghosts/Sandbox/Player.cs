using System;
using System.Runtime.CompilerServices;
using Sandbox.Internal;

namespace Sandbox
{
	// Token: 0x02000163 RID: 355
	[Title("Player")]
	[Icon("emoji_people")]
	[Description("This is what you should derive your player from. This base exists in addon code so we can take advantage of codegen for replication. The side effect is that we can put stuff in here that we don't need to access from the engine - which gives more transparency to our code.")]
	public class Player : AnimatedEntity
	{
		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x0003F904 File Offset: 0x0003DB04
		// (set) Token: 0x06001018 RID: 4120 RVA: 0x0003F911 File Offset: 0x0003DB11
		[Net]
		[Predicted]
		[Description("The PlayerController takes player input and moves the player. This needs to match between client and server. The client moves the local player and then checks that when the server moves the player, everything is the same. This is called prediction. If it doesn't match the player resets everything to what the server did, that's a prediction error. You should really never manually set this on the client - it's replicated so that setting the class on the server will automatically network and set it on the client.")]
		public PawnController Controller
		{
			get
			{
				return this._repback__Controller.GetValue();
			}
			set
			{
				this._repback__Controller.SetValue(value);
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x0003F91F File Offset: 0x0003DB1F
		// (set) Token: 0x0600101A RID: 4122 RVA: 0x0003F92C File Offset: 0x0003DB2C
		[Net]
		[Predicted]
		[Description("This is used for noclip mode")]
		public PawnController DevController
		{
			get
			{
				return this._repback__DevController.GetValue();
			}
			set
			{
				this._repback__DevController.SetValue(value);
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x0003F93A File Offset: 0x0003DB3A
		// (set) Token: 0x0600101C RID: 4124 RVA: 0x0003F954 File Offset: 0x0003DB54
		[Net]
		[Predicted]
		[Description("The active weapon, or tool, or whatever else")]
		public unsafe Entity ActiveChild
		{
			get
			{
				return *this._repback__ActiveChild.GetValue();
			}
			set
			{
				VarUnmanaged<EntityHandle<Entity>> repback__ActiveChild = this._repback__ActiveChild;
				EntityHandle<Entity> entityHandle = value;
				repback__ActiveChild.SetValue(entityHandle);
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x0600101D RID: 4125 RVA: 0x0003F975 File Offset: 0x0003DB75
		// (set) Token: 0x0600101E RID: 4126 RVA: 0x0003F97D File Offset: 0x0003DB7D
		[Description("Player's inventory for entities that can be carried. See <see cref=\"T:Sandbox.BaseCarriable\" />.")]
		public IBaseInventory Inventory { get; protected set; }

		// Token: 0x0600101F RID: 4127 RVA: 0x0003F986 File Offset: 0x0003DB86
		[Description("Return the controller to use. Remember any logic you use here needs to match on both client and server. This is called as an accessor every tick.. so maybe avoid creating new classes here or you're gonna be making a ton of garbage!")]
		public virtual PawnController GetActiveController()
		{
			if (this.DevController != null)
			{
				return this.DevController;
			}
			return this.Controller;
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x0003F99D File Offset: 0x0003DB9D
		// (set) Token: 0x06001021 RID: 4129 RVA: 0x0003F9AA File Offset: 0x0003DBAA
		[Net]
		[Predicted]
		[Description("The player animator is responsible for positioning/rotating the player and interacting with the animation graph.")]
		public PawnAnimator Animator
		{
			get
			{
				return this._repback__Animator.GetValue();
			}
			set
			{
				this._repback__Animator.SetValue(value);
			}
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0003F9B8 File Offset: 0x0003DBB8
		[Description("Return the controller to use. Remember any logic you use here needs to match on both client and server. This is called as an accessor every tick.. so maybe avoid creating new classes here or you're gonna be making a ton of garbage!")]
		public virtual PawnAnimator GetActiveAnimator()
		{
			return this.Animator;
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x0003F9C0 File Offset: 0x0003DBC0
		[Description("Called every tick to simulate the player. This is called on the client as well as the server (for prediction). So be careful!")]
		public override void Simulate(Client cl)
		{
			if (base.LifeState == LifeState.Dead)
			{
				if (this.timeSinceDied > 3f && base.IsServer)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Respawn();
				}
				return;
			}
			PawnController activeController = this.GetActiveController();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			if (activeController == null)
			{
				return;
			}
			activeController.Simulate(cl, this, this.GetActiveAnimator());
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0003FA1C File Offset: 0x0003DC1C
		public override void FrameSimulate(Client cl)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.FrameSimulate(cl);
			PawnController activeController = this.GetActiveController();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			if (activeController != null)
			{
				activeController.FrameSimulate(cl, this, this.GetActiveAnimator());
			}
			if (base.WaterLevel > 0.9f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Audio.SetEffect("underwater", 1f, 5f, -1f);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Audio.SetEffect("underwater", 0f, 1f, -1f);
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0003FAA8 File Offset: 0x0003DCA8
		[ClientRpc]
		[Description("Applies flashbang-like ear ringing effect to the player.")]
		public void Deafen(float strength)
		{
			if (!this.Deafen__RpcProxy(strength, null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Audio.SetEffect("flashbang", strength, 20f, 4f * strength);
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0003FAE9 File Offset: 0x0003DCE9
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableLagCompensation = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("player");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0003FB18 File Offset: 0x0003DD18
		[Description("Called once the player's health reaches 0.")]
		public override void OnKilled()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Game game = Game.Current;
			if (game != null)
			{
				game.OnKilled(this);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceDied = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.LifeState = LifeState.Dead;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StopUsing();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Client client = base.Client;
			if (client == null)
			{
				return;
			}
			client.AddInt("deaths", 1);
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0003FB84 File Offset: 0x0003DD84
		[Description("Sets LifeState to Alive, Health to Max, nulls velocity, and calls Gamemode.PlayerRespawn")]
		public virtual void Respawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertServer("Respawn");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.LifeState = LifeState.Alive;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Health = 100f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Velocity = Vector3.Zero;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.WaterLevel = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateHull();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Game game = Game.Current;
			if (game != null)
			{
				game.MoveToSpawnpoint(this);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ResetInterpolation();
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0003FC08 File Offset: 0x0003DE08
		[Description("Create a physics hull for this player. The hull stops physics objects and players passing through the player. It's basically a big solid box. It also what hits triggers and stuff. The player doesn't use this hull for its movement size.")]
		public virtual void CreateHull()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetupPhysicsFromAABB(PhysicsMotionType.Keyframed, new Vector3(-16f, -16f, 0f), new Vector3(16f, 16f, 72f));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableHitboxes = true;
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0003FC58 File Offset: 0x0003DE58
		[Description("Called from the gamemode, clientside only.")]
		public override void BuildInput(InputBuilder input)
		{
			if (input.StopProcessing)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Entity activeChild = this.ActiveChild;
			if (activeChild != null)
			{
				activeChild.BuildInput(input);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			PawnController activeController = this.GetActiveController();
			if (activeController != null)
			{
				activeController.BuildInput(input);
			}
			if (input.StopProcessing)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			PawnAnimator activeAnimator = this.GetActiveAnimator();
			if (activeAnimator == null)
			{
				return;
			}
			activeAnimator.BuildInput(input);
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x0003FCBB File Offset: 0x0003DEBB
		// (set) Token: 0x0600102C RID: 4140 RVA: 0x0003FCC3 File Offset: 0x0003DEC3
		[Description("A generic corpse entity")]
		public ModelEntity Corpse { get; set; }

		// Token: 0x0600102D RID: 4141 RVA: 0x0003FCCC File Offset: 0x0003DECC
		[Description("Called after the camera setup logic has run. Allow the player to do stuff to the camera, or using the camera. Such as positioning entities relative to it, like viewmodels etc.")]
		public override void PostCameraSetup(ref CameraSetup setup)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertClient("PostCameraSetup");
			if (this.ActiveChild != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ActiveChild.PostCameraSetup(ref setup);
			}
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0003FCF8 File Offset: 0x0003DEF8
		[Description("A footstep has arrived!")]
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
			if (this.timeSinceLastFootstep < 0.2f)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			volume *= this.FootstepVolume();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastFootstep = 0f;
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
			tr.Surface.DoFootstep(this, tr, foot, volume);
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0003FDB4 File Offset: 0x0003DFB4
		[Description("Allows override of footstep sound volume.")]
		public virtual float FootstepVolume()
		{
			return this.Velocity.WithZ(0f).Length.LerpInverse(0f, 200f, true) * 0.2f;
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0003FDF4 File Offset: 0x0003DFF4
		public override void StartTouch(Entity other)
		{
			if (base.IsClient)
			{
				return;
			}
			if (other is PickupTrigger)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.StartTouch(other.Parent);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			IBaseInventory inventory = this.Inventory;
			if (inventory == null)
			{
				return;
			}
			inventory.Add(other, this.Inventory.Active == null);
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x0003FE49 File Offset: 0x0003E049
		// (set) Token: 0x06001032 RID: 4146 RVA: 0x0003FE51 File Offset: 0x0003E051
		[Predicted]
		[Description("This isn't networked, but it's predicted. If it wasn't then when the prediction system re-ran the commands LastActiveChild would be the value set in a future tick, so ActiveEnd and ActiveStart would get called multiple times and out of order, causing all kinds of pain.")]
		private Entity LastActiveChild { get; set; }

		// Token: 0x06001033 RID: 4147 RVA: 0x0003FE5C File Offset: 0x0003E05C
		[Description("Simulated the active child. This is important because it calls ActiveEnd and ActiveStart. If you don't call these things, viewmodels and stuff won't work, because the entity won't know it's become the active entity.")]
		public virtual void SimulateActiveChild(Client cl, Entity child)
		{
			if (this.LastActiveChild != child)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnActiveChildChanged(this.LastActiveChild, child);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.LastActiveChild = child;
			}
			if (!this.LastActiveChild.IsValid())
			{
				return;
			}
			if (this.LastActiveChild.IsAuthority)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.LastActiveChild.Simulate(cl);
			}
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0003FEBC File Offset: 0x0003E0BC
		[Description("Called when the Active child is detected to have changed")]
		public virtual void OnActiveChildChanged(Entity previous, Entity next)
		{
			BaseCarriable previousBc = previous as BaseCarriable;
			if (previousBc != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				if (previousBc != null)
				{
					previousBc.ActiveEnd(this, previousBc.Owner != this);
				}
			}
			BaseCarriable nextBc = next as BaseCarriable;
			if (nextBc != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				if (nextBc != null)
				{
					nextBc.ActiveStart(this);
				}
			}
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0003FF08 File Offset: 0x0003E108
		public override void TakeDamage(DamageInfo info)
		{
			if (base.LifeState == LifeState.Dead)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.TakeDamage(info);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ProceduralHitReaction(info, 1f);
			if (base.LifeState == LifeState.Dead && info.Attacker != null && info.Attacker.Client != null && info.Attacker != this)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				info.Attacker.Client.AddInt("kills", 1);
			}
			if (info.Flags.HasFlag(DamageFlags.Blast))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Deafen(To.Single(base.Client), info.Damage.LerpInverse(0f, 60f, true));
			}
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0003FFC9 File Offset: 0x0003E1C9
		public override void OnChildAdded(Entity child)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			IBaseInventory inventory = this.Inventory;
			if (inventory == null)
			{
				return;
			}
			inventory.OnChildAdded(child);
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0003FFE1 File Offset: 0x0003E1E1
		public override void OnChildRemoved(Entity child)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			IBaseInventory inventory = this.Inventory;
			if (inventory == null)
			{
				return;
			}
			inventory.OnChildRemoved(child);
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x0003FFF9 File Offset: 0x0003E1F9
		// (set) Token: 0x06001039 RID: 4153 RVA: 0x00040007 File Offset: 0x0003E207
		[Description("Provides an easy way to switch our current <see cref=\"P:Sandbox.Player.CameraMode\" /> component.")]
		public CameraMode CameraMode
		{
			get
			{
				return this.Components.Get<CameraMode>(false);
			}
			set
			{
				this.Components.Add<CameraMode>(value);
			}
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00040018 File Offset: 0x0003E218
		protected bool Deafen__RpcProxy(float strength, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("Deafen", new object[]
				{
					strength
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-296764369, this))
			{
				if (!NetRead.IsSupported(strength))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] Deafen is not allowed to use Single for the parameter 'strength'!");
					return false;
				}
				writer.Write<float>(strength);
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x000400AC File Offset: 0x0003E2AC
		// (set) Token: 0x0600103C RID: 4156 RVA: 0x000400B4 File Offset: 0x0003E2B4
		[Description("Entity the player is currently using via their interaction key.")]
		public Entity Using { get; protected set; }

		// Token: 0x0600103D RID: 4157 RVA: 0x000400C0 File Offset: 0x0003E2C0
		[Description("This should be called somewhere in your player's tick to allow them to use entities")]
		protected virtual void TickPlayerUse()
		{
			if (!Host.IsServer)
			{
				return;
			}
			using (Prediction.Off())
			{
				if (Input.Pressed(InputButton.Use))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Using = this.FindUsable();
					if (this.Using == null)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.UseFail();
						return;
					}
				}
				if (!Input.Down(InputButton.Use))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.StopUsing();
				}
				else if (this.Using.IsValid())
				{
					IUse use = this.Using as IUse;
					if (use == null || !use.OnUse(this))
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.StopUsing();
					}
				}
			}
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00040178 File Offset: 0x0003E378
		[Description("Player tried to use something but there was nothing there. Tradition is to give a disappointed boop.")]
		protected virtual void UseFail()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PlaySound("player_use_fail");
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0004018B File Offset: 0x0003E38B
		[Description("If we're using an entity, stop using it")]
		protected virtual void StopUsing()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Using = null;
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0004019C File Offset: 0x0003E39C
		[Description("Returns if the entity is a valid usable entity")]
		protected bool IsValidUseEntity(Entity e)
		{
			if (e == null)
			{
				return false;
			}
			IUse use = e as IUse;
			return use != null && use.IsUsable(this);
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x000401C8 File Offset: 0x0003E3C8
		[Description("Find a usable entity for this player to use")]
		protected virtual Entity FindUsable()
		{
			Vector3 eyePosition = base.EyePosition;
			Vector3 vector = base.EyePosition + base.EyeRotation.Forward * 85f;
			Trace trace = Trace.Ray(eyePosition, vector);
			Entity entity = this;
			bool flag = true;
			Entity ent = trace.Ignore(entity, flag).Run().Entity;
			while (ent.IsValid() && !this.IsValidUseEntity(ent))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				ent = ent.Parent;
			}
			if (!this.IsValidUseEntity(ent))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				eyePosition = base.EyePosition;
				vector = base.EyePosition + base.EyeRotation.Forward * 85f;
				trace = Trace.Ray(eyePosition, vector).Radius(2f);
				entity = this;
				flag = true;
				ref TraceResult ptr = trace.Ignore(entity, flag).Run();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				ent = ptr.Entity;
				while (ent.IsValid() && !this.IsValidUseEntity(ent))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					ent = ent.Parent;
				}
			}
			if (!this.IsValidUseEntity(ent))
			{
				return null;
			}
			return ent;
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x000402EF File Offset: 0x0003E4EF
		public void Deafen(To toTarget, float strength)
		{
			this.Deafen__RpcProxy(strength, new To?(toTarget));
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x00040300 File Offset: 0x0003E500
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id == -296764369)
			{
				float __strength = 0f;
				__strength = read.ReadData<float>(__strength);
				if (!Prediction.WasPredicted("Deafen", new object[]
				{
					__strength
				}))
				{
					this.Deafen(__strength);
				}
				return;
			}
			base.OnCallRemoteProcedure(id, read);
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00040350 File Offset: 0x0003E550
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarClass<PawnController>>(ref this._repback__Controller, "Controller", true, false);
			builder.Register<VarClass<PawnController>>(ref this._repback__DevController, "DevController", true, false);
			builder.Register<VarUnmanaged<EntityHandle<Entity>>>(ref this._repback__ActiveChild, "ActiveChild", true, false);
			builder.Register<VarClass<PawnAnimator>>(ref this._repback__Animator, "Animator", true, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x0400050B RID: 1291
		private TimeSince timeSinceDied;

		// Token: 0x0400050D RID: 1293
		private TimeSince timeSinceLastFootstep = 0f;

		// Token: 0x04000510 RID: 1296
		private VarClass<PawnController> _repback__Controller = new VarClass<PawnController>();

		// Token: 0x04000511 RID: 1297
		private VarClass<PawnController> _repback__DevController = new VarClass<PawnController>();

		// Token: 0x04000512 RID: 1298
		private VarUnmanaged<EntityHandle<Entity>> _repback__ActiveChild = new VarUnmanaged<EntityHandle<Entity>>();

		// Token: 0x04000513 RID: 1299
		private VarClass<PawnAnimator> _repback__Animator = new VarClass<PawnAnimator>();
	}
}
