using System;
using Sandbox;

namespace StalkerRP.Inventory
{
	// Token: 0x020000F2 RID: 242
	[GameResource("Ammo", "ammo", "Data about a specific ammo item.", Icon = "inventory")]
	public class AmmoItemResource : ItemResource
	{
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x0002D688 File Offset: 0x0002B888
		// (set) Token: 0x06000AA1 RID: 2721 RVA: 0x0002D690 File Offset: 0x0002B890
		[Category("Ammo Info")]
		public AmmoType AmmoType { get; set; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x0002D699 File Offset: 0x0002B899
		// (set) Token: 0x06000AA3 RID: 2723 RVA: 0x0002D6A1 File Offset: 0x0002B8A1
		[Category("Ammo Info")]
		public float Mass { get; set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x0002D6AA File Offset: 0x0002B8AA
		// (set) Token: 0x06000AA5 RID: 2725 RVA: 0x0002D6B2 File Offset: 0x0002B8B2
		[Category("Ammo Info")]
		public float DragCoefficient { get; set; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x0002D6BB File Offset: 0x0002B8BB
		// (set) Token: 0x06000AA7 RID: 2727 RVA: 0x0002D6C3 File Offset: 0x0002B8C3
		[Category("Ammo Info")]
		public float CrossSectionalArea { get; set; }

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x0002D6CC File Offset: 0x0002B8CC
		// (set) Token: 0x06000AA9 RID: 2729 RVA: 0x0002D6D4 File Offset: 0x0002B8D4
		[Category("Ammo Info")]
		public float Penetration { get; set; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x0002D6DD File Offset: 0x0002B8DD
		// (set) Token: 0x06000AAB RID: 2731 RVA: 0x0002D6E5 File Offset: 0x0002B8E5
		[Category("Ammo Info")]
		[ResourceType("sound")]
		public string FlyBySound { get; set; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x0002D6EE File Offset: 0x0002B8EE
		[HideInEditor]
		public override ItemCategory Category
		{
			get
			{
				return ItemCategory.Ammo;
			}
		}
	}
}
