using System;
using System.Runtime.CompilerServices;
using Sandbox.UI;
using StalkerRP.Inventory.Gear;

namespace StalkerRP.Inventory.UI
{
	// Token: 0x02000118 RID: 280
	public class InventoryItemActionPanel : Popup
	{
		// Token: 0x06000C59 RID: 3161 RVA: 0x00032D1B File Offset: 0x00030F1B
		public InventoryItemActionPanel(Panel sourcePanel, Popup.PositionMode position, float offset) : base(sourcePanel, position, offset)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StyleSheet.Load("inventory/ui/InventoryItemActionPanel.scss", true);
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00032D3C File Offset: 0x00030F3C
		public void CreateActionsForItemPanel(InventoryItemPanel inventoryItemPanel)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.itemPanel = inventoryItemPanel;
			if (this.itemPanel.Item.EquipmentType != EquipmentType.None)
			{
				base.AddOption("Equip", null, new Action(this.RequestEquip));
			}
			if (this.itemPanel.Item.CanStack && this.itemPanel.Item.Stacks > 1)
			{
				base.AddOption("Split", null, new Action(this.itemPanel.RequestSplit));
			}
			if (this.itemPanel.Item.CanDrop)
			{
				base.AddOption("Drop", null, new Action(this.itemPanel.RequestDrop));
			}
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00032DF4 File Offset: 0x00030FF4
		public void CreateActionsForGearSlotItemPanel(GearSlotItemPanel gearSlotItemPanel)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.slotItemPanel = gearSlotItemPanel;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddOption("Unequip", null, new Action(this.RequestUnequip));
			if (this.slotItemPanel.CanDrop)
			{
				base.AddOption("Drop", null, new Action(this.RequestEquipmentDrop));
			}
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00032E54 File Offset: 0x00031054
		private void RequestEquip()
		{
			InventoryEquipRequest request = new InventoryEquipRequest(this.itemPanel.InventoryNetID, this.itemPanel.ContainerIndex, this.itemPanel.ItemIndex, SlotID.None, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ServerInventoryManager.RequestEquipItem(request.ToString());
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00032EA4 File Offset: 0x000310A4
		private void RequestUnequip()
		{
			InventoryUnequipRequest request = new InventoryUnequipRequest(this.slotItemPanel.SlotID);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ServerInventoryManager.RequestUnequipItem(request.ToString());
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00032EDC File Offset: 0x000310DC
		private void RequestEquipmentDrop()
		{
			InventoryDropRequest request = new InventoryDropRequest(this.slotItemPanel.SlotID);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ServerInventoryManager.RequestDropItem(request.ToString());
		}

		// Token: 0x040003F6 RID: 1014
		private InventoryItemPanel itemPanel;

		// Token: 0x040003F7 RID: 1015
		private GearSlotItemPanel slotItemPanel;
	}
}
