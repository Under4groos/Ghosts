using System;
using System.Runtime.CompilerServices;

namespace Sandbox.UI.Construct
{
	// Token: 0x020001E0 RID: 480
	public static class SliderWithEntryConstructor
	{
		// Token: 0x060017EA RID: 6122 RVA: 0x00063F21 File Offset: 0x00062121
		public static SliderEntry SliderWithEntry(this PanelCreator self, float min, float max, float step)
		{
			SliderEntry sliderEntry = self.panel.AddChild<SliderEntry>(null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			sliderEntry.MinValue = min;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			sliderEntry.MaxValue = max;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			sliderEntry.Step = step;
			return sliderEntry;
		}
	}
}
