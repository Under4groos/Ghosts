using System;

namespace Sandbox.player.assets
{
	// Token: 0x020001B4 RID: 436
	public struct Stamina
	{
		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x00056F40 File Offset: 0x00055140
		// (set) Token: 0x060015F4 RID: 5620 RVA: 0x00056F48 File Offset: 0x00055148
		public float Max { readonly get; set; }

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x060015F5 RID: 5621 RVA: 0x00056F51 File Offset: 0x00055151
		// (set) Token: 0x060015F6 RID: 5622 RVA: 0x00056F59 File Offset: 0x00055159
		public float RechargeDelay { readonly get; set; }

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x060015F7 RID: 5623 RVA: 0x00056F62 File Offset: 0x00055162
		// (set) Token: 0x060015F8 RID: 5624 RVA: 0x00056F6A File Offset: 0x0005516A
		public float RechargeRate { readonly get; set; }

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x060015F9 RID: 5625 RVA: 0x00056F73 File Offset: 0x00055173
		// (set) Token: 0x060015FA RID: 5626 RVA: 0x00056F7B File Offset: 0x0005517B
		public float SecondWindDelay { readonly get; set; }

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x060015FB RID: 5627 RVA: 0x00056F84 File Offset: 0x00055184
		// (set) Token: 0x060015FC RID: 5628 RVA: 0x00056F8C File Offset: 0x0005518C
		public float JumpCost { readonly get; set; }

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x060015FD RID: 5629 RVA: 0x00056F95 File Offset: 0x00055195
		// (set) Token: 0x060015FE RID: 5630 RVA: 0x00056F9D File Offset: 0x0005519D
		public float SprintCost { readonly get; set; }

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x00056FA6 File Offset: 0x000551A6
		// (set) Token: 0x06001600 RID: 5632 RVA: 0x00056FAE File Offset: 0x000551AE
		public float DamageToStaminaLossRatio { readonly get; set; }
	}
}
