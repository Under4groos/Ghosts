using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.Inventory
{
	// Token: 0x020000F7 RID: 247
	[GameResource("Stash", "stash", "Resource for items you can store.")]
	public class StashResource : StalkerResource
	{
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x0002D7F6 File Offset: 0x0002B9F6
		// (set) Token: 0x06000ACB RID: 2763 RVA: 0x0002D7FE File Offset: 0x0002B9FE
		[Category("Stash Info")]
		public List<Vector2> StorageSerialized { get; set; }

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x0002D807 File Offset: 0x0002BA07
		// (set) Token: 0x06000ACD RID: 2765 RVA: 0x0002D80F File Offset: 0x0002BA0F
		[Category("Stash Info")]
		public string Title { get; set; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x0002D818 File Offset: 0x0002BA18
		// (set) Token: 0x06000ACF RID: 2767 RVA: 0x0002D820 File Offset: 0x0002BA20
		[ResourceType("png")]
		[Category("Stash Info")]
		public string Icon { get; set; }

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x0002D829 File Offset: 0x0002BA29
		// (set) Token: 0x06000AD1 RID: 2769 RVA: 0x0002D831 File Offset: 0x0002BA31
		[ResourceType("vmdl")]
		[Category("Stash Info")]
		public string WorldModel { get; set; }

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0002D83C File Offset: 0x0002BA3C
		public Inventory GenerateInventory()
		{
			Inventory inv = new Inventory
			{
				Title = this.Title
			};
			foreach (Vector2 vector2 in this.StorageSerialized)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				inv.AddContainer(new Container(new GridSize(vector2.x.FloorToInt(), vector2.y.FloorToInt(), false)));
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ServerInventoryManager.GenerateNetIDForInventory(inv);
			return inv;
		}
	}
}
