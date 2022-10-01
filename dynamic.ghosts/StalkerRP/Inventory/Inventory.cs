using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.Inventory
{
	// Token: 0x02000109 RID: 265
	public class Inventory
	{
		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x0002FFBD File Offset: 0x0002E1BD
		// (set) Token: 0x06000BCA RID: 3018 RVA: 0x0002FFC5 File Offset: 0x0002E1C5
		public int InventoryNetID { get; set; }

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x0002FFCE File Offset: 0x0002E1CE
		// (set) Token: 0x06000BCC RID: 3020 RVA: 0x0002FFD6 File Offset: 0x0002E1D6
		[Description("The player who owns this inventory. Usually for inventories the player has equipped.")]
		public Client Owner { get; set; }

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x0002FFDF File Offset: 0x0002E1DF
		// (set) Token: 0x06000BCE RID: 3022 RVA: 0x0002FFE7 File Offset: 0x0002E1E7
		[DefaultValue("Default")]
		public string Title { get; set; } = "Default";

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x0002FFF0 File Offset: 0x0002E1F0
		// (set) Token: 0x06000BD0 RID: 3024 RVA: 0x0002FFF8 File Offset: 0x0002E1F8
		[Description("Internal list of containers inside the inventory.")]
		private List<Container> Containers { get; set; } = new List<Container>();

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x00030001 File Offset: 0x0002E201
		// (set) Token: 0x06000BD2 RID: 3026 RVA: 0x00030009 File Offset: 0x0002E209
		private HashSet<Client> listeners { get; set; } = new HashSet<Client>();

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00030014 File Offset: 0x0002E214
		public List<Item> GetAllItems()
		{
			List<Item> list = new List<Item>();
			foreach (Container container in this.Containers)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				list.AddRange(container.GetItems());
			}
			return list;
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00030078 File Offset: 0x0002E278
		public HashSet<Client> GetListeners()
		{
			return this.listeners;
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00030080 File Offset: 0x0002E280
		public void AddListener(Client client)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.listeners.Add(client);
			using (List<Item>.Enumerator enumerator = this.GetAllItems().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Inventory inventory;
					if (enumerator.Current.TryGetItemInventory(out inventory))
					{
						inventory.AddListener(client);
					}
				}
			}
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x000300F0 File Offset: 0x0002E2F0
		public void AddListeners(HashSet<Client> clients)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.listeners.UnionWith(clients);
			using (List<Item>.Enumerator enumerator = this.GetAllItems().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Inventory inventory;
					if (enumerator.Current.TryGetItemInventory(out inventory))
					{
						inventory.AddListeners(clients);
					}
				}
			}
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0003015C File Offset: 0x0002E35C
		public bool RemoveListener(Client client)
		{
			using (List<Item>.Enumerator enumerator = this.GetAllItems().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Inventory inventory;
					if (enumerator.Current.TryGetItemInventory(out inventory))
					{
						inventory.RemoveListener(client);
					}
				}
			}
			return this.listeners.Remove(client);
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x000301C4 File Offset: 0x0002E3C4
		public bool HasListener(Client client)
		{
			return this.listeners.Contains(client);
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x000301D2 File Offset: 0x0002E3D2
		public void AddContainer(Container container)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Containers.Add(container);
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x000301E5 File Offset: 0x0002E3E5
		public void SetOwner(Client owner)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner = owner;
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x000301F3 File Offset: 0x0002E3F3
		public void RemoveOwner()
		{
			if (this.Owner != null)
			{
				ClientInventoryManager.RemoveOwnerFromInventory(To.Single(this.Owner), this.InventoryNetID);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner = null;
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0003021F File Offset: 0x0002E41F
		public List<Container> GetContainers()
		{
			return this.Containers;
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00030228 File Offset: 0x0002E428
		[Description("Try and stack the item. Returns the number of items left in the stack.")]
		public void TryStackItem(Item item)
		{
			for (int i = 0; i < this.Containers.Count; i++)
			{
				List<Item> items = this.Containers[i].GetItems();
				for (int j = 0; j < items.Count; j++)
				{
					Item targItem = items[j];
					if (item.CanStackOnItem(targItem))
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						item.AddItemToStack(targItem);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						ClientInventoryManager.UpdateStackForItem(To.Multiple(this.listeners), this.InventoryNetID, i, j, targItem.Stacks);
						if (item.EmptyStack)
						{
							return;
						}
					}
				}
			}
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x000302B8 File Offset: 0x0002E4B8
		public bool TryAddItem(Item item)
		{
			using (List<Container>.Enumerator enumerator = this.Containers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.TryAddItem(item))
					{
						Inventory inventory;
						if (item.TryGetItemInventory(out inventory))
						{
							RuntimeHelpers.EnsureSufficientExecutionStack();
							inventory.AddListeners(this.listeners);
						}
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00030330 File Offset: 0x0002E530
		public bool TryAddItem(Item item, Container container, GridPosition position, GridSize size)
		{
			if (!container.ItemCanFitAtPos(item, position, size))
			{
				return false;
			}
			Inventory inventory;
			if (item.TryGetItemInventory(out inventory))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				inventory.AddListeners(this.listeners);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			item.Size = size;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			item.Position = position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			container.AddItem(item);
			return true;
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0003038C File Offset: 0x0002E58C
		public bool TryMoveItem(Item item, Container container, GridPosition position, GridSize size)
		{
			if (!container.ItemCanFitAtPos(item, position, size))
			{
				return false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			container.UpdateFreeSlots(item.Position, item.Size, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			item.Size = size;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			item.Position = position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			container.UpdateFreeSlots(item.Position, item.Size, false);
			return true;
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x000303F1 File Offset: 0x0002E5F1
		public void RemoveItem(Item item, Container container)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			container.RemoveItem(item);
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00030400 File Offset: 0x0002E600
		public bool TryRemoveItemByIndex(int containerIndex, int itemIndex)
		{
			Container container;
			Item item;
			return this.TryGetContainerByIndex(containerIndex, out container) && container.TryRemoveItemByIndex(itemIndex, out item);
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00030423 File Offset: 0x0002E623
		public bool TryGetContainerByIndex(int containerIndex, out Container container)
		{
			if (containerIndex >= this.Containers.Count || containerIndex < 0)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				container = null;
				return false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			container = this.Containers[containerIndex];
			return container != null;
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0003045C File Offset: 0x0002E65C
		public byte[] Serialize()
		{
			byte[] result;
			using (MemoryStream stream = new MemoryStream())
			{
				using (BinaryWriter writer = new BinaryWriter(stream))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					writer.Write(this.InventoryNetID);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					writer.Write(this.Title);
					if (this.Owner != null)
					{
						writer.Write(this.Owner.NetworkIdent);
					}
					else
					{
						writer.Write(-1);
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					writer.Write(this.Containers.Count);
					foreach (Container container in this.Containers)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						writer.Write(container.Serialize());
					}
					result = stream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00030558 File Offset: 0x0002E758
		public static Inventory Deserialize(BinaryReader reader)
		{
			Inventory inv = new Inventory();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			inv.InventoryNetID = reader.ReadInt32();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			inv.Title = reader.ReadString();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			inv.Owner = NetUtil.GetClientFromNetID(reader.ReadInt32());
			int numContainers = reader.ReadInt32();
			for (int i = 0; i < numContainers; i++)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				inv.AddContainer(Container.Deserialize(reader));
			}
			return inv;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x000305C8 File Offset: 0x0002E7C8
		public static Inventory Deserialize(byte[] data)
		{
			Inventory result;
			using (BinaryReader reader = new BinaryReader(new MemoryStream(data)))
			{
				result = Inventory.Deserialize(reader);
			}
			return result;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00030608 File Offset: 0x0002E808
		public void FullySyncWithListener(Client player)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Assert.True(Game.Current.IsServer);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ClientInventoryManager.ReceiveInventoryData(To.Single(player), this.Serialize());
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00030634 File Offset: 0x0002E834
		public void FullySyncWithListeners()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Assert.True(Game.Current.IsServer);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ClientInventoryManager.ReceiveInventoryData(To.Multiple(this.listeners), this.Serialize());
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00030665 File Offset: 0x0002E865
		public void FullySyncWithOwner()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Assert.True(Game.Current.IsServer);
			if (this.Owner == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ClientInventoryManager.ReceiveInventoryData(To.Single(this.Owner), this.Serialize());
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x000306A0 File Offset: 0x0002E8A0
		public bool TryFindContainerForItem(Item item, out Container foundContainer)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			foundContainer = null;
			IEnumerable<Container> containers = this.Containers;
			Func<Container, bool> <>9__0;
			Func<Container, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = ((Container container) => container.HasItem(item)));
			}
			using (IEnumerator<Container> enumerator = containers.Where(predicate).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					Container container2 = enumerator.Current;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					foundContainer = container2;
					return true;
				}
			}
			return false;
		}
	}
}
