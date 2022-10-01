using System;
using System.Text.Json.Serialization;
using ModelDoc;

namespace Sandbox
{
	// Token: 0x0200014F RID: 335
	[GameData("break_list_piece", AllowMultiple = true)]
	[HideInEditor]
	public struct ModelBreakPiece
	{
		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000F3F RID: 3903 RVA: 0x0003DAD4 File Offset: 0x0003BCD4
		// (set) Token: 0x06000F40 RID: 3904 RVA: 0x0003DADC File Offset: 0x0003BCDC
		[JsonPropertyName("piece_name")]
		public string PieceName { readonly get; set; }

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000F41 RID: 3905 RVA: 0x0003DAE5 File Offset: 0x0003BCE5
		// (set) Token: 0x06000F42 RID: 3906 RVA: 0x0003DAED File Offset: 0x0003BCED
		public string Model { readonly get; set; }

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x0003DAF6 File Offset: 0x0003BCF6
		// (set) Token: 0x06000F44 RID: 3908 RVA: 0x0003DAFE File Offset: 0x0003BCFE
		public string Ragdoll { readonly get; set; }

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x0003DB07 File Offset: 0x0003BD07
		// (set) Token: 0x06000F46 RID: 3910 RVA: 0x0003DB0F File Offset: 0x0003BD0F
		public Vector3 Offset { readonly get; set; }

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000F47 RID: 3911 RVA: 0x0003DB18 File Offset: 0x0003BD18
		// (set) Token: 0x06000F48 RID: 3912 RVA: 0x0003DB20 File Offset: 0x0003BD20
		public float FadeTime { readonly get; set; }

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x0003DB29 File Offset: 0x0003BD29
		// (set) Token: 0x06000F4A RID: 3914 RVA: 0x0003DB31 File Offset: 0x0003BD31
		public float FadeMinDist { readonly get; set; }

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x0003DB3A File Offset: 0x0003BD3A
		// (set) Token: 0x06000F4C RID: 3916 RVA: 0x0003DB42 File Offset: 0x0003BD42
		public float FadeMaxDist { readonly get; set; }

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x0003DB4B File Offset: 0x0003BD4B
		// (set) Token: 0x06000F4E RID: 3918 RVA: 0x0003DB53 File Offset: 0x0003BD53
		[JsonPropertyName("random_spawn_chance")]
		public float RandomSpawnChance { readonly get; set; }

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x0003DB5C File Offset: 0x0003BD5C
		// (set) Token: 0x06000F50 RID: 3920 RVA: 0x0003DB64 File Offset: 0x0003BD64
		[JsonPropertyName("is_essential_piece")]
		public bool IsEssential { readonly get; set; }

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x0003DB6D File Offset: 0x0003BD6D
		// (set) Token: 0x06000F52 RID: 3922 RVA: 0x0003DB75 File Offset: 0x0003BD75
		[Obsolete]
		[JsonPropertyName("collision_group_override")]
		public string CollisionGroupOverride { readonly get; set; }

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x0003DB7E File Offset: 0x0003BD7E
		// (set) Token: 0x06000F54 RID: 3924 RVA: 0x0003DB86 File Offset: 0x0003BD86
		[JsonPropertyName("collision_tags")]
		public string CollisionTags { readonly get; set; }

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x0003DB8F File Offset: 0x0003BD8F
		// (set) Token: 0x06000F56 RID: 3926 RVA: 0x0003DB97 File Offset: 0x0003BD97
		public string PlacementBone { readonly get; set; }

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x0003DBA0 File Offset: 0x0003BDA0
		// (set) Token: 0x06000F58 RID: 3928 RVA: 0x0003DBA8 File Offset: 0x0003BDA8
		public string PlacementAttachment { readonly get; set; }

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x0003DBB1 File Offset: 0x0003BDB1
		// (set) Token: 0x06000F5A RID: 3930 RVA: 0x0003DBB9 File Offset: 0x0003BDB9
		public string NameMode { readonly get; set; }
	}
}
