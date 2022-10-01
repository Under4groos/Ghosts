using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.UI;
using StalkerRP.Inventory.Gear;

namespace StalkerRP.Inventory.UI
{
	// Token: 0x02000119 RID: 281
	public class InventoryItemDragPanel : InventoryItemPanel
	{
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x00032F12 File Offset: 0x00031112
		// (set) Token: 0x06000C60 RID: 3168 RVA: 0x00032F19 File Offset: 0x00031119
		public static InventoryItemDragPanel DragPanel { get; set; }

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x00032F21 File Offset: 0x00031121
		// (set) Token: 0x06000C62 RID: 3170 RVA: 0x00032F29 File Offset: 0x00031129
		public GearSlot GearSlot { get; set; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x00032F32 File Offset: 0x00031132
		private float PixelWidth
		{
			get
			{
				float num = (float)50;
				GearSlot gearSlot = this.GearSlot;
				return num * (float)((gearSlot != null) ? gearSlot.ItemResource.Width : base.Item.Size.Width);
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x00032F5E File Offset: 0x0003115E
		private float PixelHeight
		{
			get
			{
				float num = (float)50;
				GearSlot gearSlot = this.GearSlot;
				return num * (float)((gearSlot != null) ? gearSlot.ItemResource.Height : base.Item.Size.Height);
			}
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x00032F8A File Offset: 0x0003118A
		public InventoryItemDragPanel()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StyleSheet.Load("inventory/ui/InventoryItemDragPanel.scss", true);
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x00032FA8 File Offset: 0x000311A8
		public void SetActive(bool b)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.active = b;
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00032FB6 File Offset: 0x000311B6
		public override void Tick()
		{
			if (!this.active)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PositionToMouse();
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00032FCC File Offset: 0x000311CC
		public void PositionToMouse()
		{
			Vector2 position = Mouse.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			position.x = (position.x - base.Box.Rect.Width / 2f) * base.ScaleFromScreen;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			position.y = (position.y - base.Box.Rect.Height / 2f) * base.ScaleFromScreen;
			Vector2 size = base.Rotated ? new Vector2(this.PixelHeight, this.PixelWidth) : new Vector2(this.PixelWidth, this.PixelHeight);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Style.SetRect(new Rect(position, size));
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00033088 File Offset: 0x00031288
		protected override void Resize()
		{
			if (base.Rotated)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Style.Width = Length.Pixels(this.PixelHeight);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Style.Height = Length.Pixels(this.PixelWidth);
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Style.Width = Length.Pixels(this.PixelWidth);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Style.Height = Length.Pixels(this.PixelHeight);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseStyles style = this.image.Style;
			float num = (float)50;
			GearSlot gearSlot = this.GearSlot;
			style.Width = new Length?(num * (float)((gearSlot != null) ? gearSlot.ItemResource.Width : base.Item.Size.Width));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseStyles style2 = this.image.Style;
			float num2 = (float)50;
			GearSlot gearSlot2 = this.GearSlot;
			style2.Height = new Length?(num2 * (float)((gearSlot2 != null) ? gearSlot2.ItemResource.Height : base.Item.Size.Height));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.image.SetClass("rotated", base.Rotated);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x000331B8 File Offset: 0x000313B8
		public void SetGearSlot(GearSlot slot)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.GearSlot = slot;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Resize();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.image.SetTexture(slot.ItemResource.Icon);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetRotation(false);
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x000331F8 File Offset: 0x000313F8
		public bool TryDropOnContainer()
		{
			GridPosition gridPosition;
			InventoryContainerPanel panel = InventoryContainerPanel.FindContainerAndGridPositionForItemPanel(new Vector2(base.Box.Left, base.Box.Top), this, out gridPosition);
			if (panel == null)
			{
				return false;
			}
			if (this.GearSlot != null)
			{
				this.RequestUnequip(panel, gridPosition);
			}
			else
			{
				base.RequestMove(panel, gridPosition);
			}
			return true;
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0003324C File Offset: 0x0003144C
		public bool TryDropOnGearSlot()
		{
			GearSlotPanel panel = GearSlotPanel.FindPanelUnderMouse();
			if (panel == null)
			{
				return false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.RequestEquip(panel);
			return true;
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00033274 File Offset: 0x00031474
		public bool TryDropOnItem()
		{
			InventoryItemPanel panel = InventoryItemPanel.FindPanelUnderMouse();
			if (panel == null)
			{
				return false;
			}
			if (panel.Item == base.Item)
			{
				return false;
			}
			if (panel.Item.HasItemInventory())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.RequestStore(panel.Item.GetItemInventoryNetID());
				return true;
			}
			if (base.Item.CanStackOnItem(panel.Item))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.RequestStackMerge(panel);
			}
			return false;
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x000332E4 File Offset: 0x000314E4
		public bool TryDropOnItemFromGearSlot()
		{
			InventoryItemPanel panel = InventoryItemPanel.FindPanelUnderMouse();
			if (panel == null)
			{
				return false;
			}
			if (panel.Item.HasItemInventory())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.RequestStoreFormGearSlot(panel.Item.GetItemInventoryNetID());
				return true;
			}
			return false;
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00033324 File Offset: 0x00031524
		private void RequestUnequip(InventoryContainerPanel containerPanel, GridPosition position)
		{
			InventoryUnequipRequest request = new InventoryUnequipRequest(containerPanel.InventoryNetID, containerPanel.ContainerIndex, position.X, position.Y, base.Rotated, this.GearSlot.SlotID, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ServerInventoryManager.RequestUnequipItem(request.ToString());
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0003337C File Offset: 0x0003157C
		private void RequestStoreFormGearSlot(int targetInventoryNetID)
		{
			InventoryStoreFromGearSlotRequest request = new InventoryStoreFromGearSlotRequest(this.GearSlot.SlotID, targetInventoryNetID);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ServerInventoryManager.RequestStoreItemFromGearSlot(request.ToString());
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x000333B3 File Offset: 0x000315B3
		protected override void OnMouseDown(MousePanelEvent e)
		{
			if (e.Button.Equals("mouseright"))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.ToggleRotation();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				e.StopPropagation();
			}
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x000333DD File Offset: 0x000315DD
		protected override void OnMouseUp(MousePanelEvent e)
		{
		}

		// Token: 0x040003F9 RID: 1017
		private bool active;
	}
}
