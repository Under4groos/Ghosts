using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;
using SandboxEditor;
using StalkerRP.Debug;

namespace StalkerRP.Anomaly
{
	// Token: 0x02000138 RID: 312
	[DebugSpawnable(Name = "Electro", Category = "Anomalies")]
	[Title("Electro")]
	[HammerEntity]
	[EditorModel("models/stalker/anomalies/electro.vmdl", "white", "white")]
	[Category("Anomalies")]
	[Spawnable]
	public class AnomalyElectro : AnomalyBase
	{
		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x000370BB File Offset: 0x000352BB
		[Description("How long in seconds after the active state ends the anomaly waits before exploding.")]
		private float BurstDamage
		{
			get
			{
				return 200f;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x000370C2 File Offset: 0x000352C2
		private float BurstForce
		{
			get
			{
				return 5f;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x000370C9 File Offset: 0x000352C9
		private float BurstRadius
		{
			get
			{
				return this.TriggerSize * 3f;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x000370D7 File Offset: 0x000352D7
		public override float ActiveCooldown
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x000370DE File Offset: 0x000352DE
		public override float ActiveTime
		{
			get
			{
				return 2f;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x000370E5 File Offset: 0x000352E5
		public override float ActivationDelay
		{
			get
			{
				return 0.08f;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x000370EC File Offset: 0x000352EC
		public override float TriggerSize
		{
			get
			{
				return 40f;
			}
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x000370F3 File Offset: 0x000352F3
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			AnomalyElectro.ElectroAnomalies.Add(this);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00037111 File Offset: 0x00035311
		public override void ClientSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StartIdleSound();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateIdleEffect();
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x00037129 File Offset: 0x00035329
		private void StartIdleSound()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleSound = Sound.FromEntity("electro.idle", this);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00037141 File Offset: 0x00035341
		protected override void OnAnomalyInitialTrigger()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.InitialTriggerEffects();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetIdleSoundVolume(0.1f);
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0003715E File Offset: 0x0003535E
		protected override void OnAnomalyActivated()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnAnomalyActivated();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoBurst();
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00037176 File Offset: 0x00035376
		protected override void OnAnomalyActiveFinished()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ToggleIdleEffect();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetIdleSoundVolume(1f);
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x00037193 File Offset: 0x00035393
		private void DoBurst()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DealBurstDamage();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TriggerNearbyElectroAnomalies();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoBurstEffect();
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x000371B8 File Offset: 0x000353B8
		private void DealBurstDamage()
		{
			Vector3 pos = this.Position;
			foreach (Entity entity in Entity.FindInSphere(this.Position, this.BurstRadius))
			{
				if (!(entity is AnomalyBase))
				{
					float dist = pos.Distance(entity.Position);
					float mult = 1.2f - dist / this.BurstRadius;
					Vector3 dir = (entity.Position - pos).Normal;
					float damage = this.BurstDamage * mult;
					DamageInfo damageInfo = default(DamageInfo);
					damageInfo = damageInfo.WithAttacker(this, null);
					damageInfo = damageInfo.WithFlag(DamageFlags.Shock);
					damageInfo = damageInfo.WithFlag(DamageFlags.Cook);
					DamageInfo dmginfo = damageInfo.WithForce(dir * damage * this.BurstForce);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					dmginfo.Damage = damage;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					entity.ApplyAbsoluteImpulse(dir * damage * this.BurstForce);
					if (DamageEffectsManager.IsOrganicTarget(entity))
					{
						DamageEffectShock shock = new DamageEffectShock();
						RuntimeHelpers.EnsureSufficientExecutionStack();
						entity.ApplyDamageEffect(shock);
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					entity.TakeDamage(dmginfo);
				}
			}
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x00037310 File Offset: 0x00035510
		private void TriggerNearbyElectroAnomalies()
		{
			Vector3 pos = this.Position;
			float maxDist = MathF.Pow(this.BurstRadius * 1.5f, 2f);
			foreach (AnomalyElectro electro in AnomalyElectro.ElectroAnomalies)
			{
				if (electro != this && electro.IsValid() && (electro.Position - pos).LengthSquared <= maxDist)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					electro.TryTriggerAnomaly();
				}
			}
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x000373AC File Offset: 0x000355AC
		public override bool IsValidTriggerEnt(Entity ent)
		{
			return !ent.IsWorld;
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x000373B7 File Offset: 0x000355B7
		public override void OnHitByBolt(BoltProjectile bolt)
		{
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x000373BC File Offset: 0x000355BC
		protected override void OnDestroy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnDestroy();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			AnomalyElectro.ElectroAnomalies.Remove(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleSound.Stop();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles idleElectric = this.IdleElectric;
			if (idleElectric == null)
			{
				return;
			}
			idleElectric.Destroy(false);
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0003740C File Offset: 0x0003560C
		public override void DebugDraw()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.DebugDraw();
			if (AnomalyBase.stalker_debug_anomaly_draw > 1)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.DebugOverlay.Sphere(this.Position, this.BurstRadius, Color.Green, 0f, true);
			}
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00037448 File Offset: 0x00035648
		[ClientRpc]
		private void SetIdleSoundVolume(float volume)
		{
			if (!this.SetIdleSoundVolume__RpcProxy(volume, null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleSound.SetVolume(volume);
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0003747C File Offset: 0x0003567C
		[ClientRpc]
		private void CreateIdleEffect()
		{
			if (!this.CreateIdleEffect__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleElectric = Particles.Create("particles/stalker/anomalies/electro/anomaly_electro_idle_ground.vpcf", this, null, true);
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x000374B4 File Offset: 0x000356B4
		[ClientRpc]
		private void ToggleIdleEffect()
		{
			if (!this.ToggleIdleEffect__RpcProxy(null))
			{
				return;
			}
			if (base.IsValid)
			{
				this.IdleElectric.EnableDrawing = !this.IdleElectric.EnableDrawing;
			}
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x000374F4 File Offset: 0x000356F4
		[ClientRpc]
		private void InitialTriggerEffects()
		{
			if (!this.InitialTriggerEffects__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PlaySound("electro.trigger");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ToggleIdleEffect();
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x00037530 File Offset: 0x00035730
		[ClientRpc]
		private void DoBurstEffect()
		{
			if (!this.DoBurstEffect__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PlaySound("electro.burst");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create("particles/stalker/anomalies/electro/anomaly_electro_burst_ring.vpcf", this, null, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create("particles/stalker/anomalies/electro/anomaly_electro_burst.vpcf", this, null, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create("particles/stalker/anomalies/electro/anomaly_electro_burst_light.vpcf", this, null, true);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0003759C File Offset: 0x0003579C
		[ClientRpc]
		private void DoBoltHitEffect(Vector3 pos)
		{
			if (!this.DoBoltHitEffect__RpcProxy(pos, null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create("particles/stalker/anomalies/grav_bolt_impact.vpcf", pos);
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x000375D0 File Offset: 0x000357D0
		protected bool SetIdleSoundVolume__RpcProxy(float volume, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("SetIdleSoundVolume", new object[]
				{
					volume
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(1309817535, this))
			{
				if (!NetRead.IsSupported(volume))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] SetIdleSoundVolume is not allowed to use Single for the parameter 'volume'!");
					return false;
				}
				writer.Write<float>(volume);
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x00037664 File Offset: 0x00035864
		protected bool CreateIdleEffect__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("CreateIdleEffect", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(1852050405, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x000376C4 File Offset: 0x000358C4
		protected bool ToggleIdleEffect__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("ToggleIdleEffect", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(1367870587, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00037724 File Offset: 0x00035924
		protected bool InitialTriggerEffects__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("InitialTriggerEffects", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(843384584, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x00037784 File Offset: 0x00035984
		protected bool DoBurstEffect__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("DoBurstEffect", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(897945212, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x000377E4 File Offset: 0x000359E4
		protected bool DoBoltHitEffect__RpcProxy(Vector3 pos, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("DoBoltHitEffect", new object[]
				{
					pos
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(191188900, this))
			{
				if (!NetRead.IsSupported(pos))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] DoBoltHitEffect is not allowed to use Vector3 for the parameter 'pos'!");
					return false;
				}
				writer.Write<Vector3>(pos);
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x00037878 File Offset: 0x00035A78
		private void SetIdleSoundVolume(To toTarget, float volume)
		{
			this.SetIdleSoundVolume__RpcProxy(volume, new To?(toTarget));
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00037888 File Offset: 0x00035A88
		private void CreateIdleEffect(To toTarget)
		{
			this.CreateIdleEffect__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00037897 File Offset: 0x00035A97
		private void ToggleIdleEffect(To toTarget)
		{
			this.ToggleIdleEffect__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x000378A6 File Offset: 0x00035AA6
		private void InitialTriggerEffects(To toTarget)
		{
			this.InitialTriggerEffects__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x000378B5 File Offset: 0x00035AB5
		private void DoBurstEffect(To toTarget)
		{
			this.DoBurstEffect__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x000378C4 File Offset: 0x00035AC4
		private void DoBoltHitEffect(To toTarget, Vector3 pos)
		{
			this.DoBoltHitEffect__RpcProxy(pos, new To?(toTarget));
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x000378D4 File Offset: 0x00035AD4
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id <= 897945212)
			{
				if (id == 191188900)
				{
					Vector3 __pos = read.ReadData<Vector3>(default(Vector3));
					if (!Prediction.WasPredicted("DoBoltHitEffect", new object[]
					{
						__pos
					}))
					{
						this.DoBoltHitEffect(__pos);
					}
					return;
				}
				if (id == 843384584)
				{
					if (!Prediction.WasPredicted("InitialTriggerEffects", Array.Empty<object>()))
					{
						this.InitialTriggerEffects();
					}
					return;
				}
				if (id == 897945212)
				{
					if (!Prediction.WasPredicted("DoBurstEffect", Array.Empty<object>()))
					{
						this.DoBurstEffect();
					}
					return;
				}
			}
			else
			{
				if (id == 1309817535)
				{
					float __volume = 0f;
					__volume = read.ReadData<float>(__volume);
					if (!Prediction.WasPredicted("SetIdleSoundVolume", new object[]
					{
						__volume
					}))
					{
						this.SetIdleSoundVolume(__volume);
					}
					return;
				}
				if (id == 1367870587)
				{
					if (!Prediction.WasPredicted("ToggleIdleEffect", Array.Empty<object>()))
					{
						this.ToggleIdleEffect();
					}
					return;
				}
				if (id == 1852050405)
				{
					if (!Prediction.WasPredicted("CreateIdleEffect", Array.Empty<object>()))
					{
						this.CreateIdleEffect();
					}
					return;
				}
			}
			base.OnCallRemoteProcedure(id, read);
		}

		// Token: 0x04000475 RID: 1141
		private Particles IdleElectric;

		// Token: 0x04000476 RID: 1142
		private Sound IdleSound;

		// Token: 0x04000477 RID: 1143
		private static readonly HashSet<AnomalyElectro> ElectroAnomalies = new HashSet<AnomalyElectro>();
	}
}
