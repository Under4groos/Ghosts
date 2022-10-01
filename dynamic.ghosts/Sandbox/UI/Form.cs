using System;
using System.Runtime.CompilerServices;
using Sandbox.UI.Construct;

namespace Sandbox.UI
{
	// Token: 0x020001D4 RID: 468
	[Library("form")]
	public class Form : Panel
	{
		// Token: 0x0600179B RID: 6043 RVA: 0x00062876 File Offset: 0x00060A76
		public Form()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddClass("form");
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x00062890 File Offset: 0x00060A90
		public void AddRow(string entryTitle, Panel control)
		{
			Field field = base.AddChild<Field>(null);
			Panel panel = field.Add.Panel("label");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			panel.Add.Label(entryTitle, null);
			FieldControl value = field.AddChild<FieldControl>(null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			control.Parent = value;
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x000628DC File Offset: 0x00060ADC
		public void AddHeader(string title, string icon = "category")
		{
			Panel panel = (this.currentGroup ?? this).Add.Panel("field-header");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			panel.Add.Icon(icon, null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			panel.Add.Label(title, null);
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x0006292C File Offset: 0x00060B2C
		public void AddRow(PropertyDescription member, object target, Panel control)
		{
			string entryTitle = member.GetDisplayInfo().Name ?? member.Name;
			Field field = (this.currentGroup ?? this).AddChild<Field>(null);
			Panel panel = field.Add.Panel("label");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			panel.Add.Label(entryTitle, null);
			FieldControl value = field.AddChild<FieldControl>(null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			control.Parent = value;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			control.Bind("value", target, member.Name);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			control.SetClass("disabled", !member.CanWrite);
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x000629C8 File Offset: 0x00060BC8
		public void Clear()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.DeleteChildren(true);
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x000629D8 File Offset: 0x00060BD8
		protected override void OnEvent(PanelEvent e)
		{
			if (e.Name == "onchange")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CreateEvent("form.changed", null, null);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnEvent(e);
		}

		// Token: 0x0400079C RID: 1948
		protected Panel currentGroup;
	}
}
