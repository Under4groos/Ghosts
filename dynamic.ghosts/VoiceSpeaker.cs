using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.UI;

// Token: 0x02000007 RID: 7
public class VoiceSpeaker : Label
{
	// Token: 0x0600000C RID: 12 RVA: 0x000022C6 File Offset: 0x000004C6
	public VoiceSpeaker()
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.StyleSheet.Load("/UI/VoiceChat/VoiceSpeaker.scss", true);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.Text = "mic";
	}

	// Token: 0x0600000D RID: 13 RVA: 0x000022F4 File Offset: 0x000004F4
	public override void Tick()
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		base.Tick();
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.VoiceLevel = this.VoiceLevel.LerpTo(Voice.Level, Time.Delta * 40f, true);
		PanelTransform tr = default(PanelTransform);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		tr.AddScale(1f.LerpTo(1.2f, this.VoiceLevel, true));
		RuntimeHelpers.EnsureSufficientExecutionStack();
		base.Style.Transform = new PanelTransform?(tr);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		base.Style.Dirty();
		RuntimeHelpers.EnsureSufficientExecutionStack();
		base.SetClass("active", Voice.IsRecording);
	}

	// Token: 0x0400000A RID: 10
	private float VoiceLevel;
}
