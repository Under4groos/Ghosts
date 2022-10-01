using System;
using System.Runtime.CompilerServices;

namespace StalkerRP
{
	// Token: 0x02000022 RID: 34
	public class DamageEffectFlame : DamageEffectBase
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000114 RID: 276 RVA: 0x000095DD File Offset: 0x000077DD
		public virtual string ParticlePath
		{
			get
			{
				return "particles/stalker/anomalies/burner/anomaly_burner_corpse_flames.vpcf";
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000095E4 File Offset: 0x000077E4
		public override void OnEffectAdded()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			DamageEffectsManager.ApplyEffect(this.Host, this.ParticlePath);
		}
	}
}
