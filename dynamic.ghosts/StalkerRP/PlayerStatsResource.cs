using System;
using System.Collections.Generic;
using Sandbox;
using Sandbox.player.assets;
using StalkerRP.Stats;

namespace StalkerRP
{
	// Token: 0x0200002A RID: 42
	[GameResource("Player Stats", "player", "Contains stats for a player.")]
	public class PlayerStatsResource : StalkerResource
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00009F88 File Offset: 0x00008188
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00009F90 File Offset: 0x00008190
		public Health Health { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00009F99 File Offset: 0x00008199
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00009FA1 File Offset: 0x000081A1
		public Stamina Stamina { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00009FAA File Offset: 0x000081AA
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00009FB2 File Offset: 0x000081B2
		public Movement Movement { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00009FBB File Offset: 0x000081BB
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00009FC3 File Offset: 0x000081C3
		public CameraMotion CameraMotion { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00009FCC File Offset: 0x000081CC
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00009FD4 File Offset: 0x000081D4
		[Category("Inventory")]
		public List<string> StartingItems { get; set; }
	}
}
