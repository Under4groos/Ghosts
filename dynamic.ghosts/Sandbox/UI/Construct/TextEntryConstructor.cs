using System;
using System.Runtime.CompilerServices;

namespace Sandbox.UI.Construct
{
	// Token: 0x020001E1 RID: 481
	public static class TextEntryConstructor
	{
		// Token: 0x060017EB RID: 6123 RVA: 0x00063F53 File Offset: 0x00062153
		public static TextEntry TextEntry(this PanelCreator self, string text)
		{
			TextEntry textEntry = self.panel.AddChild<TextEntry>(null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			textEntry.Text = text;
			return textEntry;
		}
	}
}
