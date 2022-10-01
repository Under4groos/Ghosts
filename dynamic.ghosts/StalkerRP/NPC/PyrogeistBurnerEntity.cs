using System;
using System.Runtime.CompilerServices;
using Sandbox;
using StalkerRP.Anomaly;
using StalkerRP.PostProcessing;

namespace StalkerRP.NPC
{
	// Token: 0x020000B9 RID: 185
	public class PyrogeistBurnerEntity : Entity
	{
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x00023D60 File Offset: 0x00021F60
		private string chargeEffect
		{
			get
			{
				return "particles/stalker/monsters/pyrogeist_burner_charge.vpcf";
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x00023D67 File Offset: 0x00021F67
		private float burnDamageDelay
		{
			get
			{
				return 0.1f;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x00023D6E File Offset: 0x00021F6E
		private float burnRadius
		{
			get
			{
				return 250f;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x00023D75 File Offset: 0x00021F75
		private float burnDamagePerSecondActive
		{
			get
			{
				return 160f;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x00023D7C File Offset: 0x00021F7C
		private float burnDamageFallOffPower
		{
			get
			{
				return 0.5f;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x00023D83 File Offset: 0x00021F83
		private float burstDamage
		{
			get
			{
				return 400f;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x00023D8A File Offset: 0x00021F8A
		private float range
		{
			get
			{
				return 300f;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x00023D91 File Offset: 0x00021F91
		private float coneAngle
		{
			get
			{
				return 0.7853982f;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x00023D98 File Offset: 0x00021F98
		private float maxLiveTime
		{
			get
			{
				return 10f;
			}
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00023DA0 File Offset: 0x00021FA0
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Transmit = TransmitType.Always;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.DeleteAsync(this.maxLiveTime);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.burnTrigger = new BurnerEntityTrigger();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.burnTrigger.SetTriggerSize(this.burnRadius);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.burnTrigger.SetParent(this, null, null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoDamage(2.5f);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00023E1C File Offset: 0x0002201C
		public void Setup(Vector3 position, Vector3 direction, float liveTime)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Position = position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Rotation = Rotation.LookAt(direction);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.burnTrigger.Position = this.Position + this.Rotation.Forward * this.range / 2f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetupChargeEffect(position, direction);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.DeleteAsync(liveTime);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00023E9E File Offset: 0x0002209E
		protected override void OnDestroy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles particles = this.chargeEffectParticles;
			if (particles == null)
			{
				return;
			}
			particles.Destroy(false);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00023EB6 File Offset: 0x000220B6
		public override void ClientSpawn()
		{
			PostProcessEmitterComponent postProcessEmitterComponent = this.Components.Create<PostProcessEmitterComponent>(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			postProcessEmitterComponent.SetUpEmitter(EffectsPostProcessManager.STALKER_ANOMALY_BURNER, 450f, 1.6f, "", 0f, 0f);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00023EEC File Offset: 0x000220EC
		private void DoDamage(float wait)
		{
			PyrogeistBurnerEntity.<DoDamage>d__25 <DoDamage>d__;
			<DoDamage>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<DoDamage>d__.<>4__this = this;
			<DoDamage>d__.wait = wait;
			<DoDamage>d__.<>1__state = -1;
			<DoDamage>d__.<>t__builder.Start<PyrogeistBurnerEntity.<DoDamage>d__25>(ref <DoDamage>d__);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00023F2B File Offset: 0x0002212B
		private void SetupChargeEffect(Vector3 position, Vector3 direction)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.chargeEffectParticles = Particles.Create(this.chargeEffect, position);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.chargeEffectParticles.SetPosition(1, direction);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00023F56 File Offset: 0x00022156
		private bool CanDoBurnDamage()
		{
			return this.timeSinceLastBurnDamage >= this.burnDamageDelay;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00023F70 File Offset: 0x00022170
		[Description("Damage dealt over time by the anomaly. Should deal enough damage to be a problem, but not enough to kill a player who's just jumping through one.")]
		private void DealBurnDamage()
		{
			float damage = this.burnDamagePerSecondActive;
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

		// Token: 0x06000817 RID: 2071 RVA: 0x00024060 File Offset: 0x00022260
		[Event.Tick.ServerAttribute]
		private void Update()
		{
			if (!this.doingDamage)
			{
				return;
			}
			if (this.CanDoBurnDamage())
			{
				this.DealBurnDamage();
			}
		}

		// Token: 0x040002AA RID: 682
		private TimeSince timeSinceLastBurnDamage = 0f;

		// Token: 0x040002AB RID: 683
		private BurnerEntityTrigger burnTrigger;

		// Token: 0x040002AC RID: 684
		private bool doingDamage;

		// Token: 0x040002AD RID: 685
		private Particles chargeEffectParticles;
	}
}
