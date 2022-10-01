using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x020001AF RID: 431
	[Library("trigger_teleport")]
	[HammerEntity]
	[Solid]
	[Title("Teleport Volume")]
	[Category("Triggers")]
	[Icon("auto_fix_normal")]
	[Description("A simple trigger volume that teleports entities that touch it.")]
	public class TeleportVolumeEntity : BaseTrigger
	{
		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x00056B1C File Offset: 0x00054D1C
		// (set) Token: 0x060015D1 RID: 5585 RVA: 0x00056B24 File Offset: 0x00054D24
		[Property("target", null)]
		[Title("Remote Destination")]
		[Description("The entity specifying a location to which entities should be teleported to.")]
		public EntityTarget TargetEntity { get; set; }

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x00056B2D File Offset: 0x00054D2D
		// (set) Token: 0x060015D3 RID: 5587 RVA: 0x00056B35 File Offset: 0x00054D35
		[Property("teleport_relative", null)]
		[Title("Teleport Relatively")]
		[Description("If set, teleports the entity with an offset depending on where the entity was in the trigger teleport. Think world portals. Place the target entity accordingly.")]
		public bool TeleportRelative { get; set; }

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x00056B3E File Offset: 0x00054D3E
		// (set) Token: 0x060015D5 RID: 5589 RVA: 0x00056B46 File Offset: 0x00054D46
		[Property("keep_velocity", null)]
		[Title("Keep Velocity")]
		[Description("If set, the teleported entity will not have it's velocity reset to 0.")]
		public bool KeepVelocity { get; set; }

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x060015D6 RID: 5590 RVA: 0x00056B4F File Offset: 0x00054D4F
		// (set) Token: 0x060015D7 RID: 5591 RVA: 0x00056B57 File Offset: 0x00054D57
		[Description("Fired when the trigger teleports an entity")]
		protected Entity.Output OnTriggered { get; set; }

		// Token: 0x060015D8 RID: 5592 RVA: 0x00056B60 File Offset: 0x00054D60
		public override void OnTouchStart(Entity other)
		{
			if (!base.Enabled)
			{
				return;
			}
			Entity Targetent = this.TargetEntity.GetTargets(null).FirstOrDefault<Entity>();
			if (Targetent != null)
			{
				Vector3 offset = Vector3.Zero;
				if (this.TeleportRelative)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					offset = other.Position - this.Position;
				}
				if (!this.KeepVelocity)
				{
					other.Velocity = Vector3.Zero;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnTriggered.Fire(other, 0f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				other.Transform = Targetent.Transform;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				other.Position += offset;
			}
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x00056C08 File Offset: 0x00054E08
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void CreateHammerOutputs()
		{
			this.OnTriggered = new Entity.Output(this, "OnTriggered");
			base.CreateHammerOutputs();
		}
	}
}
