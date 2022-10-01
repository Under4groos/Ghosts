using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox.Internal;

namespace Sandbox
{
	// Token: 0x02000195 RID: 405
	public class VoxelChunk : ModelEntity
	{
		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x0005019A File Offset: 0x0004E39A
		// (set) Token: 0x060013F6 RID: 5110 RVA: 0x000501A2 File Offset: 0x0004E3A2
		public VoxelSurface ParentSurface { get; set; }

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x000501AB File Offset: 0x0004E3AB
		// (set) Token: 0x060013F8 RID: 5112 RVA: 0x000501B8 File Offset: 0x0004E3B8
		[Net]
		private VoxelChunk.ModelDesc Desc
		{
			get
			{
				return this._repback__Desc.GetValue();
			}
			set
			{
				this._repback__Desc.SetValue(value);
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x000501C6 File Offset: 0x0004E3C6
		private static float DamageSoundCooldown
		{
			get
			{
				return 0.4f;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x000501CD File Offset: 0x0004E3CD
		private static string DamageSound
		{
			get
			{
				return "break_wood_plank";
			}
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x000501D4 File Offset: 0x0004E3D4
		public override void ClientSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ClientSpawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TryCreateModel();
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x000501EC File Offset: 0x0004E3EC
		[Event.Tick.ClientAttribute]
		protected void OnClientTick()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TryCreateModel();
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x000501FC File Offset: 0x0004E3FC
		[Event.Tick.ServerAttribute]
		protected void OnServerTick()
		{
			if (!this.IsValid())
			{
				return;
			}
			if (this.IsMarkedForDeletion)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				VoxelSurface parentSurface = this.ParentSurface;
				if (parentSurface != null)
				{
					parentSurface.RemoveChunk(this);
				}
				if (base.PhysicsBody.IsValid())
				{
					base.PhysicsBody.Sleeping = false;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Delete();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.IsMarkedForDeletion = false;
				return;
			}
			while (this.DamageQueued.Count > 0)
			{
				DamageInfo info = this.DamageQueued.Dequeue();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DamagePosition = info.Position;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DamageForce = info.Force;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DamageRadius = 5;
				if (info.Flags.HasFlag(DamageFlags.PhysicsImpact))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.DamageRadius = 6;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.BreakWorldSpace(info.Position);
			}
			if (this.HasBroken)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.HasBroken = false;
				if (this.TimeSinceDamageSound > VoxelChunk.DamageSoundCooldown)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					Sound.FromWorld(VoxelChunk.DamageSound, this.DamagePosition);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.TimeSinceDamageSound = 0f;
				}
			}
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x0005033E File Offset: 0x0004E53E
		public void MarkForDeletion()
		{
			if (this.IsMarkedForDeletion)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PhysicsEnabled = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableAllCollisions = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableDrawing = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsMarkedForDeletion = true;
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x0005037C File Offset: 0x0004E57C
		private void TryCreateModel()
		{
			if (this.IsModelCreated)
			{
				return;
			}
			Model model = this.CreateModel();
			if (model != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Model = model;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsModelCreated = true;
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x000503B4 File Offset: 0x0004E5B4
		private Model CreateModel()
		{
			if (this.Desc == null)
			{
				return null;
			}
			ModelBuilder modelBuilder = new ModelBuilder();
			if (base.IsClient)
			{
				Vector3 boundsMin = new Vector3(this.Desc.Size * -0.5f, this.Desc.Thickness * -0.5f);
				Vector3 boundsMax = new Vector3(this.Desc.Size * 0.5f, this.Desc.Thickness * 0.5f);
				BBox bounds = new BBox(boundsMin, boundsMax);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Mesh = new Mesh(Material.Load(this.Desc.Material), MeshPrimitiveType.Triangles)
				{
					Bounds = bounds
				};
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Build(modelBuilder, this.Mesh);
			return modelBuilder.Create();
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x00050480 File Offset: 0x0004E680
		public void CreateModel(Vector2 size, Vector2 offset, int width, int height, float thickness, int blockCount, string material, bool frozen, byte[] data = null)
		{
			if (!base.IsAuthority)
			{
				return;
			}
			if (this.IsMarkedForDeletion)
			{
				return;
			}
			if (blockCount == 0)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.MarkForDeletion();
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.BlockCount = blockCount;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Desc = new VoxelChunk.ModelDesc
			{
				Size = size,
				Offset = offset,
				Width = width,
				Height = height,
				Thickness = thickness,
				IsBroken = true,
				Material = material,
				Mask = (data ?? new byte[width * height])
			};
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Desc.WriteNetworkData();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Model = this.CreateModel();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetupPhysicsFromModel(frozen ? PhysicsMotionType.Static : PhysicsMotionType.Dynamic, false);
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0005054B File Offset: 0x0004E74B
		private float CalculateMass()
		{
			return 10f.LerpTo(100f, (float)this.BlockCount / 1000f, true);
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x0005056A File Offset: 0x0004E76A
		protected override void OnPhysicsCollision(CollisionEventData eventData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnPhysicsCollision(eventData);
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x00050578 File Offset: 0x0004E778
		public override void TakeDamage(DamageInfo info)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.TakeDamage(info);
			if (!base.IsAuthority)
			{
				return;
			}
			if (this.IsMarkedForDeletion)
			{
				return;
			}
			if (!info.Flags.HasFlag(DamageFlags.Bullet) && !info.Flags.HasFlag(DamageFlags.PhysicsImpact))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DamageQueued.Enqueue(info);
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x000505EB File Offset: 0x0004E7EB
		private void Unfreeze()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PhysicsEnabled = true;
			if (base.PhysicsBody.IsValid())
			{
				base.PhysicsBody.Sleeping = false;
			}
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x00050614 File Offset: 0x0004E814
		public void BreakWorldSpace(Vector3 position)
		{
			if (!base.IsAuthority)
			{
				return;
			}
			if (this.IsMarkedForDeletion)
			{
				return;
			}
			Vector2 localPosition = base.Transform.PointToLocal(position);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			localPosition += this.Desc.Size * 0.5f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			localPosition /= this.Desc.Size;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			localPosition *= new Vector2((float)this.Desc.Width, (float)this.Desc.Height);
			int x = (int)MathF.Floor(localPosition.x);
			int y = (int)MathF.Floor(localPosition.y);
			if (this.IsBlockOutOfBounds(x, y))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.BreakLocalSpace(x, y, this.DamageRadius);
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x000506E4 File Offset: 0x0004E8E4
		public void BreakLocalSpace(int x, int y)
		{
			if (!base.IsAuthority)
			{
				return;
			}
			if (this.IsMarkedForDeletion)
			{
				return;
			}
			if (this.IsBlockOutOfBounds(x, y))
			{
				return;
			}
			if (this.IsBlockEmpty(x, y))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.BreakLocalSpace(new List<VoxelChunk.BlockPosition>
			{
				new VoxelChunk.BlockPosition(x, y)
			});
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x00050738 File Offset: 0x0004E938
		public void BreakLocalSpace(int localX, int localY, int radius)
		{
			if (!base.IsAuthority)
			{
				return;
			}
			if (this.IsMarkedForDeletion)
			{
				return;
			}
			if (radius == 0)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.BreakLocalSpace(localX, localY);
				return;
			}
			if (this.IsBlockOutOfBounds(localX, localY))
			{
				return;
			}
			if (this.IsBlockEmpty(localX, localY))
			{
				return;
			}
			List<VoxelChunk.BlockPosition> positions = new List<VoxelChunk.BlockPosition>();
			Vector2 position = new Vector2((float)localX, (float)localY);
			int xMinRadius = 0;
			int xMaxRadius = 0;
			int yMinRadius = 0;
			int yMaxRadius = 0;
			for (int i = 0; i < radius; i++)
			{
				int x4 = localX + 1 + i;
				if (this.IsBlockInBounds(x4, localY) && this.IsBlockEmpty(x4, localY))
				{
					break;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				xMaxRadius++;
			}
			for (int j = 0; j < radius; j++)
			{
				int x2 = localX - 1 - j;
				if (this.IsBlockInBounds(x2, localY) && this.IsBlockEmpty(x2, localY))
				{
					break;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				xMinRadius++;
			}
			for (int k = 0; k < radius; k++)
			{
				int y = localY + 1 + k;
				if (this.IsBlockInBounds(localX, y) && this.IsBlockEmpty(localX, y))
				{
					break;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				yMaxRadius++;
			}
			for (int l = 0; l < radius; l++)
			{
				int y2 = localY - 1 - l;
				if (this.IsBlockInBounds(localX, y2) && this.IsBlockEmpty(localX, y2))
				{
					break;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				yMinRadius++;
			}
			int xMin = localX - xMinRadius;
			int yMin = localY - yMinRadius;
			int xMax = localX + xMaxRadius;
			int yMax = localY + yMaxRadius;
			for (int x3 = xMin; x3 <= xMax; x3++)
			{
				for (int y3 = yMin; y3 <= yMax; y3++)
				{
					if (!this.IsBlockOutOfBounds(x3, y3) && !this.IsBlockEmpty(x3, y3))
					{
						VoxelChunk.BlockPosition blockPosition = new VoxelChunk.BlockPosition(x3, y3);
						if (position.Distance(new Vector2((float)x3, (float)y3)) < (float)radius)
						{
							RuntimeHelpers.EnsureSufficientExecutionStack();
							positions.Add(blockPosition);
						}
					}
				}
			}
			if (positions.Count == 0)
			{
				return;
			}
			if (Math.Max(xMinRadius, xMaxRadius) > 2 && Math.Max(yMinRadius, yMaxRadius) > 2)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				positions.RemoveAll(delegate(VoxelChunk.BlockPosition x)
				{
					bool isOnEdge = false;
					foreach (VoxelChunk.BlockPosition neighbour in VoxelChunk.BlockPosition.Neighbours)
					{
						VoxelChunk.BlockPosition position2 = x + neighbour;
						if (this.IsBlockOutOfBounds(position2.x, position2.y))
						{
							return false;
						}
						if (!this.IsBlockEmpty(position2.x, position2.y) && !positions.Contains(position2))
						{
							RuntimeHelpers.EnsureSufficientExecutionStack();
							isOnEdge = true;
							break;
						}
					}
					return isOnEdge && Rand.Int(5) == 0;
				});
				if (positions.Count == 0)
				{
					return;
				}
			}
			List<VoxelChunk.BlockPosition> holePositions = new List<VoxelChunk.BlockPosition>(positions);
			VoxelChunk.BlockPosition[] directions = VoxelChunk.BlockPosition.Directions;
			for (int m = 0; m < directions.Length; m++)
			{
				VoxelChunk.BlockPosition direction = directions[m];
				RuntimeHelpers.EnsureSufficientExecutionStack();
				holePositions.RemoveAll(delegate(VoxelChunk.BlockPosition x)
				{
					VoxelChunk.BlockPosition position2 = x + direction;
					return !positions.Contains(position2) && this.IsBlockInBounds(position2.x, position2.y) && this.IsBlockSolid(position2.x, position2.y);
				});
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.BreakLocalSpace(positions);
			if (this.IsMarkedForDeletion)
			{
				return;
			}
			if (holePositions.Count == 0 || this.BlockCount == 0)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateSplitChunk(new VoxelChunk.SplitData
			{
				X = xMin,
				Y = yMin,
				Width = xMax - xMin + 1,
				Height = yMax - yMin + 1,
				Positions = holePositions.ToArray()
			});
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x00050A40 File Offset: 0x0004EC40
		private void BreakLocalSpace(List<VoxelChunk.BlockPosition> positions)
		{
			if (!base.IsAuthority)
			{
				return;
			}
			if (this.IsMarkedForDeletion)
			{
				return;
			}
			int blockCount = this.BlockCount;
			foreach (VoxelChunk.BlockPosition position in positions)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SetBlockEmpty(position.x, position.y);
			}
			if (this.BlockCount == 0)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.HasBroken = true;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.MarkForDeletion();
				return;
			}
			if (this.BlockCount == blockCount)
			{
				return;
			}
			HashSet<VoxelChunk.BlockPosition> checkPositions = new HashSet<VoxelChunk.BlockPosition>();
			foreach (VoxelChunk.BlockPosition position2 in positions)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TryAddSplitCheck(position2 + VoxelChunk.BlockPosition.Forward, checkPositions);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TryAddSplitCheck(position2 + VoxelChunk.BlockPosition.Backward, checkPositions);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TryAddSplitCheck(position2 + VoxelChunk.BlockPosition.Left, checkPositions);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TryAddSplitCheck(position2 + VoxelChunk.BlockPosition.Right, checkPositions);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TrySplit(checkPositions);
			if (this.ParentSurface.IsFrozen && !this.IsTouchingBounds())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Unfreeze();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Desc.WriteNetworkData();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreateSplitChunks();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Build(null, null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnBreakLocalSpace(this.BlockCount);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ApplyDamageForce();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HasBroken = true;
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x00050BFC File Offset: 0x0004EDFC
		private void ApplyDamageForce()
		{
			if (base.PhysicsBody.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PhysicsBody.ApplyImpulseAt(this.DamagePosition, this.DamageForce * 10f);
			}
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x00050C34 File Offset: 0x0004EE34
		private bool IsTouchingBounds()
		{
			for (int i = 0; i < this.Desc.Width; i++)
			{
				if (this.IsBlockSolid(i, 0))
				{
					return true;
				}
				if (this.IsBlockSolid(i, this.Desc.Height - 1))
				{
					return true;
				}
			}
			for (int j = 0; j < this.Desc.Height; j++)
			{
				if (this.IsBlockSolid(0, j))
				{
					return true;
				}
				if (this.IsBlockSolid(this.Desc.Width - 1, j))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x00050CB8 File Offset: 0x0004EEB8
		[ClientRpc]
		private void OnBreakLocalSpace(int blockCount)
		{
			if (!this.OnBreakLocalSpace__RpcProxy(blockCount, null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertClient("OnBreakLocalSpace");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.BlockCount = blockCount;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Build(null, this.Mesh);
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x00050D08 File Offset: 0x0004EF08
		protected bool OnBreakLocalSpace__RpcProxy(int blockCount, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("OnBreakLocalSpace", new object[]
				{
					blockCount
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(351351918, this))
			{
				if (!NetRead.IsSupported(blockCount))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] OnBreakLocalSpace is not allowed to use Int32 for the parameter 'blockCount'!");
					return false;
				}
				writer.Write<int>(blockCount);
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x00050D9C File Offset: 0x0004EF9C
		public bool IsBlockOutOfBounds(int x, int y)
		{
			return x < 0 || x >= this.Desc.Width || (y < 0 || y >= this.Desc.Height);
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x00050DC7 File Offset: 0x0004EFC7
		public bool IsBlockInBounds(int x, int y)
		{
			return x >= 0 && x < this.Desc.Width && y >= 0 && y < this.Desc.Height;
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x00050DF2 File Offset: 0x0004EFF2
		private int GetBlockIndex(int x, int y)
		{
			return x + y * this.Desc.Width;
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x00050E03 File Offset: 0x0004F003
		public bool IsBlockSolid(int x, int y)
		{
			return this.Desc.Mask[this.GetBlockIndex(x, y)] == 0;
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x00050E1C File Offset: 0x0004F01C
		public bool IsBlockEmpty(int x, int y)
		{
			return this.Desc.Mask[this.GetBlockIndex(x, y)] > 0;
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x00050E35 File Offset: 0x0004F035
		public void SetBlockEmpty(int x, int y)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetBlockMask(this.GetBlockIndex(x, y), 1);
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x00050E4B File Offset: 0x0004F04B
		public void SetBlockSolid(int x, int y)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetBlockMask(this.GetBlockIndex(x, y), 0);
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x00050E61 File Offset: 0x0004F061
		private void SetBlockMask(int index, byte mask)
		{
			if (this.Desc.Mask[index] == mask)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Desc.Mask[index] = mask;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.BlockCount += ((mask == 0) ? 1 : -1);
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x00050EA0 File Offset: 0x0004F0A0
		private Vector2 Planar(Vector3 pos, Vector3 right, Vector3 up, float scale = 64f)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pos.x += this.Desc.Offset.x;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pos.y += this.Desc.Offset.y;
			return new Vector2(Vector3.Dot(right, pos), Vector3.Dot(up, pos)) * (1f / scale);
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x00050F14 File Offset: 0x0004F114
		private void Build(ModelBuilder builder, Mesh mesh)
		{
			if (this.Desc == null)
			{
				return;
			}
			int numTilesX = this.Desc.Width;
			int numTilesY = this.Desc.Height;
			float tileWidth = this.Desc.Size.x / (float)numTilesX;
			float tileHeight = this.Desc.Size.y / (float)numTilesY;
			float halfTileWidth = tileWidth * 0.5f;
			float halfTileHeight = tileHeight * 0.5f;
			float halfThickness = this.Desc.Thickness * 0.5f;
			List<SimpleVertex> vertices = new List<SimpleVertex>();
			List<int> indices = new List<int>();
			int maskIndex = 0;
			int vertexCount = 0;
			bool[] mask = new bool[numTilesX * numTilesY];
			for (int i = 0; i < numTilesX * numTilesY; i++)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				mask[i] = (this.Desc.Mask[i] > 0);
			}
			if (base.PhysicsBody.IsValid())
			{
				foreach (PhysicsShape physicsShape in base.PhysicsBody.Shapes.ToList<PhysicsShape>())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					physicsShape.Remove();
				}
			}
			for (int y = 0; y < numTilesY; y++)
			{
				int x = 0;
				while (x < numTilesX)
				{
					if (mask[maskIndex])
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						x++;
						RuntimeHelpers.EnsureSufficientExecutionStack();
						maskIndex++;
					}
					else
					{
						int faceWidth = 1;
						while (x + faceWidth < numTilesX && !mask[maskIndex + faceWidth] && mask[maskIndex + faceWidth] == mask[maskIndex])
						{
							faceWidth++;
						}
						bool faceHeightCalculated = false;
						int faceHeight = 1;
						while (y + faceHeight < numTilesY)
						{
							uint j = 0U;
							while ((ulong)j < (ulong)((long)faceWidth))
							{
								bool faceCulled = mask[(int)(checked((IntPtr)(unchecked((long)maskIndex + (long)((ulong)j) + (long)(faceHeight * numTilesX)))))];
								if (faceCulled || faceCulled != mask[maskIndex])
								{
									RuntimeHelpers.EnsureSufficientExecutionStack();
									faceHeightCalculated = true;
									break;
								}
								j += 1U;
							}
							if (faceHeightCalculated)
							{
								break;
							}
							faceHeight++;
						}
						Vector3 extents = new Vector3((float)faceWidth * halfTileWidth, (float)faceHeight * halfTileHeight, halfThickness);
						Vector3 origin = new Vector3((float)x - (float)faceWidth * -0.5f, (float)y - (float)faceHeight * -0.5f, 0f) * new Vector3(tileWidth, tileHeight, 0f);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						origin -= new Vector3(this.Desc.Size) * 0.5f;
						if (builder != null)
						{
							RuntimeHelpers.EnsureSufficientExecutionStack();
							builder.AddCollisionBox(extents, new Vector3?(origin), null);
						}
						else if (base.PhysicsBody.IsValid())
						{
							RuntimeHelpers.EnsureSufficientExecutionStack();
							base.PhysicsBody.AddBoxShape(origin, Rotation.Identity, extents, false);
						}
						if (mesh != null)
						{
							Vector3 offset = new Vector3(this.Desc.Size * 0.5f, 0f);
							Vector3 scale = new Vector3(tileWidth, tileHeight, 0f);
							Vector3 v0 = new Vector3((float)x, (float)y, 0f) * scale - offset;
							Vector3 v = new Vector3((float)(x + faceWidth), (float)y, 0f) * scale - offset;
							Vector3 v2 = new Vector3((float)(x + faceWidth), (float)(y + faceHeight), 0f) * scale - offset;
							Vector3 v3 = new Vector3((float)x, (float)(y + faceHeight), 0f) * scale - offset;
							RuntimeHelpers.EnsureSufficientExecutionStack();
							v0 = v0.WithZ(-halfThickness);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							v = v.WithZ(-halfThickness);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							v2 = v2.WithZ(-halfThickness);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							v3 = v3.WithZ(-halfThickness);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							vertices.Add(new SimpleVertex(v0, Vector3.Down, Vector3.Forward, this.Planar(v0, Vector3.Forward, Vector3.Left, 64f)));
							RuntimeHelpers.EnsureSufficientExecutionStack();
							vertices.Add(new SimpleVertex(v, Vector3.Down, Vector3.Forward, this.Planar(v, Vector3.Forward, Vector3.Left, 64f)));
							RuntimeHelpers.EnsureSufficientExecutionStack();
							vertices.Add(new SimpleVertex(v2, Vector3.Down, Vector3.Forward, this.Planar(v2, Vector3.Forward, Vector3.Left, 64f)));
							RuntimeHelpers.EnsureSufficientExecutionStack();
							vertices.Add(new SimpleVertex(v3, Vector3.Down, Vector3.Forward, this.Planar(v3, Vector3.Forward, Vector3.Left, 64f)));
							RuntimeHelpers.EnsureSufficientExecutionStack();
							indices.Add(vertexCount);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							indices.Add(vertexCount + 3);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							indices.Add(vertexCount + 2);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							indices.Add(vertexCount + 2);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							indices.Add(vertexCount + 1);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							indices.Add(vertexCount);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							vertexCount = vertices.Count;
							RuntimeHelpers.EnsureSufficientExecutionStack();
							v0 = v0.WithZ(halfThickness);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							v = v.WithZ(halfThickness);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							v2 = v2.WithZ(halfThickness);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							v3 = v3.WithZ(halfThickness);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							vertices.Add(new SimpleVertex(v0, Vector3.Up, Vector3.Backward, this.Planar(v0, Vector3.Backward, Vector3.Left, 64f)));
							RuntimeHelpers.EnsureSufficientExecutionStack();
							vertices.Add(new SimpleVertex(v, Vector3.Up, Vector3.Backward, this.Planar(v, Vector3.Backward, Vector3.Left, 64f)));
							RuntimeHelpers.EnsureSufficientExecutionStack();
							vertices.Add(new SimpleVertex(v2, Vector3.Up, Vector3.Backward, this.Planar(v2, Vector3.Backward, Vector3.Left, 64f)));
							RuntimeHelpers.EnsureSufficientExecutionStack();
							vertices.Add(new SimpleVertex(v3, Vector3.Up, Vector3.Backward, this.Planar(v3, Vector3.Backward, Vector3.Left, 64f)));
							RuntimeHelpers.EnsureSufficientExecutionStack();
							indices.Add(vertexCount);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							indices.Add(vertexCount + 1);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							indices.Add(vertexCount + 2);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							indices.Add(vertexCount + 2);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							indices.Add(vertexCount + 3);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							indices.Add(vertexCount);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							vertexCount = vertices.Count;
							int[] array = new int[4];
							array[0] = -1;
							array[1] = faceHeight;
							int[] yOffsets = array;
							int[] xOffsets = new int[]
							{
								0,
								0,
								-1,
								faceWidth
							};
							int[] sideWidths = new int[]
							{
								(y == 0) ? faceWidth : 0,
								(y + faceHeight == numTilesY) ? faceWidth : 0,
								(x == 0) ? faceHeight : 0,
								(x + faceWidth == numTilesX) ? faceHeight : 0
							};
							int[] maxWidths = new int[]
							{
								faceWidth,
								faceWidth,
								faceHeight,
								faceHeight
							};
							for (int sideIndex = 0; sideIndex < 4; sideIndex++)
							{
								int sideWidth = sideWidths[sideIndex];
								int maxWidth = maxWidths[sideIndex];
								int sideMaskIndex = 0;
								while (sideMaskIndex < maxWidth)
								{
									for (int k = sideMaskIndex + sideWidth; k < maxWidth; k++)
									{
										int xPos = ((xOffsets[sideIndex] == 0) ? k : 0) + x + xOffsets[sideIndex];
										int yPos = ((yOffsets[sideIndex] == 0) ? k : 0) + y + yOffsets[sideIndex];
										if (this.Desc.Mask[xPos + yPos * numTilesX] == 0)
										{
											break;
										}
										RuntimeHelpers.EnsureSufficientExecutionStack();
										sideWidth++;
									}
									if (sideWidth == 0)
									{
										RuntimeHelpers.EnsureSufficientExecutionStack();
										sideMaskIndex++;
									}
									else
									{
										Vector3 v4 = Vector3.Zero;
										Vector3 v5 = Vector3.Zero;
										if (sideIndex == 0)
										{
											RuntimeHelpers.EnsureSufficientExecutionStack();
											v4 = new Vector3((float)(x + sideMaskIndex), (float)y, 0f) * scale - offset;
											RuntimeHelpers.EnsureSufficientExecutionStack();
											v5 = new Vector3((float)(x + sideMaskIndex + sideWidth), (float)y, 0f) * scale - offset;
										}
										else if (sideIndex == 1)
										{
											RuntimeHelpers.EnsureSufficientExecutionStack();
											v5 = new Vector3((float)(x + sideMaskIndex), (float)(y + faceHeight), 0f) * scale - offset;
											RuntimeHelpers.EnsureSufficientExecutionStack();
											v4 = new Vector3((float)(x + sideMaskIndex + sideWidth), (float)(y + faceHeight), 0f) * scale - offset;
										}
										else if (sideIndex == 2)
										{
											RuntimeHelpers.EnsureSufficientExecutionStack();
											v5 = new Vector3((float)x, (float)(y + sideMaskIndex), 0f) * scale - offset;
											RuntimeHelpers.EnsureSufficientExecutionStack();
											v4 = new Vector3((float)x, (float)(y + sideMaskIndex + sideWidth), 0f) * scale - offset;
										}
										else if (sideIndex == 3)
										{
											RuntimeHelpers.EnsureSufficientExecutionStack();
											v4 = new Vector3((float)(x + faceWidth), (float)(y + sideMaskIndex), 0f) * scale - offset;
											RuntimeHelpers.EnsureSufficientExecutionStack();
											v5 = new Vector3((float)(x + faceWidth), (float)(y + sideMaskIndex + sideWidth), 0f) * scale - offset;
										}
										RuntimeHelpers.EnsureSufficientExecutionStack();
										v4 = v4.WithZ(-halfThickness);
										RuntimeHelpers.EnsureSufficientExecutionStack();
										v5 = v5.WithZ(-halfThickness);
										Vector3 v6 = v4.WithZ(halfThickness);
										Vector3 v7 = v5.WithZ(halfThickness);
										Vector3 normal = VoxelChunk.SideNormals[sideIndex];
										Vector3 tangent = (v5 - v4).Normal;
										Vector3 binormal = normal.Cross(tangent);
										RuntimeHelpers.EnsureSufficientExecutionStack();
										vertices.Add(new SimpleVertex(v6, normal, tangent, this.Planar(v6.WithZ(halfThickness * 2f), tangent, binormal, 64f)));
										RuntimeHelpers.EnsureSufficientExecutionStack();
										vertices.Add(new SimpleVertex(v7, normal, tangent, this.Planar(v7.WithZ(halfThickness * 2f), tangent, binormal, 64f)));
										RuntimeHelpers.EnsureSufficientExecutionStack();
										vertices.Add(new SimpleVertex(v5, normal, tangent, this.Planar(v5.WithZ(0f), tangent, binormal, 64f)));
										RuntimeHelpers.EnsureSufficientExecutionStack();
										vertices.Add(new SimpleVertex(v4, normal, tangent, this.Planar(v4.WithZ(0f), tangent, binormal, 64f)));
										RuntimeHelpers.EnsureSufficientExecutionStack();
										indices.Add(vertexCount);
										RuntimeHelpers.EnsureSufficientExecutionStack();
										indices.Add(vertexCount + 3);
										RuntimeHelpers.EnsureSufficientExecutionStack();
										indices.Add(vertexCount + 2);
										RuntimeHelpers.EnsureSufficientExecutionStack();
										indices.Add(vertexCount + 2);
										RuntimeHelpers.EnsureSufficientExecutionStack();
										indices.Add(vertexCount + 1);
										RuntimeHelpers.EnsureSufficientExecutionStack();
										indices.Add(vertexCount);
										RuntimeHelpers.EnsureSufficientExecutionStack();
										vertexCount = vertices.Count;
										RuntimeHelpers.EnsureSufficientExecutionStack();
										sideMaskIndex += sideWidth;
										RuntimeHelpers.EnsureSufficientExecutionStack();
										sideWidth = 0;
									}
								}
							}
						}
						uint h = 0U;
						while ((ulong)h < (ulong)((long)faceHeight))
						{
							uint w = 0U;
							while ((ulong)w < (ulong)((long)faceWidth))
							{
								RuntimeHelpers.EnsureSufficientExecutionStack();
								mask[(int)(checked((IntPtr)(unchecked((long)maskIndex + (long)((ulong)w) + (long)((ulong)h * (ulong)((long)numTilesX))))))] = true;
								w += 1U;
							}
							h += 1U;
						}
						RuntimeHelpers.EnsureSufficientExecutionStack();
						x += faceWidth;
						RuntimeHelpers.EnsureSufficientExecutionStack();
						maskIndex += faceWidth;
					}
				}
			}
			if (mesh != null)
			{
				if (mesh.HasVertexBuffer)
				{
					if (vertices.Count > 0)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						mesh.SetVertexBufferSize(vertices.Count);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						mesh.SetVertexBufferData<SimpleVertex>(vertices, 0);
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					mesh.SetVertexRange(0, vertices.Count);
				}
				else
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					mesh.CreateVertexBuffer<SimpleVertex>(Math.Max(1, vertices.Count), SimpleVertex.Layout, vertices);
				}
				if (mesh.HasIndexBuffer)
				{
					if (indices.Count > 0)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						mesh.SetIndexBufferSize(indices.Count);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						mesh.SetIndexBufferData(indices, 0);
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					mesh.SetIndexRange(0, indices.Count);
				}
				else
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					mesh.CreateIndexBuffer(Math.Max(1, indices.Count), indices);
				}
			}
			float mass = this.CalculateMass();
			if (base.PhysicsBody.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PhysicsBody.Mass = mass;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PhysicsBody.SetSurface("wood");
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PhysicsBody.Sleeping = false;
			}
			if (builder != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				builder.WithMass(mass);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				builder.WithSurface("wood");
				if (this.Mesh != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					builder.AddMesh(this.Mesh);
				}
			}
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x00051B90 File Offset: 0x0004FD90
		public void QueueSplit(VoxelChunk.BlockPosition[] positions, int x, int y, int width, int height)
		{
			if (!base.IsAuthority)
			{
				return;
			}
			if (positions == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SplitsQueued.Enqueue(new VoxelChunk.SplitData
			{
				Positions = positions,
				X = x,
				Y = y,
				Width = width,
				Height = height
			});
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x00051BED File Offset: 0x0004FDED
		private void TryAddSplitCheck(VoxelChunk.BlockPosition position, HashSet<VoxelChunk.BlockPosition> positions)
		{
			if (this.IsBlockOutOfBounds(position.x, position.y))
			{
				return;
			}
			if (this.IsBlockEmpty(position.x, position.y))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			positions.Add(position);
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x00051C26 File Offset: 0x0004FE26
		private void TrySplit(HashSet<VoxelChunk.BlockPosition> positions)
		{
			VoxelChunk.SplitSearch splitSearch = new VoxelChunk.SplitSearch(positions);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			splitSearch.Search(this);
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x00051C3C File Offset: 0x0004FE3C
		private VoxelChunk CreateSplitChunk(VoxelChunk.SplitData splitData)
		{
			if (splitData.Positions.Length == 0)
			{
				return null;
			}
			float tileWidth = this.Desc.Size.x / (float)this.Desc.Width;
			float tileHeight = this.Desc.Size.y / (float)this.Desc.Height;
			Vector2 size = new Vector2(tileWidth * (float)splitData.Width, tileHeight * (float)splitData.Height);
			Vector2 localPosition = new Vector2(this.Desc.Size.x * -0.5f, this.Desc.Size.y * -0.5f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			localPosition += new Vector2((float)splitData.X * tileWidth, (float)splitData.Y * tileHeight);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			localPosition += new Vector2(size.x * 0.5f, size.y * 0.5f);
			VoxelChunk newChunk = this.ParentSurface.CreateNewChunk();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			newChunk.Transform = base.Transform.WithPosition(base.Transform.PointToWorld(localPosition));
			byte[] newData = new byte[splitData.Width * splitData.Height];
			for (int i = 0; i < newData.Length; i++)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				newData[i] = 1;
			}
			int newBlockCount = 0;
			for (int j = 0; j < splitData.Positions.Length; j++)
			{
				VoxelChunk.BlockPosition blockPosition = splitData.Positions[j];
				int blockIndex = blockPosition.x - splitData.X + (blockPosition.y - splitData.Y) * splitData.Width;
				if (newData[blockIndex] != 0)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					newData[blockIndex] = 0;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					newBlockCount++;
				}
			}
			if (newBlockCount == 0)
			{
				return null;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			newChunk.CreateModel(size, this.Desc.Offset + localPosition, splitData.Width, splitData.Height, this.ParentSurface.Thickness, newBlockCount, this.ParentSurface.Material, false, newData);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			newChunk.DamagePosition = this.DamagePosition;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			newChunk.DamageForce = this.DamageForce;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			newChunk.ApplyDamageForce();
			return newChunk;
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x00051E82 File Offset: 0x00050082
		private void CreateSplitChunks()
		{
			while (this.SplitsQueued.Count > 0)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CreateSplitChunk(this.SplitsQueued.Dequeue());
			}
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x00051EAB File Offset: 0x000500AB
		private void OnBreakLocalSpace(To toTarget, int blockCount)
		{
			this.OnBreakLocalSpace__RpcProxy(blockCount, new To?(toTarget));
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x00051EBC File Offset: 0x000500BC
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id == 351351918)
			{
				int __blockCount = 0;
				__blockCount = read.ReadData<int>(__blockCount);
				if (!Prediction.WasPredicted("OnBreakLocalSpace", new object[]
				{
					__blockCount
				}))
				{
					this.OnBreakLocalSpace(__blockCount);
				}
				return;
			}
			base.OnCallRemoteProcedure(id, read);
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x00051F07 File Offset: 0x00050107
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarClass<VoxelChunk.ModelDesc>>(ref this._repback__Desc, "Desc", false, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x04000670 RID: 1648
		private Mesh Mesh;

		// Token: 0x04000671 RID: 1649
		private bool IsModelCreated;

		// Token: 0x04000672 RID: 1650
		private bool IsMarkedForDeletion;

		// Token: 0x04000673 RID: 1651
		private bool HasBroken;

		// Token: 0x04000674 RID: 1652
		private Vector3 DamagePosition;

		// Token: 0x04000675 RID: 1653
		private Vector3 DamageForce;

		// Token: 0x04000676 RID: 1654
		private int DamageRadius;

		// Token: 0x04000677 RID: 1655
		private TimeSince TimeSinceDamageSound;

		// Token: 0x04000678 RID: 1656
		private readonly Queue<DamageInfo> DamageQueued = new Queue<DamageInfo>();

		// Token: 0x04000679 RID: 1657
		private int BlockCount;

		// Token: 0x0400067A RID: 1658
		private static readonly Vector3[] SideNormals = new Vector3[]
		{
			Vector3.Right,
			Vector3.Left,
			Vector3.Backward,
			Vector3.Forward
		};

		// Token: 0x0400067B RID: 1659
		private readonly Queue<VoxelChunk.SplitData> SplitsQueued = new Queue<VoxelChunk.SplitData>();

		// Token: 0x0400067C RID: 1660
		private VarClass<VoxelChunk.ModelDesc> _repback__Desc = new VarClass<VoxelChunk.ModelDesc>();

		// Token: 0x02000257 RID: 599
		public struct BlockPosition
		{
			// Token: 0x0600199B RID: 6555 RVA: 0x0006AA3A File Offset: 0x00068C3A
			public BlockPosition(int x, int y)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.x = x;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.y = y;
			}

			// Token: 0x0600199C RID: 6556 RVA: 0x0006AA54 File Offset: 0x00068C54
			public static VoxelChunk.BlockPosition operator +(VoxelChunk.BlockPosition a, VoxelChunk.BlockPosition b)
			{
				return new VoxelChunk.BlockPosition(a.x + b.x, a.y + b.y);
			}

			// Token: 0x170006FA RID: 1786
			// (get) Token: 0x0600199D RID: 6557 RVA: 0x0006AA75 File Offset: 0x00068C75
			public static VoxelChunk.BlockPosition Forward
			{
				get
				{
					return new VoxelChunk.BlockPosition(1, 0);
				}
			}

			// Token: 0x170006FB RID: 1787
			// (get) Token: 0x0600199E RID: 6558 RVA: 0x0006AA7E File Offset: 0x00068C7E
			public static VoxelChunk.BlockPosition Backward
			{
				get
				{
					return new VoxelChunk.BlockPosition(-1, 0);
				}
			}

			// Token: 0x170006FC RID: 1788
			// (get) Token: 0x0600199F RID: 6559 RVA: 0x0006AA87 File Offset: 0x00068C87
			public static VoxelChunk.BlockPosition Left
			{
				get
				{
					return new VoxelChunk.BlockPosition(0, 1);
				}
			}

			// Token: 0x170006FD RID: 1789
			// (get) Token: 0x060019A0 RID: 6560 RVA: 0x0006AA90 File Offset: 0x00068C90
			public static VoxelChunk.BlockPosition Right
			{
				get
				{
					return new VoxelChunk.BlockPosition(0, -1);
				}
			}

			// Token: 0x040009EC RID: 2540
			public int x;

			// Token: 0x040009ED RID: 2541
			public int y;

			// Token: 0x040009EE RID: 2542
			public static readonly VoxelChunk.BlockPosition[] Directions = new VoxelChunk.BlockPosition[]
			{
				new VoxelChunk.BlockPosition(1, 0),
				new VoxelChunk.BlockPosition(-1, 0),
				new VoxelChunk.BlockPosition(0, 1),
				new VoxelChunk.BlockPosition(0, -1),
				new VoxelChunk.BlockPosition(1, 1),
				new VoxelChunk.BlockPosition(-1, 1),
				new VoxelChunk.BlockPosition(1, -1),
				new VoxelChunk.BlockPosition(-1, -1)
			};

			// Token: 0x040009EF RID: 2543
			public static readonly VoxelChunk.BlockPosition[] Neighbours = new VoxelChunk.BlockPosition[]
			{
				new VoxelChunk.BlockPosition(1, 0),
				new VoxelChunk.BlockPosition(-1, 0),
				new VoxelChunk.BlockPosition(0, 1),
				new VoxelChunk.BlockPosition(0, -1)
			};
		}

		// Token: 0x02000258 RID: 600
		private class ModelDesc : BaseNetworkable, INetworkSerializer
		{
			// Token: 0x060019A2 RID: 6562 RVA: 0x0006AB68 File Offset: 0x00068D68
			public void Read(ref NetRead read)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Size = read.Read<Vector2>();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Offset = read.Read<Vector2>();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Width = read.Read<int>();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Height = read.Read<int>();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Thickness = read.Read<float>();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.IsBroken = read.Read<bool>();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Material = read.ReadString();
				if (this.IsBroken)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Mask = read.ReadUnmanagedArray<byte>(this.Mask);
				}
			}

			// Token: 0x060019A3 RID: 6563 RVA: 0x0006AC0C File Offset: 0x00068E0C
			public void Write(NetWrite write)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				write.Write<Vector2>(this.Size);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				write.Write<Vector2>(this.Offset);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				write.Write<int>(this.Width);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				write.Write<int>(this.Height);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				write.Write<float>(this.Thickness);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				write.Write<bool>(this.IsBroken);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				write.WriteUtf8(this.Material);
				if (this.IsBroken)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					write.WriteUnmanagedArray<byte>(this.Mask);
				}
			}

			// Token: 0x040009F0 RID: 2544
			public Vector2 Size;

			// Token: 0x040009F1 RID: 2545
			public Vector2 Offset;

			// Token: 0x040009F2 RID: 2546
			public int Width;

			// Token: 0x040009F3 RID: 2547
			public int Height;

			// Token: 0x040009F4 RID: 2548
			public float Thickness;

			// Token: 0x040009F5 RID: 2549
			public bool IsBroken;

			// Token: 0x040009F6 RID: 2550
			public byte[] Mask;

			// Token: 0x040009F7 RID: 2551
			public string Material;
		}

		// Token: 0x02000259 RID: 601
		private struct SplitData
		{
			// Token: 0x040009F8 RID: 2552
			public VoxelChunk.BlockPosition[] Positions;

			// Token: 0x040009F9 RID: 2553
			public int X;

			// Token: 0x040009FA RID: 2554
			public int Y;

			// Token: 0x040009FB RID: 2555
			public int Width;

			// Token: 0x040009FC RID: 2556
			public int Height;
		}

		// Token: 0x0200025A RID: 602
		private class SplitSearch
		{
			// Token: 0x060019A5 RID: 6565 RVA: 0x0006ACB4 File Offset: 0x00068EB4
			public SplitSearch(IEnumerable<VoxelChunk.BlockPosition> positions)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Searches.AddRange(from x in positions
				select new VoxelChunk.SplitSearch.SearchData(x));
			}

			// Token: 0x060019A6 RID: 6566 RVA: 0x0006AD14 File Offset: 0x00068F14
			public void Search(VoxelChunk chunk)
			{
				if (chunk == null)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Chunk = chunk;
				while (this.Searches.Count > 1)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Search(this.Searches.First<VoxelChunk.SplitSearch.SearchData>());
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Searches.RemoveAll(new Predicate<VoxelChunk.SplitSearch.SearchData>(this.SearchesRemoved.Contains));
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.SearchesRemoved.Clear();
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Searches.Sort(delegate(VoxelChunk.SplitSearch.SearchData x, VoxelChunk.SplitSearch.SearchData y)
					{
						if (x.PositionCount >= y.PositionCount)
						{
							return 1;
						}
						return -1;
					});
				}
			}

			// Token: 0x060019A7 RID: 6567 RVA: 0x0006ADBC File Offset: 0x00068FBC
			private void Search(VoxelChunk.SplitSearch.SearchData search)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CurrentSearch = search;
				if (search.PositionsToCheck.Count == 0)
				{
					if (this.Chunk.IsAuthority)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.QueueSplit(search.Positions);
					}
					else
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.ClearPositions(search.Positions);
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.SearchesRemoved.Add(search);
					return;
				}
				foreach (VoxelChunk.BlockPosition position in search.PositionsToCheck)
				{
					if (this.Chunk.IsBlockSolid(position.x, position.y))
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						search.Positions.Add(position);
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.TryMerge(position + VoxelChunk.BlockPosition.Forward);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.TryMerge(position + VoxelChunk.BlockPosition.Backward);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.TryMerge(position + VoxelChunk.BlockPosition.Left);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.TryMerge(position + VoxelChunk.BlockPosition.Right);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				search.NewPositions();
			}

			// Token: 0x060019A8 RID: 6568 RVA: 0x0006AEF8 File Offset: 0x000690F8
			private void TryMerge(VoxelChunk.BlockPosition position)
			{
				if (!this.Chunk.IsBlockInBounds(position.x, position.y) || this.Chunk.IsBlockEmpty(position.x, position.y))
				{
					return;
				}
				VoxelChunk.SplitSearch.SearchData search = this.Searches.Except(this.SearchesRemoved.Append(this.CurrentSearch)).FirstOrDefault((VoxelChunk.SplitSearch.SearchData x) => x.ContainsPosition(position));
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CurrentSearch.Merge(position, search);
				if (search != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.SearchesRemoved.Add(search);
				}
			}

			// Token: 0x060019A9 RID: 6569 RVA: 0x0006AFB4 File Offset: 0x000691B4
			private void QueueSplit(List<VoxelChunk.BlockPosition> positions)
			{
				if (positions.Count == 0)
				{
					return;
				}
				VoxelChunk.BlockPosition min = new VoxelChunk.BlockPosition(int.MaxValue, int.MaxValue);
				VoxelChunk.BlockPosition max = new VoxelChunk.BlockPosition(int.MinValue, int.MinValue);
				bool solid = false;
				foreach (VoxelChunk.BlockPosition position in positions)
				{
					if (this.Chunk.IsBlockSolid(position.x, position.y))
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.Chunk.SetBlockEmpty(position.x, position.y);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						min.x = ((position.x < min.x) ? position.x : min.x);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						min.y = ((position.y < min.y) ? position.y : min.y);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						max.x = ((position.x > max.x) ? position.x : max.x);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						max.y = ((position.y > max.y) ? position.y : max.y);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						solid = true;
					}
				}
				if (solid)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Chunk.QueueSplit(positions.ToArray(), min.x, min.y, max.x - min.x + 1, max.y - min.y + 1);
				}
			}

			// Token: 0x060019AA RID: 6570 RVA: 0x0006B16C File Offset: 0x0006936C
			private void ClearPositions(List<VoxelChunk.BlockPosition> positions)
			{
				if (positions.Count == 0)
				{
					return;
				}
				foreach (VoxelChunk.BlockPosition position in positions)
				{
					if (this.Chunk.IsBlockSolid(position.x, position.y))
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.Chunk.SetBlockEmpty(position.x, position.y);
					}
				}
			}

			// Token: 0x040009FD RID: 2557
			private VoxelChunk Chunk;

			// Token: 0x040009FE RID: 2558
			private VoxelChunk.SplitSearch.SearchData CurrentSearch;

			// Token: 0x040009FF RID: 2559
			private readonly List<VoxelChunk.SplitSearch.SearchData> Searches = new List<VoxelChunk.SplitSearch.SearchData>();

			// Token: 0x04000A00 RID: 2560
			private readonly List<VoxelChunk.SplitSearch.SearchData> SearchesRemoved = new List<VoxelChunk.SplitSearch.SearchData>();

			// Token: 0x02000276 RID: 630
			private class SearchData
			{
				// Token: 0x17000702 RID: 1794
				// (get) Token: 0x060019DD RID: 6621 RVA: 0x0006BFD2 File Offset: 0x0006A1D2
				public int PositionCount
				{
					get
					{
						return this.Positions.Count;
					}
				}

				// Token: 0x060019DE RID: 6622 RVA: 0x0006BFE0 File Offset: 0x0006A1E0
				public SearchData(VoxelChunk.BlockPosition position)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.PositionsToCheck.Add(position);
				}

				// Token: 0x060019DF RID: 6623 RVA: 0x0006C034 File Offset: 0x0006A234
				public void Merge(VoxelChunk.BlockPosition position, VoxelChunk.SplitSearch.SearchData search)
				{
					if (search != null)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.Positions.AddRange(search.Positions);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.PositionsToAdd.UnionWith(search.PositionsToCheck);
						return;
					}
					if (this.PositionsChecked.Contains(position))
					{
						return;
					}
					if (this.PositionsToCheck.Contains(position))
					{
						return;
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.PositionsToAdd.Add(position);
				}

				// Token: 0x060019E0 RID: 6624 RVA: 0x0006C0A1 File Offset: 0x0006A2A1
				public bool ContainsPosition(VoxelChunk.BlockPosition position)
				{
					return this.PositionsToCheck.Contains(position);
				}

				// Token: 0x060019E1 RID: 6625 RVA: 0x0006C0AF File Offset: 0x0006A2AF
				public void NewPositions()
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.PositionsChecked = this.PositionsToCheck;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.PositionsToCheck = this.PositionsToAdd;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.PositionsToAdd = new HashSet<VoxelChunk.BlockPosition>();
				}

				// Token: 0x04000A38 RID: 2616
				public readonly List<VoxelChunk.BlockPosition> Positions = new List<VoxelChunk.BlockPosition>();

				// Token: 0x04000A39 RID: 2617
				public HashSet<VoxelChunk.BlockPosition> PositionsToAdd = new HashSet<VoxelChunk.BlockPosition>();

				// Token: 0x04000A3A RID: 2618
				public HashSet<VoxelChunk.BlockPosition> PositionsToCheck = new HashSet<VoxelChunk.BlockPosition>();

				// Token: 0x04000A3B RID: 2619
				public HashSet<VoxelChunk.BlockPosition> PositionsChecked = new HashSet<VoxelChunk.BlockPosition>();
			}
		}
	}
}
