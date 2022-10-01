using System;
using System.Runtime.CompilerServices;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x0200019D RID: 413
	[Library("phys_lengthconstraint")]
	[HammerEntity]
	[EditorModel("models/editor/axis_helper_arrow.vmdl", "white", "white")]
	[Particle("particleeffect")]
	[Title("Length Constraint")]
	[Category("Constraints")]
	[Icon("linear_scale")]
	[Description("A constraint that preserves the distance between two entities. If the 'Keep Rigid' flag is set, think of it as a rod. If not, think off it as a virtual rope.")]
	public class LengthConstraint : BaseConstraint
	{
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x0005374A File Offset: 0x0005194A
		// (set) Token: 0x06001484 RID: 5252 RVA: 0x00053752 File Offset: 0x00051952
		[Property("addlength", null)]
		[Title("Additional Length")]
		[DefaultValue(0)]
		[Description("Add (or subtract) this amount to the rest length of the rope.")]
		public float AddLength { get; set; }

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x0005375B File Offset: 0x0005195B
		// (set) Token: 0x06001486 RID: 5254 RVA: 0x00053763 File Offset: 0x00051963
		[Property("minlength", null)]
		[Title("Minimum Length")]
		[DefaultValue(0)]
		[Description("If the constraint is not rigid, this is the minimum length it can be.")]
		public float MinLength { get; set; }

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x0005376C File Offset: 0x0005196C
		// (set) Token: 0x06001488 RID: 5256 RVA: 0x00053774 File Offset: 0x00051974
		[Property("attachpoint", null)]
		[Title("Attached object 2 point")]
		[PointLine]
		[FGDType("world_point", "", "")]
		[Description("The position the rope attaches to object 2")]
		public Vector3 AttachPoint { get; set; }

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x0005377D File Offset: 0x0005197D
		// (set) Token: 0x0600148A RID: 5258 RVA: 0x00053785 File Offset: 0x00051985
		[Property]
		[DefaultValue(false)]
		[Description("Keep the constraint rigid, as if it were a stick.")]
		public bool KeepRigid { get; set; }

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x0005378E File Offset: 0x0005198E
		// (set) Token: 0x0600148C RID: 5260 RVA: 0x00053796 File Offset: 0x00051996
		[Property]
		[EntityReportSource]
		[ResourceType("vpcf")]
		[Description("If give, will spawn given particle as the rope. Particle's Control Point 1 will be set to the target entity, and will be removed if the constraint breaks.")]
		public string ParticleEffect { get; set; }

		// Token: 0x0600148D RID: 5261 RVA: 0x000537A0 File Offset: 0x000519A0
		protected override PhysicsJoint CreateJoint(PhysicsBody body1, PhysicsBody body2)
		{
			if (!string.IsNullOrEmpty(this.ParticleEffect))
			{
				Entity target = new Entity
				{
					Position = this.AttachPoint,
					Name = base.Name + "_PartAttach",
					Parent = body2.GetEntity(),
					Transmit = TransmitType.Always
				};
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.particle = new ParticleSystemEntity
				{
					Position = this.Position,
					ParticleSystemName = this.ParticleEffect,
					Parent = body1.GetEntity(),
					ControlPoint1 = target.Name
				};
			}
			PhysicsPoint a = new PhysicsPoint(body1, new Vector3?(body1.Transform.PointToLocal(this.Position)), null);
			PhysicsPoint point2 = new PhysicsPoint(body2, new Vector3?(body2.Transform.PointToLocal(this.AttachPoint)), null);
			float minLength = this.MinLength;
			float maxLength = Vector3.DistanceBetween(this.Position, this.AttachPoint);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			maxLength += this.AddLength;
			if (this.KeepRigid)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				minLength = maxLength;
			}
			return PhysicsJoint.CreateSpring(a, point2, minLength, maxLength);
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x000538C9 File Offset: 0x00051AC9
		protected override void OnDestroy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnDestroy();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ParticleSystemEntity particleSystemEntity = this.particle;
			if (particleSystemEntity == null)
			{
				return;
			}
			particleSystemEntity.Delete();
		}

		// Token: 0x040006AB RID: 1707
		private ParticleSystemEntity particle;
	}
}
