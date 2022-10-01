using System;
using System.Runtime.CompilerServices;
using Sandbox.UI;

namespace StalkerRP.Inventory.UI
{
	// Token: 0x0200011F RID: 287
	public class StalkerHealthTab : Panel
	{
		// Token: 0x06000CB7 RID: 3255 RVA: 0x00034156 File Offset: 0x00032356
		public StalkerHealthTab()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StyleSheet.Load("inventory/ui/StalkerHealthTab.scss", true);
		}
	}
}
