using System;
using ModelDoc;

namespace Sandbox
{
	// Token: 0x02000154 RID: 340
	[GameData("eye", AllowMultiple = true)]
	[Description("Defines an eye on a character model.")]
	public class ModelEye
	{
		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0003DD8A File Offset: 0x0003BF8A
		// (set) Token: 0x06000F88 RID: 3976 RVA: 0x0003DD92 File Offset: 0x0003BF92
		[FGDType("model_bone", "", "")]
		public string BoneName { get; set; }

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x0003DD9B File Offset: 0x0003BF9B
		// (set) Token: 0x06000F8A RID: 3978 RVA: 0x0003DDA3 File Offset: 0x0003BFA3
		[DefaultValue(1)]
		public float Radius { get; set; } = 1f;

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x0003DDAC File Offset: 0x0003BFAC
		// (set) Token: 0x06000F8C RID: 3980 RVA: 0x0003DDB4 File Offset: 0x0003BFB4
		[Title("Yaw (Degrees)")]
		[DefaultValue(4)]
		public float Yaw { get; set; } = 4f;
	}
}
