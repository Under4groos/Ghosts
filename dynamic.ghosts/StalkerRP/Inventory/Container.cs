using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.Inventory
{
	// Token: 0x020000FD RID: 253
	public class Container
	{
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x0002E7EB File Offset: 0x0002C9EB
		// (set) Token: 0x06000B53 RID: 2899 RVA: 0x0002E7F3 File Offset: 0x0002C9F3
		private List<Item> items { get; set; } = new List<Item>();

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0002E7FC File Offset: 0x0002C9FC
		// (set) Token: 0x06000B55 RID: 2901 RVA: 0x0002E804 File Offset: 0x0002CA04
		public GridSize Size
		{
			get
			{
				return this.size;
			}
			set
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.size = value;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.freeSlots = new bool[this.size.Height, this.size.Width];
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.freeSlotsCount = this.size.Total;
			}
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0002E859 File Offset: 0x0002CA59
		public Container()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Size = new GridSize(1, 1, false);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0002E87F File Offset: 0x0002CA7F
		public Container(GridSize size)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Size = size;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ResetFreeSlots();
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0002E8AC File Offset: 0x0002CAAC
		public bool TryAddItem(Item item)
		{
			GridPosition pos = this.FindFirstFreeSlotForItem(item.Size);
			if (pos.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				item.Position = pos;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.AddItem(item);
				return true;
			}
			GridSize size = new GridSize(item.Size);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			size.IsRotated = !size.IsRotated;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pos = this.FindFirstFreeSlotForItem(size);
			if (!pos.IsValid())
			{
				return false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			item.Position = pos;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			item.Size = size;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AddItem(item);
			return true;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0002E944 File Offset: 0x0002CB44
		private void ResetFreeSlots()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.freeSlots = new bool[this.Size.Height, this.Size.Width];
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.freeSlotsCount = this.size.Total;
			for (int y = 0; y < this.Size.Height; y++)
			{
				for (int x = 0; x < this.Size.Width; x++)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.freeSlots[y, x] = true;
				}
			}
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0002E9CC File Offset: 0x0002CBCC
		[Description("Recalculates all the free slots from the currently stored items.")]
		public void RecalculateFreeSlots()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ResetFreeSlots();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.items.ForEach(delegate(Item x)
			{
				this.UpdateFreeSlots(x.Position, x.Size, false);
			});
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0002E9F8 File Offset: 0x0002CBF8
		public void AddListenersForAllItemInventories(Client client)
		{
			using (List<Item>.Enumerator enumerator = this.items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Inventory inventory;
					if (enumerator.Current.TryGetItemInventory(out inventory))
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						inventory.AddListener(client);
					}
				}
			}
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0002EA58 File Offset: 0x0002CC58
		public void RemoveListenersForAllItemInventories(Client client)
		{
			using (List<Item>.Enumerator enumerator = this.items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Inventory inventory;
					if (enumerator.Current.TryGetItemInventory(out inventory))
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						inventory.RemoveListener(client);
					}
				}
			}
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0002EABC File Offset: 0x0002CCBC
		public void AddItem(Item item)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.items.Add(item);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateFreeSlots(item.Position, item.Size, false);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0002EAE7 File Offset: 0x0002CCE7
		public bool RemoveItem(Item item)
		{
			if (this.items.Remove(item))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.UpdateFreeSlots(item.Position, item.Size, true);
				return true;
			}
			return false;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0002EB12 File Offset: 0x0002CD12
		public bool HasItem(Item item)
		{
			return this.items.Contains(item);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0002EB20 File Offset: 0x0002CD20
		public bool TryRemoveItemByIndex(int index, out Item item)
		{
			if (!this.TryGetItemByIndex(index, out item))
			{
				return false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RemoveItem(item);
			return true;
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0002EB40 File Offset: 0x0002CD40
		public void DropItem(int index, Vector3 position, Rotation rotation)
		{
			Item item;
			if (!this.TryGetItemByIndex(index, out item))
			{
				return;
			}
			if (!item.Resource.CanDrop)
			{
				return;
			}
			ItemEntity itemEntity = new ItemEntity();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			itemEntity.SetItem(item);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			itemEntity.Position = position + rotation.Forward * 20f + Vector3.Down * 10f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			itemEntity.Rotation = rotation;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			PhysicsGroup physicsGroup = itemEntity.PhysicsGroup;
			if (physicsGroup != null)
			{
				physicsGroup.ApplyImpulse(rotation.Forward * 200f + Vector3.Up * 50f, true);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			PhysicsGroup physicsGroup2 = itemEntity.PhysicsGroup;
			if (physicsGroup2 != null)
			{
				physicsGroup2.ApplyAngularImpulse(Vector3.Random * 50f, true);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RemoveItem(item);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0002EC28 File Offset: 0x0002CE28
		public bool TryGetItemByIndex(int itemIndex, out Item item)
		{
			if (itemIndex >= this.items.Count || itemIndex < 0)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				item = null;
				return false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			item = this.items[itemIndex];
			return item != null;
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0002EC5E File Offset: 0x0002CE5E
		public List<Item> GetItems()
		{
			return this.items;
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0002EC68 File Offset: 0x0002CE68
		public void UpdateFreeSlots(GridPosition pos, GridSize itemSize, bool newValue = false)
		{
			for (int i = 0; i < itemSize.EffectiveHeight; i++)
			{
				for (int j = 0; j < itemSize.EffectiveWidth; j++)
				{
					int x = j + pos.X;
					int y = i + pos.Y;
					if (x >= 0 && x < this.Size.Width && y >= 0 && y < this.Size.Height)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.freeSlots[y, x] = newValue;
						if (newValue)
						{
							this.freeSlotsCount++;
						}
						else
						{
							this.freeSlotsCount--;
						}
					}
				}
			}
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0002ED04 File Offset: 0x0002CF04
		public GridPosition FindFirstFreeSlotForItem(GridSize itemSize)
		{
			if (itemSize.Total > this.Size.Total)
			{
				return GridPosition.Invalid;
			}
			if (this.freeSlotsCount < itemSize.Total)
			{
				return GridPosition.Invalid;
			}
			if (itemSize.EffectiveWidth > this.Size.Width)
			{
				return GridPosition.Invalid;
			}
			if (itemSize.EffectiveHeight > this.Size.Height)
			{
				return GridPosition.Invalid;
			}
			for (int y = 0; y < this.Size.Height; y++)
			{
				for (int x = 0; x < this.Size.Width; x++)
				{
					GridPosition pos = new GridPosition(x, y);
					if (this.ItemCanFitAtPos(pos, itemSize))
					{
						return pos;
					}
				}
			}
			return GridPosition.Invalid;
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0002EDB8 File Offset: 0x0002CFB8
		public bool ItemCanFitAtPos(GridPosition pos, GridSize itemSize)
		{
			if (pos.X + itemSize.EffectiveWidth > this.Size.Width)
			{
				return false;
			}
			if (pos.Y + itemSize.EffectiveHeight > this.Size.Height)
			{
				return false;
			}
			if (pos.X < 0 || pos.Y < 0)
			{
				return false;
			}
			for (int i = 0; i < itemSize.EffectiveHeight; i++)
			{
				for (int j = 0; j < itemSize.EffectiveWidth; j++)
				{
					int x = j + pos.X;
					int y = i + pos.Y;
					if (!this.freeSlots[y, x])
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0002EE58 File Offset: 0x0002D058
		public bool ItemCanFitAtPos(Item item, GridPosition pos, GridSize itemSize)
		{
			bool contains = this.items.Contains(item);
			if (contains)
			{
				this.UpdateFreeSlots(item.Position, item.Size, true);
			}
			for (int i = 0; i < itemSize.EffectiveHeight; i++)
			{
				for (int j = 0; j < itemSize.EffectiveWidth; j++)
				{
					int x = j + pos.X;
					int y = i + pos.Y;
					if (x >= this.Size.Width || y >= this.Size.Height)
					{
						if (contains)
						{
							this.UpdateFreeSlots(item.Position, item.Size, false);
						}
						return false;
					}
					if (x < 0 || y < 0)
					{
						if (contains)
						{
							this.UpdateFreeSlots(item.Position, item.Size, false);
						}
						return false;
					}
					if (!this.freeSlots[y, x])
					{
						if (contains)
						{
							this.UpdateFreeSlots(item.Position, item.Size, false);
						}
						return false;
					}
				}
			}
			if (contains)
			{
				this.UpdateFreeSlots(item.Position, item.Size, false);
			}
			return true;
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0002EF5E File Offset: 0x0002D15E
		public bool IsValidGridPosition(GridPosition position)
		{
			return position.X >= 0 && position.Y >= 0 && position.X < this.Size.Width && position.Y < this.Size.Height;
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0002EFA0 File Offset: 0x0002D1A0
		public byte[] Serialize()
		{
			byte[] result;
			using (MemoryStream stream = new MemoryStream())
			{
				using (BinaryWriter writer = new BinaryWriter(stream))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					writer.Write(this.Size.Serialize());
					RuntimeHelpers.EnsureSufficientExecutionStack();
					writer.Write(this.items.Count);
					foreach (Item item in this.items)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						writer.Write(item.Serialize());
					}
					result = stream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0002F070 File Offset: 0x0002D270
		public static Container Deserialize(BinaryReader reader)
		{
			Container container = new Container();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			container.Size = GridSize.Deserialize(reader);
			int numItems = reader.ReadInt32();
			for (int i = 0; i < numItems; i++)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				container.AddItem(Item.Deserialize(reader));
			}
			return container;
		}

		// Token: 0x0400039F RID: 927
		private bool[,] freeSlots;

		// Token: 0x040003A0 RID: 928
		private GridSize size;

		// Token: 0x040003A1 RID: 929
		private int freeSlotsCount;
	}
}
