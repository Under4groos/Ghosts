using System;
using System.Runtime.CompilerServices;
using Sandbox.UI;

namespace Sandbox
{
	// Token: 0x02000146 RID: 326
	[Category("Setup")]
	[Title("Hud Entity")]
	[Icon("branding_watermark")]
	[Description("A base HUD entity that lets you define which type of RootPanel to create.")]
	public abstract class HudEntity<T> : Entity where T : RootPanel, new()
	{
		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x0003C3DD File Offset: 0x0003A5DD
		// (set) Token: 0x06000EEA RID: 3818 RVA: 0x0003C3E5 File Offset: 0x0003A5E5
		public T RootPanel { get; set; }

		// Token: 0x06000EEB RID: 3819 RVA: 0x0003C3EE File Offset: 0x0003A5EE
		public HudEntity()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Transmit = TransmitType.Always;
			if (base.IsClient)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CreateRootPanel();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Local.Hud = this.RootPanel;
			}
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0003C42A File Offset: 0x0003A62A
		[Description("Create the root panel, T")]
		public virtual void CreateRootPanel()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RootPanel = Activator.CreateInstance<T>();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RootPanel.AcceptsFocus = false;
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0003C454 File Offset: 0x0003A654
		protected override void OnDestroy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnDestroy();
			if (Local.Hud == this.RootPanel)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Local.Hud = null;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			T t = this.RootPanel;
			if (t != null)
			{
				t.Delete(true);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RootPanel = default(T);
		}
	}
}
