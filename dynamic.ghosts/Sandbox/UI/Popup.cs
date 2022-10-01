using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox.UI.Construct;

namespace Sandbox.UI
{
	// Token: 0x020001C8 RID: 456
	public class Popup : Panel
	{
		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x060016A4 RID: 5796 RVA: 0x0005F13F File Offset: 0x0005D33F
		// (set) Token: 0x060016A5 RID: 5797 RVA: 0x0005F147 File Offset: 0x0005D347
		public Panel PopupSource { get; set; }

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x0005F150 File Offset: 0x0005D350
		// (set) Token: 0x060016A7 RID: 5799 RVA: 0x0005F158 File Offset: 0x0005D358
		public Panel SelectedChild { get; set; }

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x0005F161 File Offset: 0x0005D361
		// (set) Token: 0x060016A9 RID: 5801 RVA: 0x0005F169 File Offset: 0x0005D369
		public Popup.PositionMode Position { get; set; }

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x0005F172 File Offset: 0x0005D372
		// (set) Token: 0x060016AB RID: 5803 RVA: 0x0005F17A File Offset: 0x0005D37A
		public float PopupSourceOffset { get; set; }

		// Token: 0x060016AC RID: 5804 RVA: 0x0005F184 File Offset: 0x0005D384
		public Popup(Panel sourcePanel, Popup.PositionMode position, float offset)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Parent = sourcePanel.FindPopupPanel();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PopupSource = sourcePanel;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Position = position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PopupSourceOffset = offset;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Popup.AllPopups.Add(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddClass("popup-panel");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PositionMe();
			switch (this.Position)
			{
			case Popup.PositionMode.AboveLeft:
				base.AddClass("above-left");
				return;
			case Popup.PositionMode.BelowLeft:
				base.AddClass("below-left");
				return;
			case Popup.PositionMode.BelowCenter:
				base.AddClass("below-center");
				return;
			case Popup.PositionMode.BelowStretch:
				base.AddClass("below-stretch");
				return;
			default:
				return;
			}
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x0005F244 File Offset: 0x0005D444
		public override void OnDeleted()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnDeleted();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Popup.AllPopups.Remove(this);
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x0005F264 File Offset: 0x0005D464
		private void CreateHeader()
		{
			if (this.Header != null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Header = base.Add.Panel("header");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IconPanel = this.Header.Add.Icon(null, null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TitleLabel = this.Header.Add.Label(null, "title");
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x0005F2D6 File Offset: 0x0005D4D6
		// (set) Token: 0x060016B0 RID: 5808 RVA: 0x0005F2E9 File Offset: 0x0005D4E9
		public string Title
		{
			get
			{
				Label titleLabel = this.TitleLabel;
				if (titleLabel == null)
				{
					return null;
				}
				return titleLabel.Text;
			}
			set
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CreateHeader();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TitleLabel.Text = value;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x0005F307 File Offset: 0x0005D507
		// (set) Token: 0x060016B2 RID: 5810 RVA: 0x0005F31A File Offset: 0x0005D51A
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
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CreateHeader();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.IconPanel.Text = value;
			}
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x0005F338 File Offset: 0x0005D538
		[Description("Closes all panels, marks this one as a success and closes it.")]
		public void Success()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddClass("success");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Popup.CloseAll(null);
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x0005F355 File Offset: 0x0005D555
		[Description("Closes all panels, marks this one as a failure and closes it.")]
		public void Failure()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddClass("failure");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Popup.CloseAll(null);
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x0005F374 File Offset: 0x0005D574
		public Panel AddOption(string text, Action action = null)
		{
			return base.Add.Button(text, delegate()
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Popup.CloseAll(null);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Action action2 = action;
				if (action2 == null)
				{
					return;
				}
				action2();
			});
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x0005F3A8 File Offset: 0x0005D5A8
		public Panel AddOption(string text, string icon, Action action = null)
		{
			return base.Add.ButtonWithIcon(text, icon, null, delegate
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Popup.CloseAll(null);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Action action2 = action;
				if (action2 == null)
				{
					return;
				}
				action2();
			});
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x0005F3DC File Offset: 0x0005D5DC
		public void MoveSelection(int dir)
		{
			int currentIndex = base.GetChildIndex(this.SelectedChild);
			if (currentIndex >= 0)
			{
				currentIndex += dir;
			}
			else if (currentIndex < 0)
			{
				currentIndex = ((dir == 1) ? 0 : -1);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Panel selectedChild = this.SelectedChild;
			if (selectedChild != null)
			{
				selectedChild.SetClass("active", false);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SelectedChild = base.GetChild(currentIndex, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Panel selectedChild2 = this.SelectedChild;
			if (selectedChild2 == null)
			{
				return;
			}
			selectedChild2.SetClass("active", true);
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x0005F457 File Offset: 0x0005D657
		public override void Tick()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Tick();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PositionMe();
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x0005F470 File Offset: 0x0005D670
		public override void OnLayout(ref Rect layoutRect)
		{
			int padding = 10;
			float h = Screen.Height - (float)padding;
			float w = Screen.Width - (float)padding;
			if (layoutRect.bottom > h)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				layoutRect.top -= layoutRect.bottom - h;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				layoutRect.bottom -= layoutRect.bottom - h;
			}
			if (layoutRect.right > w)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				layoutRect.left -= layoutRect.right - w;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				layoutRect.right -= layoutRect.right - w;
			}
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x0005F500 File Offset: 0x0005D700
		private void PositionMe()
		{
			Rect rect = this.PopupSource.Box.Rect * this.PopupSource.ScaleFromScreen;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Style.MaxHeight = new Length?(Screen.Height - 50f);
			switch (this.Position)
			{
			case Popup.PositionMode.AboveLeft:
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Style.Left = new Length?(rect.left);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Style.Bottom = new Length?(base.Parent.Box.Rect.Height - rect.top + this.PopupSourceOffset);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Style.BackgroundColor = new Color?(Color.Red);
				break;
			case Popup.PositionMode.BelowLeft:
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Style.Left = new Length?(rect.left);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Style.Top = new Length?(rect.bottom + this.PopupSourceOffset);
				break;
			case Popup.PositionMode.BelowCenter:
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Style.Left = new Length?(rect.Center.x);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Style.Top = new Length?(rect.bottom + this.PopupSourceOffset);
				break;
			case Popup.PositionMode.BelowStretch:
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Style.Left = new Length?(rect.left);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Style.Width = new Length?(rect.Width);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Style.Top = new Length?(rect.bottom + this.PopupSourceOffset);
				break;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Style.Dirty();
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x0005F708 File Offset: 0x0005D908
		public static void CloseAll(Panel exceptThisOne = null)
		{
			foreach (Popup panel in Popup.AllPopups.ToArray())
			{
				if (panel != exceptThisOne)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					panel.Delete(false);
				}
			}
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x0005F744 File Offset: 0x0005D944
		[Event("ui.closepopups")]
		public static void ClosePopupsEvent(object obj)
		{
			Popup floater = null;
			Panel panel = obj as Panel;
			if (panel != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				floater = panel.AncestorsAndSelf.OfType<Popup>().FirstOrDefault<Popup>();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Popup.CloseAll(floater);
		}

		// Token: 0x0400075B RID: 1883
		protected Panel Header;

		// Token: 0x0400075C RID: 1884
		protected Label TitleLabel;

		// Token: 0x0400075D RID: 1885
		protected IconPanel IconPanel;

		// Token: 0x0400075E RID: 1886
		private static List<Popup> AllPopups = new List<Popup>();

		// Token: 0x02000263 RID: 611
		public enum PositionMode
		{
			// Token: 0x04000A12 RID: 2578
			AboveLeft,
			// Token: 0x04000A13 RID: 2579
			BelowLeft,
			// Token: 0x04000A14 RID: 2580
			BelowCenter,
			// Token: 0x04000A15 RID: 2581
			BelowStretch
		}
	}
}
