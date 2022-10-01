using System;
using System.Text.Json.Serialization;
using ModelDoc;

namespace Sandbox
{
	// Token: 0x0200017C RID: 380
	[GameData("door_sounds")]
	[Description("Sounds to be used by ent_door if it does not override sounds.")]
	public class ModelDoorSounds
	{
		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x00045728 File Offset: 0x00043928
		// (set) Token: 0x060011B4 RID: 4532 RVA: 0x00045730 File Offset: 0x00043930
		[JsonPropertyName("fully_open_sound")]
		[Description("Sound to play when the door reaches it's fully open position.")]
		public string FullyOpenSound { get; set; }

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x00045739 File Offset: 0x00043939
		// (set) Token: 0x060011B6 RID: 4534 RVA: 0x00045741 File Offset: 0x00043941
		[JsonPropertyName("fully_closed_sound")]
		[Description("Sound to play when the door reaches it's fully closed position.")]
		public string FullyClosedSound { get; set; }

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x0004574A File Offset: 0x0004394A
		// (set) Token: 0x060011B8 RID: 4536 RVA: 0x00045752 File Offset: 0x00043952
		[JsonPropertyName("open_sound")]
		[Title("Start opening sound")]
		[Description("Sound to play when the door starts to open.")]
		public string OpenSound { get; set; }

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x0004575B File Offset: 0x0004395B
		// (set) Token: 0x060011BA RID: 4538 RVA: 0x00045763 File Offset: 0x00043963
		[JsonPropertyName("close_sound")]
		[Title("Start closing sound")]
		[Description("Sound to play when the door starts to close.")]
		public string CloseSound { get; set; }

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x0004576C File Offset: 0x0004396C
		// (set) Token: 0x060011BC RID: 4540 RVA: 0x00045774 File Offset: 0x00043974
		[JsonPropertyName("moving_sound")]
		[Description("Sound to play while the door is moving. Typically this should be looping or very long.")]
		public string MovingSound { get; set; }

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x0004577D File Offset: 0x0004397D
		// (set) Token: 0x060011BE RID: 4542 RVA: 0x00045785 File Offset: 0x00043985
		[JsonPropertyName("locked_sound")]
		[Description("Sound to play when the door is attempted to be opened, but is locked.")]
		public string LockedSound { get; set; }
	}
}
