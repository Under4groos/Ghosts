using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Sandbox.UI.Tests
{
	// Token: 0x020001D8 RID: 472
	public abstract class BaseVirtualScrollPanel<T> : Panel where T : class
	{
		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x00063434 File Offset: 0x00061634
		// (set) Token: 0x060017C1 RID: 6081 RVA: 0x0006343C File Offset: 0x0006163C
		public bool NeedsRebuild { get; set; }

		// Token: 0x060017C2 RID: 6082 RVA: 0x00063448 File Offset: 0x00061648
		public BaseVirtualScrollPanel()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Style.Overflow = new OverflowMode?(OverflowMode.Scroll);
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x000634A0 File Offset: 0x000616A0
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
			int hash = HashCode.Combine<int>(this.ItemCount());
			if (hash != this.LastHash)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.LastHash = hash;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.NeedsRebuild = true;
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

		// Token: 0x060017C4 RID: 6084 RVA: 0x0006358C File Offset: 0x0006178C
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

		// Token: 0x060017C5 RID: 6085
		[Description("Return true if we have this data slot")]
		public abstract bool HasData(int i);

		// Token: 0x060017C6 RID: 6086
		public abstract int ItemCount();

		// Token: 0x060017C7 RID: 6087
		public abstract T GetItem(int i);

		// Token: 0x060017C8 RID: 6088 RVA: 0x00063640 File Offset: 0x00061840
		public void RefreshCreated(int i)
		{
			if (!this.HasData(i) || i < 0)
			{
				return;
			}
			T data = this.GetItem(i);
			bool needsRebuild = true;
			T lastData;
			if (this.CellData.TryGetValue(i, out lastData))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				needsRebuild = (lastData != data);
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
				this.CellData[i] = data;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnCellCreated(i, data, panel);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Layout.Position(i, panel);
		}

		// Token: 0x060017C9 RID: 6089
		public abstract void OnCellCreated(int index, T item, Panel cell);

		// Token: 0x060017CA RID: 6090 RVA: 0x00063728 File Offset: 0x00061928
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
			rect.Height = MathF.Max(this.Layout.GetHeight(this.ItemCount()) * base.ScaleToScreen, rect.Height);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ConstrainScrolling(rect.Size);
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x000637F0 File Offset: 0x000619F0
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

		// Token: 0x040007AD RID: 1965
		private Dictionary<int, T> CellData = new Dictionary<int, T>();

		// Token: 0x040007AE RID: 1966
		public Dictionary<int, Panel> CreatedPanels = new Dictionary<int, Panel>();

		// Token: 0x040007AF RID: 1967
		private List<int> RemoveQueue = new List<int>();

		// Token: 0x040007B0 RID: 1968
		public GridLayout Layout = new GridLayout();

		// Token: 0x040007B2 RID: 1970
		internal int LastHash;
	}
}
