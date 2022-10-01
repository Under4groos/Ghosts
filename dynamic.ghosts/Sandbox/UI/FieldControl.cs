using System;
using System.Runtime.CompilerServices;

namespace Sandbox.UI
{
	// Token: 0x020001C2 RID: 450
	[Library("control")]
	[Description("A field in a form, usually contains a label and a control")]
	public class FieldControl : Panel
	{
		// Token: 0x0600166E RID: 5742 RVA: 0x0005E59B File Offset: 0x0005C79B
		public FieldControl()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddClass("field-control control");
		}
	}
}
