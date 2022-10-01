using System;
using System.Runtime.CompilerServices;
using Sandbox.UI;

// Token: 0x02000009 RID: 9
public class Crosshair : Panel
{
	// Token: 0x06000012 RID: 18 RVA: 0x000024D5 File Offset: 0x000006D5
	public Crosshair()
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		Crosshair.Current = this;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.StyleSheet.Load("/ui/Crosshair.scss", true);
	}

	// Token: 0x0400000B RID: 11
	public static Crosshair Current;
}
