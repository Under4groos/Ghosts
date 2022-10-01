using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;

namespace StalkerRP
{
	// Token: 0x02000031 RID: 49
	[Title("Player")]
	[Icon("emoji_people")]
	[Description("This is what you should derive your player from. This base exists in addon code so we can take advantage of codegen for replication. The side effect is that we can put stuff in here that we don't need to access from the engine - which gives more transparency to our code.")]
	public class Player : AnimatedEntity
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000AE3D File Offset: 0x0000903D
		// (set) Token: 0x06000194 RID: 404 RVA: 0x0000AE4A File Offset: 0x0000904A
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

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000AE58 File Offset: 0x00009058
		// (set) Token: 0x06000196 RID: 406 RVA: 0x0000AE65 File Offset: 0x00009065
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

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000AE73 File Offset: 0x00009073
		// (set) Token: 0x06000198 RID: 408 RVA: 0x0000AE8C File Offset: 0x0000908C
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

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000AEAD File Offset: 0x000090AD
		// (set) Token: 0x0600019A RID: 410 RVA: 0x0000AEB5 File Offset: 0x000090B5
		public IBaseInventory Inventory { get; protected set; }

		// Token: 0x0600019B RID: 411 RVA: 0x0000AEBE File Offset: 0x000090BE
		[Description("Return the controller to use. Remember any logic you use here needs to match on both client and server. This is called as an accessor every tick.. so maybe avoid creating new classes here or you're gonna be making a ton of garbage!")]
		public virtual PawnController GetActiveController()
		{
			if (this.DevController != null)
			{
				return this.DevController;
			}
			return this.Controller;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000AED5 File Offset: 0x000090D5
		// (set) Token: 0x0600019D RID: 413 RVA: 0x0000AEE2 File Offset: 0x000090E2
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

		// Token: 0x0600019E RID: 414 RVA: 0x0000AEF0 File Offset: 0x000090F0
		[Description("Return the controller to use. Remember any logic you use here needs to match on both client and server. This is called as an accessor every tick.. so maybe avoid creating new classes here or you're gonna be making a ton of garbage!")]
		public virtual PawnAnimator GetActiveAnimator()
		{
			return this.Animator;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000AEF8 File Offset: 0x000090F8
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

		// Token: 0x060001A0 RID: 416 RVA: 0x0000AF54 File Offset: 0x00009154
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

		// Token: 0x060001A1 RID: 417 RVA: 0x0000AFE0 File Offset: 0x000091E0
		[ClientRpc]
		public void Deafen(float strength)
		{
			if (!this.Deafen__RpcProxy(strength, null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Audio.SetEffect("flashbang", strength, 20f, 4f * strength);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000B021 File Offset: 0x00009221
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableLagCompensation = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("player");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000B050 File Offset: 0x00009250
		[Description("Called once the player's health reaches 0")]
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

		// Token: 0x060001A4 RID: 420 RVA: 0x0000B0BC File Offset: 0x000092BC
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

		// Token: 0x060001A5 RID: 421 RVA: 0x0000B140 File Offset: 0x00009340
		[Description("Create a physics hull for this player. The hull stops physics objects and players passing through the player. It's basically a big solid box. It also what hits triggers and stuff. The player doesn't use this hull for its movement size.")]
		public virtual void CreateHull()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetupPhysicsFromAABB(PhysicsMotionType.Keyframed, new Vector3(-16f, -16f, 0f), new Vector3(16f, 16f, 72f));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableHitboxes = true;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000B190 File Offset: 0x00009390
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

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000B1F3 File Offset: 0x000093F3
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x0000B1FB File Offset: 0x000093FB
		[Description("A generic corpse entity")]
		public ModelEntity Corpse { get; set; }

		// Token: 0x060001A9 RID: 425 RVA: 0x0000B204 File Offset: 0x00009404
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

		// Token: 0x060001AA RID: 426 RVA: 0x0000B230 File Offset: 0x00009430
		[Description("A foostep has arrived!")]
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

		// Token: 0x060001AB RID: 427 RVA: 0x0000B2EC File Offset: 0x000094EC
		public virtual float FootstepVolume()
		{
			return this.Velocity.WithZ(0f).Length.LerpInverse(0f, 200f, true) * 0.2f;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000B32C File Offset: 0x0000952C
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

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000B381 File Offset: 0x00009581
		// (set) Token: 0x060001AE RID: 430 RVA: 0x0000B389 File Offset: 0x00009589
		[Predicted]
		[Description("This isn't networked, but it's predicted. If it wasn't then when the prediction system re-ran the commands LastActiveChild would be the value set in a future tick, so ActiveEnd and ActiveStart would get called mulitple times and out of order, causing all kinds of pain.")]
		private Entity LastActiveChild { get; set; }

		// Token: 0x060001AF RID: 431 RVA: 0x0000B394 File Offset: 0x00009594
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

		// Token: 0x060001B0 RID: 432 RVA: 0x0000B3F4 File Offset: 0x000095F4
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

		// Token: 0x060001B1 RID: 433 RVA: 0x0000B440 File Offset: 0x00009640
		public override void TakeDamage(DamageInfo info)
		{
			if (base.LifeState == LifeState.Dead)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.LastAttacker = info.Attacker;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.LastAttackerWeapon = info.Weapon;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ProceduralHitReaction(info, 1f);
			if (info.Flags.HasFlag(DamageFlags.Blast))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Deafen(To.Single(base.Client), info.Damage.LerpInverse(0f, 60f, true));
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000B4D3 File Offset: 0x000096D3
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

		// Token: 0x060001B3 RID: 435 RVA: 0x0000B4EB File Offset: 0x000096EB
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

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000B503 File Offset: 0x00009703
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x0000B511 File Offset: 0x00009711
		[Description("Provides an easy way to switch our current cameramode component")]
		public StalkerBaseCamera Camera
		{
			get
			{
				return this.Components.Get<StalkerBaseCamera>(false);
			}
			set
			{
				this.Components.Add<StalkerBaseCamera>(value);
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000B520 File Offset: 0x00009720
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
			using (NetWrite writer = NetWrite.StartRpc(-1141587297, this))
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

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000B5B4 File Offset: 0x000097B4
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x0000B5BC File Offset: 0x000097BC
		public Entity Using { get; protected set; }

		// Token: 0x060001B9 RID: 441 RVA: 0x0000B5C8 File Offset: 0x000097C8
		[Description("This should be called somewhere in your player's tick to allow them to use entities")]
		protected virtual void TickPlayerUse()
		{
			if (!Host.IsServer)
			{
				return;
			}
			using (Prediction.Off())
			{
				if (Input.Pressed(InputButton.Flashlight))
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
				if (!Input.Down(InputButton.Flashlight))
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

		// Token: 0x060001BA RID: 442 RVA: 0x0000B680 File Offset: 0x00009880
		[Description("Player tried to use something but there was nothing there. Tradition is to give a dissapointed boop.")]
		protected virtual void UseFail()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PlaySound("player_use_fail");
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000B693 File Offset: 0x00009893
		[Description("If we're using an entity, stop using it")]
		protected virtual void StopUsing()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Using = null;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000B6A4 File Offset: 0x000098A4
		[Description("Returns if the entity is a valid usaable entity")]
		protected bool IsValidUseEntity(Entity e)
		{
			if (e == null)
			{
				return false;
			}
			IUse use = e as IUse;
			return use != null && use.IsUsable(this);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000B6D0 File Offset: 0x000098D0
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

		// Token: 0x060001BE RID: 446 RVA: 0x0000B7F7 File Offset: 0x000099F7
		public void Deafen(To toTarget, float strength)
		{
			this.Deafen__RpcProxy(strength, new To?(toTarget));
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000B808 File Offset: 0x00009A08
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id == -1141587297)
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

		// Token: 0x060001C0 RID: 448 RVA: 0x0000B858 File Offset: 0x00009A58
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarClass<PawnController>>(ref this._repback__Controller, "Controller", true, false);
			builder.Register<VarClass<PawnController>>(ref this._repback__DevController, "DevController", true, false);
			builder.Register<VarUnmanaged<EntityHandle<Entity>>>(ref this._repback__ActiveChild, "ActiveChild", true, false);
			builder.Register<VarClass<PawnAnimator>>(ref this._repback__Animator, "Animator", true, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x04000097 RID: 151
		private TimeSince timeSinceDied;

		// Token: 0x04000099 RID: 153
		private TimeSince timeSinceLastFootstep = 0f;

		// Token: 0x0400009C RID: 156
		private VarClass<PawnController> _repback__Controller = new VarClass<PawnController>();

		// Token: 0x0400009D RID: 157
		private VarClass<PawnController> _repback__DevController = new VarClass<PawnController>();

		// Token: 0x0400009E RID: 158
		private VarUnmanaged<EntityHandle<Entity>> _repback__ActiveChild = new VarUnmanaged<EntityHandle<Entity>>();

		// Token: 0x0400009F RID: 159
		private VarClass<PawnAnimator> _repback__Animator = new VarClass<PawnAnimator>();
	}
}
