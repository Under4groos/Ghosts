using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x02000193 RID: 403
	[Library("func_shatterglass")]
	[HammerEntity]
	[Solid]
	[PhysicsTypeOverrideMesh]
	[Title("Shatter Glass")]
	[Category("Destruction")]
	[Icon("wine_bar")]
	[Description("A procedurally shattering glass panel.")]
	public class ShatterGlass : ModelEntity
	{
		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060013BA RID: 5050 RVA: 0x0004F499 File Offset: 0x0004D699
		// (set) Token: 0x060013BB RID: 5051 RVA: 0x0004F4A1 File Offset: 0x0004D6A1
		public Vector2 PanelSize { get; private set; }

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x0004F4AA File Offset: 0x0004D6AA
		// (set) Token: 0x060013BD RID: 5053 RVA: 0x0004F4B2 File Offset: 0x0004D6B2
		public bool IsBroken { get; set; }

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x0004F4BB File Offset: 0x0004D6BB
		// (set) Token: 0x060013BF RID: 5055 RVA: 0x0004F4C3 File Offset: 0x0004D6C3
		public Transform InitialPanelTransform { get; private set; }

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x0004F4CC File Offset: 0x0004D6CC
		public int NumShards
		{
			get
			{
				return this.ShardsInternal.Count;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x0004F4D9 File Offset: 0x0004D6D9
		public IEnumerable<GlassShard> Shards
		{
			get
			{
				return this.ShardsInternal;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x0004F4E1 File Offset: 0x0004D6E1
		// (set) Token: 0x060013C3 RID: 5059 RVA: 0x0004F4E9 File Offset: 0x0004D6E9
		[Property("glass_thickness", null)]
		[Title("Glass Thickness")]
		[global::DefaultValue(1f)]
		[Description("Thickness of the glass")]
		public float Thickness { get; private set; } = 1f;

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x0004F4F2 File Offset: 0x0004D6F2
		public float HalfThickness
		{
			get
			{
				return this.Thickness * 0.5f;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x0004F500 File Offset: 0x0004D700
		// (set) Token: 0x060013C6 RID: 5062 RVA: 0x0004F508 File Offset: 0x0004D708
		[Property("glass_material", null)]
		[Title("Glass Material")]
		[ResourceType("vmat")]
		[global::DefaultValue("materials/glass/glass_a.vmat")]
		[Description("Material to use for the glass")]
		public string Material { get; set; } = "materials/glass/glass_a.vmat";

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x0004F511 File Offset: 0x0004D711
		// (set) Token: 0x060013C8 RID: 5064 RVA: 0x0004F519 File Offset: 0x0004D719
		[Property]
		[Title("Glass Material When Broken")]
		[ResourceType("vmat")]
		[Description("Material to use for the glass when it is broken. If not set, the material will not change on break.")]
		public string BrokenMaterial { get; set; }

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x0004F522 File Offset: 0x0004D722
		// (set) Token: 0x060013CA RID: 5066 RVA: 0x0004F52A File Offset: 0x0004D72A
		[Property("DamagePositioningEntity", null)]
		[Title("Damage Position 01")]
		[FGDType("target_destination", "", "")]
		public string DamagePositioningEntity { get; set; }

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x0004F533 File Offset: 0x0004D733
		// (set) Token: 0x060013CC RID: 5068 RVA: 0x0004F53B File Offset: 0x0004D73B
		[Property("DamagePositioningEntity02", null)]
		[Title("Damage Position 02")]
		[FGDType("target_destination", "", "")]
		public string DamagePositioningEntity02 { get; set; }

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x0004F544 File Offset: 0x0004D744
		// (set) Token: 0x060013CE RID: 5070 RVA: 0x0004F54C File Offset: 0x0004D74C
		[Property("DamagePositioningEntity03", null)]
		[Title("Damage Position 03")]
		[FGDType("target_destination", "", "")]
		public string DamagePositioningEntity03 { get; set; }

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x0004F555 File Offset: 0x0004D755
		// (set) Token: 0x060013D0 RID: 5072 RVA: 0x0004F55D File Offset: 0x0004D75D
		[Property("DamagePositioningEntity04", null)]
		[Title("Damage Position 04")]
		[FGDType("target_destination", "", "")]
		public string DamagePositioningEntity04 { get; set; }

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x0004F566 File Offset: 0x0004D766
		// (set) Token: 0x060013D2 RID: 5074 RVA: 0x0004F56E File Offset: 0x0004D76E
		[Property("quad_vertex_a", null)]
		[HideInEditor]
		public Vector3 QuadVertexA { get; set; }

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x0004F577 File Offset: 0x0004D777
		// (set) Token: 0x060013D4 RID: 5076 RVA: 0x0004F57F File Offset: 0x0004D77F
		[Property("quad_vertex_b", null)]
		[HideInEditor]
		public Vector3 QuadVertexB { get; set; }

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x0004F588 File Offset: 0x0004D788
		// (set) Token: 0x060013D6 RID: 5078 RVA: 0x0004F590 File Offset: 0x0004D790
		[Property("quad_vertex_c", null)]
		[HideInEditor]
		public Vector3 QuadVertexC { get; set; }

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x0004F599 File Offset: 0x0004D799
		// (set) Token: 0x060013D8 RID: 5080 RVA: 0x0004F5A1 File Offset: 0x0004D7A1
		[Property("quad_axis_u", null)]
		[HideInEditor]
		public Vector4 QuadAxisU { get; set; }

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x0004F5AA File Offset: 0x0004D7AA
		// (set) Token: 0x060013DA RID: 5082 RVA: 0x0004F5B2 File Offset: 0x0004D7B2
		[Property("quad_axis_v", null)]
		[HideInEditor]
		public Vector4 QuadAxisV { get; set; }

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x0004F5BB File Offset: 0x0004D7BB
		// (set) Token: 0x060013DC RID: 5084 RVA: 0x0004F5C3 File Offset: 0x0004D7C3
		[Property("quad_tex_scale", null)]
		[HideInEditor]
		public Vector2 QuadTexScale { get; set; }

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x0004F5CC File Offset: 0x0004D7CC
		// (set) Token: 0x060013DE RID: 5086 RVA: 0x0004F5D4 File Offset: 0x0004D7D4
		[Property("quad_tex_size", null)]
		[HideInEditor]
		public Vector2 QuadTexSize { get; set; }

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x0004F5DD File Offset: 0x0004D7DD
		// (set) Token: 0x060013E0 RID: 5088 RVA: 0x0004F5E5 File Offset: 0x0004D7E5
		[Property]
		[global::DefaultValue(ShatterGlass.ShatterGlassConstraint.StaticEdges)]
		[Description("Glass constraint.<br /><b>Glass with static edges</b> will not be affected by gravity (glass pieces will) and will shatter piece by piece.<br /><b>Physics glass</b> is affected by gravity and will shatter all at the same time.<br /><b>Physics but asleep</b> is same as physics but will not move on spawn.")]
		public ShatterGlass.ShatterGlassConstraint Constraint { get; private set; }

		// Token: 0x060013E1 RID: 5089 RVA: 0x0004F5F0 File Offset: 0x0004D7F0
		[ConCmd.AdminAttribute("glass_reset")]
		public static void ResetGlassCommand()
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("glass_reset");
				return;
			}
			foreach (ShatterGlass ent in Entity.All.OfType<ShatterGlass>().ToList<ShatterGlass>())
			{
				if (ent.IsBroken)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					ent.Reset();
				}
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x0004F66C File Offset: 0x0004D86C
		// (set) Token: 0x060013E3 RID: 5091 RVA: 0x0004F674 File Offset: 0x0004D874
		[Description("Fired when the panel initially breaks.")]
		public Entity.Output OnBreak { get; set; }

		// Token: 0x060013E4 RID: 5092 RVA: 0x0004F680 File Offset: 0x0004D880
		[Event.Entity.PostSpawnAttribute]
		[Event.Entity.PostCleanupAttribute]
		public void PostSpawn()
		{
			Entity damageEntity = Entity.FindByName(this.DamagePositioningEntity, null);
			if (damageEntity.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.InitialDamagePositions.Add(damageEntity.Position);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			damageEntity = Entity.FindByName(this.DamagePositioningEntity02, null);
			if (damageEntity.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.InitialDamagePositions.Add(damageEntity.Position);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			damageEntity = Entity.FindByName(this.DamagePositioningEntity03, null);
			if (damageEntity.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.InitialDamagePositions.Add(damageEntity.Position);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			damageEntity = Entity.FindByName(this.DamagePositioningEntity04, null);
			if (damageEntity.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.InitialDamagePositions.Add(damageEntity.Position);
			}
			using (List<Vector3>.Enumerator enumerator = this.InitialDamagePositions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Vector3 damagePosition = enumerator.Current;
					GlassShard shard = (from x in this.ShardsInternal
					orderby (x.Position - damagePosition).Length
					select x).FirstOrDefault<GlassShard>();
					if (shard.IsValid())
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						shard.ShatterWorldSpace(damagePosition);
					}
				}
			}
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0004F7C8 File Offset: 0x0004D9C8
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Thickness = this.Thickness.Clamp(0.2f, 20f);
			Vector3 a = base.Transform.TransformVector(this.QuadVertexA);
			Vector3 c2 = base.Transform.TransformVector(this.QuadVertexB);
			Vector3 c = base.Transform.TransformVector(this.QuadVertexC);
			Vector3 left = c2 - a;
			Vector3 up = c2 - c;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PanelSize = new Vector2(left.Length, up.Length);
			Vector3 forward = left.Cross(up);
			Rotation rotation = Rotation.LookAt(left.Normal, forward.Normal);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PanelTransform = base.Transform.ToLocal(new Transform(base.CollisionWorldSpaceCenter, rotation, 1f));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.InitialPanelTransform = this.GetPanelTransform();
			if (this.QuadTexScale.IsNearZeroLength)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.QuadTexScale = 0.25;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.QuadTexSize = 512.0;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Vector3 normal = left.Normal;
				this.QuadAxisU = new Vector4(ref normal, 0f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				normal = up.Normal;
				this.QuadAxisV = new Vector4(ref normal, 0f);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetModel("");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Reset();
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x0004F968 File Offset: 0x0004DB68
		[Input]
		[Description("Cleans up broken shards and creates a new primary shard")]
		public void Reset()
		{
			if (!base.IsAuthority)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsBroken = false;
			foreach (GlassShard glassShard in this.ShardsInternal)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				glassShard.MarkForDeletion();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ShardsInternal.Clear();
			if (this.PanelSize.x > 0f && this.PanelSize.y > 0f)
			{
				GlassShard primaryShard = this.CreateNewShard(null);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				primaryShard.AddVertex(new Vector2(-this.PanelSize.x * 0.5f, -this.PanelSize.y * 0.5f));
				RuntimeHelpers.EnsureSufficientExecutionStack();
				primaryShard.AddVertex(new Vector2(-this.PanelSize.x * 0.5f, this.PanelSize.y * 0.5f));
				RuntimeHelpers.EnsureSufficientExecutionStack();
				primaryShard.AddVertex(new Vector2(this.PanelSize.x * 0.5f, this.PanelSize.y * 0.5f));
				RuntimeHelpers.EnsureSufficientExecutionStack();
				primaryShard.AddVertex(new Vector2(this.PanelSize.x * 0.5f, -this.PanelSize.y * 0.5f));
				RuntimeHelpers.EnsureSufficientExecutionStack();
				primaryShard.GenerateShardModel();
				if (this.Constraint == ShatterGlass.ShatterGlassConstraint.StaticEdges)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					primaryShard.Freeze();
				}
				else if (this.Constraint == ShatterGlass.ShatterGlassConstraint.PhysicsButAsleep)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					primaryShard.PhysicsBody.Sleeping = true;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				primaryShard.Parent = this.Parent;
			}
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0004FB4C File Offset: 0x0004DD4C
		public GlassShard CreateNewShard(GlassShard parentShard)
		{
			string material = this.Material;
			if (this.IsBroken && !string.IsNullOrEmpty(this.BrokenMaterial))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				material = this.BrokenMaterial;
			}
			GlassShard newShard = new GlassShard
			{
				ParentPanel = this,
				ParentShard = parentShard,
				Material = Sandbox.Material.Load(material)
			};
			if (parentShard != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				newShard.StressPosition = parentShard.StressPosition;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ShardsInternal.Insert(0, newShard);
			return newShard;
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0004FBC8 File Offset: 0x0004DDC8
		public void RemoveShard(GlassShard shard)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ShardsInternal.Remove(shard);
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0004FBDC File Offset: 0x0004DDDC
		public Transform GetPanelTransform()
		{
			return base.Transform.ToWorld(this.PanelTransform);
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0004FBFD File Offset: 0x0004DDFD
		[Input]
		[Description("Breaks the glass at its center.")]
		public void Break()
		{
			if (!base.IsAuthority)
			{
				return;
			}
			if (this.IsBroken)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlassShard glassShard = this.ShardsInternal.FirstOrDefault<GlassShard>();
			if (glassShard == null)
			{
				return;
			}
			glassShard.ShatterLocalSpace(Vector2.Zero);
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0004FC31 File Offset: 0x0004DE31
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void CreateHammerOutputs()
		{
			this.OnBreak = new Entity.Output(this, "OnBreak");
			base.CreateHammerOutputs();
		}

		// Token: 0x0400065B RID: 1627
		private Transform PanelTransform;

		// Token: 0x0400065D RID: 1629
		private readonly List<GlassShard> ShardsInternal = new List<GlassShard>();

		// Token: 0x0400066E RID: 1646
		private readonly List<Vector3> InitialDamagePositions = new List<Vector3>();

		// Token: 0x02000255 RID: 597
		public enum ShatterGlassConstraint
		{
			// Token: 0x040009E8 RID: 2536
			StaticEdges,
			// Token: 0x040009E9 RID: 2537
			Physics,
			// Token: 0x040009EA RID: 2538
			PhysicsButAsleep
		}
	}
}
