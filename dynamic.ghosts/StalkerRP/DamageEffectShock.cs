using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x02000023 RID: 35
	public class DamageEffectShock : DamageEffectBase
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00009604 File Offset: 0x00007804
		public virtual string ParticlePath
		{
			get
			{
				return "particles/stalker/anomalies/electro/anomaly_electro_corpse_sparks.vpcf";
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000960B File Offset: 0x0000780B
		private float ImpulseDelayMinTime
		{
			get
			{
				return 0.05f;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00009612 File Offset: 0x00007812
		private float ImpulseDelayMaxTime
		{
			get
			{
				return 0.3f;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00009619 File Offset: 0x00007819
		private float ImpulseForceMax
		{
			get
			{
				return 900f;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00009620 File Offset: 0x00007820
		private float ImpulseForceMin
		{
			get
			{
				return 400f;
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00009628 File Offset: 0x00007828
		public override void OnEffectAdded()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			DamageEffectsManager.ApplyEffect(this.Host, this.ParticlePath);
			DeathRagdoll rag = this.Host as DeathRagdoll;
			if (rag != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.IsRagdoll = true;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Ragdoll = rag;
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsRagdoll = false;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00009680 File Offset: 0x00007880
		public override void EffectTick()
		{
			if (!this.IsRagdoll)
			{
				return;
			}
			if (this.TimeSinceLastImpulse > this.ImpulseDelay)
			{
				PhysicsBody bone = this.Ragdoll.GetBonePhysicsBody(Rand.Int(this.Ragdoll.BoneCount - 1));
				if (!bone.IsValid())
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				bone.ApplyImpulse(Vector3.Random.Normal * Rand.Float(this.ImpulseForceMin, this.ImpulseForceMax) * bone.Mass);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ImpulseDelay = Rand.Float(this.ImpulseDelayMinTime, this.ImpulseDelayMaxTime);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TimeSinceLastImpulse = 0f;
			}
		}

		// Token: 0x0400006D RID: 109
		public bool IsRagdoll;

		// Token: 0x0400006E RID: 110
		public DeathRagdoll Ragdoll;

		// Token: 0x0400006F RID: 111
		private TimeSince TimeSinceLastImpulse = 0f;

		// Token: 0x04000070 RID: 112
		private float ImpulseDelay = 0.5f;
	}
}
