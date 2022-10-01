using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;
using StalkerRP.Inventory.UI;

namespace StalkerRP.Inventory.Gear
{
	// Token: 0x02000125 RID: 293
	public abstract class GearSlot : BaseNetworkable
	{
		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x000351AD File Offset: 0x000333AD
		// (set) Token: 0x06000D01 RID: 3329 RVA: 0x000351C4 File Offset: 0x000333C4
		[Net]
		[Local]
		public unsafe StalkerPlayer Player
		{
			get
			{
				return *this._repback__Player.GetValue();
			}
			set
			{
				VarUnmanaged<EntityHandle<StalkerPlayer>> repback__Player = this._repback__Player;
				EntityHandle<StalkerPlayer> entityHandle = value;
				repback__Player.SetValue(entityHandle);
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x000351E5 File Offset: 0x000333E5
		// (set) Token: 0x06000D03 RID: 3331 RVA: 0x000351F3 File Offset: 0x000333F3
		[Net]
		[Local]
		public unsafe SlotID SlotID
		{
			get
			{
				return *this._repback__SlotID.GetValue();
			}
			set
			{
				this._repback__SlotID.SetValue(value);
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x00035202 File Offset: 0x00033402
		// (set) Token: 0x06000D05 RID: 3333 RVA: 0x00035210 File Offset: 0x00033410
		[Net]
		[Local]
		public unsafe EquipmentType EquipmentType
		{
			get
			{
				return *this._repback__EquipmentType.GetValue();
			}
			set
			{
				this._repback__EquipmentType.SetValue(value);
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x0003521F File Offset: 0x0003341F
		// (set) Token: 0x06000D07 RID: 3335 RVA: 0x0003522C File Offset: 0x0003342C
		[Net]
		[Local]
		[Change("OnItemChanged")]
		public ItemResource ItemResource
		{
			get
			{
				return this._repback__ItemResource.GetValue();
			}
			set
			{
				this._repback__ItemResource.SetValue(value);
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x0003523A File Offset: 0x0003343A
		// (set) Token: 0x06000D09 RID: 3337 RVA: 0x00035242 File Offset: 0x00033442
		public Item Item { get; set; }

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0003524B File Offset: 0x0003344B
		public bool Occupied
		{
			get
			{
				return this.ItemResource != null;
			}
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00035256 File Offset: 0x00033456
		public GearSlot()
		{
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0003528C File Offset: 0x0003348C
		public GearSlot(SlotID slotId, EquipmentType equipmentType)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SlotID = slotId;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.EquipmentType = equipmentType;
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x000352E3 File Offset: 0x000334E3
		public void Reset()
		{
			if (this.Occupied)
			{
				this.UnequipItem();
			}
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x000352F3 File Offset: 0x000334F3
		public void UnequipItem()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ItemResource = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnItemUnequipped();
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0003530C File Offset: 0x0003350C
		public void EquipItem(Item item)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Item = item;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ItemResource = item.Resource;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnItemEquipped();
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00035336 File Offset: 0x00033536
		protected virtual void OnItemEquipped()
		{
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00035338 File Offset: 0x00033538
		protected virtual void OnItemUnequipped()
		{
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0003533A File Offset: 0x0003353A
		private void OnItemChanged()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StalkerGearTab instance = StalkerGearTab.Instance;
			if (instance == null)
			{
				return;
			}
			instance.PlayerGear.UpdateSlotForGearSlot(this);
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00035358 File Offset: 0x00033558
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarUnmanaged<EntityHandle<StalkerPlayer>>>(ref this._repback__Player, "Player", false, true);
			builder.Register<VarUnmanaged<SlotID>>(ref this._repback__SlotID, "SlotID", false, true);
			builder.Register<VarUnmanaged<EquipmentType>>(ref this._repback__EquipmentType, "EquipmentType", false, true);
			builder.Register<VarGeneric<ItemResource>>(ref this._repback__ItemResource, "ItemResource", false, true);
			this._repback__ItemResource.SetCallback<ItemResource>(new Action(this.OnItemChanged));
			base.BuildNetworkTable(builder);
		}

		// Token: 0x04000433 RID: 1075
		private VarUnmanaged<EntityHandle<StalkerPlayer>> _repback__Player = new VarUnmanaged<EntityHandle<StalkerPlayer>>();

		// Token: 0x04000434 RID: 1076
		private VarUnmanaged<SlotID> _repback__SlotID = new VarUnmanaged<SlotID>();

		// Token: 0x04000435 RID: 1077
		private VarUnmanaged<EquipmentType> _repback__EquipmentType = new VarUnmanaged<EquipmentType>();

		// Token: 0x04000436 RID: 1078
		private VarGeneric<ItemResource> _repback__ItemResource = new VarGeneric<ItemResource>();
	}
}
