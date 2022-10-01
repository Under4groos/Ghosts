using System;
using Sandbox;
using StalkerRP;
using StalkerRP.Inventory;
using StalkerRP.Inventory.UI;

// Token: 0x0200000F RID: 15
public static class GlobalRpcHandler
{
	// Token: 0x06000096 RID: 150 RVA: 0x00007700 File Offset: 0x00005900
	public static bool OnRpc(int id, NetRead read)
	{
		if (id <= -1056671446)
		{
			if (id <= -1960780537)
			{
				if (id == -2086731779)
				{
					int __netID = 0;
					__netID = read.ReadData<int>(__netID);
					if (!Prediction.WasPredicted("RemoveListenerFromInventory", new object[]
					{
						__netID
					}))
					{
						ClientInventoryManager.RemoveListenerFromInventory(__netID);
					}
					return true;
				}
				if (id == -1960780537)
				{
					bool __open = false;
					__open = read.ReadData<bool>(__open);
					if (!Prediction.WasPredicted("ForceSetOpen", new object[]
					{
						__open
					}))
					{
						StalkerMenu.ForceSetOpen(__open);
					}
					return true;
				}
			}
			else
			{
				if (id == -1550248496)
				{
					int __guid = 0;
					__guid = read.ReadData<int>(__guid);
					string __lbName = null;
					__lbName = read.ReadClass<string>(__lbName);
					int __score = 0;
					__score = read.ReadData<int>(__score);
					bool __alwaysReplace = false;
					__alwaysReplace = read.ReadData<bool>(__alwaysReplace);
					if (!Prediction.WasPredicted("ClientSubmit", new object[]
					{
						__guid,
						__lbName,
						__score,
						__alwaysReplace
					}))
					{
						LeaderboardExtensions.ClientSubmit(__guid, __lbName, __score, __alwaysReplace);
					}
					return true;
				}
				if (id == -1056671446)
				{
					int __netID2 = 0;
					__netID2 = read.ReadData<int>(__netID2);
					if (!Prediction.WasPredicted("RemoveOwnerFromInventory", new object[]
					{
						__netID2
					}))
					{
						ClientInventoryManager.RemoveOwnerFromInventory(__netID2);
					}
					return true;
				}
			}
		}
		else if (id <= 80785712)
		{
			if (id == -1003190969)
			{
				byte[] __data = null;
				__data = read.ReadUnmanagedArray<byte>(__data);
				if (!Prediction.WasPredicted("ReceiveInventoryData", new object[]
				{
					__data
				}))
				{
					ClientInventoryManager.ReceiveInventoryData(__data);
				}
				return true;
			}
			if (id == 80785712)
			{
				Entity __entity = null;
				__entity = read.ReadClass<Entity>(__entity);
				string __particle = null;
				__particle = read.ReadClass<string>(__particle);
				if (!Prediction.WasPredicted("ApplyEffect", new object[]
				{
					__entity,
					__particle
				}))
				{
					DamageEffectsManager.ApplyEffect(__entity, __particle);
				}
				return true;
			}
		}
		else
		{
			if (id == 282347913)
			{
				InventoryDropRequest __dropRequest = read.ReadData<InventoryDropRequest>(default(InventoryDropRequest));
				if (!Prediction.WasPredicted("RemoveItemFromInventory", new object[]
				{
					__dropRequest
				}))
				{
					ClientInventoryManager.RemoveItemFromInventory(__dropRequest);
				}
				return true;
			}
			if (id == 2026005911)
			{
				int __netID3 = 0;
				__netID3 = read.ReadData<int>(__netID3);
				int __containerIndex = 0;
				__containerIndex = read.ReadData<int>(__containerIndex);
				int __itemIndex = 0;
				__itemIndex = read.ReadData<int>(__itemIndex);
				int __newStacks = 0;
				__newStacks = read.ReadData<int>(__newStacks);
				if (!Prediction.WasPredicted("UpdateStackForItem", new object[]
				{
					__netID3,
					__containerIndex,
					__itemIndex,
					__newStacks
				}))
				{
					ClientInventoryManager.UpdateStackForItem(__netID3, __containerIndex, __itemIndex, __newStacks);
				}
				return true;
			}
		}
		return false;
	}
}
