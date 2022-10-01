using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;

namespace StalkerRP
{
	// Token: 0x0200005E RID: 94
	public class Weapon : BaseWeapon, IUse
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x00015E95 File Offset: 0x00014095
		public virtual float ReloadTime
		{
			get
			{
				return 3f;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x00015E9C File Offset: 0x0001409C
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x00015EA4 File Offset: 0x000140A4
		public PickupTrigger PickupTrigger { get; protected set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00015EAD File Offset: 0x000140AD
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x00015EBF File Offset: 0x000140BF
		[Net]
		[Predicted]
		public unsafe TimeSince TimeSinceReload
		{
			get
			{
				return *this._repback__TimeSinceReload.GetValue();
			}
			set
			{
				this._repback__TimeSinceReload.SetValue(value);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00015ECE File Offset: 0x000140CE
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x00015EDC File Offset: 0x000140DC
		[Net]
		[Predicted]
		public unsafe bool IsReloading
		{
			get
			{
				return *this._repback__IsReloading.GetValue();
			}
			set
			{
				this._repback__IsReloading.SetValue(value);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00015EEB File Offset: 0x000140EB
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x00015EFD File Offset: 0x000140FD
		[Net]
		[Predicted]
		public unsafe TimeSince TimeSinceDeployed
		{
			get
			{
				return *this._repback__TimeSinceDeployed.GetValue();
			}
			set
			{
				this._repback__TimeSinceDeployed.SetValue(value);
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00015F0C File Offset: 0x0001410C
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PickupTrigger = new PickupTrigger
			{
				Parent = this,
				Position = this.Position,
				EnableTouch = true,
				EnableSelfCollisions = false
			};
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PickupTrigger.PhysicsBody.AutoSleep = false;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00015F6B File Offset: 0x0001416B
		public override void ActiveStart(Entity ent)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ActiveStart(ent);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceDeployed = 0f;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00015F90 File Offset: 0x00014190
		public override void Reload()
		{
			if (this.IsReloading)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceReload = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsReloading = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			AnimatedEntity animatedEntity = this.Owner as AnimatedEntity;
			if (animatedEntity != null)
			{
				animatedEntity.SetAnimParameter("b_reload", true);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StartReloadEffects();
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00015FF4 File Offset: 0x000141F4
		public override void Simulate(Client owner)
		{
			if (this.TimeSinceDeployed < 0.6f)
			{
				return;
			}
			if (!this.IsReloading)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Simulate(owner);
			}
			if (this.IsReloading && this.TimeSinceReload > this.ReloadTime)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnReloadFinish();
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001604E File Offset: 0x0001424E
		public virtual void OnReloadFinish()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsReloading = false;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0001605C File Offset: 0x0001425C
		[ClientRpc]
		public virtual void StartReloadEffects()
		{
			if (!this.StartReloadEffects__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseViewModel viewModelEntity = base.ViewModelEntity;
			if (viewModelEntity == null)
			{
				return;
			}
			viewModelEntity.SetAnimParameter("reload", true);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00016098 File Offset: 0x00014298
		public override void CreateViewModel()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertClient("CreateViewModel");
			if (string.IsNullOrEmpty(this.ViewModelPath))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ViewModelEntity = new ViewModel
			{
				Position = this.Position,
				Owner = this.Owner,
				EnableViewmodelRendering = true
			};
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ViewModelEntity.SetModel(this.ViewModelPath);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00016107 File Offset: 0x00014307
		public bool OnUse(Entity user)
		{
			if (this.Owner != null)
			{
				return false;
			}
			if (!user.IsValid())
			{
				return false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			user.StartTouch(this);
			return false;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0001612A File Offset: 0x0001432A
		public virtual bool IsUsable(Entity user)
		{
			return user is Player && this.Owner == null;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00016141 File Offset: 0x00014341
		public void Remove()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Delete();
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00016150 File Offset: 0x00014350
		[ClientRpc]
		protected virtual void ShootEffects()
		{
			if (!this.ShootEffects__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertClient("ShootEffects");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create("particles/pistol_muzzleflash.vpcf", this.EffectEntity, "muzzle", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseViewModel viewModelEntity = base.ViewModelEntity;
			if (viewModelEntity == null)
			{
				return;
			}
			viewModelEntity.SetAnimParameter("fire", true);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000161B8 File Offset: 0x000143B8
		[Description("Shoot a single bullet")]
		public virtual void ShootBullet(Vector3 pos, Vector3 dir, float spread, float force, float damage, float bulletSize)
		{
			Vector3 forward = dir;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			forward += (Vector3.Random + Vector3.Random + Vector3.Random + Vector3.Random) * spread * 0.25f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			forward = forward.Normal;
			foreach (TraceResult tr in this.TraceBullet(pos, pos + forward * 5000f, bulletSize))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				tr.Surface.DoBulletImpact(tr);
				if (base.IsServer && tr.Entity.IsValid())
				{
					using (Prediction.Off())
					{
						DamageInfo damageInfo = DamageInfo.FromBullet(tr.EndPosition, forward * 100f * force, damage).UsingTraceResult(tr).WithAttacker(this.Owner, null).WithWeapon(this);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						tr.Entity.TakeDamage(damageInfo);
					}
				}
			}
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00016304 File Offset: 0x00014504
		[Description("Shoot a single bullet from owners view point")]
		public virtual void ShootBullet(float spread, float force, float damage, float bulletSize)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ShootBullet(this.Owner.EyePosition, this.Owner.EyeRotation.Forward, spread, force, damage, bulletSize);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00016340 File Offset: 0x00014540
		[Description("Shoot a multiple bullets from owners view point")]
		public virtual void ShootBullets(int numBullets, float spread, float force, float damage, float bulletSize)
		{
			Vector3 pos = this.Owner.EyePosition;
			StalkerPlayer player = this.Owner as StalkerPlayer;
			if (player != null)
			{
				pos = player.GetShootPosition();
			}
			Vector3 dir = this.Owner.EyeRotation.Forward;
			for (int i = 0; i < numBullets; i++)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ShootBullet(pos, dir, spread, force / (float)numBullets, damage, bulletSize);
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000163AC File Offset: 0x000145AC
		protected bool StartReloadEffects__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("StartReloadEffects", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(63195871, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001640C File Offset: 0x0001460C
		protected bool ShootEffects__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("ShootEffects", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-1887504719, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0001646C File Offset: 0x0001466C
		public virtual void StartReloadEffects(To toTarget)
		{
			this.StartReloadEffects__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0001647B File Offset: 0x0001467B
		protected virtual void ShootEffects(To toTarget)
		{
			this.ShootEffects__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001648C File Offset: 0x0001468C
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id == -1887504719)
			{
				if (!Prediction.WasPredicted("ShootEffects", Array.Empty<object>()))
				{
					this.ShootEffects();
				}
				return;
			}
			if (id == 63195871)
			{
				if (!Prediction.WasPredicted("StartReloadEffects", Array.Empty<object>()))
				{
					this.StartReloadEffects();
				}
				return;
			}
			base.OnCallRemoteProcedure(id, read);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x000164E4 File Offset: 0x000146E4
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarUnmanaged<TimeSince>>(ref this._repback__TimeSinceReload, "TimeSinceReload", true, false);
			builder.Register<VarUnmanaged<bool>>(ref this._repback__IsReloading, "IsReloading", true, false);
			builder.Register<VarUnmanaged<TimeSince>>(ref this._repback__TimeSinceDeployed, "TimeSinceDeployed", true, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x04000141 RID: 321
		private VarUnmanaged<TimeSince> _repback__TimeSinceReload = new VarUnmanaged<TimeSince>();

		// Token: 0x04000142 RID: 322
		private VarUnmanaged<bool> _repback__IsReloading = new VarUnmanaged<bool>();

		// Token: 0x04000143 RID: 323
		private VarUnmanaged<TimeSince> _repback__TimeSinceDeployed = new VarUnmanaged<TimeSince>();
	}
}
