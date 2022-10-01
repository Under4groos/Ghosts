using System;
using System.Runtime.CompilerServices;

namespace Sandbox.UI
{
	// Token: 0x020001BE RID: 446
	[Library("ConvarToggleButton")]
	public class ConvarToggleButton : Button
	{
		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x00057E9E File Offset: 0x0005609E
		// (set) Token: 0x06001653 RID: 5715 RVA: 0x00057EA6 File Offset: 0x000560A6
		public string ConVar { get; set; }

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x00057EAF File Offset: 0x000560AF
		// (set) Token: 0x06001655 RID: 5717 RVA: 0x00057EB7 File Offset: 0x000560B7
		public string ValueOn { get; set; }

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x00057EC0 File Offset: 0x000560C0
		// (set) Token: 0x06001657 RID: 5719 RVA: 0x00057EC8 File Offset: 0x000560C8
		public string ValueOff { get; set; }

		// Token: 0x06001658 RID: 5720 RVA: 0x00057ED1 File Offset: 0x000560D1
		public ConvarToggleButton()
		{
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x00057EDC File Offset: 0x000560DC
		public ConvarToggleButton(Panel parent, string label, string convar, string onvalue, string offvalue, string icon = null)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Parent = parent;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Icon = icon;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Text = label;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ConVar = convar;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ValueOn = onvalue;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ValueOff = offvalue;
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x00057F3C File Offset: 0x0005613C
		public override void Tick()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Tick();
			if (this.ConVar == null)
			{
				return;
			}
			string val = ConsoleSystem.GetValue(this.ConVar, null);
			if (val == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetClass("active", string.Equals(val, this.ValueOn, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x00057F8C File Offset: 0x0005618C
		public void Toggle()
		{
			if (this.ConVar == null)
			{
				return;
			}
			bool status = string.Equals(ConsoleSystem.GetValue(this.ConVar, null), this.ValueOn, StringComparison.OrdinalIgnoreCase);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ConsoleSystem.Run(this.ConVar, new object[]
			{
				status ? this.ValueOff : this.ValueOn
			});
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x00057FE8 File Offset: 0x000561E8
		public override void SetProperty(string name, string value)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetProperty(name, value);
			if (name == "on")
			{
				this.ValueOn = value;
			}
			if (name == "off")
			{
				this.ValueOff = value;
			}
			if (name == "convar")
			{
				this.ConVar = value;
			}
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x0005803E File Offset: 0x0005623E
		protected override void OnClick(MousePanelEvent e)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Toggle();
		}
	}
}
