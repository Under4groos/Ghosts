using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox.Internal;
using Sandbox.UI;

namespace Sandbox
{
	// Token: 0x02000169 RID: 361
	[UseTemplate]
	internal class DevCamSettings : Panel
	{
		// Token: 0x06001076 RID: 4214 RVA: 0x000415BE File Offset: 0x0003F7BE
		public DevCamSettings()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Hide();
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x000415D4 File Offset: 0x0003F7D4
		public void TogglePanel(string name)
		{
			Panel panel = base.Children.FirstOrDefault((Panel x) => x.ElementName == name);
			if (panel == null)
			{
				return;
			}
			bool wasVisible = panel.HasClass("visible");
			foreach (Panel panel2 in from x in base.Children
			where x.HasClass("panel")
			select x)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				panel2.SetClass("visible", false);
			}
			if (wasVisible)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.HighlightBottom("none");
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			panel.SetClass("visible", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			panel.Style.Left = new Length?(Mouse.Position.x * base.ScaleFromScreen - 150f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HighlightBottom(name);
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x000416F0 File Offset: 0x0003F8F0
		private void HighlightBottom(string name)
		{
			foreach (Panel child in from x in base.Children.FirstOrDefault((Panel x) => x.HasClass("menubar")).Children
			where x.HasClass("button")
			select x)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				child.SetClass("active", child.GetAttribute("for", null) == name);
			}
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x000417A8 File Offset: 0x0003F9A8
		public override void Tick()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Tick();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Style.Cursor = ((DevCamSettings.OnMouseMoved == null) ? "" : "pointer");
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x000417D8 File Offset: 0x0003F9D8
		public void Activated()
		{
			if (GlobalGameNamespace.PostProcess.Get<DevCamPP>() == null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.PostProcess.Add<DevCamPP>(new DevCamPP());
			}
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x000417FC File Offset: 0x0003F9FC
		public void Deactivated()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Hide();
			DevCamPP pp = GlobalGameNamespace.PostProcess.Get<DevCamPP>();
			if (pp != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.PostProcess.Remove<DevCamPP>(pp);
			}
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00041832 File Offset: 0x0003FA32
		public void Show()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetClass("hidden", false);
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x00041845 File Offset: 0x0003FA45
		public void Hide()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetClass("hidden", true);
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x00041858 File Offset: 0x0003FA58
		protected override void OnClick(MousePanelEvent e)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnClick(e);
			if (e.Target != this)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Action<MousePanelEvent> onMouseClicked = DevCamSettings.OnMouseClicked;
			if (onMouseClicked == null)
			{
				return;
			}
			onMouseClicked(e);
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00041885 File Offset: 0x0003FA85
		protected override void OnMouseMove(MousePanelEvent e)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnMouseMove(e);
			if (e.Target != this)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Action<MousePanelEvent> onMouseMoved = DevCamSettings.OnMouseMoved;
			if (onMouseMoved == null)
			{
				return;
			}
			onMouseMoved(e);
		}

		// Token: 0x04000530 RID: 1328
		internal static Action<MousePanelEvent> OnMouseClicked;

		// Token: 0x04000531 RID: 1329
		internal static Action<MousePanelEvent> OnMouseMoved;
	}
}
