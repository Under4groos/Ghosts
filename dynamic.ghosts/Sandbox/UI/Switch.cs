using System;
using System.Runtime.CompilerServices;

namespace Sandbox.UI
{
	// Token: 0x020001CE RID: 462
	public class Switch : Checkbox
	{
		// Token: 0x06001708 RID: 5896 RVA: 0x00060674 File Offset: 0x0005E874
		public Switch()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddClass("switch");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Panel checkMark = base.CheckMark;
			if (checkMark != null)
			{
				checkMark.Delete(true);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.CheckMark = base.Add.Panel("checkmark");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.CheckMark.Add.Panel("handle");
		}
	}
}
