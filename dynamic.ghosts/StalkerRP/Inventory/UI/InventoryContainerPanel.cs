using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox.UI;

namespace StalkerRP.Inventory.UI
{
	// Token: 0x02000117 RID: 279
	public class InventoryContainerPanel : Panel
	{
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x00032ADB File Offset: 0x00030CDB
		// (set) Token: 0x06000C4E RID: 3150 RVA: 0x00032AE3 File Offset: 0x00030CE3
		public Container Container { get; set; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x00032AEC File Offset: 0x00030CEC
		// (set) Token: 0x06000C50 RID: 3152 RVA: 0x00032AF4 File Offset: 0x00030CF4
		public int ContainerIndex { get; set; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x00032AFD File Offset: 0x00030CFD
		// (set) Token: 0x06000C52 RID: 3154 RVA: 0x00032B05 File Offset: 0x00030D05
		public int InventoryNetID { get; set; }

		// Token: 0x06000C53 RID: 3155 RVA: 0x00032B0E File Offset: 0x00030D0E
		public InventoryContainerPanel()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StyleSheet.Load("inventory/ui/InventoryContainerPanel.scss", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			InventoryContainerPanel.ContainerPanels.Add(this);
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00032B3C File Offset: 0x00030D3C
		public static InventoryContainerPanel FindContainerAndGridPositionForItemPanel(Vector2 screenPos, InventoryItemPanel panel, out GridPosition pos)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pos = GridPosition.Invalid;
			foreach (InventoryContainerPanel containerPanel in InventoryContainerPanel.ContainerPanels)
			{
				Vector2 containerPos = new Vector2(containerPanel.Box.Left, containerPanel.Box.Top);
				Vector2 deltaPos = screenPos - containerPos;
				int x = (int)MathF.Round(deltaPos.x / (50f * containerPanel.ScaleToScreen));
				int y = (int)MathF.Round(deltaPos.y / (50f * containerPanel.ScaleToScreen));
				GridPosition gridPos = new GridPosition(x, y);
				if (containerPanel.Container.IsValidGridPosition(gridPos))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					pos = gridPos;
					return containerPanel;
				}
			}
			return null;
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00032C20 File Offset: 0x00030E20
		public void SetContainer(Container container)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Container = container;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Style.Width = Length.Pixels((float)(50 * container.Size.Width));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Style.Height = Length.Pixels((float)(50 * container.Size.Height));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateItems();
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00032C8C File Offset: 0x00030E8C
		private void CreateItems()
		{
			List<Item> items = this.Container.GetItems();
			for (int i = 0; i < items.Count; i++)
			{
				InventoryItemPanel inventoryItemPanel = base.AddChild<InventoryItemPanel>(null);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				inventoryItemPanel.ItemIndex = i;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				inventoryItemPanel.ContainerIndex = this.ContainerIndex;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				inventoryItemPanel.InventoryNetID = this.InventoryNetID;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				inventoryItemPanel.SetItem(items[i]);
			}
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x00032CFC File Offset: 0x00030EFC
		public override void OnDeleted()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			InventoryContainerPanel.ContainerPanels.Remove(this);
		}

		// Token: 0x040003F1 RID: 1009
		private static readonly List<InventoryContainerPanel> ContainerPanels = new List<InventoryContainerPanel>();

		// Token: 0x040003F5 RID: 1013
		public const int InventoryGridPixelsPerTile = 50;
	}
}
