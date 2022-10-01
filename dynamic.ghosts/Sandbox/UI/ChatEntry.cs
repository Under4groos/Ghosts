using System;
using System.Runtime.CompilerServices;
using Sandbox.UI.Construct;

namespace Sandbox.UI
{
	// Token: 0x020001D3 RID: 467
	public class ChatEntry : Panel
	{
		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001793 RID: 6035 RVA: 0x0006279C File Offset: 0x0006099C
		// (set) Token: 0x06001794 RID: 6036 RVA: 0x000627A4 File Offset: 0x000609A4
		public Label NameLabel { get; internal set; }

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001795 RID: 6037 RVA: 0x000627AD File Offset: 0x000609AD
		// (set) Token: 0x06001796 RID: 6038 RVA: 0x000627B5 File Offset: 0x000609B5
		public Label Message { get; internal set; }

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001797 RID: 6039 RVA: 0x000627BE File Offset: 0x000609BE
		// (set) Token: 0x06001798 RID: 6040 RVA: 0x000627C6 File Offset: 0x000609C6
		public Image Avatar { get; internal set; }

		// Token: 0x06001799 RID: 6041 RVA: 0x000627D0 File Offset: 0x000609D0
		public ChatEntry()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Avatar = base.Add.Image(null, null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.NameLabel = base.Add.Label("Name", "name");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Message = base.Add.Label("Message", "message");
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x0006284B File Offset: 0x00060A4B
		public override void Tick()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Tick();
			if (this.TimeSinceBorn > 10f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Delete(false);
			}
		}

		// Token: 0x0400079B RID: 1947
		public RealTimeSince TimeSinceBorn = 0f;
	}
}
