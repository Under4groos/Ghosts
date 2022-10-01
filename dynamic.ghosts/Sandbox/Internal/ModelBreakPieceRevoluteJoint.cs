using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using ModelDoc;

namespace Sandbox.Internal
{
	// Token: 0x020001E3 RID: 483
	[Library("break_create_joint_revolute")]
	[Axis(Origin = "anchor_position", Angles = "anchor_angles")]
	[HingeJoint(Origin = "anchor_position", Angles = "anchor_angles", EnableLimit = "enable_limit", MinAngle = "min_angle", MaxAngle = "max_angle")]
	[Description("Creates a revolute (hinge) joint between two spawned breakpieces.")]
	internal class ModelBreakPieceRevoluteJoint : IModelBreakCommand
	{
		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x0006424A File Offset: 0x0006244A
		// (set) Token: 0x06001801 RID: 6145 RVA: 0x00064252 File Offset: 0x00062452
		[JsonPropertyName("parent_piece")]
		[FGDType("model_breakpiece", "", "")]
		public string ParentPiece { get; set; }

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x0006425B File Offset: 0x0006245B
		// (set) Token: 0x06001803 RID: 6147 RVA: 0x00064263 File Offset: 0x00062463
		[JsonPropertyName("child_piece")]
		[FGDType("model_breakpiece", "", "")]
		public string ChildPiece { get; set; }

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x0006426C File Offset: 0x0006246C
		// (set) Token: 0x06001805 RID: 6149 RVA: 0x00064274 File Offset: 0x00062474
		[JsonPropertyName("anchor_position")]
		[Title("Anchor Position (relative to model)")]
		public Vector3 Position { get; set; }

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x0006427D File Offset: 0x0006247D
		// (set) Token: 0x06001807 RID: 6151 RVA: 0x00064285 File Offset: 0x00062485
		[JsonPropertyName("anchor_angles")]
		[Title("Anchor Axis (relative to model)")]
		[Description("Axis around which the revolute/hinge joint rotates.")]
		public Angles Angles { get; set; }

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x0006428E File Offset: 0x0006248E
		// (set) Token: 0x06001809 RID: 6153 RVA: 0x00064296 File Offset: 0x00062496
		[Description("Hinge friction.")]
		public float Friction { get; set; }

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x0600180A RID: 6154 RVA: 0x0006429F File Offset: 0x0006249F
		// (set) Token: 0x0600180B RID: 6155 RVA: 0x000642A7 File Offset: 0x000624A7
		[JsonPropertyName("enable_limit")]
		[Description("Whether the angle limit should be enabled or not.")]
		public bool LimitAngles { get; set; }

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x0600180C RID: 6156 RVA: 0x000642B0 File Offset: 0x000624B0
		// (set) Token: 0x0600180D RID: 6157 RVA: 0x000642B8 File Offset: 0x000624B8
		[JsonPropertyName("min_angle")]
		[MinMax(-179f, 179f)]
		public float MinimumAngle { get; set; }

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x000642C1 File Offset: 0x000624C1
		// (set) Token: 0x0600180F RID: 6159 RVA: 0x000642C9 File Offset: 0x000624C9
		[JsonPropertyName("max_angle")]
		[MinMax(-179f, 179f)]
		public float MaximumAngle { get; set; }

		// Token: 0x06001810 RID: 6160 RVA: 0x000642D4 File Offset: 0x000624D4
		public void OnBreak(Breakables.Result res)
		{
			ModelEntity ParentEnt = res.Props.Find(delegate(ModelEntity prop)
			{
				PropGib gib = prop as PropGib;
				return gib != null && gib.BreakpieceName == this.ParentPiece;
			});
			ModelEntity ChildEnt = res.Props.Find(delegate(ModelEntity prop)
			{
				PropGib gib = prop as PropGib;
				return gib != null && gib.BreakpieceName == this.ChildPiece;
			});
			if (ParentEnt == null || this.ChildPiece == null)
			{
				return;
			}
			Vector3 WorldPos = res.Source.Transform.PointToWorld(this.Position);
			Rotation WorldAngle = res.Source.Transform.RotationToWorld(Rotation.From(this.Angles));
			HingeJoint hinge = PhysicsJoint.CreateHinge(ParentEnt.PhysicsBody, ChildEnt.PhysicsBody, WorldPos, WorldAngle.Up);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			hinge.EnableAngularConstraint = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			hinge.EnableLinearConstraint = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			hinge.Friction = this.Friction;
			if (this.LimitAngles)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				hinge.MinAngle = this.MinimumAngle;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				hinge.MaxAngle = this.MaximumAngle;
			}
		}
	}
}
