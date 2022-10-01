using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Sandbox.Internal;
using Sandbox.UI.Construct;

namespace Sandbox.UI
{
	// Token: 0x020001D0 RID: 464
	[Library("TextEntry")]
	public class TextEntry : Panel, IInputControl
	{
		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001718 RID: 5912 RVA: 0x000609E7 File Offset: 0x0005EBE7
		// (set) Token: 0x06001719 RID: 5913 RVA: 0x000609EF File Offset: 0x0005EBEF
		[Description("If you hook a method up here we'll do autocomplete on it")]
		public Func<string, object[]> AutoComplete { get; set; }

		// Token: 0x0600171A RID: 5914 RVA: 0x000609F8 File Offset: 0x0005EBF8
		public void UpdateAutoComplete()
		{
			if (this.AutoComplete == null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DestroyAutoComplete();
				return;
			}
			object[] results = this.AutoComplete(this.Text);
			if (results == null || results.Length == 0)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DestroyAutoComplete();
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateAutoComplete(results);
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x00060A4C File Offset: 0x0005EC4C
		public void UpdateAutoComplete(object[] options)
		{
			if (this.AutoCompletePanel == null || this.AutoCompletePanel.IsDeleting)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.AutoCompletePanel = new Popup(this, Popup.PositionMode.AboveLeft, 8f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.AutoCompletePanel.AddClass("autocomplete");
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.AutoCompletePanel.SkipTransitions();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AutoCompletePanel.DeleteChildren(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AutoCompletePanel.UserData = this.Text;
			for (int i = 0; i < options.Length; i++)
			{
				object r = options[i];
				Panel panel = this.AutoCompletePanel.AddOption(r.ToString(), delegate()
				{
					this.AutoCompleteSelected(r);
				});
				RuntimeHelpers.EnsureSufficientExecutionStack();
				panel.UserData = r;
			}
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x00060B29 File Offset: 0x0005ED29
		public virtual void DestroyAutoComplete()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Popup autoCompletePanel = this.AutoCompletePanel;
			if (autoCompletePanel != null)
			{
				autoCompletePanel.Delete(false);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AutoCompletePanel = null;
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x00060B4E File Offset: 0x0005ED4E
		private void AutoCompleteSelected(object obj)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Text = obj.ToString();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Focus();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnValueChanged();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Label.MoveToLineEnd(false);
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x00060B8C File Offset: 0x0005ED8C
		protected virtual void AutoCompleteSelectionChanged()
		{
			Panel selected = this.AutoCompletePanel.SelectedChild;
			if (selected == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Text = selected.UserData.ToString();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Label.MoveToLineEnd(false);
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x00060BD0 File Offset: 0x0005EDD0
		protected virtual void AutoCompleteCancel()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Text = this.AutoCompletePanel.UserData.ToString();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DestroyAutoComplete();
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x00060BF8 File Offset: 0x0005EDF8
		// (set) Token: 0x06001721 RID: 5921 RVA: 0x00060C00 File Offset: 0x0005EE00
		public Label Label { get; set; }

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x00060C09 File Offset: 0x0005EE09
		// (set) Token: 0x06001723 RID: 5923 RVA: 0x00060C16 File Offset: 0x0005EE16
		public string Text
		{
			get
			{
				return this.Label.Text;
			}
			set
			{
				this.Label.Text = value;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x00060C24 File Offset: 0x0005EE24
		// (set) Token: 0x06001725 RID: 5925 RVA: 0x00060C31 File Offset: 0x0005EE31
		public string Value
		{
			get
			{
				return this.Label.Text;
			}
			set
			{
				this.Label.Text = value;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x00060C3F File Offset: 0x0005EE3F
		public int TextLength
		{
			get
			{
				return this.Label.TextLength;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x00060C4C File Offset: 0x0005EE4C
		// (set) Token: 0x06001728 RID: 5928 RVA: 0x00060C59 File Offset: 0x0005EE59
		public int CaretPosition
		{
			get
			{
				return this.Label.CaretPosition;
			}
			set
			{
				this.Label.CaretPosition = value;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x00060C67 File Offset: 0x0005EE67
		public override bool HasContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x0600172A RID: 5930 RVA: 0x00060C6A File Offset: 0x0005EE6A
		// (set) Token: 0x0600172B RID: 5931 RVA: 0x00060C72 File Offset: 0x0005EE72
		[DefaultValue(false)]
		public bool AllowEmojiReplace { get; set; }

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x0600172C RID: 5932 RVA: 0x00060C7B File Offset: 0x0005EE7B
		[Description("Allow IME input when this is focused")]
		public override bool AcceptsImeInput
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x00060C7E File Offset: 0x0005EE7E
		// (set) Token: 0x0600172E RID: 5934 RVA: 0x00060C86 File Offset: 0x0005EE86
		[Category("Presentation")]
		[DefaultValue(null)]
		public string NumberFormat { get; set; }

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x00060C8F File Offset: 0x0005EE8F
		// (set) Token: 0x06001730 RID: 5936 RVA: 0x00060C97 File Offset: 0x0005EE97
		[Property]
		[DefaultValue(false)]
		public bool Multiline { get; set; }

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x00060CA0 File Offset: 0x0005EEA0
		// (set) Token: 0x06001732 RID: 5938 RVA: 0x00060CA8 File Offset: 0x0005EEA8
		[Property]
		public Color CaretColor { get; set; } = Color.White;

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x00060CB1 File Offset: 0x0005EEB1
		// (set) Token: 0x06001734 RID: 5940 RVA: 0x00060CB9 File Offset: 0x0005EEB9
		[Description("If we're numeric, this is the lowest numeric value allowed")]
		public float? MinValue { get; set; }

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001735 RID: 5941 RVA: 0x00060CC2 File Offset: 0x0005EEC2
		// (set) Token: 0x06001736 RID: 5942 RVA: 0x00060CCA File Offset: 0x0005EECA
		[Description("If we're numeric, this is the highest numeric value allowed")]
		public float? MaxValue { get; set; }

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001737 RID: 5943 RVA: 0x00060CD3 File Offset: 0x0005EED3
		// (set) Token: 0x06001738 RID: 5944 RVA: 0x00060CDB File Offset: 0x0005EEDB
		public Label PrefixLabel { get; protected set; }

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001739 RID: 5945 RVA: 0x00060CE4 File Offset: 0x0005EEE4
		// (set) Token: 0x0600173A RID: 5946 RVA: 0x00060CF8 File Offset: 0x0005EEF8
		public string Prefix
		{
			get
			{
				Label prefixLabel = this.PrefixLabel;
				if (prefixLabel == null)
				{
					return null;
				}
				return prefixLabel.Text;
			}
			set
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				if (this.PrefixLabel == null)
				{
					this.PrefixLabel = base.Add.Label(value, "prefix-label");
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.PrefixLabel.Text = value;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.SetClass("has-prefix", this.PrefixLabel != null);
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x00060D55 File Offset: 0x0005EF55
		// (set) Token: 0x0600173C RID: 5948 RVA: 0x00060D5D File Offset: 0x0005EF5D
		public Label SuffixLabel { get; protected set; }

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x0600173D RID: 5949 RVA: 0x00060D66 File Offset: 0x0005EF66
		// (set) Token: 0x0600173E RID: 5950 RVA: 0x00060D7C File Offset: 0x0005EF7C
		public string Suffix
		{
			get
			{
				Label suffixLabel = this.SuffixLabel;
				if (suffixLabel == null)
				{
					return null;
				}
				return suffixLabel.Text;
			}
			set
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				if (this.SuffixLabel == null)
				{
					this.SuffixLabel = base.Add.Label(value, "suffix-label");
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SuffixLabel.Text = value;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.SetClass("has-suffix", this.SuffixLabel != null);
			}
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x00060DDC File Offset: 0x0005EFDC
		public TextEntry()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AcceptsFocus = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddClass("textentry");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Label = base.Add.Label("", "content-label");
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x00060E4C File Offset: 0x0005F04C
		public override void OnPaste(string text)
		{
			if (this.Label.HasSelection())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Label.ReplaceSelection("");
			}
			TextElementEnumerator e = StringInfo.GetTextElementEnumerator(text);
			while (e.MoveNext())
			{
				if (this.MaxLength != null)
				{
					int textLength = this.TextLength;
					int? maxLength = this.MaxLength;
					if (textLength >= maxLength.GetValueOrDefault() & maxLength != null)
					{
						break;
					}
				}
				string str = e.GetTextElement();
				if (!str.Any((char x) => !this.CanEnterCharacter(x)))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					if (this.Text == null)
					{
						this.Text = "";
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Label.InsertText(str, this.CaretPosition, null);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Label.MoveCaratPos(1, false);
					if (str.Length == 1 && str[0] == ':')
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.RealtimeEmojiReplace();
					}
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnValueChanged();
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x00060F59 File Offset: 0x0005F159
		public override string GetClipboardValue(bool cut)
		{
			string clipboardValue = this.Label.GetClipboardValue(cut);
			if (cut)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Label.ReplaceSelection("");
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnValueChanged();
			}
			return clipboardValue;
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x00060F8C File Offset: 0x0005F18C
		public override void OnButtonTyped(string button, KeyModifiers km)
		{
			if (this.Label.HasSelection() && (button == "delete" || button == "backspace"))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Label.ReplaceSelection("");
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnValueChanged();
				return;
			}
			if (button == "delete")
			{
				if (this.CaretPosition < this.TextLength)
				{
					if (km.Ctrl)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.Label.MoveToWordBoundaryRight(true);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.Label.ReplaceSelection(string.Empty);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.OnValueChanged();
						return;
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Label.RemoveText(this.CaretPosition, 1);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.OnValueChanged();
				}
				return;
			}
			if (button == "backspace")
			{
				if (this.CaretPosition > 0)
				{
					if (km.Ctrl)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.Label.MoveToWordBoundaryLeft(true);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.Label.ReplaceSelection(string.Empty);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.OnValueChanged();
						return;
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Label.MoveCaratPos(-1, false);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Label.RemoveText(this.CaretPosition, 1);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.OnValueChanged();
				}
				return;
			}
			if (button == "a" && km.Ctrl)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Label.SelectionStart = 0;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Label.SelectionEnd = this.TextLength;
				return;
			}
			if (button == "home")
			{
				if (!km.Ctrl)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Label.MoveToLineStart(km.Shift);
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Label.SetCaretPosition(0, km.Shift);
				return;
			}
			else if (button == "end")
			{
				if (!km.Ctrl)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Label.MoveToLineEnd(km.Shift);
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Label.SetCaretPosition(this.TextLength, km.Shift);
				return;
			}
			else if (button == "left")
			{
				if (!km.Ctrl)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Label.MoveCaratPos(-1, km.Shift);
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Label.MoveToWordBoundaryLeft(km.Shift);
				return;
			}
			else if (button == "right")
			{
				if (!km.Ctrl)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Label.MoveCaratPos(1, km.Shift);
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Label.MoveToWordBoundaryRight(km.Shift);
				return;
			}
			else if (button == "down" || button == "up")
			{
				if (this.AutoCompletePanel != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.AutoCompletePanel.MoveSelection((button == "up") ? -1 : 1);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.AutoCompleteSelectionChanged();
					return;
				}
				if (string.IsNullOrEmpty(this.Text) && this.AutoCompletePanel == null && this.History.Count > 0)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					object[] options = this.History.ToArray();
					this.UpdateAutoComplete(options);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.AutoCompletePanel.MoveSelection(-1);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.AutoCompleteSelectionChanged();
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Label.MoveCaretLine((button == "up") ? -1 : 1, km.Shift);
				return;
			}
			else if (button == "enter" || button == "pad_enter")
			{
				if (this.Multiline)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.OnKeyTyped('\n');
					return;
				}
				if (this.AutoCompletePanel != null && this.AutoCompletePanel.SelectedChild != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.DestroyAutoComplete();
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Blur();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CreateEvent("onsubmit", this.Text, null);
				return;
			}
			else
			{
				if (!(button == "escape"))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					base.OnButtonTyped(button, km);
					return;
				}
				if (this.AutoCompletePanel != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.AutoCompleteCancel();
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Blur();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CreateEvent("oncancel", null, null);
				return;
			}
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x000613D4 File Offset: 0x0005F5D4
		protected override void OnMouseDown(MousePanelEvent e)
		{
			int pos = this.Label.GetLetterAtScreenPosition(Mouse.Position);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Label.SelectionStart = 0;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Label.SelectionEnd = 0;
			if (pos >= 0)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Label.SetCaretPosition(pos, false);
			}
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x0006142C File Offset: 0x0005F62C
		protected override void OnMouseUp(MousePanelEvent e)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SelectingWords = false;
			int pos = this.Label.GetLetterAtScreenPosition(Mouse.Position);
			if (this.Label.SelectionEnd > 0)
			{
				pos = this.Label.SelectionEnd;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Label.CaretPosition = pos.Clamp(0, this.TextLength);
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x0006148D File Offset: 0x0005F68D
		protected override void OnFocus(PanelEvent e)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateAutoComplete();
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x0006149A File Offset: 0x0005F69A
		protected override void OnBlur(PanelEvent e)
		{
			if (this.Numeric)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Text = this.FixNumeric();
			}
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x000614B5 File Offset: 0x0005F6B5
		protected override void OnDoubleClick(MousePanelEvent e)
		{
			if (e.Button == "mouseleft")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Label.SelectWord(this.Label.GetLetterAtScreenPosition(Mouse.Position));
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SelectingWords = true;
			}
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x000614F8 File Offset: 0x0005F6F8
		public override void OnKeyTyped(char k)
		{
			if (!this.CanEnterCharacter(k))
			{
				return;
			}
			if (this.MaxLength != null)
			{
				int textLength = this.TextLength;
				int? maxLength = this.MaxLength;
				if (textLength >= maxLength.GetValueOrDefault() & maxLength != null)
				{
					return;
				}
			}
			if (this.Label.HasSelection())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Label.ReplaceSelection(k.ToString());
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				if (this.Text == null)
				{
					this.Text = "";
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Label.InsertText(k.ToString(), this.CaretPosition, null);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Label.MoveCaratPos(1, false);
			}
			if (k == ':')
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.RealtimeEmojiReplace();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnValueChanged();
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x000615D8 File Offset: 0x0005F7D8
		public override void DrawContent(ref RenderState state)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Label.ShouldDrawSelection = base.HasFocus;
			float blinkRate = 0.8f;
			if (base.HasFocus)
			{
				bool blink = this.TimeSinceNotInFocus * blinkRate % blinkRate < blinkRate * 0.5f;
				Rect caret = this.Label.GetCaretRect(this.CaretPosition);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				caret.right += 0.4f;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				caret.left -= 0.4f;
				Color color = this.CaretColor;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				color.a *= (blink ? 1f : 0.05f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Render.Draw2D.Color = color;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Render.Draw2D.Box(caret, default(Vector4));
			}
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x000616B4 File Offset: 0x0005F8B4
		private void RealtimeEmojiReplace()
		{
			if (!this.AllowEmojiReplace)
			{
				return;
			}
			if (this.CaretPosition == 0)
			{
				return;
			}
			string lookup = null;
			int caretStringPosition = StringInfo.ParseCombiningCharacters(this.Text)[this.CaretPosition - 1];
			for (int i = caretStringPosition - 2; i >= 0; i--)
			{
				char c = this.Text[i];
				if (char.IsWhiteSpace(c))
				{
					return;
				}
				if (c == ':')
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					lookup = this.Text.Substring(i, caretStringPosition - i + 1);
					break;
				}
				if (i == 0)
				{
					return;
				}
			}
			if (lookup == null)
			{
				return;
			}
			string replace = Emoji.FindEmoji(lookup);
			if (replace == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CaretPosition -= lookup.Length - 1;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Text = this.Text.Replace(lookup, replace);
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x00061774 File Offset: 0x0005F974
		public virtual void OnValueChanged()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateAutoComplete();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateValidation();
			if (this.Numeric)
			{
				string text = this.FixNumeric();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CreateEvent("onchange", null, null);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.CreateValueEvent("value", text);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateEvent("onchange", null, null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.CreateValueEvent("value", this.Text);
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x00061804 File Offset: 0x0005FA04
		public override void Tick()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Tick();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetClass("is-multiline", this.Multiline);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetClass("has-placeholder", string.IsNullOrEmpty(this.Text) && this.PlaceholderLabel != null);
			if (this.Label != null)
			{
				this.Label.Multiline = this.Multiline;
			}
			if (!base.HasFocus)
			{
				this.TimeSinceNotInFocus = 0f;
			}
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x0006188C File Offset: 0x0005FA8C
		public override void SetProperty(string name, string value)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetProperty(name, value);
			if (name == "placeholder")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Placeholder = value;
			}
			if (name == "numeric")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Numeric = value.ToBool();
			}
			if (name == "format")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.NumberFormat = value;
			}
			if (name == "value" && !base.HasFocus)
			{
				if (this.Numeric)
				{
					float floatValue;
					if (!float.TryParse(value, out floatValue))
					{
						return;
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Text = floatValue.ToString(this.NumberFormat);
					return;
				}
				else
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Text = value;
				}
			}
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00061944 File Offset: 0x0005FB44
		public virtual string FixNumeric()
		{
			float floatValue;
			if (!float.TryParse(this.Text, out floatValue))
			{
				return 0f.Clamp(this.MinValue ?? floatValue, this.MaxValue ?? floatValue).ToString();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			floatValue = floatValue.Clamp(this.MinValue ?? floatValue, this.MaxValue ?? floatValue);
			return floatValue.ToString(this.NumberFormat);
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x000619F4 File Offset: 0x0005FBF4
		protected override void OnDragSelect(SelectionEvent e)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Label.ShouldDrawSelection = true;
			Vector2 tl = new Vector2(e.SelectionRect.left, e.SelectionRect.top);
			Vector2 br = new Vector2(e.SelectionRect.right, e.SelectionRect.bottom);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Label.SelectionStart = this.Label.GetLetterAtScreenPosition(tl);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Label.SelectionEnd = this.Label.GetLetterAtScreenPosition(br);
			if (this.SelectingWords)
			{
				List<int> wordBoundaryIndices = this.Label.GetWordBoundaryIndices();
				int left = wordBoundaryIndices.LastOrDefault((int x) => x < this.Label.SelectionStart);
				int right = wordBoundaryIndices.FirstOrDefault((int x) => x > this.Label.SelectionEnd);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				left = Math.Min(left, this.Label.SelectionStart);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				right = Math.Max(right, this.Label.SelectionEnd);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Label.SelectionStart = left;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Label.SelectionEnd = right;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001750 RID: 5968 RVA: 0x00061B0F File Offset: 0x0005FD0F
		// (set) Token: 0x06001751 RID: 5969 RVA: 0x00061B17 File Offset: 0x0005FD17
		internal List<string> History { get; set; } = new List<string>();

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001752 RID: 5970 RVA: 0x00061B20 File Offset: 0x0005FD20
		// (set) Token: 0x06001753 RID: 5971 RVA: 0x00061B28 File Offset: 0x0005FD28
		[DefaultValue(30)]
		public int HistoryMaxItems { get; set; } = 30;

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001754 RID: 5972 RVA: 0x00061B31 File Offset: 0x0005FD31
		// (set) Token: 0x06001755 RID: 5973 RVA: 0x00061B3C File Offset: 0x0005FD3C
		public string HistoryCookie
		{
			get
			{
				return this._historyCookie;
			}
			set
			{
				if (this._historyCookie == value)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this._historyCookie = value;
				if (string.IsNullOrEmpty(this._historyCookie))
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.History = GlobalGameNamespace.Cookie.Get<List<string>>(value, this.History);
			}
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x00061B90 File Offset: 0x0005FD90
		public void AddToHistory(string str)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.History.RemoveAll((string x) => x == str);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.History.Add(str);
			if (this.HistoryMaxItems > 0)
			{
				while (this.History.Count > this.HistoryMaxItems)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.History.RemoveAt(0);
				}
			}
			if (!string.IsNullOrEmpty(this.HistoryCookie))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Cookie.Set<List<string>>(this.HistoryCookie, this.History);
			}
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x00061C34 File Offset: 0x0005FE34
		public void ClearHistory()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.History.Clear();
			if (!string.IsNullOrEmpty(this.HistoryCookie))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Cookie.Set<List<string>>(this.HistoryCookie, this.History);
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x00061C6E File Offset: 0x0005FE6E
		// (set) Token: 0x06001759 RID: 5977 RVA: 0x00061C76 File Offset: 0x0005FE76
		public IconPanel IconPanel { get; protected set; }

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x00061C7F File Offset: 0x0005FE7F
		// (set) Token: 0x0600175B RID: 5979 RVA: 0x00061C94 File Offset: 0x0005FE94
		[Property]
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
				if (string.IsNullOrEmpty(value))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					IconPanel iconPanel = this.IconPanel;
					if (iconPanel != null)
					{
						iconPanel.Delete(true);
					}
				}
				else
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					if (this.IconPanel == null)
					{
						this.IconPanel = base.Add.Icon(value, null);
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.IconPanel.Text = value;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.SetClass("has-icon", this.IconPanel != null);
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x00061D0E File Offset: 0x0005FF0E
		// (set) Token: 0x0600175D RID: 5981 RVA: 0x00061D24 File Offset: 0x0005FF24
		public string Placeholder
		{
			get
			{
				Label placeholderLabel = this.PlaceholderLabel;
				if (placeholderLabel == null)
				{
					return null;
				}
				return placeholderLabel.Text;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					Label placeholderLabel = this.PlaceholderLabel;
					if (placeholderLabel != null)
					{
						placeholderLabel.Delete(false);
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.PlaceholderLabel = null;
					return;
				}
				if (this.PlaceholderLabel == null)
				{
					this.PlaceholderLabel = base.Add.Label(value, "placeholder");
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.PlaceholderLabel.Text = value;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x00061D8D File Offset: 0x0005FF8D
		// (set) Token: 0x0600175F RID: 5983 RVA: 0x00061D95 File Offset: 0x0005FF95
		[Category("Validation")]
		public int? MinLength { get; set; }

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001760 RID: 5984 RVA: 0x00061D9E File Offset: 0x0005FF9E
		// (set) Token: 0x06001761 RID: 5985 RVA: 0x00061DA6 File Offset: 0x0005FFA6
		[Category("Validation")]
		public int? MaxLength { get; set; }

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001762 RID: 5986 RVA: 0x00061DAF File Offset: 0x0005FFAF
		// (set) Token: 0x06001763 RID: 5987 RVA: 0x00061DB7 File Offset: 0x0005FFB7
		[Nullable(2)]
		[Category("Validation")]
		[Description("If set, will block the input of any character that doesn't match")]
		public string CharacterRegex { [NullableContext(2)] get; [NullableContext(2)] set; }

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001764 RID: 5988 RVA: 0x00061DC0 File Offset: 0x0005FFC0
		// (set) Token: 0x06001765 RID: 5989 RVA: 0x00061DC8 File Offset: 0x0005FFC8
		[Nullable(2)]
		[Category("Validation")]
		[Description("If set, HasValidationErrors will return true if doesn't match regex")]
		public string StringRegex { [NullableContext(2)] get; [NullableContext(2)] set; }

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001766 RID: 5990 RVA: 0x00061DD1 File Offset: 0x0005FFD1
		// (set) Token: 0x06001767 RID: 5991 RVA: 0x00061DD9 File Offset: 0x0005FFD9
		[Category("Validation")]
		[DefaultValue(false)]
		public bool Numeric { get; set; }

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001768 RID: 5992 RVA: 0x00061DE2 File Offset: 0x0005FFE2
		bool IInputControl.HasValidationErrors
		{
			get
			{
				return this.HasValidationErrors;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x00061DEA File Offset: 0x0005FFEA
		// (set) Token: 0x0600176A RID: 5994 RVA: 0x00061DF2 File Offset: 0x0005FFF2
		[Description("If true then this control has validation errors and the input shouldn't be accepted")]
		public bool HasValidationErrors { get; set; }

		// Token: 0x0600176B RID: 5995 RVA: 0x00061DFC File Offset: 0x0005FFFC
		[Description("Update the validation state of this control.")]
		public void UpdateValidation()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasValidationErrors = false;
			if (this.MinLength != null)
			{
				int textLength = this.TextLength;
				int? num = this.MinLength;
				if (textLength < num.GetValueOrDefault() & num != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.HasValidationErrors = true;
				}
			}
			if (this.MaxLength != null)
			{
				int textLength2 = this.TextLength;
				int? num = this.MaxLength;
				if (textLength2 > num.GetValueOrDefault() & num != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.HasValidationErrors = true;
				}
			}
			if (this.StringRegex != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.HasValidationErrors = (this.HasValidationErrors || !Regex.IsMatch(this.Text, this.StringRegex));
			}
			if (this.CharacterRegex != null)
			{
				foreach (char chr in this.Text)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.HasValidationErrors = (this.HasValidationErrors || !Regex.IsMatch(chr.ToString(), this.CharacterRegex));
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetClass("invalid", this.HasValidationErrors);
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00061F28 File Offset: 0x00060128
		public virtual bool CanEnterCharacter(char c)
		{
			return (this.CharacterRegex == null || Regex.IsMatch(c.ToString(), this.CharacterRegex)) && (!this.Numeric || c == '.' || c == '-' || c == ',' || char.IsDigit(c) || c == '-' || c == ',' || c == '.');
		}

		// Token: 0x0400077C RID: 1916
		internal Popup AutoCompletePanel;

		// Token: 0x04000786 RID: 1926
		private bool SelectingWords;

		// Token: 0x04000787 RID: 1927
		private RealTimeSince TimeSinceNotInFocus;

		// Token: 0x0400078A RID: 1930
		private string _historyCookie;

		// Token: 0x0400078C RID: 1932
		internal Label PlaceholderLabel;
	}
}
