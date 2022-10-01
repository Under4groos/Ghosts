using System;

namespace Sandbox.player.assets
{
	// Token: 0x020001B3 RID: 435
	public struct CameraMotion
	{
		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x060015EB RID: 5611 RVA: 0x00056EFC File Offset: 0x000550FC
		// (set) Token: 0x060015EC RID: 5612 RVA: 0x00056F04 File Offset: 0x00055104
		public float ViewBobSpeedThreshold { readonly get; set; }

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x060015ED RID: 5613 RVA: 0x00056F0D File Offset: 0x0005510D
		// (set) Token: 0x060015EE RID: 5614 RVA: 0x00056F15 File Offset: 0x00055115
		public float ViewBobMagnitude { readonly get; set; }

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x00056F1E File Offset: 0x0005511E
		// (set) Token: 0x060015F0 RID: 5616 RVA: 0x00056F26 File Offset: 0x00055126
		public float ViewBobFrequency { readonly get; set; }

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x00056F2F File Offset: 0x0005512F
		// (set) Token: 0x060015F2 RID: 5618 RVA: 0x00056F37 File Offset: 0x00055137
		public float ViewBobRunningMultiplier { readonly get; set; }
	}
}
