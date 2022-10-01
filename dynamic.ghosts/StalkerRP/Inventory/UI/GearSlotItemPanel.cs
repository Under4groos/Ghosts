using System;
using System.Runtime.CompilerServices;
using Sandbox.UI;
using Sandbox.UI.Construct;
using StalkerRP.Inventory.Gear;

namespace StalkerRP.Inventory.UI
{
	// Token: 0x02000115 RID: 277
	public class GearSlotItemPanel : Panel
	{
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x00032837 File Offset: 0x00030A37
		public bool CanDrop
		{
			get
			{
				return this.slot.ItemResource.CanDrop;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x00032849 File Offset: 0x00030A49
		public SlotID SlotID
		{
			get
			{
				return this.slot.SlotID;
			}
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00032856 File Offset: 0x00030A56
		public GearSlotItemPanel()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.image = base.Add.Image(null, "itemImage");
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0003287A File Offset: 0x00030A7A
		public void SetGearSlot(GearSlot slot)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.slot = slot;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.image.SetTexture(slot.ItemResource.Icon);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x000328A4 File Offset: 0x00030AA4
		private void UpdateHoverPanel()
		{
			if (InventoryItemDragPanel.DragPanel == null)
			{
				Panel panel = base.FindRootPanel();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				InventoryItemDragPanel.DragPanel = panel.AddChild<InventoryItemDragPanel>(null);
			}
			InventoryItemDragPanel dragPanel = InventoryItemDragPanel.DragPanel;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			dragPanel.SetActive(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			dragPanel.Item = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			dragPanel.SetGearSlot(this.slot);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			dragPanel.PositionToMouse();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			dragPanel.SetClass("hidden", false);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00032918 File Offset: 0x00030B18
		protected override void OnMouseDown(MousePanelEvent e)
		{
			if (e.Button.Equals("mouseleft"))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.SetClass("hidden", true);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				e.StopPropagation();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.UpdateHoverPanel();
			}
			if (e.Button.Equals("mouseright"))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				e.StopPropagation();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CreateActionPanel();
			}
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00032986 File Offset: 0x00030B86
		private void CreateActionPanel()
		{
			InventoryItemActionPanel inventoryItemActionPanel = new InventoryItemActionPanel(this, Popup.PositionMode.BelowCenter, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			inventoryItemActionPanel.Title = this.slot.ItemResource.Title;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			inventoryItemActionPanel.CreateActionsForGearSlotItemPanel(this);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x000329BC File Offset: 0x00030BBC
		protected override void OnMouseUp(MousePanelEvent e)
		{
			if (!e.Button.Equals("mouseleft"))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetClass("hidden", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			e.StopPropagation();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			InventoryItemDragPanel.DragPanel.SetActive(false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			InventoryItemDragPanel.DragPanel.SetClass("hidden", true);
			if (InventoryItemDragPanel.DragPanel.TryDropOnItemFromGearSlot())
			{
				return;
			}
			InventoryItemDragPanel.DragPanel.TryDropOnContainer();
		}

		// Token: 0x040003ED RID: 1005
		private Image image;

		// Token: 0x040003EE RID: 1006
		private GearSlot slot;
	}
}
