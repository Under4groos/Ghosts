using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.Inventory
{
	// Token: 0x020000FC RID: 252
	public class InventoryComponent : EntityComponent<StalkerPlayer>
	{
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x0002E4DB File Offset: 0x0002C6DB
		// (set) Token: 0x06000B46 RID: 2886 RVA: 0x0002E4E3 File Offset: 0x0002C6E3
		[Description("This is a list of inventories this component holds. Only one component can be examined at a time.")]
		private List<Inventory> inventories { get; set; } = new List<Inventory>();

		// Token: 0x06000B47 RID: 2887 RVA: 0x0002E4EC File Offset: 0x0002C6EC
		public List<Item> GetAllItems()
		{
			List<Item> list = new List<Item>();
			foreach (Inventory inventory in this.inventories)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				list.AddRange(inventory.GetAllItems());
			}
			return list;
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0002E550 File Offset: 0x0002C750
		public void Reset()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ReleaseAllOwnerships();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.inventories = new List<Inventory>();
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0002E570 File Offset: 0x0002C770
		private void ReleaseAllOwnerships()
		{
			foreach (Inventory inventory in this.inventories)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				inventory.RemoveListener(base.Entity.Client);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				inventory.RemoveOwner();
			}
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0002E5DC File Offset: 0x0002C7DC
		public void AddInventory(Inventory inventory)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.inventories.Add(inventory);
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0002E5EF File Offset: 0x0002C7EF
		public void RemoveInventory(Inventory inventory)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.inventories.Remove(inventory);
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0002E604 File Offset: 0x0002C804
		public void TryStackItem(Item item)
		{
			foreach (Inventory inventory in this.inventories)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				inventory.TryStackItem(item);
				if (item.EmptyStack)
				{
					break;
				}
			}
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0002E664 File Offset: 0x0002C864
		public bool TryPickupItem(Item item)
		{
			if (base.Entity.EquipmentComponent.TryEquipItem(item))
			{
				return true;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TryStackItem(item);
			return item.EmptyStack || (!item.EmptyStack && this.TryAddItem(item));
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0002E6A4 File Offset: 0x0002C8A4
		public bool TryAddItem(Item item)
		{
			foreach (Inventory inventory in this.inventories)
			{
				if (!item.IsInventoryInsideItem(inventory) && inventory.TryAddItem(item))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					inventory.FullySyncWithListeners();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0002E714 File Offset: 0x0002C914
		public void FullSyncAllInventoriesWithOwner()
		{
			foreach (Inventory inventory in this.inventories)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				inventory.FullySyncWithOwner();
			}
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0002E76C File Offset: 0x0002C96C
		[Description("Returns the container an item is sitting in.")]
		public bool TryFindContainerForItem(Item item, out Container foundContainer)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			foundContainer = null;
			using (List<Inventory>.Enumerator enumerator = this.inventories.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Container container;
					if (enumerator.Current.TryFindContainerForItem(item, out container))
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						foundContainer = container;
						return true;
					}
				}
			}
			return false;
		}
	}
}
