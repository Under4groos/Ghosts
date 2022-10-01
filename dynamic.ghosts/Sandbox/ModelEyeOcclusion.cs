using System;
using System.Text.Json.Serialization;
using ModelDoc;

namespace Sandbox
{
	// Token: 0x02000156 RID: 342
	[GameData("eye_occlusion_renderer")]
	[Box("dimensions", Bone = "bonename", Origin = "offset_origin", Angles = "offset_angles")]
	[Axis(Bone = "bonename", Origin = "offset_origin", Angles = "offset_angles")]
	public class ModelEyeOcclusion
	{
		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x0003DDF4 File Offset: 0x0003BFF4
		// (set) Token: 0x06000F92 RID: 3986 RVA: 0x0003DDFC File Offset: 0x0003BFFC
		[FGDType("model_bone", "", "")]
		public string BoneName { get; set; }

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x0003DE05 File Offset: 0x0003C005
		// (set) Token: 0x06000F94 RID: 3988 RVA: 0x0003DE0D File Offset: 0x0003C00D
		[ScaleBoneRelative]
		[DefaultValue("12 12 12")]
		public Vector3 Dimensions { get; set; }

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x0003DE16 File Offset: 0x0003C016
		// (set) Token: 0x06000F96 RID: 3990 RVA: 0x0003DE1E File Offset: 0x0003C01E
		[JsonPropertyName("offset_origin")]
		[ScaleBoneRelative]
		public Vector3 OriginOffset { get; set; }

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x0003DE27 File Offset: 0x0003C027
		// (set) Token: 0x06000F98 RID: 3992 RVA: 0x0003DE2F File Offset: 0x0003C02F
		[JsonPropertyName("offset_angles")]
		public Angles Angles { get; set; }
	}
}
