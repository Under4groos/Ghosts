using System;
using System.Runtime.CompilerServices;
using Sandbox.UI.Construct;

namespace Sandbox.UI
{
	// Token: 0x020001BD RID: 445
	[Library("checkbox")]
	public class Checkbox : Panel
	{
		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x00057C3E File Offset: 0x00055E3E
		// (set) Token: 0x06001643 RID: 5699 RVA: 0x00057C46 File Offset: 0x00055E46
		[Description("The checkmark icon. Although no guarantees it's an icon!")]
		public Panel CheckMark { get; protected set; }

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x00057C4F File Offset: 0x00055E4F
		// (set) Token: 0x06001645 RID: 5701 RVA: 0x00057C57 File Offset: 0x00055E57
		[Description("Returns true if this checkbox is checked")]
		public bool Checked
		{
			get
			{
				return this.isChecked;
			}
			set
			{
				if (this.isChecked == value)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.isChecked = value;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnValueChanged();
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x00057C7A File Offset: 0x00055E7A
		// (set) Token: 0x06001647 RID: 5703 RVA: 0x00057C82 File Offset: 0x00055E82
		[Description("Returns true if this checkbox is checked")]
		public bool Value
		{
			get
			{
				return this.Checked;
			}
			set
			{
				this.Checked = value;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x00057C8B File Offset: 0x00055E8B
		// (set) Token: 0x06001649 RID: 5705 RVA: 0x00057C93 File Offset: 0x00055E93
		public Label Label { get; protected set; }

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x00057C9C File Offset: 0x00055E9C
		// (set) Token: 0x0600164B RID: 5707 RVA: 0x00057CAF File Offset: 0x00055EAF
		public string LabelText
		{
			get
			{
				Label label = this.Label;
				if (label == null)
				{
					return null;
				}
				return label.Text;
			}
			set
			{
				if (this.Label == null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Label = base.Add.Label(null, null);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Label.Text = value;
			}
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x00057CE2 File Offset: 0x00055EE2
		public Checkbox()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddClass("checkbox");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CheckMark = base.Add.Icon("check", "checkmark");
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x00057D1C File Offset: 0x00055F1C
		public override void SetProperty(string name, string value)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetProperty(name, value);
			if (name == "checked" || name == "value")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Checked = value.ToBool();
			}
			if (name == "text")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.LabelText = value;
			}
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x00057D7A File Offset: 0x00055F7A
		public override void SetContent(string value)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LabelText = (((value != null) ? value.Trim() : null) ?? "");
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x00057D9C File Offset: 0x00055F9C
		public virtual void OnValueChanged()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateState();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateEvent("onchange", this.Checked, null);
			if (this.Checked)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CreateEvent("onchecked", null, null);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateEvent("onunchecked", null, null);
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x00057E15 File Offset: 0x00056015
		protected virtual void UpdateState()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetClass("checked", this.Checked);
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x00057E30 File Offset: 0x00056030
		protected override void OnClick(MousePanelEvent e)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnClick(e);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Checked = !this.Checked;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.CreateValueEvent("checked", this.Checked);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.CreateValueEvent("value", this.Checked);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			e.StopPropagation();
		}

		// Token: 0x04000740 RID: 1856
		protected bool isChecked;
	}
}
