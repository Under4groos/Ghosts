using System;
using System.Runtime.CompilerServices;

namespace Sandbox.UI
{
	// Token: 0x020001C3 RID: 451
	[Library("IconPanel")]
	[Alias(new string[]
	{
		"icon"
	})]
	public class IconPanel : Label
	{
		// Token: 0x0600166F RID: 5743 RVA: 0x0005E5B3 File Offset: 0x0005C7B3
		public IconPanel()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddClass("iconpanel");
		}
	}
}
