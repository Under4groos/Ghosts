using System;
using System.Runtime.CompilerServices;
using Sandbox.UI.Construct;

namespace Sandbox.UI
{
	// Token: 0x020001BC RID: 444
	[Library("ButtonGroup")]
	public class ButtonGroup : Panel
	{
		// Token: 0x0600163C RID: 5692 RVA: 0x00057AA9 File Offset: 0x00055CA9
		public Button AddButton(string value, Action action)
		{
			return base.Add.Button(value, action);
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x00057AB8 File Offset: 0x00055CB8
		public Button AddButtonActive(string value, Action<bool> action)
		{
			Button button = base.Add.Button(value, null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			button.AddEventListener("startactive", delegate()
			{
				action(true);
			});
			RuntimeHelpers.EnsureSufficientExecutionStack();
			button.AddEventListener("stopactive", delegate()
			{
				action(false);
			});
			return button;
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x00057B18 File Offset: 0x00055D18
		protected override void OnChildAdded(Panel child)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnChildAdded(child);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			child.AddEventListener("onclick", delegate()
			{
				this.SelectedButton = child;
			});
			if (child.HasClass("active"))
			{
				this.SelectedButton = child;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x00057B89 File Offset: 0x00055D89
		// (set) Token: 0x06001640 RID: 5696 RVA: 0x00057B94 File Offset: 0x00055D94
		public Panel SelectedButton
		{
			get
			{
				return this._selected;
			}
			set
			{
				if (this._selected == value)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Panel selected = this._selected;
				if (selected != null)
				{
					selected.RemoveClass("active");
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Panel selected2 = this._selected;
				if (selected2 != null)
				{
					selected2.CreateEvent("stopactive", null, null);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this._selected = value;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Panel selected3 = this._selected;
				if (selected3 != null)
				{
					selected3.AddClass("active");
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Panel selected4 = this._selected;
				if (selected4 == null)
				{
					return;
				}
				selected4.CreateEvent("startactive", null, null);
			}
		}

		// Token: 0x0400073E RID: 1854
		private Panel _selected;
	}
}
