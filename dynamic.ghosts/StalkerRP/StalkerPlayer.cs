using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.camera;
using Sandbox.Debug.settings;
using Sandbox.Internal;
using StalkerRP.Inventory;
using StalkerRP.Inventory.Gear;
using StalkerRP.NPC;
using StalkerRP.PostProcessing;
using StalkerRP.UI;

namespace StalkerRP
{
	// Token: 0x02000042 RID: 66
	[NullableContext(1)]
	[Nullable(0)]
	public class StalkerPlayer : Player, IBoltable
	{
		// Token: 0x06000296 RID: 662 RVA: 0x0000F6E4 File Offset: 0x0000D8E4
		[NullableContext(0)]
		public void OnHitByBolt(BoltProjectile bolt)
		{
			if (bolt.HasTouchedWorld || bolt.Owner == this)
			{
				return;
			}
			if (ServerSettings.stalker_setting_bolt_mode)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Gib();
				return;
			}
			DamageInfo damage = DamageInfo.Generic(7f).WithFlag(DamageFlags.Cook);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HealthComponent.TakeDamage(damage);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000F73F File Offset: 0x0000D93F
		public void Gib()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.hasGibbed = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.GibEffect(this.Position);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnKilled();
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000F76C File Offset: 0x0000D96C
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

