using System;
using System.Runtime.CompilerServices;
using Sandbox.UI;
using StalkerRP.Inventory.Gear;

namespace StalkerRP.Inventory.UI
{
	// Token: 0x0200011D RID: 285
	[UseTemplate("inventory/ui/playergear.html")]
	public class PlayerGear : Panel
	{
		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x00033C9B File Offset: 0x00031E9B
		// (set) Token: 0x06000C9D RID: 3229 RVA: 0x00033CA3 File Offset: 0x00031EA3
		public GearSlotPanel HelmetSlot { get; set; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x00033CAC File Offset: 0x00031EAC
		// (set) Token: 0x06000C9F RID: 3231 RVA: 0x00033CB4 File Offset: 0x00031EB4
		public GearSlotPanel HeadGearSlot { get; set; }

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x00033CBD File Offset: 0x00031EBD
		// (set) Token: 0x06000CA1 RID: 3233 RVA: 0x00033CC5 File Offset: 0x00031EC5
		public GearSlotPanel ArmorSlot { get; set; }

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x00033CCE File Offset: 0x00031ECE
		// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x00033CD6 File Offset: 0x00031ED6
		public GearSlotPanel BackpackSlot { get; set; }

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x00033CDF File Offset: 0x00031EDF
		// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x00033CE7 File Offset: 0x00031EE7
		public GearSlotPanel RifleSlot { get; set; }

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x00033CF0 File Offset: 0x00031EF0
		// (set) Token: 0x06000CA7 RID: 3239 RVA: 0x00033CF8 File Offset: 0x00031EF8
		public GearSlotPanel SlingSlot { get; set; }

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x00033D01 File Offset: 0x00031F01
		// (set) Token: 0x06000CA9 RID: 3241 RVA: 0x00033D09 File Offset: 0x00031F09
		public GearSlotPanel PistolSlot { get; set; }

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x00033D12 File Offset: 0x00031F12
		// (set) Token: 0x06000CAB RID: 3243 RVA: 0x00033D1A File Offset: 0x00031F1A
		public GearSlotPanel MeleeSlot { get; set; }

		// Token: 0x06000CAC RID: 3244 RVA: 0x00033D24 File Offset: 0x00031F24
		protected override void PostTemplateApplied()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HelmetSlot.SlotID = SlotID.Helmet;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HeadGearSlot.SlotID = SlotID.HeadGear;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ArmorSlot.SlotID = SlotID.Armor;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.BackpackSlot.SlotID = SlotID.Backpack;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RifleSlot.SlotID = SlotID.Rifle;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SlingSlot.SlotID = SlotID.Sling;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PistolSlot.SlotID = SlotID.Pistol;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MeleeSlot.SlotID = SlotID.Melee;
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00033DBC File Offset: 0x00031FBC
		public void CreateFromPlayer(StalkerPlayer player)
		{
			EquipmentComponent equipment = player.EquipmentComponent;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreatePanelForSlot(this.HelmetSlot, equipment.HelmetSlot);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreatePanelForSlot(this.HeadGearSlot, equipment.HeadGearSlot);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreatePanelForSlot(this.ArmorSlot, equipment.ArmorSlot);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreatePanelForSlot(this.BackpackSlot, equipment.BackpackSlot);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreatePanelForSlot(this.RifleSlot, equipment.RifleSlot);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreatePanelForSlot(this.SlingSlot, equipment.SlingSlot);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreatePanelForSlot(this.PistolSlot, equipment.PistolSlot);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreatePanelForSlot(this.MeleeSlot, equipment.MeleeSlot);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00033E88 File Offset: 0x00032088
		private void CreatePanelForSlot(GearSlotPanel panel, GearSlot slot)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			panel.DeleteChildren(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			panel.SetClass("hide-background", slot.Occupied);
			if (!slot.Occupied)
			{
				return;
			}
			GearSlotItemPanel gearSlotItemPanel = panel.AddChild<GearSlotItemPanel>(null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			gearSlotItemPanel.SetGearSlot(slot);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00033EC8 File Offset: 0x000320C8
		private GearSlotPanel GetSlotFromID(SlotID id)
		{
			GearSlotPanel result;
			switch (id)
			{
			case SlotID.Helmet:
				result = this.HelmetSlot;
				break;
			case SlotID.HeadGear:
				result = this.HeadGearSlot;
				break;
			case SlotID.Armor:
				result = this.ArmorSlot;
				break;
			case SlotID.Rifle:
				result = this.RifleSlot;
				break;
			case SlotID.Sling:
				result = this.SlingSlot;
				break;
			case SlotID.Pistol:
				result = this.PistolSlot;
				break;
			case SlotID.Melee:
				result = this.MeleeSlot;
				break;
			case SlotID.Backpack:
				result = this.BackpackSlot;
				break;
			default:
				result = null;
				break;
			}
			return result;
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00033F4C File Offset: 0x0003214C
		public void UpdateSlotForGearSlot(GearSlot gearSlot)
		{
			GearSlotPanel panel = this.GetSlotFromID(gearSlot.SlotID);
			if (panel != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CreatePanelForSlot(panel, gearSlot);
			}
		}
	}
}
