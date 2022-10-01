using System;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x0200019E RID: 414
	[Library("phys_pulleyconstraint")]
	[HammerEntity]
	[EditorModel("models/editor/axis_helper_arrow.vmdl", "white", "white")]
	[Title("Pulley Constraint")]
	[Category("Constraints")]
	[Icon("merge")]
	[Description("A constraint that is essentially two length constraints and two points. Imagine it as a virtual rope connected to two objects, each suspended from a pulley above them. The constraint keeps the sum of the distances between the pulley points and their suspended objects constant.")]
	public class PulleyConstraint : BaseConstraint
	{
		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x000538F3 File Offset: 0x00051AF3
		// (set) Token: 0x06001491 RID: 5265 RVA: 0x000538FB File Offset: 0x00051AFB
		[Property("position2", null)]
		[Title("Pulley Position 2")]
		[PointLine]
		[FGDType("world_point", "", "")]
		[Description("The position of the pulley for Entity 2. The pulley for Entity 1 is the origin of this constraint entity. Entity 1 is always suspended from pulley point 1, and Entity 2 is always suspended from pulley point 2.")]
		public Vector3 Position2 { get; set; }

		// Token: 0x06001492 RID: 5266 RVA: 0x00053904 File Offset: 0x00051B04
		protected override PhysicsJoint CreateJoint(PhysicsBody body1, PhysicsBody body2)
		{
			return PhysicsJoint.CreatePulley(body1, body2, body1.Transform.Position, this.Position, body2.Transform.Position, this.Position2);
		}
	}
}
