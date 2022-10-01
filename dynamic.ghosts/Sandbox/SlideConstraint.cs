using System;
using System.Runtime.CompilerServices;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x0200019F RID: 415
	[Library("phys_slideconstraint")]
	[HammerEntity]
	[EditorModel("models/editor/axis_helper_arrow.vmdl", "white", "white")]
	[Title("Slide Constraint")]
	[Category("Constraints")]
	[Icon("open_in_full")]
	[Description("A constraint that constrains an entity along a line segment.")]
	public class SlideConstraint : BaseConstraint
	{
		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x00053937 File Offset: 0x00051B37
		// (set) Token: 0x06001495 RID: 5269 RVA: 0x0005393F File Offset: 0x00051B3F
		[Property]
		[Title("Sliding Axis")]
		[PointLine]
		[FGDType("world_point", "", "")]
		[Description("Position used to determine sliding axis, the axis being between this position and the position of the constraint entity. Use the helper to set the axis.")]
		public Vector3 SlideAxis { get; set; }

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x00053948 File Offset: 0x00051B48
		// (set) Token: 0x06001497 RID: 5271 RVA: 0x00053950 File Offset: 0x00051B50
		[Property]
		[Title("Friction")]
		[DefaultValue(0)]
		[Description("Set motor friction, 0 = no friction, 1 = friction that is about enough to counter gravity")]
		public float SlideFriction { get; set; }

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x00053959 File Offset: 0x00051B59
		// (set) Token: 0x06001499 RID: 5273 RVA: 0x00053961 File Offset: 0x00051B61
		[Property]
		[Title("Initial Offset")]
		[Obsolete("Does not function.")]
		[DefaultValue(0)]
		[Description("Initial offset in the range from -1 to 1 where -1 means spawn at the min limit, 0 means apply no offset, and 1 means spawn at max limit")]
		public float InitialOffset { get; set; }

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x0005396A File Offset: 0x00051B6A
		// (set) Token: 0x0600149B RID: 5275 RVA: 0x00053972 File Offset: 0x00051B72
		[Property]
		[Title("Enable Linear Constraint")]
		[Obsolete("Does not function.")]
		[DefaultValue(true)]
		public bool EnableLinearConstraint { get; set; } = true;

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x0005397B File Offset: 0x00051B7B
		// (set) Token: 0x0600149D RID: 5277 RVA: 0x00053983 File Offset: 0x00051B83
		[Property]
		[Title("Enable Angular Constraint")]
		[Obsolete("Does not function.")]
		[DefaultValue(true)]
		public bool EnableAngularConstraint { get; set; } = true;

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x0005398C File Offset: 0x00051B8C
		// (set) Token: 0x0600149F RID: 5279 RVA: 0x00053994 File Offset: 0x00051B94
		[Property]
		[Title("Motor frequency")]
		[Obsolete("Does not function.")]
		[DefaultValue(10)]
		[Description("Range 0 - 30 (only used when driving the relative offset through the offset input)")]
		public float MotorFrequency { get; set; } = 10f;

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x0005399D File Offset: 0x00051B9D
		// (set) Token: 0x060014A1 RID: 5281 RVA: 0x000539A5 File Offset: 0x00051BA5
		[Property]
		[Title("Motor damping ratio")]
		[Obsolete("Does not function.")]
		[DefaultValue(1)]
		[Description("Range 0 - 1 (only used when driving the relative offset through the offset input)")]
		public float MotorDampingRatio { get; set; } = 1f;

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x000539AE File Offset: 0x00051BAE
		// (set) Token: 0x060014A3 RID: 5283 RVA: 0x000539B6 File Offset: 0x00051BB6
		[Property]
		[Title("Motor max force")]
		[Obsolete("Does not function.")]
		[DefaultValue(0)]
		[Description("Measured in multiples (e.g. 10x) of the mass where zero means NO limit (only used when driving the relative offset through the offset input)")]
		public float MotorMaxForceMultiplier { get; set; }

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x000539BF File Offset: 0x00051BBF
		// (set) Token: 0x060014A5 RID: 5285 RVA: 0x000539C7 File Offset: 0x00051BC7
		[Property("useEntityPivot", null)]
		[Title("Force Pivot")]
		[DefaultValue(false)]
		[Description("Force joint position as constraint pivot")]
		public bool UseEntityPivot { get; set; }

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x000539D0 File Offset: 0x00051BD0
		// (set) Token: 0x060014A7 RID: 5287 RVA: 0x000539D8 File Offset: 0x00051BD8
		[Property]
		[Title("Limit End Point")]
		[DefaultValue(false)]
		[Description("Stop at the end point")]
		public bool LimitEndPoint { get; set; }

		// Token: 0x060014A8 RID: 5288 RVA: 0x000539E4 File Offset: 0x00051BE4
		protected override PhysicsJoint CreateJoint(PhysicsBody body1, PhysicsBody body2)
		{
			float minLength = 0f;
			float maxLength = 0f;
			if (this.LimitEndPoint)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				maxLength = Vector3.DistanceBetween(this.Position, this.SlideAxis);
			}
			Vector3 axisDirection = (this.SlideAxis - this.Position).Normal;
			if (this.UseEntityPivot)
			{
				Rotation rotation = Rotation.LookAt(axisDirection);
				PhysicsPoint point = new PhysicsPoint(body1, new Vector3?(body1.Transform.PointToLocal(this.Position)), new Rotation?(body1.Transform.RotationToLocal(rotation)));
				PhysicsPoint point2 = new PhysicsPoint(body2, new Vector3?(body2.Transform.PointToLocal(this.Position)), new Rotation?(body2.Transform.RotationToLocal(rotation)));
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.joint = PhysicsJoint.CreateSlider(point, point2, minLength, maxLength);
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.joint = PhysicsJoint.CreateSlider(body1, body2, axisDirection, minLength, maxLength);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.joint.Friction = this.SlideFriction;
			return this.joint;
		}

		// Token: 0x040006B7 RID: 1719
		private SliderJoint joint;
	}
}
