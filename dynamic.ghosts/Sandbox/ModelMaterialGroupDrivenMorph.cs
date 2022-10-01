using System;
using System.Text.Json.Serialization;
using ModelDoc;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x02000158 RID: 344
	[GameData("materialgroup_driven_morph", AllowMultiple = true)]
	public class ModelMaterialGroupDrivenMorph
	{
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x0003DE7B File Offset: 0x0003C07B
		// (set) Token: 0x06000FA2 RID: 4002 RVA: 0x0003DE83 File Offset: 0x0003C083
		[JsonPropertyName("materialgroup_name")]
		[FGDType("materialgroup", "", "")]
		public string MaterialGroup { get; set; }

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x0003DE8C File Offset: 0x0003C08C
		// (set) Token: 0x06000FA4 RID: 4004 RVA: 0x0003DE94 File Offset: 0x0003C094
		[JsonPropertyName("morph_name")]
		[FGDType("model_morphchannel", "", "")]
		[EntityReportSource]
		public string MorphChannel { get; set; }
	}
}
