using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;

namespace StalkerRP.PostProcessing
{
	// Token: 0x02000065 RID: 101
	public class HealthPostProcessManager : StalkerPostProcessingBase
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x00017245 File Offset: 0x00015445
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x0001724C File Offset: 0x0001544C
		public static HealthPostProcessManager Instance { get; private set; }

		// Token: 0x06000470 RID: 1136 RVA: 0x00017254 File Offset: 0x00015454
		public HealthPostProcessManager()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RefreshPostProcess();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			HealthPostProcessManager.Instance = this;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x000172C0 File Offset: 0x000154C0
		public sealed override void RefreshPostProcess()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.RefreshPostProcess();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AddBloodLayer();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.ColorOverlay.Enabled = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.ColorOverlay.Color = Color.Black;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.ColorOverlay.Mode = StandardPostProcess.ColorOverlaySettings.OverlayMode.Mix;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.Saturate.Enabled = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.Vignette.Enabled = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.Vignette.Smoothness = 1f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.Vignette.Roundness = 1.45f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.Blur.Enabled = true;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x000173A0 File Offset: 0x000155A0
		private void AddBloodLayer()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.PostProcess.Remove<StandardPostProcess>(this.BloodVignette);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.BloodVignette = new StandardPostProcess
			{
				Vignette = 
				{
					Enabled = true,
					Intensity = 0f,
					Smoothness = 1f,
					Roundness = 2f,
					Color = Color.Red
				}
			};
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.PostProcess.Add<StandardPostProcess>(this.BloodVignette);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00017434 File Offset: 0x00015634
		public void AddSuppression(float sup)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceSuppressed = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.suppression += sup;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.suppression = this.suppression.Clamp(0f, 1f);
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00017489 File Offset: 0x00015689
		private float maximumSaturationLoss
		{
			get
			{
				return 0.45f;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00017490 File Offset: 0x00015690
		private float healthEffectsThreshold
		{
			get
			{
				return 0.6f;
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00017498 File Offset: 0x00015698
		public void SetHealthLevel(float n)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			n = n.Clamp(0f, 1f);
			if (n < this.healthEffectsThreshold)
			{
				float amount = 1f - (1f - n / this.healthEffectsThreshold) * this.maximumSaturationLoss;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.StandardPostProcess.Saturate.Amount = amount;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.BloodVignette.Vignette.Intensity = 1f - n / this.healthEffectsThreshold;
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.Saturate.Amount = 1f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.BloodVignette.Vignette.Intensity = 0f;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0001754F File Offset: 0x0001574F
		public void OnTakeDamage()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoBlur(0.08f);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00017561 File Offset: 0x00015761
		public void DoBlur(float strength)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.blurStrength = strength;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceBlurStarted = 0f;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00017584 File Offset: 0x00015784
		private float fadeOutTime
		{
			get
			{
				return 2.5f;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0001758B File Offset: 0x0001578B
		private float fadeInTime
		{
			get
			{
				return 0.5f;
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00017592 File Offset: 0x00015792
		public new void OnKilled()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.dead = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceDied = 0f;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x000175B5 File Offset: 0x000157B5
		public void OnRespawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.dead = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceRespawn = 0f;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000175D8 File Offset: 0x000157D8
		[Event.FrameAttribute]
		private void FrameUpdate()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateVignette();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateBlur();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateColorOverlay();
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000175FC File Offset: 0x000157FC
		private void UpdateBlur()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.Blur.Strength = this.currentBlur;
			if (this.timeSinceBlurStarted > 0.2f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.currentBlur = this.currentBlur.LerpTo(0f, Time.Delta * 15f, true);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.blurStrength = 0f;
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.currentBlur = this.currentBlur.LerpTo(this.blurStrength, Time.Delta * 25f, true);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00017698 File Offset: 0x00015898
		private void UpdateVignette()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.Vignette.Intensity = this.currentSuppression;
			if (this.timeSinceSuppressed > 2f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.currentSuppression = this.currentSuppression.LerpTo(0f, Time.Delta, true);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.suppression = 0f;
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.currentSuppression = this.currentSuppression.LerpTo(this.suppression, Time.Delta * 25f, true);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0001772C File Offset: 0x0001592C
		private void UpdateColorOverlay()
		{
			if (this.dead)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.StandardPostProcess.ColorOverlay.Amount = this.timeSinceDied.Relative.LerpInverse(0f, this.fadeOutTime, true);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.ColorOverlay.Amount = 1f - this.timeSinceRespawn.Relative.LerpInverse(0f, this.fadeInTime, true);
		}

		// Token: 0x04000173 RID: 371
		private StandardPostProcess BloodVignette;

		// Token: 0x04000174 RID: 372
		private TimeSince timeSinceSuppressed = 0f;

		// Token: 0x04000175 RID: 373
		private float suppression;

		// Token: 0x04000176 RID: 374
		private float currentSuppression;

		// Token: 0x04000177 RID: 375
		private bool dead;

		// Token: 0x04000178 RID: 376
		private TimeSince timeSinceDied = 0f;

		// Token: 0x04000179 RID: 377
		private TimeSince timeSinceRespawn = 0f;

		// Token: 0x0400017A RID: 378
		private TimeSince timeSinceBlurStarted = 0f;

		// Token: 0x0400017B RID: 379
		private float blurStrength;

		// Token: 0x0400017C RID: 380
		private float currentBlur;
	}
}
