using System;
using ModelDoc;

namespace Sandbox
{
	// Token: 0x02000152 RID: 338
	[GameData("ao_proxy_capsule", AllowMultiple = true)]
	[Capsule("point0", "point1", "radius", Bone = "bonename")]
	[Description("Capsule Ambient Occlusion Proxy. Used internally by the engine for AO and reflections.")]
	public class ModelCapsuleAOProxy
	{
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x0003DCE7 File Offset: 0x0003BEE7
		// (set) Token: 0x06000F76 RID: 3958 RVA: 0x0003DCEF File Offset: 0x0003BEEF
		[FGDType("model_bone", "", "")]
		public string BoneName { get; set; }

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x0003DCF8 File Offset: 0x0003BEF8
		// (set) Token: 0x06000F78 RID: 3960 RVA: 0x0003DD00 File Offset: 0x0003BF00
		[ScaleBoneRelative]
		[DefaultValue(5)]
		public float Radius { get; set; } = 5f;

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x0003DD09 File Offset: 0x0003BF09
		// (set) Token: 0x06000F7A RID: 3962 RVA: 0x0003DD11 File Offset: 0x0003BF11
		[ScaleBoneRelative]
		public Vector3 Point0 { get; set; }

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x0003DD1A File Offset: 0x0003BF1A
		// (set) Token: 0x06000F7C RID: 3964 RVA: 0x0003DD22 File Offset: 0x0003BF22
		[ScaleBoneRelative]
		[DefaultValue("10 0 0")]
		public Vector3 Point1 { get; set; }
	}
}
