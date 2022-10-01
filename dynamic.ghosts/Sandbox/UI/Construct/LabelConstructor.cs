using System;

namespace Sandbox.UI.Construct
{
	// Token: 0x020001DE RID: 478
	public static class LabelConstructor
	{
		// Token: 0x060017E8 RID: 6120 RVA: 0x00063EC0 File Offset: 0x000620C0
		public static Label Label(this PanelCreator self, string text = null, string classname = null)
		{
			Label control = self.panel.AddChild<Label>(null);
			if (text != null)
			{
				control.Text = text;
			}
			if (classname != null)
			{
				control.AddClass(classname);
			}
			return control;
		}
	}
}
