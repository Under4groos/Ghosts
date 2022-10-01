using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x020001AD RID: 429
	[Library("trigger_multiple")]
	[HammerEntity]
	[Solid]
	[Title("Trigger Multiple")]
	[Category("Triggers")]
	[Icon("done_all")]
	[Description("A volume that can be triggered multiple times, including at an interval while something is inside the trigger volume.")]
	public class TriggerMultiple : BaseTrigger
	{
		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x0005696B File Offset: 0x00054B6B
		// (set) Token: 0x060015C1 RID: 5569 RVA: 0x00056973 File Offset: 0x00054B73
		[Property("wait", null, Title = "Delay before reset")]
		[global::DefaultValue(1)]
		[Description("Amount of time, in seconds, after the trigger_multiple has triggered before it can be triggered again. If set to -1, it will never trigger again (in which case you should just use a trigger_once). This affects OnTrigger output.")]
		public float Wait { get; set; } = 1f;

		// Token: 0x060015C2 RID: 5570 RVA: 0x0005697C File Offset: 0x00054B7C
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableTouchPersists = true;
			if (this.Wait <= 0f)
			{
				this.Wait = 0.2f;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x000569AD File Offset: 0x00054BAD
		// (set) Token: 0x060015C4 RID: 5572 RVA: 0x000569B5 File Offset: 0x00054BB5
		[Description("Called every \"Delay before reset\" seconds as long as at least one entity that passes filters is touching this trigger")]
		protected Entity.Output OnTrigger { get; set; }

		// Token: 0x060015C5 RID: 5573 RVA: 0x000569C0 File Offset: 0x00054BC0
		public virtual void OnTriggered(Entity other)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnTrigger.Fire(other, 0f);
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x000569E8 File Offset: 0x00054BE8
		public override void Touch(Entity other)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Touch(other);
			if (!base.Enabled)
			{
				return;
			}
			if (this.TimeSinceTriggered < this.Wait)
			{
				return;
			}
			if (base.TouchingEntityCount < 1)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceTriggered = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnTriggered(other);
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x00056A49 File Offset: 0x00054C49
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void CreateHammerOutputs()
		{
			this.OnTrigger = new Entity.Output(this, "OnTrigger");
			base.CreateHammerOutputs();
		}

		// Token: 0x04000714 RID: 1812
		private TimeSince TimeSinceTriggered;
	}
}
