using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;

namespace StalkerRP
{
	// Token: 0x0200005A RID: 90
	public class StalkerShotgunBase : StalkerWeaponBase
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0001386F File Offset: 0x00011A6F
		public virtual string ShootLastSound
		{
			get
			{
				return "";
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600038A RID: 906 RVA: 0x00013876 File Offset: 0x00011A76
		[Description("If this is defined, we will play this sound servside and the regular shootsound clientside. Useful for situations like when a shotgun pump is included as part of the shooting animation.")]
		public virtual string ServerShootSound
		{
			get
			{
				return "";
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0001387D File Offset: 0x00011A7D
		// (set) Token: 0x0600038C RID: 908 RVA: 0x0001388B File Offset: 0x00011A8B
		[Net]
		[Predicted]
		[DefaultValue(false)]
		protected unsafe bool FinishingReload
		{
			get
			{
				return *this._repback__FinishingReload.GetValue();
			}
			set
			{
				this._repback__FinishingReload.SetValue(value);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0001389A File Offset: 0x00011A9A
		protected virtual float ReloadFinishTime
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600038E RID: 910 RVA: 0x000138A1 File Offset: 0x00011AA1
		[Description("How long it should take to insert a new shell.")]
		public virtual float ShellInsertDelay
		{
			get
			{
				return 1.5f;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600038F RID: 911 RVA: 0x000138A8 File Offset: 0x00011AA8
		[Description("How long it should take to insert a new shell into an empty gun.")]
		public virtual float FirstShellInsertDelay
		{
			get
			{
				return 2.5f;
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000138AF File Offset: 0x00011AAF
		public override bool CanPrimaryAttack()
		{
			return !this.FinishingReload && base.CanPrimaryAttack();
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000138C1 File Offset: 0x00011AC1
		public override bool CanSecondaryAttack()
		{
			return !this.FinishingReload && base.CanSecondaryAttack();
		}

		// Token: 0x06000392 RID: 914 RVA: 0x000138D3 File Offset: 0x00011AD3
		public override void Simulate(Client owner)
		{
			if (base.IsReloading)
			{
				this.ReloadUpdate();
			}
			if (this.TimeSinceReloadFinished > this.ReloadFinishTime)
			{
				this.FinishingReload = false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Simulate(owner);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0001390C File Offset: 0x00011B0C
		protected virtual void ReloadUpdate()
		{
			if (this.Owner.IsValid() && Input.Pressed(InputButton.PrimaryAttack) && !base.MagazinePrimaryEmpty)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.StopReload();
				return;
			}
			if (this.TimeSinceLastShellInsert > (base.MagazinePrimaryEmpty ? this.FirstShellInsertDelay : this.ShellInsertDelay))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				int magazinePrimaryCurrentAmmo = base.MagazinePrimaryCurrentAmmo;
				base.MagazinePrimaryCurrentAmmo = magazinePrimaryCurrentAmmo + 1;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				BaseViewModel viewModelEntity = base.ViewModelEntity;
				if (viewModelEntity != null)
				{
					viewModelEntity.SetAnimParameter("bEmpty", base.MagazinePrimaryEmpty);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TimeSinceLastShellInsert = 0f;
				if (base.MagazinePrimaryCurrentAmmo >= base.Resource.MagazinePrimarySize)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.StopReload();
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ShellInsertEffect();
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x000139DF File Offset: 0x00011BDF
		public override void OnReloadFinish()
		{
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000139E1 File Offset: 0x00011BE1
		public override bool CanReload()
		{
			return !this.FinishingReload && base.CanReload();
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000139F3 File Offset: 0x00011BF3
		public void StopReload()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.IsReloading = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FinishingReload = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceReloadFinished = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.EndShotgunReload();
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00013A30 File Offset: 0x00011C30
		public override void Reload()
		{
			if (base.IsReloading)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.StopReload();
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceLastShellInsert = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.IsReloading = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.InSights = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			AnimatedEntity animatedEntity = this.Owner as AnimatedEntity;
			if (animatedEntity != null)
			{
				animatedEntity.SetAnimParameter("b_reload", true);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ShellInsertEffect();
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00013AAC File Offset: 0x00011CAC
		public override void PlayShootSound()
		{
			if (base.MagazinePrimaryEmpty)
			{
				base.PlaySound(this.ShootLastSound);
				return;
			}
			if (!string.IsNullOrEmpty(this.ServerShootSound))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PlaySound(base.IsServer ? this.ServerShootSound : base.Resource.FireSound);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PlayShootSound();
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00013B10 File Offset: 0x00011D10
		protected override void FrameUpdate()
		{
			float shellReloadFraction;
			if (this.FinishingReload)
			{
				shellReloadFraction = this.TimeSinceReloadFinished / this.ReloadFinishTime;
			}
			else
			{
				shellReloadFraction = this.TimeSinceLastShellInsert / (base.MagazinePrimaryEmpty ? this.FirstShellInsertDelay : this.ShellInsertDelay);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseViewModel viewModelEntity = base.ViewModelEntity;
			if (viewModelEntity != null)
			{
				viewModelEntity.SetAnimParameter("fShellReloadFraction", shellReloadFraction);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.FrameUpdate();
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00013B84 File Offset: 0x00011D84
		[ClientRpc]
		public virtual void ShellInsertEffect()
		{
			if (!this.ShellInsertEffect__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseViewModel viewModelEntity = base.ViewModelEntity;
			if (viewModelEntity != null)
			{
				viewModelEntity.SetAnimParameter("fShellReloadFraction", 0);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseViewModel viewModelEntity2 = base.ViewModelEntity;
			if (viewModelEntity2 == null)
			{
				return;
			}
			viewModelEntity2.SetAnimParameter("bReload", true);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00013BDC File Offset: 0x00011DDC
		[ClientRpc]
		public virtual void EndShotgunReload()
		{
			if (!this.EndShotgunReload__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseViewModel viewModelEntity = base.ViewModelEntity;
			if (viewModelEntity == null)
			{
				return;
			}
			viewModelEntity.SetAnimParameter("bReloadFinished", true);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00013C18 File Offset: 0x00011E18
		protected bool ShellInsertEffect__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("ShellInsertEffect", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(524186228, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00013C78 File Offset: 0x00011E78
		protected bool EndShotgunReload__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("EndShotgunReload", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-571428584, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00013CD8 File Offset: 0x00011ED8
		public virtual void ShellInsertEffect(To toTarget)
		{
			this.ShellInsertEffect__RpcProxy(new To?(toTarget));
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00013CE7 File Offset: 0x00011EE7
		public virtual void EndShotgunReload(To toTarget)
		{
			this.EndShotgunReload__RpcProxy(new To?(toTarget));
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00013CF8 File Offset: 0x00011EF8
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id == -571428584)
			{
				if (!Prediction.WasPredicted("EndShotgunReload", Array.Empty<object>()))
				{
					this.EndShotgunReload();
				}
				return;
			}
			if (id == 524186228)
			{
				if (!Prediction.WasPredicted("ShellInsertEffect", Array.Empty<object>()))
				{
					this.ShellInsertEffect();
				}
				return;
			}
			base.OnCallRemoteProcedure(id, read);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00013D4D File Offset: 0x00011F4D
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarUnmanaged<bool>>(ref this._repback__FinishingReload, "FinishingReload", true, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x0400011B RID: 283
		protected TimeSince TimeSinceReloadFinished = 0f;

		// Token: 0x0400011C RID: 284
		protected TimeSince TimeSinceLastShellInsert = 0f;

		// Token: 0x0400011D RID: 285
		private VarUnmanaged<bool> _repback__FinishingReload = new VarUnmanaged<bool>(false);
	}
}
