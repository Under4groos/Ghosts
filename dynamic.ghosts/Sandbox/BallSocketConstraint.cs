using System;
using System.Runtime.CompilerServices;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x0200019B RID: 411
	[Library("phys_ballsocket")]
	[HammerEntity]
	[EditorModel("models/editor/axis_helper_arrow.vmdl", "white", "white")]
	[Title("Ball Socket Constraint")]
	[Category("Constraints")]
	[Icon("circle")]
	[Description("A constraint that keeps the position of two objects fixed, relative to the constraint's origin. It does not affect rotation.")]
	public class BallSocketConstraint : BaseConstraint
	{
		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x0600146F RID: 5231 RVA: 0x000535E0 File Offset: 0x000517E0
		// (set) Token: 0x06001470 RID: 5232 RVA: 0x000535E8 File Offset: 0x000517E8
		[Property]
		[DefaultValue(0)]
		[Description("Resistance/friction in the constraint.")]
		public float Friction { get; set; }

		// Token: 0x06001471 RID: 5233 RVA: 0x000535F1 File Offset: 0x000517F1
		protected override PhysicsJoint CreateJoint(PhysicsBody body1, PhysicsBody body2)
		{
			BallSocketJoint ballSocketJoint = PhysicsJoint.CreateBallSocket(body1, body2, this.Position);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ballSocketJoint.Friction = this.Friction;
			return ballSocketJoint;
		}
	}
}
