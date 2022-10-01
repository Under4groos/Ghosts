using System;
using System.Runtime.CompilerServices;
using Sandbox.UI;

namespace StalkerRP.Inventory.UI
{
	// Token: 0x0200011B RID: 283
	public class InventoryPage : Panel
	{
		// Token: 0x06000C95 RID: 3221 RVA: 0x00033B8D File Offset: 0x00031D8D
		public InventoryPage()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StyleSheet.Load("inventory/ui/InventoryPage.scss", true);
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00033BAB File Offset: 0x00031DAB
		public void Clear()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.DeleteChildren(true);
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00033BB9 File Offset: 0x00031DB9
		public void AddInventory(Inventory inventory)
		{
			InventoryPanel inventoryPanel = base.AddChild<InventoryPanel>(null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			inventoryPanel.SetInventory(inventory);
		}
	}
}
