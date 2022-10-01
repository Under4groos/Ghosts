using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;
using Sandbox.UI;

namespace StalkerRP.Inventory.UI
{
	// Token: 0x0200011E RID: 286
	public class StalkerGearTab : Panel
	{
		// Token: 0x06000CB2 RID: 3250 RVA: 0x00033F80 File Offset: 0x00032180
		public StalkerGearTab()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StalkerGearTab.Instance = this;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StyleSheet.Load("inventory/ui/StalkerGearTab.scss", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PlayerGear = base.AddChild<PlayerGear>("cell");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PlayerInventory = base.AddChild<InventoryPage>("cell");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LootInventory = base.AddChild<InventoryPage>("cell");
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00033FF8 File Offset: 0x000321F8
		public void CreatePlayerInventory()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Creating player inventory!");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PlayerInventory.Clear();
			foreach (Inventory inventory in ClientInventoryManager.LocalInventoriesCache.Values)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.PlayerInventory.AddInventory(inventory);
			}
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00034080 File Offset: 0x00032280
		public void CreateLootInventory()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Creating loot inventory!");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LootInventory.Clear();
			foreach (Inventory inventory in ClientInventoryManager.InventoryCache.Values)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.LootInventory.AddInventory(inventory);
			}
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00034108 File Offset: 0x00032308
		public void CreatePlayerGear()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PlayerGear.CreateFromPlayer(Local.Pawn as StalkerPlayer);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00034124 File Offset: 0x00032324
		public void OnClosed()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			InventoryItemDragPanel dragPanel = InventoryItemDragPanel.DragPanel;
			if (dragPanel != null)
			{
				dragPanel.SetActive(false);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			InventoryItemDragPanel dragPanel2 = InventoryItemDragPanel.DragPanel;
			if (dragPanel2 == null)
			{
				return;
			}
			dragPanel2.SetClass("hidden", true);
		}

		// Token: 0x0400040C RID: 1036
		public static StalkerGearTab Instance;

		// Token: 0x0400040D RID: 1037
		public PlayerGear PlayerGear;

		// Token: 0x0400040E RID: 1038
		public InventoryPage PlayerInventory;

		// Token: 0x0400040F RID: 1039
		public InventoryPage LootInventory;
	}
}
