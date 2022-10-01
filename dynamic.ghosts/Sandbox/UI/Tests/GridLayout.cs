using System;
using System.Runtime.CompilerServices;

namespace Sandbox.UI.Tests
{
	// Token: 0x020001DA RID: 474
	public class GridLayout
	{
		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x000638C4 File Offset: 0x00061AC4
		// (set) Token: 0x060017CF RID: 6095 RVA: 0x000638CC File Offset: 0x00061ACC
		[Description("The fixed size of each item. If x is lower than 0 then we'll stretch to fill the size.")]
		public Length ItemWidth { get; set; }

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x000638D5 File Offset: 0x00061AD5
		// (set) Token: 0x060017D1 RID: 6097 RVA: 0x000638DD File Offset: 0x00061ADD
		public Length ItemHeight { get; set; }

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x000638E6 File Offset: 0x00061AE6
		// (set) Token: 0x060017D3 RID: 6099 RVA: 0x000638EE File Offset: 0x00061AEE
		[Description("Should we update Columns automatically?")]
		public bool AutoColumns { get; set; }

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x000638F7 File Offset: 0x00061AF7
		// (set) Token: 0x060017D5 RID: 6101 RVA: 0x000638FF File Offset: 0x00061AFF
		[Description("How many columns should we have?")]
		public int Columns { get; set; }

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x00063908 File Offset: 0x00061B08
		// (set) Token: 0x060017D7 RID: 6103 RVA: 0x00063910 File Offset: 0x00061B10
		[Description("The Rect of this layout. Set via Update.")]
		public Rect Rect { get; protected set; }

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x00063919 File Offset: 0x00061B19
		// (set) Token: 0x060017D9 RID: 6105 RVA: 0x00063921 File Offset: 0x00061B21
		[Description("Where the top of the visible space is")]
		public float ScrollOffset { get; protected set; }

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x0006392A File Offset: 0x00061B2A
		// (set) Token: 0x060017DB RID: 6107 RVA: 0x00063932 File Offset: 0x00061B32
		[Description("How columns should be justified")]
		public Justify Justify { get; protected set; }

		// Token: 0x060017DC RID: 6108 RVA: 0x0006393C File Offset: 0x00061B3C
		public GridLayout()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ItemWidth = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ItemHeight = 100f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Columns = 1;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AutoColumns = false;
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00063994 File Offset: 0x00061B94
		[Description("Update specifics of this layout. Returns true if we're dirty.")]
		public bool Update(Box box, float scaleFromScreen, float scrollOffset, Justify justify)
		{
			int hash = HashCode.Combine<Rect, float, float, bool, int, Length, Length, Justify>(box.RectInner, scaleFromScreen, scrollOffset, this.AutoColumns, this.Columns, this.ItemWidth, this.ItemHeight, justify);
			if (hash == this.updateHash)
			{
				return false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.updateHash = hash;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.cellSize.x = this.ItemWidth.GetPixels(box.Rect.Width * scaleFromScreen);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.cellSize.y = this.ItemHeight.GetPixels(box.Rect.Height * scaleFromScreen);
			if (this.cellSize.y < 1f)
			{
				this.cellSize.y = 1f;
			}
			if (this.cellSize.x < 0f)
			{
				this.cellSize.x = 0f;
			}
			Rect r = box.RectInner;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			r.Position = box.RectInner.Position - box.Rect.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Padding = (box.Rect.Size - box.RectInner.Size) * scaleFromScreen;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Rect = r * scaleFromScreen;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ScrollOffset = scrollOffset;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Justify = justify;
			if (this.AutoColumns)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Columns = (this.Rect.Width / this.cellSize.x).FloorToInt();
				if (this.Columns <= 0)
				{
					this.Columns = 1;
				}
			}
			return true;
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x00063B44 File Offset: 0x00061D44
		[Description("Get the range of cells that are visible")]
		public void GetVisibleRange(out int firstIndex, out int lastIndex)
		{
			float itemHeight = this.cellSize.y;
			int topItem = (this.ScrollOffset / itemHeight).FloorToInt();
			int inRange = (this.Rect.Height / itemHeight).CeilToInt() + 1;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			firstIndex = topItem * this.Columns;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			lastIndex = firstIndex + inRange * this.Columns;
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x00063BA4 File Offset: 0x00061DA4
		[Description("Get the position of this cell")]
		public Rect GetPosition(int index)
		{
			float x = (float)index.UnsignedMod(this.Columns);
			float y = (float)(index / this.Columns);
			float width = (this.cellSize.x > 0f) ? this.cellSize.x : this.Rect.Width;
			if (this.Justify == Justify.SpaceBetween)
			{
				float spareSpace = this.Rect.Width - (float)this.Columns * this.cellSize.x;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				spareSpace /= (float)(this.Columns - 1);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				x *= this.cellSize.x + spareSpace;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				y *= this.cellSize.y;
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				x *= this.cellSize.x;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				y *= this.cellSize.y;
			}
			return new Rect(x + this.Rect.left, y + this.Rect.top, width, this.cellSize.y);
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x00063CB4 File Offset: 0x00061EB4
		[Description("Move this panel into the position. This will set the Left/Top/Width/Height on the panel")]
		public void Position(int index, Panel panel)
		{
			Rect rect = this.GetPosition(index);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			panel.Style.Left = new Length?(rect.left);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			panel.Style.Top = new Length?(rect.top);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			panel.Style.Width = new Length?(rect.Width);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			panel.Style.Height = new Length?(rect.Height);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			panel.Style.Dirty();
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00063D5B File Offset: 0x00061F5B
		[Description("Get the full height if we have this many items")]
		public float GetHeight(int count)
		{
			return MathF.Ceiling((float)count / (float)this.Columns) * this.cellSize.y + this.Padding.y;
		}

		// Token: 0x040007BA RID: 1978
		private int updateHash;

		// Token: 0x040007BB RID: 1979
		private Vector2 Padding;

		// Token: 0x040007BC RID: 1980
		private Vector2 cellSize;
	}
}
