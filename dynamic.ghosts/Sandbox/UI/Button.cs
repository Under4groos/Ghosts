using System;
using System.Runtime.CompilerServices;
using Sandbox.UI.Construct;

namespace Sandbox.UI
{
	// Token: 0x020001BB RID: 443
	[Library("Button")]
	public class Button : Panel
	{
		// Token: 0x0600162C RID: 5676 RVA: 0x000577AB File Offset: 0x000559AB
		public Button()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddClass("button");
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x000577C3 File Offset: 0x000559C3
		public Button(string text) : this()
		{
			if (text != null)
			{
				this.Text = text;
			}
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x000577D5 File Offset: 0x000559D5
		public Button(string text, string icon) : this()
		{
			if (icon != null)
			{
				this.Icon = icon;
			}
			if (text != null)
			{
				this.Text = text;
			}
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x000577F1 File Offset: 0x000559F1
		public Button(string text, string icon, Action onClick) : this(text, icon)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddEventListener("onclick", onClick);
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001630 RID: 5680 RVA: 0x0005780C File Offset: 0x00055A0C
		// (set) Token: 0x06001631 RID: 5681 RVA: 0x00057820 File Offset: 0x00055A20
		public string Text
		{
			get
			{
				Label textLabel = this.TextLabel;
				if (textLabel == null)
				{
					return null;
				}
				return textLabel.Text;
			}
			set
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				if (this.TextLabel == null)
				{
					this.TextLabel = base.Add.Label(value, "button-label");
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TextLabel.Text = value;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.SetClass("has-label", this.TextLabel != null);
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x0005787B File Offset: 0x00055A7B
		// (set) Token: 0x06001633 RID: 5683 RVA: 0x00057890 File Offset: 0x00055A90
		public string Subtitle
		{
			get
			{
				Label subtitleLabel = this.SubtitleLabel;
				if (subtitleLabel == null)
				{
					return null;
				}
				return subtitleLabel.Text;
			}
			set
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				if (this.SubtitleLabel == null)
				{
					this.SubtitleLabel = base.Add.Label(value, "button-subtitle");
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SubtitleLabel.Text = value;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.SetClass("has-subtitle", this.SubtitleLabel != null);
			}
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x000578EB File Offset: 0x00055AEB
		public void DeleteText()
		{
			if (this.TextLabel == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Label textLabel = this.TextLabel;
			if (textLabel != null)
			{
				textLabel.Delete(false);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TextLabel = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.RemoveClass("has-label");
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001635 RID: 5685 RVA: 0x00057929 File Offset: 0x00055B29
		// (set) Token: 0x06001636 RID: 5686 RVA: 0x0005793C File Offset: 0x00055B3C
		public string Icon
		{
			get
			{
				IconPanel iconPanel = this.IconPanel;
				if (iconPanel == null)
				{
					return null;
				}
				return iconPanel.Text;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					IconPanel iconPanel = this.IconPanel;
					if (iconPanel != null)
					{
						iconPanel.Delete(true);
					}
				}
				else
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					if (this.IconPanel == null)
					{
						this.IconPanel = base.Add.Icon(value, null);
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.IconPanel.Text = value;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.SetClass("has-icon", this.IconPanel != null);
			}
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x000579B4 File Offset: 0x00055BB4
		public void DeleteIcon()
		{
			if (this.IconPanel == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			IconPanel iconPanel = this.IconPanel;
			if (iconPanel != null)
			{
				iconPanel.Delete(false);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IconPanel = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.RemoveClass("has-icon");
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x000579F2 File Offset: 0x00055BF2
		[Description("Calls Text = value")]
		public virtual void SetText(string text)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Text = text;
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x00057A00 File Offset: 0x00055C00
		public override void SetProperty(string name, string value)
		{
			if (name == "text")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SetText(value);
				return;
			}
			if (name == "html")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SetText(value);
				return;
			}
			if (!(name == "icon"))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.SetProperty(name, value);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Icon = value;
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x00057A6A File Offset: 0x00055C6A
		public void Click()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateEvent(new MousePanelEvent("onclick", this, "mouseleft"));
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x00057A87 File Offset: 0x00055C87
		public override void SetContent(string value)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetText(((value != null) ? value.Trim() : null) ?? "");
		}

		// Token: 0x0400073B RID: 1851
		protected Label TextLabel;

		// Token: 0x0400073C RID: 1852
		protected Label SubtitleLabel;

		// Token: 0x0400073D RID: 1853
		protected IconPanel IconPanel;
	}
}
