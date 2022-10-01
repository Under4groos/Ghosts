using System;
using System.Runtime.CompilerServices;
using Sandbox.Internal;

namespace Sandbox
{
	// Token: 0x02000165 RID: 357
	public class StandardPostProcess : BasePostProcess
	{
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x00040565 File Offset: 0x0003E765
		public override PostProcessPass Passes
		{
			get
			{
				return PostProcessPass.Hdr | PostProcessPass.Sdr;
			}
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x00040568 File Offset: 0x0003E768
		public StandardPostProcess()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertClient(".ctor");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PostProcessPass1 = Material.Load("materials/postprocess/standard_pass1.vmat");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PostProcessPass2 = Material.Load("materials/postprocess/standard_pass2.vmat");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PostProcessDof = Material.Load("materials/postprocess/standard_dof.vmat");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PostProcessPass3 = Material.Load("materials/postprocess/standard_pass3.vmat");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.BuildRenderTargets();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Event.Register(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Event.Register(this.DepthOfField);
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x000406B4 File Offset: 0x0003E8B4
		[Event.Screen.SizeChangedAttribute]
		[Description("We use this to monitor recreation of render targets on resolution changes. We can't do this in post processing because we cannot create textures in the render block")]
		public void BuildRenderTargets()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LastScreenSize = Screen.Size;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DofHalfResSize = this.LastScreenSize * 0.5f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DofCocLut = Texture.CreateRenderTarget("Dof_CocLut", ImageFormat.RGBA16161616F, this.LastScreenSize);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DofDownscale2 = Texture.CreateRenderTarget("Dof_Half2", ImageFormat.RGBA16161616F, this.DofHalfResSize);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DofDownscale = Texture.CreateRenderTarget("Dof_Half", ImageFormat.RGBA16161616F, this.DofHalfResSize);
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x00040744 File Offset: 0x0003E944
		private bool HasPass1
		{
			get
			{
				return this.PostProcessPass1 != null && (this.Pixelate.Enabled || this.LensDistortion.Enabled || this.PaniniProjection.Enabled || this.ChromaticAberration.Enabled || this.MotionBlur.Enabled || this.Sharpen.Enabled);
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x000407A9 File Offset: 0x0003E9A9
		private bool HasPass2
		{
			get
			{
				return this.PostProcessPass2 != null && this.Blur.Enabled;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x000407C0 File Offset: 0x0003E9C0
		private bool HasDof
		{
			get
			{
				return this.PostProcessDof != null && this.DepthOfField.Enabled;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x000407D8 File Offset: 0x0003E9D8
		private bool HasPass3
		{
			get
			{
				return this.PostProcessPass3 != null && (this.ColorOverlay.Enabled || this.Saturate.Enabled || this.FilmGrain.Enabled || this.Vignette.Enabled || this.HueRotate.Enabled || this.Brightness.Enabled || this.Contrast.Enabled);
			}
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0004084C File Offset: 0x0003EA4C
		public override void Render()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StandardPostProcess.BlurSettings blur = this.Blur;
			if (blur != null)
			{
				blur.Apply(base.Attributes);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StandardPostProcess.BrightnessSettings brightness = this.Brightness;
			if (brightness != null)
			{
				brightness.Apply(base.Attributes);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StandardPostProcess.ChromaticAberrationSettings chromaticAberration = this.ChromaticAberration;
			if (chromaticAberration != null)
			{
				chromaticAberration.Apply(base.Attributes);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StandardPostProcess.ColorOverlaySettings colorOverlay = this.ColorOverlay;
			if (colorOverlay != null)
			{
				colorOverlay.Apply(base.Attributes);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StandardPostProcess.ContrastSettings contrast = this.Contrast;
			if (contrast != null)
			{
				contrast.Apply(base.Attributes);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StandardPostProcess.DepthOfFieldSettings depthOfField = this.DepthOfField;
			if (depthOfField != null)
			{
				depthOfField.Apply(base.Attributes);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StandardPostProcess.FalloffSettings falloff = this.Falloff;
			if (falloff != null)
			{
				falloff.Apply(base.Attributes);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StandardPostProcess.FilmGrainSettings filmGrain = this.FilmGrain;
			if (filmGrain != null)
			{
				filmGrain.Apply(base.Attributes);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StandardPostProcess.HueRotateSettings hueRotate = this.HueRotate;
			if (hueRotate != null)
			{
				hueRotate.Apply(base.Attributes);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StandardPostProcess.LensDistortionSettings lensDistortion = this.LensDistortion;
			if (lensDistortion != null)
			{
				lensDistortion.Apply(base.Attributes);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StandardPostProcess.MotionBlurSettings motionBlur = this.MotionBlur;
			if (motionBlur != null)
			{
				motionBlur.Apply(base.Attributes);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StandardPostProcess.PaniniProjectionSettings paniniProjection = this.PaniniProjection;
			if (paniniProjection != null)
			{
				paniniProjection.Apply(base.Attributes);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StandardPostProcess.PixelateSettings pixelate = this.Pixelate;
			if (pixelate != null)
			{
				pixelate.Apply(base.Attributes);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StandardPostProcess.SaturateSettings saturate = this.Saturate;
			if (saturate != null)
			{
				saturate.Apply(base.Attributes);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StandardPostProcess.SharpenSettings sharpen = this.Sharpen;
			if (sharpen != null)
			{
				sharpen.Apply(base.Attributes);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StandardPostProcess.VignetteSettings vignette = this.Vignette;
			if (vignette != null)
			{
				vignette.Apply(base.Attributes);
			}
			if (GlobalGameNamespace.PostProcess.CurrentPass == PostProcessPass.Sdr)
			{
				int passCount = (this.HasPass1 ? 1 : 0) + (this.HasPass2 ? 1 : 0) + (this.HasPass3 ? 1 : 0);
				if (passCount == 0)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					base.Passthrough();
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				passCount--;
				if (this.HasPass1)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					Sandbox.Render.Material = this.PostProcessPass1;
					if (this.PaniniProjection.Enabled)
					{
						RenderAttributes attributes = Sandbox.Render.Attributes;
						string text = "CameraFOV";
						float num = CurrentView.FieldOfView.DegreeToRadian();
						attributes.Set(text, num);
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					base.RenderScreenQuad(false);
					if (passCount > 0)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						base.NextPass();
						RuntimeHelpers.EnsureSufficientExecutionStack();
						passCount--;
					}
				}
				if (this.HasPass2)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					Sandbox.Render.Material = this.PostProcessPass2;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					base.RenderScreenQuad(false);
					if (passCount > 0)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						base.NextPass();
						RuntimeHelpers.EnsureSufficientExecutionStack();
						passCount--;
					}
				}
				if (this.HasPass3)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					Sandbox.Render.Material = this.PostProcessPass3;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					base.RenderScreenQuad(false);
					return;
				}
			}
			else if (GlobalGameNamespace.PostProcess.CurrentPass == PostProcessPass.Hdr)
			{
				if (this.HasDof)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					Sandbox.Render.Material = this.PostProcessDof;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					RenderAttributes attributes2 = base.Attributes;
					string text = "D_DOF_PASS";
					int num2 = 0;
					attributes2.SetCombo(text, num2);
					using (base.ScopedRenderTarget())
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						Sandbox.Render.SetViewport(0, 0, (int)this.DofHalfResSize.x, (int)this.DofHalfResSize.y);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						Sandbox.Render.SetRenderTarget(this.DofDownscale);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						base.RenderScreenQuad(false);
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					RenderAttributes attributes3 = base.Attributes;
					text = "D_DOF_PASS";
					num2 = 1;
					attributes3.SetCombo(text, num2);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					RenderAttributes attributes4 = base.Attributes;
					text = "ColorDownscaleBuffer";
					num2 = -1;
					attributes4.Set(text, this.DofDownscale, num2);
					using (base.ScopedRenderTarget())
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						Sandbox.Render.SetViewport(0, 0, (int)this.LastScreenSize.x, (int)this.LastScreenSize.y);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						Sandbox.Render.SetRenderTarget(this.DofCocLut);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						base.RenderScreenQuad(false);
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					RenderAttributes attributes5 = base.Attributes;
					text = "CocLut";
					num2 = -1;
					attributes5.Set(text, this.DofCocLut, num2);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					RenderAttributes attributes6 = base.Attributes;
					text = "D_DOF_PASS";
					num2 = 3;
					attributes6.SetCombo(text, num2);
					using (base.ScopedRenderTarget())
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						Sandbox.Render.SetViewport(0, 0, (int)this.DofHalfResSize.x, (int)this.DofHalfResSize.y);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						Sandbox.Render.SetRenderTarget(this.DofDownscale2);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						base.RenderScreenQuad(true);
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					RenderAttributes attributes7 = base.Attributes;
					text = "ColorDownscaleBuffer";
					num2 = -1;
					attributes7.Set(text, this.DofDownscale2, num2);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					RenderAttributes attributes8 = base.Attributes;
					text = "D_DOF_PASS";
					num2 = 4;
					attributes8.SetCombo(text, num2);
					using (base.ScopedRenderTarget())
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						Sandbox.Render.SetViewport(0, 0, (int)this.DofHalfResSize.x, (int)this.DofHalfResSize.y);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						Sandbox.Render.SetRenderTarget(this.DofDownscale);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						base.RenderScreenQuad(true);
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					RenderAttributes attributes9 = base.Attributes;
					text = "D_DOF_PASS";
					num2 = 6;
					attributes9.SetCombo(text, num2);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					base.RenderScreenQuad(false);
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Passthrough();
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x00040DF8 File Offset: 0x0003EFF8
		[Description("Fall-Off allows you to adjust the distance of influence for most of the post processing effects. This is used as a modifier to adjust how much influence the post processing effect has based on a start and end distance. This allows you to fade a post-processing effect based on how far away a point is in the world. It's like depth of field but for all post processing effects.")]
		public StandardPostProcess.FalloffSettings Falloff { get; } = new StandardPostProcess.FalloffSettings();

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x00040E00 File Offset: 0x0003F000
		[Description("Pixelate lets you define how many pixels should be drawn on the screen at once. This is identical to scaling your screen down to a specific size and resizing it back to it's original use using a nearest neighbour filter.")]
		public StandardPostProcess.PixelateSettings Pixelate { get; } = new StandardPostProcess.PixelateSettings();

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x00040E08 File Offset: 0x0003F008
		[Description("Lens Distortion is used to mainly create a \"barrel\" distortion or a \"fish-eye\" effect. Both parameters control how the distorted the image becomes. This is done by making the lens a more concave or a more convex shape")]
		public StandardPostProcess.LensDistortionSettings LensDistortion { get; } = new StandardPostProcess.LensDistortionSettings();

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x00040E10 File Offset: 0x0003F010
		[Description("Panini Projection distorts the view similar to what lens distortion does, however it's distorted in a way to give the feel of having a wider field of view. The sides of the view is distorted to allow for this")]
		public StandardPostProcess.PaniniProjectionSettings PaniniProjection { get; } = new StandardPostProcess.PaniniProjectionSettings();

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001057 RID: 4183 RVA: 0x00040E18 File Offset: 0x0003F018
		[Description("Chromatic Aberration is how much the separate color channels should separate. As this is in UV space, the color channel separation should be extremely minimal (close to zero) values.")]
		public StandardPostProcess.ChromaticAberrationSettings ChromaticAberration { get; } = new StandardPostProcess.ChromaticAberrationSettings();

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001058 RID: 4184 RVA: 0x00040E20 File Offset: 0x0003F020
		[Description("Motion blur defines how much an object should blur(based on the current view) depending on how fast it's moving. The faster the user looks around, the more the objects will blur.")]
		public StandardPostProcess.MotionBlurSettings MotionBlur { get; } = new StandardPostProcess.MotionBlurSettings();

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001059 RID: 4185 RVA: 0x00040E28 File Offset: 0x0003F028
		[Description("Sharpen brings emphasis on edges and smaller details. As this is increased, the edges and details become more defined in the scene")]
		public StandardPostProcess.SharpenSettings Sharpen { get; } = new StandardPostProcess.SharpenSettings();

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x0600105A RID: 4186 RVA: 0x00040E30 File Offset: 0x0003F030
		[Description("Blur does a simple Gaussian blur on the current view.")]
		public StandardPostProcess.BlurSettings Blur { get; } = new StandardPostProcess.BlurSettings();

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x00040E38 File Offset: 0x0003F038
		[Description("Depth of field is used to emulate an actual camera with focus. It allows the user to keep objects in focus and adjust various other settings to emulate how a film camera works.")]
		public StandardPostProcess.DepthOfFieldSettings DepthOfField { get; } = new StandardPostProcess.DepthOfFieldSettings();

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x00040E40 File Offset: 0x0003F040
		[Description("Color Overlays allow tinting, mixing or completely changing the color of the current view. This allows for tinting how the world looks or implementation of such effects such as flashing")]
		public StandardPostProcess.ColorOverlaySettings ColorOverlay { get; } = new StandardPostProcess.ColorOverlaySettings();

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x00040E48 File Offset: 0x0003F048
		[Description("Saturation defines how saturated a view is. A saturation of 0 leads to a black and white view, whereas a saturation of 1 leads to the regular view.")]
		public StandardPostProcess.SaturateSettings Saturate { get; } = new StandardPostProcess.SaturateSettings();

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x00040E50 File Offset: 0x0003F050
		[Description("Film grain introduces noise to the screen. The noise can be customized to introduce more noise in darker areas. This allows the emulation of a camera sensor.")]
		public StandardPostProcess.FilmGrainSettings FilmGrain { get; } = new StandardPostProcess.FilmGrainSettings();

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x00040E58 File Offset: 0x0003F058
		[Description("This creates a vignette or a sort of black border around the edges of the screen. This can used to bring attention to the center of the frame")]
		public StandardPostProcess.VignetteSettings Vignette { get; } = new StandardPostProcess.VignetteSettings();

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x00040E60 File Offset: 0x0003F060
		[Description("Shift the hue of all the current colors on the screen. This is also known as hue shifting")]
		public StandardPostProcess.HueRotateSettings HueRotate { get; } = new StandardPostProcess.HueRotateSettings();

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x00040E68 File Offset: 0x0003F068
		[Description("Boosting of the brightness or dimming of the brightness of the screen")]
		public StandardPostProcess.BrightnessSettings Brightness { get; } = new StandardPostProcess.BrightnessSettings();

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x00040E70 File Offset: 0x0003F070
		[Description("Ability to increase or decrease the contrast of the screen")]
		public StandardPostProcess.ContrastSettings Contrast { get; } = new StandardPostProcess.ContrastSettings();

		// Token: 0x04000515 RID: 1301
		private Material PostProcessPass1;

		// Token: 0x04000516 RID: 1302
		private Material PostProcessPass2;

		// Token: 0x04000517 RID: 1303
		private Material PostProcessDof;

		// Token: 0x04000518 RID: 1304
		private Texture DofCocLut;

		// Token: 0x04000519 RID: 1305
		private Texture DofDownscale;

		// Token: 0x0400051A RID: 1306
		private Texture DofDownscale2;

		// Token: 0x0400051B RID: 1307
		private Vector2 LastScreenSize;

		// Token: 0x0400051C RID: 1308
		private Vector2 DofHalfResSize;

		// Token: 0x0400051D RID: 1309
		private Material PostProcessPass3;

		// Token: 0x0200021A RID: 538
		public class BlurSettings
		{
			// Token: 0x06001896 RID: 6294 RVA: 0x000666C0 File Offset: 0x000648C0
			internal void Apply(RenderAttributes attr)
			{
				bool shouldBlur = this.Enabled && this.Strength > 0f;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				string text = "D_BLUR";
				attr.SetCombo(text, shouldBlur);
				if (!shouldBlur)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.blur.falloff";
				float num = this.UseFalloff ? 0f : 1f;
				attr.Set(text, num);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.blur.size";
				num = this.Strength;
				attr.Set(text, num);
			}

			// Token: 0x170006B4 RID: 1716
			// (get) Token: 0x06001897 RID: 6295 RVA: 0x00066745 File Offset: 0x00064945
			// (set) Token: 0x06001898 RID: 6296 RVA: 0x0006674D File Offset: 0x0006494D
			[DefaultValue(false)]
			[Description("Enable blur post processing")]
			public bool Enabled { get; set; }

			// Token: 0x170006B5 RID: 1717
			// (get) Token: 0x06001899 RID: 6297 RVA: 0x00066756 File Offset: 0x00064956
			// (set) Token: 0x0600189A RID: 6298 RVA: 0x0006675E File Offset: 0x0006495E
			[DefaultValue(false)]
			[Description("Enable the usage of the post processing distance fall off for this post processing effect")]
			public bool UseFalloff { get; set; }

			// Token: 0x170006B6 RID: 1718
			// (get) Token: 0x0600189B RID: 6299 RVA: 0x00066767 File Offset: 0x00064967
			// (set) Token: 0x0600189C RID: 6300 RVA: 0x0006676F File Offset: 0x0006496F
			[DefaultValue(0.5f)]
			[Description("Set how strong/the size of the blur is. Range 0-&gt;1")]
			public float Strength { get; set; } = 0.5f;
		}

		// Token: 0x0200021B RID: 539
		public class BrightnessSettings
		{
			// Token: 0x0600189E RID: 6302 RVA: 0x0006678C File Offset: 0x0006498C
			internal void Apply(RenderAttributes attr)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				string text = "D_BRIGHTNESS";
				bool enabled = this.Enabled;
				attr.SetCombo(text, enabled);
				if (!this.Enabled)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.brightness.multiplier";
				float num = this.Multiplier;
				attr.Set(text, num);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.brightness.falloff";
				num = (this.UseFalloff ? 0f : 1f);
				attr.Set(text, num);
			}

			// Token: 0x170006B7 RID: 1719
			// (get) Token: 0x0600189F RID: 6303 RVA: 0x00066804 File Offset: 0x00064A04
			// (set) Token: 0x060018A0 RID: 6304 RVA: 0x0006680C File Offset: 0x00064A0C
			[DefaultValue(false)]
			[Description("Enables brightness post processing effect")]
			public bool Enabled { get; set; }

			// Token: 0x170006B8 RID: 1720
			// (get) Token: 0x060018A1 RID: 6305 RVA: 0x00066815 File Offset: 0x00064A15
			// (set) Token: 0x060018A2 RID: 6306 RVA: 0x0006681D File Offset: 0x00064A1D
			[DefaultValue(false)]
			[Description("Enable the usage of the post processing distance fall off for this post processing effect")]
			public bool UseFalloff { get; set; }

			// Token: 0x170006B9 RID: 1721
			// (get) Token: 0x060018A3 RID: 6307 RVA: 0x00066826 File Offset: 0x00064A26
			// (set) Token: 0x060018A4 RID: 6308 RVA: 0x0006682E File Offset: 0x00064A2E
			[DefaultValue(0f)]
			[Description("How much to rotate the hue by, this is a value from 0-&gt;n. The default value for this is 1.0f which means no change")]
			public float Multiplier { get; set; }
		}

		// Token: 0x0200021C RID: 540
		public class ChromaticAberrationSettings
		{
			// Token: 0x060018A6 RID: 6310 RVA: 0x00066840 File Offset: 0x00064A40
			internal void Apply(RenderAttributes attr)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				string text = "D_CHROMATIC_ABERRATION";
				bool enabled = this.Enabled;
				attr.SetCombo(text, enabled);
				if (!this.Enabled)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.chromaticaberration.amount";
				Vector3 offset = this.Offset;
				attr.Set(text, offset);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.chromaticaberration.falloff";
				float num = this.UseFalloff ? 0f : 1f;
				attr.Set(text, num);
			}

			// Token: 0x170006BA RID: 1722
			// (get) Token: 0x060018A7 RID: 6311 RVA: 0x000668B8 File Offset: 0x00064AB8
			// (set) Token: 0x060018A8 RID: 6312 RVA: 0x000668C0 File Offset: 0x00064AC0
			[DefaultValue(false)]
			[Description("Enable chromatic aberration")]
			public bool Enabled { get; set; }

			// Token: 0x170006BB RID: 1723
			// (get) Token: 0x060018A9 RID: 6313 RVA: 0x000668C9 File Offset: 0x00064AC9
			// (set) Token: 0x060018AA RID: 6314 RVA: 0x000668D1 File Offset: 0x00064AD1
			[DefaultValue(false)]
			[Description("Enable the usage of the post processing distance fall off for this post processing effect")]
			public bool UseFalloff { get; set; }

			// Token: 0x170006BC RID: 1724
			// (get) Token: 0x060018AB RID: 6315 RVA: 0x000668DA File Offset: 0x00064ADA
			// (set) Token: 0x060018AC RID: 6316 RVA: 0x000668E2 File Offset: 0x00064AE2
			[Description("The pixel offset for each color channel. These values should be very small as it's in UV space. (0.004 for example) X = Red Y = Green Z = Blue")]
			public Vector3 Offset { get; set; } = new Vector3(0.004f, 0.006f, 0f);
		}

		// Token: 0x0200021D RID: 541
		public class ColorOverlaySettings
		{
			// Token: 0x060018AE RID: 6318 RVA: 0x00066910 File Offset: 0x00064B10
			internal void Apply(RenderAttributes attr)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				string text = "D_COLOR_OVERLAY";
				int num = (int)(this.Enabled ? ((byte)this.Mode) : 0);
				attr.SetCombo(text, num);
				if (!this.Enabled)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.coloroverlay.color";
				Color color = this.Color;
				Vector4 vector = color;
				attr.Set(text, vector);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.coloroverlay.amount";
				float num2 = this.Amount;
				attr.Set(text, num2);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.coloroverlay.falloff";
				num2 = (this.UseFalloff ? 0f : 1f);
				attr.Set(text, num2);
			}

			// Token: 0x170006BD RID: 1725
			// (get) Token: 0x060018AF RID: 6319 RVA: 0x000669BA File Offset: 0x00064BBA
			// (set) Token: 0x060018B0 RID: 6320 RVA: 0x000669C2 File Offset: 0x00064BC2
			[DefaultValue(false)]
			[Description("Enable the color overlay")]
			public bool Enabled { get; set; }

			// Token: 0x170006BE RID: 1726
			// (get) Token: 0x060018B1 RID: 6321 RVA: 0x000669CB File Offset: 0x00064BCB
			// (set) Token: 0x060018B2 RID: 6322 RVA: 0x000669D3 File Offset: 0x00064BD3
			[DefaultValue(false)]
			[Description("Enable the usage of the post processing distance fall off for this post processing effect")]
			public bool UseFalloff { get; set; }

			// Token: 0x170006BF RID: 1727
			// (get) Token: 0x060018B3 RID: 6323 RVA: 0x000669DC File Offset: 0x00064BDC
			// (set) Token: 0x060018B4 RID: 6324 RVA: 0x000669E4 File Offset: 0x00064BE4
			[DefaultValue(StandardPostProcess.ColorOverlaySettings.OverlayMode.Additive)]
			[Description("Determine which color overlay we should use")]
			public StandardPostProcess.ColorOverlaySettings.OverlayMode Mode { get; set; } = StandardPostProcess.ColorOverlaySettings.OverlayMode.Additive;

			// Token: 0x170006C0 RID: 1728
			// (get) Token: 0x060018B5 RID: 6325 RVA: 0x000669ED File Offset: 0x00064BED
			// (set) Token: 0x060018B6 RID: 6326 RVA: 0x000669F5 File Offset: 0x00064BF5
			[Description("The color which should be used with the color overlay")]
			public Color Color { get; set; } = Color.White;

			// Token: 0x170006C1 RID: 1729
			// (get) Token: 0x060018B7 RID: 6327 RVA: 0x000669FE File Offset: 0x00064BFE
			// (set) Token: 0x060018B8 RID: 6328 RVA: 0x00066A06 File Offset: 0x00064C06
			[DefaultValue(1f)]
			[Description("How much the color overlays influence is. This is a value between 0.0f -&gt; 1.0f")]
			public float Amount { get; set; } = 1f;

			// Token: 0x02000273 RID: 627
			public enum OverlayMode
			{
				// Token: 0x04000A30 RID: 2608
				Additive = 1,
				// Token: 0x04000A31 RID: 2609
				Multiply,
				// Token: 0x04000A32 RID: 2610
				Mix
			}
		}

		// Token: 0x0200021E RID: 542
		public class ContrastSettings
		{
			// Token: 0x060018BA RID: 6330 RVA: 0x00066A34 File Offset: 0x00064C34
			internal void Apply(RenderAttributes attr)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				string text = "D_CONTRAST";
				bool enabled = this.Enabled;
				attr.SetCombo(text, enabled);
				if (!this.Enabled)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.contrast.contrast";
				float num = this.Contrast;
				attr.Set(text, num);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.contrast.falloff";
				num = (this.UseFalloff ? 0f : 1f);
				attr.Set(text, num);
			}

			// Token: 0x170006C2 RID: 1730
			// (get) Token: 0x060018BB RID: 6331 RVA: 0x00066AAC File Offset: 0x00064CAC
			// (set) Token: 0x060018BC RID: 6332 RVA: 0x00066AB4 File Offset: 0x00064CB4
			[DefaultValue(false)]
			[Description("Enables the contrast post processing effect")]
			public bool Enabled { get; set; }

			// Token: 0x170006C3 RID: 1731
			// (get) Token: 0x060018BD RID: 6333 RVA: 0x00066ABD File Offset: 0x00064CBD
			// (set) Token: 0x060018BE RID: 6334 RVA: 0x00066AC5 File Offset: 0x00064CC5
			[DefaultValue(false)]
			[Description("Enable the usage of the post processing distance fall off for this post processing effect")]
			public bool UseFalloff { get; set; }

			// Token: 0x170006C4 RID: 1732
			// (get) Token: 0x060018BF RID: 6335 RVA: 0x00066ACE File Offset: 0x00064CCE
			// (set) Token: 0x060018C0 RID: 6336 RVA: 0x00066AD6 File Offset: 0x00064CD6
			[DefaultValue(1f)]
			[Description("How much contrast the screen should have? The default value for this is 1.0f which means no change")]
			public float Contrast { get; set; } = 1f;
		}

		// Token: 0x0200021F RID: 543
		public class DepthOfFieldSettings
		{
			// Token: 0x060018C2 RID: 6338 RVA: 0x00066AF4 File Offset: 0x00064CF4
			internal void Apply(RenderAttributes attr)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				string text = "D_DOF";
				bool flag = this.Enabled;
				attr.SetCombo(text, flag);
				if (!this.Enabled)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "D_DOF_BLUR_COC";
				flag = this.BlurCircleOfConfusion;
				attr.SetCombo(text, flag);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "D_DOF_COC_IGNORE_ELIPSON";
				flag = this.BleedFocusEdge;
				attr.SetCombo(text, flag);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "D_DOF_COLOR_BLUR";
				int num = (int)((byte)this.BlurColoringMode);
				attr.SetCombo(text, num);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "D_DOF_KERNEL";
				num = (int)this.calculatedKernelSize;
				attr.SetCombo(text, num);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.dof.lens";
				attr.Set(text, this.calculatedLens);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.dof.focusplane";
				attr.Set(text, this.calculatedFocusPlane);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.dof.blurcolor";
				Color blurColor = this.BlurColor;
				Vector4 vector = blurColor;
				attr.Set(text, vector);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.dof.radius";
				attr.Set(text, this.calculatedRadius);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.dof.maxcoc";
				attr.Set(text, this.calculatedMaxCoC);
			}

			// Token: 0x170006C5 RID: 1733
			// (get) Token: 0x060018C3 RID: 6339 RVA: 0x00066C20 File Offset: 0x00064E20
			// (set) Token: 0x060018C4 RID: 6340 RVA: 0x00066C28 File Offset: 0x00064E28
			[DefaultValue(false)]
			public bool Enabled { get; set; }

			// Token: 0x170006C6 RID: 1734
			// (get) Token: 0x060018C6 RID: 6342 RVA: 0x00066C4A File Offset: 0x00064E4A
			// (set) Token: 0x060018C5 RID: 6341 RVA: 0x00066C31 File Offset: 0x00064E31
			[Description("Defines the current camera sensors width in mm. By default we have this as 35mm")]
			public float FilmWidth
			{
				get
				{
					return this.filmWidth;
				}
				set
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.filmWidth = value;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.UpdateFilmWidth();
				}
			}

			// Token: 0x170006C7 RID: 1735
			// (get) Token: 0x060018C7 RID: 6343 RVA: 0x00066C52 File Offset: 0x00064E52
			// (set) Token: 0x060018C8 RID: 6344 RVA: 0x00066C5A File Offset: 0x00064E5A
			[DefaultValue(false)]
			[Description("Provide additional blur on our circle of confusion to remove harsh transitions from in focus objects to out of focus objects")]
			public bool BlurCircleOfConfusion { get; set; }

			// Token: 0x170006C8 RID: 1736
			// (get) Token: 0x060018C9 RID: 6345 RVA: 0x00066C63 File Offset: 0x00064E63
			// (set) Token: 0x060018CA RID: 6346 RVA: 0x00066C6B File Offset: 0x00064E6B
			[DefaultValue(false)]
			[Description("Allow in focus objects to bleed their colors into out of focus objects")]
			public bool BleedFocusEdge { get; set; }

			// Token: 0x170006C9 RID: 1737
			// (get) Token: 0x060018CB RID: 6347 RVA: 0x00066C74 File Offset: 0x00064E74
			// (set) Token: 0x060018CC RID: 6348 RVA: 0x00066C7C File Offset: 0x00064E7C
			[DefaultValue(StandardPostProcess.DepthOfFieldSettings.BlurColorMode.None)]
			[Description("Ability to color the scene for what's out of focus. This is meant to give a more stylized look to your scenes.")]
			public StandardPostProcess.DepthOfFieldSettings.BlurColorMode BlurColoringMode { get; set; }

			// Token: 0x170006CA RID: 1738
			// (get) Token: 0x060018CD RID: 6349 RVA: 0x00066C85 File Offset: 0x00064E85
			// (set) Token: 0x060018CE RID: 6350 RVA: 0x00066C8D File Offset: 0x00064E8D
			[Description("Sets the out of focus color, this only works if the BlurColoringMode is set to BlurColorMode.Colorize")]
			public Color BlurColor { get; set; } = Color.White;

			// Token: 0x170006CB RID: 1739
			// (get) Token: 0x060018D0 RID: 6352 RVA: 0x00066CAF File Offset: 0x00064EAF
			// (set) Token: 0x060018CF RID: 6351 RVA: 0x00066C96 File Offset: 0x00064E96
			[Description("Define how much we should blur what's out of focus")]
			public float Radius
			{
				get
				{
					return this.calculatedRadius;
				}
				set
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.calculatedRadius = value;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.UpdateMaxCoC();
				}
			}

			// Token: 0x170006CC RID: 1740
			// (get) Token: 0x060018D2 RID: 6354 RVA: 0x00066CD5 File Offset: 0x00064ED5
			// (set) Token: 0x060018D1 RID: 6353 RVA: 0x00066CB7 File Offset: 0x00064EB7
			[Description("Define where our point of focus is. This is in engine units or inches")]
			public float FocalPoint
			{
				get
				{
					return this.calculatedFocalPoint;
				}
				set
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.calculatedFocalPoint = value.InchToMilimeter();
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.UpdateLens();
				}
			}

			// Token: 0x170006CD RID: 1741
			// (get) Token: 0x060018D4 RID: 6356 RVA: 0x00066CF6 File Offset: 0x00064EF6
			// (set) Token: 0x060018D3 RID: 6355 RVA: 0x00066CDD File Offset: 0x00064EDD
			[Description("Define what our current F-Stop/Aperture size is")]
			public float FStop
			{
				get
				{
					return this.calculatedFocalPoint;
				}
				set
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.calculatedFStop = value;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.UpdateLens();
				}
			}

			// Token: 0x170006CE RID: 1742
			// (get) Token: 0x060018D6 RID: 6358 RVA: 0x00066D1C File Offset: 0x00064F1C
			// (set) Token: 0x060018D5 RID: 6357 RVA: 0x00066CFE File Offset: 0x00064EFE
			[Description("Define our current focal length, this is in engine units or inches")]
			public float FocalLength
			{
				get
				{
					return this.calculatedFocalLength;
				}
				set
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.calculatedFocalLength = value.InchToMilimeter();
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.UpdateLens();
				}
			}

			// Token: 0x060018D7 RID: 6359 RVA: 0x00066D24 File Offset: 0x00064F24
			[Event.Screen.SizeChangedAttribute]
			private void UpdateFilmWidth()
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.calculatedFilmHeight = this.filmWidth * 1.5f * (Screen.Height / 1080f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.UpdateLens();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.UpdateMaxCoC();
			}

			// Token: 0x060018D8 RID: 6360 RVA: 0x00066D60 File Offset: 0x00064F60
			private void UpdateLens()
			{
				float focusPlane = Math.Max(this.calculatedFocalPoint, this.calculatedFocalLength);
				float lens = this.calculatedFocalLength * this.calculatedFocalLength / (this.calculatedFStop * (focusPlane - this.calculatedFocalLength) * this.calculatedFilmHeight * 2f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.calculatedLens = lens;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.calculatedFocusPlane = focusPlane;
			}

			// Token: 0x060018D9 RID: 6361 RVA: 0x00066DC4 File Offset: 0x00064FC4
			private void UpdateMaxCoC()
			{
				byte kernelSize = 0;
				if (this.calculatedRadius > 1f)
				{
					kernelSize = 1;
				}
				if (this.calculatedRadius > 4f)
				{
					kernelSize = 2;
				}
				if (this.calculatedRadius > 6f)
				{
					kernelSize = 3;
				}
				float num = (kernelSize <= 2) ? (1f + (float)kernelSize) : this.calculatedRadius;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.calculatedKernelSize = kernelSize;
				float blurRadius = num * 4f * 6f;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.calculatedMaxCoC = MathF.Min(0.05f, blurRadius / Screen.Height);
			}

			// Token: 0x040008CF RID: 2255
			private const float FilmAspect = 1.5f;

			// Token: 0x040008D0 RID: 2256
			private float filmWidth = 0.024f;

			// Token: 0x040008D1 RID: 2257
			private float calculatedFilmHeight = 0.024f;

			// Token: 0x040008D2 RID: 2258
			private float calculatedFocalLength = 0.05f;

			// Token: 0x040008D3 RID: 2259
			private float calculatedFStop = 5.6f;

			// Token: 0x040008D4 RID: 2260
			private float calculatedFocalPoint = 64f.InchToMilimeter();

			// Token: 0x040008D5 RID: 2261
			private float calculatedRadius;

			// Token: 0x040008D6 RID: 2262
			private byte calculatedKernelSize;

			// Token: 0x040008D7 RID: 2263
			private float calculatedMaxCoC;

			// Token: 0x040008D8 RID: 2264
			private float calculatedLens;

			// Token: 0x040008D9 RID: 2265
			private float calculatedFocusPlane;

			// Token: 0x02000274 RID: 628
			public enum BlurColorMode
			{
				// Token: 0x04000A34 RID: 2612
				None,
				// Token: 0x04000A35 RID: 2613
				Colorize,
				// Token: 0x04000A36 RID: 2614
				Grayscale
			}
		}

		// Token: 0x02000220 RID: 544
		public class FalloffSettings
		{
			// Token: 0x060018DB RID: 6363 RVA: 0x00066EA8 File Offset: 0x000650A8
			internal void Apply(RenderAttributes attr)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				string text = "D_FALLOFF";
				bool enabled = this.Enabled;
				attr.SetCombo(text, enabled);
				if (!this.Enabled)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.falloff.start";
				float num = this.StartDistance;
				attr.Set(text, num);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.falloff.end";
				num = this.EndDistance;
				attr.Set(text, num);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.falloff.exponent";
				num = this.Exponent;
				attr.Set(text, num);
			}

			// Token: 0x170006CF RID: 1743
			// (get) Token: 0x060018DC RID: 6364 RVA: 0x00066F2E File Offset: 0x0006512E
			// (set) Token: 0x060018DD RID: 6365 RVA: 0x00066F36 File Offset: 0x00065136
			[DefaultValue(false)]
			[Description("Enables distance based fall off on all of the post processing effects")]
			public bool Enabled { get; set; }

			// Token: 0x170006D0 RID: 1744
			// (get) Token: 0x060018DE RID: 6366 RVA: 0x00066F3F File Offset: 0x0006513F
			// (set) Token: 0x060018DF RID: 6367 RVA: 0x00066F47 File Offset: 0x00065147
			[DefaultValue(0f)]
			[Description("The distance(in units) on where the post processing effect should begin")]
			public float StartDistance { get; set; }

			// Token: 0x170006D1 RID: 1745
			// (get) Token: 0x060018E0 RID: 6368 RVA: 0x00066F50 File Offset: 0x00065150
			// (set) Token: 0x060018E1 RID: 6369 RVA: 0x00066F58 File Offset: 0x00065158
			[DefaultValue(1024f)]
			[Description("The distance(in units) on how far the post processing effect should be before it reaches full intensity")]
			public float EndDistance { get; set; } = 1024f;

			// Token: 0x170006D2 RID: 1746
			// (get) Token: 0x060018E2 RID: 6370 RVA: 0x00066F61 File Offset: 0x00065161
			// (set) Token: 0x060018E3 RID: 6371 RVA: 0x00066F69 File Offset: 0x00065169
			[DefaultValue(2f)]
			[Description("The fall off exponent to adjust how quickly the falloff should happen A range from 0.001 -&gt; 8 seems to work best")]
			public float Exponent { get; set; } = 2f;
		}

		// Token: 0x02000221 RID: 545
		public class FilmGrainSettings
		{
			// Token: 0x060018E5 RID: 6373 RVA: 0x00066F90 File Offset: 0x00065190
			internal void Apply(RenderAttributes attr)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				string text = "D_FILM_GRAIN";
				bool enabled = this.Enabled;
				attr.SetCombo(text, enabled);
				if (!this.Enabled)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.grain.intensity";
				float num = this.Intensity;
				attr.Set(text, num);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.grain.response";
				num = this.Response;
				attr.Set(text, num);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.grain.falloff";
				num = (this.UseFalloff ? 0f : 1f);
				attr.Set(text, num);
			}

			// Token: 0x170006D3 RID: 1747
			// (get) Token: 0x060018E6 RID: 6374 RVA: 0x00067024 File Offset: 0x00065224
			// (set) Token: 0x060018E7 RID: 6375 RVA: 0x0006702C File Offset: 0x0006522C
			[DefaultValue(false)]
			[Description("Enable the grain post processing")]
			public bool Enabled { get; set; }

			// Token: 0x170006D4 RID: 1748
			// (get) Token: 0x060018E8 RID: 6376 RVA: 0x00067035 File Offset: 0x00065235
			// (set) Token: 0x060018E9 RID: 6377 RVA: 0x0006703D File Offset: 0x0006523D
			[DefaultValue(false)]
			[Description("Enable the usage of the post processing distance fall off for this post processing effect")]
			public bool UseFalloff { get; set; }

			// Token: 0x170006D5 RID: 1749
			// (get) Token: 0x060018EA RID: 6378 RVA: 0x00067046 File Offset: 0x00065246
			// (set) Token: 0x060018EB RID: 6379 RVA: 0x0006704E File Offset: 0x0006524E
			[DefaultValue(0.5f)]
			[Description("How intense the grain output should be. This is a value from 0-&gt;1")]
			public float Intensity { get; set; } = 0.5f;

			// Token: 0x170006D6 RID: 1750
			// (get) Token: 0x060018EC RID: 6380 RVA: 0x00067057 File Offset: 0x00065257
			// (set) Token: 0x060018ED RID: 6381 RVA: 0x0006705F File Offset: 0x0006525F
			[DefaultValue(0f)]
			[Description("How responsive the grain should be to light values. A result of 0 will lead to the grain being very responsive everywhere whereas a value of 1 will lead to the grain only being responsive in darker areas. The range is from 0-&gt;1")]
			public float Response { get; set; }
		}

		// Token: 0x02000222 RID: 546
		public class HueRotateSettings
		{
			// Token: 0x060018EF RID: 6383 RVA: 0x0006707C File Offset: 0x0006527C
			internal void Apply(RenderAttributes attr)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				string text = "D_HUE_ROTATE";
				bool enabled = this.Enabled;
				attr.SetCombo(text, enabled);
				if (!this.Enabled)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.hue_rotate.angle";
				float num = this.Angle;
				attr.Set(text, num);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.hue_rotate.falloff";
				num = (this.UseFalloff ? 0f : 1f);
				attr.Set(text, num);
			}

			// Token: 0x170006D7 RID: 1751
			// (get) Token: 0x060018F0 RID: 6384 RVA: 0x000670F4 File Offset: 0x000652F4
			// (set) Token: 0x060018F1 RID: 6385 RVA: 0x000670FC File Offset: 0x000652FC
			[DefaultValue(false)]
			[Description("Enables hue rotation post processing")]
			public bool Enabled { get; set; }

			// Token: 0x170006D8 RID: 1752
			// (get) Token: 0x060018F2 RID: 6386 RVA: 0x00067105 File Offset: 0x00065305
			// (set) Token: 0x060018F3 RID: 6387 RVA: 0x0006710D File Offset: 0x0006530D
			[DefaultValue(false)]
			[Description("Enable the usage of the post processing distance fall off for this post processing effect")]
			public bool UseFalloff { get; set; }

			// Token: 0x170006D9 RID: 1753
			// (get) Token: 0x060018F4 RID: 6388 RVA: 0x00067116 File Offset: 0x00065316
			// (set) Token: 0x060018F5 RID: 6389 RVA: 0x0006711E File Offset: 0x0006531E
			[DefaultValue(0f)]
			[Description("How much to rotate the hue by, this is a value from 0-&gt;360. The default value for this is 0.0f which means no change")]
			public float Angle { get; set; }
		}

		// Token: 0x02000223 RID: 547
		public class LensDistortionSettings
		{
			// Token: 0x060018F7 RID: 6391 RVA: 0x00067130 File Offset: 0x00065330
			internal void Apply(RenderAttributes attr)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				string text = "D_LENS_DISTORTION";
				bool enabled = this.Enabled;
				attr.SetCombo(text, enabled);
				if (!this.Enabled)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.lensdistortion.k1";
				float num = this.K1;
				attr.Set(text, num);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.lensdistortion.k2";
				num = this.K2;
				attr.Set(text, num);
			}

			// Token: 0x170006DA RID: 1754
			// (get) Token: 0x060018F8 RID: 6392 RVA: 0x0006719A File Offset: 0x0006539A
			// (set) Token: 0x060018F9 RID: 6393 RVA: 0x000671A2 File Offset: 0x000653A2
			[DefaultValue(false)]
			public bool Enabled { get; set; }

			// Token: 0x170006DB RID: 1755
			// (get) Token: 0x060018FA RID: 6394 RVA: 0x000671AB File Offset: 0x000653AB
			// (set) Token: 0x060018FB RID: 6395 RVA: 0x000671B3 File Offset: 0x000653B3
			[DefaultValue(0f)]
			[Description("Lens distortion multiplier 1 The range should be from -1.0 -&gt; 1.0 Positive values = More Convex Negative values = More Concave")]
			public float K1 { get; set; }

			// Token: 0x170006DC RID: 1756
			// (get) Token: 0x060018FC RID: 6396 RVA: 0x000671BC File Offset: 0x000653BC
			// (set) Token: 0x060018FD RID: 6397 RVA: 0x000671C4 File Offset: 0x000653C4
			[DefaultValue(0f)]
			[Description("Lens distortion multiplier 2. The range should be from -1.0 -&gt; 1.0 Positive values = More Convex Negative values = More Concave")]
			public float K2 { get; set; }
		}

		// Token: 0x02000224 RID: 548
		public class MotionBlurSettings
		{
			// Token: 0x060018FF RID: 6399 RVA: 0x000671D8 File Offset: 0x000653D8
			internal void Apply(RenderAttributes attr)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				string text = "D_MOTION_BLUR";
				bool enabled = this.Enabled;
				attr.SetCombo(text, enabled);
				if (!this.Enabled || this.Samples <= 0)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.motionblur.scale";
				float num = this.Scale;
				attr.Set(text, num);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.motionblur.samples";
				int samples = this.Samples;
				attr.Set(text, samples);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.motionblur.falloff";
				num = (this.UseFalloff ? 0f : 1f);
				attr.Set(text, num);
			}

			// Token: 0x170006DD RID: 1757
			// (get) Token: 0x06001900 RID: 6400 RVA: 0x00067275 File Offset: 0x00065475
			// (set) Token: 0x06001901 RID: 6401 RVA: 0x0006727D File Offset: 0x0006547D
			[DefaultValue(false)]
			[Description("Enable motion blur")]
			public bool Enabled { get; set; }

			// Token: 0x170006DE RID: 1758
			// (get) Token: 0x06001902 RID: 6402 RVA: 0x00067286 File Offset: 0x00065486
			// (set) Token: 0x06001903 RID: 6403 RVA: 0x0006728E File Offset: 0x0006548E
			[DefaultValue(false)]
			[Description("Enable the usage of the post processing distance fall off for this post processing effect")]
			public bool UseFalloff { get; set; }

			// Token: 0x170006DF RID: 1759
			// (get) Token: 0x06001904 RID: 6404 RVA: 0x00067297 File Offset: 0x00065497
			// (set) Token: 0x06001905 RID: 6405 RVA: 0x0006729F File Offset: 0x0006549F
			[DefaultValue(0.05f)]
			[Description("How strong the motion blur should be. Smaller values seem to work better here, default value is ~0.05f")]
			public float Scale { get; set; } = 0.05f;

			// Token: 0x170006E0 RID: 1760
			// (get) Token: 0x06001906 RID: 6406 RVA: 0x000672A8 File Offset: 0x000654A8
			// (set) Token: 0x06001907 RID: 6407 RVA: 0x000672B0 File Offset: 0x000654B0
			[DefaultValue(16)]
			[Description("How many blur samples we should take. The higher this value is the more expensive it is. This value determines how smooth the blur will be")]
			public int Samples { get; set; } = 16;
		}

		// Token: 0x02000225 RID: 549
		public class PaniniProjectionSettings
		{
			// Token: 0x06001909 RID: 6409 RVA: 0x000672D4 File Offset: 0x000654D4
			internal void Apply(RenderAttributes attr)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				string text = "D_PANINI_PROJECTION";
				bool enabled = this.Enabled;
				attr.SetCombo(text, enabled);
				if (!this.Enabled)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.paniniprojection.amount";
				float num = this.Amount;
				attr.Set(text, num);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.paniniprojection.crop";
				num = this.Crop;
				attr.Set(text, num);
			}

			// Token: 0x170006E1 RID: 1761
			// (get) Token: 0x0600190A RID: 6410 RVA: 0x0006733E File Offset: 0x0006553E
			// (set) Token: 0x0600190B RID: 6411 RVA: 0x00067346 File Offset: 0x00065546
			[DefaultValue(false)]
			[Description("Enable Panini Projection")]
			public bool Enabled { get; set; }

			// Token: 0x170006E2 RID: 1762
			// (get) Token: 0x0600190C RID: 6412 RVA: 0x0006734F File Offset: 0x0006554F
			// (set) Token: 0x0600190D RID: 6413 RVA: 0x00067357 File Offset: 0x00065557
			[DefaultValue(0f)]
			[Description("How intense the panini projection should be? A range from 0-&gt;1 seems to work best here")]
			public float Amount { get; set; }

			// Token: 0x170006E3 RID: 1763
			// (get) Token: 0x0600190E RID: 6414 RVA: 0x00067360 File Offset: 0x00065560
			// (set) Token: 0x0600190F RID: 6415 RVA: 0x00067368 File Offset: 0x00065568
			[DefaultValue(1f)]
			[Description("How much of the excess view should be cropped Range 0-&gt;1")]
			public float Crop { get; set; } = 1f;
		}

		// Token: 0x02000226 RID: 550
		public class PixelateSettings
		{
			// Token: 0x06001911 RID: 6417 RVA: 0x00067384 File Offset: 0x00065584
			internal void Apply(RenderAttributes attr)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				string text = "D_PIXELATE";
				bool enabled = this.Enabled;
				attr.SetCombo(text, enabled);
				if (!this.Enabled)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.pixelate.pixel_count";
				Vector2 pixelCount = this.PixelCount;
				attr.Set(text, pixelCount);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.pixelate.falloff";
				float num = this.UseFalloff ? 0f : 1f;
				attr.Set(text, num);
			}

			// Token: 0x170006E4 RID: 1764
			// (get) Token: 0x06001912 RID: 6418 RVA: 0x000673FC File Offset: 0x000655FC
			// (set) Token: 0x06001913 RID: 6419 RVA: 0x00067404 File Offset: 0x00065604
			[DefaultValue(false)]
			[Description("Enable pixelation post processing")]
			public bool Enabled { get; set; }

			// Token: 0x170006E5 RID: 1765
			// (get) Token: 0x06001914 RID: 6420 RVA: 0x0006740D File Offset: 0x0006560D
			// (set) Token: 0x06001915 RID: 6421 RVA: 0x00067415 File Offset: 0x00065615
			[DefaultValue(false)]
			[Description("Enable the usage of the post processing distance fall off for this post processing effect")]
			public bool UseFalloff { get; set; }

			// Token: 0x170006E6 RID: 1766
			// (get) Token: 0x06001916 RID: 6422 RVA: 0x0006741E File Offset: 0x0006561E
			// (set) Token: 0x06001917 RID: 6423 RVA: 0x00067426 File Offset: 0x00065626
			[Description("Amount of pixels on the x/y axis of the screen.")]
			public Vector2 PixelCount { get; set; } = new Vector2(640f, 480f);
		}

		// Token: 0x02000227 RID: 551
		public class SaturateSettings
		{
			// Token: 0x06001919 RID: 6425 RVA: 0x0006744C File Offset: 0x0006564C
			internal void Apply(RenderAttributes attr)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				string text = "D_SATURATE";
				bool enabled = this.Enabled;
				attr.SetCombo(text, enabled);
				if (!this.Enabled)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.saturate.amount";
				float num = this.Amount;
				attr.Set(text, num);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.saturate.falloff";
				num = (this.UseFalloff ? 0f : 1f);
				attr.Set(text, num);
			}

			// Token: 0x170006E7 RID: 1767
			// (get) Token: 0x0600191A RID: 6426 RVA: 0x000674C4 File Offset: 0x000656C4
			// (set) Token: 0x0600191B RID: 6427 RVA: 0x000674CC File Offset: 0x000656CC
			[DefaultValue(false)]
			[Description("Enable the saturation post processing effect")]
			public bool Enabled { get; set; }

			// Token: 0x170006E8 RID: 1768
			// (get) Token: 0x0600191C RID: 6428 RVA: 0x000674D5 File Offset: 0x000656D5
			// (set) Token: 0x0600191D RID: 6429 RVA: 0x000674DD File Offset: 0x000656DD
			[DefaultValue(false)]
			[Description("Enable the usage of the post processing distance fall off for this post processing effect")]
			public bool UseFalloff { get; set; }

			// Token: 0x170006E9 RID: 1769
			// (get) Token: 0x0600191E RID: 6430 RVA: 0x000674E6 File Offset: 0x000656E6
			// (set) Token: 0x0600191F RID: 6431 RVA: 0x000674EE File Offset: 0x000656EE
			[DefaultValue(1f)]
			[Description("How saturated our view should be. A value of  0 is a grayscale result whereas 1 is no change. Any value greater than 1 will lead to an over-saturated or \"deepfried\" look")]
			public float Amount { get; set; } = 1f;
		}

		// Token: 0x02000228 RID: 552
		public class SharpenSettings
		{
			// Token: 0x06001921 RID: 6433 RVA: 0x0006750C File Offset: 0x0006570C
			internal void Apply(RenderAttributes attr)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				string text = "D_SHARPEN";
				bool enabled = this.Enabled;
				attr.SetCombo(text, enabled);
				if (!this.Enabled)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.sharpen.strength";
				float num = this.Strength;
				attr.Set(text, num);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.sharpen.falloff";
				num = (this.UseFalloff ? 0f : 1f);
				attr.Set(text, num);
			}

			// Token: 0x170006EA RID: 1770
			// (get) Token: 0x06001922 RID: 6434 RVA: 0x00067584 File Offset: 0x00065784
			// (set) Token: 0x06001923 RID: 6435 RVA: 0x0006758C File Offset: 0x0006578C
			[DefaultValue(false)]
			[Description("Enable sharpen post processing")]
			public bool Enabled { get; set; }

			// Token: 0x170006EB RID: 1771
			// (get) Token: 0x06001924 RID: 6436 RVA: 0x00067595 File Offset: 0x00065795
			// (set) Token: 0x06001925 RID: 6437 RVA: 0x0006759D File Offset: 0x0006579D
			[DefaultValue(false)]
			[Description("Enable the usage of the post processing distance fall off for this post processing effect")]
			public bool UseFalloff { get; set; }

			// Token: 0x170006EC RID: 1772
			// (get) Token: 0x06001926 RID: 6438 RVA: 0x000675A6 File Offset: 0x000657A6
			// (set) Token: 0x06001927 RID: 6439 RVA: 0x000675AE File Offset: 0x000657AE
			[DefaultValue(0f)]
			[Description("How strong the sharpening effect should be. This is a range from 0-&gt;1 however the range can be higher to yield and even stronger effect")]
			public float Strength { get; set; }
		}

		// Token: 0x02000229 RID: 553
		public class VignetteSettings
		{
			// Token: 0x06001929 RID: 6441 RVA: 0x000675C0 File Offset: 0x000657C0
			internal void Apply(RenderAttributes attr)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				string text = "D_VIGNETTE";
				bool enabled = this.Enabled;
				attr.SetCombo(text, enabled);
				if (!this.Enabled)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.vignette.color";
				Vector3 vector = new Vector3(this.Color);
				attr.Set(text, vector);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.vignette.intensity";
				float num = this.Intensity;
				attr.Set(text, num);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.vignette.smoothness";
				num = this.Smoothness;
				attr.Set(text, num);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.vignette.roundness";
				num = this.Roundness;
				attr.Set(text, num);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				text = "standard.vignette.center";
				Vector2 center = this.Center;
				attr.Set(text, center);
			}

			// Token: 0x170006ED RID: 1773
			// (get) Token: 0x0600192A RID: 6442 RVA: 0x00067689 File Offset: 0x00065889
			// (set) Token: 0x0600192B RID: 6443 RVA: 0x00067691 File Offset: 0x00065891
			[DefaultValue(false)]
			[Description("Enables vignette post processing")]
			public bool Enabled { get; set; }

			// Token: 0x170006EE RID: 1774
			// (get) Token: 0x0600192C RID: 6444 RVA: 0x0006769A File Offset: 0x0006589A
			// (set) Token: 0x0600192D RID: 6445 RVA: 0x000676A2 File Offset: 0x000658A2
			[Description("The color of the vignette or the \"border\"")]
			public Color Color { get; set; } = Color.Black;

			// Token: 0x170006EF RID: 1775
			// (get) Token: 0x0600192E RID: 6446 RVA: 0x000676AB File Offset: 0x000658AB
			// (set) Token: 0x0600192F RID: 6447 RVA: 0x000676B3 File Offset: 0x000658B3
			[DefaultValue(1f)]
			[Description("How strong the vignette is. This is a value between 0 -&gt; 1")]
			public float Intensity { get; set; } = 1f;

			// Token: 0x170006F0 RID: 1776
			// (get) Token: 0x06001930 RID: 6448 RVA: 0x000676BC File Offset: 0x000658BC
			// (set) Token: 0x06001931 RID: 6449 RVA: 0x000676C4 File Offset: 0x000658C4
			[DefaultValue(1f)]
			[Description("How much fall off or how blurry the vignette is")]
			public float Smoothness { get; set; } = 1f;

			// Token: 0x170006F1 RID: 1777
			// (get) Token: 0x06001932 RID: 6450 RVA: 0x000676CD File Offset: 0x000658CD
			// (set) Token: 0x06001933 RID: 6451 RVA: 0x000676D5 File Offset: 0x000658D5
			[DefaultValue(1f)]
			[Description("How circular or round the vignette is")]
			public float Roundness { get; set; } = 1f;

			// Token: 0x170006F2 RID: 1778
			// (get) Token: 0x06001934 RID: 6452 RVA: 0x000676DE File Offset: 0x000658DE
			// (set) Token: 0x06001935 RID: 6453 RVA: 0x000676E6 File Offset: 0x000658E6
			[Description("The center of the vignette in relation to UV space. This means a value of {0.5, 0.5} is the center of the screen")]
			public Vector2 Center { get; set; } = new Vector2(0.5f, 0.5f);
		}
	}
}
