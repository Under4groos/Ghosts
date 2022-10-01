using System;
using System.Runtime.CompilerServices;

namespace Sandbox.UI
{
	// Token: 0x020001CB RID: 459
	[Description("A horizontal slider. Can be float or whole number.")]
	public class Slider2D : Panel
	{
		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x060016D6 RID: 5846 RVA: 0x0005FCB0 File Offset: 0x0005DEB0
		// (set) Token: 0x060016D7 RID: 5847 RVA: 0x0005FCB8 File Offset: 0x0005DEB8
		public Panel Track { get; protected set; }

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x060016D8 RID: 5848 RVA: 0x0005FCC1 File Offset: 0x0005DEC1
		// (set) Token: 0x060016D9 RID: 5849 RVA: 0x0005FCC9 File Offset: 0x0005DEC9
		public Panel Thumb { get; protected set; }

		// Token: 0x060016DA RID: 5850 RVA: 0x0005FCD4 File Offset: 0x0005DED4
		public Slider2D()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddClass("slider2d");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Track = base.Add.Panel("track");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Thumb = base.Add.Panel("thumb");
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x060016DB RID: 5851 RVA: 0x0005FD33 File Offset: 0x0005DF33
		// (set) Token: 0x060016DC RID: 5852 RVA: 0x0005FD3C File Offset: 0x0005DF3C
		[Property]
		[Description("The actual value. Setting the value will snap and clamp it.")]
		public Vector2 Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if (this._value == value)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this._value = value;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.CreateValueEvent("value", this._value);
				if (!base.HasActive)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.UpdateSliderPositions();
				}
			}
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x0005FD94 File Offset: 0x0005DF94
		public override void SetProperty(string name, string value)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetProperty(name, value);
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x0005FDA4 File Offset: 0x0005DFA4
		[Description("Convert a screen position to a value. The value is clamped, but not snapped.")]
		public virtual Vector2 ScreenPositionToValue(Vector2 pos)
		{
			Vector2 localPos = base.ScreenPositionToPanelDelta(pos);
			float x = localPos.x.Clamp(0f, 1f);
			float y = localPos.y.Clamp(0f, 1f);
			return new Vector2(x, 1f - y);
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x0005FDF4 File Offset: 0x0005DFF4
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
			this.Value = this.ScreenPositionToValue(Mouse.Position);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateSliderPositions();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SkipTransitions();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			e.StopPropagation();
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x0005FE4D File Offset: 0x0005E04D
		[Description("On mouse press jump to that position")]
		protected override void OnMouseDown(MousePanelEvent e)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnMouseDown(e);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Value = this.ScreenPositionToValue(Mouse.Position);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateSliderPositions();
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x0005FE7C File Offset: 0x0005E07C
		[Description("Updates the styles for TrackInner and Thumb to position us based on the current value. Note this purposely uses percentages instead of pixels when setting up, this way we don't have to worry about parent size, screen scale etc.")]
		private void UpdateSliderPositions()
		{
			int hash = HashCode.Combine<Vector2>(this.Value);
			if (hash == this.positionHash)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.positionHash = hash;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Thumb.Style.Left = Length.Fraction(this.Value.x);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Thumb.Style.Top = Length.Fraction(1f - this.Value.y);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Thumb.Style.Dirty();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Style.PaddingRight = new Length?(0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Style.PaddingBottom = new Length?(0f);
		}

		// Token: 0x0400076A RID: 1898
		protected Vector2 _value;

		// Token: 0x0400076B RID: 1899
		private int positionHash;
	}
}
