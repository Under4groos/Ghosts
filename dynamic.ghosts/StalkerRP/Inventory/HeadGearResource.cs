using System;
using Sandbox;

namespace StalkerRP.Inventory
{
	// Token: 0x020000F4 RID: 244
	[GameResource("Head Gear Resource", "headgear", "An item that you can wear as a headpiece.")]
	public class HeadGearResource : ItemResource
	{
		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x0002D6F9 File Offset: 0x0002B8F9
		// (set) Token: 0x06000AAF RID: 2735 RVA: 0x0002D701 File Offset: 0x0002B901
		[Category("Flashlight")]
		public bool HasFlashlight { get; set; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x0002D70A File Offset: 0x0002B90A
		[HideInEditor]
		public override ItemCategory Category
		{
			get
			{
				return ItemCategory.Accessory;
			}
		}
	}
}
