using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox.UI;
using Sandbox.UI.Construct;
using StalkerRP.Inventory.Gear;

namespace StalkerRP.Inventory.UI
{
	// Token: 0x0200011A RID: 282
	public class InventoryItemPanel : Panel
	{
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x000333DF File Offset: 0x000315DF
		// (set) Token: 0x06000C74 RID: 3188 RVA: 0x000333E7 File Offset: 0x000315E7
		public Item Item { get; set; }

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x000333F0 File Offset: 0x000315F0
		// (set) Token: 0x06000C76 RID: 3190 RVA: 0x000333F8 File Offset: 0x000315F8
		public int ItemIndex { get; set; }

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x00033401 File Offset: 0x00031601
		// (set) Token: 0x06000C78 RID: 3192 RVA: 0x00033409 File Offset: 0x00031609
		public int ContainerIndex { get; set; }

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x00033412 File Offset: 0x00031612
		// (set) Token: 0x06000C7A RID: 3194 RVA: 0x0003341A File Offset: 0x0003161A
		public int InventoryNetID { get; set; }

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x00033423 File Offset: 0x00031623
		// (set) Token: 0x06000C7C RID: 3196 RVA: 0x0003342B File Offset: 0x0003162B
		public bool Rotated { get; set; }

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x00033434 File Offset: 0x00031634
		private float ItemPixelWidth
		{
			get
			{
				return (float)(50 * (this.Rotated ? this.Item.Size.Height : this.Item.Size.Width));
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x00033464 File Offset: 0x00031664
		private float ItemPixelHeight
		{
			get
			{
				return (float)(50 * (this.Rotated ? this.Item.Size.Width : this.Item.Size.Height));
			}
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00033494 File Offset: 0x00031694
		public InventoryItemPanel()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StyleSheet.Load("inventory/ui/InventoryItemPanel.scss", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.image = base.Add.Image(null, "itemImage");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.stacksLabel = base.Add.Label("1", "item-stacks");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			InventoryItemPanel.itemPanels.Add(this);
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0003350C File Offset: 0x0003170C
		public static void RefreshPanelForItem(Item item)
		{
			foreach (InventoryItemPanel inventoryItemPanel in InventoryItemPanel.itemPanels)
			{
				if (!(inventoryItemPanel is InventoryItemDragPanel) && inventoryItemPanel.Item == item)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					inventoryItemPanel.Refresh();
					break;
				}
			}
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00033578 File Offset: 0x00031778
		public static InventoryItemPanel FindPanelUnderMouse()
		{
			foreach (InventoryItemPanel itemPanel in InventoryItemPanel.itemPanels)
			{
				if (!(itemPanel is InventoryItemDragPanel) && itemPanel.IsMouseInside())
				{
					return itemPanel;
				}
			}
			return null;
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x000335DC File Offset: 0x000317DC
		protected virtual void Resize()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Style.Width = Length.Pixels(this.ItemPixelWidth);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Style.Height = Length.Pixels(this.ItemPixelWidth);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.image.Style.Width = new Length?((float)(50 * this.Item.Size.Width));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.image.Style.Height = new Length?((float)(50 * this.Item.Size.Height));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.image.SetClass("rotated", this.Rotated);
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x000336A0 File Offset: 0x000318A0
		public void SetItem(Item item)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Item = item;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.image.SetTexture(item.Resource.Icon);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateStacks();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetRotation(item.Size.IsRotated);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Resize();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RepositionPanel();
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0003370C File Offset: 0x0003190C
		private void UpdateStacks()
		{
			if (!this.Item.CanStack || this.Item.Stacks <= 0)
			{
				this.stacksLabel.SetText("");
				return;
			}
			this.stacksLabel.SetText(this.Item.Stacks.ToString());
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00033763 File Offset: 0x00031963
		public void Refresh()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateStacks();
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00033770 File Offset: 0x00031970
		protected void SetRotation(bool rotation)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Rotated = rotation;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Resize();
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00033789 File Offset: 0x00031989
		protected void ToggleRotation()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetRotation(!this.Rotated);
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x000337A0 File Offset: 0x000319A0
		public void RepositionPanel()
		{
			int x = 50 * this.Item.Position.X;
			int y = 50 * this.Item.Position.Y;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Style.SetRect(new Rect((float)x, (float)y, this.ItemPixelWidth, this.ItemPixelHeight));
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x000337FC File Offset: 0x000319FC
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
			dragPanel.GearSlot = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			dragPanel.SetItem(this.Item);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			dragPanel.ItemIndex = this.ItemIndex;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			dragPanel.ContainerIndex = this.ContainerIndex;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			dragPanel.InventoryNetID = this.InventoryNetID;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			dragPanel.PositionToMouse();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			dragPanel.SetClass("hidden", false);
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x000338A2 File Offset: 0x00031AA2
		private void CreateActionPanel()
		{
			InventoryItemActionPanel inventoryItemActionPanel = new InventoryItemActionPanel(this, Popup.PositionMode.BelowCenter, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			inventoryItemActionPanel.Title = this.Item.Title;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			inventoryItemActionPanel.CreateActionsForItemPanel(this);
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x000338D4 File Offset: 0x00031AD4
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

		// Token: 0x06000C8C RID: 3212 RVA: 0x00033944 File Offset: 0x00031B44
		public void RequestDrop()
		{
			InventoryDropRequest request = new InventoryDropRequest(this.InventoryNetID, this.ContainerIndex, this.ItemIndex, 1, SlotID.None);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ServerInventoryManager.RequestDropItem(request.ToString());
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00033984 File Offset: 0x00031B84
		public void RequestSplit()
		{
			InventorySplitStackRequest request = new InventorySplitStackRequest(this.InventoryNetID, this.ContainerIndex, this.ItemIndex);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ServerInventoryManager.RequestSplitItemStack(request.ToString());
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x000339C4 File Offset: 0x00031BC4
		protected void RequestEquip(GearSlotPanel panel)
		{
			InventoryEquipRequest request = new InventoryEquipRequest(this.InventoryNetID, this.ContainerIndex, this.ItemIndex, panel.SlotID, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ServerInventoryManager.RequestEquipItem(request.ToString());
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00033A08 File Offset: 0x00031C08
		protected void RequestMove(InventoryContainerPanel containerPanel, GridPosition position)
		{
			InventoryMoveRequest request = new InventoryMoveRequest(this.InventoryNetID, this.ContainerIndex, this.ItemIndex, containerPanel.InventoryNetID, containerPanel.ContainerIndex, position, this.Rotated);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ServerInventoryManager.RequestMoveItem(request.ToString());
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00033A58 File Offset: 0x00031C58
		protected void RequestStackMerge(InventoryItemPanel panel)
		{
			InventoryMergeStacksRequest request = new InventoryMergeStacksRequest(this.InventoryNetID, this.ContainerIndex, this.ItemIndex, panel.InventoryNetID, panel.ContainerIndex, panel.ItemIndex);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ServerInventoryManager.RequestMergeItemStacks(request.ToString());
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00033AA8 File Offset: 0x00031CA8
		protected void RequestStore(int targetInventoryNetID)
		{
			InventoryStoreRequest request = new InventoryStoreRequest(this.InventoryNetID, this.ContainerIndex, this.ItemIndex, targetInventoryNetID);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ServerInventoryManager.RequestStoreItem(request.ToString());
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00033AE8 File Offset: 0x00031CE8
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
			if (InventoryItemDragPanel.DragPanel.TryDropOnItem())
			{
				return;
			}
			if (InventoryItemDragPanel.DragPanel.TryDropOnGearSlot())
			{
				return;
			}
			InventoryItemDragPanel.DragPanel.TryDropOnContainer();
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00033B6E File Offset: 0x00031D6E
		public override void OnDeleted()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			InventoryItemPanel.itemPanels.Remove(this);
		}

		// Token: 0x040003FB RID: 1019
		protected Label stacksLabel;

		// Token: 0x040003FC RID: 1020
		protected Image image;

		// Token: 0x040003FD RID: 1021
		protected static readonly List<InventoryItemPanel> itemPanels = new List<InventoryItemPanel>();
	}
}
