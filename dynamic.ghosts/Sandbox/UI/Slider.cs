using System;
using System.Runtime.CompilerServices;

namespace Sandbox.UI
{
	// Token: 0x020001CA RID: 458
	[Description("A horizontal slider. Can be float or whole number.")]
	public class Slider : Panel
	{
		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x060016C2 RID: 5826 RVA: 0x0005F85D File Offset: 0x0005DA5D
		// (set) Token: 0x060016C3 RID: 5827 RVA: 0x0005F865 File Offset: 0x0005DA65
		public Panel Track { get; protected set; }

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x060016C4 RID: 5828 RVA: 0x0005F86E File Offset: 0x0005DA6E
		// (set) Token: 0x060016C5 RID: 5829 RVA: 0x0005F876 File Offset: 0x0005DA76
		public Panel TrackInner { get; protected set; }

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x0005F87F File Offset: 0x0005DA7F
		// (set) Token: 0x060016C7 RID: 5831 RVA: 0x0005F887 File Offset: 0x0005DA87
		public Panel Thumb { get; protected set; }

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x060016C8 RID: 5832 RVA: 0x0005F890 File Offset: 0x0005DA90
		// (set) Token: 0x060016C9 RID: 5833 RVA: 0x0005F898 File Offset: 0x0005DA98
		[DefaultValue(100)]
		[Description("The right side of the slider")]
		public float MaxValue { get; set; } = 100f;

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x0005F8A1 File Offset: 0x0005DAA1
		// (set) Token: 0x060016CB RID: 5835 RVA: 0x0005F8A9 File Offset: 0x0005DAA9
		[DefaultValue(0)]
		[Description("The left side of the slider")]
		public float MinValue { get; set; }

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x0005F8B2 File Offset: 0x0005DAB2
		// (set) Token: 0x060016CD RID: 5837 RVA: 0x0005F8BA File Offset: 0x0005DABA
		[DefaultValue(1f)]
		[Description("If set to 1, value will be rounded to 1's If set to 10, value will be rounded to 10's If set to 0.1, value will be rounded to 0.1's")]
		public float Step { get; set; } = 1f;

		// Token: 0x060016CE RID: 5838 RVA: 0x0005F8C4 File Offset: 0x0005DAC4
		public Slider()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddClass("slider");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Track = base.Add.Panel("track");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TrackInner = this.Track.Add.Panel("inner");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Thumb = base.Add.Panel("thumb");
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x0005F967 File Offset: 0x0005DB67
		// (set) Token: 0x060016D0 RID: 5840 RVA: 0x0005F980 File Offset: 0x0005DB80
		[Description("The actual value. Setting the value will snap and clamp it.")]
		public float Value
		{
			get
			{
				return this._value.Clamp(this.MinValue, this.MaxValue);
			}
			set
			{
				float snapped = (this.Step > 0f) ? value.SnapToGrid(this.Step) : value;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				snapped = snapped.Clamp(this.MinValue, this.MaxValue);
				if (this._value == snapped)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this._value = snapped;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CreateEvent("onchange", null, null);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.CreateValueEvent("value", this._value);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.UpdateSliderPositions();
			}
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x0005FA18 File Offset: 0x0005DC18
		public override void SetProperty(string name, string value)
		{
			float floatValue;
			if (name == "min" && float.TryParse(value, out floatValue))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.MinValue = floatValue;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.UpdateSliderPositions();
				return;
			}
			if (name == "step" && float.TryParse(value, out floatValue))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Step = floatValue;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.UpdateSliderPositions();
				return;
			}
			if (name == "max" && float.TryParse(value, out floatValue))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.MaxValue = floatValue;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.UpdateSliderPositions();
				return;
			}
			if (name == "value" && float.TryParse(value, out floatValue))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Value = floatValue;
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetProperty(name, value);
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x0005FAE4 File Offset: 0x0005DCE4
		[Description("Convert a screen position to a value. The value is clamped, but not snapped.")]
		public virtual float ScreenPosToValue(Vector2 pos)
		{
			Vector2 localPos = base.ScreenPositionToPanelPosition(pos);
			float thumbSize = this.Thumb.Box.Rect.Width * 0.5f;
			float normalized = localPos.x.LerpInverse(thumbSize, base.Box.Rect.Width - thumbSize, true);
			float scaled = this.MinValue.LerpTo(this.MaxValue, normalized, true);
			if (this.Step <= 0f)
			{
				return scaled;
			}
			return scaled.SnapToGrid(this.Step);
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x0005FB68 File Offset: 0x0005DD68
		[Description("If we move the mouse while we're being pressed then set the position, but skip transitions.")]
		protected override void OnMouseMove(MousePanelEvent e)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnMouseMove(e);
			if (!base.HasActive)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Value = this.ScreenPosToValue(Mouse.Position);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateSliderPositions();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SkipTransitions();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			e.StopPropagation();
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x0005FBC1 File Offset: 0x0005DDC1
		[Description("On mouse press jump to that position")]
		protected override void OnMouseDown(MousePanelEvent e)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnMouseDown(e);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Value = this.ScreenPosToValue(Mouse.Position);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateSliderPositions();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			e.StopPropagation();
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x0005FBFC File Offset: 0x0005DDFC
		[Description("Updates the styles for TrackInner and Thumb to position us based on the current value. Note this purposely uses percentages instead of pixels when setting up, this way we don't have to worry about parent size, screen scale etc.")]
		private void UpdateSliderPositions()
		{
			int hash = HashCode.Combine<float, float, float>(this.Value, this.MinValue, this.MaxValue);
			if (hash == this.positionHash)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.positionHash = hash;
			float pos = this.Value.LerpInverse(this.MinValue, this.MaxValue, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TrackInner.Style.Width = Length.Fraction(pos);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Thumb.Style.Left = Length.Fraction(pos);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TrackInner.Style.Dirty();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Thumb.Style.Dirty();
		}

		// Token: 0x04000766 RID: 1894
		protected float _value = float.MaxValue;

		// Token: 0x04000767 RID: 1895
		private int positionHash;
	}
}
