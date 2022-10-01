using System;
using System.Text.Json.Serialization;
using ModelDoc;

namespace Sandbox
{
	// Token: 0x02000153 RID: 339
	[GameData("ao_proxy_box", AllowMultiple = true)]
	[Box("dimensions", Bone = "bonename", Origin = "offset_origin", Angles = "offset_angles")]
	[Axis(Bone = "bonename", Origin = "offset_origin", Angles = "offset_angles")]
	[Description("Box Ambient Occlusion Proxy. Used internally by the engine for AO and reflections.")]
	public class ModelBoxAOProxy
	{
		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x0003DD3E File Offset: 0x0003BF3E
		// (set) Token: 0x06000F7F RID: 3967 RVA: 0x0003DD46 File Offset: 0x0003BF46
		[FGDType("model_bone", "", "")]
		public string BoneName { get; set; }

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x0003DD4F File Offset: 0x0003BF4F
		// (set) Token: 0x06000F81 RID: 3969 RVA: 0x0003DD57 File Offset: 0x0003BF57
		[ScaleBoneRelative]
		[DefaultValue("10 10 10")]
		public Vector3 Dimensions { get; set; }

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x0003DD60 File Offset: 0x0003BF60
		// (set) Token: 0x06000F83 RID: 3971 RVA: 0x0003DD68 File Offset: 0x0003BF68
		[JsonPropertyName("offset_origin")]
		[ScaleBoneRelative]
		public Vector3 OriginOffset { get; set; }

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x0003DD71 File Offset: 0x0003BF71
		// (set) Token: 0x06000F85 RID: 3973 RVA: 0x0003DD79 File Offset: 0x0003BF79
		[JsonPropertyName("offset_angles")]
		public Angles Angles { get; set; }
	}
}
