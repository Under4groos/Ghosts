using System;
using System.Runtime.CompilerServices;
using Sandbox.Internal;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x020001A3 RID: 419
	[Library("info_particle_system")]
	[HammerEntity]
	[Particle(null)]
	[VisGroup(VisGroup.Particles)]
	[EditorModel("models/editor/cone_helper.vmdl", "white", "white")]
	[Title("Particle System")]
	[Category("Effects")]
	[Icon("auto_fix_high")]
	[Description("A entity that represents and allows control of a single particle system.")]
	public class ParticleSystemEntity : Entity
	{
		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x0005483A File Offset: 0x00052A3A
		// (set) Token: 0x060014ED RID: 5357 RVA: 0x00054842 File Offset: 0x00052A42
		[Property("effect_name", null)]
		[EntityReportSource]
		[ResourceType("vpcf")]
		[Description("The name of the particle system.")]
		public string ParticleSystemName { get; set; }

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x0005484B File Offset: 0x00052A4B
		// (set) Token: 0x060014EF RID: 5359 RVA: 0x00054853 File Offset: 0x00052A53
		[Property("start_active", null)]
		[DefaultValue(true)]
		[Description("Should this system start active when it enters a player's PVS?")]
		public bool StartActive { get; set; } = true;

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x0005485C File Offset: 0x00052A5C
		// (set) Token: 0x060014F1 RID: 5361 RVA: 0x00054864 File Offset: 0x00052A64
		[Property("snapshot_file", null)]
		[ResourceType("vsnap")]
		[Description("A particle snapshot file) to be loaded and used by this particle system. Set to Control Point 0.")]
		public string SnapshotFile { get; set; }

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x0005486D File Offset: 0x00052A6D
		// (set) Token: 0x060014F3 RID: 5363 RVA: 0x00054875 File Offset: 0x00052A75
		[Property("snapshot_mesh", null)]
		[FGDType("node_id", "", "")]
		[Description("ID of a mesh in the map to be used to generate a particle snapshot, overriding Snapshot File property.<br /> Meshes tied to an entity cannot be used.")]
		private string SnapshotMesh { get; set; }

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x0005487E File Offset: 0x00052A7E
		// (set) Token: 0x060014F5 RID: 5365 RVA: 0x00054886 File Offset: 0x00052A86
		[Property("datacp", null, Title = "Data Control Point (-1 unused)")]
		[DefaultValue(-1)]
		[Description("What controlpoint to set data on. -1 means this is not used.")]
		public int DataControlPoint { get; set; } = -1;

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x0005488F File Offset: 0x00052A8F
		// (set) Token: 0x060014F7 RID: 5367 RVA: 0x00054897 File Offset: 0x00052A97
		[Property(Title = "Data Control Point Value")]
		[Description("Data Control Point Value to set.")]
		public Vector3 DataControlPointValue { get; set; }

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x000548A0 File Offset: 0x00052AA0
		// (set) Token: 0x060014F9 RID: 5369 RVA: 0x000548A8 File Offset: 0x00052AA8
		[Property(Title = "Tint Control Point (-1 unused)")]
		[DefaultValue(-1)]
		[Description("What controlpoint to set data on. -1 means this is not used.")]
		public int TintControlPoint { get; set; } = -1;

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x000548B1 File Offset: 0x00052AB1
		// (set) Token: 0x060014FB RID: 5371 RVA: 0x000548B9 File Offset: 0x00052AB9
		[Property(Title = "Tint Control Point Color")]
		[DefaultValue("255 255 255 255")]
		[Description("Set the Tint of the Particle.")]
		public Color TintControlPointColor { get; set; }

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x000548C2 File Offset: 0x00052AC2
		// (set) Token: 0x060014FD RID: 5373 RVA: 0x000548CA File Offset: 0x00052ACA
		[Property("cpoint0", null, Title = "Control Point 0")]
		[FGDType("target_destination", "", "")]
		[Description("If set, control point 0 of the effect will be at this entity's location. (Otherwise it is at the info_particle_system origin)")]
		public string ControlPoint0 { get; set; }

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x000548D3 File Offset: 0x00052AD3
		// (set) Token: 0x060014FF RID: 5375 RVA: 0x000548DB File Offset: 0x00052ADB
		[Property("cpoint1", null, Title = "Control Point 1")]
		[FGDType("target_destination", "", "")]
		[Description("If set, control point 1 of the effect will be at this entity's location.")]
		public string ControlPoint1 { get; set; }

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x000548E4 File Offset: 0x00052AE4
		// (set) Token: 0x06001501 RID: 5377 RVA: 0x000548EC File Offset: 0x00052AEC
		[Property("cpoint2", null, Title = "Control Point 2")]
		[FGDType("target_destination", "", "")]
		[Description("If set, control point 2 of the effect will be at this entity's location. If control point 1 is not set, this will be ignored.")]
		public string ControlPoint2 { get; set; }

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x000548F5 File Offset: 0x00052AF5
		// (set) Token: 0x06001503 RID: 5379 RVA: 0x000548FD File Offset: 0x00052AFD
		[Property("cpoint3", null)]
		[FGDType("target_destination", "", "")]
		public string ControlPoint3 { get; set; }

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x00054906 File Offset: 0x00052B06
		// (set) Token: 0x06001505 RID: 5381 RVA: 0x0005490E File Offset: 0x00052B0E
		[Property("cpoint4", null)]
		[FGDType("target_destination", "", "")]
		public string ControlPoint4 { get; set; }

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x00054917 File Offset: 0x00052B17
		// (set) Token: 0x06001507 RID: 5383 RVA: 0x0005491F File Offset: 0x00052B1F
		[Property("cpoint5", null)]
		[FGDType("target_destination", "", "")]
		public string ControlPoint5 { get; set; }

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001508 RID: 5384 RVA: 0x00054928 File Offset: 0x00052B28
		// (set) Token: 0x06001509 RID: 5385 RVA: 0x00054930 File Offset: 0x00052B30
		public bool IsActive { get; set; }

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x00054939 File Offset: 0x00052B39
		// (set) Token: 0x0600150B RID: 5387 RVA: 0x00054941 File Offset: 0x00052B41
		public Particles ActiveSystem { get; protected set; }

		// Token: 0x0600150C RID: 5388 RVA: 0x0005494A File Offset: 0x00052B4A
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Transmit = TransmitType.Always;
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x00054963 File Offset: 0x00052B63
		protected override void OnDestroy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnDestroy();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles activeSystem = this.ActiveSystem;
			if (activeSystem == null)
			{
				return;
			}
			activeSystem.Destroy(true);
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x00054986 File Offset: 0x00052B86
		[Event.Entity.PostCleanupAttribute]
		private void OnMapCleanup()
		{
			if (this.StartActive)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Start();
			}
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0005499B File Offset: 0x00052B9B
		public override void OnClientActive(Client cl)
		{
			if (this.StartActive)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Start();
			}
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x000549B0 File Offset: 0x00052BB0
		[Event.Tick.ServerAttribute]
		private void Think()
		{
			if (this.IsActive && this.ActiveSystem == null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CreateSystem();
				return;
			}
			if (!this.IsActive && this.ActiveSystem != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ActiveSystem.Destroy(true);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ActiveSystem = null;
			}
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x00054A08 File Offset: 0x00052C08
		private void CreateSystem()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles activeSystem = this.ActiveSystem;
			if (activeSystem != null)
			{
				activeSystem.Destroy(true);
			}
			if (string.IsNullOrEmpty(this.ParticleSystemName))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ActiveSystem = Particles.Create(this.ParticleSystemName, this, true);
			if (this.TintControlPoint >= 0)
			{
				this.ActiveSystem.SetPosition(this.TintControlPoint, this.TintControlPointColor);
			}
			if (this.DataControlPoint >= 0)
			{
				this.ActiveSystem.SetPosition(this.DataControlPoint, this.DataControlPointValue);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateControlPoints();
			if (!string.IsNullOrEmpty(this.SnapshotFile))
			{
				this.ActiveSystem.SetSnapshot(0, this.SnapshotFile);
			}
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x00054AC4 File Offset: 0x00052CC4
		private void UpdateControlPoints()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Entity ent = Entity.FindByName(this.ControlPoint0, this);
			if (ent == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ActiveSystem.SetEntity(0, ent, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ent = Entity.FindByName(this.ControlPoint1, null);
			if (ent == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ActiveSystem.SetEntity(1, ent, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ent = Entity.FindByName(this.ControlPoint2, null);
			if (ent == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ActiveSystem.SetEntity(2, ent, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ent = Entity.FindByName(this.ControlPoint3, null);
			if (ent == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ActiveSystem.SetEntity(3, ent, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ent = Entity.FindByName(this.ControlPoint4, null);
			if (ent == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ActiveSystem.SetEntity(4, ent, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ent = Entity.FindByName(this.ControlPoint5, null);
			if (ent == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ActiveSystem.SetEntity(5, ent, true);
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x00054BC7 File Offset: 0x00052DC7
		[Input]
		[Description("Tell the particle system to start emitting.")]
		protected void Start()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsActive = true;
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x00054BD5 File Offset: 0x00052DD5
		[Input]
		[Description("Tell the particle system to stop emitting.")]
		protected void Stop()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsActive = false;
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x00054BE3 File Offset: 0x00052DE3
		[Input]
		[Description("Tell the particle system to stop emitting and play its End Cap Effect.")]
		protected void StopPlayEndCap()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsActive = false;
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x00054BF1 File Offset: 0x00052DF1
		[Input]
		[Description("Destroy the particle system and remove all particles immediately.")]
		protected void DestroyImmediately()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsActive = false;
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x00054BFF File Offset: 0x00052DFF
		[Input]
		[Description("Set a Control Point via format - CP: X Y Z")]
		protected void SetControlPoint()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info(FormattableStringFactory.Create("// TODO SetControlPoint: recv argument, parse etc", Array.Empty<object>()));
		}
	}
}
