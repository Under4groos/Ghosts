using System;
using System.Runtime.CompilerServices;

namespace Sandbox.UI
{
	// Token: 0x020001C1 RID: 449
	[Library("field")]
	[Description("A field in a form, usually contains a label and a control")]
	public class Field : Panel
	{
		// Token: 0x0600166D RID: 5741 RVA: 0x0005E583 File Offset: 0x0005C783
		public Field()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddClass("field");
		}
	}
}
