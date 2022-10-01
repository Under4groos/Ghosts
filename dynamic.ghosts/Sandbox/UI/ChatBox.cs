using System;
using System.Runtime.CompilerServices;
using Sandbox.Internal;
using Sandbox.UI.Construct;

namespace Sandbox.UI
{
	// Token: 0x020001D2 RID: 466
	public class ChatBox : Panel
	{
		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001782 RID: 6018 RVA: 0x0006235B File Offset: 0x0006055B
		// (set) Token: 0x06001783 RID: 6019 RVA: 0x00062363 File Offset: 0x00060563
		public Panel Canvas { get; protected set; }

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x0006236C File Offset: 0x0006056C
		// (set) Token: 0x06001785 RID: 6021 RVA: 0x00062374 File Offset: 0x00060574
		public TextEntry Input { get; protected set; }

		// Token: 0x06001786 RID: 6022 RVA: 0x00062380 File Offset: 0x00060580
		public ChatBox()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ChatBox.Current = this;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StyleSheet.Load("/ui/chat/ChatBox.scss", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Canvas = base.Add.Panel("chat_canvas");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Input = base.Add.TextEntry("");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Input.AddEventListener("onsubmit", delegate()
			{
				this.Submit();
			});
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Input.AddEventListener("onblur", delegate()
			{
				this.Close();
			});
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Input.AcceptsFocus = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Input.AllowEmojiReplace = true;
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x00062451 File Offset: 0x00060651
		private void Open()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AddClass("open");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Input.Focus();
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x00062474 File Offset: 0x00060674
		private void Close()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.RemoveClass("open");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Input.Blur();
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x00062497 File Offset: 0x00060697
		public override void Tick()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Tick();
			if (Sandbox.Input.Pressed(InputButton.Chat))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Open();
			}
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x000624BC File Offset: 0x000606BC
		private void Submit()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Close();
			string msg = this.Input.Text.Trim();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Input.Text = "";
			if (string.IsNullOrWhiteSpace(msg))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ChatBox.Say(msg);
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x00062510 File Offset: 0x00060710
		public void AddEntry(string name, string message, string avatar, string lobbyState = null)
		{
			ChatEntry e = this.Canvas.AddChild<ChatEntry>(null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			e.Message.Text = message;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			e.NameLabel.Text = name;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			e.Avatar.SetTexture(avatar);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			e.SetClass("noname", string.IsNullOrEmpty(name));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			e.SetClass("noavatar", string.IsNullOrEmpty(avatar));
			if (lobbyState == "ready" || lobbyState == "staging")
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				e.SetClass("is-lobby", true);
			}
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x000625B8 File Offset: 0x000607B8
		[ConCmd.ClientAttribute("chat_add", CanBeCalledFromServer = true)]
		public static void AddChatEntry(string name, string message, string avatar = null, string lobbyState = null)
		{
			if (Host.IsServer)
			{
				throw new Exception("Trying to call AddChatEntry serverside!");
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ChatBox current = ChatBox.Current;
			if (current != null)
			{
				current.AddEntry(name, message, avatar, lobbyState);
			}
			if (!GlobalGameNamespace.Global.IsListenServer)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Log.Info(FormattableStringFactory.Create("{0}: {1}", new object[]
				{
					name,
					message
				}));
			}
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x00062623 File Offset: 0x00060823
		[ConCmd.ClientAttribute("chat_addinfo", CanBeCalledFromServer = true)]
		public static void AddInformation(string message, string avatar = null)
		{
			if (Host.IsServer)
			{
				throw new Exception("Trying to call AddInformation serverside!");
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ChatBox current = ChatBox.Current;
			if (current == null)
			{
				return;
			}
			current.AddEntry(null, message, avatar, null);
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x00062650 File Offset: 0x00060850
		[ConCmd.ServerAttribute("say")]
		public static void Say(string message)
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("say", new object[]
				{
					message
				});
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Assert.NotNull<Client>(ConsoleSystem.Caller);
			if (message.Contains('\n') || message.Contains('\r'))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info(FormattableStringFactory.Create("{0}: {1}", new object[]
			{
				ConsoleSystem.Caller,
				message
			}));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			To everyone = To.Everyone;
			string name = ConsoleSystem.Caller.Name;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 1);
			defaultInterpolatedStringHandler.AppendLiteral("avatar:");
			defaultInterpolatedStringHandler.AppendFormatted<long>(ConsoleSystem.Caller.PlayerId);
			ChatBox.AddChatEntry(everyone, name, message, defaultInterpolatedStringHandler.ToStringAndClear(), null);
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x00062710 File Offset: 0x00060910
		public static void AddChatEntry(To to_target, string name, string message, string avatar = null, string lobbyState = null)
		{
			Host.AssertServer("AddChatEntry");
			string _cmdstr_ = ConsoleSystem.Build("chat_add", new object[]
			{
				name,
				message,
				avatar,
				lobbyState
			});
			to_target.SendCommand(_cmdstr_);
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x00062754 File Offset: 0x00060954
		public static void AddInformation(To to_target, string message, string avatar = null)
		{
			Host.AssertServer("AddInformation");
			string _cmdstr_ = ConsoleSystem.Build("chat_addinfo", new object[]
			{
				message,
				avatar
			});
			to_target.SendCommand(_cmdstr_);
		}

		// Token: 0x04000795 RID: 1941
		private static ChatBox Current;
	}
}
