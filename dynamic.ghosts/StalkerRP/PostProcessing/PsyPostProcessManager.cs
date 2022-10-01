using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;

namespace StalkerRP.PostProcessing
{
	// Token: 0x02000066 RID: 102
	public class PsyPostProcessManager : StalkerPostProcessingBase
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x000177AA File Offset: 0x000159AA
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x000177B1 File Offset: 0x000159B1
		public static PsyPostProcessManager Instance { get; private set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x000177B9 File Offset: 0x000159B9
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x000177C1 File Offset: 0x000159C1
		private StandardPostProcess ControllerPostProcess { get; set; }

		// Token: 0x06000485 RID: 1157 RVA: 0x000177CA File Offset: 0x000159CA
		public PsyPostProcessManager()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RefreshPostProcess();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			PsyPostProcessManager.Instance = this;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x000177E8 File Offset: 0x000159E8
		public sealed override void RefreshPostProcess()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.RefreshPostProcess();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AddControllerLayer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.ColorOverlay.Enabled = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.ColorOverlay.Color = Color.FromBytes(255, 255, 30, 255);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.ColorOverlay.Mode = StandardPostProcess.ColorOverlaySettings.OverlayMode.Multiply;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.Vignette.Enabled = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.Vignette.Smoothness = 1f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.Vignette.Roundness = 1.9f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.Blur.Enabled = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.MotionBlur.Enabled = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.FilmGrain.Enabled = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.Contrast.Enabled = true;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00017904 File Offset: 0x00015B04
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Initialize();
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00017914 File Offset: 0x00015B14
		private void AddControllerLayer()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.PostProcess.Remove<StandardPostProcess>(this.ControllerPostProcess);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ControllerPostProcess = new StandardPostProcess
			{
				Vignette = 
				{
					Enabled = true,
					Intensity = 0f,
					Smoothness = 1f,
					Roundness = 1f,
					Color = Color.Black
				},
				Blur = 
				{
					Enabled = true,
					Strength = 0f
				},
				Contrast = 
				{
					Enabled = true,
					Contrast = 1f
				}
			};
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.PostProcess.Add<StandardPostProcess>(this.ControllerPostProcess);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000179DF File Offset: 0x00015BDF
		public void SetControllerVignette(float intensity, float roundness)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ControllerPostProcess.Vignette.Intensity = intensity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ControllerPostProcess.Vignette.Roundness = roundness;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00017A0D File Offset: 0x00015C0D
		public void SetControllerBlur(float amount)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ControllerPostProcess.Blur.Strength = amount;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00017A25 File Offset: 0x00015C25
		public void SetControllerContrast(float amount)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ControllerPostProcess.Contrast.Contrast = amount;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00017A40 File Offset: 0x00015C40
		private void Initialize()
		{
			PsyPostProcessManager.<Initialize>d__17 <Initialize>d__;
			<Initialize>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Initialize>d__.<>4__this = this;
			<Initialize>d__.<>1__state = -1;
			<Initialize>d__.<>t__builder.Start<PsyPostProcessManager.<Initialize>d__17>(ref <Initialize>d__);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00017A77 File Offset: 0x00015C77
		public void SetPsyHealthFraction(float n)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.psyHealthFraction = n;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00017A85 File Offset: 0x00015C85
		[Event.FrameAttribute]
		private void FrameUpdate()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.currentFraction = this.currentFraction.LerpTo(this.psyHealthFraction, Time.Delta * 10f, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdatePsyOverlay();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateBlur();
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00017AC8 File Offset: 0x00015CC8
		private void UpdatePsyOverlay()
		{
			float frac = 1f - this.currentFraction;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			frac /= 0.8f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			frac = frac.Clamp(0f, 1f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles particles = this.psyParticles;
			if (particles != null)
			{
				particles.SetPositionComponent(1, 0, 510f - 250f * frac);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.ColorOverlay.Amount = frac * 0.8f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.Contrast.Contrast = 1f + 0.03f * frac;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.Vignette.Intensity = frac;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.FilmGrain.Intensity = frac * 0.45f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.psyWhispers.SetVolume(frac);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00017BAC File Offset: 0x00015DAC
		private void UpdateBlur()
		{
			float frac = 1f - this.currentFraction;
			float strength = 1f - this.currentFraction;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			strength *= 0.35f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			strength *= MathF.Sin(Time.Now * 1.5f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			strength *= Noise.Simplex(Time.Now);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			strength = strength.Clamp(0f, 1f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.Blur.Strength = strength;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.MotionBlur.Scale = frac / 5f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ControllerPostProcess.Blur.Strength = this.ControllerPostProcess.Blur.Strength.LerpTo(0f, Time.Delta, true);
		}

		// Token: 0x0400017F RID: 383
		private Sound psyWhispers;

		// Token: 0x04000180 RID: 384
		private Particles psyParticles;

		// Token: 0x04000181 RID: 385
		private float psyHealthFraction;

		// Token: 0x04000182 RID: 386
		private float currentFraction;
	}
}