		// Token: 0x06000299 RID: 665 RVA: 0x0000F7C4 File Offset: 0x0000D9C4
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
			using (NetWrite writer = NetWrite.StartRpc(-2131026325, this))
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

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000F858 File Offset: 0x0000DA58
		[BindComponent]
		public PsyHealthComponent PsyHealthComponent
		{
			get
			{
				return this.Components.Get<PsyHealthComponent>(false);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000F866 File Offset: 0x0000DA66
		[BindComponent]
		public BleedComponent BleedComponent
		{
			get
			{
				return this.Components.Get<BleedComponent>(false);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000F874 File Offset: 0x0000DA74
		[BindComponent]
		public HealthComponent HealthComponent
		{
			get
			{
				return this.Components.Get<HealthComponent>(false);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000F882 File Offset: 0x0000DA82
		[BindComponent]
		public InventoryComponent InventoryComponent
		{
			get
			{
				return this.Components.Get<InventoryComponent>(false);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000F890 File Offset: 0x0000DA90
		[BindComponent]
		public EquipmentComponent EquipmentComponent
		{
			get
			{
				return this.Components.Get<EquipmentComponent>(false);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000F89E File Offset: 0x0000DA9E
		[BindComponent]
		public TargetComponent TargetComponent
		{
			get
			{
				return this.Components.Get<TargetComponent>(false);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000F8AC File Offset: 0x0000DAAC
		[BindComponent]
		public HeadGearComponent HeadGearComponent
		{
			get
			{
				return this.Components.Get<HeadGearComponent>(false);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000F8BA File Offset: 0x0000DABA
		[BindComponent]
		public VoiceSFXComponent VoiceSFXComponent
		{
			get
			{
				return this.Components.Get<VoiceSFXComponent>(false);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000F8C8 File Offset: 0x0000DAC8
		[BindComponent]
		public BulletWhizzComponent BulletWhizzComponent
		{
			get
			{
				return this.Components.Get<BulletWhizzComponent>(false);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000F8D6 File Offset: 0x0000DAD6
		[BindComponent]
		public HeadMovementComponent HeadMovementComponent
		{
			get
			{
				return this.Components.Get<HeadMovementComponent>(false);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000F8E4 File Offset: 0x0000DAE4
		[BindComponent]
		public StaminaComponent StaminaComponent
		{
			get
			{
				return this.Components.Get<StaminaComponent>(false);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000F8F2 File Offset: 0x0000DAF2
		[BindComponent]
		public LeanComponent LeanComponent
		{
			get
			{
				return this.Components.Get<LeanComponent>(false);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000F900 File Offset: 0x0000DB00
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x0000F908 File Offset: 0x0000DB08
		public PlayerStatsResource Stats { get; set; } = StalkerResource.Get<PlayerStatsResource>("default");

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000F911 File Offset: 0x0000DB11
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x0000F919 File Offset: 0x0000DB19
		public TimeSince TimeSinceStartedRunning { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000F922 File Offset: 0x0000DB22
		// (set) Token: 0x060002AB RID: 683 RVA: 0x0000F930 File Offset: 0x0000DB30
		[Net]
		[Predicted]
		public unsafe bool InSights
		{
			get
			{
				return *this._repback__InSights.GetValue();
			}
			set
			{
				this._repback__InSights.SetValue(value);
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000F940 File Offset: 0x0000DB40
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("player");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Components.Create<PsyHealthComponent>(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Components.Create<BleedComponent>(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Components.Create<HealthComponent>(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Components.Create<InventoryComponent>(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Components.Create<EquipmentComponent>(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Components.Create<TargetComponent>(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Components.Create<HeadGearComponent>(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Components.Create<VoiceSFXComponent>(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Components.Create<BulletWhizzComponent>(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Components.Create<HeadMovementComponent>(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Components.Create<StaminaComponent>(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Components.Create<LeanComponent>(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetComponent.Identity = CreatureIdentity.Player;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.EquipmentComponent.Initialise();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SurroundingBoundsMode = SurroundingBoundsType.Hitboxes;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000FA74 File Offset: 0x0000DC74
		public override void Respawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.InSights = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.hasGibbed = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.BleedComponent.Reset();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HealthComponent.Reset();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PsyHealthComponent.Reset();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StaminaComponent.Reset();
			if (!this.firstSpawn)
			{
				this.ResetPlayerInventory();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.firstSpawn = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetModel("models/player/stalker_dolg/stalker_dolg.vmdl");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Controller = new StalkerWalkController();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Animator = new StandardPlayerAnimator();
			if (base.DevController is NoclipController)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.DevController = null;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableAllCollisions = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableDrawing = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableHideInFirstPerson = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableShadowInFirstPerson = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Camera = new FirstPersonCamera();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnRespawnClient(To.Single(base.Client));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Respawn();
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000FB9D File Offset: 0x0000DD9D
		public override void ClientSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetUpFirstPersonLegs();
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000FBAA File Offset: 0x0000DDAA
		public override PawnController GetActiveController()
		{
			if (base.DevController != null)
			{
				return base.DevController;
			}
			return base.GetActiveController();
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000FBC4 File Offset: 0x0000DDC4
		public override void Simulate(Client cl)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Simulate(cl);
			if (Input.ActiveChild != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.ActiveChild = Input.ActiveChild;
			}
			if (base.LifeState != LifeState.Alive)
			{
				return;
			}
			PawnController controller = this.GetActiveController();
			if (controller != null)
			{
				base.EnableSolidCollisions = !controller.HasTag("noclip");
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Animator.SetAnimParameter("b_sprint", this.StaminaComponent.IsRunning);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.EquipmentComponent.Simulate(cl);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TickPlayerUse();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SimulateActiveChild(cl, base.ActiveChild);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HeadGearComponent.Simulate(cl);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.VoiceSFXComponent.Simulate(cl);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StaminaComponent.Simulate(cl);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HealthComponent.Simulate(cl);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PsyHealthComponent.Simulate(cl);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LeanComponent.Simulate(cl);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000FCCF File Offset: 0x0000DECF
		public override void OnClientActive(Client client)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.GenerateDefaultInventory();
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000FCDC File Offset: 0x0000DEDC
		[ConCmd.ServerAttribute("inventory_current")]
		public static void SetInventoryCurrent(string entName)
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("inventory_current", new object[]
				{
					entName
				});
				return;
			}
			Player target = ConsoleSystem.Caller.Pawn as Player;
			if (target == null)
			{
				return;
			}
			IBaseInventory inventory = target.Inventory;
			if (inventory == null)
			{
				return;
			}
			for (int i = 0; i < inventory.Count(); i++)
			{
				Entity slot = inventory.GetSlot(i);
				if (slot.IsValid() && !(slot.ClassName != entName))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					inventory.SetActiveSlot(i, false);
					return;
				}
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000FD64 File Offset: 0x0000DF64
		public override float FootstepVolume()
		{
			float speed = this.Velocity.WithZ(0f).Length;
			if (speed <= 120f)
			{
				return 0f;
			}
			float value = speed.LerpInverse(0f, 150f, false) * 20f;
			if (base.Client == Local.Client)
			{
				return value / 10f;
			}
			return value;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000FDC9 File Offset: 0x0000DFC9
		public Vector3 GetShootPosition()
		{
			return base.EyePosition + this.LeanComponent.GetLeanOffset(base.EyeRotation);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000FDE8 File Offset: 0x0000DFE8
		[ClientRpc]
		public void DoPsyBoltEffect(Entity target)
		{
			if (!this.DoPsyBoltEffect__RpcProxy(target, null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info(target);
			FirstPersonCamera camera = base.Camera as FirstPersonCamera;
			if (camera == null || !target.IsValid())
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			camera.DoPsyBoltZoom(target);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000FE3C File Offset: 0x0000E03C
		[ConCmd.ClientAttribute("stalker_toggle_view")]
		private static void ChangeView()
		{
			if (Host.IsServer)
			{
				throw new Exception("Trying to call ChangeView serverside!");
			}
			StalkerPlayer player = Local.Pawn as StalkerPlayer;
			if (player == null)
			{
				return;
			}
			if (!ServerSettings.stalker_setting_debug_enabled)
			{
				return;
			}
			if (player.Camera is ThirdPersonCamera)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				player.Camera = new FirstPersonCamera();
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			player.Camera = new ThirdPersonCamera();
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000FEA0 File Offset: 0x0000E0A0
		protected bool DoPsyBoltEffect__RpcProxy(Entity target, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("DoPsyBoltEffect", new object[]
				{
					target
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-1936323455, this))
			{
				if (!NetRead.IsSupported(target))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] DoPsyBoltEffect is not allowed to use Entity for the parameter 'target'!");
					return false;
				}
				writer.Write<Entity>(target);
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000FF28 File Offset: 0x0000E128
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000FF30 File Offset: 0x0000E130
		[Nullable(0)]
		public AnimatedEntity FirstPersonBody { [NullableContext(0)] get; [NullableContext(0)] set; }

		// Token: 0x060002BA RID: 698 RVA: 0x0000FF39 File Offset: 0x0000E139
		[Event.HotloadAttribute]
		private void SetUpFirstPersonLegs()
		{
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000FF3B File Offset: 0x0000E13B
		private float hitFlinchCooldown
		{
			get
			{
				return 0.25f;
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000FF44 File Offset: 0x0000E144
		public override void TakeDamage(DamageInfo info)
		{
			if (this.Tags.Has("stalker_godmode"))
			{
				info.Damage = 0f;
			}
			if (this.WasHeadshot(info))
			{
				if (info.Flags.HasFlag(DamageFlags.Bullet))
				{
					Particles particles = Particles.Create("particles/stalker/weapons/bullet_headshot.vpcf", info.Position);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					particles.SetPosition(1, info.Force.Normal);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					Sound.FromWorld("rust.headshot", info.Position);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.wasHeadshot = true;
					if (this.timeUntilHeadDamageCacheReset <= 0f)
					{
						this.headDamageCache = 0f;
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.headDamageCache += info.Damage;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.timeUntilHeadDamageCacheReset = 0.1f;
				}
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.wasHeadshot = false;
				if (info.Flags.HasFlag(DamageFlags.Bullet))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.PlayBulletImpact(To.Single(base.Client));
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.lastDamage = info;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.TakeDamage(info);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ProceduralHitReaction(info, 1f);
			if (info.Flags.HasFlag(DamageFlags.Blast))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Deafen(To.Single(base.Client), info.Damage.LerpInverse(0f, 60f, true));
			}
			if (base.LifeState != LifeState.Alive)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HealthComponent.TakeDamage(info);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StaminaComponent.OnTakeDamage(info);
			if (base.LifeState == LifeState.Dead && info.Attacker != null && info.Attacker.Client != null && info.Attacker != this)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				info.Attacker.Client.AddInt("kills", 1);
			}
			if (info.Attacker is StalkerPlayer)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TookDamage(To.Single(this), info.Weapon.IsValid() ? info.Weapon.Position : info.Attacker.Position);
			}
			if (info.Damage > 2f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DoDamageReactions();
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x000101AE File Offset: 0x0000E3AE
		public void DoDamageReactions()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TryHitFlinch();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.VoiceSFXComponent.OnTakeDamage();
		}

		// Token: 0x060002BE RID: 702 RVA: 0x000101CC File Offset: 0x0000E3CC
		private void TryHitFlinch()
		{
			if (this.timeSinceLastHitFlinch < this.hitFlinchCooldown)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoBulletHitFlinch(To.Single(base.Client));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastHitFlinch = 0f;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00010218 File Offset: 0x0000E418
		public override void OnKilled()
		{
			if (base.LifeState != LifeState.Alive)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.VoiceSFXComponent.OnKilled();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetComponent.RemoveAsValidTarget();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HeadGearComponent.OnPlayerDied();
			if (this.lastDamage.Flags.HasFlag(DamageFlags.Vehicle))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Particles.Create("particles/impact.flesh.bloodpuff-big.vpcf", this.lastDamage.Position);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Particles.Create("particles/impact.flesh-big.vpcf", this.lastDamage.Position);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PlaySound("kersplat");
			}
			if (!this.hasGibbed)
			{
				if (this.WasHeadshot(this.lastDamage) && this.headDamageCache > 50f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.BecomeRagdollOnClientWithHeadExplode(this.Velocity, this.lastDamage.Flags, this.lastDamage.Position, this.lastDamage.Force, this.lastDamage.BoneIndex);
				}
				else
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.BecomeRagdollOnClient(this.Velocity, this.lastDamage.Flags, this.lastDamage.Position, this.lastDamage.Force, this.lastDamage.BoneIndex);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Camera = new FirstPersonDeathCamera();
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Camera = new FloatingCamera();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Controller = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableAllCollisions = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableDrawing = false;
			foreach (Entity entity in base.Children)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				entity.EnableDrawing = false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnKilledClient(To.Single(this));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnKilled();
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0001040C File Offset: 0x0000E60C
		[ClientRpc]
		public void TookDamage(Vector3 pos)
		{
			if (!this.TookDamage__RpcProxy(pos, null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			DamageIndicator current = DamageIndicator.Current;
			if (current == null)
			{
				return;
			}
			current.OnHit(pos);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00010444 File Offset: 0x0000E644
		[ClientRpc]
		public void DoBulletHitFlinch()
		{
			if (!this.DoBulletHitFlinch__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			HealthPostProcessManager.Instance.OnTakeDamage();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StalkerBaseCamera camera = base.Camera;
			if (camera == null)
			{
				return;
			}
			camera.AddHitFlinch(10f, 17f, -1f, 0.38f);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0001049C File Offset: 0x0000E69C
		[ClientRpc]
		public void PlayBulletImpact()
		{
			if (!this.PlayBulletImpact__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromScreen("bullet.hit.flesh", 0.5f, 0.5f);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x000104D8 File Offset: 0x0000E6D8
		[ClientRpc]
		public void OnKilledClient()
		{
			if (!this.OnKilledClient__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			HealthPostProcessManager instance = HealthPostProcessManager.Instance;
			if (instance == null)
			{
				return;
			}
			instance.OnKilled();
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0001050C File Offset: 0x0000E70C
		[ClientRpc]
		public void OnRespawnClient()
		{
			if (!this.OnRespawnClient__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			HealthPostProcessManager instance = HealthPostProcessManager.Instance;
			if (instance == null)
			{
				return;
			}
			instance.OnRespawn();
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00010540 File Offset: 0x0000E740
		public void DoFallDamage(float speed)
		{
			if (speed < this.Stats.Movement.FallDamageThreshold)
			{
				if (speed > 30f)
				{
					Sound.FromWorld("human.fall", this.Position);
				}
				return;
			}
			float speedDiff = speed - this.Stats.Movement.FallDamageThreshold;
			float frac = this.Stats.Movement.FallDamageCurve.Evaluate(speedDiff / this.Stats.Movement.FallDamageKillThreshold);
			float damage = this.Stats.Health.MaxHealth * frac;
			float shake = 80f * frac;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			shake = shake.Clamp(20f, 80f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StalkerBaseCamera instance = StalkerBaseCamera.Instance;
			if (instance != null)
			{
				instance.AddShake(shake, 90f, -1f, 0.4f);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromWorld("human.fall_damage", this.Position);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromWorld("cloth.fall", this.Position);
			if (!base.IsServer)
			{
				return;
			}
			using (Prediction.Off())
			{
				DamageInfo damageInfo2 = default(DamageInfo);
				damageInfo2 = damageInfo2.WithFlag(DamageFlags.Fall);
				damageInfo2 = damageInfo2.WithPosition(this.Position);
				DamageInfo damageInfo = damageInfo2.WithHitbox(15);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				damageInfo.Damage = damage / 2f;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TakeDamage(damageInfo);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				damageInfo.HitboxIndex = 18;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TakeDamage(damageInfo);
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000106E4 File Offset: 0x0000E8E4
		[NullableContext(0)]
		[ClientRpc]
		public void DoBulletWhizz(Vector3 position, float speed, string sound)
		{
			if (!this.DoBulletWhizz__RpcProxy(position, speed, sound, null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromWorld(sound, position);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			HealthPostProcessManager.Instance.AddSuppression(0.5f);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00010728 File Offset: 0x0000E928
		[ClientRpc]
		public void SetHealthFraction(float n)
		{
			if (!this.SetHealthFraction__RpcProxy(n, null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			HealthPostProcessManager.Instance.SetHealthLevel(n);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00010758 File Offset: 0x0000E958
		protected bool TookDamage__RpcProxy(Vector3 pos, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("TookDamage", new object[]
				{
					pos
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-2092521892, this))
			{
				if (!NetRead.IsSupported(pos))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] TookDamage is not allowed to use Vector3 for the parameter 'pos'!");
					return false;
				}
				writer.Write<Vector3>(pos);
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000107EC File Offset: 0x0000E9EC
		protected bool DoBulletHitFlinch__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("DoBulletHitFlinch", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(115057940, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0001084C File Offset: 0x0000EA4C
		protected bool PlayBulletImpact__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("PlayBulletImpact", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-194899302, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000108AC File Offset: 0x0000EAAC
		protected bool OnKilledClient__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("OnKilledClient", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-653270783, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0001090C File Offset: 0x0000EB0C
		protected bool OnRespawnClient__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("OnRespawnClient", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(231042102, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0001096C File Offset: 0x0000EB6C
		[NullableContext(0)]
		protected bool DoBulletWhizz__RpcProxy(Vector3 position, float speed, string sound, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("DoBulletWhizz", new object[]
				{
					position,
					speed,
					sound
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(986359238, this))
			{
				if (!NetRead.IsSupported(position))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] DoBulletWhizz is not allowed to use Vector3 for the parameter 'position'!");
					return false;
				}
				writer.Write<Vector3>(position);
				if (!NetRead.IsSupported(speed))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] DoBulletWhizz is not allowed to use Single for the parameter 'speed'!");
					return false;
				}
				writer.Write<float>(speed);
				if (!NetRead.IsSupported(sound))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] DoBulletWhizz is not allowed to use String for the parameter 'sound'!");
					return false;
				}
				writer.Write<string>(sound);
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00010A58 File Offset: 0x0000EC58
		protected bool SetHealthFraction__RpcProxy(float n, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("SetHealthFraction", new object[]
				{
					n
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(1966364246, this))
			{
				if (!NetRead.IsSupported(n))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] SetHealthFraction is not allowed to use Single for the parameter 'n'!");
					return false;
				}
				writer.Write<float>(n);
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00010AEC File Offset: 0x0000ECEC
		[ConCmd.ServerAttribute("stalker_debug_reset_inventory", Help = "Resets your inventory to be empty.")]
		public static void ResetInventory()
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("stalker_debug_reset_inventory");
				return;
			}
			Client target = ConsoleSystem.Caller;
			if (target == null)
			{
				return;
			}
			StalkerPlayer player = target.Pawn as StalkerPlayer;
			if (player == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			player.ResetPlayerInventory();
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00010B30 File Offset: 0x0000ED30
		private void GenerateDefaultInventory()
		{
			Inventory inv = new Inventory
			{
				Title = "Pockets"
			};
			RuntimeHelpers.EnsureSufficientExecutionStack();
			inv.AddContainer(new Container(new GridSize(3, 3, false)));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			inv.AddContainer(new Container(new GridSize(2, 2, false)));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ServerInventoryManager.GenerateNetIDForInventory(inv);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			inv.AddListener(base.Client);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			inv.SetOwner(base.Client);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			inv.FullySyncWithOwner();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.InventoryComponent.AddInventory(inv);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00010BC7 File Offset: 0x0000EDC7
		private void ResetPlayerInventory()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.InventoryComponent.Reset();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.EquipmentComponent.Reset();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.GenerateDefaultInventory();
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00010BF4 File Offset: 0x0000EDF4
		[NullableContext(0)]
		public void DropItem(Item item)
		{
			ItemEntity itemEntity = new ItemEntity();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			itemEntity.SetItem(item);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			itemEntity.Position = base.EyePosition + base.EyeRotation.Forward * 20f + Vector3.Down * 10f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			itemEntity.Rotation = base.EyeRotation;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			PhysicsGroup physicsGroup = itemEntity.PhysicsGroup;
			if (physicsGroup != null)
			{
				physicsGroup.ApplyImpulse(base.EyeRotation.Forward * 200f + Vector3.Up * 50f, true);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			PhysicsGroup physicsGroup2 = itemEntity.PhysicsGroup;
			if (physicsGroup2 == null)
			{
				return;
			}
			physicsGroup2.ApplyAngularImpulse(Vector3.Random * 50f, true);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00010CCC File Offset: 0x0000EECC
		[ClientRpc]
		private void BecomeRagdollOnClient(Vector3 velocity, DamageFlags damageFlags, Vector3 forcePos, Vector3 force, int bone)
		{
			if (!this.BecomeRagdollOnClient__RpcProxy(velocity, damageFlags, forcePos, force, bone, null))
			{
				return;
			}
			DamageInfo damageInfo = default(DamageInfo);
			damageInfo = damageInfo.WithFlag(damageFlags);
			damageInfo = damageInfo.WithPosition(forcePos);
			damageInfo = damageInfo.WithForce(force);
			DamageInfo info = damageInfo.WithBone(bone);
			DeathRagdoll ent = new DeathRagdoll();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ent.CopyFrom(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ent.TakeDamage(info);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Corpse = ent;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Corpse.PlaySound("human.death");
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00010D60 File Offset: 0x0000EF60
		[ClientRpc]
		private void BecomeRagdollOnClientWithHeadExplode(Vector3 velocity, DamageFlags damageFlags, Vector3 forcePos, Vector3 force, int bone)
		{
			if (!this.BecomeRagdollOnClientWithHeadExplode__RpcProxy(velocity, damageFlags, forcePos, force, bone, null))
			{
				return;
			}
			DamageInfo damageInfo = default(DamageInfo);
			damageInfo = damageInfo.WithFlag(damageFlags);
			damageInfo = damageInfo.WithPosition(forcePos);
			damageInfo = damageInfo.WithForce(force);
			DamageInfo info = damageInfo.WithBone(bone);
			DeathRagdoll ent = new DeathRagdoll();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ent.CopyFrom(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ent.TakeDamage(info);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Corpse = ent;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create("particles/stalker/gore/head_explode.vpcf", ent, "hat", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create("particles/stalker/gore_blood_trail.vpcf", ent, "hat", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ent.ShrinkHead();
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00010E18 File Offset: 0x0000F018
		protected bool BecomeRagdollOnClient__RpcProxy(Vector3 velocity, DamageFlags damageFlags, Vector3 forcePos, Vector3 force, int bone, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("BecomeRagdollOnClient", new object[]
				{
					velocity,
					damageFlags,
					forcePos,
					force,
					bone
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-2056231656, this))
			{
				if (!NetRead.IsSupported(velocity))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] BecomeRagdollOnClient is not allowed to use Vector3 for the parameter 'velocity'!");
					return false;
				}
				writer.Write<Vector3>(velocity);
				if (!NetRead.IsSupported(damageFlags))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] BecomeRagdollOnClient is not allowed to use DamageFlags for the parameter 'damageFlags'!");
					return false;
				}
				writer.Write<DamageFlags>(damageFlags);
				if (!NetRead.IsSupported(forcePos))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] BecomeRagdollOnClient is not allowed to use Vector3 for the parameter 'forcePos'!");
					return false;
				}
				writer.Write<Vector3>(forcePos);
				if (!NetRead.IsSupported(force))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] BecomeRagdollOnClient is not allowed to use Vector3 for the parameter 'force'!");
					return false;
				}
				writer.Write<Vector3>(force);
				if (!NetRead.IsSupported(bone))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] BecomeRagdollOnClient is not allowed to use Int32 for the parameter 'bone'!");
					return false;
				}
				writer.Write<int>(bone);
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00010F78 File Offset: 0x0000F178
		protected bool BecomeRagdollOnClientWithHeadExplode__RpcProxy(Vector3 velocity, DamageFlags damageFlags, Vector3 forcePos, Vector3 force, int bone, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("BecomeRagdollOnClientWithHeadExplode", new object[]
				{
					velocity,
					damageFlags,
					forcePos,
					force,
					bone
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(916314309, this))
			{
				if (!NetRead.IsSupported(velocity))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] BecomeRagdollOnClientWithHeadExplode is not allowed to use Vector3 for the parameter 'velocity'!");
					return false;
				}
				writer.Write<Vector3>(velocity);
				if (!NetRead.IsSupported(damageFlags))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] BecomeRagdollOnClientWithHeadExplode is not allowed to use DamageFlags for the parameter 'damageFlags'!");
					return false;
				}
				writer.Write<DamageFlags>(damageFlags);
				if (!NetRead.IsSupported(forcePos))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] BecomeRagdollOnClientWithHeadExplode is not allowed to use Vector3 for the parameter 'forcePos'!");
					return false;
				}
				writer.Write<Vector3>(forcePos);
				if (!NetRead.IsSupported(force))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] BecomeRagdollOnClientWithHeadExplode is not allowed to use Vector3 for the parameter 'force'!");
					return false;
				}
				writer.Write<Vector3>(force);
				if (!NetRead.IsSupported(bone))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] BecomeRagdollOnClientWithHeadExplode is not allowed to use Int32 for the parameter 'bone'!");
					return false;
				}
				writer.Write<int>(bone);
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x000110D8 File Offset: 0x0000F2D8
		public bool IsUseDisabled()
		{
			IUse use = base.ActiveChild as IUse;
			return use != null && use.IsUsable(this);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00011100 File Offset: 0x0000F300
		[NullableContext(0)]
		protected override Entity FindUsable()
		{
			if (this.IsUseDisabled())
			{
				return null;
			}
			Vector3 eyePosition = base.EyePosition;
			Vector3 vector = base.EyePosition + base.EyeRotation.Forward * (85f * this.Scale);
			Trace trace = Trace.Ray(eyePosition, vector).WithAnyTags(new string[]
			{
				"entity",
				"weapon",
				"solid"
			});
			Entity entity = this;
			bool flag = true;
			Entity ent = trace.Ignore(entity, flag).Run().Entity;
			while (ent.IsValid() && !base.IsValidUseEntity(ent))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				ent = ent.Parent;
			}
			if (!base.IsValidUseEntity(ent))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				eyePosition = base.EyePosition;
				vector = base.EyePosition + base.EyeRotation.Forward * (85f * this.Scale);
				trace = Trace.Ray(eyePosition, vector).Radius(2f).WithAnyTags(new string[]
				{
					"entity",
					"weapon",
					"solid"
				});
				entity = this;
				flag = true;
				ref TraceResult ptr = trace.Ignore(entity, flag).Run();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				ent = ptr.Entity;
				while (ent.IsValid() && !base.IsValidUseEntity(ent))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					ent = ent.Parent;
				}
			}
			if (!base.IsValidUseEntity(ent))
			{
				return null;
			}
			return ent;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0001128D File Offset: 0x0000F48D
		protected override void UseFail()
		{
			if (this.IsUseDisabled())
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.UseFail();
		}

		// Token: 0x060002DA RID: 730 RVA: 0x000112A3 File Offset: 0x0000F4A3
		public virtual void GibEffect(To toTarget, Vector3 pos)
		{
			this.GibEffect__RpcProxy(pos, new To?(toTarget));
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000112B3 File Offset: 0x0000F4B3
		public void DoPsyBoltEffect(To toTarget, Entity target)
		{
			this.DoPsyBoltEffect__RpcProxy(target, new To?(toTarget));
		}

		// Token: 0x060002DC RID: 732 RVA: 0x000112C3 File Offset: 0x0000F4C3
		public void TookDamage(To toTarget, Vector3 pos)
		{
			this.TookDamage__RpcProxy(pos, new To?(toTarget));
		}

		// Token: 0x060002DD RID: 733 RVA: 0x000112D3 File Offset: 0x0000F4D3
		public void DoBulletHitFlinch(To toTarget)
		{
			this.DoBulletHitFlinch__RpcProxy(new To?(toTarget));
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000112E2 File Offset: 0x0000F4E2
		public void PlayBulletImpact(To toTarget)
		{
			this.PlayBulletImpact__RpcProxy(new To?(toTarget));
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000112F1 File Offset: 0x0000F4F1
		public void OnKilledClient(To toTarget)
		{
			this.OnKilledClient__RpcProxy(new To?(toTarget));
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00011300 File Offset: 0x0000F500
		public void OnRespawnClient(To toTarget)
		{
			this.OnRespawnClient__RpcProxy(new To?(toTarget));
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0001130F File Offset: 0x0000F50F
		public void DoBulletWhizz(To toTarget, Vector3 position, float speed, string sound)
		{
			this.DoBulletWhizz__RpcProxy(position, speed, sound, new To?(toTarget));
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00011322 File Offset: 0x0000F522
		public void SetHealthFraction(To toTarget, float n)
		{
			this.SetHealthFraction__RpcProxy(n, new To?(toTarget));
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00011332 File Offset: 0x0000F532
		private void BecomeRagdollOnClient(To toTarget, Vector3 velocity, DamageFlags damageFlags, Vector3 forcePos, Vector3 force, int bone)
		{
			this.BecomeRagdollOnClient__RpcProxy(velocity, damageFlags, forcePos, force, bone, new To?(toTarget));
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00011349 File Offset: 0x0000F549
		private void BecomeRagdollOnClientWithHeadExplode(To toTarget, Vector3 velocity, DamageFlags damageFlags, Vector3 forcePos, Vector3 force, int bone)
		{
			this.BecomeRagdollOnClientWithHeadExplode__RpcProxy(velocity, damageFlags, forcePos, force, bone, new To?(toTarget));
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00011360 File Offset: 0x0000F560
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id <= -653270783)
			{
				if (id <= -2092521892)
				{
					if (id == -2131026325)
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
					if (id == -2092521892)
					{
						Vector3 __pos2 = read.ReadData<Vector3>(default(Vector3));
						if (!Prediction.WasPredicted("TookDamage", new object[]
						{
							__pos2
						}))
						{
							this.TookDamage(__pos2);
						}
						return;
					}
				}
				else
				{
					if (id == -2056231656)
					{
						Vector3 __velocity = read.ReadData<Vector3>(default(Vector3));
						DamageFlags __damageFlags = DamageFlags.Generic;
						__damageFlags = read.ReadData<DamageFlags>(__damageFlags);
						Vector3 __forcePos = read.ReadData<Vector3>(default(Vector3));
						Vector3 __force = read.ReadData<Vector3>(default(Vector3));
						int __bone = 0;
						__bone = read.ReadData<int>(__bone);
						if (!Prediction.WasPredicted("BecomeRagdollOnClient", new object[]
						{
							__velocity,
							__damageFlags,
							__forcePos,
							__force,
							__bone
						}))
						{
							this.BecomeRagdollOnClient(__velocity, __damageFlags, __forcePos, __force, __bone);
						}
						return;
					}
					if (id == -1936323455)
					{
						Entity __target = null;
						__target = read.ReadClass<Entity>(__target);
						if (!Prediction.WasPredicted("DoPsyBoltEffect", new object[]
						{
							__target
						}))
						{
							this.DoPsyBoltEffect(__target);
						}
						return;
					}
					if (id == -653270783)
					{
						if (!Prediction.WasPredicted("OnKilledClient", Array.Empty<object>()))
						{
							this.OnKilledClient();
						}
						return;
					}
				}
			}
			else if (id <= 231042102)
			{
				if (id == -194899302)
				{
					if (!Prediction.WasPredicted("PlayBulletImpact", Array.Empty<object>()))
					{
						this.PlayBulletImpact();
					}
					return;
				}
				if (id == 115057940)
				{
					if (!Prediction.WasPredicted("DoBulletHitFlinch", Array.Empty<object>()))
					{
						this.DoBulletHitFlinch();
					}
					return;
				}
				if (id == 231042102)
				{
					if (!Prediction.WasPredicted("OnRespawnClient", Array.Empty<object>()))
					{
						this.OnRespawnClient();
					}
					return;
				}
			}
			else
			{
				if (id == 916314309)
				{
					Vector3 __velocity2 = read.ReadData<Vector3>(default(Vector3));
					DamageFlags __damageFlags2 = DamageFlags.Generic;
					__damageFlags2 = read.ReadData<DamageFlags>(__damageFlags2);
					Vector3 __forcePos2 = read.ReadData<Vector3>(default(Vector3));
					Vector3 __force2 = read.ReadData<Vector3>(default(Vector3));
					int __bone2 = 0;
					__bone2 = read.ReadData<int>(__bone2);
					if (!Prediction.WasPredicted("BecomeRagdollOnClientWithHeadExplode", new object[]
					{
						__velocity2,
						__damageFlags2,
						__forcePos2,
						__force2,
						__bone2
					}))
					{
						this.BecomeRagdollOnClientWithHeadExplode(__velocity2, __damageFlags2, __forcePos2, __force2, __bone2);
					}
					return;
				}
				if (id == 986359238)
				{
					Vector3 __position = read.ReadData<Vector3>(default(Vector3));
					float __speed = 0f;
					__speed = read.ReadData<float>(__speed);
					string __sound = null;
					__sound = read.ReadClass<string>(__sound);
					if (!Prediction.WasPredicted("DoBulletWhizz", new object[]
					{
						__position,
						__speed,
						__sound
					}))
					{
						this.DoBulletWhizz(__position, __speed, __sound);
					}
					return;
				}
				if (id == 1966364246)
				{
					float __n = 0f;
					__n = read.ReadData<float>(__n);
					if (!Prediction.WasPredicted("SetHealthFraction", new object[]
					{
						__n
					}))
					{
						this.SetHealthFraction(__n);
					}
					return;
				}
			}
			base.OnCallRemoteProcedure(id, read);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000116FD File Offset: 0x0000F8FD
		[NullableContext(0)]
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarUnmanaged<bool>>(ref this._repback__InSights, "InSights", true, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x040000DC RID: 220
		private bool hasGibbed;

		// Token: 0x040000DD RID: 221
		private const float MaxPsyHealth = 100f;

		// Token: 0x040000DE RID: 222
		private const float PsyHealthRegenRate = 1f;

		// Token: 0x040000DF RID: 223
		private DamageInfo lastDamage;

		// Token: 0x040000E2 RID: 226
		private bool firstSpawn = true;

		// Token: 0x040000E4 RID: 228
		private TimeSince timeSinceLastHitFlinch = 0f;

		// Token: 0x040000E5 RID: 229
		private bool wasHeadshot;

		// Token: 0x040000E6 RID: 230
		private float headDamageCache;

		// Token: 0x040000E7 RID: 231
		private TimeUntil timeUntilHeadDamageCacheReset = 0f;

		// Token: 0x040000E8 RID: 232
		private VarUnmanaged<bool> _repback__InSights = new VarUnmanaged<bool>();
	}
}
