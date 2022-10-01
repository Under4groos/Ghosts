using System;
using System.Runtime.CompilerServices;

namespace Sandbox.UI
{
	// Token: 0x020001CC RID: 460
	[Description("A horizontal slider with a text entry on the right")]
	public class SliderEntry : Panel
	{
		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x060016E2 RID: 5858 RVA: 0x0005FF54 File Offset: 0x0005E154
		// (set) Token: 0x060016E3 RID: 5859 RVA: 0x0005FF5C File Offset: 0x0005E15C
		public Slider Slider { get; protected set; }

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060016E4 RID: 5860 RVA: 0x0005FF65 File Offset: 0x0005E165
		// (set) Token: 0x060016E5 RID: 5861 RVA: 0x0005FF6D File Offset: 0x0005E16D
		public TextEntry TextEntry { get; protected set; }

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x0005FF76 File Offset: 0x0005E176
		// (set) Token: 0x060016E7 RID: 5863 RVA: 0x0005FF83 File Offset: 0x0005E183
		public float MinValue
		{
			get
			{
				return this.Slider.MinValue;
			}
			set
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Slider.MinValue = value;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TextEntry.MinValue = new float?(value);
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x0005FFAC File Offset: 0x0005E1AC
		// (set) Token: 0x060016E9 RID: 5865 RVA: 0x0005FFB9 File Offset: 0x0005E1B9
		public float MaxValue
		{
			get
			{
				return this.Slider.MaxValue;
			}
			set
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Slider.MaxValue = value;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TextEntry.MaxValue = new float?(value);
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x0005FFE2 File Offset: 0x0005E1E2
		// (set) Token: 0x060016EB RID: 5867 RVA: 0x0005FFEF File Offset: 0x0005E1EF
		public float Step
		{
			get
			{
				return this.Slider.Step;
			}
			set
			{
				this.Slider.Step = value;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x0005FFFD File Offset: 0x0005E1FD
		// (set) Token: 0x060016ED RID: 5869 RVA: 0x0006000A File Offset: 0x0005E20A
		public string Format
		{
			get
			{
				return this.TextEntry.NumberFormat;
			}
			set
			{
				this.TextEntry.NumberFormat = value;
			}
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x00060018 File Offset: 0x0005E218
		public SliderEntry()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddClass("sliderentry");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Slider = base.AddChild<Slider>(null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TextEntry = base.AddChild<TextEntry>(null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TextEntry.Numeric = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TextEntry.NumberFormat = "0.###";
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TextEntry.Bind("value", this.Slider, "Value");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Slider.AddEventListener("value.changed", delegate()
			{
				this.OnValueChanged(this.Slider.Value);
			});
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TextEntry.AddEventListener("value.changed", delegate()
			{
				this.OnValueChanged(this.TextEntry.Text);
			});
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x000600E7 File Offset: 0x0005E2E7
		protected void OnValueChanged(object value)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.CreateValueEvent("value", value);
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060016F0 RID: 5872 RVA: 0x000600FA File Offset: 0x0005E2FA
		// (set) Token: 0x060016F1 RID: 5873 RVA: 0x00060107 File Offset: 0x0005E307
		[Description("The actual value. Setting the value will snap and clamp it.")]
		public float Value
		{
			get
			{
				return this.Slider.Value;
			}
			set
			{
				this.Slider.Value = value;
			}
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x00060118 File Offset: 0x0005E318
		public override void SetProperty(string name, string value)
		{
			if (name == "min" || name == "max" || name == "value" || name == "step")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Slider.SetProperty(name, value);
				return;
			}
			if (name == "format")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TextEntry.NumberFormat = value;
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetProperty(name, value);
		}
	}
}
