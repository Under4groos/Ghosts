using System;
using System.Runtime.CompilerServices;

namespace Sandbox
{
	// Token: 0x02000145 RID: 325
	public abstract class CameraMode : EntityComponent, ISingletonComponent
	{
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x0003C0FB File Offset: 0x0003A2FB
		// (set) Token: 0x06000ECA RID: 3786 RVA: 0x0003C108 File Offset: 0x0003A308
		public Vector3 Position
		{
			get
			{
				return this.setup.Position;
			}
			set
			{
				this.setup.Position = value;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x0003C116 File Offset: 0x0003A316
		// (set) Token: 0x06000ECC RID: 3788 RVA: 0x0003C123 File Offset: 0x0003A323
		public Rotation Rotation
		{
			get
			{
				return this.setup.Rotation;
			}
			set
			{
				this.setup.Rotation = value;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x0003C131 File Offset: 0x0003A331
		// (set) Token: 0x06000ECE RID: 3790 RVA: 0x0003C13E File Offset: 0x0003A33E
		public float FieldOfView
		{
			get
			{
				return this.setup.FieldOfView;
			}
			set
			{
				this.setup.FieldOfView = value;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x0003C14C File Offset: 0x0003A34C
		// (set) Token: 0x06000ED0 RID: 3792 RVA: 0x0003C159 File Offset: 0x0003A359
		[Description("If this is set, we won't draw the third person model for this entity")]
		public Entity Viewer
		{
			get
			{
				return this.setup.Viewer;
			}
			set
			{
				this.setup.Viewer = value;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x0003C167 File Offset: 0x0003A367
		// (set) Token: 0x06000ED2 RID: 3794 RVA: 0x0003C16F File Offset: 0x0003A36F
		[Obsolete("Use PostProcess.Add, StandardPostProcess")]
		[Description("Length until the Depth of Field focus point")]
		public float DoFPoint { get; set; }

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x0003C178 File Offset: 0x0003A378
		// (set) Token: 0x06000ED4 RID: 3796 RVA: 0x0003C180 File Offset: 0x0003A380
		[Obsolete("Use PostProcess.Add, StandardPostProcess")]
		[Description("How big is the DoF aperture, in pixels")]
		public float DoFBlurSize { get; set; }

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x0003C189 File Offset: 0x0003A389
		// (set) Token: 0x06000ED6 RID: 3798 RVA: 0x0003C191 File Offset: 0x0003A391
		[DefaultValue(80)]
		[Description("Viewmodel specific setup")]
		public float ViewModelFieldOfView { get; set; } = 80f;

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x0003C19A File Offset: 0x0003A39A
		// (set) Token: 0x06000ED8 RID: 3800 RVA: 0x0003C1AC File Offset: 0x0003A3AC
		[Description("The viewmodel near Z clip plane")]
		public float ViewModelZNear
		{
			get
			{
				return this.setup.ViewModel.ZNear;
			}
			set
			{
				this.setup.ViewModel.ZNear = value;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x0003C1BF File Offset: 0x0003A3BF
		// (set) Token: 0x06000EDA RID: 3802 RVA: 0x0003C1D1 File Offset: 0x0003A3D1
		[Description("The viewmodel far Z clip plane")]
		public float ViewModelZFar
		{
			get
			{
				return this.setup.ViewModel.ZFar;
			}
			set
			{
				this.setup.ViewModel.ZFar = value;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x0003C1E4 File Offset: 0x0003A3E4
		// (set) Token: 0x06000EDC RID: 3804 RVA: 0x0003C1F1 File Offset: 0x0003A3F1
		[Description("The near Z clip plane")]
		public float ZNear
		{
			get
			{
				return this.setup.ZNear;
			}
			set
			{
				this.setup.ZNear = value;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x0003C1FF File Offset: 0x0003A3FF
		// (set) Token: 0x06000EDE RID: 3806 RVA: 0x0003C20C File Offset: 0x0003A40C
		[Description("The far Z clip plane")]
		public float ZFar
		{
			get
			{
				return this.setup.ZFar;
			}
			set
			{
				this.setup.ZFar = value;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x0003C21A File Offset: 0x0003A41A
		// (set) Token: 0x06000EE0 RID: 3808 RVA: 0x0003C227 File Offset: 0x0003A427
		[Description("Use orthographic projection")]
		public bool Ortho
		{
			get
			{
				return this.setup.Ortho;
			}
			set
			{
				this.setup.Ortho = value;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x0003C235 File Offset: 0x0003A435
		// (set) Token: 0x06000EE2 RID: 3810 RVA: 0x0003C242 File Offset: 0x0003A442
		[Description("Acts as a zoom when <see cref=\"P:Sandbox.CameraMode.Ortho\" /> is enabled")]
		public float OrthoSize
		{
			get
			{
				return this.setup.OrthoSize;
			}
			set
			{
				this.setup.OrthoSize = value;
			}
		}

		// Token: 0x06000EE3 RID: 3811
		public abstract void Update();

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0003C250 File Offset: 0x0003A450
		public virtual void Build(ref CameraSetup camSetup)
		{
			float defaultFieldOfView = camSetup.FieldOfView;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Update();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			camSetup = this.setup;
			if (camSetup.FieldOfView == 0f)
			{
				camSetup.FieldOfView = defaultFieldOfView;
			}
			if (camSetup.ViewModel.FieldOfView == 0f)
			{
				camSetup.ViewModel.FieldOfView = camSetup.FieldOfView;
			}
			if (camSetup.ViewModel.ZNear == 0f)
			{
				camSetup.ViewModel.ZNear = 0.1f;
			}
			if (camSetup.ViewModel.ZFar == 0f)
			{
				camSetup.ViewModel.ZFar = 2000f;
			}
			if (camSetup.ZNear == 0f)
			{
				camSetup.ZNear = 10f;
			}
			if (camSetup.ZFar == 0f)
			{
				camSetup.ZFar = 80000f;
			}
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0003C32C File Offset: 0x0003A52C
		[Description("This builds the default behaviour for our input")]
		public virtual void BuildInput(InputBuilder input)
		{
			if (!input.UsingController)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				input.AnalogLook.pitch = input.AnalogLook.pitch * 1.5f;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			input.ViewAngles += input.AnalogLook;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			input.ViewAngles.pitch = input.ViewAngles.pitch.Clamp(-89f, 89f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			input.ViewAngles.roll = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			input.InputDirection = input.AnalogMove;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0003C3C6 File Offset: 0x0003A5C6
		[Description("Camera has become the active camera. You can use this as an opportunity to snap the positions if you're lerping etc.")]
		public virtual void Activated()
		{
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0003C3C8 File Offset: 0x0003A5C8
		[Description("Camera has stopped being the active camera.")]
		public virtual void Deactivated()
		{
		}

		// Token: 0x040004AB RID: 1195
		internal CameraSetup setup;
	}
}
