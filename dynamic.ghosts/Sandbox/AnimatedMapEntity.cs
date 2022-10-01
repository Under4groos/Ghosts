using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Sandbox.Internal;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x02000178 RID: 376
	[Library("prop_animated")]
	[HammerEntity]
	[Model(Archetypes = ModelArchetype.animated_model)]
	[RenderFields]
	[VisGroup(VisGroup.Dynamic)]
	[Tag("PropDynamic")]
	[Title("Animated Entity")]
	[Category("Gameplay")]
	[Icon("animation")]
	[Description("A static prop that can play animations. If a door is wanted, please use the door entity.")]
	public class AnimatedMapEntity : AnimatedEntity
	{
		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x00044476 File Offset: 0x00042676
		// (set) Token: 0x06001135 RID: 4405 RVA: 0x00044483 File Offset: 0x00042683
		[Property]
		[Net]
		[FGDType("sequence", "", "")]
		[Description("The name of the idle animation that this prop will revert to whenever it finishes a random or forced animation.")]
		private string DefaultAnimation
		{
			get
			{
				return this._repback__DefaultAnimation.GetValue();
			}
			set
			{
				this._repback__DefaultAnimation.SetValue(value);
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x00044491 File Offset: 0x00042691
		// (set) Token: 0x06001137 RID: 4407 RVA: 0x0004449F File Offset: 0x0004269F
		[Property]
		[Net]
		[global::DefaultValue(false)]
		[Description("Allow this entity to use its animgraph")]
		private unsafe bool UseAnimationGraph
		{
			get
			{
				return *this._repback__UseAnimationGraph.GetValue();
			}
			set
			{
				this._repback__UseAnimationGraph.SetValue(value);
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x000444AE File Offset: 0x000426AE
		// (set) Token: 0x06001139 RID: 4409 RVA: 0x000444BC File Offset: 0x000426BC
		[Property]
		[Net]
		[global::DefaultValue(false)]
		[Description("If set, the prop will not loop its animation, but hold the last frame.")]
		private unsafe bool HoldAnimation
		{
			get
			{
				return *this._repback__HoldAnimation.GetValue();
			}
			set
			{
				this._repback__HoldAnimation.SetValue(value);
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x000444D4 File Offset: 0x000426D4
		// (set) Token: 0x0600113A RID: 4410 RVA: 0x000444CB File Offset: 0x000426CB
		[Property]
		[Title("Animate On Server")]
		[global::DefaultValue(false)]
		[Description("Enable this to animate on the server, such as models with physics bodies attached to bones. Use sparingly as there is a performance cost.")]
		private bool DoAnimateOnServer { get; set; }

		// Token: 0x0600113C RID: 4412 RVA: 0x000444DC File Offset: 0x000426DC
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetupPhysicsFromModel(PhysicsMotionType.Keyframed, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.AnimateOnServer = this.DoAnimateOnServer;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Setup();
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00044513 File Offset: 0x00042713
		public override void OnClientActive(Client cl)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Setup();
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x00044520 File Offset: 0x00042720
		private void Setup()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.UseAnimGraph = this.UseAnimationGraph;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAnimation(this.DefaultAnimation);
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x00044544 File Offset: 0x00042744
		// (set) Token: 0x06001140 RID: 4416 RVA: 0x0004454C File Offset: 0x0004274C
		[Description("Fired whenever a new animation has begun playing.")]
		private Entity.Output OnAnimationBegun { get; set; }

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x00044555 File Offset: 0x00042755
		// (set) Token: 0x06001142 RID: 4418 RVA: 0x0004455D File Offset: 0x0004275D
		[Description("Fired whenever an animation is complete.")]
		private Entity.Output OnAnimationDone { get; set; }

		// Token: 0x06001143 RID: 4419 RVA: 0x00044568 File Offset: 0x00042768
		private void PlayAnimation(string name)
		{
			if (base.UseAnimGraph)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.CurrentSequence.Name = name;
			if (base.IsServer)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnAnimationBegun.Fire(this, 0f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SetAnimationRPC(name);
			}
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x000445C0 File Offset: 0x000427C0
		[ClientRpc]
		private void SetAnimationRPC(string name)
		{
			if (!this.SetAnimationRPC__RpcProxy(name, null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PlayAnimation(name);
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x000445EC File Offset: 0x000427EC
		protected override void OnSequenceFinished(bool looped)
		{
			if (!this.HoldAnimation)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SetAnimation(this.DefaultAnimation);
			}
			if (base.IsServer)
			{
				this.OnAnimationDone.Fire(this, 0f);
			}
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0004462F File Offset: 0x0004282F
		[Input]
		[Description("Play a specific animation (sequence) on the entity. The parameter should be the name of the animation.")]
		public void SetAnimation(string name)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PlayAnimation(name);
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0004463D File Offset: 0x0004283D
		[Input]
		[Description("Sets the default animation. This is the animation will auto repeat if looping is enabled.")]
		private void SetDefaultAnimation(string name)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DefaultAnimation = name;
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0004464B File Offset: 0x0004284B
		[Input]
		[Description("Sets the animation playback rate. 1 is normal, 0.5 is half speed, etc.")]
		private void SetPlaybackRate(float rate)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PlaybackRate = rate;
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0004465C File Offset: 0x0004285C
		[Input]
		[Description("Set an animation graph parameter. Format: \"name=value\"")]
		private void SetAnimGraphParameterBool(string data)
		{
			string[] args = data.Split(new char[]
			{
				'='
			});
			if (args.Length != 2)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Log.Warning(FormattableStringFactory.Create("AnimatedMapEntity.SetAnimGraphParameterBool was given invalid input \"{0}\", expceted \"name=value\"!", new object[]
				{
					args
				}));
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetAnimParameter(args[0], bool.Parse(args[1]));
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x000446BC File Offset: 0x000428BC
		[Input]
		[Description("Set an animation graph parameter. Format: \"name=value\"")]
		private void SetAnimGraphParameterInt(string data)
		{
			string[] args = data.Split(new char[]
			{
				'='
			});
			if (args.Length != 2)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Log.Warning(FormattableStringFactory.Create("AnimatedMapEntity.SetAnimGraphParameterInt was given invalid input \"{0}\", expceted \"name=value\"!", new object[]
				{
					args
				}));
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetAnimParameter(args[0], int.Parse(args[1]));
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x0004471C File Offset: 0x0004291C
		[Input]
		[Description("Set an animation graph parameter. Format: \"name=value\"")]
		private void SetAnimGraphParameterFloat(string data)
		{
			string[] args = data.Split(new char[]
			{
				'='
			});
			if (args.Length != 2)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Log.Warning(FormattableStringFactory.Create("AnimatedMapEntity.SetAnimGraphParameterFloat was given invalid input \"{0}\", expceted \"name=value\"!", new object[]
				{
					args
				}));
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetAnimParameter(args[0], float.Parse(args[1]));
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x0004477C File Offset: 0x0004297C
		[Input]
		[Description("Set an animation graph parameter. Format: \"name=value\"")]
		private void SetAnimGraphParameterVector(string data)
		{
			string[] args = data.Split(new char[]
			{
				'='
			});
			if (args.Length != 2)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Log.Warning(FormattableStringFactory.Create("AnimatedMapEntity.SetAnimGraphParameterVector was given invalid input \"{0}\", expceted \"name=value\"!", new object[]
				{
					args
				}));
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetAnimParameter(args[0], Vector3.Parse(args[1]));
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x000447DC File Offset: 0x000429DC
		[Input]
		[Description("Set an animation graph parameter. Format: \"name=value\"")]
		private void SetAnimGraphParameterAngles(string data)
		{
			string[] args = data.Split(new char[]
			{
				'='
			});
			if (args.Length != 2)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Log.Warning(FormattableStringFactory.Create("AnimatedMapEntity.SetAnimGraphParameterVector was given invalid input \"{0}\", expceted \"name=value\"!", new object[]
				{
					args
				}));
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetAnimParameter(args[0], Angles.Parse(args[1]).ToRotation());
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00044843 File Offset: 0x00042A43
		[Input("Delete")]
		[Description("Deletes this prop.")]
		protected void DeleteInput()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Delete();
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x00044850 File Offset: 0x00042A50
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

		// Token: 0x06001150 RID: 4432 RVA: 0x000448C8 File Offset: 0x00042AC8
		[Input("SetMaterialGroup")]
		[Description("Sets the material group of the props' model by name, as set in ModelDoc.")]
		protected void SetMaterialGroupInput(string group)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetMaterialGroup(group);
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x000448D8 File Offset: 0x00042AD8
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

		// Token: 0x06001152 RID: 4434 RVA: 0x0004493D File Offset: 0x00042B3D
		[Input]
		[Description("Enables or disables collisions on this prop.")]
		protected void SetCollisions(bool enabled)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableSolidCollisions = enabled;
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0004494B File Offset: 0x00042B4B
		[Input]
		[Description("Enables or disables collisions on this prop.")]
		protected void SetVisible(bool enabled)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableDrawing = enabled;
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x0004495C File Offset: 0x00042B5C
		protected bool SetAnimationRPC__RpcProxy(string name, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("SetAnimationRPC", new object[]
				{
					name
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(213961666, this))
			{
				if (!NetRead.IsSupported(name))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] SetAnimationRPC is not allowed to use String for the parameter 'name'!");
					return false;
				}
				writer.Write<string>(name);
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x000449E4 File Offset: 0x00042BE4
		private void SetAnimationRPC(To toTarget, string name)
		{
			this.SetAnimationRPC__RpcProxy(name, new To?(toTarget));
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x000449F4 File Offset: 0x00042BF4
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id == 213961666)
			{
				string __name = null;
				__name = read.ReadClass<string>(__name);
				if (!Prediction.WasPredicted("SetAnimationRPC", new object[]
				{
					__name
				}))
				{
					this.SetAnimationRPC(__name);
				}
				return;
			}
			base.OnCallRemoteProcedure(id, read);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00044A3C File Offset: 0x00042C3C
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarGeneric<string>>(ref this._repback__DefaultAnimation, "DefaultAnimation", false, false);
			builder.Register<VarUnmanaged<bool>>(ref this._repback__UseAnimationGraph, "UseAnimationGraph", false, false);
			builder.Register<VarUnmanaged<bool>>(ref this._repback__HoldAnimation, "HoldAnimation", false, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00044A89 File Offset: 0x00042C89
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void CreateHammerOutputs()
		{
			this.OnAnimationBegun = new Entity.Output(this, "OnAnimationBegun");
			this.OnAnimationDone = new Entity.Output(this, "OnAnimationDone");
			base.CreateHammerOutputs();
		}

		// Token: 0x0400055D RID: 1373
		private VarGeneric<string> _repback__DefaultAnimation = new VarGeneric<string>();

		// Token: 0x0400055E RID: 1374
		private VarUnmanaged<bool> _repback__UseAnimationGraph = new VarUnmanaged<bool>(false);

		// Token: 0x0400055F RID: 1375
		private VarUnmanaged<bool> _repback__HoldAnimation = new VarUnmanaged<bool>(false);
	}
}
