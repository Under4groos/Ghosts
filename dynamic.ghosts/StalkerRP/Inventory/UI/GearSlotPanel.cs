using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox.UI;
using StalkerRP.Inventory.Gear;

namespace StalkerRP.Inventory.UI
{
	// Token: 0x02000116 RID: 278
	public class GearSlotPanel : Panel
	{
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x00032A35 File Offset: 0x00030C35
		// (set) Token: 0x06000C48 RID: 3144 RVA: 0x00032A3D File Offset: 0x00030C3D
		public SlotID SlotID { get; set; }

		// Token: 0x06000C49 RID: 3145 RVA: 0x00032A46 File Offset: 0x00030C46
		public GearSlotPanel()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GearSlotPanel.gearSlotPanels.Add(this);
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x00032A60 File Offset: 0x00030C60
		public static GearSlotPanel FindPanelUnderMouse()
		{
			foreach (GearSlotPanel gearSlotPanel in GearSlotPanel.gearSlotPanels)
			{
				if (gearSlotPanel.IsMouseInside())
				{
					return gearSlotPanel;
				}
			}
			return null;
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x00032ABC File Offset: 0x00030CBC
		public override void OnDeleted()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GearSlotPanel.gearSlotPanels.Remove(this);
		}

		// Token: 0x040003EF RID: 1007
		public static readonly List<GearSlotPanel> gearSlotPanels = new List<GearSlotPanel>();
	}
}
