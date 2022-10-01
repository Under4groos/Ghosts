using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Sandbox.UI.Tests
{
	// Token: 0x020001D7 RID: 471
	[Library("virtualscrollpanel")]
	[Description("Scroll panel that creates its contents as they become visible  TODO: we need to let panels know, or recreate them, when Data changes")]
	public class VirtualScrollPanel : Panel
	{
		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x00062E8F File Offset: 0x0006108F
		// (set) Token: 0x060017AF RID: 6063 RVA: 0x00062E97 File Offset: 0x00061097
		public virtual List<object> Data { get; set; } = new List<object>();

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x00062EA0 File Offset: 0x000610A0
		// (set) Token: 0x060017B1 RID: 6065 RVA: 0x00062EA8 File Offset: 0x000610A8
		public bool NeedsRebuild { get; set; }

		// Token: 0x060017B2 RID: 6066 RVA: 0x00062EB1 File Offset: 0x000610B1
		public virtual void AddItem(object item)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Data.Add(item);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.NeedsRebuild = true;
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x00062ED0 File Offset: 0x000610D0
		public void AddItems(object[] items)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Data.AddRange(items);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.NeedsRebuild = true;
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x00062EF0 File Offset: 0x000610F0
		public virtual void Clear()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Data.Clear();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.NeedsRebuild = true;
			foreach (KeyValuePair<int, Panel> p in this.CreatedPanels)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				p.Value.Delete(true);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreatedPanels.Clear();
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x00062F7C File Offset: 0x0006117C
		public override void Tick()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Tick();
			if (base.ComputedStyle == null)
			{
				return;
			}
			if (!base.IsVisible)
			{
				return;
			}
			if (this.Layout.Update(base.Box, base.ScaleFromScreen, base.ScrollOffset.y * base.ScaleFromScreen, base.ComputedStyle.JustifyContent.GetValueOrDefault()) || this.NeedsRebuild)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.NeedsRebuild = false;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				int firstVisibleIndex;
				int lastVisibleIndex;
				this.Layout.GetVisibleRange(out firstVisibleIndex, out lastVisibleIndex);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DeleteNotVisible(firstVisibleIndex, lastVisibleIndex);
				for (int i = firstVisibleIndex; i < lastVisibleIndex; i++)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.RefreshCreated(i);
				}
			}
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x00063039 File Offset: 0x00061239
		public void SetItems(IEnumerable<object> enumerable)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Data.Clear();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Data.AddRange(enumerable);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.NeedsRebuild = true;
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x00063068 File Offset: 0x00061268
		private void DeleteNotVisible(int min, int max)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RemoveQueue.Clear();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RemoveQueue.AddRange(from x in this.CreatedPanels.Keys
			where x < min || x > max || !this.HasData(x)
			select x);
			for (int i = 0; i < this.RemoveQueue.Count; i++)
			{
				int idx = this.RemoveQueue[i];
				Panel panel;
				if (this.CreatedPanels.Remove(idx, out panel))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					panel.Delete(true);
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RemoveQueue.Clear();
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x0006311B File Offset: 0x0006131B
		[Description("Return true if we have this data slot")]
		public bool HasData(int i)
		{
			return i < this.Data.Count && i >= 0;
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x00063134 File Offset: 0x00061334
		public void RefreshCreated(int i)
		{
			if (this.Data.Count <= i || i < 0)
			{
				return;
			}
			bool needsRebuild = true;
			object lastData;
			if (this.CellData.TryGetValue(i, out lastData))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				needsRebuild = (lastData != this.Data[i]);
			}
			Panel panel;
			if (!this.CreatedPanels.TryGetValue(i, out panel) || needsRebuild)
			{
				if (needsRebuild)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					if (panel != null)
					{
						panel.Delete(false);
					}
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				panel = base.Add.Panel("cell");
				RuntimeHelpers.EnsureSufficientExecutionStack();
				panel.Style.Position = new PositionMode?(PositionMode.Absolute);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CreatedPanels[i] = panel;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CellData[i] = this.Data[i];
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnCellCreated(i, panel);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Layout.Position(i, panel);
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x060017BA RID: 6074 RVA: 0x00063222 File Offset: 0x00061422
		// (set) Token: 0x060017BB RID: 6075 RVA: 0x0006322A File Offset: 0x0006142A
		[Description("Create a new panel. You should add a child to the passed panel (which is the cell).")]
		public Action<Panel, object> OnCreateCell { get; set; }

		// Token: 0x060017BC RID: 6076 RVA: 0x00063234 File Offset: 0x00061434
		public virtual void OnCellCreated(int i, Panel cell)
		{
			object data = this.Data[i];
			if (this.OnCreateCell != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnCreateCell(cell, data);
			}
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x00063268 File Offset: 0x00061468
		protected override void FinalLayoutChildren()
		{
			foreach (KeyValuePair<int, Panel> entry in this.CreatedPanels)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				entry.Value.FinalLayout();
			}
			Rect rect = base.Box.Rect;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			rect.Position -= base.ScrollOffset;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			rect.Height = MathF.Max(this.Layout.GetHeight(this.Data.Count) * base.ScaleToScreen, rect.Height);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ConstrainScrolling(rect.Size);
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x00063338 File Offset: 0x00061538
		public override void SetProperty(string name, string value)
		{
			if (name == "item-width")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Layout.ItemWidth = (Length.Parse(value) ?? 100f);
			}
			if (name == "item-height")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Layout.ItemHeight = (Length.Parse(value) ?? 100f);
			}
			if (name == "auto-columns")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Layout.AutoColumns = value.ToBool();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetProperty(name, value);
		}

		// Token: 0x040007A6 RID: 1958
		private Dictionary<int, object> CellData = new Dictionary<int, object>();

		// Token: 0x040007A8 RID: 1960
		public Dictionary<int, Panel> CreatedPanels = new Dictionary<int, Panel>();

		// Token: 0x040007A9 RID: 1961
		private List<int> RemoveQueue = new List<int>();

		// Token: 0x040007AA RID: 1962
		public GridLayout Layout = new GridLayout();
	}
}
