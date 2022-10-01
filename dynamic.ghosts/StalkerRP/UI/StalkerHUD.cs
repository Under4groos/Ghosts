using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.UI;

namespace StalkerRP.UI
{
	// Token: 0x02000061 RID: 97
	[UseTemplate("ui/stalkerhud.html")]
	public class StalkerHUD : RootPanel
	{
		// Token: 0x0600044E RID: 1102 RVA: 0x0001683A File Offset: 0x00014A3A
		public StalkerHUD()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Local.Hud = this;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddChild<Scoreboard<ScoreboardEntry>>(null);
		}
	}
}
