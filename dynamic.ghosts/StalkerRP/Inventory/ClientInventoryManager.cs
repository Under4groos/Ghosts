using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;
using StalkerRP.Inventory.UI;

namespace StalkerRP.Inventory
{
	// Token: 0x020000FA RID: 250
	public static class ClientInventoryManager
	{
		// Token: 0x06000B2B RID: 2859 RVA: 0x0002DD70 File Offset: 0x0002BF70
		[ClientRpc]
		public static void ReceiveInventoryData(byte[] data)
		{
			if (!ClientInventoryManager.ReceiveInventoryData__RpcProxy(data, null))
			{
				return;
			}
			Inventory inventory = Inventory.Deserialize(data);
			if (inventory.Owner == Local.Client)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				ClientInventoryManager.LocalInventoriesCache[inventory.InventoryNetID] = inventory;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				StalkerGearTab.Instance.CreatePlayerInventory();
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ClientInventoryManager.InventoryCache[inventory.InventoryNetID] = inventory;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StalkerGearTab.Instance.CreateLootInventory();
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0002DDF0 File Offset: 0x0002BFF0
		[ClientRpc]
		[Description("This is sent from the server to the client as a delta change to the inventory. Meaning, the 'drop request' has already been validated and performed on the server.")]
		public static void RemoveItemFromInventory(InventoryDropRequest dropRequest)
		{
			if (!ClientInventoryManager.RemoveItemFromInventory__RpcProxy(dropRequest, null))
			{
				return;
			}
			Inventory localInventory;
			Inventory inventory;
			if (ClientInventoryManager.LocalInventoriesCache.TryGetValue(dropRequest.OriginInventoryNetID, out localInventory))
			{
				if (localInventory.TryRemoveItemByIndex(dropRequest.ContainerIndex, dropRequest.ItemIndex))
				{
					StalkerGearTab.Instance.CreatePlayerInventory();
					return;
				}
			}
			else if (ClientInventoryManager.InventoryCache.TryGetValue(dropRequest.OriginInventoryNetID, out inventory) && inventory.TryRemoveItemByIndex(dropRequest.ContainerIndex, dropRequest.ItemIndex))
			{
				StalkerGearTab.Instance.CreateLootInventory();
			}
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0002DE74 File Offset: 0x0002C074
		[ClientRpc]
		public static void UpdateStackForItem(int netID, int containerIndex, int itemIndex, int newStacks)
		{
			if (!ClientInventoryManager.UpdateStackForItem__RpcProxy(netID, containerIndex, itemIndex, newStacks, null))
			{
				return;
			}
			Inventory localInventory;
			if (!ClientInventoryManager.LocalInventoriesCache.TryGetValue(netID, out localInventory))
			{
				Inventory inventory;
				if (ClientInventoryManager.InventoryCache.TryGetValue(netID, out inventory))
				{
					Container container;
					if (!inventory.TryGetContainerByIndex(containerIndex, out container))
					{
						return;
					}
					Item item;
					if (!container.TryGetItemByIndex(itemIndex, out item))
					{
						return;
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					item.Stacks = newStacks;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					InventoryItemPanel.RefreshPanelForItem(item);
				}
				return;
			}
			Container container2;
			if (!localInventory.TryGetContainerByIndex(containerIndex, out container2))
			{
				return;
			}
			Item item2;
			if (!container2.TryGetItemByIndex(itemIndex, out item2))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			item2.Stacks = newStacks;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			InventoryItemPanel.RefreshPanelForItem(item2);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0002DF18 File Offset: 0x0002C118
		[ClientRpc]
		public static void RemoveOwnerFromInventory(int netID)
		{
			if (!ClientInventoryManager.RemoveOwnerFromInventory__RpcProxy(netID, null))
			{
				return;
			}
			if (ClientInventoryManager.LocalInventoriesCache.Remove(netID))
			{
				StalkerGearTab.Instance.CreatePlayerInventory();
			}
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0002DF50 File Offset: 0x0002C150
		[ClientRpc]
		public static void RemoveListenerFromInventory(int netID)
		{
			if (!ClientInventoryManager.RemoveListenerFromInventory__RpcProxy(netID, null))
			{
				return;
			}
			if (ClientInventoryManager.InventoryCache.Remove(netID))
			{
				StalkerGearTab.Instance.CreateLootInventory();
			}
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0002DF88 File Offset: 0x0002C188
		private static bool ReceiveInventoryData__RpcProxy(byte[] data, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("ReceiveInventoryData", new object[]
				{
					data
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-1003190969, null))
			{
				if (!NetRead.IsSupported(data))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] ReceiveInventoryData is not allowed to use  for the parameter 'data'!");
					return false;
				}
				writer.WriteUnmanagedArray<byte>(data);
				writer.SendRpc(toTarget, null);
			}
			return false;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0002E010 File Offset: 0x0002C210
		private static bool RemoveItemFromInventory__RpcProxy(InventoryDropRequest dropRequest, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("RemoveItemFromInventory", new object[]
				{
					dropRequest
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(282347913, null))
			{
				if (!NetRead.IsSupported(dropRequest))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] RemoveItemFromInventory is not allowed to use InventoryDropRequest for the parameter 'dropRequest'!");
					return false;
				}
				writer.Write<InventoryDropRequest>(dropRequest);
				writer.SendRpc(toTarget, null);
			}
			return false;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0002E0A4 File Offset: 0x0002C2A4
		private static bool UpdateStackForItem__RpcProxy(int netID, int containerIndex, int itemIndex, int newStacks, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("UpdateStackForItem", new object[]
				{
					netID,
					containerIndex,
					itemIndex,
					newStacks
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(2026005911, null))
			{
				if (!NetRead.IsSupported(netID))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] UpdateStackForItem is not allowed to use Int32 for the parameter 'netID'!");
					return false;
				}
				writer.Write<int>(netID);
				if (!NetRead.IsSupported(containerIndex))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] UpdateStackForItem is not allowed to use Int32 for the parameter 'containerIndex'!");
					return false;
				}
				writer.Write<int>(containerIndex);
				if (!NetRead.IsSupported(itemIndex))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] UpdateStackForItem is not allowed to use Int32 for the parameter 'itemIndex'!");
					return false;
				}
				writer.Write<int>(itemIndex);
				if (!NetRead.IsSupported(newStacks))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] UpdateStackForItem is not allowed to use Int32 for the parameter 'newStacks'!");
					return false;
				}
				writer.Write<int>(newStacks);
				writer.SendRpc(toTarget, null);
			}
			return false;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0002E1CC File Offset: 0x0002C3CC
		private static bool RemoveOwnerFromInventory__RpcProxy(int netID, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("RemoveOwnerFromInventory", new object[]
				{
					netID
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-1056671446, null))
			{
				if (!NetRead.IsSupported(netID))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] RemoveOwnerFromInventory is not allowed to use Int32 for the parameter 'netID'!");
					return false;
				}
				writer.Write<int>(netID);
				writer.SendRpc(toTarget, null);
			}
			return false;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0002E260 File Offset: 0x0002C460
		private static bool RemoveListenerFromInventory__RpcProxy(int netID, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("RemoveListenerFromInventory", new object[]
				{
					netID
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-2086731779, null))
			{
				if (!NetRead.IsSupported(netID))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] RemoveListenerFromInventory is not allowed to use Int32 for the parameter 'netID'!");
					return false;
				}
				writer.Write<int>(netID);
				writer.SendRpc(toTarget, null);
			}
			return false;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0002E2F4 File Offset: 0x0002C4F4
		public static void ReceiveInventoryData(To toTarget, byte[] data)
		{
			ClientInventoryManager.ReceiveInventoryData__RpcProxy(data, new To?(toTarget));
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0002E303 File Offset: 0x0002C503
		public static void RemoveItemFromInventory(To toTarget, InventoryDropRequest dropRequest)
		{
			ClientInventoryManager.RemoveItemFromInventory__RpcProxy(dropRequest, new To?(toTarget));
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0002E312 File Offset: 0x0002C512
		public static void UpdateStackForItem(To toTarget, int netID, int containerIndex, int itemIndex, int newStacks)
		{
			ClientInventoryManager.UpdateStackForItem__RpcProxy(netID, containerIndex, itemIndex, newStacks, new To?(toTarget));
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0002E325 File Offset: 0x0002C525
		public static void RemoveOwnerFromInventory(To toTarget, int netID)
		{
			ClientInventoryManager.RemoveOwnerFromInventory__RpcProxy(netID, new To?(toTarget));
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0002E334 File Offset: 0x0002C534
		public static void RemoveListenerFromInventory(To toTarget, int netID)
		{
			ClientInventoryManager.RemoveListenerFromInventory__RpcProxy(netID, new To?(toTarget));
		}

		// Token: 0x0400039A RID: 922
		public static readonly Dictionary<int, Inventory> InventoryCache = new Dictionary<int, Inventory>();

		// Token: 0x0400039B RID: 923
		public static readonly Dictionary<int, Inventory> LocalInventoriesCache = new Dictionary<int, Inventory>();
	}
}
