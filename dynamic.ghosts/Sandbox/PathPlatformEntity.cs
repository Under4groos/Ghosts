using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Sandbox.Internal;
using Sandbox.Internal.Globals;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x02000182 RID: 386
	[Library("ent_path_platform")]
	[HammerEntity]
	[SupportsSolid]
	[Model]
	[RenderFields]
	[DrawAngles("movedir", null)]
	[VisGroup(VisGroup.Dynamic)]
	[Title("Path Platform")]
	[Category("Gameplay")]
	[Icon("moving")]
	[Description("A platform that moves between nodes on a predefined path. See movement_path in the Path Tool.")]
	public class PathPlatformEntity : KeyframeEntity
	{
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x00046E39 File Offset: 0x00045039
		// (set) Token: 0x0600124E RID: 4686 RVA: 0x00046E41 File Offset: 0x00045041
		[Property]
		[global::DefaultValue(64f)]
		[Description("The speed to move/rotate with.")]
		protected float Speed { get; set; } = 64f;

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x00046E4A File Offset: 0x0004504A
		// (set) Token: 0x06001250 RID: 4688 RVA: 0x00046E52 File Offset: 0x00045052
		[Property]
		[global::DefaultValue(32f)]
		[Description("This represents imaginary \"distance to wheels\" length from the center of the platform in either direction and is used to calculate correct angles when turning.<br /> Higher values will make turns smoother. This value should not exceed half of the platform's length in the direction of movement.")]
		protected float Length { get; set; } = 32f;

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x00046E5B File Offset: 0x0004505B
		// (set) Token: 0x06001252 RID: 4690 RVA: 0x00046E63 File Offset: 0x00045063
		[Property("path_entity", null)]
		[FGDType("target_destination", "", "")]
		[Description("The path_generic entity that defines path nodes for this platform.")]
		public string PathEntity { get; set; }

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x00046E6C File Offset: 0x0004506C
		// (set) Token: 0x06001254 RID: 4692 RVA: 0x00046E74 File Offset: 0x00045074
		[Property("starts_moving", null, Title = "Starts Moving on Spawn")]
		[Category("Automatic Movement")]
		[Description("If set, will automatically start moving forwards from first node on spawn.")]
		public bool StartsMoving { get; set; }

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x00046E7D File Offset: 0x0004507D
		// (set) Token: 0x06001256 RID: 4694 RVA: 0x00046E85 File Offset: 0x00045085
		[Property("rotate_along_path", null)]
		[Category("Automatic Rotation")]
		[Description("If set, the entity will automatically rotate to face the direction of movement. Moving backwards will NOT flip the rotation 180 degrees.")]
		public bool RotateAlongsidePath { get; set; }

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x00046E8E File Offset: 0x0004508E
		// (set) Token: 0x06001258 RID: 4696 RVA: 0x00046E96 File Offset: 0x00045096
		[Property("movedir", null, Title = "Forward Direction")]
		[Category("Automatic Rotation")]
		[Description("Specifies the direction to move in when the platform is used, or axis of rotation for rotating platforms.")]
		public Angles MoveDir { get; set; } = new Angles(0f, 0f, 0f);

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x00046E9F File Offset: 0x0004509F
		// (set) Token: 0x0600125A RID: 4698 RVA: 0x00046EA7 File Offset: 0x000450A7
		[Property("end_action", null)]
		[Category("Automatic Movement")]
		[global::DefaultValue(PathPlatformEntity.OnEndAction.Stop)]
		[Description("What to do when reaching the end of the path when movement was initiated by the \"StartMoving\" input or \"Starts Moving\" flag. This also applies when moving backwards.")]
		public PathPlatformEntity.OnEndAction EndAction { get; set; }

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x00046EB0 File Offset: 0x000450B0
		// (set) Token: 0x0600125C RID: 4700 RVA: 0x00046EB8 File Offset: 0x000450B8
		[Property("start_move_sound", null)]
		[Category("Sounds")]
		[FGDType("sound", "", "")]
		[Description("Sound to play when starting to move.")]
		public string StartMoveSound { get; set; }

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x00046EC1 File Offset: 0x000450C1
		// (set) Token: 0x0600125E RID: 4702 RVA: 0x00046EC9 File Offset: 0x000450C9
		[Property("stop_move_sound", null)]
		[Category("Sounds")]
		[FGDType("sound", "", "")]
		[Description("Sound to play when we stopped moving.")]
		public string StopMoveSound { get; set; }

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x00046ED2 File Offset: 0x000450D2
		// (set) Token: 0x06001260 RID: 4704 RVA: 0x00046EDA File Offset: 0x000450DA
		[Property("moving_sound", null)]
		[Category("Sounds")]
		[FGDType("sound", "", "")]
		[Description("Sound to play while platform is moving.")]
		public string MovingSound { get; set; }

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x00046EE3 File Offset: 0x000450E3
		// (set) Token: 0x06001262 RID: 4706 RVA: 0x00046EEB File Offset: 0x000450EB
		[Description("Whether the platform is moving or not")]
		public bool IsMoving { get; protected set; }

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x00046EF4 File Offset: 0x000450F4
		// (set) Token: 0x06001264 RID: 4708 RVA: 0x00046EFC File Offset: 0x000450FC
		[Description("The platform's move direction.")]
		public bool IsMovingForwards { get; protected set; }

		// Token: 0x06001265 RID: 4709 RVA: 0x00046F05 File Offset: 0x00045105
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetupPhysicsFromModel(PhysicsMotionType.Keyframed, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsMoving = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsMovingForwards = true;
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00046F38 File Offset: 0x00045138
		[Event.Entity.PostSpawnAttribute]
		[Event.Entity.PostCleanupAttribute]
		private void PostMapSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.WarpToPoint(1);
			if (this.StartsMoving && base.IsServer)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.StartMoving();
			}
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00046F64 File Offset: 0x00045164
		[Event.Tick.ServerAttribute]
		private void DebugOverlayTick()
		{
			if (!base.DebugFlags.HasFlag(EntityDebugFlags.Text) && !base.DebugFlags.HasFlag(EntityDebugFlags.OVERLAY_BBOX_BIT))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GenericPathEntity pathEntity = this.GetPathEntity();
			if (pathEntity != null)
			{
				pathEntity.DrawPath(24, false);
			}
			float len = (this.Length <= 0f) ? 32f : this.Length;
			Vector3 nextPos = this.GetPointOnNodePath(Math.Min(this.lengthGone, this.lengthTotal) + len);
			Vector3 prevPos = this.GetPointOnNodePath(Math.Min(this.lengthGone, this.lengthTotal) - len);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.DebugOverlay.Sphere(nextPos, 3f, Color.Blue, 0f, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.DebugOverlay.Sphere(prevPos, 3f, Color.Blue, 0f, false);
			Vector3 targetPos = this.GetPointOnNodePath(this.lengthGone);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.DebugOverlay.Sphere(targetPos, 3f, Color.Green, 0f, false);
			Vector3 lastPos = this.pathPoints.First<Vector3>();
			GenericPathEntity pathEnt = this.GetPathEntity();
			for (int i = 1; i < this.pathPoints.Count; i++)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.DebugOverlay.Line(pathEnt.Transform.PointToWorld(lastPos) + Vector3.Up, pathEnt.Transform.PointToWorld(this.pathPoints[i]) + Vector3.Up, Color.Blue, 0f, true);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				lastPos = this.pathPoints[i];
			}
			if (!base.DebugFlags.HasFlag(EntityDebugFlags.Text))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			DebugOverlay debugOverlay = GlobalGameNamespace.DebugOverlay;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(13, 3);
			defaultInterpolatedStringHandler.AppendLiteral("Node: ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.m_currentNode + 1);
			defaultInterpolatedStringHandler.AppendLiteral(" (");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.m_nextNode + 1);
			defaultInterpolatedStringHandler.AppendLiteral(") => ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.m_targetNode + 1);
			debugOverlay.Text(defaultInterpolatedStringHandler.ToStringAndClear(), base.WorldSpaceBounds.Center, 10, Color.White, 0f, 2500f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			DebugOverlay debugOverlay2 = GlobalGameNamespace.DebugOverlay;
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(14, 3);
			defaultInterpolatedStringHandler.AppendLiteral("Moving: ");
			defaultInterpolatedStringHandler.AppendFormatted<bool>(this.IsMoving);
			defaultInterpolatedStringHandler.AppendLiteral(" (");
			defaultInterpolatedStringHandler.AppendFormatted(this.IsMovingForwards ? "Forwards" : "Backwards");
			defaultInterpolatedStringHandler.AppendLiteral(") @ ");
			defaultInterpolatedStringHandler.AppendFormatted<float>(this.Speed);
			debugOverlay2.Text(defaultInterpolatedStringHandler.ToStringAndClear(), base.WorldSpaceBounds.Center, 11, Color.White, 0f, 2500f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			DebugOverlay debugOverlay3 = GlobalGameNamespace.DebugOverlay;
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(23, 3);
			defaultInterpolatedStringHandler.AppendLiteral("Progress: ");
			defaultInterpolatedStringHandler.AppendFormatted<float>(this.lengthGone.Floor());
			defaultInterpolatedStringHandler.AppendLiteral(" of ");
			defaultInterpolatedStringHandler.AppendFormatted<float>(this.lengthToGo.Floor());
			defaultInterpolatedStringHandler.AppendLiteral(" (");
			defaultInterpolatedStringHandler.AppendFormatted<float>(this.lengthTotal.Floor());
			defaultInterpolatedStringHandler.AppendLiteral(" total)");
			debugOverlay3.Text(defaultInterpolatedStringHandler.ToStringAndClear(), base.WorldSpaceBounds.Center, 12, Color.White, 0f, 2500f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			DebugOverlay debugOverlay4 = GlobalGameNamespace.DebugOverlay;
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 1);
			defaultInterpolatedStringHandler.AppendLiteral("Path Changing: ");
			defaultInterpolatedStringHandler.AppendFormatted<PathPlatformEntity.PathChangingState>(this.pathChanging);
			debugOverlay4.Text(defaultInterpolatedStringHandler.ToStringAndClear(), base.WorldSpaceBounds.Center, 13, Color.White, 0f, 2500f);
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x0004735F File Offset: 0x0004555F
		public GenericPathEntity GetPathEntity()
		{
			if (this.internalPathEntity != null && this.internalPathEntity.IsValid)
			{
				return this.internalPathEntity;
			}
			return (GenericPathEntity)Entity.FindByName(this.PathEntity, null);
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00047390 File Offset: 0x00045590
		private void GeneratePoints(BasePathNode start, BasePathNode end, int segments, bool reverse)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.pathPoints.Clear();
			GenericPathEntity pathEnt = this.GetPathEntity();
			if (pathEnt == null)
			{
				return;
			}
			for (int i = 0; i <= segments; i++)
			{
				Vector3 lerpPos = pathEnt.GetPointBetweenNodes(start, end, (float)i / (float)segments, reverse);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.pathPoints.Add(pathEnt.Transform.PointToLocal(lerpPos));
			}
			BasePathNode nextNextNode = this.GetAdjacentNode(end, reverse);
			if (nextNextNode != null)
			{
				float lengthAdded = 0f;
				while (lengthAdded < this.Length * 2f && nextNextNode != null)
				{
					Vector3 lastPos = this.pathPoints.Last<Vector3>();
					for (int j = 0; j <= segments; j++)
					{
						Vector3 lerpPos2 = pathEnt.GetPointBetweenNodes(end, nextNextNode, (float)j / (float)segments, reverse);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.pathPoints.Add(pathEnt.Transform.PointToLocal(lerpPos2));
						RuntimeHelpers.EnsureSufficientExecutionStack();
						lengthAdded += lastPos.Distance(this.pathPoints.Last<Vector3>());
						RuntimeHelpers.EnsureSufficientExecutionStack();
						lastPos = this.pathPoints.Last<Vector3>();
						if (lengthAdded > this.Length * 2f)
						{
							break;
						}
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					end = nextNextNode;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					nextNextNode = this.GetAdjacentNode(end, reverse);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.lengthTotal += lengthAdded;
			}
			BasePathNode prevNode = this.m_lastNode ?? this.GetAdjacentNode(start, !reverse);
			if (prevNode != null)
			{
				float lengthAdded2 = 0f;
				while (lengthAdded2 < this.Length * 2f && prevNode != null)
				{
					Vector3 lastPos2 = this.pathPoints.First<Vector3>();
					for (int k = 0; k <= segments; k++)
					{
						Vector3 lerpPos3 = pathEnt.GetPointBetweenNodes(prevNode, start, (float)(segments - k) / (float)segments, reverse);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.pathPoints.Insert(0, pathEnt.Transform.PointToLocal(lerpPos3));
						RuntimeHelpers.EnsureSufficientExecutionStack();
						lengthAdded2 += lastPos2.Distance(this.pathPoints.First<Vector3>());
						RuntimeHelpers.EnsureSufficientExecutionStack();
						lastPos2 = this.pathPoints.First<Vector3>();
						if (lengthAdded2 > this.Length * 2f)
						{
							break;
						}
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					start = prevNode;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					prevNode = this.GetAdjacentNode(start, !reverse);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.lengthTotal += lengthAdded2;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.lengthToGo += lengthAdded2;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.lengthGone += lengthAdded2;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.lengthStart = lengthAdded2;
			}
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00047614 File Offset: 0x00045814
		[Description("Necessary for constant speed on beizer curve")]
		private Vector3 GetPointOnNodePath(float targetDist)
		{
			GenericPathEntity pathEnt = this.GetPathEntity();
			if (pathEnt == null || this.pathPoints.Count < 1)
			{
				return Vector3.Zero;
			}
			float dist = 0f;
			Vector3 prevPos = this.pathPoints[0];
			foreach (Vector3 point in this.pathPoints)
			{
				float distLocal = point.Distance(prevPos);
				if (dist + distLocal >= targetDist)
				{
					Vector3 output = prevPos.LerpTo(point, (targetDist - dist) / distLocal);
					return pathEnt.Transform.PointToWorld(output);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				dist += distLocal;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				prevPos = point;
			}
			Vector3 output_l = this.pathPoints[this.pathPoints.Count - 1];
			return pathEnt.Transform.PointToWorld(output_l);
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00047708 File Offset: 0x00045908
		private BasePathNode GetNextNode(bool forceAltPath = false)
		{
			if (this.m_nextNode < 0)
			{
				return null;
			}
			GenericPathEntity pathEnt = this.GetPathEntity();
			if (pathEnt == null)
			{
				return null;
			}
			BasePathNode node = pathEnt.PathNodes[this.m_nextNode];
			if (this.m_lastNode == null)
			{
				MovementPathNodeEntity movNode = pathEnt.PathNodes[this.m_currentNode].Entity as MovementPathNodeEntity;
				if (movNode != null)
				{
					if (((forceAltPath && movNode.AlternativePathEnabled && this.IsMovingForwards) || this.pathChanging == PathPlatformEntity.PathChangingState.ChangingForwards) && movNode.AlternativeNodeForwards.IsValid())
					{
						BasePathNodeEntity nodeEnt = movNode.AlternativeNodeForwards.GetTargets(this).First<Entity>() as BasePathNodeEntity;
						BasePathNode outNode = (from x in (nodeEnt.PathEntity as GenericPathEntity).PathNodes
						where x.Entity == nodeEnt
						select x).FirstOrDefault<BasePathNode>();
						if (outNode != null)
						{
							return outNode;
						}
					}
					if (((forceAltPath && movNode.AlternativePathEnabled && !this.IsMovingForwards) || this.pathChanging == PathPlatformEntity.PathChangingState.ChangingBackwards) && movNode.AlternativeNodeBackwards.IsValid())
					{
						BasePathNodeEntity nodeEnt = movNode.AlternativeNodeBackwards.GetTargets(this).First<Entity>() as BasePathNodeEntity;
						BasePathNode outNode2 = (from x in (nodeEnt.PathEntity as GenericPathEntity).PathNodes
						where x.Entity == nodeEnt
						select x).FirstOrDefault<BasePathNode>();
						if (outNode2 != null)
						{
							return outNode2;
						}
					}
				}
			}
			return node;
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00047880 File Offset: 0x00045A80
		private BasePathNode GetAdjacentNode(BasePathNode node, bool reverse)
		{
			GenericPathEntity pathEnt = node.PathEntity as GenericPathEntity;
			MovementPathNodeEntity movNode = node.Entity as MovementPathNodeEntity;
			if (movNode != null && movNode.AlternativePathEnabled)
			{
				if (reverse)
				{
					if (!this.IsMovingForwards)
					{
						Entity entity = movNode.AlternativeNodeBackwards.GetTargets(null).FirstOrDefault<Entity>();
						BasePathNodeEntity newNode = entity as BasePathNodeEntity;
						if (newNode != null)
						{
							BasePathNode outNode = (from x in (newNode.PathEntity as GenericPathEntity).PathNodes
							where x.Entity == newNode
							select x).FirstOrDefault<BasePathNode>();
							if (outNode != null)
							{
								return outNode;
							}
						}
					}
				}
				else
				{
					if (this.IsMovingForwards)
					{
						Entity entity = movNode.AlternativeNodeForwards.GetTargets(null).FirstOrDefault<Entity>();
						BasePathNodeEntity newNode = entity as BasePathNodeEntity;
						if (newNode != null)
						{
							BasePathNode outNode2 = (from x in (newNode.PathEntity as GenericPathEntity).PathNodes
							where x.Entity == newNode
							select x).FirstOrDefault<BasePathNode>();
							if (outNode2 != null)
							{
								return outNode2;
							}
						}
					}
				}
			}
			int index = pathEnt.PathNodes.IndexOf(node);
			int nextIndex = index + (reverse ? -1 : 1);
			if (nextIndex >= 0 && nextIndex <= pathEnt.PathNodes.Count - 1)
			{
				return pathEnt.PathNodes[nextIndex];
			}
			MovementPathEntity movPath = node.PathEntity as MovementPathEntity;
			if (movPath != null && movPath.Looped)
			{
				if (index == 0 && reverse)
				{
					return pathEnt.PathNodes.Last<BasePathNode>();
				}
				if (index == pathEnt.PathNodes.Count - 1 && !reverse)
				{
					return pathEnt.PathNodes.First<BasePathNode>();
				}
			}
			return null;
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00047A2C File Offset: 0x00045C2C
		private void GoToNextNode(bool generateOnly = false)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.lengthGone = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.lengthToGo = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.lengthTotal = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.lengthStart = 0f;
			GenericPathEntity pathEnt = this.GetPathEntity();
			if (pathEnt == null)
			{
				return;
			}
			MovementPathEntity movPath = pathEnt as MovementPathEntity;
			bool looped = movPath != null && movPath.Looped;
			int lastNode = pathEnt.PathNodes.Count - 1;
			if (!generateOnly)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.m_nextNode = this.m_currentNode + 1;
				if (!this.IsMovingForwards)
				{
					this.m_nextNode = this.m_currentNode - 1;
				}
				if (looped && this.IsMovingForwards && this.m_currentNode == lastNode)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.m_nextNode = 0;
				}
				else if (looped && !this.IsMovingForwards && this.m_currentNode == 0)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.m_nextNode = lastNode;
				}
				if (this.m_nextNode < 0 || this.m_nextNode > lastNode)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					GlobalGameNamespace.Log.Warning(FormattableStringFactory.Create("{0} Can't move to node {1}", new object[]
					{
						this,
						this.m_nextNode
					}));
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.m_nextNode = -1;
					return;
				}
				MovementPathNodeEntity moveNode = pathEnt.PathNodes[this.m_currentNode].Entity as MovementPathNodeEntity;
				if (moveNode != null && moveNode.Speed > 0f)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Speed = moveNode.Speed;
				}
				if (this.GetNextNode(true).PathEntity != pathEnt)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.pathChanging = (this.IsMovingForwards ? PathPlatformEntity.PathChangingState.ChangingForwards : PathPlatformEntity.PathChangingState.ChangingBackwards);
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.lengthToGo = pathEnt.GetCurveLength(pathEnt.PathNodes[this.m_currentNode], this.GetNextNode(false), 128, !this.IsMovingForwards);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.lengthTotal = this.lengthToGo;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.GeneratePoints(pathEnt.PathNodes[this.m_currentNode], this.GetNextNode(false), 128, !this.IsMovingForwards);
			if (!generateOnly)
			{
				this.m_lastNode = null;
			}
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00047C50 File Offset: 0x00045E50
		private void HandleDirectionChange(bool wantsForward)
		{
			if (this.IsMovingForwards == wantsForward)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsMovingForwards = wantsForward;
			if (this.m_nextNode == -1 || this.lengthToGo <= 0f)
			{
				return;
			}
			bool reverseDir = false;
			if (this.pathChanging != PathPlatformEntity.PathChangingState.None)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				reverseDir = (this.pathChanging == (this.IsMovingForwards ? PathPlatformEntity.PathChangingState.ChangingBackwards : PathPlatformEntity.PathChangingState.ChangingForwards));
			}
			else
			{
				int current = this.m_currentNode;
				int next = this.m_nextNode;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.m_currentNode = next;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.m_nextNode = current;
			}
			float prevRemainder = this.lengthToGo - this.lengthGone;
			if (reverseDir)
			{
				this.IsMovingForwards = !this.IsMovingForwards;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.GoToNextNode(true);
			if (reverseDir)
			{
				float oldEnd = this.lengthTotal - this.lengthToGo;
				float oldStart = this.lengthStart;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.IsMovingForwards = !this.IsMovingForwards;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.pathPoints.Reverse();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.lengthStart = oldEnd;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.lengthToGo += oldEnd - oldStart;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.lengthGone = this.lengthStart + prevRemainder;
			if (this.IsMoving)
			{
				base.PlaySound(this.StartMoveSound);
			}
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00047D8C File Offset: 0x00045F8C
		private void AlignToPath()
		{
			if (!this.RotateAlongsidePath)
			{
				return;
			}
			GenericPathEntity pathEnt = this.GetPathEntity();
			if (pathEnt == null)
			{
				return;
			}
			float len = (this.Length <= 0f) ? 32f : this.Length;
			Vector3 nextPos = this.GetPointOnNodePath(Math.Min(this.lengthGone, this.lengthTotal) + len);
			Vector3 lastPos = this.GetPointOnNodePath(Math.Min(this.lengthGone, this.lengthTotal) - len);
			if (this.IsMovingForwards)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Rotation = Rotation.LookAt((nextPos - lastPos).Normal, (pathEnt.Parent != null) ? pathEnt.Parent.Rotation.Up : Vector3.Up) * Rotation.From(this.MoveDir).Inverse;
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Rotation = Rotation.LookAt((lastPos - nextPos).Normal, (pathEnt.Parent != null) ? pathEnt.Parent.Rotation.Up : Vector3.Up) * Rotation.From(this.MoveDir).Inverse;
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x00047EBD File Offset: 0x000460BD
		// (set) Token: 0x06001271 RID: 4721 RVA: 0x00047EC5 File Offset: 0x000460C5
		[Description("Fired when the platform starts to move.")]
		protected Entity.Output OnMovementStart { get; set; }

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x00047ECE File Offset: 0x000460CE
		// (set) Token: 0x06001273 RID: 4723 RVA: 0x00047ED6 File Offset: 0x000460D6
		[Description("Fired when the platform stops moving. Sends current point number as parameter.")]
		protected Entity.Output<int> OnMovementEnd { get; set; }

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x00047EDF File Offset: 0x000460DF
		// (set) Token: 0x06001275 RID: 4725 RVA: 0x00047EE7 File Offset: 0x000460E7
		[Description("Fired when the platform is already at given node number, when using inputs to move it. Carries the current node number as parameter.")]
		protected Entity.Output<int> OnAlreadyThere { get; set; }

		// Token: 0x06001276 RID: 4726 RVA: 0x00047EF0 File Offset: 0x000460F0
		private Task DoMove()
		{
			PathPlatformEntity.<DoMove>d__87 <DoMove>d__;
			<DoMove>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DoMove>d__.<>4__this = this;
			<DoMove>d__.<>1__state = -1;
			<DoMove>d__.<>t__builder.Start<PathPlatformEntity.<DoMove>d__87>(ref <DoMove>d__);
			return <DoMove>d__.<>t__builder.Task;
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x00047F34 File Offset: 0x00046134
		[Input]
		[Description("Start moving through our nodes in whatever direction we were moving before until we reach either end of the path.")]
		public void StartMoving()
		{
			GenericPathEntity pathEnt = this.GetPathEntity();
			if (pathEnt == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.m_targetNode = (this.IsMovingForwards ? (pathEnt.PathNodes.Count - 1) : 0);
			MovementPathEntity movPath = pathEnt as MovementPathEntity;
			if (movPath != null && movPath.Looped)
			{
				int lastNode = pathEnt.PathNodes.Count - 1;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.m_targetNode = (this.IsMovingForwards ? ((this.m_currentNode == lastNode) ? 0 : lastNode) : ((this.m_currentNode == 0) ? lastNode : 0));
			}
			if (this.m_targetNode == this.m_currentNode && this.lengthToGo < 1f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnAlreadyThere.Fire(this, this.m_currentNode + 1, 0f);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AutomaticMovement = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoMove();
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00048013 File Offset: 0x00046213
		[Input]
		[Description("Start moving forward through our nodes until we reach end of the path.")]
		public void StartForward()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HandleDirectionChange(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StartMoving();
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x0004802C File Offset: 0x0004622C
		[Input]
		[Description("Start moving backwards through our nodes until we reach start of the path.")]
		public void StartBackwards()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HandleDirectionChange(false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StartMoving();
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x00048045 File Offset: 0x00046245
		[Input]
		[Description("Reverse current movement direction, regardless whether we are currently moving or not.")]
		public void ReverseDirection()
		{
			bool isMoving = this.IsMoving;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HandleDirectionChange(!this.IsMovingForwards);
			if (isMoving)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.StartMoving();
			}
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x00048070 File Offset: 0x00046270
		[Input]
		[Description("Stop moving.")]
		public void StopMoving()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.movement++;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LocalVelocity = Vector3.Zero;
			if (this.IsMoving)
			{
				if (this.MoveSoundInstance != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.MoveSoundInstance.Value.Stop();
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.MoveSoundInstance = null;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.PlaySound(this.StopMoveSound);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnMovementEnd.Fire(this, this.m_currentNode + 1, 0f);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsMoving = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AutomaticMovement = false;
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x0004812C File Offset: 0x0004632C
		[Input]
		[Description("Go to specific node (Starting with 1) set by the parameter and stop there.")]
		public void GoToPoint(int targetNode)
		{
			GenericPathEntity pathEnt = this.GetPathEntity();
			if (pathEnt == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.m_targetNode = Math.Clamp(targetNode - 1, 0, pathEnt.PathNodes.Count - 1);
			if (this.m_targetNode == this.m_currentNode && this.lengthToGo < 1f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnAlreadyThere.Fire(this, this.m_currentNode + 1, 0f);
				return;
			}
			bool wantsForward = this.m_currentNode < this.m_targetNode;
			if (this.m_currentNode == this.m_targetNode && this.m_nextNode != -1)
			{
				wantsForward = (this.m_nextNode < this.m_targetNode);
			}
			MovementPathEntity movPath = pathEnt as MovementPathEntity;
			if (movPath != null && movPath.Looped)
			{
				float fwdDist = 0f;
				int nextNode = this.m_currentNode;
				do
				{
					if (nextNode++ >= pathEnt.PathNodes.Count - 1)
					{
						nextNode = 0;
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					fwdDist += pathEnt.GetCurveLength(pathEnt.PathNodes[nextNode], this.GetAdjacentNode(pathEnt.PathNodes[nextNode], false), 48, false);
				}
				while (nextNode != this.m_targetNode);
				float backDist = 0f;
				int prevNode = this.m_currentNode;
				do
				{
					if (prevNode-- <= 0)
					{
						prevNode = pathEnt.PathNodes.Count - 1;
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					backDist += pathEnt.GetCurveLength(pathEnt.PathNodes[prevNode], this.GetAdjacentNode(pathEnt.PathNodes[prevNode], true), 48, true);
				}
				while (prevNode != this.m_targetNode);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				wantsForward = (fwdDist < backDist);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HandleDirectionChange(wantsForward);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AutomaticMovement = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoMove();
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x000482EC File Offset: 0x000464EC
		[Input]
		[Description("Go to the next node on the path and stop there.")]
		public void GoToNextPoint()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.GoToPoint(this.m_currentNode + 1 + 1);
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00048303 File Offset: 0x00046503
		[Input]
		[Description("Go to the previous node on the path and stop there.")]
		public void GoToPrevPoint()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.GoToPoint(this.m_currentNode - 1 + 1);
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x0004831C File Offset: 0x0004651C
		[Input]
		[Description("Teleport to a given node (Starting with 1). Does not stop or start movement.")]
		public void WarpToPoint(int targetNode)
		{
			GenericPathEntity pathEnt = this.GetPathEntity();
			if (pathEnt == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			targetNode = Math.Clamp(targetNode - 1, 0, pathEnt.PathNodes.Count - 1);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.m_currentNode = targetNode;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.m_nextNode = -1;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.lengthToGo = -1f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Position = pathEnt.PathNodes[targetNode].WorldPosition;
			MovementPathNodeEntity moveNode = pathEnt.PathNodes[this.m_currentNode].Entity as MovementPathNodeEntity;
			if (moveNode != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				moveNode.OnPassed.Fire(this, 0f);
			}
			if (this.IsMoving)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DoMove();
				return;
			}
			if (this.m_currentNode >= pathEnt.PathNodes.Count - 1)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.GeneratePoints(pathEnt.PathNodes[this.m_currentNode - 1], pathEnt.PathNodes[this.m_currentNode], 128, false);
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.GeneratePoints(pathEnt.PathNodes[this.m_currentNode], pathEnt.PathNodes[this.m_currentNode + 1], 128, false);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AlignToPath();
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x0004846F File Offset: 0x0004666F
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void CreateHammerOutputs()
		{
			this.OnMovementStart = new Entity.Output(this, "OnMovementStart");
			this.OnMovementEnd = new Entity.Output<int>(this, "OnMovementEnd");
			this.OnAlreadyThere = new Entity.Output<int>(this, "OnAlreadyThere");
			base.CreateHammerOutputs();
		}

		// Token: 0x040005D4 RID: 1492
		private bool AutomaticMovement;

		// Token: 0x040005D5 RID: 1493
		private GenericPathEntity internalPathEntity;

		// Token: 0x040005D6 RID: 1494
		private List<Vector3> pathPoints = new List<Vector3>();

		// Token: 0x040005D7 RID: 1495
		private float lengthToGo;

		// Token: 0x040005D8 RID: 1496
		private float lengthTotal;

		// Token: 0x040005D9 RID: 1497
		private float lengthGone;

		// Token: 0x040005DA RID: 1498
		private float lengthStart;

		// Token: 0x040005DE RID: 1502
		private PathPlatformEntity.PathChangingState pathChanging;

		// Token: 0x040005DF RID: 1503
		private int movement;

		// Token: 0x040005E0 RID: 1504
		private int m_currentNode;

		// Token: 0x040005E1 RID: 1505
		private int m_nextNode = -1;

		// Token: 0x040005E2 RID: 1506
		private int m_targetNode = -1;

		// Token: 0x040005E3 RID: 1507
		private BasePathNode m_lastNode;

		// Token: 0x040005E4 RID: 1508
		private Sound? MoveSoundInstance;

		// Token: 0x02000241 RID: 577
		public enum OnEndAction
		{
			// Token: 0x04000984 RID: 2436
			Stop,
			// Token: 0x04000985 RID: 2437
			WarpToStart,
			// Token: 0x04000986 RID: 2438
			ReverseDirection
		}

		// Token: 0x02000242 RID: 578
		private enum PathChangingState
		{
			// Token: 0x04000988 RID: 2440
			None,
			// Token: 0x04000989 RID: 2441
			ChangingForwards,
			// Token: 0x0400098A RID: 2442
			ChangingBackwards
		}
	}
}
