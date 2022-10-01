using System;
using System.Text.Json.Serialization;
using ModelDoc;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x02000157 RID: 343
	[GameData("bodygroup_driven_morph", AllowMultiple = true)]
	public class ModelBodygroupDrivenMorph
	{
		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000F9A RID: 3994 RVA: 0x0003DE40 File Offset: 0x0003C040
		// (set) Token: 0x06000F9B RID: 3995 RVA: 0x0003DE48 File Offset: 0x0003C048
		[JsonPropertyName("bodygroup_name")]
		[FGDType("model_bodygroup", "", "")]
		public string BodygroupName { get; set; }

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x0003DE51 File Offset: 0x0003C051
		// (set) Token: 0x06000F9D RID: 3997 RVA: 0x0003DE59 File Offset: 0x0003C059
		[JsonPropertyName("bodygroup_choice")]
		public int bodygroupChoice { get; set; }

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x0003DE62 File Offset: 0x0003C062
		// (set) Token: 0x06000F9F RID: 3999 RVA: 0x0003DE6A File Offset: 0x0003C06A
		[JsonPropertyName("morph_name")]
		[FGDType("model_morphchannel", "", "")]
		[EntityReportSource]
		public string MorphChannel { get; set; }
	}
}
