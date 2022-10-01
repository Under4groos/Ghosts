using System;
using System.Runtime.CompilerServices;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x020001A0 RID: 416
	[Library("phys_spring")]
	[HammerEntity]
	[EditorModel("models/editor/axis_helper_arrow.vmdl", "white", "white")]
	[Title("Spring Constraint")]
	[Category("Constraints")]
	[Icon("waves")]
	[Description("A physically simulated spring. 'Length' is what's known as the 'natural spring length'. This is how long the spring would be if it was at rest (nothing hanging on it or attached). When you attach something to the spring, it will stretch longer than its 'natural length'. The amount of stretch is determined by the 'Spring Frequency'. The larger the spring frequency the less stretch the spring.")]
	public class SpringConstraint : BaseConstraint
	{
		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x00053B2E File Offset: 0x00051D2E
		// (set) Token: 0x060014AB RID: 5291 RVA: 0x00053B36 File Offset: 0x00051D36
		[Property]
		[PointLine]
		[FGDType("world_point", "", "")]
		[Description("Use the helper. Drag it out to match the virtual spring.")]
		public Vector3 SpringAxis { get; set; }

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x00053B3F File Offset: 0x00051D3F
		// (set) Token: 0x060014AD RID: 5293 RVA: 0x00053B47 File Offset: 0x00051D47
		[Property]
		[DefaultValue(0)]
		[Description("How long the spring would be if it was at rest (nothing hanging on it or attached). 0 means the length of the helper.")]
		public float Length { get; set; }

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x00053B50 File Offset: 0x00051D50
		// (set) Token: 0x060014AF RID: 5295 RVA: 0x00053B58 File Offset: 0x00051D58
		[Property]
		[MinMax(0f, 30f)]
		[DefaultValue(5f)]
		[Description("The stiffness of the spring.  The larger the number the less the spring will stretch. The maximum should be not more than 30!")]
		public float Frequency { get; set; } = 5f;

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x00053B61 File Offset: 0x00051D61
		// (set) Token: 0x060014B1 RID: 5297 RVA: 0x00053B69 File Offset: 0x00051D69
		[Property]
		[Title("Damping Ratio")]
		[DefaultValue(0.7f)]
		[Description("How much energy the spring loses. Values less than one give you an oscillating spring. A value of one makes the spring return without overshooting")]
		public float Damping { get; set; } = 0.7f;

		// Token: 0x060014B2 RID: 5298 RVA: 0x00053B74 File Offset: 0x00051D74
		protected override PhysicsJoint CreateJoint(PhysicsBody body1, PhysicsBody body2)
		{
			PhysicsPoint a = new PhysicsPoint(body1, new Vector3?(body1.Transform.PointToLocal(this.Position)), null);
			PhysicsPoint point2 = new PhysicsPoint(body2, new Vector3?(body2.Transform.PointToLocal(this.SpringAxis)), null);
			int minLength = 0;
			float maxLength = (this.Length > 0f) ? this.Length : Vector3.DistanceBetween(this.Position, this.SpringAxis);
			SpringJoint springJoint = PhysicsJoint.CreateSpring(a, point2, (float)minLength, maxLength);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			springJoint.SpringLinear = new PhysicsSpring(this.Frequency, this.Damping, 0f);
			return springJoint;
		}
	}
}
