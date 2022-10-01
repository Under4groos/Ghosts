using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace StalkerRP.Inventory.UI
{
	// Token: 0x0200011C RID: 284
	public class InventoryPanel : Panel
	{
		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x00033BCD File Offset: 0x00031DCD
		// (set) Token: 0x06000C99 RID: 3225 RVA: 0x00033BD5 File Offset: 0x00031DD5
		public Inventory Inventory { get; set; }

		// Token: 0x06000C9A RID: 3226 RVA: 0x00033BDE File Offset: 0x00031DDE
		public InventoryPanel()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StyleSheet.Load("inventory/ui/InventoryPanel.scss", true);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00033BFC File Offset: 0x00031DFC
		public void SetInventory(Inventory inventory)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Inventory = inventory;
			base.Add.Panel("header").Add.Button(inventory.Title, null);
			Panel containerBox = base.Add.Panel("containerbox");
			List<Container> containers = inventory.GetContainers();
			for (int i = 0; i < containers.Count; i++)
			{
				InventoryContainerPanel inventoryContainerPanel = containerBox.AddChild<InventoryContainerPanel>(null);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				inventoryContainerPanel.ContainerIndex = i;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				inventoryContainerPanel.InventoryNetID = inventory.InventoryNetID;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				inventoryContainerPanel.SetContainer(containers[i]);
			}
		}
	}
}
