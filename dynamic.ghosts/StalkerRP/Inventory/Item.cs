using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Sandbox;
using StalkerRP.Inventory.Gear;

namespace StalkerRP.Inventory
{
	// Token: 0x0200010A RID: 266
	public class Item
	{
		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x00030759 File Offset: 0x0002E959
		// (set) Token: 0x06000BED RID: 3053 RVA: 0x00030761 File Offset: 0x0002E961
		public GridPosition Position { get; set; } = new GridPosition(0, 0);

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0003076A File Offset: 0x0002E96A
		// (set) Token: 0x06000BEF RID: 3055 RVA: 0x00030772 File Offset: 0x0002E972
		public GridSize Size { get; set; }

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x0003077B File Offset: 0x0002E97B
		// (set) Token: 0x06000BF1 RID: 3057 RVA: 0x00030783 File Offset: 0x0002E983
		[DefaultValue(1)]
		public int Stacks { get; set; } = 1;

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x0003078C File Offset: 0x0002E98C
		// (set) Token: 0x06000BF3 RID: 3059 RVA: 0x00030794 File Offset: 0x0002E994
		[Description("Miscellaneous data stored inside the item.")]
		public Dictionary<string, string> Data { get; set; } = new Dictionary<string, string>();

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x0003079D File Offset: 0x0002E99D
		// (set) Token: 0x06000BF5 RID: 3061 RVA: 0x000307A5 File Offset: 0x0002E9A5
		[Description("Represents the cached ID of an ItemAsset. This MUST be set.")]
		public string AssetID { get; set; }

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x000307AE File Offset: 0x0002E9AE
		public ItemResource Resource
		{
			get
			{
				return StalkerResource.Get<ItemResource>(this.AssetID);
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x000307BB File Offset: 0x0002E9BB
		public string Title
		{
			get
			{
				return this.Resource.Title;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x000307C8 File Offset: 0x0002E9C8
		public string Description
		{
			get
			{
				return this.Resource.Description;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x000307D5 File Offset: 0x0002E9D5
		public EquipmentType EquipmentType
		{
			get
			{
				return this.Resource.EquipmentType;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x000307E2 File Offset: 0x0002E9E2
		public int MaxStack
		{
			get
			{
				return this.Resource.MaxStacks;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x000307EF File Offset: 0x0002E9EF
		public bool CanAddToStack
		{
			get
			{
				return this.CanStack && this.Stacks < this.MaxStack;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x00030809 File Offset: 0x0002EA09
		public bool CanStack
		{
			get
			{
				return this.Resource.CanStack;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x00030816 File Offset: 0x0002EA16
		public bool CanDrop
		{
			get
			{
				return this.Resource.CanDrop;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x00030823 File Offset: 0x0002EA23
		public bool EmptyStack
		{
			get
			{
				return this.Stacks <= 0;
			}
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00030831 File Offset: 0x0002EA31
		public Item()
		{
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00030858 File Offset: 0x0002EA58
		public Item(string assetId)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AssetID = assetId;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Size = new GridSize(this.Resource.Width, this.Resource.Height, false);
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x000308C0 File Offset: 0x0002EAC0
		public Item(Item item)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AssetID = item.AssetID;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Size = item.Size;
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00030914 File Offset: 0x0002EB14
		public void InitializeFromAsset()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertServer("InitializeFromAsset");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Resource.InitializeForItem(this);
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x00030936 File Offset: 0x0002EB36
		public bool CanStackOnItem(Item item)
		{
			return this != item && item.AssetID == this.AssetID && item.CanAddToStack;
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00030958 File Offset: 0x0002EB58
		public int AddItemToStack(Item item)
		{
			int currentStack = item.Stacks;
			int newStack = Math.Min(currentStack + this.Stacks, item.MaxStack);
			int dif = newStack - currentStack;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Stacks -= dif;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			item.Stacks = newStack;
			return dif;
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x000309A4 File Offset: 0x0002EBA4
		public byte[] Serialize()
		{
			byte[] result;
			using (MemoryStream stream = new MemoryStream())
			{
				using (BinaryWriter writer = new BinaryWriter(stream))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					writer.Write(this.Position.Serialize());
					RuntimeHelpers.EnsureSufficientExecutionStack();
					writer.Write(this.Size.Serialize());
					RuntimeHelpers.EnsureSufficientExecutionStack();
					writer.Write(this.AssetID);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					writer.Write(this.Stacks);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					writer.Write(this.Data.Count);
					foreach (KeyValuePair<string, string> keyValuePair in this.Data)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						writer.Write(keyValuePair.Key);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						writer.Write(keyValuePair.Value);
					}
					result = stream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00030AC0 File Offset: 0x0002ECC0
		public static Item Deserialize(BinaryReader reader)
		{
			Item item = new Item();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			item.Position = GridPosition.Deserialize(reader);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			item.Size = GridSize.Deserialize(reader);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			item.AssetID = reader.ReadString();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			item.Stacks = reader.ReadInt32();
			int count = reader.ReadInt32();
			for (int i = 0; i < count; i++)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				item.Data[reader.ReadString()] = reader.ReadString();
			}
			return item;
		}
	}
}
