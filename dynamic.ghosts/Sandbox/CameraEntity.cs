using System;
using System.Runtime.CompilerServices;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x0200017B RID: 379
	[Library("point_camera")]
	[HammerEntity]
	[FrustumBoundless("fov", "znear", "zfar")]
	[EditorModel("models/editor/camera.vmdl", "white", "white")]
	[Title("Point Camera")]
	[Category("Gameplay")]
	[Icon("photo_camera")]
	[Description("Camera for use with <see cref=\"T:Sandbox.MonitorEntity\">monitor</see> entity.")]
	public class CameraEntity : Entity
	{
		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x000456A4 File Offset: 0x000438A4
		// (set) Token: 0x060011AB RID: 4523 RVA: 0x000456AC File Offset: 0x000438AC
		[Property]
		[DefaultValue(90f)]
		[Description("Field of view in degrees")]
		public float Fov { get; set; } = 90f;

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x000456B5 File Offset: 0x000438B5
		// (set) Token: 0x060011AD RID: 4525 RVA: 0x000456BD File Offset: 0x000438BD
		[Property]
		[DefaultValue(4f)]
		[Description("Distance to the near plane")]
		public float ZNear { get; set; } = 4f;

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x000456C6 File Offset: 0x000438C6
		// (set) Token: 0x060011AF RID: 4527 RVA: 0x000456CE File Offset: 0x000438CE
		[Property]
		[DefaultValue(10000f)]
		[Description("Distance to the far plane")]
		public float ZFar { get; set; } = 10000f;

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x000456D7 File Offset: 0x000438D7
		// (set) Token: 0x060011B1 RID: 4529 RVA: 0x000456DF File Offset: 0x000438DF
		[Property]
		[DefaultValue(1f)]
		[Description("Aspect ratio")]
		public float Aspect { get; set; } = 1f;

		// Token: 0x060011B2 RID: 4530 RVA: 0x000456E8 File Offset: 0x000438E8
		public CameraEntity()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Transmit = TransmitType.Always;
		}
	}
}
