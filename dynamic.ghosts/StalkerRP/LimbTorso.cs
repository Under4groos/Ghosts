using System;
using System.Runtime.CompilerServices;

namespace StalkerRP
{
	// Token: 0x0200003E RID: 62
	public class LimbTorso : LimbBase
	{
		// Token: 0x0600025D RID: 605 RVA: 0x0000EBF2 File Offset: 0x0000CDF2
		protected override void OnLimbBreak()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Host.OnKilled();
		}
	}
}
