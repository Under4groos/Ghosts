using System;
using System.Runtime.CompilerServices;

namespace StalkerRP
{
	// Token: 0x0200003B RID: 59
	public class LimbHead : LimbBase
	{
		// Token: 0x06000256 RID: 598 RVA: 0x0000EB89 File Offset: 0x0000CD89
		protected override void OnLimbBreak()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Host.OnKilled();
		}
	}
}
