using System;
using System.Runtime.CompilerServices;

namespace StalkerRP
{
	// Token: 0x0200003C RID: 60
	public class LimbLeg : LimbBase
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000EBA3 File Offset: 0x0000CDA3
		protected override float SpillOverDamageMultiplier
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000EBAA File Offset: 0x0000CDAA
		protected override void OnLimbBreak()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Host.StaminaComponent.AddStaminaMultiplier(-0.25f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Host.HealthComponent.OnLegBroken();
		}
	}
}
