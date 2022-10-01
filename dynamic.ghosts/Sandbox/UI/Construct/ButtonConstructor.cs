using System;
using System.Runtime.CompilerServices;

namespace Sandbox.UI.Construct
{
	// Token: 0x020001DB RID: 475
	public static class ButtonConstructor
	{
		// Token: 0x060017E2 RID: 6114 RVA: 0x00063D84 File Offset: 0x00061F84
		public static Button Button(this PanelCreator self, string text, Action onClick = null)
		{
			Button b = new Button(text, null, onClick);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			self.panel.AddChild(b);
			return b;
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00063DAC File Offset: 0x00061FAC
		public static Button Button(this PanelCreator self, string text, string className, Action onClick = null)
		{
			Button control = self.panel.AddChild<Button>(null);
			if (text != null)
			{
				control.SetText(text);
			}
			if (onClick != null)
			{
				control.AddEventListener("onclick", onClick);
			}
			if (className != null)
			{
				control.AddClass(className);
			}
			return control;
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x00063DEC File Offset: 0x00061FEC
		public static Button ButtonWithIcon(this PanelCreator self, string text, string icon, string className, Action onClick = null)
		{
			Button control = self.panel.AddChild<Button>(null);
			if (icon != null)
			{
				control.Icon = icon;
			}
			if (text != null)
			{
				control.SetText(text);
			}
			if (onClick != null)
			{
				control.AddEventListener("onclick", onClick);
			}
			if (className != null)
			{
				control.AddClass(className);
			}
			return control;
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x00063E38 File Offset: 0x00062038
		public static Button ButtonWithConsoleCommand(this PanelCreator self, string text, string command)
		{
			return self.Button(text, delegate()
			{
				ConsoleSystem.Run(command);
			});
		}
	}
}
