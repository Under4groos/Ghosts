using System;

namespace Sandbox.UI.Construct
{
	// Token: 0x020001DD RID: 477
	public static class IconPanelConstructor
	{
		// Token: 0x060017E7 RID: 6119 RVA: 0x00063E90 File Offset: 0x00062090
		public static IconPanel Icon(this PanelCreator self, string icon, string classes = null)
		{
			IconPanel control = self.panel.AddChild<IconPanel>(null);
			if (icon != null)
			{
				control.Text = icon;
			}
			if (classes != null)
			{
				control.AddClass(classes);
			}
			return control;
		}
	}
}
