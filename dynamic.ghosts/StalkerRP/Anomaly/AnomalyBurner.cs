using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;
using SandboxEditor;
using StalkerRP.Debug;
using StalkerRP.PostProcessing;

namespace StalkerRP.Anomaly
{
	// Token: 0x02000139 RID: 313
	[DebugSpawnable(Name = "Burner", Category = "Anomalies")]
	[Title("Burner")]
	[HammerEntity]
	[Category("Anomalies")]
	[EditorModel("models/stalker/anomalies/burner.vmdl", "white", "white")]
	[Spawnable]
	public class AnomalyBurner : AnomalyBase
	{
		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x00037A0E File Offset: 0x00035C0E
		public override float ActiveCooldown
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x00037A15 File Offset: 0x00035C15
		public override float ActiveTime
		{
			get
			{
				return 10f;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x00037A1C File Offset: 0x00035C1C
		public override float ActivationDelay
		{
			get
			{
				return 0.08f;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x00037A23 File Offset: 0x00035C23
		public override float TriggerSize
		{
			get
			{
				return 40f;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x00037A2A File Offset: 0x00035C2A
		[Description("How long in seconds after the active state ends the anomaly waits before exploding.")]
		private float burstDamage
		{
			get
			{
				return 200f;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x00037A31 File Offset: 0x00035C31
		private float burstForce
		{
			get
			{
				return 50f;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x00037A38 File Offset: 0x00035C38
		private float burnDamageDelay
		{
			get
			{
				return 0.1f;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x00037A3F File Offset: 0x00035C3F
		[Description("The radius at which the anomaly will begin dealing burn damage to entities.")]
		private float burnRadius
		{
			get
			{
				return 200f;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x00037A46 File Offset: 0x00035C46
		private float burnDamagePerSecondInactive
		{
			get
			{
				return 10f;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x00037A4D File Offset: 0x00035C4D
		private float burnDamagePerSecondActive
		{
			get
			{
				return 100f;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x00037A54 File Offset: 0x00035C54
		private float burnDamageFallOffPower
		{
			get
			{
				return 2f;
			}
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x00037A5C File Offset: 0x00035C5C
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.burnTrigger = new BurnerEntityTrigger();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.burnTrigger.SetTriggerSize(this.burnRadius);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.burnTrigger.SetParent(this, null, null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.burnTrigger.Position = this.Position;
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x00037ACC File Offset: 0x00035CCC
		public override void ClientSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateIdleEffect();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StartIdleSound();
			PostProcessEmitterComponent postProcessEmitterComponent = this.Components.Create<PostProcessEmitterComponent>(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			postProcessEmitterComponent.SetUpEmitter(EffectsPostProcessManager.STALKER_ANOMALY_BURNER, 450f, 2f, "", 0f, 0f);
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x00037B23 File Offset: 0x00035D23
		public override void OnNetworkDormant()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.idleSound.Stop();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles particles = this.idleHeatWave;
			if (particles != null)
			{
				particles.Destroy(false);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles particles2 = this.activatedParticle;
			if (particles2 == null)
			{
				return;
			}
			particles2.Destroy(false);
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x00037B63 File Offset: 0x00035D63
		public override void OnNetworkActive()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateIdleEffect();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StartIdleSound();
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x00037B7B File Offset: 0x00035D7B
		private void StartIdleSound()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.idleSound = Sound.FromEntity("burner.idle", this);
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x00037B93 File Offset: 0x00035D93
		protected override void OnAnomalyInitialTrigger()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.InitialTriggerEffects();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.idleSound.SetVolume(0.1f);
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x00037BB6 File Offset: 0x00035DB6
		protected override void OnAnomalyActivated()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnAnomalyActivated();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateActiveEffects();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoBurst();
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x00037BD9 File Offset: 0x00035DD9
		protected override void OnAnomalyActiveFinished()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ActiveFinishedEffects();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.idleSound.SetVolume(1f);
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x00037BFC File Offset: 0x00035DFC
		private void DoBurst()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DealBurstDamage();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoBurstEffect();
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00037C14 File Offset: 0x00035E14
		public override bool IsValidTriggerEnt(Entity ent)
		{
			return !ent.IsWorld;
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x00037C1F File Offset: 0x00035E1F
		public override void OnHitByBolt(BoltProjectile bolt)
		{
			if (!base.IsTriggered)
			{
				bool isActive = base.IsActive;
			}
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00037C30 File Offset: 0x00035E30
		protected override void AnomalyTick()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AnomalyTick();
			if (this.CanDoBurnDamage())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DealBurnDamage();
			}
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x00037C50 File Offset: 0x00035E50
		protected override void OnDestroy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnDestroy();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.idleSound.Stop();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles particles = this.idleHeatWave;
			if (particles != null)
			{
				particles.Destroy(false);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles particles2 = this.activatedParticle;
			if (particles2 == null)
			{
				return;
			}
			particles2.Destroy(false);
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00037CA8 File Offset: 0x00035EA8
		private void DealBurstDamage()
		{
			Vector3 pos = this.Position;
			foreach (Entity entity in base.TouchingEntities)
			{
				if (!(entity is AnomalyBase))
				{
					Vector3 dir = (entity.Position - pos).Normal;
					DamageInfo damageInfo = default(DamageInfo);
					damageInfo = damageInfo.WithAttacker(this, null);
					damageInfo = damageInfo.WithFlag(DamageFlags.Burn);
					damageInfo = damageInfo.WithFlag(DamageFlags.Cook);
					DamageInfo dmginfo = damageInfo.WithForce(dir * this.burstForce);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					dmginfo.Damage = this.burstDamage;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					entity.ApplyAbsoluteImpulse(dir * this.burstForce);
					if (DamageEffectsManager.IsOrganicTarget(entity))
					{
						entity.ApplyDamageEffect(new DamageEffectFlame());
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					entity.TakeDamage(dmginfo);
				}
			}
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00037DAC File Offset: 0x00035FAC
		private bool CanDoBurnDamage()
		{
			return this.timeSinceLastBurnDamage >= this.burnDamageDelay;
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00037DC4 File Offset: 0x00035FC4
		[Description("Damage dealt over time by the anomaly. Should deal enough damage to be a problem, but not enough to kill a player who's just jumping through one.")]
		private void DealBurnDamage()
		{
			float damage = base.IsActive ? this.burnDamagePerSecondActive : this.burnDamagePerSecondInactive;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			damage *= this.burnDamageDelay;
			foreach (Entity entity in this.burnTrigger.TriggeringEntities)
			{
				float distance = entity.Position.Distance(this.Position);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				damage /= MathF.Pow(distance / 39.3701f, this.burnDamageFallOffPower);
				DamageInfo damageInfo = default(DamageInfo);
				damageInfo = damageInfo.WithAttacker(this, null);
				damageInfo = damageInfo.WithFlag(DamageFlags.Cook);
				DamageInfo info = damageInfo.WithFlag(DamageFlags.Burn);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				info.Damage = damage;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				entity.TakeDamage(info);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastBurnDamage = 0f;
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00037EC4 File Offset: 0x000360C4
		public override void DebugDraw()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.DebugDraw();
			if (AnomalyBase.stalker_debug_anomaly_draw > 1)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.DebugOverlay.Sphere(this.Position, this.burnRadius, Color.Orange, 0f, true);
			}
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00037F00 File Offset: 0x00036100
		[ClientRpc]
		private void ActiveFinishedEffects()
		{
			if (!this.ActiveFinishedEffects__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateIdleEffect();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles particles = this.activatedParticle;
			if (particles == null)
			{
				return;
			}
			particles.Destroy(false);
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00037F40 File Offset: 0x00036140
		[ClientRpc]
		private void CreateIdleEffect()
		{
			if (!this.CreateIdleEffect__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.idleHeatWave = Particles.Create("particles/stalker/anomalies/burner/anomaly_burner_idle_haze.vpcf", this, null, true);
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x00037F78 File Offset: 0x00036178
		[ClientRpc]
		private void CreateActiveEffects()
		{
			if (!this.CreateActiveEffects__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.activatedParticle = Particles.Create("particles/stalker/anomalies/burner/anomaly_burner_blowout.vpcf", this, null, true);
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x00037FB0 File Offset: 0x000361B0
		[ClientRpc]
		private void InitialTriggerEffects()
		{
			if (!this.InitialTriggerEffects__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles particles = this.idleHeatWave;
			if (particles == null)
			{
				return;
			}
			particles.Destroy(false);
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x00037FE8 File Offset: 0x000361E8
		[ClientRpc]
		private void EndTriggeredEffects()
		{
			if (!this.EndTriggeredEffects__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles particles = this.activatedParticle;
			if (particles == null)
			{
				return;
			}
			particles.Destroy(false);
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x00038020 File Offset: 0x00036220
		[ClientRpc]
		private void DoBurstEffect()
		{
			if (!this.DoBurstEffect__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PlaySound("burner.blowout");
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00038050 File Offset: 0x00036250
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

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00038084 File Offset: 0x00036284
		protected bool ActiveFinishedEffects__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("ActiveFinishedEffects", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-1615561408, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x000380E4 File Offset: 0x000362E4
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
			using (NetWrite writer = NetWrite.StartRpc(884982821, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00038144 File Offset: 0x00036344
		protected bool CreateActiveEffects__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("CreateActiveEffects", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(1975635126, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x000381A4 File Offset: 0x000363A4
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
			using (NetWrite writer = NetWrite.StartRpc(-40645432, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00038204 File Offset: 0x00036404
		protected bool EndTriggeredEffects__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("EndTriggeredEffects", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-1638146880, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x00038264 File Offset: 0x00036464
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
			using (NetWrite writer = NetWrite.StartRpc(1624544828, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x000382C4 File Offset: 0x000364C4
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
			using (NetWrite writer = NetWrite.StartRpc(-446148764, this))
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

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00038358 File Offset: 0x00036558
		private void ActiveFinishedEffects(To toTarget)
		{
			this.ActiveFinishedEffects__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00038367 File Offset: 0x00036567
		private void CreateIdleEffect(To toTarget)
		{
			this.CreateIdleEffect__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00038376 File Offset: 0x00036576
		private void CreateActiveEffects(To toTarget)
		{
			this.CreateActiveEffects__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00038385 File Offset: 0x00036585
		private void InitialTriggerEffects(To toTarget)
		{
			this.InitialTriggerEffects__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00038394 File Offset: 0x00036594
		private void EndTriggeredEffects(To toTarget)
		{
			this.EndTriggeredEffects__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x000383A3 File Offset: 0x000365A3
		private void DoBurstEffect(To toTarget)
		{
			this.DoBurstEffect__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x000383B2 File Offset: 0x000365B2
		private void DoBoltHitEffect(To toTarget, Vector3 pos)
		{
			this.DoBoltHitEffect__RpcProxy(pos, new To?(toTarget));
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x000383C4 File Offset: 0x000365C4
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id <= -446148764)
			{
				if (id == -1638146880)
				{
					if (!Prediction.WasPredicted("EndTriggeredEffects", Array.Empty<object>()))
					{
						this.EndTriggeredEffects();
					}
					return;
				}
				if (id == -1615561408)
				{
					if (!Prediction.WasPredicted("ActiveFinishedEffects", Array.Empty<object>()))
					{
						this.ActiveFinishedEffects();
					}
					return;
				}
				if (id == -446148764)
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
			}
			else if (id <= 884982821)
			{
				if (id == -40645432)
				{
					if (!Prediction.WasPredicted("InitialTriggerEffects", Array.Empty<object>()))
					{
						this.InitialTriggerEffects();
					}
					return;
				}
				if (id == 884982821)
				{
					if (!Prediction.WasPredicted("CreateIdleEffect", Array.Empty<object>()))
					{
						this.CreateIdleEffect();
					}
					return;
				}
			}
			else
			{
				if (id == 1624544828)
				{
					if (!Prediction.WasPredicted("DoBurstEffect", Array.Empty<object>()))
					{
						this.DoBurstEffect();
					}
					return;
				}
				if (id == 1975635126)
				{
					if (!Prediction.WasPredicted("CreateActiveEffects", Array.Empty<object>()))
					{
						this.CreateActiveEffects();
					}
					return;
				}
			}
			base.OnCallRemoteProcedure(id, read);
		}

		// Token: 0x04000478 RID: 1144
		private Particles idleHeatWave;

		// Token: 0x04000479 RID: 1145
		private Particles activatedParticle;

		// Token: 0x0400047A RID: 1146
		private Sound idleSound;

		// Token: 0x0400047B RID: 1147
		private BurnerEntityTrigger burnTrigger;

		// Token: 0x0400047C RID: 1148
		private TimeSince timeSinceLastBurnDamage = 0f;
	}
}
