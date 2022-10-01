using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox.Html;
using Sandbox.Internal;

namespace Sandbox.UI
{
	// Token: 0x020001CF RID: 463
	[Library("tabcontainer")]
	[Alias(new string[]
	{
		"tabcontrol",
		"tabs"
	})]
	[Description("A container with tabs, allowing you to switch between different sheets.  You can position the tabs by adding the class tabs-bottom, tabs-left, tabs-right (default is tabs top)")]
	public class TabContainer : Panel
	{
		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x000606EA File Offset: 0x0005E8EA
		// (set) Token: 0x0600170A RID: 5898 RVA: 0x000606F2 File Offset: 0x0005E8F2
		[Description("A control housing the tabs")]
		public Panel TabsContainer { get; protected set; }

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x0600170B RID: 5899 RVA: 0x000606FB File Offset: 0x0005E8FB
		// (set) Token: 0x0600170C RID: 5900 RVA: 0x00060703 File Offset: 0x0005E903
		[Description("A control housing the sheets")]
		public Panel SheetContainer { get; protected set; }

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x0006070C File Offset: 0x0005E90C
		// (set) Token: 0x0600170E RID: 5902 RVA: 0x00060714 File Offset: 0x0005E914
		[Description("If a cookie is set then the selected tab will be saved and restored.")]
		public string TabCookie { get; set; }

		// Token: 0x17000659 RID: 1625
		// (set) Token: 0x0600170F RID: 5903 RVA: 0x0006071D File Offset: 0x0005E91D
		[Description("If true we will act as a tab bar and have no body.")]
		public bool NoBody
		{
			set
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SheetContainer.Style.Display = new DisplayMode?(value ? DisplayMode.None : DisplayMode.Flex);
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x00060740 File Offset: 0x0005E940
		// (set) Token: 0x06001711 RID: 5905 RVA: 0x00060748 File Offset: 0x0005E948
		[Description("The tab that is active")]
		public string ActiveTab
		{
			get
			{
				return this._activeTab;
			}
			set
			{
				if (this._activeTab == value)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this._activeTab = value;
				TabContainer.Tab t = this.Tabs.FirstOrDefault((TabContainer.Tab x) => x.TabName == this._activeTab);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SwitchTab(t, true);
			}
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x00060798 File Offset: 0x0005E998
		public TabContainer()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddClass("tabcontainer");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TabsContainer = base.Add.Panel("tabs");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SheetContainer = base.Add.Panel("sheets");
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x00060802 File Offset: 0x0005EA02
		public override void SetProperty(string name, string value)
		{
			if (name == "cookie")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TabCookie = value;
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetProperty(name, value);
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x0006082C File Offset: 0x0005EA2C
		[Description("Add a tab to the sheet")]
		public TabContainer.Tab AddTab(Panel panel, string tabName, string title, string icon = null)
		{
			int index = this.Tabs.Count;
			TabContainer.Tab tab = new TabContainer.Tab(this, title, icon, panel);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			tab.TabName = tabName;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tabs.Add(tab);
			int cookieIndex = string.IsNullOrWhiteSpace(this.TabCookie) ? -1 : GlobalGameNamespace.Cookie.Get<int>("dropdown." + this.TabCookie, -1);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			panel.Parent = this.SheetContainer;
			if (index == 0 || cookieIndex == index)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SwitchTab(tab, false);
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				tab.Active = false;
			}
			return tab;
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x000608D0 File Offset: 0x0005EAD0
		public override void OnTemplateSlot(INode element, string slotName, Panel panel)
		{
			if (slotName == "tab")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.AddTab(panel, element.GetAttribute("tabName", null), element.GetAttribute("tabtext", null), element.GetAttribute("tabicon", null));
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnTemplateSlot(element, slotName, panel);
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x0006092C File Offset: 0x0005EB2C
		[Description("Switch to a specific tab")]
		public void SwitchTab(TabContainer.Tab tab, bool setCookie = true)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ActiveTab = tab.TabName;
			foreach (TabContainer.Tab tab2 in this.Tabs)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				tab2.Active = (tab2 == tab);
			}
			if (setCookie && !string.IsNullOrEmpty(this.TabCookie))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Cookie.Set<int>("dropdown." + this.TabCookie, this.Tabs.IndexOf(tab));
			}
		}

		// Token: 0x04000778 RID: 1912
		public List<TabContainer.Tab> Tabs = new List<TabContainer.Tab>();

		// Token: 0x0400077A RID: 1914
		private string _activeTab;

		// Token: 0x02000266 RID: 614
		[Description("Holds a Tab button and a Page for each sheet on the TabControl")]
		public class Tab
		{
			// Token: 0x170006FE RID: 1790
			// (get) Token: 0x060019C7 RID: 6599 RVA: 0x0006BCE6 File Offset: 0x00069EE6
			// (set) Token: 0x060019C8 RID: 6600 RVA: 0x0006BCEE File Offset: 0x00069EEE
			public Button Button { get; protected set; }

			// Token: 0x170006FF RID: 1791
			// (get) Token: 0x060019C9 RID: 6601 RVA: 0x0006BCF7 File Offset: 0x00069EF7
			// (set) Token: 0x060019CA RID: 6602 RVA: 0x0006BCFF File Offset: 0x00069EFF
			public Panel Page { get; protected set; }

			// Token: 0x17000700 RID: 1792
			// (get) Token: 0x060019CB RID: 6603 RVA: 0x0006BD08 File Offset: 0x00069F08
			// (set) Token: 0x060019CC RID: 6604 RVA: 0x0006BD10 File Offset: 0x00069F10
			public string TabName { get; set; }

			// Token: 0x060019CD RID: 6605 RVA: 0x0006BD1C File Offset: 0x00069F1C
			public Tab(TabContainer tabControl, string title, string icon, Panel panel)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Parent = tabControl;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Page = panel;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Button = new Button(title, icon, delegate()
				{
					TabContainer parent = this.Parent;
					if (parent == null)
					{
						return;
					}
					parent.SwitchTab(this, true);
				});
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Button.Parent = tabControl.TabsContainer;
			}

			// Token: 0x17000701 RID: 1793
			// (get) Token: 0x060019CE RID: 6606 RVA: 0x0006BD7C File Offset: 0x00069F7C
			// (set) Token: 0x060019CF RID: 6607 RVA: 0x0006BD84 File Offset: 0x00069F84
			[Description("Change appearance based on active status")]
			public bool Active
			{
				get
				{
					return this.active;
				}
				set
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.active = value;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Button.SetClass("active", value);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Page.SetClass("active", value);
				}
			}

			// Token: 0x04000A18 RID: 2584
			private TabContainer Parent;

			// Token: 0x04000A1C RID: 2588
			private bool active;
		}
	}
}
