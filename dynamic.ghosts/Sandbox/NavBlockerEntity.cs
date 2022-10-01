using System;
using System.Runtime.CompilerServices;
using Sandbox.Component;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x020001A9 RID: 425
	[Library("ent_nav_blocker")]
	[AutoApplyMaterial("materials/tools/toolsnavattribute.vmat")]
	[HammerEntity]
	[Solid]
	[VisGroup(VisGroup.Navigation)]
	[HideProperty("enable_shadows")]
	[Title("Nav Blocker")]
	[Category("Navigation")]
	[Icon("block")]
	[Description("Has the ability to dynamically block the navigation mesh. When active, AI will see this entity as an obstacle and will not try to walk through it.")]
	public class NavBlockerEntity : ModelEntity
	{
		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x000564E7 File Offset: 0x000546E7
		// (set) Token: 0x060015A5 RID: 5541 RVA: 0x000564EF File Offset: 0x000546EF
		[Property]
		[DefaultValue(true)]
		[Description("Enabled state of this entity.")]
		public bool Enabled { get; protected set; } = true;

		// Token: 0x060015A6 RID: 5542 RVA: 0x000564F8 File Offset: 0x000546F8
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetupPhysicsFromModel(PhysicsMotionType.Static, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableAllCollisions = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Transmit = TransmitType.Never;
			if (this.Enabled)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Enable();
			}
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x00056549 File Offset: 0x00054749
		[Input]
		[Description("Enables this blocker.")]
		public void Enable()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Enabled = true;
			if (this.Components.Get<NavBlocker>(true) != null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Components.Add<NavBlocker>(new NavBlocker(NavBlockerType.BLOCK));
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x0005657D File Offset: 0x0005477D
		[Input]
		[Description("Disables this blocker.")]
		public void Disable()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Enabled = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Components.RemoveAny<NavBlocker>();
		}
	}
}
