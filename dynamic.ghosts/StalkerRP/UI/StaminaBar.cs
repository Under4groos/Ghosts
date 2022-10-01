using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.UI;

namespace StalkerRP.UI
{
	// Token: 0x02000062 RID: 98
	[UseTemplate("ui/staminabar.html")]
	public class StaminaBar : Panel
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0001685A File Offset: 0x00014A5A
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x00016861 File Offset: 0x00014A61
		public static StaminaBar Instance { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00016869 File Offset: 0x00014A69
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x00016871 File Offset: 0x00014A71
		public Panel FillBar { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0001687A File Offset: 0x00014A7A
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x00016882 File Offset: 0x00014A82
		public Panel BrokenBar { get; set; }

		// Token: 0x06000455 RID: 1109 RVA: 0x0001688C File Offset: 0x00014A8C
		public StaminaBar()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StaminaBar.Instance = this;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00016917 File Offset: 0x00014B17
		public void SetFraction(float n)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.staminaFraction = n;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.staminaFraction = this.staminaFraction.Clamp(0f, 1f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.staminaAbsoluteFraction = n;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00016954 File Offset: 0x00014B54
		public void SetBrokenFraction(float n)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.brokenFraction = n;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.BrokenBar.Style.Opacity = new float?((float)((n > 0f) ? 1 : 0));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.BrokenBar.Style.Width = Length.Percent(n * 100f);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x000169B8 File Offset: 0x00014BB8
		[Event.FrameAttribute]
		private void FrameUpdate()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.currentStaminaFraction = this.currentStaminaFraction.LerpTo(this.staminaFraction, Time.Delta * 15f, true);
			float brokeFrac = 1f - this.brokenFraction;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FillBar.Style.Width = Length.Percent(this.currentStaminaFraction * 100f * brokeFrac - 1f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetClass("visible", (this.staminaAbsoluteFraction < 1f && Local.Pawn.LifeState == LifeState.Alive) || Input.Down(InputButton.Score));
			float num = this.currentStaminaFraction;
			if (num > 0.25f)
			{
				if (num <= 0.5f)
				{
					float delta = 1f - (this.currentStaminaFraction - 0.25f) / 0.25f;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.FillBar.Style.BackgroundColor = new Color?(Color.Lerp(this.Half, this.Low, delta, true));
					return;
				}
				float delta2 = (1f - this.currentStaminaFraction) / 0.5f;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.FillBar.Style.BackgroundColor = new Color?(Color.Lerp(this.Full, this.Half, delta2, true));
				return;
			}
			else
			{
				if (num <= 0f)
				{
					return;
				}
				float delta3 = 1f - this.currentStaminaFraction / 0.25f;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.FillBar.Style.BackgroundColor = new Color?(Color.Lerp(this.Low, this.Critical, delta3, true));
				return;
			}
		}

		// Token: 0x04000154 RID: 340
		private float staminaFraction;

		// Token: 0x04000155 RID: 341
		private float staminaAbsoluteFraction;

		// Token: 0x04000156 RID: 342
		private float currentStaminaFraction = 1f;

		// Token: 0x04000157 RID: 343
		private float brokenFraction;

		// Token: 0x04000158 RID: 344
		private Color Full = Color.FromBytes(42, 95, 125, 255);

		// Token: 0x04000159 RID: 345
		private Color Half = Color.FromBytes(237, 215, 119, 255);

		// Token: 0x0400015A RID: 346
		private Color Low = Color.FromBytes(240, 81, 81, 255);

		// Token: 0x0400015B RID: 347
		private Color Critical = Color.FromBytes(255, 0, 0, 255);
	}
}
