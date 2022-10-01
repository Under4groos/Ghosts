using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;

namespace StalkerRP.Inventory.Gear
{
	// Token: 0x02000121 RID: 289
	public class EquipmentComponent : EntityComponent<StalkerPlayer>, ISingletonComponent
	{
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x0003443F File Offset: 0x0003263F
		// (set) Token: 0x06000CCA RID: 3274 RVA: 0x0003444C File Offset: 0x0003264C
		[Net]
		private IDictionary<int, BaseCarriable> CarrySlots
		{
			get
			{
				return this._repback__CarrySlots.GetValue();
			}
			set
			{
				this._repback__CarrySlots.SetValue(value);
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x0003445A File Offset: 0x0003265A
		// (set) Token: 0x06000CCC RID: 3276 RVA: 0x00034468 File Offset: 0x00032668
		[Net]
		[Predicted]
		private unsafe int ActiveSlot
		{
			get
			{
				return *this._repback__ActiveSlot.GetValue();
			}
			set
			{
				this._repback__ActiveSlot.SetValue(value);
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x00034477 File Offset: 0x00032677
		// (set) Token: 0x06000CCE RID: 3278 RVA: 0x00034485 File Offset: 0x00032685
		[Net]
		[Predicted]
		[DefaultValue(false)]
		private unsafe bool IsSwitchingSlot
		{
			get
			{
				return *this._repback__IsSwitchingSlot.GetValue();
			}
			set
			{
				this._repback__IsSwitchingSlot.SetValue(value);
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x00034494 File Offset: 0x00032694
		// (set) Token: 0x06000CD0 RID: 3280 RVA: 0x000344A2 File Offset: 0x000326A2
		[Net]
		[Predicted]
		private unsafe int QueuedSlot
		{
			get
			{
				return *this._repback__QueuedSlot.GetValue();
			}
			set
			{
				this._repback__QueuedSlot.SetValue(value);
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x000344B1 File Offset: 0x000326B1
		// (set) Token: 0x06000CD2 RID: 3282 RVA: 0x000344C3 File Offset: 0x000326C3
		[Net]
		[Predicted]
		[DefaultValue(0)]
		private unsafe TimeUntil TimeUntilSwitchFinished
		{
			get
			{
				return *this._repback__TimeUntilSwitchFinished.GetValue();
			}
			set
			{
				this._repback__TimeUntilSwitchFinished.SetValue(value);
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x000344D2 File Offset: 0x000326D2
		private BaseCarriable Active
		{
			get
			{
				return this.GetActive();
			}
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x000344DC File Offset: 0x000326DC
		public BaseCarriable GetActive()
		{
			BaseCarriable active;
			if (!this.CarrySlots.TryGetValue(this.ActiveSlot, out active))
			{
				return null;
			}
			return active;
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00034504 File Offset: 0x00032704
		public BaseCarriable GetCarriableAtSlot(int slot)
		{
			BaseCarriable value;
			if (this.CarrySlots.TryGetValue(slot, out value))
			{
				return value;
			}
			return null;
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00034524 File Offset: 0x00032724
		public virtual bool SetActive(int slot)
		{
			BaseCarriable ent = this.GetCarriableAtSlot(slot);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ActiveSlot = slot;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Entity.ActiveChild = ent;
			return true;
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00034558 File Offset: 0x00032758
		public bool AddToSlot(BaseCarriable carriable, int slot, bool makeActive)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertServer("AddToSlot");
			if (!carriable.IsValid())
			{
				return false;
			}
			if (carriable.Owner != null)
			{
				return false;
			}
			if (!carriable.CanCarry(base.Entity))
			{
				return false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			carriable.Parent = base.Entity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CarrySlots[slot] = carriable;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			carriable.OnCarryStart(base.Entity);
			if (makeActive || slot == this.ActiveSlot)
			{
				this.SetActive(slot);
			}
			return true;
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x000345E4 File Offset: 0x000327E4
		public BaseCarriable DropActive()
		{
			BaseCarriable active = this.GetActive();
			if (!active.IsValid())
			{
				return null;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnDroppedWeapon(active);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			active.Parent = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			active.OnCarryDrop(base.Entity);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			PhysicsGroup physicsGroup = active.PhysicsGroup;
			if (physicsGroup != null)
			{
				physicsGroup.ApplyImpulse(base.Entity.Velocity + base.Entity.EyeRotation.Forward * 500f + Vector3.Up * 100f, true);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			PhysicsGroup physicsGroup2 = active.PhysicsGroup;
			if (physicsGroup2 != null)
			{
				physicsGroup2.ApplyAngularImpulse(Vector3.Random * 100f, true);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CarrySlots[this.ActiveSlot] = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Entity.ActiveChild = null;
			return active;
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x000346D4 File Offset: 0x000328D4
		private void SwitchToSlot(int newSlot)
		{
			if (this.IsSwitchingSlot)
			{
				return;
			}
			if (newSlot == this.ActiveSlot)
			{
				return;
			}
			if (this.Active == null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SetActive(newSlot);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeUntilSwitchFinished = 0.5f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsSwitchingSlot = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.QueuedSlot = newSlot;
			StalkerWeaponBase weaponBase = this.Active as StalkerWeaponBase;
			if (weaponBase != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				weaponBase.Holster();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TimeUntilSwitchFinished = weaponBase.Resource.HolsterDelay;
			}
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0003476C File Offset: 0x0003296C
		public void Simulate(Client client)
		{
			if (Prediction.FirstTime)
			{
				if (this.IsSwitchingSlot)
				{
					if (this.TimeUntilSwitchFinished >= 0f)
					{
						return;
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.SetActive(this.QueuedSlot);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.IsSwitchingSlot = false;
				}
				if (Input.Pressed(InputButton.Slot1))
				{
					this.SwitchToSlot(0);
				}
				if (Input.Pressed(InputButton.Slot2))
				{
					this.SwitchToSlot(1);
				}
				if (Input.Pressed(InputButton.Slot3))
				{
					this.SwitchToSlot(2);
				}
				if (Input.Pressed(InputButton.Slot4))
				{
					this.SwitchToSlot(3);
				}
				if (Input.Pressed(InputButton.Slot5))
				{
					this.SwitchToSlot(4);
				}
				if (Input.Pressed(InputButton.Slot6))
				{
					this.SwitchToSlot(5);
				}
				if (Input.Pressed(InputButton.Slot7))
				{
					this.SwitchToSlot(6);
				}
			}
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0003484C File Offset: 0x00032A4C
		public bool TryGiveItem(string assetPath)
		{
			ItemResource resource = StalkerResource.Get<ItemResource>(assetPath);
			if (resource != null)
			{
				Item item = new Item(resource.ResourceName);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				item.InitializeFromAsset();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				item.Stacks = item.MaxStack;
				return base.Entity.InventoryComponent.TryPickupItem(item);
			}
			return false;
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0003489E File Offset: 0x00032A9E
		// (set) Token: 0x06000CDD RID: 3293 RVA: 0x000348AB File Offset: 0x00032AAB
		[Net]
		public HelmetSlot HelmetSlot
		{
			get
			{
				return this._repback__HelmetSlot.GetValue();
			}
			set
			{
				this._repback__HelmetSlot.SetValue(value);
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x000348B9 File Offset: 0x00032AB9
		// (set) Token: 0x06000CDF RID: 3295 RVA: 0x000348C6 File Offset: 0x00032AC6
		[Net]
		public HeadGearSlot HeadGearSlot
		{
			get
			{
				return this._repback__HeadGearSlot.GetValue();
			}
			set
			{
				this._repback__HeadGearSlot.SetValue(value);
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x000348D4 File Offset: 0x00032AD4
		// (set) Token: 0x06000CE1 RID: 3297 RVA: 0x000348E1 File Offset: 0x00032AE1
		[Net]
		public ArmorSlot ArmorSlot
		{
			get
			{
				return this._repback__ArmorSlot.GetValue();
			}
			set
			{
				this._repback__ArmorSlot.SetValue(value);
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x000348EF File Offset: 0x00032AEF
		// (set) Token: 0x06000CE3 RID: 3299 RVA: 0x000348FC File Offset: 0x00032AFC
		[Net]
		public BackpackSlot BackpackSlot
		{
			get
			{
				return this._repback__BackpackSlot.GetValue();
			}
			set
			{
				this._repback__BackpackSlot.SetValue(value);
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x0003490A File Offset: 0x00032B0A
		// (set) Token: 0x06000CE5 RID: 3301 RVA: 0x00034917 File Offset: 0x00032B17
		[Net]
		public WeaponSlot RifleSlot
		{
			get
			{
				return this._repback__RifleSlot.GetValue();
			}
			set
			{
				this._repback__RifleSlot.SetValue(value);
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x00034925 File Offset: 0x00032B25
		// (set) Token: 0x06000CE7 RID: 3303 RVA: 0x00034932 File Offset: 0x00032B32
		[Net]
		public WeaponSlot SlingSlot
		{
			get
			{
				return this._repback__SlingSlot.GetValue();
			}
			set
			{
				this._repback__SlingSlot.SetValue(value);
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x00034940 File Offset: 0x00032B40
		// (set) Token: 0x06000CE9 RID: 3305 RVA: 0x0003494D File Offset: 0x00032B4D
		[Net]
		public WeaponSlot PistolSlot
		{
			get
			{
				return this._repback__PistolSlot.GetValue();
			}
			set
			{
				this._repback__PistolSlot.SetValue(value);
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x0003495B File Offset: 0x00032B5B
		// (set) Token: 0x06000CEB RID: 3307 RVA: 0x00034968 File Offset: 0x00032B68
		[Net]
		public WeaponSlot MeleeSlot
		{
			get
			{
				return this._repback__MeleeSlot.GetValue();
			}
			set
			{
				this._repback__MeleeSlot.SetValue(value);
			}
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00034978 File Offset: 0x00032B78
		public void Initialise()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HelmetSlot.Player = base.Entity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HeadGearSlot.Player = base.Entity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ArmorSlot.Player = base.Entity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.BackpackSlot.Player = base.Entity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RifleSlot.Player = base.Entity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SlingSlot.Player = base.Entity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PistolSlot.Player = base.Entity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MeleeSlot.Player = base.Entity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.slots.Add(this.HelmetSlot);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.slots.Add(this.HeadGearSlot);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.slots.Add(this.ArmorSlot);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.slots.Add(this.BackpackSlot);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.slots.Add(this.RifleSlot);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.slots.Add(this.SlingSlot);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.slots.Add(this.PistolSlot);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.slots.Add(this.MeleeSlot);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AddDefaultItems();
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00034AF0 File Offset: 0x00032CF0
		public void Reset()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HelmetSlot.Reset();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HeadGearSlot.Reset();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ArmorSlot.Reset();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.BackpackSlot.Reset();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RifleSlot.Reset();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SlingSlot.Reset();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PistolSlot.Reset();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MeleeSlot.Reset();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AddDefaultItems();
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00034B88 File Offset: 0x00032D88
		private void AddDefaultItems()
		{
			EquipmentComponent.<AddDefaultItems>d__58 <AddDefaultItems>d__;
			<AddDefaultItems>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<AddDefaultItems>d__.<>4__this = this;
			<AddDefaultItems>d__.<>1__state = -1;
			<AddDefaultItems>d__.<>t__builder.Start<EquipmentComponent.<AddDefaultItems>d__58>(ref <AddDefaultItems>d__);
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00034BC0 File Offset: 0x00032DC0
		public GearSlot GetGearSlotFromSlotID(SlotID id)
		{
			GearSlot result;
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

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00034C44 File Offset: 0x00032E44
		public GearSlot GetFirstGearSlotForItem(Item item, bool canReplace = false)
		{
			GearSlot slot = null;
			foreach (GearSlot gearSlot in this.slots)
			{
				if ((!gearSlot.Occupied || canReplace) && gearSlot.EquipmentType == item.Resource.EquipmentType)
				{
					if (canReplace && slot == null)
					{
						slot = gearSlot;
					}
					else if (!canReplace || !gearSlot.Occupied)
					{
						return gearSlot;
					}
				}
			}
			return slot;
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00034CCC File Offset: 0x00032ECC
		private bool ItemFitsInGearSlot(Item item, GearSlot slot)
		{
			return slot.EquipmentType == item.Resource.EquipmentType;
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00034CE1 File Offset: 0x00032EE1
		public bool TryUnequipSlot(SlotID slotID)
		{
			return this.TryUnequipSlot(this.GetGearSlotFromSlotID(slotID));
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00034CF0 File Offset: 0x00032EF0
		public bool TryUnequipSlot(GearSlot slot)
		{
			if (slot == null)
			{
				return false;
			}
			if (!base.Entity.InventoryComponent.TryAddItem(slot.Item))
			{
				return false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			slot.UnequipItem();
			return true;
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00034D20 File Offset: 0x00032F20
		public bool TryDropEquipment(SlotID slotID)
		{
			GearSlot slot = this.GetGearSlotFromSlotID(slotID);
			if (slot == null)
			{
				return false;
			}
			if (!slot.Occupied)
			{
				return false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			slot.UnequipItem();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Entity.DropItem(slot.Item);
			return true;
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00034D68 File Offset: 0x00032F68
		public bool TryEquipItem(Item item)
		{
			GearSlot slot = this.GetFirstGearSlotForItem(item, false);
			if (slot == null)
			{
				return false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			slot.EquipItem(item);
			return true;
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00034D90 File Offset: 0x00032F90
		public bool TryEquipItem(Item item, InventoryEquipRequest equipRequest)
		{
			GearSlot slot = equipRequest.FirstValidSlot ? this.GetFirstGearSlotForItem(item, true) : this.GetGearSlotFromSlotID(equipRequest.SlotID);
			if (slot == null)
			{
				return false;
			}
			if (!this.ItemFitsInGearSlot(item, slot))
			{
				return false;
			}
			if (slot.Occupied)
			{
				Container container;
				if (base.Entity.InventoryComponent.TryFindContainerForItem(item, out container))
				{
					container.UpdateFreeSlots(item.Position, item.Size, true);
				}
				Item slotItem = slot.Item;
				if (!base.Entity.InventoryComponent.TryAddItem(slot.Item))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					if (container != null)
					{
						container.UpdateFreeSlots(item.Position, item.Size, false);
					}
					return false;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				slot.UnequipItem();
				Inventory inventory;
				if (slotItem.TryGetItemInventory(out inventory))
				{
					inventory.AddListener(base.Entity.Client);
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			slot.EquipItem(item);
			return true;
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00034E70 File Offset: 0x00033070
		public void OnDroppedWeapon(BaseCarriable stalkerWeaponBase)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RifleSlot.OnWeaponDropped(stalkerWeaponBase);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SlingSlot.OnWeaponDropped(stalkerWeaponBase);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PistolSlot.OnWeaponDropped(stalkerWeaponBase);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MeleeSlot.OnWeaponDropped(stalkerWeaponBase);
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00034EC4 File Offset: 0x000330C4
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarDictionaryUC<int, BaseCarriable>>(ref this._repback__CarrySlots, "CarrySlots", false, false);
			builder.Register<VarUnmanaged<int>>(ref this._repback__ActiveSlot, "ActiveSlot", true, false);
			builder.Register<VarUnmanaged<bool>>(ref this._repback__IsSwitchingSlot, "IsSwitchingSlot", true, false);
			builder.Register<VarUnmanaged<int>>(ref this._repback__QueuedSlot, "QueuedSlot", true, false);
			builder.Register<VarUnmanaged<TimeUntil>>(ref this._repback__TimeUntilSwitchFinished, "TimeUntilSwitchFinished", true, false);
			builder.Register<VarClass<HelmetSlot>>(ref this._repback__HelmetSlot, "HelmetSlot", false, false);
			builder.Register<VarClass<HeadGearSlot>>(ref this._repback__HeadGearSlot, "HeadGearSlot", false, false);
			builder.Register<VarClass<ArmorSlot>>(ref this._repback__ArmorSlot, "ArmorSlot", false, false);
			builder.Register<VarClass<BackpackSlot>>(ref this._repback__BackpackSlot, "BackpackSlot", false, false);
			builder.Register<VarClass<WeaponSlot>>(ref this._repback__RifleSlot, "RifleSlot", false, false);
			builder.Register<VarClass<WeaponSlot>>(ref this._repback__SlingSlot, "SlingSlot", false, false);
			builder.Register<VarClass<WeaponSlot>>(ref this._repback__PistolSlot, "PistolSlot", false, false);
			builder.Register<VarClass<WeaponSlot>>(ref this._repback__MeleeSlot, "MeleeSlot", false, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x04000414 RID: 1044
		public const int SLOT_RIFLE = 0;

		// Token: 0x04000415 RID: 1045
		public const int SLOT_SLING = 1;

		// Token: 0x04000416 RID: 1046
		public const int SLOT_PISTOL = 2;

		// Token: 0x04000417 RID: 1047
		public const int SLOT_MELEE = 3;

		// Token: 0x04000418 RID: 1048
		public const int SLOT_UTIL = 4;

		// Token: 0x04000419 RID: 1049
		public const int SLOT_BOLT = 5;

		// Token: 0x0400041A RID: 1050
		private readonly List<GearSlot> slots = new List<GearSlot>();

		// Token: 0x0400041B RID: 1051
		private VarDictionaryUC<int, BaseCarriable> _repback__CarrySlots = new VarDictionaryUC<int, BaseCarriable>(new Dictionary<int, BaseCarriable>());

		// Token: 0x0400041C RID: 1052
		private VarUnmanaged<int> _repback__ActiveSlot = new VarUnmanaged<int>();

		// Token: 0x0400041D RID: 1053
		private VarUnmanaged<bool> _repback__IsSwitchingSlot = new VarUnmanaged<bool>(false);

		// Token: 0x0400041E RID: 1054
		private VarUnmanaged<int> _repback__QueuedSlot = new VarUnmanaged<int>();

		// Token: 0x0400041F RID: 1055
		private VarUnmanaged<TimeUntil> _repback__TimeUntilSwitchFinished = new VarUnmanaged<TimeUntil>(0f);

		// Token: 0x04000420 RID: 1056
		private VarClass<HelmetSlot> _repback__HelmetSlot = new VarClass<HelmetSlot>(new HelmetSlot(SlotID.Helmet, EquipmentType.Helmet));

		// Token: 0x04000421 RID: 1057
		private VarClass<HeadGearSlot> _repback__HeadGearSlot = new VarClass<HeadGearSlot>(new HeadGearSlot(SlotID.HeadGear, EquipmentType.HeadGear));

		// Token: 0x04000422 RID: 1058
		private VarClass<ArmorSlot> _repback__ArmorSlot = new VarClass<ArmorSlot>(new ArmorSlot(SlotID.Armor, EquipmentType.Armor));

		// Token: 0x04000423 RID: 1059
		private VarClass<BackpackSlot> _repback__BackpackSlot = new VarClass<BackpackSlot>(new BackpackSlot(SlotID.Backpack, EquipmentType.Backpack));

		// Token: 0x04000424 RID: 1060
		private VarClass<WeaponSlot> _repback__RifleSlot = new VarClass<WeaponSlot>(new WeaponSlot(SlotID.Rifle, EquipmentType.Rifle, 0));

		// Token: 0x04000425 RID: 1061
		private VarClass<WeaponSlot> _repback__SlingSlot = new VarClass<WeaponSlot>(new WeaponSlot(SlotID.Sling, EquipmentType.Rifle, 1));

		// Token: 0x04000426 RID: 1062
		private VarClass<WeaponSlot> _repback__PistolSlot = new VarClass<WeaponSlot>(new WeaponSlot(SlotID.Pistol, EquipmentType.Pistol, 2));

		// Token: 0x04000427 RID: 1063
		private VarClass<WeaponSlot> _repback__MeleeSlot = new VarClass<WeaponSlot>(new WeaponSlot(SlotID.Melee, EquipmentType.Melee, 3));
	}
}
