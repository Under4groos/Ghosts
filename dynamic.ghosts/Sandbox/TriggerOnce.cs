using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x020001AE RID: 430
	[Library("trigger_once")]
	[HammerEntity]
	[Solid]
	[Title("Trigger Once")]
	[Category("Triggers")]
	[Icon("done")]
	[Description("A simple trigger volume that fires once and then removes itself.")]
	public class TriggerOnce : BaseTrigger
	{
		// Token: 0x060015C9 RID: 5577 RVA: 0x00056A75 File Offset: 0x00054C75
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableTouchPersists = true;
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00056A8E File Offset: 0x00054C8E
		public override void OnTouchStart(Entity other)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnTouchStart(other);
			if (!base.Enabled)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnTriggered(other);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.DeleteAsync(Time.Delta);
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x060015CB RID: 5579 RVA: 0x00056AC2 File Offset: 0x00054CC2
		// (set) Token: 0x060015CC RID: 5580 RVA: 0x00056ACA File Offset: 0x00054CCA
		[Description("Called once at least a single entity that passes filters is touching this trigger, just before this trigger getting deleted")]
		protected Entity.Output OnTrigger { get; set; }

		// Token: 0x060015CD RID: 5581 RVA: 0x00056AD4 File Offset: 0x00054CD4
		public virtual void OnTriggered(Entity other)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnTrigger.Fire(other, 0f);
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x00056AFB File Offset: 0x00054CFB
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void CreateHammerOutputs()
		{
			this.OnTrigger = new Entity.Output(this, "OnTrigger");
			base.CreateHammerOutputs();
		}
	}
}
