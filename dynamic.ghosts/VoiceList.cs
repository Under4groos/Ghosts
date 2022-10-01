using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox.UI;

// Token: 0x02000006 RID: 6
public class VoiceList : Panel
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000008 RID: 8 RVA: 0x0000223F File Offset: 0x0000043F
	// (set) Token: 0x06000009 RID: 9 RVA: 0x00002246 File Offset: 0x00000446
	public static VoiceList Current { get; internal set; }

	// Token: 0x0600000A RID: 10 RVA: 0x0000224E File Offset: 0x0000044E
	public VoiceList()
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		VoiceList.Current = this;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.StyleSheet.Load("/UI/VoiceChat/VoiceList.scss", true);
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002278 File Offset: 0x00000478
	public void OnVoicePlayed(long steamId, float level)
	{
		VoiceEntry entry = base.ChildrenOfType<VoiceEntry>().FirstOrDefault((VoiceEntry x) => x.Friend.Id == steamId);
		if (entry == null)
		{
			entry = new VoiceEntry(this, steamId);
		}
		RuntimeHelpers.EnsureSufficientExecutionStack();
		entry.Update(level);
	}
}
