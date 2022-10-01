using System;
using System.Runtime.CompilerServices;

namespace StalkerRP
{
	// Token: 0x02000021 RID: 33
	public class DamageEffectChem : DamageEffectBase
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000095B6 File Offset: 0x000077B6
		public virtual string ParticlePath
		{
			get
			{
				return "particles/stalker/anomalies/chemical/anomaly_fruitpunch_corpse_burn.vpcf";
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000095BD File Offset: 0x000077BD
		public override void OnEffectAdded()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			DamageEffectsManager.ApplyEffect(this.Host, this.ParticlePath);
		}
	}
}
