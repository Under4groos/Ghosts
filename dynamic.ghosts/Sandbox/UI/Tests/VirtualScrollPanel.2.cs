using System;
using System.Runtime.CompilerServices;

namespace Sandbox.UI.Tests
{
	// Token: 0x020001D9 RID: 473
	public class VirtualScrollPanel<T> : VirtualScrollPanel where T : Panel, new()
	{
		// Token: 0x060017CC RID: 6092 RVA: 0x000638AD File Offset: 0x00061AAD
		public override void OnCellCreated(int i, Panel cell)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			cell.AddChild<T>(null);
		}
	}
}
