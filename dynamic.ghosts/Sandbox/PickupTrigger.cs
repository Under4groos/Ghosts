using System;
using System.Runtime.CompilerServices;

namespace Sandbox
{
	// Token: 0x02000162 RID: 354
	[Title("Pickup Trigger")]
	[Icon("select_all")]
	[Description("A utility class. Add as a child to your pickupable entities to expand the trigger boundaries. They'll be able to pick up the parent entity using these bounds.")]
	public class PickupTrigger : ModelEntity
	{
		// Token: 0x06001014 RID: 4116 RVA: 0x0003F895 File Offset: 0x0003DA95
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("trigger");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetTriggerSize(16f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Transmit = TransmitType.Never;
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0003F8D3 File Offset: 0x0003DAD3
		[Description("Set the trigger radius. Default is 16.")]
		public void SetTriggerSize(float radius)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetupPhysicsFromCapsule(PhysicsMotionType.Keyframed, new Capsule(Vector3.Zero, Vector3.One * 0.1f, radius));
		}
	}
}
