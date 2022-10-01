using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.Inventory.Gear
{
	// Token: 0x02000129 RID: 297
	public class WeaponSlot : GearSlot
	{
		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x0003542A File Offset: 0x0003362A
		// (set) Token: 0x06000D19 RID: 3353 RVA: 0x00035432 File Offset: 0x00033632
		private int SlotNumber { get; set; }

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x0003543B File Offset: 0x0003363B
		private WeaponItemResource WeaponItemResource
		{
			get
			{
				return base.ItemResource as WeaponItemResource;
			}
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00035448 File Offset: 0x00033648
		public WeaponSlot(SlotID slotId, EquipmentType equipmentType, int slotNumber) : base(slotId, equipmentType)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SlotNumber = slotNumber;
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0003545E File Offset: 0x0003365E
		public void OnWeaponDropped(Entity entity)
		{
			if (entity == this.weapon)
			{
				base.UnequipItem();
			}
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x0003546F File Offset: 0x0003366F
		private void CreateWeapon()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.weapon = this.WeaponItemResource.CreateWeaponEntity();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Player.EquipmentComponent.AddToSlot((BaseCarriable)this.weapon, this.SlotNumber, true);
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x000354AF File Offset: 0x000336AF
		protected override void OnItemEquipped()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateWeapon();
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x000354BC File Offset: 0x000336BC
		protected override void OnItemUnequipped()
		{
			if (this.weapon.IsValid())
			{
				this.weapon.Delete();
			}
		}

		// Token: 0x04000446 RID: 1094
		private Entity weapon;
	}
}
