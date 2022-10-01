using System;
using System.Runtime.CompilerServices;
using Sandbox;
using SandboxEditor;
using StalkerRP.NPC;

namespace StalkerRP.Anomaly
{
	// Token: 0x02000137 RID: 311
	[Title("Gas")]
	[HammerEntity]
	[Category("Anomalies")]
	[Solid]
	[AutoApplyMaterial("materials/tools/fogvolume.vmat")]
	public class AnomalyGas : TriggerMultiple
	{
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x00036EF5 File Offset: 0x000350F5
		private float EmitPerUnitVolume
		{
			get
			{
				return 1E-05f;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x00036EFC File Offset: 0x000350FC
		// (set) Token: 0x06000D9E RID: 3486 RVA: 0x00036F04 File Offset: 0x00035104
		[Property("Intensity", "How intense the particle effect should be.")]
		[DefaultValue(1)]
		public float Intensity { get; set; } = 1f;

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x00036F10 File Offset: 0x00035110
		private float Volume
		{
			get
			{
				return base.Model.PhysicsBounds.Volume;
			}
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00036F30 File Offset: 0x00035130
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableShadowReceive = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableShadowCasting = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Transmit = TransmitType.Pvs;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("ENT_ANOMALY");
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00036F84 File Offset: 0x00035184
		public override void ClientSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ClientSpawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.gasParticles = Particles.Create("particles/stalker/anomalies/chemical/anomaly_gas.vpcf", this, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.gasParticles.SetPositionComponent(1, 0, this.Volume * this.EmitPerUnitVolume);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00036FD2 File Offset: 0x000351D2
		[Event.HotloadAttribute]
		private void OnHotload()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles particles = this.gasParticles;
			if (particles == null)
			{
				return;
			}
			particles.SetPositionComponent(1, 0, this.Volume * this.EmitPerUnitVolume);
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00036FF8 File Offset: 0x000351F8
		public override bool PassesTriggerFilters(Entity other)
		{
			return other is Player || other is NPCBase;
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00037010 File Offset: 0x00035210
		public override void OnTriggered(Entity other)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnTriggered(other);
			if (!base.IsServer)
			{
				return;
			}
			DamageInfo damageInfo = default(DamageInfo);
			damageInfo = damageInfo.WithFlag(DamageFlags.Acid);
			DamageInfo dmg = damageInfo.WithFlag(DamageFlags.Cook);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			dmg.Damage = 5f;
			foreach (Entity entity in base.TouchingEntities)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				entity.TakeDamage(dmg);
			}
		}

		// Token: 0x04000474 RID: 1140
		private Particles gasParticles;
	}
}
