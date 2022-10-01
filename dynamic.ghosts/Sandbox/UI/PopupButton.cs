using System;
using System.Runtime.CompilerServices;

namespace Sandbox.UI
{
	// Token: 0x020001C9 RID: 457
	[Description("A button that opens a popup panel. Useless on its own - you need to implement Open")]
	public abstract class PopupButton : Button
	{
		// Token: 0x060016BE RID: 5822 RVA: 0x0005F78A File Offset: 0x0005D98A
		public PopupButton()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddClass("popupbutton");
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x0005F7A2 File Offset: 0x0005D9A2
		protected override void OnClick(MousePanelEvent e)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnClick(e);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Open();
		}

		// Token: 0x060016C0 RID: 5824
		public abstract void Open();

		// Token: 0x060016C1 RID: 5825 RVA: 0x0005F7BC File Offset: 0x0005D9BC
		public override void Tick()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Tick();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetClass("open", this.Popup != null && !this.Popup.IsDeleting);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetClass("active", this.Popup != null && !this.Popup.IsDeleting);
			if (this.Popup != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Popup.Style.Width = new Length?(base.Box.Rect.Width);
			}
		}

		// Token: 0x0400075F RID: 1887
		protected Popup Popup;
	}
}
