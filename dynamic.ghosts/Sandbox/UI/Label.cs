using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Sandbox.Internal.UI;

namespace Sandbox.UI
{
	// Token: 0x020001C5 RID: 453
	[Library("label")]
	[Alias(new string[]
	{
		"text"
	})]
	public class Label : Panel
	{
		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x0005E5CB File Offset: 0x0005C7CB
		// (set) Token: 0x06001672 RID: 5746 RVA: 0x0005E5D8 File Offset: 0x0005C7D8
		public virtual string Text
		{
			get
			{
				return this.InternalLabel.Text;
			}
			set
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.InternalLabel.Text = value;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.StringInfo.String = (this.InternalLabel.Text ?? string.Empty);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CaretSantity();
			}
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x0005E625 File Offset: 0x0005C825
		[Description("Calls Text = value")]
		public virtual void SetText(string text)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Text = text;
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x0005E633 File Offset: 0x0005C833
		public override void SetProperty(string name, string value)
		{
			if (name == "text")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Text = value;
				return;
			}
			if (name == "selectable")
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetProperty(name, value);
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x0005E66A File Offset: 0x0005C86A
		// (set) Token: 0x06001676 RID: 5750 RVA: 0x0005E67D File Offset: 0x0005C87D
		[Description("If false then this label won't be selected, even if the parent has TextSelection enabled.")]
		public bool Selectable
		{
			get
			{
				TextPanel internalLabel = this.InternalLabel;
				return internalLabel != null && internalLabel.Selectable;
			}
			set
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.InternalLabel.Selectable = value;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x0005E690 File Offset: 0x0005C890
		// (set) Token: 0x06001678 RID: 5752 RVA: 0x0005E6A3 File Offset: 0x0005C8A3
		public bool ShouldDrawSelection
		{
			get
			{
				TextPanel internalLabel = this.InternalLabel;
				return internalLabel != null && internalLabel.ShouldDrawSelection;
			}
			set
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.InternalLabel.ShouldDrawSelection = value;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x0005E6B6 File Offset: 0x0005C8B6
		// (set) Token: 0x0600167A RID: 5754 RVA: 0x0005E6C9 File Offset: 0x0005C8C9
		public int SelectionStart
		{
			get
			{
				TextPanel internalLabel = this.InternalLabel;
				if (internalLabel == null)
				{
					return 0;
				}
				return internalLabel.SelectionStart;
			}
			set
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.InternalLabel.SelectionStart = value.Clamp(0, this.StringInfo.LengthInTextElements);
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x0005E6ED File Offset: 0x0005C8ED
		// (set) Token: 0x0600167C RID: 5756 RVA: 0x0005E700 File Offset: 0x0005C900
		public int SelectionEnd
		{
			get
			{
				TextPanel internalLabel = this.InternalLabel;
				if (internalLabel == null)
				{
					return 0;
				}
				return internalLabel.SelectionEnd;
			}
			set
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.InternalLabel.SelectionEnd = value.Clamp(0, this.StringInfo.LengthInTextElements);
			}
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x0005E724 File Offset: 0x0005C924
		public Label()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.InternalLabel = new TextPanel(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddClass("label");
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x0005E758 File Offset: 0x0005C958
		public Rect GetCaretRect(int i)
		{
			return this.InternalLabel.GetCaretRect(i);
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x0005E766 File Offset: 0x0005C966
		public int GetLetterAt(Vector2 pos)
		{
			return this.InternalLabel.GetLetterAt(pos);
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x0005E774 File Offset: 0x0005C974
		public int GetLetterAtScreenPosition(Vector2 pos)
		{
			return this.InternalLabel.GetLetterAtScreenPosition(pos);
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x0005E782 File Offset: 0x0005C982
		public override void SetContent(string value)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Text = (((value != null) ? value.Trim() : null) ?? "");
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x0005E7A4 File Offset: 0x0005C9A4
		// (set) Token: 0x06001683 RID: 5763 RVA: 0x0005E7AC File Offset: 0x0005C9AC
		public int CaretPosition { get; set; }

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x0005E7B5 File Offset: 0x0005C9B5
		public int TextLength
		{
			get
			{
				return this.StringInfo.LengthInTextElements;
			}
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x0005E7C4 File Offset: 0x0005C9C4
		protected void CaretSantity()
		{
			if (this.CaretPosition > this.TextLength)
			{
				this.CaretPosition = this.TextLength;
			}
			if (this.SelectionStart > this.TextLength)
			{
				this.SelectionStart = this.TextLength;
			}
			if (this.SelectionEnd > this.TextLength)
			{
				this.SelectionEnd = this.TextLength;
			}
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x0005E81F File Offset: 0x0005CA1F
		public bool HasSelection()
		{
			return this.ShouldDrawSelection && this.SelectionStart != this.SelectionEnd;
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x0005E83C File Offset: 0x0005CA3C
		public string GetSelectedText()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CaretSantity();
			int s = Math.Min(this.SelectionStart, this.SelectionEnd);
			int e = Math.Max(this.SelectionStart, this.SelectionEnd);
			return this.StringInfo.SubstringByTextElements(s, e - s);
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x0005E887 File Offset: 0x0005CA87
		public override string GetClipboardValue(bool cut)
		{
			if (!this.HasSelection())
			{
				return null;
			}
			return this.GetSelectedText();
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001689 RID: 5769 RVA: 0x0005E899 File Offset: 0x0005CA99
		// (set) Token: 0x0600168A RID: 5770 RVA: 0x0005E8A1 File Offset: 0x0005CAA1
		public bool Multiline { get; set; }

		// Token: 0x0600168B RID: 5771 RVA: 0x0005E8AC File Offset: 0x0005CAAC
		public void ReplaceSelection(string str)
		{
			int s = Math.Min(this.SelectionStart, this.SelectionEnd);
			int e = Math.Max(this.SelectionStart, this.SelectionEnd);
			int len = e - s;
			if (this.CaretPosition > e)
			{
				this.CaretPosition -= len;
			}
			else if (this.CaretPosition > s)
			{
				this.CaretPosition = s;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CaretPosition += new StringInfo(str).LengthInTextElements;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.InsertText(str, s, new int?(e));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SelectionStart = 0;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SelectionEnd = 0;
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x0005E954 File Offset: 0x0005CB54
		public void SetSelection(int start, int end)
		{
			int s = Math.Min(start, end).Clamp(0, this.TextLength);
			int e = Math.Max(start, end).Clamp(0, this.TextLength);
			if (s == e)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				s = 0;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				e = 0;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SelectionStart = s;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SelectionEnd = e;
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x0005E9B4 File Offset: 0x0005CBB4
		public void SetCaretPosition(int p, bool selecting = false)
		{
			if (this.SelectionEnd == 0 && this.SelectionStart == 0 && selecting)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SelectionStart = this.CaretPosition.Clamp(0, this.TextLength);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CaretPosition = p.Clamp(0, this.TextLength);
			if (selecting)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SelectionEnd = this.CaretPosition;
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SelectionEnd = 0;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SelectionStart = 0;
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x0005EA3C File Offset: 0x0005CC3C
		public void MoveToWordBoundaryLeft(bool andSelect)
		{
			int left = this.GetWordBoundaryIndices().LastOrDefault((int x) => x < this.CaretPosition);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MoveCaratPos(left - this.CaretPosition, andSelect);
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x0005EA78 File Offset: 0x0005CC78
		public void MoveToWordBoundaryRight(bool andSelect)
		{
			int right = this.GetWordBoundaryIndices().FirstOrDefault((int x) => x > this.CaretPosition);
			if (right == 0)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MoveCaratPos(right - this.CaretPosition, andSelect);
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x0005EAB5 File Offset: 0x0005CCB5
		public void MoveCaratPos(int delta, bool selecting = false)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetCaretPosition(this.CaretPosition + delta, selecting);
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x0005EACC File Offset: 0x0005CCCC
		public void InsertText(string str, int pos, int? endpos = null)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CaretSantity();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pos = Math.Clamp(pos, 0, this.TextLength);
			if (endpos != null)
			{
				endpos = new int?(Math.Clamp(endpos.Value, 0, this.TextLength));
			}
			string a = (pos > 0) ? this.StringInfo.SubstringByTextElements(0, pos) : "";
			string b = "";
			if (endpos != null)
			{
				int? num = endpos;
				int textLength = this.TextLength;
				if (num.GetValueOrDefault() < textLength & num != null)
				{
					b = this.StringInfo.SubstringByTextElements(endpos.Value);
				}
			}
			else if (pos < this.TextLength)
			{
				b = this.StringInfo.SubstringByTextElements(pos);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Text = a + str + b;
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x0005EBA0 File Offset: 0x0005CDA0
		public virtual void RemoveText(int start, int count)
		{
			string a = (start > 0) ? this.StringInfo.SubstringByTextElements(0, start) : "";
			string b = (start + count < this.TextLength) ? this.StringInfo.SubstringByTextElements(start + count) : "";
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Text = a + b;
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x0005EBF9 File Offset: 0x0005CDF9
		public bool IsNewline(string str)
		{
			return str == "\n" || str == "\r\n" || str == "\r";
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x0005EC2C File Offset: 0x0005CE2C
		public void MoveToLineStart(bool andSelect = false)
		{
			if (!this.Multiline)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SetCaretPosition(0, andSelect);
				return;
			}
			int iNewline = 0;
			TextElementEnumerator e = StringInfo.GetTextElementEnumerator(this.Text);
			while (e.MoveNext() && e.ElementIndex < this.CaretPosition)
			{
				if (this.IsNewline(e.GetTextElement()))
				{
					iNewline = e.ElementIndex + 1;
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetCaretPosition(iNewline, andSelect);
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x0005EC9C File Offset: 0x0005CE9C
		public void MoveToLineEnd(bool andSelect = false)
		{
			if (!this.Multiline)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SetCaretPosition(this.TextLength, andSelect);
				return;
			}
			TextElementEnumerator e = StringInfo.GetTextElementEnumerator(this.Text);
			while (e.MoveNext())
			{
				if (e.ElementIndex >= this.CaretPosition && this.IsNewline(e.GetTextElement()))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.SetCaretPosition(e.ElementIndex, andSelect);
					return;
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetCaretPosition(this.TextLength, andSelect);
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x0005ED1C File Offset: 0x0005CF1C
		public void MoveCaretLine(int offset_line, bool andSelect)
		{
			if (!this.Multiline)
			{
				if (offset_line < 0)
				{
					this.SetCaretPosition(0, andSelect);
				}
				if (offset_line > 0)
				{
					this.SetCaretPosition(this.TextLength, andSelect);
				}
				return;
			}
			Rect caret = this.GetCaretRect(this.CaretPosition);
			Vector2 height = caret.Size;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			height.x = 0f;
			Vector2 click = caret.Position + caret.Size * 0.5f + height * (float)offset_line * 1.2f;
			int pos = this.GetLetterAtScreenPosition(click);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetCaretPosition(pos, andSelect);
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x0005EDC0 File Offset: 0x0005CFC0
		public void SelectWord(int wordPos)
		{
			if (this.TextLength == 0)
			{
				return;
			}
			List<int> boundaries = this.GetWordBoundaryIndices();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SelectionStart = boundaries.LastOrDefault((int x) => x < wordPos);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SelectionEnd = boundaries.FirstOrDefault((int x) => x > wordPos);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CaretPosition = this.SelectionEnd;
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x0005EE38 File Offset: 0x0005D038
		protected override void OnEvent(PanelEvent e)
		{
			if (e.Name == "onimestart")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ImeInputStart = this.Text;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ImeInputPos = new int?(this.CaretPosition);
			}
			if (e.Name == "onimeend")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ImeInputStart = null;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ImeInputPos = null;
			}
			if (e.Name == "onime")
			{
				if (this.ImeInputPos == null)
				{
					return;
				}
				string str = (string)e.Value;
				StringInfo info = new StringInfo(str);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Text = this.ImeInputStart;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.InsertText(str, this.ImeInputPos.Value, null);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CaretPosition = this.ImeInputPos.Value + info.LengthInTextElements;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnEvent(e);
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x0005EF3C File Offset: 0x0005D13C
		public List<int> GetWordBoundaryIndices()
		{
			List<int> result = new List<int>
			{
				0,
				this.StringInfo.LengthInTextElements
			};
			TextElementEnumerator e = StringInfo.GetTextElementEnumerator(this.Text);
			string input = string.Empty;
			while (e.MoveNext())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				input += e.GetTextElement()[0].ToString();
			}
			Match match = Regex.Match(input, "\\b");
			while (match.Success)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				result.Add(match.Index);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				match = match.NextMatch();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			result = result.Distinct<int>().ToList<int>();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			result.Sort();
			return result;
		}

		// Token: 0x0400074B RID: 1867
		internal TextPanel InternalLabel;

		// Token: 0x0400074C RID: 1868
		protected StringInfo StringInfo = new StringInfo();

		// Token: 0x0400074F RID: 1871
		private int? ImeInputPos;

		// Token: 0x04000750 RID: 1872
		private string ImeInputStart;
	}
}
