using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;
using StalkerRP.Inventory;
using StalkerRP.Inventory.Gear;

namespace StalkerRP
{
	// Token: 0x0200005D RID: 93
	public class StalkerWeaponBase : Weapon
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00014859 File Offset: 0x00012A59
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x00014867 File Offset: 0x00012A67
		[Net]
		[Predicted]
		[Local]
		[DefaultValue(false)]
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

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00014876 File Offset: 0x00012A76
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x00014884 File Offset: 0x00012A84
		[Net]
		[Predicted]
		[Local]
		[DefaultValue(false)]
		public unsafe bool IsHolstering
		{
			get
			{
				return *this._repback__IsHolstering.GetValue();
			}
			set
			{
				this._repback__IsHolstering.SetValue(value);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00014893 File Offset: 0x00012A93
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x000148A1 File Offset: 0x00012AA1
		[Net]
		[Predicted]
		[Local]
		[DefaultValue(100)]
		public unsafe int MagazinePrimaryCurrentAmmo
		{
			get
			{
				return *this._repback__MagazinePrimaryCurrentAmmo.GetValue();
			}
			set
			{
				this._repback__MagazinePrimaryCurrentAmmo.SetValue(value);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x000148B0 File Offset: 0x00012AB0
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x000148BD File Offset: 0x00012ABD
		[Net]
		[Change(null)]
		[Local]
		public WeaponItemResource Resource
		{
			get
			{
				return this._repback__Resource.GetValue();
			}
			set
			{
				this._repback__Resource.SetValue(value);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003DA RID: 986 RVA: 0x000148CB File Offset: 0x00012ACB
		// (set) Token: 0x060003DB RID: 987 RVA: 0x000148DD File Offset: 0x00012ADD
		[Net]
		[Predicted]
		[Local]
		[DefaultValue(0)]
		private unsafe TimeSince TimeSinceStoppedRunning
		{
			get
			{
				return *this._repback__TimeSinceStoppedRunning.GetValue();
			}
			set
			{
				this._repback__TimeSinceStoppedRunning.SetValue(value);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003DC RID: 988 RVA: 0x000148EC File Offset: 0x00012AEC
		public override float ReloadTime
		{
			get
			{
				if (!this.MagazinePrimaryEmpty)
				{
					return this.Resource.ReloadTime;
				}
				return this.Resource.ReloadTimeEmpty;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0001490D File Offset: 0x00012B0D
		public override string ViewModelPath
		{
			get
			{
				WeaponItemResource resource = this.Resource;
				if (resource == null)
				{
					return null;
				}
				return resource.ViewModelPath;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00014920 File Offset: 0x00012B20
		public bool MagazinePrimaryEmpty
		{
			get
			{
				return this.MagazinePrimaryCurrentAmmo <= 0;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0001492E File Offset: 0x00012B2E
		protected virtual Vector3 BobDirection
		{
			get
			{
				return new Vector3(0f, 0.1f, 0.05f);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00014944 File Offset: 0x00012B44
		protected virtual Vector3 SightsPositionOffset
		{
			get
			{
				return Vector3.Zero;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0001494B File Offset: 0x00012B4B
		protected virtual Angles SightsAngleOffset
		{
			get
			{
				return Angles.Zero;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00014952 File Offset: 0x00012B52
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x0001495A File Offset: 0x00012B5A
		protected Vector3 SightsCurrentPosOffset { get; set; } = Vector3.Zero;

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00014963 File Offset: 0x00012B63
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x0001496B File Offset: 0x00012B6B
		protected Angles SightsCurrentAngleOffset { get; set; } = Angles.Zero;

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x00014974 File Offset: 0x00012B74
		protected virtual bool IsRunning
		{
			get
			{
				if (!base.IsReloading)
				{
					StalkerPlayer stalkerPlayer = this.Owner as StalkerPlayer;
					if (stalkerPlayer != null)
					{
						StaminaComponent staminaComponent = stalkerPlayer.StaminaComponent;
						if (staminaComponent != null && staminaComponent.IsRunning)
						{
							return stalkerPlayer.Velocity.Length > 180f;
						}
					}
					return false;
				}
				return false;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x000149C3 File Offset: 0x00012BC3
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x000149CB File Offset: 0x00012BCB
		[DefaultValue(0)]
		private float VerticalRecoil { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x000149D4 File Offset: 0x00012BD4
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x000149DC File Offset: 0x00012BDC
		[DefaultValue(0)]
		private float HorizontalRecoilMagnitude { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x000149E5 File Offset: 0x00012BE5
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x000149ED File Offset: 0x00012BED
		[DefaultValue(0)]
		private float HorizontalRecoilYaw { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x000149F6 File Offset: 0x00012BF6
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x000149FE File Offset: 0x00012BFE
		[DefaultValue(0)]
		private float RecoilShield { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00014A07 File Offset: 0x00012C07
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x00014A0F File Offset: 0x00012C0F
		[DefaultValue(0)]
		private float CurrentSpreadIncrease { get; set; }

		// Token: 0x060003F1 RID: 1009 RVA: 0x00014A18 File Offset: 0x00012C18
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("nocollide");
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00014A3C File Offset: 0x00012C3C
		public void Init(WeaponItemResource resource)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Resource = resource;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MagazinePrimaryCurrentAmmo = this.Resource.MagazinePrimarySize;
			if (!string.IsNullOrWhiteSpace(this.Resource.WorldModel))
			{
				base.SetModel(this.Resource.WorldModel);
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00014A90 File Offset: 0x00012C90
		public override void Simulate(Client owner)
		{
			if (base.ViewModelEntity != null)
			{
				Vector3 vel = this.Owner.Velocity;
				float speed = vel.Length;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.ViewModelEntity.SetAnimParameter("bRunning", this.IsRunning);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.ViewModelEntity.SetAnimParameter("bMoving", speed > 10f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.ViewModelEntity.SetAnimParameter("fMoveSpeed", speed);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.ViewModelEntity.SetAnimParameter("bCrouching", Input.Down(InputButton.Duck));
				Vector3 eyeForward = this.Owner.Rotation.Forward;
				Vector3 eyeRight = this.Owner.Rotation.Right;
				float forwardComponent = vel.Dot(eyeForward);
				float horizontalComponent = vel.Dot(eyeRight);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.ViewModelEntity.SetAnimParameter("fMoveX", forwardComponent);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.ViewModelEntity.SetAnimParameter("fMoveY", horizontalComponent);
			}
			if (this.IsRunning)
			{
				this.SetSights(false);
			}
			if (this.oldRunning && !this.IsRunning)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TimeSinceStoppedRunning = 0f;
			}
			if (this.InSights && !base.Client.IsBot && !base.Client.GetClientData("stalker_setting_toggle_sights", null).ToBool() && !Input.Down(InputButton.SecondaryAttack))
			{
				this.SetSights(false);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Simulate(owner);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.oldRunning = this.IsRunning;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			AnimatedEntity animatedEntity = this.Owner as AnimatedEntity;
			if (animatedEntity != null)
			{
				animatedEntity.SetAnimParameter("sight", this.InSights ? 1 : 0);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateSpread();
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00014C5C File Offset: 0x00012E5C
		public virtual void Holster()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsHolstering = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseViewModel viewModelEntity = base.ViewModelEntity;
			if (viewModelEntity != null)
			{
				viewModelEntity.SetAnimParameter("bHolster", true);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetSights(false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.IsReloading = false;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00014CA9 File Offset: 0x00012EA9
		public virtual void Deploy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			AnimatedEntity animatedEntity = this.Owner as AnimatedEntity;
			if (animatedEntity != null)
			{
				animatedEntity.SetAnimParameter("b_deploy", true);
			}
			bool isServer = base.IsServer;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00014CD4 File Offset: 0x00012ED4
		[Description("This entity has become the active entity. This most likely means a player was carrying it in their inventory and now has it in their hands.")]
		public override void ActiveStart(Entity ent)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ActiveStart(ent);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsHolstering = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Deploy();
			if (this.IsLocalPawn && base.ViewModelEntity != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.ViewModelEntity.SetAnimParameter("bEmpty", this.MagazinePrimaryEmpty);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.ViewModelEntity.SetAnimParameter("bDeploy", true);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				((StalkerViewModel)base.ViewModelEntity).BobDirection = this.BobDirection;
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00014D60 File Offset: 0x00012F60
		[Description("This entity has stopped being the active entity. This most likely means a player was holding it but has switched away or dropped it (in which case dropped = true)")]
		public override void ActiveEnd(Entity ent, bool dropped)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetSights(false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.IsReloading = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ActiveEnd(ent, dropped);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00014D88 File Offset: 0x00012F88
		public override bool CanPrimaryAttack()
		{
			return !this.IsRunning && this.TimeSinceStoppedRunning >= 0.35f && this.Owner.IsValid() && !(this.Resource.Automatic ? (!Input.Down(InputButton.PrimaryAttack)) : (!Input.Pressed(InputButton.PrimaryAttack))) && (this.Resource.RPM <= 0f || base.TimeSincePrimaryAttack > this.Resource.PrimaryFireDelay);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00014E1D File Offset: 0x0001301D
		public virtual void DryFire()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ShootEffects();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PlaySound(this.Resource.DryFireSound);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00014E41 File Offset: 0x00013041
		public virtual void PlayShootSound()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PlaySound(this.Resource.FireSound);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00014E5C File Offset: 0x0001305C
		public override void AttackPrimary()
		{
			if (this.MagazinePrimaryCurrentAmmo <= 0)
			{
				if (Input.Pressed(InputButton.PrimaryAttack))
				{
					this.DryFire();
				}
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.TimeSincePrimaryAttack = 0f;
			if (Prediction.FirstTime)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ShootEffects();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.AddRecoil();
			}
			if (!this.Owner.Tags.Has("stalker_infinite_ammo"))
			{
				int magazinePrimaryCurrentAmmo = this.MagazinePrimaryCurrentAmmo;
				this.MagazinePrimaryCurrentAmmo = magazinePrimaryCurrentAmmo - 1;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseViewModel viewModelEntity = base.ViewModelEntity;
			if (viewModelEntity != null)
			{
				viewModelEntity.SetAnimParameter("bEmpty", this.MagazinePrimaryEmpty);
			}
			IEnumerable<Client> clients = from x in To.Everyone
			where x.Client != this.Owner.Client
			select x;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MuzzleFlashWorld(To.Multiple(clients));
			if (base.IsClient)
			{
				this.MuzzleFlashViewmodel();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			AnimatedEntity animatedEntity = this.Owner as AnimatedEntity;
			if (animatedEntity != null)
			{
				animatedEntity.SetAnimParameter("b_attack", true);
			}
			if (Prediction.FirstTime)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.PlayShootSound();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ShootBullets(this.Resource.NumBullets, this.GetEffectiveSpread(), this.Resource.Force, this.Resource.Damage, this.Resource.BulletSize);
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00014FAC File Offset: 0x000131AC
		[ClientRpc]
		public void MuzzleFlashWorld()
		{
			if (!this.MuzzleFlashWorld__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create("particles/stalker/weapons/muzzle_flash_light.vpcf", this.EffectEntity, "muzzle", true);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00014FE8 File Offset: 0x000131E8
		[ClientRpc]
		public void MuzzleFlashViewmodel()
		{
			if (!this.MuzzleFlashViewmodel__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create("particles/stalker/weapons/muzzle_flash_light_viewmodel.vpcf", this.Owner.EyePosition + this.Owner.EyeRotation.Forward * 50f);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00015044 File Offset: 0x00013244
		public override bool CanSecondaryAttack()
		{
			if (base.TimeSinceSecondaryAttack < 0.2f)
			{
				return false;
			}
			if (!this.Owner.IsValid())
			{
				return false;
			}
			if (!base.Client.GetClientData("stalker_setting_toggle_sights", null).ToBool())
			{
				if (!Input.Down(InputButton.SecondaryAttack) || this.InSights)
				{
					return false;
				}
			}
			else if (!Input.Pressed(InputButton.SecondaryAttack))
			{
				return false;
			}
			return !this.IsRunning && !base.IsReloading && this.Resource.HasSights;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x000150D0 File Offset: 0x000132D0
		private void SetSights(bool b)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.InSights = b;
			StalkerPlayer player = this.Owner as StalkerPlayer;
			if (player != null)
			{
				player.InSights = b;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseViewModel viewModelEntity = base.ViewModelEntity;
			if (viewModelEntity == null)
			{
				return;
			}
			viewModelEntity.SetAnimParameter("bAiming", this.InSights);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0001511F File Offset: 0x0001331F
		public override void AttackSecondary()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetSights(!this.InSights);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00015138 File Offset: 0x00013338
		public override void CreateViewModel()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertClient("CreateViewModel");
			if (string.IsNullOrEmpty(this.ViewModelPath))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ViewModelEntity = new StalkerViewModel
			{
				Position = this.Position,
				Owner = this.Owner,
				EnableViewmodelRendering = true
			};
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ViewModelEntity.SetModel(this.ViewModelPath);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000151A8 File Offset: 0x000133A8
		[Event.FrameAttribute]
		protected virtual void FrameUpdate()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateRecoil();
			StalkerViewModel svm = base.ViewModelEntity as StalkerViewModel;
			if (svm == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			svm.ReduceSwing = this.InSights;
			if (this.InSights)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SightsCurrentPosOffset = this.SightsCurrentPosOffset.LerpTo(this.SightsPositionOffset, Time.Delta * this.Resource.EnterSightsSpeed);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SightsCurrentAngleOffset = Angles.Lerp(this.SightsCurrentAngleOffset, this.SightsAngleOffset, Time.Delta * this.Resource.EnterSightsSpeed);
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SightsCurrentPosOffset = this.SightsCurrentPosOffset.LerpTo(0f, Time.Delta * this.Resource.EnterSightsSpeed);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SightsCurrentAngleOffset = Angles.Lerp(this.SightsCurrentAngleOffset, Angles.Zero, Time.Delta * this.Resource.EnterSightsSpeed);
			}
			float reloadFrac = base.TimeSinceReload / this.ReloadTime;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ViewModelEntity.SetAnimParameter("fReloadFraction", reloadFrac);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			svm.PositionOffset = this.SightsCurrentPosOffset;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			svm.RotationOffset = this.SightsCurrentAngleOffset;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			svm.PositionOffset += this.Resource.ViewModelOffset;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			svm.RotationOffset += this.Resource.ViewModelRotationOffset;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00015335 File Offset: 0x00013535
		public override bool CanReload()
		{
			return this.MagazinePrimaryCurrentAmmo < this.Resource.MagazinePrimarySize && base.CanReload();
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00015352 File Offset: 0x00013552
		public override void Reload()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetSights(false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Reload();
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001536C File Offset: 0x0001356C
		public override void OnReloadFinish()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MagazinePrimaryCurrentAmmo = this.Resource.MagazinePrimarySize;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseViewModel viewModelEntity = base.ViewModelEntity;
			if (viewModelEntity != null)
			{
				viewModelEntity.SetAnimParameter("bIsReloading", false);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseViewModel viewModelEntity2 = base.ViewModelEntity;
			if (viewModelEntity2 != null)
			{
				viewModelEntity2.SetAnimParameter("bEmpty", this.MagazinePrimaryEmpty);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnReloadFinish();
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x000153D8 File Offset: 0x000135D8
		[ClientRpc]
		public override void StartReloadEffects()
		{
			if (!base.StartReloadEffects__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseViewModel viewModelEntity = base.ViewModelEntity;
			if (viewModelEntity != null)
			{
				viewModelEntity.SetAnimParameter("bReload", true);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseViewModel viewModelEntity2 = base.ViewModelEntity;
			if (viewModelEntity2 == null)
			{
				return;
			}
			viewModelEntity2.SetAnimParameter("bIsReloading", true);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00015430 File Offset: 0x00013630
		[ClientRpc]
		protected override void ShootEffects()
		{
			if (!base.ShootEffects__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertClient("ShootEffects");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseViewModel viewModelEntity = base.ViewModelEntity;
			if (viewModelEntity != null)
			{
				viewModelEntity.SetAnimParameter("bLastShot", this.MagazinePrimaryCurrentAmmo == 1);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseViewModel viewModelEntity2 = base.ViewModelEntity;
			if (viewModelEntity2 != null)
			{
				viewModelEntity2.SetAnimParameter("bShoot", true);
			}
			if (this.MagazinePrimaryCurrentAmmo <= 0)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create(this.Resource.MuzzleEffectPath, this.EffectEntity, "muzzle", true);
			if (this.bulletSmoke != null)
			{
				this.timeSinceLastBulletSmoke;
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000154E4 File Offset: 0x000136E4
		public override void ShootBullet(Vector3 pos, Vector3 dir, float spread, float force, float damage, float bulletSize)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ShootPhysicsBullet(pos, dir, spread, force, damage, bulletSize);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x000154FC File Offset: 0x000136FC
		public virtual void ShootPhysicsBullet(Vector3 pos, Vector3 dir, float spread, float force, float damage, float bulletSize)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Rand.SetSeed(Time.Tick + this.bulletsFired);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Vector3 forward = dir + (Vector3.Random + Vector3.Random + Vector3.Random + Vector3.Random) * spread * 0.25f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			forward = forward.Normal;
			StalkerPlayer player = this.Owner as StalkerPlayer;
			if (player != null)
			{
				Rotation rot = player.HealthComponent.GetArmsSway();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				rot = Rotation.From(-rot.Pitch(), rot.Yaw(), rot.Roll());
				RuntimeHelpers.EnsureSufficientExecutionStack();
				forward = rot * forward;
			}
			BulletPhysBase bullet = BulletPhysBase.Pool.Request();
			float speed = this.Resource.MuzzleVelocity * 39.3701f;
			using (Entity.LagCompensation())
			{
				bullet.Fire(pos, forward, speed, this.Owner, this, this.Resource.AmmoResource, this.Resource.Force, damage, false);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.bulletsFired++;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00015638 File Offset: 0x00013838
		[Description("Called when the resource for this gun changes. Usually when you've just equipped it for the first time.")]
		private void OnResourceChanged()
		{
			StalkerPlayer player = this.Owner as StalkerPlayer;
			if (player != null)
			{
				EquipmentComponent equipmentComponent = player.EquipmentComponent;
				if (((equipmentComponent != null) ? equipmentComponent.GetActive() : null) == this)
				{
					this.ActiveStart(this.Owner);
				}
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00015678 File Offset: 0x00013878
		public override void SimulateAnimator(PawnAnimator anim)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			anim.SetAnimParameter("holdtype", (int)this.Resource.HoldType);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			anim.SetAnimParameter("aim_body_weight", 1f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			anim.SetAnimParameter("holdtype_handedness", 0);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000156C8 File Offset: 0x000138C8
		public override void PostCameraSetup(ref CameraSetup camSetup)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PostCameraSetup(ref camSetup);
			if (this.notSet)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.lerpZoomAmount = camSetup.FieldOfView;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.notSet = false;
			}
			if (this.InSights)
			{
				this.fovReduction = this.fovReduction.LerpTo(this.Resource.SightsFOVReduction, 10f * Time.Delta, true);
			}
			else
			{
				this.fovReduction = this.fovReduction.LerpTo(0f, 10f * Time.Delta, true);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			camSetup.FieldOfView -= this.fovReduction;
			StalkerViewModel svm = base.ViewModelEntity as StalkerViewModel;
			if (svm != null)
			{
				svm.ViewModelFOVReduction = this.fovReduction;
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001578C File Offset: 0x0001398C
		private void AddRecoil()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CurrentSpreadIncrease += this.Resource.SpreadIncreasePerShot;
			if (base.IsClient)
			{
				StalkerPlayer player = this.Owner as StalkerPlayer;
				if (player != null)
				{
					float recoilMult = (this.Resource.HorizontalRecoil - this.RecoilShield).Clamp(0f, this.Resource.HorizontalRecoil) / this.Resource.HorizontalRecoil;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.RecoilShield -= this.Resource.HorizontalRecoil;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.RecoilShield = this.RecoilShield.Clamp(0f, this.RecoilShield);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.VerticalRecoil += this.Resource.VerticalRecoil * player.HealthComponent.GetRecoilMultiplier();
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.HorizontalRecoilMagnitude += this.Resource.HorizontalRecoil * recoilMult * player.HealthComponent.GetRecoilMultiplier();
					float noise = Noise.Perlin(Time.Now * 60f) - 0.5f;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					noise *= 2f;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.HorizontalRecoilYaw += noise * this.HorizontalRecoilMagnitude * recoilMult;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					player.Camera.AddShake(this.Resource.CameraShakeMagnitude, this.Resource.CameraShakeRoughness, -1f, 0.4f);
					return;
				}
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00015904 File Offset: 0x00013B04
		private void UpdateSpread()
		{
			float decayRate = this.Resource.RecoilDecayRate;
			if (base.TimeSincePrimaryAttack > this.Resource.PrimaryFireDelay * 1.5f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CurrentSpreadIncrease = this.CurrentSpreadIncrease.LerpTo(0f, Time.Delta * decayRate, true);
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00015960 File Offset: 0x00013B60
		private void UpdateRecoil()
		{
			float decayRate = this.Resource.RecoilDecayRate;
			if (base.TimeSincePrimaryAttack > this.Resource.PrimaryFireDelay * 1.5f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				decayRate *= 5f;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.RecoilShield = this.RecoilShield.LerpTo(this.Resource.RecoilShield, Time.Delta * 50f, true);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.VerticalRecoil = this.VerticalRecoil.LerpTo(0f, Time.Delta * decayRate, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HorizontalRecoilMagnitude = this.HorizontalRecoilMagnitude.LerpTo(0f, Time.Delta * decayRate, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HorizontalRecoilYaw = this.HorizontalRecoilYaw.LerpTo(0f, Time.Delta * decayRate, true);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00015A3C File Offset: 0x00013C3C
		private float GetEffectiveSpread()
		{
			StalkerPlayer player = this.Owner as StalkerPlayer;
			if (player == null)
			{
				return 0f;
			}
			float velMult = player.Velocity.Length / 150f * this.Resource.SpreadMovingAdd;
			float jumpMult = 0f;
			float sightsMult = this.InSights ? this.Resource.SpreadSightsAdd : 0f;
			float crouchMult = 0f;
			if (player.GroundEntity != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				crouchMult += (Input.Down(InputButton.Duck) ? this.Resource.SpreadCrouchingAdd : 0f);
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				jumpMult += this.Resource.SpreadJumpingAdd;
			}
			float x = this.Resource.SpreadMinimum + this.CurrentSpreadIncrease;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			return MathF.Min(x, this.Resource.SpreadSoftMaximum) + (velMult + jumpMult + sightsMult + crouchMult) * player.HealthComponent.GetRecoilMultiplier();
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00015B28 File Offset: 0x00013D28
		public override void BuildInput(InputBuilder inputBuilder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.BuildInput(inputBuilder);
			if (this.InSights)
			{
				float frac = 1f - base.Client.GetClientData("stalker_setting_aim_sensitivity_mult", null).ToFloat(0f);
				Angles dif = inputBuilder.ViewAngles - inputBuilder.OriginalViewAngles;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				dif = dif.Normal;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				inputBuilder.ViewAngles -= dif * frac;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			inputBuilder.ViewAngles.pitch = inputBuilder.ViewAngles.pitch - this.VerticalRecoil;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			inputBuilder.ViewAngles.yaw = inputBuilder.ViewAngles.yaw + this.HorizontalRecoilYaw;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00015BDC File Offset: 0x00013DDC
		protected bool MuzzleFlashWorld__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("MuzzleFlashWorld", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-857610183, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00015C3C File Offset: 0x00013E3C
		protected bool MuzzleFlashViewmodel__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("MuzzleFlashViewmodel", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-1052508447, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00015C9C File Offset: 0x00013E9C
		public void MuzzleFlashWorld(To toTarget)
		{
			this.MuzzleFlashWorld__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00015CAB File Offset: 0x00013EAB
		public void MuzzleFlashViewmodel(To toTarget)
		{
			this.MuzzleFlashViewmodel__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00015CBA File Offset: 0x00013EBA
		public override void StartReloadEffects(To toTarget)
		{
			base.StartReloadEffects__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00015CC9 File Offset: 0x00013EC9
		protected override void ShootEffects(To toTarget)
		{
			base.ShootEffects__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00015CD8 File Offset: 0x00013ED8
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id <= -857610183)
			{
				if (id == -1052508447)
				{
					if (!Prediction.WasPredicted("MuzzleFlashViewmodel", Array.Empty<object>()))
					{
						this.MuzzleFlashViewmodel();
					}
					return;
				}
				if (id == -857610183)
				{
					if (!Prediction.WasPredicted("MuzzleFlashWorld", Array.Empty<object>()))
					{
						this.MuzzleFlashWorld();
					}
					return;
				}
			}
			else
			{
				if (id == -450951263)
				{
					if (!Prediction.WasPredicted("ShootEffects", Array.Empty<object>()))
					{
						this.ShootEffects();
					}
					return;
				}
				if (id == 94731439)
				{
					if (!Prediction.WasPredicted("StartReloadEffects", Array.Empty<object>()))
					{
						this.StartReloadEffects();
					}
					return;
				}
			}
			base.OnCallRemoteProcedure(id, read);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00015D7C File Offset: 0x00013F7C
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarUnmanaged<bool>>(ref this._repback__InSights, "InSights", true, true);
			builder.Register<VarUnmanaged<bool>>(ref this._repback__IsHolstering, "IsHolstering", true, true);
			builder.Register<VarUnmanaged<int>>(ref this._repback__MagazinePrimaryCurrentAmmo, "MagazinePrimaryCurrentAmmo", true, true);
			builder.Register<VarGeneric<WeaponItemResource>>(ref this._repback__Resource, "Resource", false, true);
			this._repback__Resource.SetCallback<WeaponItemResource>(new Action(this.OnResourceChanged));
			builder.Register<VarUnmanaged<TimeSince>>(ref this._repback__TimeSinceStoppedRunning, "TimeSinceStoppedRunning", true, true);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x04000134 RID: 308
		private bool oldRunning;

		// Token: 0x04000135 RID: 309
		private TimeSince timeSinceLastBulletSmoke;

		// Token: 0x04000136 RID: 310
		private Particles bulletSmoke;

		// Token: 0x04000137 RID: 311
		private int bulletsFired;

		// Token: 0x04000138 RID: 312
		private bool notSet = true;

		// Token: 0x04000139 RID: 313
		private float lerpZoomAmount;

		// Token: 0x0400013A RID: 314
		private float fovReduction;

		// Token: 0x0400013B RID: 315
		private VarUnmanaged<bool> _repback__InSights = new VarUnmanaged<bool>(false);

		// Token: 0x0400013C RID: 316
		private VarUnmanaged<bool> _repback__IsHolstering = new VarUnmanaged<bool>(false);

		// Token: 0x0400013D RID: 317
		private VarUnmanaged<int> _repback__MagazinePrimaryCurrentAmmo = new VarUnmanaged<int>(100);

		// Token: 0x0400013E RID: 318
		private VarGeneric<WeaponItemResource> _repback__Resource = new VarGeneric<WeaponItemResource>();

		// Token: 0x0400013F RID: 319
		private VarUnmanaged<TimeSince> _repback__TimeSinceStoppedRunning = new VarUnmanaged<TimeSince>(0f);
	}
}
