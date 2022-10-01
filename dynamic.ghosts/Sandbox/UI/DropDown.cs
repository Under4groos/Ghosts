using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox.Html;
using Sandbox.Internal;
using Sandbox.UI.Construct;

namespace Sandbox.UI
{
	// Token: 0x020001BF RID: 447
	[Library("select")]
	[Alias(new string[]
	{
		"dropdown"
	})]
	[Description("A UI control which provides multiple options via a dropdown box")]
	public class DropDown : PopupButton
	{
		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x0600165E RID: 5726 RVA: 0x0005804B File Offset: 0x0005624B
		[Description("The options to show on click. You can edit these directly via this property.")]
		public List<Option> Options { get; } = new List<Option>();

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x0600165F RID: 5727 RVA: 0x00058053 File Offset: 0x00056253
		// (set) Token: 0x06001660 RID: 5728 RVA: 0x0005805C File Offset: 0x0005625C
		[Description("The current string value. This is useful to have if Selected is null.")]
		public object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if (this._valueHash == HashCode.Combine<object>(value))
				{
					return;
				}
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
				defaultInterpolatedStringHandler.AppendFormatted<object>(this._value);
				string a = defaultInterpolatedStringHandler.ToStringAndClear();
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
				defaultInterpolatedStringHandler.AppendFormatted<object>(value);
				if (a == defaultInterpolatedStringHandler.ToStringAndClear())
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this._valueHash = HashCode.Combine<object>(value);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this._value = value;
				if (this._value != null && this.Options.Count == 0)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.PopulateOptionsFromType(this._value.GetType());
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				object value2 = this._value;
				this.Select((value2 != null) ? value2.ToString() : null, false);
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x0005811B File Offset: 0x0005631B
		// (set) Token: 0x06001662 RID: 5730 RVA: 0x00058124 File Offset: 0x00056324
		[Description("The currently selected option")]
		public Option Selected
		{
			get
			{
				return this.selected;
			}
			set
			{
				if (this.selected == value)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.selected = value;
				if (this.selected != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
					defaultInterpolatedStringHandler.AppendFormatted<object>(this.selected.Value);
					this.Value = defaultInterpolatedStringHandler.ToStringAndClear();
					RuntimeHelpers.EnsureSufficientExecutionStack();
					base.Icon = this.selected.Icon;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					base.Text = this.selected.Title;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.CreateEvent("onchange", null, null);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					string name = "value";
					Option option = this.selected;
					base.CreateValueEvent(name, (option != null) ? option.Value : null);
				}
			}
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x000581E8 File Offset: 0x000563E8
		public DropDown()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddClass("dropdown");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DropdownIndicator = base.Add.Icon("expand_more", "dropdown_indicator");
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x00058236 File Offset: 0x00056436
		public DropDown(Panel parent) : this()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Parent = parent;
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x0005824A File Offset: 0x0005644A
		public override void SetPropertyObject(string name, object value)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetPropertyObject(name, value);
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x0005825C File Offset: 0x0005645C
		[Description("Given the type, populate options. This is useful if you're an enum type.")]
		private void PopulateOptionsFromType(Type type)
		{
			if (type == typeof(bool))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Options.Add(new Option("True", true));
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Options.Add(new Option("False", false));
				return;
			}
			if (type.IsEnum)
			{
				string[] names = type.GetEnumNames();
				Array values = type.GetEnumValues();
				for (int i = 0; i < names.Length; i++)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Options.Add(new Option(names[i], values.GetValue(i)));
				}
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info(FormattableStringFactory.Create("Dropdown Type: {0}", new object[]
			{
				type
			}));
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x00058324 File Offset: 0x00056524
		[Description("Open the dropdown")]
		public override void Open()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Popup = new Popup(this, Popup.PositionMode.BelowStretch, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Popup.AddClass("flat-top");
			using (List<Option>.Enumerator enumerator = this.Options.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Option option = enumerator.Current;
					Panel o = this.Popup.AddOption(option.Title, option.Icon, delegate()
					{
						this.Select(option, true);
					});
					if (this.Selected != null && option.Value == this.Selected.Value)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						o.AddClass("active");
					}
				}
			}
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x00058410 File Offset: 0x00056610
		[Description("Select an option")]
		protected virtual void Select(Option option, bool triggerChange = true)
		{
			if (!triggerChange)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.selected = option;
				if (option != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Value = option.Value;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					base.Icon = option.Icon;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					base.Text = option.Title;
					return;
				}
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Selected = option;
			}
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x00058470 File Offset: 0x00056670
		[Description("Select an option by value string")]
		protected virtual void Select(string value, bool triggerChange = true)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Select(this.Options.FirstOrDefault((Option x) => string.Equals(x.Value.ToString(), value, StringComparison.OrdinalIgnoreCase)), triggerChange);
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x000584B0 File Offset: 0x000566B0
		[Description("Give support for option elements in html template")]
		public override bool OnTemplateElement(INode element)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Options.Clear();
			foreach (INode child in element.Children)
			{
				if (child.IsElement && child.Name.Equals("option", StringComparison.OrdinalIgnoreCase))
				{
					Option o = new Option();
					RuntimeHelpers.EnsureSufficientExecutionStack();
					o.Title = child.InnerHtml;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					o.Value = child.GetAttribute("value", o.Title);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					o.Icon = child.GetAttribute("icon", null);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Options.Add(o);
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
			defaultInterpolatedStringHandler.AppendFormatted<object>(this.Value);
			this.Select(defaultInterpolatedStringHandler.ToStringAndClear(), true);
			return true;
		}

		// Token: 0x04000745 RID: 1861
		protected IconPanel DropdownIndicator;

		// Token: 0x04000747 RID: 1863
		private Option selected;

		// Token: 0x04000748 RID: 1864
		private object _value;

		// Token: 0x04000749 RID: 1865
		private int _valueHash;
	}
}
