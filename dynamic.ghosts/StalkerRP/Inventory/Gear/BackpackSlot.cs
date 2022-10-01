using System;
using System.Runtime.CompilerServices;

namespace StalkerRP.Inventory.Gear
{
	// Token: 0x02000123 RID: 291
	public class BackpackSlot : GearSlot
	{
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x000350DD File Offset: 0x000332DD
		private StorageItemResource StorageItemResource
		{
			get
			{
				return base.ItemResource as StorageItemResource;
			}
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x000350EA File Offset: 0x000332EA
		public BackpackSlot(SlotID backpack, EquipmentType equipmentType) : base(backpack, equipmentType)
		{
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x000350F4 File Offset: 0x000332F4
		protected override void OnItemEquipped()
		{
			Inventory inv = base.Item.GetOrCreateInventory();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			inv.SetOwner(base.Player.Client);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			inv.AddListener(base.Player.Client);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			inv.FullySyncWithListeners();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Player.InventoryComponent.AddInventory(inv);
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0003515C File Offset: 0x0003335C
		protected override void OnItemUnequipped()
		{
			Inventory inv = base.Item.GetOrCreateInventory();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			inv.RemoveListener(base.Player.Client);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			inv.RemoveOwner();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Player.InventoryComponent.RemoveInventory(inv);
		}
	}
}
