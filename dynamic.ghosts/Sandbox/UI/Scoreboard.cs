using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox.UI.Construct;

namespace Sandbox.UI
{
	// Token: 0x020001D5 RID: 469
	public class Scoreboard<T> : Panel where T : ScoreboardEntry, new()
	{
		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x060017A1 RID: 6049 RVA: 0x00062A1E File Offset: 0x00060C1E
		// (set) Token: 0x060017A2 RID: 6050 RVA: 0x00062A26 File Offset: 0x00060C26
		public Panel Canvas { get; protected set; }

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x060017A3 RID: 6051 RVA: 0x00062A2F File Offset: 0x00060C2F
		// (set) Token: 0x060017A4 RID: 6052 RVA: 0x00062A37 File Offset: 0x00060C37
		public Panel Header { get; protected set; }

		// Token: 0x060017A5 RID: 6053 RVA: 0x00062A40 File Offset: 0x00060C40
		public Scoreboard()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StyleSheet.Load("/ui/scoreboard/Scoreboard.scss", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddClass("scoreboard");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AddHeader();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Canvas = base.Add.Panel("canvas");
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x00062AB0 File Offset: 0x00060CB0
		public override void Tick()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Tick();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetClass("open", this.ShouldBeOpen());
			if (!base.IsVisible)
			{
				return;
			}
			foreach (Client client in Client.All.Except(this.Rows.Keys))
			{
				T entry = this.AddClient(client);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Rows[client] = entry;
			}
			foreach (Client client2 in this.Rows.Keys.Except(Client.All))
			{
				T row;
				if (this.Rows.TryGetValue(client2, out row))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					T t = row;
					if (t != null)
					{
						t.Delete(false);
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Rows.Remove(client2);
				}
			}
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x00062BC8 File Offset: 0x00060DC8
		public virtual bool ShouldBeOpen()
		{
			return Input.Down(InputButton.Score);
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x00062BD8 File Offset: 0x00060DD8
		protected virtual void AddHeader()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Header = base.Add.Panel("header");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Header.Add.Label("Name", "name");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Header.Add.Label("Kills", "kills");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Header.Add.Label("Deaths", "deaths");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Header.Add.Label("Ping", "ping");
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x00062C83 File Offset: 0x00060E83
		protected virtual T AddClient(Client entry)
		{
			T t = this.Canvas.AddChild<T>(null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			t.Client = entry;
			return t;
		}

		// Token: 0x0400079E RID: 1950
		private Dictionary<Client, T> Rows = new Dictionary<Client, T>();
	}
}
