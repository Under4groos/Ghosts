using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

// Token: 0x02000005 RID: 5
public class VoiceEntry : Panel
{
	// Token: 0x06000005 RID: 5 RVA: 0x00002090 File Offset: 0x00000290
	public VoiceEntry(Panel parent, long steamId)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		base.Parent = parent;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.Friend = new Friend(steamId);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.Avatar = base.Add.Image("", "avatar");
		RuntimeHelpers.EnsureSufficientExecutionStack();
		Image avatar = this.Avatar;
		DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 1);
		defaultInterpolatedStringHandler.AppendLiteral("avatar:");
		defaultInterpolatedStringHandler.AppendFormatted<long>(steamId);
		avatar.SetTexture(defaultInterpolatedStringHandler.ToStringAndClear());
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.Name = base.Add.Label(this.Friend.Name, "name");
	}

	// Token: 0x06000006 RID: 6 RVA: 0x0000213A File Offset: 0x0000033A
	public void Update(float level)
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.timeSincePlayed = 0f;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.Name.Text = this.Friend.Name;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.TargetVoiceLevel = level;
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002178 File Offset: 0x00000378
	public override void Tick()
	{
		RuntimeHelpers.EnsureSufficientExecutionStack();
		base.Tick();
		if (base.IsDeleting)
		{
			return;
		}
		float SpeakTimeout = 2f;
		float timeoutInv = 1f - this.timeSincePlayed / SpeakTimeout;
		RuntimeHelpers.EnsureSufficientExecutionStack();
		timeoutInv = MathF.Min(timeoutInv * 2f, 1f);
		if (timeoutInv <= 0f)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Delete(false);
			return;
		}
		RuntimeHelpers.EnsureSufficientExecutionStack();
		this.VoiceLevel = this.VoiceLevel.LerpTo(this.TargetVoiceLevel, Time.Delta * 40f, true);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		base.Style.Left = new Length?(this.VoiceLevel * -32f * timeoutInv);
		RuntimeHelpers.EnsureSufficientExecutionStack();
		base.Style.Dirty();
	}

	// Token: 0x04000003 RID: 3
	public Friend Friend;

	// Token: 0x04000004 RID: 4
	private readonly Label Name;

	// Token: 0x04000005 RID: 5
	private readonly Image Avatar;

	// Token: 0x04000006 RID: 6
	private float VoiceLevel;

	// Token: 0x04000007 RID: 7
	private float TargetVoiceLevel;

	// Token: 0x04000008 RID: 8
	private RealTimeSince timeSincePlayed;
}
