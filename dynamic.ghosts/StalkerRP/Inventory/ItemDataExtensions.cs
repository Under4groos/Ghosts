using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.Inventory
{
	// Token: 0x0200010B RID: 267
	public static class ItemDataExtensions
	{
		// Token: 0x06000C07 RID: 3079 RVA: 0x00030B47 File Offset: 0x0002ED47
		public static void SetItemInventory(this Item item, int netID)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			item.Data["ITEM_INVENTORY"] = netID.ToString();
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00030B68 File Offset: 0x0002ED68
		public static Inventory GetItemInventory(this Item item)
		{
			string netID;
			if (!item.Data.TryGetValue("ITEM_INVENTORY", out netID))
			{
				return null;
			}
			Inventory inventory;
			if (!ServerInventoryManager.TryGetInventoryByNetID(netID.ToInt(0), out inventory))
			{
				return null;
			}
			return inventory;
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00030B9E File Offset: 0x0002ED9E
		public static bool TryGetItemInventory(this Item item, out Inventory inventory)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			inventory = item.GetItemInventory();
			return inventory != null;
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00030BB4 File Offset: 0x0002EDB4
		[Description("Initialise the storage of an item. This should only be done serverside, and only done once.")]
		public static Inventory SetupItemInventory(this Item item)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Assert.True(Game.Current.IsServer);
			Inventory inv = ((StorageItemResource)item.Resource).GenerateInventory();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ServerInventoryManager.GenerateNetIDForInventory(inv);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			item.SetItemInventory(inv.InventoryNetID);
			return inv;
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00030C03 File Offset: 0x0002EE03
		public static Inventory GetOrCreateInventory(this Item item)
		{
			return item.GetItemInventory() ?? item.SetupItemInventory();
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00030C18 File Offset: 0x0002EE18
		private static bool IsInventoryInsideInventory(Inventory parent, Inventory child)
		{
			using (List<Item>.Enumerator enumerator = child.GetAllItems().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Inventory itemInventory;
					if (enumerator.Current.TryGetItemInventory(out itemInventory) && ItemDataExtensions.IsInventoryInsideInventory(parent, itemInventory))
					{
						return true;
					}
				}
			}
			return parent == child;
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00030C80 File Offset: 0x0002EE80
		public static bool IsInventoryInsideItem(this Item item, Inventory inventory)
		{
			Inventory itemInventory;
			return item.TryGetItemInventory(out itemInventory) && ItemDataExtensions.IsInventoryInsideInventory(inventory, itemInventory);
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00030CA0 File Offset: 0x0002EEA0
		public static bool HasItemInventory(this Item item)
		{
			return item.Data.ContainsKey("ITEM_INVENTORY");
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00030CB2 File Offset: 0x0002EEB2
		public static int GetItemInventoryNetID(this Item item)
		{
			return item.Data["ITEM_INVENTORY"].ToInt(0);
		}

		// Token: 0x040003C2 RID: 962
		public const string StorageInventoryKey = "ITEM_INVENTORY";
	}
}
