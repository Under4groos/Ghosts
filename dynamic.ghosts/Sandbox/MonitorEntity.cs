using System;
using System.Runtime.CompilerServices;
using Sandbox.Internal;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x0200017F RID: 383
	[Library("func_monitor")]
	[HammerEntity]
	[Title("Monitor")]
	[Category("Gameplay")]
	[Icon("monitor")]
	[Description("A monitor that renders the view from a given <see cref=\"T:Sandbox.CameraEntity\">point_camera</see> entity. This entity requires a material with $MonitorTexture dynamic expression on one of its textures, usually \"Color\", to function properly.")]
	public class MonitorEntity : BrushEntity
	{
		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x00046848 File Offset: 0x00044A48
		// (set) Token: 0x0600122E RID: 4654 RVA: 0x00046850 File Offset: 0x00044A50
		[Property("Camera name", null)]
		[FGDType("target_destination", "", "")]
		[Description("Name of a <see cref=\"T:Sandbox.CameraEntity\">point_camera</see> to display.")]
		public string CameraName { get; set; }

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x00046859 File Offset: 0x00044A59
		// (set) Token: 0x06001230 RID: 4656 RVA: 0x00046870 File Offset: 0x00044A70
		[Net]
		[Description("The camera instance currently being displayed.")]
		public unsafe CameraEntity TargetCamera
		{
			get
			{
				return *this._repback__TargetCamera.GetValue();
			}
			set
			{
				VarUnmanaged<EntityHandle<CameraEntity>> repback__TargetCamera = this._repback__TargetCamera;
				EntityHandle<CameraEntity> entityHandle = value;
				repback__TargetCamera.SetValue(entityHandle);
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x00046891 File Offset: 0x00044A91
		// (set) Token: 0x06001232 RID: 4658 RVA: 0x0004689E File Offset: 0x00044A9E
		[Net]
		private string ModelName
		{
			get
			{
				return this._repback__ModelName.GetValue();
			}
			set
			{
				this._repback__ModelName.SetValue(value);
			}
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x000468AC File Offset: 0x00044AAC
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetCamera = (Entity.FindByName(this.CameraName, null) as CameraEntity);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ModelName = base.GetModelName();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetModel("");
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00046901 File Offset: 0x00044B01
		public override void ClientSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ClientSpawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.so = new ScenePortal(GlobalGameNamespace.Map.Scene, Model.Load(this.ModelName), base.Transform, false, 512);
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0004693F File Offset: 0x00044B3F
		protected override void OnDestroy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnDestroy();
			if (this.so != null && this.so.IsValid)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.so.Delete();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.so = null;
			}
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00046980 File Offset: 0x00044B80
		[Event.FrameAttribute]
		protected void OnFrame()
		{
			if (!base.IsClient)
			{
				return;
			}
			if (this.so == null || !this.so.IsValid)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.so.Transform = base.Transform;
			if (!this.TargetCamera.IsValid())
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.so.ViewPosition = this.TargetCamera.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.so.ViewRotation = this.TargetCamera.Rotation;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.so.FieldOfView = this.TargetCamera.Fov;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.so.ZNear = this.TargetCamera.ZNear;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.so.ZFar = this.TargetCamera.ZFar;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.so.Aspect = this.TargetCamera.Aspect;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00046A74 File Offset: 0x00044C74
		[Input]
		[Description("Set a new camera to display by its <see cref=\"P:Sandbox.Entity.Name\">name</see>.")]
		public void SetCamera(string entityName)
		{
			Entity ent = Entity.FindByName(entityName, null);
			if (!ent.IsValid)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.Log.Warning(FormattableStringFactory.Create("{0}: SetCamera with invalid entity!", new object[]
				{
					this
				}));
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetCamera = (ent as CameraEntity);
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x00046AC6 File Offset: 0x00044CC6
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarUnmanaged<EntityHandle<CameraEntity>>>(ref this._repback__TargetCamera, "TargetCamera", false, false);
			builder.Register<VarGeneric<string>>(ref this._repback__ModelName, "ModelName", false, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x040005BF RID: 1471
		private ScenePortal so;

		// Token: 0x040005C0 RID: 1472
		private VarUnmanaged<EntityHandle<CameraEntity>> _repback__TargetCamera = new VarUnmanaged<EntityHandle<CameraEntity>>();

		// Token: 0x040005C1 RID: 1473
		private VarGeneric<string> _repback__ModelName = new VarGeneric<string>();
	}
}
