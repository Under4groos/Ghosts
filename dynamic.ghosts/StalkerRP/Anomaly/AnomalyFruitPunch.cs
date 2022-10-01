using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;
using SandboxEditor;
using StalkerRP.Debug;

namespace StalkerRP.Anomaly
{
	// Token: 0x02000136 RID: 310
	[DebugSpawnable(Name = "Fruit Punch", Category = "Anomalies")]
	[Title("Fruitpunch")]
	[HammerEntity]
	[Category("Anomalies")]
	[EditorModel("models/stalker/anomalies/fruit_punch.vmdl", "white", "white")]
	[Spawnable]
	public class AnomalyFruitPunch : AnomalyBase
	{
		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x00036A0E File Offset: 0x00034C0E
		[Description("How long in seconds after the active state ends the anomaly waits before exploding.")]
		private float BurstDamage
		{
			get
			{
				return 33f;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x00036A15 File Offset: 0x00034C15
		private float BurstForce
		{
			get
			{
				return 2f;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x00036A1C File Offset: 0x00034C1C
		private float BurstRadius
		{
			get
			{
				return this.TriggerSize * 1f;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x00036A2A File Offset: 0x00034C2A
		public override float ActiveCooldown
		{
			get
			{
				return 0.7f;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x00036A31 File Offset: 0x00034C31
		public override float ActiveTime
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x00036A38 File Offset: 0x00034C38
		public override float ActivationDelay
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x00036A3F File Offset: 0x00034C3F
		public override float TriggerSize
		{
			get
			{
				return 45f;
			}
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00036A46 File Offset: 0x00034C46
		public override void ClientSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StartIdleSound();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetUpLight();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateIdleEffect();
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00036A69 File Offset: 0x00034C69
		private void StartIdleSound()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleSound = Sound.FromEntity("fruitpunch.idle", this);
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00036A81 File Offset: 0x00034C81
		protected override void OnAnomalyActivated()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnAnomalyActivated();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoBurst();
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00036A99 File Offset: 0x00034C99
		private void DoBurst()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DealBurstDamage();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoBurstEffect();
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00036AB4 File Offset: 0x00034CB4
		private void DealBurstDamage()
		{
			Vector3 pos = this.Position;
			foreach (Entity entity in Entity.FindInSphere(this.Position, this.BurstRadius))
			{
				if (!(entity is AnomalyBase))
				{
					Vector3 dir = (entity.Position - pos).Normal;
					float damage = this.BurstDamage;
					DamageInfo damageInfo = default(DamageInfo);
					damageInfo = damageInfo.WithAttacker(this, null);
					damageInfo = damageInfo.WithFlag(DamageFlags.Acid);
					damageInfo = damageInfo.WithFlag(DamageFlags.Cook);
					DamageInfo dmginfo = damageInfo.WithForce(dir * damage * this.BurstForce);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					dmginfo.Damage = damage;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					entity.ApplyAbsoluteImpulse(dir * damage * this.BurstForce);
					if (DamageEffectsManager.IsOrganicTarget(entity))
					{
						DamageEffectChem chem = new DamageEffectChem();
						RuntimeHelpers.EnsureSufficientExecutionStack();
						entity.ApplyDamageEffect(chem);
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					entity.TakeDamage(dmginfo);
				}
			}
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00036BDC File Offset: 0x00034DDC
		public override bool IsValidTriggerEnt(Entity ent)
		{
			return !ent.IsWorld && !ent.Tags.Has("ENT_ANOMALY");
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00036BFB File Offset: 0x00034DFB
		protected override void OnDestroy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnDestroy();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleSound.Stop();
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00036C19 File Offset: 0x00034E19
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

		// Token: 0x06000D93 RID: 3475 RVA: 0x00036C54 File Offset: 0x00034E54
		[ClientRpc]
		private void CreateIdleEffect()
		{
			if (!this.CreateIdleEffect__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create("particles/stalker/anomalies/chemical/anomaly_fruitpunch_idle_ground.vpcf", this, null, true);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00036C88 File Offset: 0x00034E88
		[Event.HotloadAttribute]
		private void SetUpLight()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			PointLightEntity lightEntity = this.LightEntity;
			if (lightEntity != null)
			{
				lightEntity.Delete();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LightEntity = new PointLightEntity();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LightEntity.SetParent(this, null, null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LightEntity.Position = this.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LightEntity.Enabled = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LightEntity.EnableShadowCasting = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LightEntity.Brightness = 0.08f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LightEntity.Falloff = 1f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LightEntity.Range = 200f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LightEntity.Color = Color.FromBytes(212, 255, 0, 255);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00036D74 File Offset: 0x00034F74
		[ClientRpc]
		private void DoBurstEffect()
		{
			if (!this.DoBurstEffect__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PlaySound("chem.hit");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create("particles/stalker/anomalies/chemical/anomaly_fruitpunch_burst.vpcf", this, null, true);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00036DB8 File Offset: 0x00034FB8
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
			using (NetWrite writer = NetWrite.StartRpc(511308901, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00036E18 File Offset: 0x00035018
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
			using (NetWrite writer = NetWrite.StartRpc(-1478370564, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00036E78 File Offset: 0x00035078
		private void CreateIdleEffect(To toTarget)
		{
			this.CreateIdleEffect__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00036E87 File Offset: 0x00035087
		private void DoBurstEffect(To toTarget)
		{
			this.DoBurstEffect__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00036E98 File Offset: 0x00035098
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id == -1478370564)
			{
				if (!Prediction.WasPredicted("DoBurstEffect", Array.Empty<object>()))
				{
					this.DoBurstEffect();
				}
				return;
			}
			if (id == 511308901)
			{
				if (!Prediction.WasPredicted("CreateIdleEffect", Array.Empty<object>()))
				{
					this.CreateIdleEffect();
				}
				return;
			}
			base.OnCallRemoteProcedure(id, read);
		}

		// Token: 0x04000471 RID: 1137
		private Sound IdleSound;

		// Token: 0x04000472 RID: 1138
		private PointLightEntity LightEntity;
	}
}
