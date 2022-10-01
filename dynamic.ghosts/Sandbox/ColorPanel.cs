using System;
using System.Runtime.CompilerServices;
using Sandbox.Internal;
using Sandbox.UI;

namespace Sandbox
{
	// Token: 0x02000190 RID: 400
	[UseTemplate]
	internal class ColorPanel : Panel
	{
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x0004C64F File Offset: 0x0004A84F
		// (set) Token: 0x0600136C RID: 4972 RVA: 0x0004C657 File Offset: 0x0004A857
		[DefaultValue(1f)]
		public float Saturation { get; set; } = 1f;

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x0600136D RID: 4973 RVA: 0x0004C660 File Offset: 0x0004A860
		// (set) Token: 0x0600136E RID: 4974 RVA: 0x0004C668 File Offset: 0x0004A868
		[DefaultValue(0f)]
		public float Vignette { get; set; }

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x0600136F RID: 4975 RVA: 0x0004C671 File Offset: 0x0004A871
		// (set) Token: 0x06001370 RID: 4976 RVA: 0x0004C679 File Offset: 0x0004A879
		[DefaultValue(0f)]
		public float Sharpen { get; set; }

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001371 RID: 4977 RVA: 0x0004C682 File Offset: 0x0004A882
		// (set) Token: 0x06001372 RID: 4978 RVA: 0x0004C68A File Offset: 0x0004A88A
		[DefaultValue(0f)]
		public float Hue { get; set; }

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x0004C693 File Offset: 0x0004A893
		// (set) Token: 0x06001374 RID: 4980 RVA: 0x0004C69B File Offset: 0x0004A89B
		[DefaultValue(1f)]
		public float Brightness { get; set; } = 1f;

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001375 RID: 4981 RVA: 0x0004C6A4 File Offset: 0x0004A8A4
		// (set) Token: 0x06001376 RID: 4982 RVA: 0x0004C6AC File Offset: 0x0004A8AC
		[DefaultValue(1f)]
		public float Contrast { get; set; } = 1f;

		// Token: 0x06001377 RID: 4983 RVA: 0x0004C6B8 File Offset: 0x0004A8B8
		public override void Tick()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Tick();
			DevCamPP pp = GlobalGameNamespace.PostProcess.Get<DevCamPP>();
			if (pp == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pp.Saturate.Enabled = (this.Saturation != 1f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pp.Saturate.Amount = this.Saturation;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pp.Vignette.Enabled = (this.Vignette != 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pp.Vignette.Intensity = this.Vignette;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pp.Sharpen.Enabled = (this.Sharpen != 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pp.Sharpen.Strength = this.Sharpen;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pp.HueRotate.Enabled = (this.Hue != 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pp.HueRotate.Angle = this.Hue;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pp.Brightness.Enabled = (this.Brightness != 1f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pp.Brightness.Multiplier = this.Brightness;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pp.Contrast.Enabled = (this.Contrast != 1f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pp.Contrast.Contrast = this.Contrast;
		}
	}
}
