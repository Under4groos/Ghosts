using System;
using System.Runtime.CompilerServices;
using Sandbox.UI.Construct;

namespace Sandbox.UI
{
	// Token: 0x020001D6 RID: 470
	public class ScoreboardEntry : Panel
	{
		// Token: 0x060017AA RID: 6058 RVA: 0x00062CA4 File Offset: 0x00060EA4
		public ScoreboardEntry()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddClass("entry");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PlayerName = base.Add.Label("PlayerName", "name");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Kills = base.Add.Label("", "kills");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Deaths = base.Add.Label("", "deaths");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Ping = base.Add.Label("", "ping");
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x00062D58 File Offset: 0x00060F58
		public override void Tick()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Tick();
			if (!base.IsVisible)
			{
				return;
			}
			if (!this.Client.IsValid())
			{
				return;
			}
			if (this.TimeSinceUpdate < 0.1f)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceUpdate = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateData();
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x00062DBC File Offset: 0x00060FBC
		public virtual void UpdateData()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PlayerName.Text = this.Client.Name;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Kills.Text = this.Client.GetInt("kills", 0).ToString();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Deaths.Text = this.Client.GetInt("deaths", 0).ToString();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Ping.Text = this.Client.Ping.ToString();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetClass("me", this.Client == Local.Client);
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x00062E76 File Offset: 0x00061076
		public virtual void UpdateFrom(Client client)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Client = client;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateData();
		}

		// Token: 0x040007A0 RID: 1952
		public Client Client;

		// Token: 0x040007A1 RID: 1953
		public Label PlayerName;

		// Token: 0x040007A2 RID: 1954
		public Label Kills;

		// Token: 0x040007A3 RID: 1955
		public Label Deaths;

		// Token: 0x040007A4 RID: 1956
		public Label Ping;

		// Token: 0x040007A5 RID: 1957
		private RealTimeSince TimeSinceUpdate = 0f;
	}
}
