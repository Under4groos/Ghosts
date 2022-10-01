using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Sandbox.Internal;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x0200016E RID: 366
	[PhysicsConstraint]
	public abstract class BaseConstraint : Entity
	{
		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060010B7 RID: 4279 RVA: 0x00042F0E File Offset: 0x0004110E
		// (set) Token: 0x060010B8 RID: 4280 RVA: 0x00042F16 File Offset: 0x00041116
		[Property("attach1", null)]
		[Title("Entity 1")]
		[FGDType("target_destination", "", "")]
		[Description("The source entity to constrain from. Leave empty to constrain from the world entity.")]
		public string EntityName1 { get; set; }

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x00042F1F File Offset: 0x0004111F
		// (set) Token: 0x060010BA RID: 4282 RVA: 0x00042F27 File Offset: 0x00041127
		[Property("attach2", null)]
		[Title("Entity 2")]
		[FGDType("target_destination", "", "")]
		[Description("The entity we constrain to.")]
		public string EntityName2 { get; set; }

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x00042F30 File Offset: 0x00041130
		// (set) Token: 0x060010BC RID: 4284 RVA: 0x00042F38 File Offset: 0x00041138
		[Property]
		[Title("Enable Collision")]
		[global::DefaultValue(false)]
		[Description("Constraints disable collision between the attached entities. In some rare cases we want to enable this collision.")]
		public bool EnableCollision { get; set; }

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x00042F41 File Offset: 0x00041141
		// (set) Token: 0x060010BE RID: 4286 RVA: 0x00042F49 File Offset: 0x00041149
		[Property]
		[Title("Impulse Limit to Break (kg)")]
		[Category("Break Limits")]
		[global::DefaultValue(0)]
		[Description("The amount of impulse an impact must apply to the constraint to break it. A way of calculating this is to set it to the mass of an object that would break this constraint if it were resting on the constrained objects.")]
		public float ForceLimit { get; set; }

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x00042F52 File Offset: 0x00041152
		// (set) Token: 0x060010C0 RID: 4288 RVA: 0x00042F5A File Offset: 0x0004115A
		[Property]
		[Title("Angular Impulse Limit to Break (kg * distance)")]
		[Category("Break Limits")]
		[global::DefaultValue(0)]
		[Description("The amount of angular impulse required to break the constraint. A way of calculating this is to multiply any reference mass by the resting distance (from the center of mass of the object) needed to break the constraint.")]
		public float TorqueLimit { get; set; }

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x00042F63 File Offset: 0x00041163
		// (set) Token: 0x060010C2 RID: 4290 RVA: 0x00042F6B File Offset: 0x0004116B
		[Property]
		[Title("Play Sound on Break")]
		[FGDType("sound", "", "")]
		[Category("Break Limits")]
		[Description("A sound played when the constraint is broken.")]
		public string BreakSound { get; set; }

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x00042F74 File Offset: 0x00041174
		// (set) Token: 0x060010C4 RID: 4292 RVA: 0x00042F7C File Offset: 0x0004117C
		public Entity Entity1 { get; set; }

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x00042F85 File Offset: 0x00041185
		// (set) Token: 0x060010C6 RID: 4294 RVA: 0x00042F8D File Offset: 0x0004118D
		public Entity Entity2 { get; set; }

		// Token: 0x060010C7 RID: 4295 RVA: 0x00042F96 File Offset: 0x00041196
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Transmit = TransmitType.Never;
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x00042FAF File Offset: 0x000411AF
		protected override void OnDestroy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnDestroy();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			PhysicsJoint joint = this.Joint;
			if (joint != null)
			{
				joint.Remove();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Joint = null;
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x00042FE0 File Offset: 0x000411E0
		[Event.Entity.PostSpawnAttribute]
		[Event.Entity.PostCleanupAttribute]
		public void PostSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Entity1 = (string.IsNullOrEmpty(this.EntityName1) ? GlobalGameNamespace.Map.Entity : Entity.FindByName(this.EntityName1, null));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Entity2 = (string.IsNullOrEmpty(this.EntityName2) ? GlobalGameNamespace.Map.Entity : Entity.FindByName(this.EntityName2, null));
			if (!this.Entity1.IsValid() || !this.Entity2.IsValid())
			{
				return;
			}
			if (!this.Entity1.PhysicsGroup.IsValid() && !this.Entity2.PhysicsGroup.IsValid())
			{
				return;
			}
			PhysicsBody body;
			PhysicsBody body2;
			if (!this.Entity1.PhysicsGroup.IsValid() && this.Entity2.PhysicsGroup.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				body = GlobalGameNamespace.Map.Physics.Body;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				body2 = this.Entity2.PhysicsGroup.GetBody(0);
			}
			else if (this.Entity1.PhysicsGroup.IsValid() && !this.Entity2.PhysicsGroup.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				body = GlobalGameNamespace.Map.Physics.Body;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				body2 = this.Entity1.PhysicsGroup.GetBody(0);
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				body = this.Entity1.PhysicsGroup.GetBody(0);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				body2 = this.Entity2.PhysicsGroup.GetBody(0);
			}
			if (!body.IsValid() || !body2.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Log.Warning(FormattableStringFactory.Create("{0} failed to create constraint due to invalid entities!", new object[]
				{
					this
				}));
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Joint = this.CreateJoint(body, body2);
			if (this.Joint == null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Log.Warning(FormattableStringFactory.Create("{0} failed to create constraint joint!", new object[]
				{
					this
				}));
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Joint.Collisions = this.EnableCollision;
			if (this.ForceLimit > 0f || this.TorqueLimit > 0f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Joint.Strength = this.ForceLimit;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Joint.AngularStrength = this.TorqueLimit;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Joint.OnBreak += delegate()
				{
					this.OnBreakCallback();
				};
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Joint.OnBreak += delegate()
			{
				base.Delete();
			};
		}

		// Token: 0x060010CA RID: 4298
		[Description("Called to create the joint/constraint.")]
		protected abstract PhysicsJoint CreateJoint(PhysicsBody body1, PhysicsBody body2);

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x0004326A File Offset: 0x0004146A
		// (set) Token: 0x060010CC RID: 4300 RVA: 0x00043272 File Offset: 0x00041472
		[Description("Fired when the constraint breaks due to exceeding force limits or the Break input.")]
		protected Entity.Output OnBreak { get; set; }

		// Token: 0x060010CD RID: 4301 RVA: 0x0004327C File Offset: 0x0004147C
		protected void OnBreakCallback()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnBreak.Fire(this, 0f);
			if (!string.IsNullOrWhiteSpace(this.BreakSound))
			{
				Vector3 pos = this.Position;
				if (this.Joint != null && this.Joint.Body1.IsValid() && this.Joint.Body2.IsValid())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					pos = this.Joint.Body1.Position.LerpTo(this.Joint.Body2.Position, 0.5f);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Sound.FromWorld(this.BreakSound, pos);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Delete();
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x00043333 File Offset: 0x00041533
		[Input]
		[Description("Breaks the constraint.")]
		public void Break()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnBreakCallback();
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00043340 File Offset: 0x00041540
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void CreateHammerOutputs()
		{
			this.OnBreak = new Entity.Output(this, "OnBreak");
			base.CreateHammerOutputs();
		}

		// Token: 0x0400053F RID: 1343
		private PhysicsJoint Joint;
	}
}
