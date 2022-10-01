using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Sandbox.Component;
using Sandbox.Internal;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x02000147 RID: 327
	[Library("prop_physics")]
	[HammerEntity]
	[CanBeClientsideOnly]
	[Model]
	[RenderFields]
	[VisGroup(VisGroup.Physics)]
	[Title("Prop")]
	[Category("Gameplay")]
	[Icon("chair")]
	[Description("A prop that physically simulates as a single rigid body. It can be constrained to other physics objects using hinges or other constraints. It can also be configured to break when it takes enough damage. Note that the health of the object will be overridden by the health inside the model, to ensure consistent health game-wide. If the model used by the prop is configured to be used as a prop_dynamic (i.e. it should not be physically simulated) then it CANNOT be used as a prop_physics. Upon level load it will display a warning in the console and remove itself. Use a prop_dynamic instead.")]
	public class Prop : BasePhysics
	{
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x0003C4B9 File Offset: 0x0003A6B9
		// (set) Token: 0x06000EEF RID: 3823 RVA: 0x0003C4C1 File Offset: 0x0003A6C1
		[Property]
		[global::DefaultValue(false)]
		[Description("If set, the prop will spawn with motion disabled and will act as a navigation blocker until broken.")]
		public bool Static { get; set; }

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x0003C4CA File Offset: 0x0003A6CA
		// (set) Token: 0x06000EF1 RID: 3825 RVA: 0x0003C4D2 File Offset: 0x0003A6D2
		[Property("boneTransforms", null)]
		[HideInEditor]
		private string BoneTransforms { get; set; }

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x0003C4DB File Offset: 0x0003A6DB
		// (set) Token: 0x06000EF3 RID: 3827 RVA: 0x0003C4E3 File Offset: 0x0003A6E3
		[Property("massscale", null, Title = "Mass Scale")]
		[Category("Physics")]
		[global::DefaultValue(1f)]
		[Description("Multiplier for the object's mass.")]
		private float MassScale { get; set; } = 1f;

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x0003C4EC File Offset: 0x0003A6EC
		// (set) Token: 0x06000EF5 RID: 3829 RVA: 0x0003C4F4 File Offset: 0x0003A6F4
		[Property("lineardamping", null, Title = "Linear Damping")]
		[Category("Physics")]
		[global::DefaultValue(0f)]
		[Description("Physics linear damping.")]
		private float LinearDamping { get; set; }

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x0003C4FD File Offset: 0x0003A6FD
		// (set) Token: 0x06000EF7 RID: 3831 RVA: 0x0003C505 File Offset: 0x0003A705
		[Property("angulardamping", null, Title = "Angular Damping")]
		[Category("Physics")]
		[global::DefaultValue(0f)]
		[Description("Physics angular damping.")]
		private float AngularDamping { get; set; }

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0003C510 File Offset: 0x0003A710
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PhysicsEnabled = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.UsePhysicsCollision = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableHideInFirstPerson = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableShadowInFirstPerson = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add(new string[]
			{
				"prop",
				"solid"
			});
			if (this.Static)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PhysicsEnabled = false;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Components.Add<NavBlocker>(new NavBlocker());
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetupPhysics();
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0003C5B4 File Offset: 0x0003A7B4
		private void SetupPhysics()
		{
			PhysicsGroup physics = base.SetupPhysicsFromModel(PhysicsMotionType.Dynamic, false);
			if (!physics.IsValid())
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ApplyBoneTransforms();
			if (this.MassScale != 1f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				physics.Mass *= this.MassScale;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			physics.LinearDamping = this.LinearDamping;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			physics.AngularDamping = this.AngularDamping;
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0003C628 File Offset: 0x0003A828
		private void ApplyBoneTransforms()
		{
			if (string.IsNullOrWhiteSpace(this.BoneTransforms))
			{
				return;
			}
			string[] array = this.BoneTransforms.Split(';', StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i++)
			{
				string[] split = array[i].Split(':', StringSplitOptions.TrimEntries);
				if (split.Length == 2)
				{
					string boneName = split[0];
					Transform boneTransform = Transform.Parse(split[1]);
					PhysicsBody body = base.GetBonePhysicsBody(base.GetBoneIndex(boneName));
					if (body.IsValid())
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						body.Transform = base.Transform.ToWorld(boneTransform);
					}
				}
			}
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0003C6B5 File Offset: 0x0003A8B5
		public override void OnNewModel(Model model)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnNewModel(model);
			if (model == null || model.IsError)
			{
				return;
			}
			if (base.IsServer)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.UpdatePropData(model);
			}
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x0003C6E4 File Offset: 0x0003A8E4
		protected virtual void UpdatePropData(Model model)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertServer("UpdatePropData");
			ModelPropData propInfo;
			if (model.TryGetData<ModelPropData>(out propInfo))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Health = propInfo.Health;
			}
			if (base.Health <= 0f)
			{
				base.Health = -1f;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x0003C733 File Offset: 0x0003A933
		// (set) Token: 0x06000EFE RID: 3838 RVA: 0x0003C73B File Offset: 0x0003A93B
		[Description("Fired when the entity gets damaged.")]
		protected Entity.Output OnDamaged { get; set; }

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x0003C744 File Offset: 0x0003A944
		// (set) Token: 0x06000F00 RID: 3840 RVA: 0x0003C74C File Offset: 0x0003A94C
		[Description("This prop won't be able to be damaged for this amount of time")]
		public RealTimeUntil Invulnerable { get; set; }

		// Token: 0x06000F01 RID: 3841 RVA: 0x0003C758 File Offset: 0x0003A958
		public override void TakeDamage(DamageInfo info)
		{
			if (this.Invulnerable > 0f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.ApplyDamageForces(info);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LastDamage = info;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.TakeDamage(info);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnDamaged.Fire(this, 0f);
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0003C7B8 File Offset: 0x0003A9B8
		public override void OnKilled()
		{
			if (base.LifeState != LifeState.Alive)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.LifeState = LifeState.Dead;
			if (this.LastDamage.Flags.HasFlag(DamageFlags.PhysicsImpact))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Velocity = this.lastCollision.This.PreVelocity;
			}
			if (this.HasExplosionBehavior())
			{
				if (this.LastDamage.Flags.HasFlag(DamageFlags.Blast))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					base.LifeState = LifeState.Dying;
					Random rand = new Random();
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.ExplodeAsync(rand.Float(0.05f, 0.25f));
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DoGibs();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DoExplosion();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Delete();
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DoGibs();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Delete();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnKilled();
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0003C8AF File Offset: 0x0003AAAF
		protected override void OnPhysicsCollision(CollisionEventData eventData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.lastCollision = eventData;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnPhysicsCollision(eventData);
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0003C8C9 File Offset: 0x0003AAC9
		private bool HasExplosionBehavior()
		{
			return base.Model != null && !base.Model.IsError && base.Model.HasData<ModelExplosionBehavior>();
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x0003C8ED File Offset: 0x0003AAED
		// (set) Token: 0x06000F06 RID: 3846 RVA: 0x0003C8F5 File Offset: 0x0003AAF5
		[Description("Fired when the entity gets destroyed.")]
		protected Entity.Output OnBreak { get; set; }

		// Token: 0x06000F07 RID: 3847 RVA: 0x0003C900 File Offset: 0x0003AB00
		private void DoGibs()
		{
			Breakables.Result result = new Breakables.Result();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			result.CopyParamsFrom(this.LastDamage);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Breakables.Break(this, result);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnBreak.Fire(this.LastDamage.Attacker, 0f);
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0003C954 File Offset: 0x0003AB54
		public Task ExplodeAsync(float fTime)
		{
			Prop.<ExplodeAsync>d__44 <ExplodeAsync>d__;
			<ExplodeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ExplodeAsync>d__.<>4__this = this;
			<ExplodeAsync>d__.fTime = fTime;
			<ExplodeAsync>d__.<>1__state = -1;
			<ExplodeAsync>d__.<>t__builder.Start<Prop.<ExplodeAsync>d__44>(ref <ExplodeAsync>d__);
			return <ExplodeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0003C9A0 File Offset: 0x0003ABA0
		private void DoExplosion()
		{
			if (base.Model == null || base.Model.IsError)
			{
				return;
			}
			ModelExplosionBehavior explosionBehavior;
			if (!base.Model.TryGetData<ModelExplosionBehavior>(out explosionBehavior))
			{
				return;
			}
			Vector3 srcPos = this.Position;
			if (base.PhysicsBody.IsValid())
			{
				srcPos = base.PhysicsBody.MassCenter;
			}
			if (explosionBehavior.Radius > 0f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				new ExplosionEntity
				{
					Position = srcPos,
					Radius = explosionBehavior.Radius,
					Damage = explosionBehavior.Damage,
					ForceScale = explosionBehavior.Force,
					ParticleOverride = explosionBehavior.Effect,
					SoundOverride = explosionBehavior.Sound
				}.Explode(this);
			}
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0003CA53 File Offset: 0x0003AC53
		protected override void OnDestroy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnDestroy();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Unweld(true, null);
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0003CA6D File Offset: 0x0003AC6D
		[Input]
		[Description("Causes this prop to break, regardless if it is actually breakable or not. (i.e. ignores health and whether the model has gibs)")]
		public void Break()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnKilled();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.LifeState = LifeState.Dead;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Delete();
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0003CA91 File Offset: 0x0003AC91
		[Input("Delete")]
		[Description("Deletes this prop.")]
		protected void DeleteInput()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Delete();
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0003CAA0 File Offset: 0x0003ACA0
		[Input]
		[Description("Sets the scale of the prop, affecting physics and visual scale.")]
		protected void SetScale(float scale)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Scale = scale.Clamp(0.1f, 100f);
			if (base.PhysicsGroup != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PhysicsGroup.RebuildMass();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PhysicsGroup.Mass *= scale.Clamp(1f, 100f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PhysicsGroup.Sleeping = false;
			}
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0003CB18 File Offset: 0x0003AD18
		[Input("SetMaterialGroup")]
		[Description("Sets the material group of the props' model by name, as set in ModelDoc.")]
		protected void SetMaterialGroupInput(string group)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetMaterialGroup(group);
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0003CB28 File Offset: 0x0003AD28
		[Input("SetBodyGroup")]
		[Description("Sets the body group of the props' model by name, as set in ModelDoc. Format is \"name,option\"")]
		protected void SetBodyGroupInput(string group)
		{
			string[] opts = group.Split(new char[]
			{
				' ',
				','
			});
			if (opts.Length != 2)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Log.Warning(FormattableStringFactory.Create("Prop.SetBodyGroup was given invalid input \"{0}\", expceted \"name,option\"!", new object[]
				{
					group
				}));
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetBodyGroup(opts[0], opts[1].ToInt(0));
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0003CB8D File Offset: 0x0003AD8D
		[Input]
		[Description("Enables or disables collisions on this prop.")]
		protected void SetCollisions(bool enabled)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableSolidCollisions = enabled;
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0003CB9B File Offset: 0x0003AD9B
		[Input]
		[Description("Enables or disables collisions on this prop.")]
		protected void SetVisible(bool enabled)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableDrawing = enabled;
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0003CBAC File Offset: 0x0003ADAC
		public void Unweld(bool reweldChildren = false, Prop reweldProp = null)
		{
			if (!this.weldParent.IsValid())
			{
				if (this.childrenProps.Count > 0)
				{
					List<Prop> chilrenPropsCopy = this.childrenProps.ToList<Prop>();
					foreach (Prop prop in chilrenPropsCopy)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						prop.Unweld(false, null);
					}
					if (!reweldChildren)
					{
						return;
					}
					if (!reweldProp.IsValid())
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						reweldProp = chilrenPropsCopy.First<Prop>();
					}
					if (!reweldProp.IsValid())
					{
						return;
					}
					foreach (Prop childProp in chilrenPropsCopy)
					{
						if (childProp != reweldProp)
						{
							RuntimeHelpers.EnsureSufficientExecutionStack();
							reweldProp.Weld(childProp);
						}
					}
				}
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.weldParent.childrenProps.Remove(this);
			if (this.weldParent.PhysicsBody.IsValid())
			{
				foreach (PhysicsShape physicsShape in this.clonedShapes)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					physicsShape.Remove();
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.clonedShapes.Clear();
			if (base.PhysicsBody.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Parent = null;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PhysicsBody.Parent = null;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.EnableSolidCollisions = true;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PhysicsBody.RebuildMass();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.weldParent = null;
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0003CD64 File Offset: 0x0003AF64
		public void Weld(Prop prop)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertServer("Weld");
			if (!prop.IsValid())
			{
				return;
			}
			if (prop == this)
			{
				return;
			}
			if (prop.weldParent == this || this.weldParent == prop)
			{
				return;
			}
			if (this.Parent != null || prop.Parent != null)
			{
				return;
			}
			if (!base.PhysicsBody.IsValid() || !prop.PhysicsBody.IsValid())
			{
				return;
			}
			if (base.PhysicsGroup.BodyCount > 1 || prop.PhysicsGroup.BodyCount > 1)
			{
				return;
			}
			if (prop.childrenProps.Count > 0)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				prop.Unweld(true, this);
			}
			PhysicsBody thisBody = base.PhysicsBody;
			PhysicsBody physicsBody = prop.PhysicsBody;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			physicsBody.Parent = thisBody;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			prop.EnableSolidCollisions = false;
			foreach (PhysicsShape shape in physicsBody.Shapes)
			{
				PhysicsShape clonedShape = thisBody.AddCloneShape(shape);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				clonedShape.DisableTraceQuery();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				prop.clonedShapes.Add(clonedShape);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			prop.Parent = this;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			prop.weldParent = this;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.childrenProps.Add(prop);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			thisBody.RebuildMass();
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0003CEBC File Offset: 0x0003B0BC
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void CreateHammerOutputs()
		{
			this.OnDamaged = new Entity.Output(this, "OnDamaged");
			this.OnBreak = new Entity.Output(this, "OnBreak");
			base.CreateHammerOutputs();
		}

		// Token: 0x040004B5 RID: 1205
		private DamageInfo LastDamage;

		// Token: 0x040004B8 RID: 1208
		private CollisionEventData lastCollision;

		// Token: 0x040004BA RID: 1210
		protected Prop weldParent;

		// Token: 0x040004BB RID: 1211
		protected List<Prop> childrenProps = new List<Prop>();

		// Token: 0x040004BC RID: 1212
		protected List<PhysicsShape> clonedShapes = new List<PhysicsShape>();
	}
}
