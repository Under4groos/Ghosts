using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;
using StalkerRP.Inventory.Gear;

namespace StalkerRP.Inventory
{
	// Token: 0x02000114 RID: 276
	public static class ServerInventoryManager
	{
		// Token: 0x06000C2B RID: 3115 RVA: 0x00031978 File Offset: 0x0002FB78
		[ConCmd.ServerAttribute(null)]
		public static void RequestSplitItemStack(string mergeRequestSerialized)
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("RequestSplitItemStack", new object[]
				{
					mergeRequestSerialized
				});
				return;
			}
			InventorySplitStackRequest? splitRequest = InventorySplitStackRequest.Parse(mergeRequestSerialized);
			if (splitRequest == null)
			{
				return;
			}
			Client target = ConsoleSystem.Caller;
			if (target == null)
			{
				return;
			}
			StalkerPlayer player = target.Pawn as StalkerPlayer;
			if (player == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ServerInventoryManager.TryInventorySplitItemStack(player, splitRequest.Value);
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x000319E0 File Offset: 0x0002FBE0
		[ConCmd.ServerAttribute(null)]
		public static void RequestMergeItemStacks(string mergeRequestSerialized)
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("RequestMergeItemStacks", new object[]
				{
					mergeRequestSerialized
				});
				return;
			}
			InventoryMergeStacksRequest? mergeRequest = InventoryMergeStacksRequest.Parse(mergeRequestSerialized);
			if (mergeRequest == null)
			{
				return;
			}
			Client target = ConsoleSystem.Caller;
			if (target == null)
			{
				return;
			}
			StalkerPlayer player = target.Pawn as StalkerPlayer;
			if (player == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ServerInventoryManager.TryInventoryMergeItemStacks(player, mergeRequest.Value);
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x00031A48 File Offset: 0x0002FC48
		[ConCmd.ServerAttribute(null)]
		public static void RequestStoreItem(string storeRequestSerialized)
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("RequestStoreItem", new object[]
				{
					storeRequestSerialized
				});
				return;
			}
			InventoryStoreRequest? storeRequest = InventoryStoreRequest.Parse(storeRequestSerialized);
			if (storeRequest == null)
			{
				return;
			}
			Client target = ConsoleSystem.Caller;
			if (target == null)
			{
				return;
			}
			StalkerPlayer player = target.Pawn as StalkerPlayer;
			if (player == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ServerInventoryManager.TryInventoryStore(player, storeRequest.Value);
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00031AB0 File Offset: 0x0002FCB0
		[ConCmd.ServerAttribute(null)]
		public static void RequestStoreItemFromGearSlot(string storeRequestSerialized)
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("RequestStoreItemFromGearSlot", new object[]
				{
					storeRequestSerialized
				});
				return;
			}
			InventoryStoreFromGearSlotRequest? storeRequest = InventoryStoreFromGearSlotRequest.Parse(storeRequestSerialized);
			if (storeRequest == null)
			{
				return;
			}
			Client target = ConsoleSystem.Caller;
			if (target == null)
			{
				return;
			}
			StalkerPlayer player = target.Pawn as StalkerPlayer;
			if (player == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ServerInventoryManager.TryInventoryStoreFromGearSlot(player, storeRequest.Value);
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x00031B18 File Offset: 0x0002FD18
		[ConCmd.ServerAttribute(null)]
		public static void RequestMoveItem(string moveRequestSerialized)
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("RequestMoveItem", new object[]
				{
					moveRequestSerialized
				});
				return;
			}
			InventoryMoveRequest? moveRequest = InventoryMoveRequest.Parse(moveRequestSerialized);
			if (moveRequest == null)
			{
				return;
			}
			Client target = ConsoleSystem.Caller;
			if (target == null)
			{
				return;
			}
			StalkerPlayer player = target.Pawn as StalkerPlayer;
			if (player == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ServerInventoryManager.TryInventoryMove(player, moveRequest.Value);
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00031B80 File Offset: 0x0002FD80
		[ConCmd.ServerAttribute(null)]
		public static void RequestDropItem(string dropRequestSerialized)
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("RequestDropItem", new object[]
				{
					dropRequestSerialized
				});
				return;
			}
			InventoryDropRequest? dropRequest = InventoryDropRequest.Parse(dropRequestSerialized);
			if (dropRequest == null)
			{
				return;
			}
			Client target = ConsoleSystem.Caller;
			if (target == null)
			{
				return;
			}
			StalkerPlayer player = target.Pawn as StalkerPlayer;
			if (player == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ServerInventoryManager.TryInventoryDrop(player, dropRequest.Value);
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00031BE8 File Offset: 0x0002FDE8
		[ConCmd.ServerAttribute(null)]
		public static void RequestEquipItem(string equipRequestSerialized)
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("RequestEquipItem", new object[]
				{
					equipRequestSerialized
				});
				return;
			}
			InventoryEquipRequest? equipRequest = InventoryEquipRequest.Parse(equipRequestSerialized);
			if (equipRequest == null)
			{
				return;
			}
			Client target = ConsoleSystem.Caller;
			if (target == null)
			{
				return;
			}
			StalkerPlayer player = target.Pawn as StalkerPlayer;
			if (player == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ServerInventoryManager.TryInventoryEquip(player, equipRequest.Value);
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00031C50 File Offset: 0x0002FE50
		[ConCmd.ServerAttribute(null)]
		public static void RequestUnequipItem(string unequipSerialized)
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("RequestUnequipItem", new object[]
				{
					unequipSerialized
				});
				return;
			}
			InventoryUnequipRequest? unequipRequest = InventoryUnequipRequest.Parse(unequipSerialized);
			if (unequipRequest == null)
			{
				return;
			}
			Client target = ConsoleSystem.Caller;
			if (target == null)
			{
				return;
			}
			StalkerPlayer player = target.Pawn as StalkerPlayer;
			if (player == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ServerInventoryManager.TryInventoryUnequip(player, unequipRequest.Value);
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00031CB8 File Offset: 0x0002FEB8
		[Description("Generate a netID for this inventory and cache a reference. This should only ever be called serverside.")]
		public static void GenerateNetIDForInventory(Inventory inventory)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Assert.True(Game.Current.IsServer);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			inventory.InventoryNetID = ServerInventoryManager.InventoryNetIDIteration;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ServerInventoryManager.InventoryNetIDIteration++;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ServerInventoryManager.inventories.Add(inventory.InventoryNetID, inventory);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00031D10 File Offset: 0x0002FF10
		[Description("Returns whether or not a player can interact with an inventory. My theory being that anyone who can interact with an inventory needs to be a listener and be alive.")]
		private static bool CanPlayerInteractWithInventory(StalkerPlayer player, Inventory inventory)
		{
			return player.LifeState == LifeState.Alive && inventory.HasListener(player.Client);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00031D28 File Offset: 0x0002FF28
		public static bool TryGetInventoryByNetID(int netID, out Inventory inventory)
		{
			return ServerInventoryManager.inventories.TryGetValue(netID, out inventory);
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00031D38 File Offset: 0x0002FF38
		private static void TryInventoryEquip(StalkerPlayer player, InventoryEquipRequest equipRequest)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("-----------------TRYING EQUIP!---------------------");
			Inventory originInventory;
			if (!ServerInventoryManager.TryGetInventoryByNetID(equipRequest.OriginInventoryNetID, out originInventory))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Origin Inventory Validated");
			if (!ServerInventoryManager.CanPlayerInteractWithInventory(player, originInventory))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Valid Interaction with Origin Inventory");
			Container originContainer;
			if (!originInventory.TryGetContainerByIndex(equipRequest.ContainerIndex, out originContainer))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Valid Container in Origin Inventory");
			Item item;
			if (!originContainer.TryGetItemByIndex(equipRequest.ItemIndex, out item))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Valid Item in Origin Inventory");
			if (player.EquipmentComponent.TryEquipItem(item, equipRequest))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				originContainer.RemoveItem(item);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				originContainer.RecalculateFreeSlots();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				ClientInventoryManager.RemoveItemFromInventory(To.Single(player), new InventoryDropRequest(equipRequest.OriginInventoryNetID, equipRequest.ContainerIndex, equipRequest.ItemIndex, 1, SlotID.None));
			}
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00031E38 File Offset: 0x00030038
		private static void TryInventoryUnequip(StalkerPlayer player, InventoryUnequipRequest unequipRequest)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info(unequipRequest.FirstFreeSlot);
			if (unequipRequest.FirstFreeSlot)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				player.EquipmentComponent.TryUnequipSlot(unequipRequest.SlotID);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("-----------------TRYING UNEQUIP!---------------------");
			Inventory targetInventory;
			if (!ServerInventoryManager.TryGetInventoryByNetID(unequipRequest.TargetInventoryNetID, out targetInventory))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Origin Inventory Validated");
			if (!ServerInventoryManager.CanPlayerInteractWithInventory(player, targetInventory))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Valid Interaction with Target Inventory");
			Container targetContainer;
			if (!targetInventory.TryGetContainerByIndex(unequipRequest.TargetContainerIndex, out targetContainer))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Valid Container in Target Inventory");
			GearSlot slot = player.EquipmentComponent.GetGearSlotFromSlotID(unequipRequest.SlotID);
			if (!slot.Occupied)
			{
				return;
			}
			Item item = slot.Item;
			if (item.IsInventoryInsideItem(targetInventory))
			{
				return;
			}
			GridPosition targPos = new GridPosition(unequipRequest.X, unequipRequest.Y);
			GridSize size = new GridSize(item.Size);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			size.IsRotated = unequipRequest.Rotated;
			if (targetInventory.TryAddItem(item, targetContainer, targPos, size))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				targetInventory.FullySyncWithListeners();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				slot.UnequipItem();
			}
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x00031F7C File Offset: 0x0003017C
		private static void TryInventoryDrop(StalkerPlayer player, InventoryDropRequest dropRequest)
		{
			if (dropRequest.SlotID != SlotID.None)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				player.EquipmentComponent.TryDropEquipment(dropRequest.SlotID);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("-----------------TRYING DROP!---------------------");
			Inventory originInventory;
			if (!ServerInventoryManager.TryGetInventoryByNetID(dropRequest.OriginInventoryNetID, out originInventory))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Origin Inventory Validated");
			if (!ServerInventoryManager.CanPlayerInteractWithInventory(player, originInventory))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Valid Interaction with Origin Inventory");
			Container originContainer;
			if (!originInventory.TryGetContainerByIndex(dropRequest.ContainerIndex, out originContainer))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Valid Container in Origin Inventory");
			Item item;
			if (originContainer.TryRemoveItemByIndex(dropRequest.ItemIndex, out item))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				player.DropItem(item);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				ClientInventoryManager.RemoveItemFromInventory(To.Single(player), dropRequest);
			}
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x00032054 File Offset: 0x00030254
		private static void TryInventorySplitItemStack(StalkerPlayer player, InventorySplitStackRequest splitRequest)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("-----------------TRYING SPLIT!---------------------");
			Inventory originInventory;
			if (!ServerInventoryManager.TryGetInventoryByNetID(splitRequest.OriginInventoryNetID, out originInventory))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Origin Inventory Validated");
			if (!ServerInventoryManager.CanPlayerInteractWithInventory(player, originInventory))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Valid Interaction with Origin Inventory");
			Container originContainer;
			if (!originInventory.TryGetContainerByIndex(splitRequest.ContainerIndex, out originContainer))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Valid Container in Origin Inventory");
			Item item;
			if (!originContainer.TryGetItemByIndex(splitRequest.ItemIndex, out item))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Valid Item in Origin Inventory");
			if (!item.CanStack || item.Stacks <= 1)
			{
				return;
			}
			Item copyItem = new Item(item);
			int oldStacks = item.Stacks;
			int stackSize = item.Stacks / 2;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			copyItem.Stacks = stackSize;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			item.Stacks -= stackSize;
			if (!player.InventoryComponent.TryAddItem(copyItem))
			{
				item.Stacks = oldStacks;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ClientInventoryManager.UpdateStackForItem(To.Multiple(originInventory.GetListeners()), splitRequest.OriginInventoryNetID, splitRequest.ContainerIndex, splitRequest.ItemIndex, item.Stacks);
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00032190 File Offset: 0x00030390
		private static void TryInventoryMergeItemStacks(StalkerPlayer player, InventoryMergeStacksRequest mergeRequest)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("-----------------TRYING MERGE!---------------------");
			Inventory originInventory;
			if (!ServerInventoryManager.TryGetInventoryByNetID(mergeRequest.OriginInventoryNetID, out originInventory))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Origin Inventory Validated");
			Inventory targetInventory;
			if (!ServerInventoryManager.TryGetInventoryByNetID(mergeRequest.TargetInventoryNetID, out targetInventory))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Target Inventory Validated");
			if (!ServerInventoryManager.CanPlayerInteractWithInventory(player, originInventory))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Valid Interaction with Origin Inventory");
			if (!ServerInventoryManager.CanPlayerInteractWithInventory(player, targetInventory))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Valid Interaction with Target Inventory");
			Container originContainer;
			if (!originInventory.TryGetContainerByIndex(mergeRequest.OriginContainerIndex, out originContainer))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Valid Container in Origin Inventory");
			Container targetContainer;
			if (!targetInventory.TryGetContainerByIndex(mergeRequest.TargetContainerIndex, out targetContainer))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Valid Container in Target Inventory");
			Item originItem;
			if (!originContainer.TryGetItemByIndex(mergeRequest.OriginItemIndex, out originItem))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Valid Item in Origin Inventory");
			Item targetItem;
			if (!targetContainer.TryGetItemByIndex(mergeRequest.TargetItemIndex, out targetItem))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Valid Item in Origin Inventory");
			if (!originItem.CanStackOnItem(targetItem))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Items can stack!");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			originItem.AddItemToStack(targetItem);
			if (originItem.EmptyStack)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				originInventory.TryRemoveItemByIndex(mergeRequest.OriginContainerIndex, mergeRequest.OriginItemIndex);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				originInventory.FullySyncWithListeners();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				ClientInventoryManager.UpdateStackForItem(To.Multiple(targetInventory.GetListeners()), mergeRequest.TargetInventoryNetID, mergeRequest.TargetContainerIndex, mergeRequest.TargetItemIndex, targetItem.Stacks);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ClientInventoryManager.UpdateStackForItem(To.Multiple(originInventory.GetListeners()), mergeRequest.OriginInventoryNetID, mergeRequest.OriginContainerIndex, mergeRequest.OriginItemIndex, originItem.Stacks);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ClientInventoryManager.UpdateStackForItem(To.Multiple(targetInventory.GetListeners()), mergeRequest.TargetInventoryNetID, mergeRequest.TargetContainerIndex, mergeRequest.TargetItemIndex, targetItem.Stacks);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x000323B0 File Offset: 0x000305B0
		private static void TryInventoryStore(StalkerPlayer player, InventoryStoreRequest storeRequest)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("-----------------TRYING STORE!---------------------");
			Inventory originInventory;
			if (!ServerInventoryManager.TryGetInventoryByNetID(storeRequest.OriginInventoryNetID, out originInventory))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Origin Inventory Validated");
			Inventory targetInventory;
			if (!ServerInventoryManager.TryGetInventoryByNetID(storeRequest.TargetInventoryNetID, out targetInventory))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Target Inventory Validated");
			if (!ServerInventoryManager.CanPlayerInteractWithInventory(player, originInventory))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Valid Interaction with Origin Inventory");
			if (!ServerInventoryManager.CanPlayerInteractWithInventory(player, targetInventory))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Valid Interaction with Target Inventory");
			Container originContainer;
			if (!originInventory.TryGetContainerByIndex(storeRequest.ContainerIndex, out originContainer))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Valid Container in Origin Inventory");
			Item item;
			if (!originContainer.TryGetItemByIndex(storeRequest.ItemIndex, out item))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Valid Item in Origin Inventory");
			if (item.IsInventoryInsideItem(targetInventory))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Is not our own inventory.");
			GridSize oldSize = new GridSize(item.Size);
			GridPosition oldPos = new GridPosition(item.Position);
			if (targetInventory.TryAddItem(item))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Log.Info("Added item!");
				GridSize newSize = new GridSize(item.Size);
				GridPosition newPos = new GridPosition(item.Position);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				item.Size = oldSize;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				item.Position = oldPos;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				originContainer.RemoveItem(item);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				item.Position = newPos;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				item.Size = newSize;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				originInventory.FullySyncWithListeners();
			}
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0003255C File Offset: 0x0003075C
		private static void TryInventoryStoreFromGearSlot(StalkerPlayer player, InventoryStoreFromGearSlotRequest storeRequest)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("-----------------TRYING STORE!---------------------");
			Inventory targetInventory;
			if (!ServerInventoryManager.TryGetInventoryByNetID(storeRequest.TargetInventoryNetID, out targetInventory))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Target Inventory Validated");
			if (!ServerInventoryManager.CanPlayerInteractWithInventory(player, targetInventory))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Valid Interaction with Target Inventory");
			GearSlot slot = player.EquipmentComponent.GetGearSlotFromSlotID(storeRequest.SlotID);
			if (!slot.Occupied)
			{
				return;
			}
			Item item = slot.Item;
			if (item.IsInventoryInsideItem(targetInventory))
			{
				return;
			}
			if (targetInventory.TryAddItem(item))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				slot.UnequipItem();
			}
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00032600 File Offset: 0x00030800
		public static void TryInventoryMove(StalkerPlayer player, InventoryMoveRequest moveRequest)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("-----------------TRYING MOVE!---------------------");
			Inventory originInventory;
			if (!ServerInventoryManager.TryGetInventoryByNetID(moveRequest.OriginInventoryNetID, out originInventory))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Origin Inventory Validated");
			Inventory targetInventory;
			if (!ServerInventoryManager.TryGetInventoryByNetID(moveRequest.TargetInventoryNetID, out targetInventory))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Target Inventory Validated");
			if (!ServerInventoryManager.CanPlayerInteractWithInventory(player, originInventory))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Valid Interaction with Origin Inventory");
			if (!ServerInventoryManager.CanPlayerInteractWithInventory(player, targetInventory))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Valid Interaction with Target Inventory");
			Container originContainer;
			if (!originInventory.TryGetContainerByIndex(moveRequest.ContainerIndex, out originContainer))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Valid Container in Origin Inventory");
			Item item;
			if (!originContainer.TryGetItemByIndex(moveRequest.ItemIndex, out item))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Valid Item in Origin Inventory");
			if (item.IsInventoryInsideItem(targetInventory))
			{
				return;
			}
			Container targetContainer;
			if (!targetInventory.TryGetContainerByIndex(moveRequest.TargetContainerIndex, out targetContainer))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("Valid Container in Target Inventory");
			GridPosition targPos = new GridPosition(moveRequest.X, moveRequest.Y);
			GridSize size = new GridSize(item.Size.Width, item.Size.Height, moveRequest.Rotated);
			if (targetContainer == originContainer)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				targetInventory.TryMoveItem(item, targetContainer, targPos, size);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				targetInventory.FullySyncWithListeners();
				return;
			}
			GridPosition oldPos = new GridPosition(item.Position);
			GridSize oldSize = new GridSize(item.Size);
			if (targetInventory.TryAddItem(item, targetContainer, targPos, size))
			{
				GridPosition newPos = new GridPosition(item.Position);
				GridSize newSize = new GridSize(item.Size);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				item.Position = oldPos;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				item.Size = oldSize;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				originInventory.RemoveItem(item, originContainer);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				item.Position = newPos;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				item.Size = newSize;
				if (targetInventory != originInventory)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					targetInventory.FullySyncWithListeners();
					RuntimeHelpers.EnsureSufficientExecutionStack();
					originInventory.FullySyncWithListeners();
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				targetInventory.FullySyncWithListeners();
			}
		}

		// Token: 0x040003EB RID: 1003
		private static readonly Dictionary<int, Inventory> inventories = new Dictionary<int, Inventory>();

		// Token: 0x040003EC RID: 1004
		private static int InventoryNetIDIteration = 0;
	}
}
