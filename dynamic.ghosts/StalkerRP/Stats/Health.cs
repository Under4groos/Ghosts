using System;

namespace StalkerRP.Stats
{
	// Token: 0x02000068 RID: 104
	public struct Health
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x00017CEF File Offset: 0x00015EEF
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x00017CF7 File Offset: 0x00015EF7
		public float Head { readonly get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x00017D00 File Offset: 0x00015F00
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x00017D08 File Offset: 0x00015F08
		public float Torso { readonly get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00017D11 File Offset: 0x00015F11
		// (set) Token: 0x0600049A RID: 1178 RVA: 0x00017D19 File Offset: 0x00015F19
		public float Stomach { readonly get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00017D22 File Offset: 0x00015F22
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x00017D2A File Offset: 0x00015F2A
		public float Arm { readonly get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x00017D33 File Offset: 0x00015F33
		// (set) Token: 0x0600049E RID: 1182 RVA: 0x00017D3B File Offset: 0x00015F3B
		public float Leg { readonly get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x00017D44 File Offset: 0x00015F44
		// (set) Token: 0x060004A0 RID: 1184 RVA: 0x00017D4C File Offset: 0x00015F4C
		public float Psy { readonly get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00017D55 File Offset: 0x00015F55
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x00017D5D File Offset: 0x00015F5D
		public float PsyRegenRate { readonly get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00017D66 File Offset: 0x00015F66
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x00017D6E File Offset: 0x00015F6E
		[Description("How much damage per second you should take for sprinting with a broken leg. Applied twice when both legs are broken")]
		public float SprintBrokenDamage { readonly get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00017D77 File Offset: 0x00015F77
		// (set) Token: 0x060004A6 RID: 1190 RVA: 0x00017D7F File Offset: 0x00015F7F
		[Description("How much to increase recoil by when arms are broken")]
		public float BrokenArmRecoilMultiplier { readonly get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00017D88 File Offset: 0x00015F88
		[HideInEditor]
		public float MaxHealth
		{
			get
			{
				return this.Head + this.Torso + this.Stomach + this.Arm * 2f + this.Leg * 2f;
			}
		}
	}
}
