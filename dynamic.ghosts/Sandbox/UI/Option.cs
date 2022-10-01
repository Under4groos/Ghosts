using System;
using System.Runtime.CompilerServices;

namespace Sandbox.UI
{
	// Token: 0x020001C7 RID: 455
	public class Option
	{
		// Token: 0x060016A1 RID: 5793 RVA: 0x0005F0EB File Offset: 0x0005D2EB
		public Option()
		{
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x0005F0F3 File Offset: 0x0005D2F3
		public Option(string title, object value)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Title = title;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Value = value;
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x0005F113 File Offset: 0x0005D313
		public Option(string title, string icon, object value)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Title = title;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Icon = icon;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Value = value;
		}

		// Token: 0x04000752 RID: 1874
		public string Title;

		// Token: 0x04000753 RID: 1875
		public string Icon;

		// Token: 0x04000754 RID: 1876
		public string Subtitle;

		// Token: 0x04000755 RID: 1877
		public string Tooltip;

		// Token: 0x04000756 RID: 1878
		public object Value;
	}
}
