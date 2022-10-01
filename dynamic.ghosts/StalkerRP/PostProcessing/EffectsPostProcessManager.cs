using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.PostProcessing
{
	// Token: 0x02000064 RID: 100
	public class EffectsPostProcessManager : StalkerPostProcessingBase
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x00016DD5 File Offset: 0x00014FD5
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x00016DDC File Offset: 0x00014FDC
		public static EffectsPostProcessManager Instance { get; private set; }

		// Token: 0x06000464 RID: 1124 RVA: 0x00016DE4 File Offset: 0x00014FE4
		public EffectsPostProcessManager()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RefreshPostProcess();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			EffectsPostProcessManager.Instance = this;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00016E3C File Offset: 0x0001503C
		public sealed override void RefreshPostProcess()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.RefreshPostProcess();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.ColorOverlay.Enabled = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.ColorOverlay.Mode = StandardPostProcess.ColorOverlaySettings.OverlayMode.Multiply;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.Saturate.Enabled = true;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00016E98 File Offset: 0x00015098
		public void AddColorMultiplier(Color color, float value)
		{
			if (!this.colorMultipliers.ContainsKey(color))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.colorMultipliers[color] = value;
				return;
			}
			float maxMult;
			if (EffectsPostProcessManager.maxColorMultipliers.TryGetValue(color, out maxMult))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.colorMultipliers[color] = Math.Clamp(this.colorMultipliers[color] + value, 0f, maxMult);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.colorMultipliers[color] = Math.Clamp(this.colorMultipliers[color] + value, 0f, this.maxColorMult);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00016F2F File Offset: 0x0001512F
		public void SetColorMultiplier(Color color, float value)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.colorMultipliers[color] = value;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00016F43 File Offset: 0x00015143
		public void RemoveColorMultiplier(Color color)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.colorMultipliers.Remove(color);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00016F57 File Offset: 0x00015157
		public void SetFilmGrain(float n)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.FilmGrain.Enabled = (n > 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.FilmGrain.Intensity = n;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00016F8C File Offset: 0x0001518C
		public void SetSaturation(float n)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.Saturate.Amount = n;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00016FA4 File Offset: 0x000151A4
		private void UpdateColorMultiplier()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.multiplierStrength = 0f;
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			foreach (KeyValuePair<Color, float> multiplier in this.colorMultipliers)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.multiplierStrength += multiplier.Value;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				num += multiplier.Key.r * multiplier.Value;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				num2 += multiplier.Key.g * multiplier.Value;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				num3 += multiplier.Key.b * multiplier.Value;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				num4 += multiplier.Key.a * multiplier.Value;
			}
			int len = this.colorMultipliers.Count;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.colorMultiplier = new Color(num, num2, num3, num4);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.ColorOverlay.Color = this.colorMultiplier;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.ColorOverlay.Amount = this.multiplierStrength;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StandardPostProcess.ColorOverlay.Enabled = (len > 0);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0001711C File Offset: 0x0001531C
		[Event.FrameAttribute]
		private void UpdatePostProcessingEffects()
		{
			float delta = Time.Delta;
			foreach (KeyValuePair<Color, float> multiplier in this.colorMultipliers)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.colorMultipliers[multiplier.Key] = (this.colorMultipliers[multiplier.Key] - 1f * delta).Clamp(0f, 100f);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateColorMultiplier();
		}

		// Token: 0x04000166 RID: 358
		private const float colorDecayRate = 1f;

		// Token: 0x04000168 RID: 360
		public static readonly Color STALKER_PSY_CONTROLLER_COLOR = Color.Yellow;

		// Token: 0x04000169 RID: 361
		public static readonly Color STALKER_PSY_BURER_COLOR = Color.Blue;

		// Token: 0x0400016A RID: 362
		public static readonly Color STALKER_PSY_PSYDOG_COLOR = Color.White;

		// Token: 0x0400016B RID: 363
		public static readonly Color STALKER_PSY_KARLIK_COLOR = Color.Green;

		// Token: 0x0400016C RID: 364
		public static readonly Color STALKER_ANOMALY_BURNER = Color.Orange;

		// Token: 0x0400016D RID: 365
		private static readonly Dictionary<Color, float> maxColorMultipliers = new Dictionary<Color, float>
		{
			{
				EffectsPostProcessManager.STALKER_PSY_BURER_COLOR,
				0.7f
			},
			{
				EffectsPostProcessManager.STALKER_PSY_CONTROLLER_COLOR,
				0.35f
			},
			{
				EffectsPostProcessManager.STALKER_ANOMALY_BURNER,
				0.7f
			},
			{
				EffectsPostProcessManager.STALKER_PSY_KARLIK_COLOR,
				0.7f
			}
		};

		// Token: 0x0400016E RID: 366
		private float maxColorMult = 1f;

		// Token: 0x0400016F RID: 367
		private readonly Dictionary<Color, float> colorMultipliers = new Dictionary<Color, float>();

		// Token: 0x04000170 RID: 368
		private Color colorMultiplier = Color.White;

		// Token: 0x04000171 RID: 369
		private float multiplierStrength = 1f;
	}
}
