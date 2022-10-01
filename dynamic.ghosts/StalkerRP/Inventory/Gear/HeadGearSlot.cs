using System;
using System.Runtime.CompilerServices;

namespace StalkerRP.Inventory.Gear
{
	// Token: 0x02000126 RID: 294
	public class HeadGearSlot : GearSlot
	{
		// Token: 0x06000D14 RID: 3348 RVA: 0x000353CF File Offset: 0x000335CF
		public HeadGearSlot(SlotID headgear, EquipmentType equipmentType) : base(headgear, equipmentType)
		{
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x000353DC File Offset: 0x000335DC
		protected override void OnItemEquipped()
		{
			HeadGearResource asset = base.ItemResource as HeadGearResource;
			if (asset != null)
			{
				base.Player.HeadGearComponent.OnHeadGearEquipped(asset);
			}
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00035409 File Offset: 0x00033609
		protected override void OnItemUnequipped()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Player.HeadGearComponent.OnHeadGearUnequipped();
		}
	}
}
