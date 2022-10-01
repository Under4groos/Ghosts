using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x02000196 RID: 406
	[Library("func_voxelsurface")]
	[HammerEntity]
	[Solid]
	[PhysicsTypeOverrideMesh]
	[Title("Voxel Surface")]
	[Category("Destruction")]
	[Icon("wine_bar")]
	[Description("A procedurally breakable voxel surface.")]
	public class VoxelSurface : ModelEntity
	{
		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x00051F89 File Offset: 0x00050189
		// (set) Token: 0x06001423 RID: 5155 RVA: 0x00051F91 File Offset: 0x00050191
		public Vector2 Size { get; private set; }

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x00051F9A File Offset: 0x0005019A
		public int NumChunks
		{
			get
			{
				return this.Chunks.Count;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x00051FA7 File Offset: 0x000501A7
		// (set) Token: 0x06001426 RID: 5158 RVA: 0x00051FAF File Offset: 0x000501AF
		[Property("Width", null)]
		[DefaultValue(32)]
		[Description("How many voxels on the width (Limited to 64)")]
		public int Width { get; private set; } = 32;

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x00051FB8 File Offset: 0x000501B8
		// (set) Token: 0x06001428 RID: 5160 RVA: 0x00051FC0 File Offset: 0x000501C0
		[Property("Height", null)]
		[DefaultValue(32)]
		[Description("How many voxels on the height (Limited to 64)")]
		public int Height { get; private set; } = 32;

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x00051FC9 File Offset: 0x000501C9
		// (set) Token: 0x0600142A RID: 5162 RVA: 0x00051FD1 File Offset: 0x000501D1
		[Property("Thickness", null)]
		[DefaultValue(1)]
		[Description("How thick is the surface (Limited to 64)")]
		public float Thickness { get; private set; } = 1f;

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x00051FDA File Offset: 0x000501DA
		// (set) Token: 0x0600142C RID: 5164 RVA: 0x00051FE2 File Offset: 0x000501E2
		[Property("Material", null)]
		[ResourceType("vmat")]
		[DefaultValue("materials/dev/black_grid_8.vmat")]
		[Description("Material to use for the surface")]
		public string Material { get; set; } = "materials/dev/black_grid_8.vmat";

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x00051FEB File Offset: 0x000501EB
		// (set) Token: 0x0600142E RID: 5166 RVA: 0x00051FF3 File Offset: 0x000501F3
		[Property("Frozen", null)]
		[DefaultValue(true)]
		[Description("Is the panel frozen")]
		public bool IsFrozen { get; private set; } = true;

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x00051FFC File Offset: 0x000501FC
		// (set) Token: 0x06001430 RID: 5168 RVA: 0x00052004 File Offset: 0x00050204
		[Property("quad_vertex_a", null)]
		[HideInEditor]
		public Vector3 QuadVertexA { get; set; }

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x0005200D File Offset: 0x0005020D
		// (set) Token: 0x06001432 RID: 5170 RVA: 0x00052015 File Offset: 0x00050215
		[Property("quad_vertex_b", null)]
		[HideInEditor]
		public Vector3 QuadVertexB { get; set; }

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x0005201E File Offset: 0x0005021E
		// (set) Token: 0x06001434 RID: 5172 RVA: 0x00052026 File Offset: 0x00050226
		[Property("quad_vertex_c", null)]
		[HideInEditor]
		public Vector3 QuadVertexC { get; set; }

		// Token: 0x06001435 RID: 5173 RVA: 0x00052030 File Offset: 0x00050230
		[ConCmd.AdminAttribute("voxel_reset")]
		public static void ResetGlassCommand()
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("voxel_reset");
				return;
			}
			foreach (VoxelSurface voxelSurface in Entity.All.OfType<VoxelSurface>().ToList<VoxelSurface>())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				voxelSurface.Reset();
			}
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x000520A0 File Offset: 0x000502A0
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			Vector3 a = base.Transform.TransformVector(this.QuadVertexA);
			Vector3 c2 = base.Transform.TransformVector(this.QuadVertexB);
			Vector3 c = base.Transform.TransformVector(this.QuadVertexC);
			Vector3 left = c2 - a;
			Vector3 up = c2 - c;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Size = new Vector2(left.Length, up.Length);
			Vector3 forward = left.Cross(up);
			Rotation rotation = Rotation.LookAt(left.Normal, forward.Normal);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LocalTransform = base.Transform.ToLocal(new Transform(base.CollisionWorldSpaceCenter, rotation, 1f));
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetModel("");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Reset();
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0005218C File Offset: 0x0005038C
		public void Reset()
		{
			if (!base.IsAuthority)
			{
				return;
			}
			foreach (VoxelChunk voxelChunk in this.Chunks)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				voxelChunk.MarkForDeletion();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Chunks.Clear();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Width = this.Width.Clamp(1, 64);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Height = this.Height.Clamp(1, 64);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Thickness = this.Thickness.Clamp(0.1f, 64f);
			if (this.Size.x > 0f && this.Size.y > 0f)
			{
				VoxelChunk voxelChunk2 = this.CreateNewChunk();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				voxelChunk2.CreateModel(this.Size, Vector2.Zero, this.Width, this.Height, this.Thickness, this.Width * this.Height, this.Material, this.IsFrozen, null);
			}
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x000522C0 File Offset: 0x000504C0
		public VoxelChunk CreateNewChunk()
		{
			VoxelChunk newChunk = new VoxelChunk
			{
				ParentSurface = this,
				Transform = this.GetPanelTransform()
			};
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Chunks.Insert(0, newChunk);
			return newChunk;
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x000522F9 File Offset: 0x000504F9
		public void RemoveChunk(VoxelChunk chunk)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Chunks.Remove(chunk);
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x00052310 File Offset: 0x00050510
		public Transform GetPanelTransform()
		{
			return base.Transform.ToWorld(this.LocalTransform);
		}

		// Token: 0x0400067E RID: 1662
		private Transform LocalTransform;

		// Token: 0x0400067F RID: 1663
		private readonly List<VoxelChunk> Chunks = new List<VoxelChunk>();
	}
}
