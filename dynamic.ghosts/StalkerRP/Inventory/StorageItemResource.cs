using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.Inventory
{
	// Token: 0x020000F8 RID: 248
	[GameResource("Item Storage", "storage", "Resource for items that contain other inventories.")]
	public class StorageItemResource : ItemResource
	{
		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x0002D8DC File Offset: 0x0002BADC
		// (set) Token: 0x06000AD5 RID: 2773 RVA: 0x0002D8E4 File Offset: 0x0002BAE4
		[Category("Storage Info")]
		public List<Vector2> StorageSerialized { get; set; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x0002D8ED File Offset: 0x0002BAED
		public override ItemCategory Category
		{
			get
			{
				return ItemCategory.Storage;
			}
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0002D8F0 File Offset: 0x0002BAF0
		public Inventory GenerateInventory()
		{
			Inventory inv = new Inventory
			{
				Title = base.Title
			};
			foreach (Vector2 vector2 in this.StorageSerialized)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				inv.AddContainer(new Container(new GridSize(vector2.x.FloorToInt(), vector2.y.FloorToInt(), false)));
			}
			return inv;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0002D980 File Offset: 0x0002BB80
		public override void InitializeForItem(Item item)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			item.SetupItemInventory();
		}
	}
}
