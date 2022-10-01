using System;
using System.Runtime.CompilerServices;

namespace Sandbox.Hooks
{
	// Token: 0x020001BA RID: 442
	public static class Chat
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06001629 RID: 5673 RVA: 0x0005771C File Offset: 0x0005591C
		// (remove) Token: 0x0600162A RID: 5674 RVA: 0x00057750 File Offset: 0x00055950
		[Obsolete("Use InputButton.Chat instead")]
		public static event Action OnOpenChat;

		// Token: 0x0600162B RID: 5675 RVA: 0x00057783 File Offset: 0x00055983
		[ConCmd.ClientAttribute("openchat")]
		internal static void MessageMode()
		{
			if (Host.IsServer)
			{
				throw new Exception("Trying to call MessageMode serverside!");
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Action onOpenChat = Chat.OnOpenChat;
			if (onOpenChat == null)
			{
				return;
			}
			onOpenChat();
		}
	}
}
