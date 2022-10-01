using System;

namespace Sandbox.UI.Construct
{
	// Token: 0x020001DC RID: 476
	public static class ButtonGroupConstructor
	{
		// Token: 0x060017E6 RID: 6118 RVA: 0x00063E68 File Offset: 0x00062068
		public static ButtonGroup ButtonGroup(this PanelCreator self, string classes = null)
		{
			ButtonGroup control = self.panel.AddChild<ButtonGroup>(null);
			if (classes != null)
			{
				control.AddClass(classes);
			}
			return control;
		}
	}
}
