using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox.Internal;

namespace Sandbox
{
	// Token: 0x02000192 RID: 402
	[Library("glass_shard")]
	[HideInEditor]
	[Title("Glass Shard")]
	[Icon("play_arrow")]
	[Category("Glass Shards")]
	[Description("A procedurally shattering glass shard.")]
	public class GlassShard : ModelEntity
	{
		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x0004CAEF File Offset: 0x0004ACEF
		// (set) Token: 0x06001387 RID: 4999 RVA: 0x0004CB08 File Offset: 0x0004AD08
		[Net]
		public unsafe ShatterGlass ParentPanel
		{
			get
			{
				return *this._repback__ParentPanel.GetValue();
			}
			set
			{
				VarUnmanaged<EntityHandle<ShatterGlass>> repback__ParentPanel = this._repback__ParentPanel;
				EntityHandle<ShatterGlass> entityHandle = value;
				repback__ParentPanel.SetValue(entityHandle);
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001388 RID: 5000 RVA: 0x0004CB29 File Offset: 0x0004AD29
		// (set) Token: 0x06001389 RID: 5001 RVA: 0x0004CB36 File Offset: 0x0004AD36
		[Net]
		private GlassShard.ModelDesc Desc
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

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x0004CB44 File Offset: 0x0004AD44
		// (set) Token: 0x0600138B RID: 5003 RVA: 0x0004CB51 File Offset: 0x0004AD51
		[Net]
		public Material Material
		{
			get
			{
				return this._repback__Material.GetValue();
			}
			set
			{
				this._repback__Material.SetValue(value);
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x0004CB5F File Offset: 0x0004AD5F
		// (set) Token: 0x0600138D RID: 5005 RVA: 0x0004CB67 File Offset: 0x0004AD67
		public Vector2 StressPosition { get; set; }

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x0004CB70 File Offset: 0x0004AD70
		// (set) Token: 0x0600138F RID: 5007 RVA: 0x0004CB78 File Offset: 0x0004AD78
		public GlassShard ParentShard { get; set; }

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x0004CB81 File Offset: 0x0004AD81
		// (set) Token: 0x06001391 RID: 5009 RVA: 0x0004CB89 File Offset: 0x0004AD89
		public Vector3 StressVelocity { get; set; }

		// Token: 0x06001392 RID: 5010 RVA: 0x0004CB92 File Offset: 0x0004AD92
		public override void ClientSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ClientSpawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TryGenerateShardModel();
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0004CBAA File Offset: 0x0004ADAA
		[Event.Tick.ClientAttribute]
		protected void ClientTick()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TryGenerateShardModel();
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0004CBB7 File Offset: 0x0004ADB7
		[Event.Tick.ServerAttribute]
		protected void OnServerTick()
		{
			if (this.IsMarkedForDeletion && this.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Delete();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.IsMarkedForDeletion = false;
			}
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x0004CBE0 File Offset: 0x0004ADE0
		public void MarkForDeletion()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PhysicsEnabled = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableAllCollisions = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableDrawing = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsMarkedForDeletion = true;
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x0004CC14 File Offset: 0x0004AE14
		protected void TryGenerateShardModel()
		{
			if (this.IsModelGenerated)
			{
				return;
			}
			if (!this.ParentPanel.IsValid() || this.Material == null)
			{
				return;
			}
			Model model = this.GenerateShardModel(out this.LocalPanelSpaceOrigin);
			if (model != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Model = model;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsModelGenerated = true;
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x0004CC68 File Offset: 0x0004AE68
		protected override void OnDestroy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnDestroy();
			if (this.ParentPanel.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ParentPanel.RemoveShard(this);
			}
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0004CC94 File Offset: 0x0004AE94
		protected override void OnPhysicsCollision(CollisionEventData eventData)
		{
			CollisionEntityData other = eventData.Other;
			if (other.Entity.IsWorld)
			{
				return;
			}
			if (!other.Entity.PhysicsGroup.IsValid())
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			other.Entity.PhysicsGroup.Velocity = other.PreVelocity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			other.Entity.PhysicsGroup.AngularVelocity = other.PreAngularVelocity;
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0004CD00 File Offset: 0x0004AF00
		public override void TakeDamage(DamageInfo info)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.TakeDamage(info);
			Vector3 position = info.Position;
			Plane plane = new Plane(this.Position, this.Rotation.Up);
			Vector3? hit = plane.Trace(new Ray(info.Position, info.Force.Normal), true, double.MaxValue);
			if (hit != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				position = hit.Value;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ShatterTypeIndex = 0;
			if (info.Flags.HasFlag(DamageFlags.Bullet))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ShatterTypeIndex = 1;
			}
			else if (info.Flags.HasFlag(DamageFlags.PhysicsImpact))
			{
				if (!this.ParentPanel.IsValid() || this.ParentPanel.IsBroken)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				info.Force /= 10f;
			}
			else if (info.Flags.HasFlag(DamageFlags.Blast))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ShatterTypeIndex = 2;
			}
			Vector2 localPosition = this.TransformPosWorldToPanel(position);
			if (this.ParentPanel.IsValid())
			{
				using (List<GlassShard>.Enumerator enumerator = this.ParentPanel.Shards.ToList<GlassShard>().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GlassShard shard = enumerator.Current;
						if (shard.IsValid())
						{
							RuntimeHelpers.EnsureSufficientExecutionStack();
							shard.StressVelocity = info.Force;
							if (shard.ShatterLocalSpace(localPosition))
							{
								break;
							}
						}
					}
					return;
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StressVelocity = info.Force;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ShatterLocalSpace(localPosition);
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0004CED8 File Offset: 0x0004B0D8
		public void AddVertex(Vector2 vertex)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PanelVertices.Add(vertex);
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0004CEEC File Offset: 0x0004B0EC
		private void ScaleVerts(float scale)
		{
			if (scale <= 0f)
			{
				return;
			}
			Vector2 average = this.GetPanelVertexAverage();
			for (int i = 0; i < this.PanelVertices.Count; i++)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.PanelVertices[i] = Vector2.Lerp(average, this.PanelVertices[i], scale, true);
			}
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0004CF44 File Offset: 0x0004B144
		public Vector2 GetPanelVertexAverage()
		{
			Vector2 vecAverageVertPosition = Vector2.Zero;
			if (this.PanelVertices.Count > 0)
			{
				foreach (Vector2 vertex in this.PanelVertices)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					vecAverageVertPosition += vertex;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vecAverageVertPosition /= (float)this.PanelVertices.Count;
			}
			return vecAverageVertPosition;
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0004CFCC File Offset: 0x0004B1CC
		public void TryGenerateShardModel(Vector3 vShatterPos, Vector3 vShatterVel, bool bVelocity, bool bFreeze = false)
		{
			if (!this.CanGenerateShardModel() || !this.GenerateShardModel())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Delete();
				return;
			}
			bool bApplyVelocity = bVelocity && !bFreeze;
			if (bFreeze)
			{
				if (this.IsOnFrameEdge() && this.IsShardNearPanelSpacePosition())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Freeze();
				}
				else
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					bApplyVelocity = true;
				}
			}
			if (!this.IsFrozen() && !this.IsABigPiece())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.DeleteAsync(5f);
			}
			if (bApplyVelocity)
			{
				Vector3 overallVel = vShatterVel * base.PhysicsBody.Mass;
				float smallestAxis = Math.Min(this.ParentPanel.PanelSize.x, this.ParentPanel.PanelSize.y);
				float scale = Math.Max(0f, smallestAxis - vShatterPos.Distance(base.WorldSpaceBounds.Center)) / smallestAxis;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				overallVel *= scale;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PhysicsBody.ApplyImpulse(overallVel * 0.9f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PhysicsBody.ApplyImpulseAt(vShatterPos, overallVel * 0.1f);
			}
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0004D0FC File Offset: 0x0004B2FC
		public bool GenerateShardModel()
		{
			if (this.PanelVertices.Count < 3)
			{
				return false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Desc = new GlassShard.ModelDesc
			{
				PanelSize = this.ParentPanel.PanelSize,
				StressPosition = this.StressPosition,
				PanelVertices = this.PanelVertices.ToArray(),
				HalfThickness = this.ParentPanel.HalfThickness,
				IsParentFrozen = (this.ParentShard.IsValid() && this.ParentShard.IsFrozen()),
				HasParent = this.ParentShard.IsValid(),
				TextureOffset = new Vector2(this.ParentPanel.QuadAxisU.w, this.ParentPanel.QuadAxisV.w),
				TextureScale = this.ParentPanel.QuadTexScale,
				TextureSize = this.ParentPanel.QuadTexSize,
				TextureAxisU = this.ParentPanel.QuadAxisU,
				TextureAxisV = this.ParentPanel.QuadAxisV,
				PanelTransform = this.ParentPanel.InitialPanelTransform
			};
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Desc.WriteNetworkData();
			Model model = this.GenerateShardModel(out this.LocalPanelSpaceOrigin);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Transform = this.GetSpawnTransform();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Model = model;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetupPhysicsFromModel(PhysicsMotionType.Dynamic, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Unfreeze();
			if (!this.ParentShard.IsValid() || this.IsABigPiece())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.EnableTouch = false;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Tags.Remove("glass");
			}
			return true;
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0004D2B4 File Offset: 0x0004B4B4
		public override void Touch(Entity other)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Touch(other);
			if (!base.IsAuthority)
			{
				return;
			}
			if (other.IsWorld)
			{
				return;
			}
			if (!this.ParentPanel.IsBroken)
			{
				return;
			}
			GlassShard shard = other as GlassShard;
			if (shard != null && shard.ParentPanel == this.ParentPanel)
			{
				return;
			}
			if (!this.IsFrozen())
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Unfreeze();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StressVelocity = other.Velocity;
			if (this.CalculateArea() > 800f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ShatterTypeIndex = 0;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ShatterLocalSpace(this.GetPanelVertexAverage());
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PhysicsBody.ApplyImpulse(this.StressVelocity * base.PhysicsBody.Mass);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.DeleteAsync(5f);
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x0004D390 File Offset: 0x0004B590
		public void Freeze()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PhysicsEnabled = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PhysicsBody.BodyType = PhysicsBodyType.Keyframed;
			if (!this.ParentShard.IsValid() || this.IsABigPiece())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.EnableTouch = false;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Tags.Remove("glass");
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.EnableTouch = true;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Tags.Add("glass");
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Parent = this.ParentPanel.Parent;
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0004D42C File Offset: 0x0004B62C
		public void Unfreeze()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PhysicsEnabled = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PhysicsBody.BodyType = PhysicsBodyType.Dynamic;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PhysicsBody.Sleeping = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableTouch = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("glass");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Parent = null;
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0004D494 File Offset: 0x0004B694
		public bool IsFrozen()
		{
			return !base.PhysicsEnabled;
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0004D4A0 File Offset: 0x0004B6A0
		public bool IsOnFrameEdge()
		{
			if (this.OnFrameEdge == GlassShard.OnFrame.Unknown)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnFrameEdge = GlassShard.OnFrame.False;
				Vector2 parentPanelSize = this.ParentPanel.PanelSize * 0.5f * 0.99f;
				foreach (Vector2 vertex in this.PanelVertices)
				{
					if (MathF.Abs(vertex.x) >= parentPanelSize.x || MathF.Abs(vertex.y) >= parentPanelSize.y)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.OnFrameEdge = GlassShard.OnFrame.True;
						break;
					}
				}
			}
			return this.OnFrameEdge == GlassShard.OnFrame.True;
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0004D564 File Offset: 0x0004B764
		public bool IsShardNearPanelSpacePosition()
		{
			return this.ParentPanel.IsValid() && (this.ParentPanel.GetPanelTransform().TransformVector(this.LocalPanelSpaceOrigin) - this.Position).LengthSquared < 4f;
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0004D5B8 File Offset: 0x0004B7B8
		public Vector2 TransformPosWorldToPanel(Vector3 position)
		{
			return base.Transform.PointToLocal(position) + this.LocalPanelSpaceOrigin;
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0004D5E4 File Offset: 0x0004B7E4
		public Vector3 TransformPosPanelToWorld(Vector2 position)
		{
			return base.Transform.TransformVector(position - this.LocalPanelSpaceOrigin);
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x0004D610 File Offset: 0x0004B810
		private Transform GetSpawnTransform()
		{
			if (this.ParentShard != null)
			{
				Vector2 localPos = this.LocalPanelSpaceOrigin - this.ParentShard.LocalPanelSpaceOrigin;
				Transform parentTransform = this.ParentShard.Transform;
				return parentTransform.WithPosition(parentTransform.Position + parentTransform.Rotation * localPos);
			}
			Transform transform = this.ParentPanel.GetPanelTransform();
			return transform.WithPosition(transform.TransformVector(this.LocalPanelSpaceOrigin));
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x0004D691 File Offset: 0x0004B891
		public void ShatterWorldSpace(Vector3 position)
		{
			if (!base.IsAuthority)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ShatterLocalSpace(this.TransformPosWorldToPanel(position));
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0004D6B0 File Offset: 0x0004B8B0
		public bool ShatterLocalSpace(Vector2 position)
		{
			if (!base.IsAuthority)
			{
				return false;
			}
			if (!base.EnableDrawing)
			{
				return false;
			}
			if (!this.IsPointInsideShard(position))
			{
				return false;
			}
			if (this.IsTooSmall())
			{
				if (this.ParentPanel.IsValid())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.ParentPanel.IsBroken = true;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PhysicsEnabled = false;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.EnableAllCollisions = false;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.EnableDrawing = false;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PhysicsBody.Sleeping = false;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Delete();
				return true;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StressPosition = position;
			if (this.GenerateShatterShards())
			{
				if (this.ParentPanel.IsValid())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.ParentPanel.IsBroken = true;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.ParentPanel.OnBreak.Fire(this, 0f);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.MarkForDeletion();
			}
			return true;
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0004D7A1 File Offset: 0x0004B9A1
		private bool CanGenerateShardModel()
		{
			return !this.IsTooSmall();
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0004D7AE File Offset: 0x0004B9AE
		private bool IsTooSmall()
		{
			return this.CalculateArea() < 4f;
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0004D7BD File Offset: 0x0004B9BD
		private bool IsABigPiece()
		{
			return this.CalculateArea() > 5000f;
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0004D7CC File Offset: 0x0004B9CC
		private float CalculateArea()
		{
			float area = 0f;
			Vector2[] verticies = this.PanelVertices.ToArray();
			int vertexCount = this.PanelVertices.Count;
			if (vertexCount < 3)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vertexCount = this.Desc.PanelVertices.Length;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				verticies = this.Desc.PanelVertices;
			}
			if (vertexCount < 3)
			{
				return 0f;
			}
			Vector2 v = verticies[0];
			for (int i = 1; i < vertexCount - 1; i++)
			{
				Vector2 v2 = verticies[i];
				Vector2 v3 = verticies[i + 1];
				float x = v2.x - v.x;
				float y = v2.y - v.y;
				float x2 = v3.x - v.x;
				float y2 = v3.y - v.y;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				area += MathF.Abs(x * y2 - x2 * y);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			return MathF.Abs(area * 0.5f);
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0004D8C8 File Offset: 0x0004BAC8
		private bool IsPointInsideShard(Vector2 point)
		{
			if (this.PanelVertices.Count < 3)
			{
				return false;
			}
			if (this.PanelVertices.Count > 100)
			{
				return false;
			}
			int positive = 0;
			int negative = 0;
			for (int i = 0; i < this.PanelVertices.Count; i++)
			{
				Vector2 v = this.PanelVertices[i];
				Vector2 v2 = this.PanelVertices[(i < this.PanelVertices.Count - 1) ? (i + 1) : 0];
				float cross = (point.x - v.x) * (v2.y - v.y) - (point.y - v.y) * (v2.x - v.x);
				if (cross > 0f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					positive++;
				}
				else if (cross < 0f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					negative++;
				}
				if (positive > 0 && negative > 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0004D9B8 File Offset: 0x0004BBB8
		private static Vector4 ComputeTangentForFace(Vector3 faceS, Vector3 faceT, Vector3 normal)
		{
			bool flag = Vector3.Dot(Vector3.Cross(faceS, faceT), normal) < 0f;
			Vector4 tangent = Vector4.Zero;
			if (!flag)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				faceT = Vector3.Cross(normal, faceS);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				faceS = Vector3.Cross(faceT, normal);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				faceS = faceS.Normal;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				tangent.x = faceS[0];
				RuntimeHelpers.EnsureSufficientExecutionStack();
				tangent.y = faceS[1];
				RuntimeHelpers.EnsureSufficientExecutionStack();
				tangent.z = faceS[2];
				RuntimeHelpers.EnsureSufficientExecutionStack();
				tangent.w = 1f;
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				faceT = Vector3.Cross(faceS, normal);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				faceS = Vector3.Cross(normal, faceT);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				faceS = faceS.Normal;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				tangent.x = faceS[0];
				RuntimeHelpers.EnsureSufficientExecutionStack();
				tangent.y = faceS[1];
				RuntimeHelpers.EnsureSufficientExecutionStack();
				tangent.z = faceS[2];
				RuntimeHelpers.EnsureSufficientExecutionStack();
				tangent.w = -1f;
			}
			return tangent;
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0004DAD4 File Offset: 0x0004BCD4
		private static Vector3 ComputeTriangleNormal(Vector3 v1, Vector3 v2, Vector3 v3)
		{
			Vector3 a = v2 - v1;
			Vector3 e2 = v3 - v1;
			return Vector3.Cross(a, e2).Normal;
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0004DB00 File Offset: 0x0004BD00
		private static void ComputeTriangleTangentSpace(Vector3 p0, Vector3 p1, Vector3 p2, Vector2 t0, Vector2 t1, Vector2 t2, out Vector3 s, out Vector3 t)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			s = Vector3.Zero;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			t = Vector3.Zero;
			Vector3 a = new Vector3(p1.x - p0.x, t1.x - t0.x, t1.y - t0.y);
			Vector3 edge = new Vector3(p2.x - p0.x, t2.x - t0.x, t2.y - t0.y);
			Vector3 cross = Vector3.Cross(a, edge);
			if (MathF.Abs(cross.x) > 1E-12f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				s.x += -cross.y / cross.x;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				t.x += -cross.z / cross.x;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Vector3 a2 = new Vector3(p1.y - p0.y, t1.x - t0.x, t1.y - t0.y);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			edge = new Vector3(p2.y - p0.y, t2.x - t0.x, t2.y - t0.y);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			cross = Vector3.Cross(a2, edge);
			if (MathF.Abs(cross.x) > 1E-12f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				s.y += -cross.y / cross.x;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				t.y += -cross.z / cross.x;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Vector3 a3 = new Vector3(p1.z - p0.z, t1.x - t0.x, t1.y - t0.y);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			edge = new Vector3(p2.z - p0.z, t2.x - t0.x, t2.y - t0.y);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			cross = Vector3.Cross(a3, edge);
			if (MathF.Abs(cross.x) > 1E-12f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				s.z += -cross.y / cross.x;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				t.z += -cross.z / cross.x;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			s = s.Normal;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			t = t.Normal;
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0004DDC8 File Offset: 0x0004BFC8
		private static void ComputeTriangleNormalAndTangent(out Vector3 outNormal, out Vector4 outTangent, Vector3 v0, Vector3 v1, Vector3 v2, Vector2 uv0, Vector2 uv1, Vector2 uv2)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			outNormal = GlassShard.ComputeTriangleNormal(v0, v1, v2);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Vector3 faceS;
			Vector3 faceT;
			GlassShard.ComputeTriangleTangentSpace(v0, v1, v2, uv0, uv1, uv2, out faceS, out faceT);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			outTangent = GlassShard.ComputeTangentForFace(faceS, faceT, outNormal);
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0004DE1C File Offset: 0x0004C01C
		private static bool LineIntersect(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, out Vector2 intersection)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			intersection = Vector2.Zero;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			float xD = p2.x - p1.x;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			float xD2 = p4.x - p3.x;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			float yD = p2.y - p1.y;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			float yD2 = p4.y - p3.y;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			float xD3 = p1.x - p3.x;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			float yD3 = p1.y - p3.y;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			float length = MathF.Sqrt(xD * xD + yD * yD);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			float length2 = MathF.Sqrt(xD2 * xD2 + yD2 * yD2);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			float num = xD * xD2 + yD * yD2;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			if (Math.Abs(num / (length * length2)) == 1f)
			{
				return false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			float div = yD2 * xD - xD2 * yD;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			float ua = (xD2 * yD3 - yD2 * xD3) / div;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			intersection.x = p1.x + ua * xD;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			intersection.y = p1.y + ua * yD;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			xD = intersection.x - p1.x;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			xD2 = intersection.x - p2.x;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			yD = intersection.y - p1.y;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			yD2 = intersection.y - p2.y;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			float segmentLength = MathF.Sqrt(xD * xD + yD * yD) + MathF.Sqrt(xD2 * xD2 + yD2 * yD2);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			xD = intersection.x - p3.x;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			xD2 = intersection.x - p4.x;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			yD = intersection.y - p3.y;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			yD2 = intersection.y - p4.y;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			float segmentLength2 = MathF.Sqrt(xD * xD + yD * yD) + MathF.Sqrt(xD2 * xD2 + yD2 * yD2);
			return MathF.Abs(length - segmentLength) <= 0.01f && MathF.Abs(length2 - segmentLength2) <= 0.01f;
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0004E064 File Offset: 0x0004C264
		private bool GenerateShatterShards()
		{
			GlassShard.ShatterType shatterType = GlassShard.ShatterTypes[this.ShatterTypeIndex];
			bool shouldFreeze = this.IsFrozen() && this.ParentPanel.Constraint == ShatterGlass.ShatterGlassConstraint.StaticEdges;
			Vector3 shatterPos = this.TransformPosPanelToWorld(this.StressPosition);
			Vector3 shatterVel = this.StressVelocity;
			bool didShatter = false;
			float spokeLength = this.ParentPanel.PanelSize.x + this.ParentPanel.PanelSize.y;
			int numSpokes = Math.Max(3, Rand.Int(shatterType.SpokesMin, shatterType.SpokesMax));
			List<GlassShard.ShatterSpoke> spokes = new List<GlassShard.ShatterSpoke>();
			float segmentRange = 6.2831855f / (float)numSpokes;
			float limitedRangeDeviation = Math.Min(segmentRange, 2.0943952f);
			for (int i = 0; i < numSpokes; i++)
			{
				float spokeRadians = (float)i * segmentRange + Rand.Float(limitedRangeDeviation * -0.5f, limitedRangeDeviation * 0.5f) * 0.9f;
				GlassShard.ShatterSpoke spoke = new GlassShard.ShatterSpoke
				{
					OuterPos = new Vector2(this.StressPosition.x + spokeLength * MathF.Cos(spokeRadians), this.StressPosition.y + spokeLength * MathF.Sin(spokeRadians)),
					IntersectionPos = Vector2.Zero,
					IntersectsEdgeIndex = -1,
					Length = -1f
				};
				RuntimeHelpers.EnsureSufficientExecutionStack();
				spokes.Insert(0, spoke);
			}
			List<GlassShard.ShatterEdgeSegment> edgeSegments = new List<GlassShard.ShatterEdgeSegment>();
			for (int j = 0; j < this.PanelVertices.Count; j++)
			{
				Vector2 v = this.PanelVertices[j];
				Vector2 v2 = this.PanelVertices[(j < this.PanelVertices.Count - 1) ? (j + 1) : 0];
				RuntimeHelpers.EnsureSufficientExecutionStack();
				edgeSegments.Add(new GlassShard.ShatterEdgeSegment(v, v2));
			}
			for (int spokeIndex = 0; spokeIndex < spokes.Count; spokeIndex++)
			{
				for (int edgeIndex = 0; edgeIndex < edgeSegments.Count; edgeIndex++)
				{
					Vector2 point;
					if (GlassShard.LineIntersect(edgeSegments[edgeIndex].Start, edgeSegments[edgeIndex].End, spokes[spokeIndex].OuterPos, this.StressPosition, out point))
					{
						GlassShard.ShatterSpoke spoke2 = spokes[spokeIndex];
						RuntimeHelpers.EnsureSufficientExecutionStack();
						spoke2.IntersectionPos = point;
						RuntimeHelpers.EnsureSufficientExecutionStack();
						spoke2.IntersectsEdgeIndex = edgeIndex;
						RuntimeHelpers.EnsureSufficientExecutionStack();
						spoke2.Length = Vector2.DistanceBetween(this.StressPosition, spoke2.IntersectionPos);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						spokes[spokeIndex] = spoke2;
						break;
					}
				}
			}
			List<Vector2> centerHoleVertices = new List<Vector2>();
			for (int spokeIndex2 = 0; spokeIndex2 < spokes.Count; spokeIndex2++)
			{
				int nextSpokeIndex = (spokeIndex2 < spokes.Count - 1) ? (spokeIndex2 + 1) : 0;
				int currentEdgeIndex = spokes[spokeIndex2].IntersectsEdgeIndex;
				int nextEdgeIndex = spokes[nextSpokeIndex].IntersectsEdgeIndex;
				if (nextSpokeIndex >= 0 && currentEdgeIndex >= 0 && nextEdgeIndex >= 0 && (spokes[spokeIndex2].Length >= 0.5f || spokes[nextSpokeIndex].Length >= 0.5f))
				{
					GlassShard subShard = this.ParentPanel.CreateNewShard(this);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					subShard.AddVertex(this.StressPosition);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					subShard.AddVertex(spokes[spokeIndex2].IntersectionPos);
					if (currentEdgeIndex == nextEdgeIndex)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						subShard.AddVertex(spokes[nextSpokeIndex].IntersectionPos);
					}
					else
					{
						int k = 0;
						while (k < 32 && currentEdgeIndex != nextEdgeIndex)
						{
							RuntimeHelpers.EnsureSufficientExecutionStack();
							subShard.AddVertex(edgeSegments[currentEdgeIndex].End);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							currentEdgeIndex = ((currentEdgeIndex < edgeSegments.Count - 1) ? (currentEdgeIndex + 1) : 0);
							k++;
						}
						RuntimeHelpers.EnsureSufficientExecutionStack();
						subShard.AddVertex(spokes[nextSpokeIndex].IntersectionPos);
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					Assert.True(subShard.PanelVertices.Count >= 3);
					Vector2 tipPoint = Vector2.Lerp(subShard.PanelVertices[0], subShard.PanelVertices[1], Rand.Float(shatterType.TipScaleMin, shatterType.TipScaleMax), true);
					Vector2 a = subShard.PanelVertices[0];
					List<Vector2> panelVertices = subShard.PanelVertices;
					Vector2 tipPoint2 = Vector2.Lerp(a, panelVertices[panelVertices.Count - 1], Rand.Float(shatterType.TipScaleMin, shatterType.TipScaleMax), true);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					centerHoleVertices.Add(Vector2.Lerp(tipPoint, tipPoint2, 0.5f, true));
					if (shatterType.TipSpawnChance > 0f && Rand.Float(0f, shatterType.TipSpawnChance) < 1f)
					{
						GlassShard glassShard = this.ParentPanel.CreateNewShard(this);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						glassShard.AddVertex(subShard.PanelVertices[0]);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						glassShard.AddVertex(tipPoint);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						glassShard.AddVertex(tipPoint2);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						glassShard.ScaleVerts(shatterType.TipScale);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						glassShard.TryGenerateShardModel(shatterPos, shatterVel, true, false);
					}
					if (shatterType.SecondTipSpawnChance > 0f && Rand.Float(0f, shatterType.SecondTipSpawnChance) < 1f)
					{
						Vector2 secondTipPoint = Vector2.Lerp(tipPoint, subShard.PanelVertices[1], Rand.Float(0.2f, 0.5f), true);
						Vector2 a2 = tipPoint2;
						List<Vector2> panelVertices2 = subShard.PanelVertices;
						Vector2 secondTopPoint2 = Vector2.Lerp(a2, panelVertices2[panelVertices2.Count - 1], Rand.Float(0.2f, 0.5f), true);
						GlassShard glassShard2 = this.ParentPanel.CreateNewShard(this);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						glassShard2.AddVertex(tipPoint);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						glassShard2.AddVertex(secondTipPoint);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						glassShard2.AddVertex(secondTopPoint2);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						glassShard2.AddVertex(tipPoint2);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						glassShard2.ScaleVerts(shatterType.SecondShardScale);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						glassShard2.TryGenerateShardModel(shatterPos, shatterVel, false, false);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						tipPoint = secondTipPoint;
						RuntimeHelpers.EnsureSufficientExecutionStack();
						tipPoint2 = secondTopPoint2;
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					subShard.PanelVertices.RemoveAt(0);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					subShard.PanelVertices.Insert(0, tipPoint);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					subShard.PanelVertices.Add(tipPoint2);
					if ((tipPoint - tipPoint2).LengthSquared > 9f)
					{
						Vector2 vecBetweenCorners = Vector2.Lerp(Vector2.Lerp(tipPoint, tipPoint2, Rand.Float(0.4f, 0.6f), true), this.StressPosition, Rand.Float(0.1f, 0.3f), true);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						subShard.PanelVertices.Add(vecBetweenCorners);
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					subShard.ScaleVerts(shatterType.ShardScale);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					subShard.TryGenerateShardModel(shatterPos, shatterVel, !shouldFreeze, shouldFreeze);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					didShatter = true;
				}
			}
			if (shatterType.HasCenterChunk && centerHoleVertices.Count > 2)
			{
				GlassShard pShardCenter = this.ParentPanel.CreateNewShard(this);
				foreach (Vector2 vertex in centerHoleVertices)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					pShardCenter.AddVertex(vertex);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				pShardCenter.ScaleVerts(shatterType.CenterChunkScale);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				pShardCenter.TryGenerateShardModel(shatterPos, shatterVel, true, false);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				didShatter = true;
			}
			return didShatter;
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x0004E7D4 File Offset: 0x0004C9D4
		public Model GenerateShardModel(out Vector2 localPanelSpaceOrigin)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			localPanelSpaceOrigin = Vector2.Zero;
			if (this.Desc == null)
			{
				return null;
			}
			if (this.Desc.PanelVertices == null)
			{
				return null;
			}
			GlassShard.RenderData renderData = new GlassShard.RenderData
			{
				Average = this.Desc.GetPanelVertexAverage()
			};
			Vector2[] panelVertices = this.Desc.PanelVertices;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			localPanelSpaceOrigin = renderData.Average;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			renderData.LocalPanelSpaceOrigin = new Vector3(renderData.Average.x, renderData.Average.y, 0f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			renderData.Init(panelVertices.Length);
			float halfThickness = this.Desc.HalfThickness;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			renderData.VertexPositions.Add(new Vector3(renderData.Average.x, renderData.Average.y, halfThickness));
			for (int i = 0; i < renderData.FaceVertexCount - 1; i++)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				renderData.VertexPositions.Add(new Vector3(panelVertices[i].x, panelVertices[i].y, halfThickness));
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			renderData.VertexPositions.Add(new Vector3(renderData.Average.x, renderData.Average.y, -halfThickness));
			for (int j = 0; j < renderData.FaceVertexCount - 1; j++)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				renderData.VertexPositions.Add(new Vector3(panelVertices[j].x, panelVertices[j].y, -halfThickness));
			}
			ModelBuilder modelBuilder = new ModelBuilder();
			Vector3[] hullPos = new Vector3[renderData.VertexPositions.Count];
			for (int k = 0; k < hullPos.Length; k++)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				hullPos[k] = renderData.VertexPositions[k] - renderData.LocalPanelSpaceOrigin;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			modelBuilder.AddCollisionHull(hullPos, null, null);
			if (!base.IsServer)
			{
				for (int l = 0; l < renderData.EdgeQuadCount; l++)
				{
					int v0 = l;
					int v = (l < renderData.EdgeQuadCount - 1) ? (l + 1) : 0;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					renderData.VertexPositions.Add(new Vector3(panelVertices[v0].x, panelVertices[v0].y, -halfThickness));
					RuntimeHelpers.EnsureSufficientExecutionStack();
					renderData.VertexPositions.Add(new Vector3(panelVertices[v].x, panelVertices[v].y, -halfThickness));
					RuntimeHelpers.EnsureSufficientExecutionStack();
					renderData.VertexPositions.Add(new Vector3(panelVertices[v].x, panelVertices[v].y, halfThickness));
					RuntimeHelpers.EnsureSufficientExecutionStack();
					renderData.VertexPositions.Add(new Vector3(panelVertices[v0].x, panelVertices[v0].y, halfThickness));
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				renderData.EdgeVerticesStart = renderData.TotalShardVertices - renderData.EdgeVertexCount;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Assert.AreEqual<int>(renderData.VertexPositions.Count, renderData.TotalShardVertices, null);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				modelBuilder.AddMesh(this.CreateMeshForShard(renderData));
			}
			string surf = "glass";
			if (this.IsABigPiece())
			{
				surf = "glass.pane";
			}
			if (this.CalculateArea() < 20f)
			{
				surf = "glass.shard";
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			modelBuilder.WithSurface(surf);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			modelBuilder.WithMass(this.CalculateArea() * this.ParentPanel.Thickness * 0.04226f);
			return modelBuilder.Create();
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x0004EB98 File Offset: 0x0004CD98
		private Mesh CreateMeshForShard(GlassShard.RenderData renderData)
		{
			GlassShard.ShardVertex[] vertices = new GlassShard.ShardVertex[renderData.TotalShardVertices];
			int[] indices = new int[renderData.TotalSharedIndices];
			BBox bounds = default(BBox);
			for (int i = 0; i < renderData.TotalShardVertices; i++)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vertices[i].Position = renderData.VertexPositions[i] - renderData.LocalPanelSpaceOrigin;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				bounds = bounds.AddPoint(vertices[i].Position);
				Vector3 vertexPos = this.Desc.PanelTransform.PointToWorld(new Vector3(renderData.VertexPositions[i].x, renderData.VertexPositions[i].y, 0f));
				float u = Vector3.Dot(this.Desc.TextureAxisU, vertexPos) / this.Desc.TextureScale.x;
				float v = Vector3.Dot(this.Desc.TextureAxisV, vertexPos) / this.Desc.TextureScale.y;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				u += this.Desc.TextureOffset.x;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				v += this.Desc.TextureOffset.y;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				u /= this.Desc.TextureSize.x;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				v /= this.Desc.TextureSize.y;
				Vector2 uv = new Vector2(u, v);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vertices[i].TexCoord0 = uv;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vertices[i].TexCoord1 = renderData.VertexPositions[i];
				if (i < renderData.EdgeVerticesStart)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					vertices[i].Color = Vector3.Zero;
				}
				else
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					GlassShard.ShardVertex[] array = vertices;
					int num = i;
					array[num].TexCoord0 = array[num].TexCoord0 + GlassShard.EdgeUVs[i % 4];
					RuntimeHelpers.EnsureSufficientExecutionStack();
					GlassShard.ShardVertex[] array2 = vertices;
					int num2 = i;
					array2[num2].TexCoord1 = array2[num2].TexCoord1 + GlassShard.EdgeUVs[i % 4];
					RuntimeHelpers.EnsureSufficientExecutionStack();
					vertices[i].Color[0] = 1f;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					vertices[i].Color[1] = 0f;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					vertices[i].Color[2] = 0f;
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Vector3 normalSideA;
			Vector4 tangentSideA;
			GlassShard.ComputeTriangleNormalAndTangent(out normalSideA, out tangentSideA, vertices[1].Position, vertices[0].Position, vertices[2].Position, vertices[1].TexCoord1, vertices[0].TexCoord1, vertices[2].TexCoord1);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Vector3 normalSideB;
			Vector4 tangentSideB;
			GlassShard.ComputeTriangleNormalAndTangent(out normalSideB, out tangentSideB, vertices[renderData.FaceVertexCount].Position, vertices[renderData.FaceVertexCount + 1].Position, vertices[renderData.FaceVertexCount + 2].Position, vertices[renderData.FaceVertexCount].TexCoord1, vertices[renderData.FaceVertexCount + 1].TexCoord1, vertices[renderData.FaceVertexCount + 2].TexCoord1);
			for (int j = 0; j < renderData.FaceTriangleCount; j++)
			{
				int index = j * 3;
				int offset0 = j + 1;
				int offset = (j + 2 < renderData.FaceVertexCount) ? (j + 2) : 1;
				int offset2 = 0;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				indices[index] = offset;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				indices[index + 1] = offset0;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				indices[index + 2] = offset2;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vertices[offset0].Normal = normalSideA;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vertices[offset].Normal = normalSideA;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vertices[offset2].Normal = normalSideA;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vertices[offset0].Tangent = tangentSideA;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vertices[offset].Tangent = tangentSideA;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vertices[offset2].Tangent = tangentSideA;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				index += renderData.FaceIndexCount;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				offset0 += renderData.FaceVertexCount;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				offset += renderData.FaceVertexCount;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				offset2 = renderData.FaceVertexCount;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				indices[index] = offset0;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				indices[index + 1] = offset;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				indices[index + 2] = offset2;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vertices[offset0].Normal = normalSideB;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vertices[offset].Normal = normalSideB;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vertices[offset2].Normal = normalSideB;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vertices[offset0].Tangent = tangentSideB;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vertices[offset].Tangent = tangentSideB;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vertices[offset2].Tangent = tangentSideB;
			}
			int edgeIndexOffset = renderData.TotalSharedIndices - renderData.EdgeIndexCount;
			for (int k = 0; k < renderData.EdgeQuadCount; k++)
			{
				int index2 = edgeIndexOffset + k * 6;
				int vertexOffset = renderData.EdgeVerticesStart + k * 4;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				indices[index2] = vertexOffset + 2;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				indices[index2 + 1] = vertexOffset + 1;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				indices[index2 + 2] = vertexOffset;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				indices[index2 + 3] = vertexOffset + 3;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				indices[index2 + 4] = vertexOffset + 2;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				indices[index2 + 5] = vertexOffset;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Vector3 faceNormal;
				Vector4 faceTangent;
				GlassShard.ComputeTriangleNormalAndTangent(out faceNormal, out faceTangent, vertices[vertexOffset + 2].Position, vertices[vertexOffset + 1].Position, vertices[vertexOffset].Position, vertices[vertexOffset + 2].TexCoord1, vertices[vertexOffset + 1].TexCoord1, vertices[vertexOffset].TexCoord1);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vertices[vertexOffset].Normal = faceNormal;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vertices[vertexOffset + 1].Normal = faceNormal;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vertices[vertexOffset + 2].Normal = faceNormal;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vertices[vertexOffset + 3].Normal = faceNormal;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vertices[vertexOffset].Tangent = faceTangent;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vertices[vertexOffset + 1].Tangent = faceTangent;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vertices[vertexOffset + 2].Tangent = faceTangent;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vertices[vertexOffset + 3].Tangent = faceTangent;
			}
			Mesh mesh = new Mesh(this.Material, MeshPrimitiveType.Triangles);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			mesh.CreateVertexBuffer<GlassShard.ShardVertex>(vertices.Length, GlassShard.ShardVertex.Layout, vertices);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			mesh.CreateIndexBuffer(indices.Length, indices);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			mesh.Bounds = bounds;
			return mesh;
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x0004F2B0 File Offset: 0x0004D4B0
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarUnmanaged<EntityHandle<ShatterGlass>>>(ref this._repback__ParentPanel, "ParentPanel", false, false);
			builder.Register<VarClass<GlassShard.ModelDesc>>(ref this._repback__Desc, "Desc", false, false);
			builder.Register<VarGeneric<Material>>(ref this._repback__Material, "Material", false, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x0400064D RID: 1613
		private readonly List<Vector2> PanelVertices = new List<Vector2>();

		// Token: 0x0400064E RID: 1614
		private Vector2 LocalPanelSpaceOrigin;

		// Token: 0x04000650 RID: 1616
		private int ShatterTypeIndex = 1;

		// Token: 0x04000651 RID: 1617
		private bool IsModelGenerated;

		// Token: 0x04000652 RID: 1618
		private bool IsMarkedForDeletion;

		// Token: 0x04000653 RID: 1619
		private GlassShard.OnFrame OnFrameEdge;

		// Token: 0x04000654 RID: 1620
		private static readonly Vector2[] EdgeUVs = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(0f, 0.01f),
			new Vector2(0.01f, 0.01f),
			new Vector2(0.01f, 0f)
		};

		// Token: 0x04000655 RID: 1621
		private static readonly GlassShard.ShatterType[] ShatterTypes = new GlassShard.ShatterType[]
		{
			new GlassShard.ShatterType(5, 10, 0.2f, 0.5f, 1f, 0.95f, 1f, 8f, 0.98f, false, 0f, 4),
			new GlassShard.ShatterType(8, 14, 0.1f, 0.3f, 3f, 0.95f, 1f, 16f, 0.98f, false, 0f, 4),
			new GlassShard.ShatterType(8, 10, 0.4f, 0.6f, 0f, 0.95f, 0.95f, 1.2f, 0.98f, false, 0f, 2),
			new GlassShard.ShatterType(20, 20, 0.7f, 0.99f, 3f, 0.95f, 1f, 16f, 0.98f, false, 0.9f, 10)
		};

		// Token: 0x04000656 RID: 1622
		private VarUnmanaged<EntityHandle<ShatterGlass>> _repback__ParentPanel = new VarUnmanaged<EntityHandle<ShatterGlass>>();

		// Token: 0x04000657 RID: 1623
		private VarClass<GlassShard.ModelDesc> _repback__Desc = new VarClass<GlassShard.ModelDesc>();

		// Token: 0x04000658 RID: 1624
		private VarGeneric<Material> _repback__Material = new VarGeneric<Material>();

		// Token: 0x0200024E RID: 590
		private enum OnFrame : byte
		{
			// Token: 0x040009B2 RID: 2482
			Unknown,
			// Token: 0x040009B3 RID: 2483
			True,
			// Token: 0x040009B4 RID: 2484
			False
		}

		// Token: 0x0200024F RID: 591
		private struct ShatterSpoke
		{
			// Token: 0x040009B5 RID: 2485
			public Vector2 OuterPos;

			// Token: 0x040009B6 RID: 2486
			public Vector2 IntersectionPos;

			// Token: 0x040009B7 RID: 2487
			public int IntersectsEdgeIndex;

			// Token: 0x040009B8 RID: 2488
			public float Length;
		}

		// Token: 0x02000250 RID: 592
		private struct ShatterEdgeSegment
		{
			// Token: 0x0600198F RID: 6543 RVA: 0x0006A45C File Offset: 0x0006865C
			public ShatterEdgeSegment(Vector2 start, Vector2 end)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Start = start;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.End = end;
			}

			// Token: 0x040009B9 RID: 2489
			public Vector2 Start;

			// Token: 0x040009BA RID: 2490
			public Vector2 End;
		}

		// Token: 0x02000251 RID: 593
		private class ModelDesc : BaseNetworkable, INetworkSerializer
		{
			// Token: 0x06001990 RID: 6544 RVA: 0x0006A478 File Offset: 0x00068678
			public void Read(ref NetRead read)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.PanelSize = read.Read<Vector2>();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.StressPosition = read.Read<Vector2>();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.PanelVertices = read.ReadArray<Vector2>(this.PanelVertices);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.HalfThickness = read.Read<float>();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.HasParent = read.Read<bool>();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.IsParentFrozen = read.Read<bool>();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TextureOffset = read.Read<Vector2>();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TextureScale = read.Read<Vector2>();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TextureSize = read.Read<Vector2>();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TextureAxisU = read.Read<Vector3>();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TextureAxisV = read.Read<Vector3>();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.PanelTransform = read.Read<Transform>();
			}

			// Token: 0x06001991 RID: 6545 RVA: 0x0006A558 File Offset: 0x00068758
			public void Write(NetWrite write)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				write.Write<Vector2>(this.PanelSize);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				write.Write<Vector2>(this.StressPosition);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				write.Write<Vector2>(this.PanelVertices);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				write.Write<float>(this.HalfThickness);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				write.Write<bool>(this.HasParent);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				write.Write<bool>(this.IsParentFrozen);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				write.Write<Vector2>(this.TextureOffset);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				write.Write<Vector2>(this.TextureScale);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				write.Write<Vector2>(this.TextureSize);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				write.Write<Vector3>(this.TextureAxisU);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				write.Write<Vector3>(this.TextureAxisV);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				write.Write<Transform>(this.PanelTransform);
			}

			// Token: 0x06001992 RID: 6546 RVA: 0x0006A634 File Offset: 0x00068834
			public Vector2 GetPanelVertexAverage()
			{
				Vector2 average = Vector2.Zero;
				int vertexCount = this.PanelVertices.Length;
				if (vertexCount > 0)
				{
					foreach (Vector2 vertex in this.PanelVertices)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						average += vertex;
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					average /= (float)vertexCount;
				}
				return average;
			}

			// Token: 0x06001993 RID: 6547 RVA: 0x0006A690 File Offset: 0x00068890
			public float GetArea()
			{
				float area = 0f;
				int vertexCount = this.PanelVertices.Length;
				if (vertexCount < 3)
				{
					return 0f;
				}
				Vector2 v = this.PanelVertices[0];
				for (int i = 1; i < vertexCount - 1; i++)
				{
					Vector2 v2 = this.PanelVertices[i];
					Vector2 v3 = this.PanelVertices[i + 1];
					float x = v2.x - v.x;
					float y = v2.y - v.y;
					float x2 = v3.x - v.x;
					float y2 = v3.y - v.y;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					area += MathF.Abs(x * y2 - x2 * y);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				return MathF.Abs(area * 0.5f);
			}

			// Token: 0x06001994 RID: 6548 RVA: 0x0006A764 File Offset: 0x00068964
			public float GetLongestAcross()
			{
				float shortestAcross = float.MaxValue;
				float longestAcross = 0f;
				float sumOfAllEdges = 0f;
				for (int i = 0; i < this.PanelVertices.Length; i++)
				{
					for (int j = 0; j < this.PanelVertices.Length; j++)
					{
						if (i != j && j > i)
						{
							Vector2 a = this.PanelVertices[i];
							Vector2 v2 = this.PanelVertices[j];
							float flEdge = Vector2.DistanceBetween(a, v2);
							if (flEdge < shortestAcross)
							{
								shortestAcross = flEdge;
							}
							if (flEdge > longestAcross)
							{
								longestAcross = flEdge;
							}
							RuntimeHelpers.EnsureSufficientExecutionStack();
							sumOfAllEdges += flEdge;
						}
					}
				}
				return longestAcross;
			}

			// Token: 0x040009BB RID: 2491
			public Vector2 PanelSize;

			// Token: 0x040009BC RID: 2492
			public Vector2 StressPosition;

			// Token: 0x040009BD RID: 2493
			public Vector2[] PanelVertices;

			// Token: 0x040009BE RID: 2494
			public float HalfThickness;

			// Token: 0x040009BF RID: 2495
			public bool HasParent;

			// Token: 0x040009C0 RID: 2496
			public bool IsParentFrozen;

			// Token: 0x040009C1 RID: 2497
			public Vector2 TextureOffset;

			// Token: 0x040009C2 RID: 2498
			public Vector2 TextureScale;

			// Token: 0x040009C3 RID: 2499
			public Vector2 TextureSize;

			// Token: 0x040009C4 RID: 2500
			public Vector3 TextureAxisU;

			// Token: 0x040009C5 RID: 2501
			public Vector3 TextureAxisV;

			// Token: 0x040009C6 RID: 2502
			public Transform PanelTransform;
		}

		// Token: 0x02000252 RID: 594
		private struct RenderData
		{
			// Token: 0x06001996 RID: 6550 RVA: 0x0006A800 File Offset: 0x00068A00
			public void Init(int numPanelVerts)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.FaceVertexCount = numPanelVerts + 1;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.FaceTriangleCount = this.FaceVertexCount - 1;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.FaceIndexCount = this.FaceTriangleCount * 3;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.EdgeQuadCount = this.FaceVertexCount - 1;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.EdgeVertexCount = this.EdgeQuadCount * 4;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.EdgeTriangleCount = this.EdgeQuadCount * 2;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.EdgeIndexCount = this.EdgeTriangleCount * 3;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TotalShardVertices = this.FaceVertexCount + this.FaceVertexCount + this.EdgeVertexCount;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TotalSharedIndices = this.FaceIndexCount + this.FaceIndexCount + this.EdgeIndexCount;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.VertexPositions = new List<Vector3>(this.FaceVertexCount * 2 + this.EdgeVertexCount);
			}

			// Token: 0x040009C7 RID: 2503
			public List<Vector3> VertexPositions;

			// Token: 0x040009C8 RID: 2504
			public Vector2 Average;

			// Token: 0x040009C9 RID: 2505
			public Vector3 LocalPanelSpaceOrigin;

			// Token: 0x040009CA RID: 2506
			public int TotalShardVertices;

			// Token: 0x040009CB RID: 2507
			public int TotalSharedIndices;

			// Token: 0x040009CC RID: 2508
			public int EdgeVerticesStart;

			// Token: 0x040009CD RID: 2509
			public int FaceVertexCount;

			// Token: 0x040009CE RID: 2510
			public int FaceTriangleCount;

			// Token: 0x040009CF RID: 2511
			public int FaceIndexCount;

			// Token: 0x040009D0 RID: 2512
			public int EdgeQuadCount;

			// Token: 0x040009D1 RID: 2513
			public int EdgeVertexCount;

			// Token: 0x040009D2 RID: 2514
			public int EdgeTriangleCount;

			// Token: 0x040009D3 RID: 2515
			public int EdgeIndexCount;
		}

		// Token: 0x02000253 RID: 595
		private struct ShatterType
		{
			// Token: 0x06001997 RID: 6551 RVA: 0x0006A8EC File Offset: 0x00068AEC
			public ShatterType(int spokesMin, int spokesMax, float tipScaleMin, float tipScaleMax, float tipSpawnChance, float tipScale, float shardScale, float secondTipSpawnChance, float secondShardScale, bool hasCenterChunk, float centerChunkScale, int shardLimit)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SpokesMin = spokesMin;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SpokesMax = spokesMax;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TipScaleMin = tipScaleMin;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TipScaleMax = tipScaleMax;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TipSpawnChance = tipSpawnChance;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TipScale = tipScale;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ShardScale = shardScale;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SecondTipSpawnChance = secondTipSpawnChance;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.SecondShardScale = secondShardScale;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.HasCenterChunk = hasCenterChunk;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CenterChunkScale = centerChunkScale;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ShardLimit = shardLimit;
			}

			// Token: 0x040009D4 RID: 2516
			public int SpokesMin;

			// Token: 0x040009D5 RID: 2517
			public int SpokesMax;

			// Token: 0x040009D6 RID: 2518
			public float TipScaleMin;

			// Token: 0x040009D7 RID: 2519
			public float TipScaleMax;

			// Token: 0x040009D8 RID: 2520
			public float TipSpawnChance;

			// Token: 0x040009D9 RID: 2521
			public float TipScale;

			// Token: 0x040009DA RID: 2522
			public float ShardScale;

			// Token: 0x040009DB RID: 2523
			public float SecondTipSpawnChance;

			// Token: 0x040009DC RID: 2524
			public float SecondShardScale;

			// Token: 0x040009DD RID: 2525
			public bool HasCenterChunk;

			// Token: 0x040009DE RID: 2526
			public float CenterChunkScale;

			// Token: 0x040009DF RID: 2527
			public int ShardLimit;
		}

		// Token: 0x02000254 RID: 596
		private struct ShardVertex
		{
			// Token: 0x040009E0 RID: 2528
			public Vector3 Position;

			// Token: 0x040009E1 RID: 2529
			public Vector3 Normal;

			// Token: 0x040009E2 RID: 2530
			public Vector2 TexCoord0;

			// Token: 0x040009E3 RID: 2531
			public Vector2 TexCoord1;

			// Token: 0x040009E4 RID: 2532
			public Vector3 Color;

			// Token: 0x040009E5 RID: 2533
			public Vector4 Tangent;

			// Token: 0x040009E6 RID: 2534
			public static readonly VertexAttribute[] Layout = new VertexAttribute[]
			{
				new VertexAttribute(VertexAttributeType.Position, VertexAttributeFormat.Float32, 3, 0),
				new VertexAttribute(VertexAttributeType.Normal, VertexAttributeFormat.Float32, 3, 0),
				new VertexAttribute(VertexAttributeType.TexCoord, VertexAttributeFormat.Float32, 2, 0),
				new VertexAttribute(VertexAttributeType.TexCoord, VertexAttributeFormat.Float32, 2, 1),
				new VertexAttribute(VertexAttributeType.Color, VertexAttributeFormat.Float32, 3, 0),
				new VertexAttribute(VertexAttributeType.Tangent, VertexAttributeFormat.Float32, 4, 0)
			};
		}
	}
}
