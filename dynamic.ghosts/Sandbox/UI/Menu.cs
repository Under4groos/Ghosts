using System;
using System.Runtime.CompilerServices;

namespace Sandbox.UI
{
	// Token: 0x020001C6 RID: 454
	public class Menu : Panel
	{
		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x0005F00A File Offset: 0x0005D20A
		// (set) Token: 0x0600169D RID: 5789 RVA: 0x0005F012 File Offset: 0x0005D212
		public Panel SelectedChild { get; set; }

		// Token: 0x0600169E RID: 5790 RVA: 0x0005F01C File Offset: 0x0005D21C
		public override void OnButtonTyped(string button, KeyModifiers km)
		{
			if (button == "up")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.MoveSelection(-1);
			}
			if (button == "down")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.MoveSelection(-1);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnButtonTyped(button, km);
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x0005F068 File Offset: 0x0005D268
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
	}
}
