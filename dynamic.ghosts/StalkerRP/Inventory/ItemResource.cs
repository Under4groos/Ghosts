using System;
using Sandbox;
using StalkerRP.Inventory.Gear;

namespace StalkerRP.Inventory
{
	// Token: 0x020000F6 RID: 246
	[GameResource("Item", "item", "A type of item.")]
	public class ItemResource : StalkerResource
	{
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x0002D715 File Offset: 0x0002B915
		// (set) Token: 0x06000AB3 RID: 2739 RVA: 0x0002D71D File Offset: 0x0002B91D
		[Category("Item Info")]
		public string Title { get; set; }

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x0002D726 File Offset: 0x0002B926
		// (set) Token: 0x06000AB5 RID: 2741 RVA: 0x0002D72E File Offset: 0x0002B92E
		[Category("Item Info")]
		public string Description { get; set; }

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x0002D737 File Offset: 0x0002B937
		// (set) Token: 0x06000AB7 RID: 2743 RVA: 0x0002D73F File Offset: 0x0002B93F
		[Category("Item Info")]
		[DefaultValue(1)]
		public int Width { get; set; } = 1;

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x0002D748 File Offset: 0x0002B948
		// (set) Token: 0x06000AB9 RID: 2745 RVA: 0x0002D750 File Offset: 0x0002B950
		[Category("Item Info")]
		[DefaultValue(1)]
		public int Height { get; set; } = 1;

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x0002D759 File Offset: 0x0002B959
		// (set) Token: 0x06000ABB RID: 2747 RVA: 0x0002D761 File Offset: 0x0002B961
		[Category("Item Info")]
		[DefaultValue(1)]
		public int MaxStacks { get; set; } = 1;

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x0002D76A File Offset: 0x0002B96A
		// (set) Token: 0x06000ABD RID: 2749 RVA: 0x0002D772 File Offset: 0x0002B972
		[Category("Item Info")]
		[DefaultValue(false)]
		public bool CanStack { get; set; }

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x0002D77B File Offset: 0x0002B97B
		// (set) Token: 0x06000ABF RID: 2751 RVA: 0x0002D783 File Offset: 0x0002B983
		[Category("Item Info")]
		[DefaultValue(false)]
		public bool CanDrop { get; set; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x0002D78C File Offset: 0x0002B98C
		// (set) Token: 0x06000AC1 RID: 2753 RVA: 0x0002D794 File Offset: 0x0002B994
		[Category("Item Info")]
		[DefaultValue(EquipmentType.None)]
		public EquipmentType EquipmentType { get; set; }

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x0002D79D File Offset: 0x0002B99D
		// (set) Token: 0x06000AC3 RID: 2755 RVA: 0x0002D7A5 File Offset: 0x0002B9A5
		[Category("Item Info")]
		[DefaultValue(ItemCategory.Misc)]
		public virtual ItemCategory Category { get; set; } = ItemCategory.Misc;

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x0002D7AE File Offset: 0x0002B9AE
		// (set) Token: 0x06000AC5 RID: 2757 RVA: 0x0002D7B6 File Offset: 0x0002B9B6
		[ResourceType("png")]
		[Category("Item Info")]
		public string Icon { get; set; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x0002D7BF File Offset: 0x0002B9BF
		// (set) Token: 0x06000AC7 RID: 2759 RVA: 0x0002D7C7 File Offset: 0x0002B9C7
		[ResourceType("vmdl")]
		[Category("Item Info")]
		public string WorldModel { get; set; }

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0002D7D0 File Offset: 0x0002B9D0
		public virtual void InitializeForItem(Item item)
		{
		}
	}
}
