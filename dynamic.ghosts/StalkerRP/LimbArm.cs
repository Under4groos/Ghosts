using System;
using System.Runtime.CompilerServices;

namespace StalkerRP
{
	// Token: 0x02000039 RID: 57
	public class LimbArm : LimbBase
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000E917 File Offset: 0x0000CB17
		protected override float SpillOverDamageMultiplier
		{
			get
			{
				return 0.7f;
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000E91E File Offset: 0x0000CB1E
		protected override void OnLimbBreak()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Host.HealthComponent.OnArmBroken();
		}
	}
}
