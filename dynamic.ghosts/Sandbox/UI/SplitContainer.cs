using System;
using System.Runtime.CompilerServices;
using Sandbox.Html;
using Sandbox.Internal;

namespace Sandbox.UI
{
	// Token: 0x020001CD RID: 461
	[Library("split")]
	[Description("A control that has two panes with a splitter in between. You can drag the splitter to change the size of the two panels.")]
	public class SplitContainer : Panel
	{
		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x000601C3 File Offset: 0x0005E3C3
		// (set) Token: 0x060016F6 RID: 5878 RVA: 0x000601CB File Offset: 0x0005E3CB
		[Description("The left, or top panel. Has class \"split-left\".")]
		public Panel Left { get; protected set; }

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x000601D4 File Offset: 0x0005E3D4
		// (set) Token: 0x060016F8 RID: 5880 RVA: 0x000601DC File Offset: 0x0005E3DC
		[Description("The left, or bottom panel. Has class \"split-right\".")]
		public Panel Right { get; protected set; }

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060016F9 RID: 5881 RVA: 0x000601E5 File Offset: 0x0005E3E5
		// (set) Token: 0x060016FA RID: 5882 RVA: 0x000601ED File Offset: 0x0005E3ED
		[Description("The splitter control")]
		public Panel Splitter { get; protected set; }

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060016FB RID: 5883 RVA: 0x000601F6 File Offset: 0x0005E3F6
		// (set) Token: 0x060016FC RID: 5884 RVA: 0x000601FE File Offset: 0x0005E3FE
		[Description("Returns true if splitter is being dragged")]
		public bool IsDragging { get; protected set; }

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060016FD RID: 5885 RVA: 0x00060207 File Offset: 0x0005E407
		// (set) Token: 0x060016FE RID: 5886 RVA: 0x0006020F File Offset: 0x0005E40F
		[Description("Should this be laid out vertically? If you set this to vertical you should mentally change Left to Top and Right to Bottom.")]
		public bool Vertical
		{
			get
			{
				return this._vertical;
			}
			set
			{
				if (this._vertical == value)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this._vertical = value;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.SetClass("vertical", this._vertical);
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x0006023D File Offset: 0x0005E43D
		// (set) Token: 0x06001700 RID: 5888 RVA: 0x00060248 File Offset: 0x0005E448
		[Description("We can save the position of this splitter in a cookie. To do that set this (or \"cookie\" in a template). We'll automatically save and restore from the cookie.")]
		public string FractionCookie
		{
			get
			{
				return this._cookie;
			}
			set
			{
				if (this._cookie == value)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this._cookie = value;
				if (string.IsNullOrEmpty(this._cookie))
				{
					return;
				}
				float fr = GlobalGameNamespace.Cookie.Get<float>("splitter." + this._cookie, -1f);
				if (fr >= 0f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.UpdateSplitFraction(fr);
				}
			}
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x000602B4 File Offset: 0x0005E4B4
		public SplitContainer()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddClass("splitcontainer");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Left = base.Add.Panel("split-left");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Splitter = base.Add.Panel("splitter");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Right = base.Add.Panel("split-right");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Splitter.AddEventListener("onmousedown", new Action<PanelEvent>(this.StartDragging));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Splitter.AddEventListener("onmouseup", new Action<PanelEvent>(this.StopDragging));
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x00060389 File Offset: 0x0005E589
		[Description("The splitter has been pressed")]
		private void StartDragging(PanelEvent e)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetClass("dragging", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsDragging = true;
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x000603A8 File Offset: 0x0005E5A8
		[Description("The splitter has been released")]
		private void StopDragging(PanelEvent e)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetClass("dragging", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsDragging = false;
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x000603C8 File Offset: 0x0005E5C8
		[Description("If we're dragging then position the split where the mouse is.")]
		protected override void OnMouseMove(MousePanelEvent e)
		{
			if (this.IsDragging)
			{
				Vector2 local = base.ScreenPositionToPanelDelta(Mouse.Position);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.UpdateSplitFraction(this.Vertical ? local.y : local.x);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnMouseMove(e);
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x00060418 File Offset: 0x0005E618
		[Description("Sets the split fraction to this value. Will automatically adjust the value according to MinimumFraction parameters, and will save the new value to cookie.")]
		public virtual void UpdateSplitFraction(float f)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			f = MathF.Max(f, this.MinimumFractionLeft);
			if (1f - f < this.MinimumFractionRight)
			{
				f = 1f - this.MinimumFractionRight;
			}
			if (this.Vertical)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Left.Style.Height = Length.Fraction(f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Right.Style.Height = Length.Fraction(1f - f);
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Left.Style.Width = Length.Fraction(f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Right.Style.Width = Length.Fraction(1f - f);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Left.Style.Dirty();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Right.Style.Dirty();
			if (!string.IsNullOrWhiteSpace(this.FractionCookie))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Cookie.Set<float>("splitter." + this.FractionCookie, f);
			}
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x00060530 File Offset: 0x0005E730
		[Description("You can create child panels in the template by setting attributes on them, like slot=\"left\" to make that panel appear in the left panel.")]
		public override void OnTemplateSlot(INode element, string slotName, Panel panel)
		{
			if (slotName == "left")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				panel.Parent = this.Left;
				return;
			}
			if (slotName == "right")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				panel.Parent = this.Right;
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnTemplateSlot(element, slotName, panel);
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x0006058C File Offset: 0x0005E78C
		public override void SetProperty(string name, string value)
		{
			if (name == "direction")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Vertical = (value == "vertical");
			}
			if (name == "min-left")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.MinimumFractionLeft = value.ToFloat(0f);
			}
			if (name == "min-right")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.MinimumFractionRight = value.ToFloat(0f);
			}
			if (name == "default-left")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.UpdateSplitFraction(value.ToFloat(0f));
			}
			if (name == "default-right")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.UpdateSplitFraction(1f - value.ToFloat(0f));
			}
			if (name == "cookie")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.FractionCookie = value;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetProperty(name, value);
		}

		// Token: 0x04000771 RID: 1905
		public float MinimumFractionLeft = 0.2f;

		// Token: 0x04000772 RID: 1906
		public float MinimumFractionRight = 0.2f;

		// Token: 0x04000774 RID: 1908
		private bool _vertical;

		// Token: 0x04000775 RID: 1909
		private string _cookie;
	}
}
