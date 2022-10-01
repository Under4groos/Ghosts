using System;
using System.Runtime.CompilerServices;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x0200019C RID: 412
	[Library("phys_hinge")]
	[HammerEntity]
	[EditorModel("models/editor/axis_helper_arrow.vmdl", "white", "white")]
	[Title("Hinge Constraint")]
	[Category("Constraints")]
	[Icon("door_front")]
	[Description("A physically simulated hinge. Use the helper to define the axis of rotation.")]
	public class HingeConstraint : BaseConstraint
	{
		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x00053619 File Offset: 0x00051819
		// (set) Token: 0x06001474 RID: 5236 RVA: 0x00053621 File Offset: 0x00051821
		[Property]
		[Title("Friction")]
		[DefaultValue(0)]
		[Description("Resistance/friction in the hinge. A value of 1 will hold the child object still under gravity")]
		public float HingeFriction { get; set; }

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x0005362A File Offset: 0x0005182A
		// (set) Token: 0x06001476 RID: 5238 RVA: 0x00053632 File Offset: 0x00051832
		[Property("min_rotation", null)]
		[Title("Min Rotation Limit")]
		[MinMax(-180f, 0f)]
		[DefaultValue(0)]
		[Description("Minimum rotation limit around hinge axis")]
		public float MinRotation { get; set; }

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x0005363B File Offset: 0x0005183B
		// (set) Token: 0x06001478 RID: 5240 RVA: 0x00053643 File Offset: 0x00051843
		[Property("max_rotation", null)]
		[Title("Max Rotation Limit")]
		[MinMax(0f, 180f)]
		[DefaultValue(0)]
		[Description("Maximum rotation limit around hinge axis")]
		public float MaxRotation { get; set; }

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x0005364C File Offset: 0x0005184C
		// (set) Token: 0x0600147A RID: 5242 RVA: 0x00053654 File Offset: 0x00051854
		[Property("initial_rotation", null)]
		[Title("Initial rotation")]
		[MinMax(-1f, 1f)]
		[Obsolete("Does not function.")]
		[DefaultValue(0)]
		[Description("Initial rotation of the hinge (values -1 to 1 where -1 mean open at minimum limit and 1 means open at maximum limit)")]
		public float InitialRotation { get; set; }

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x0005365D File Offset: 0x0005185D
		// (set) Token: 0x0600147C RID: 5244 RVA: 0x00053665 File Offset: 0x00051865
		[Property]
		[Title("Hinge Axis")]
		[PointLine]
		[FGDType("world_point", "", "")]
		[Description("Position used to determine hinge axis, the axis being between this position and the position of the constraint entity. Use the helper to set the axis.")]
		public Vector3 HingeAxis { get; set; }

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x0005366E File Offset: 0x0005186E
		// (set) Token: 0x0600147E RID: 5246 RVA: 0x00053676 File Offset: 0x00051876
		[Obsolete("Does not function.")]
		[Property]
		[Title("Motor frequency")]
		[DefaultValue(10)]
		[Description("Range 0 - 30 (only used when driving the relative angle through the angle input)")]
		public float MotorFrequency { get; set; } = 10f;

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x0600147F RID: 5247 RVA: 0x0005367F File Offset: 0x0005187F
		// (set) Token: 0x06001480 RID: 5248 RVA: 0x00053687 File Offset: 0x00051887
		[Property]
		[Title("Motor damping ratio")]
		[Obsolete("Does not function.")]
		[DefaultValue(1)]
		[Description("Range 0 - 1 (only used when driving the relative angle through the angle input)")]
		public float MotorDampingRatio { get; set; } = 1f;

		// Token: 0x06001481 RID: 5249 RVA: 0x00053690 File Offset: 0x00051890
		protected override PhysicsJoint CreateJoint(PhysicsBody body1, PhysicsBody body2)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.joint = PhysicsJoint.CreateHinge(body1, body2, this.Position, (this.HingeAxis - this.Position).Normal);
			if (this.MinRotation != this.MaxRotation && this.joint != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.joint.MinAngle = this.MinRotation;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.joint.MaxAngle = this.MaxRotation;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.joint.Friction = this.HingeFriction;
			return this.joint;
		}

		// Token: 0x040006A5 RID: 1701
		private HingeJoint joint;
	}
}
