using System;
using System.Runtime.CompilerServices;

namespace Sandbox.UI.Construct
{
	// Token: 0x020001DF RID: 479
	public static class SliderConstructor
	{
		// Token: 0x060017E9 RID: 6121 RVA: 0x00063EEF File Offset: 0x000620EF
		public static Slider Slider(this PanelCreator self, float min, float max, float step)
		{
			Slider slider = self.panel.AddChild<Slider>(null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			slider.MinValue = min;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			slider.MaxValue = max;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			slider.Step = step;
			return slider;
		}
	}
}
