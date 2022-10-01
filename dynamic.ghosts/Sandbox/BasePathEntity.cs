using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Sandbox.Internal;
using Sandbox.Internal.Globals;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x02000170 RID: 368
	[Path(null, false)]
	[Description("A base entity that will appear in Hammer's Path Tool and automatically parse data from Hammer into a ready-to-use format in C#.")]
	public class BasePathEntity<T> : Entity where T : BasePathNode
	{
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x00043379 File Offset: 0x00041579
		// (set) Token: 0x060010D5 RID: 4309 RVA: 0x00043386 File Offset: 0x00041586
		[Property("pathNodesJSON", null)]
		[HideInEditor]
		[Net]
		[Description("This is generated this automatically during map compile time")]
		protected string pathNodesJSON
		{
			get
			{
				return this._repback__pathNodesJSON.GetValue();
			}
			set
			{
				this._repback__pathNodesJSON.SetValue(value);
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x0004339D File Offset: 0x0004159D
		// (set) Token: 0x060010D6 RID: 4310 RVA: 0x00043394 File Offset: 0x00041594
		[Description("A list of nodes this entity represents, as set up in Hammer.")]
		public List<T> PathNodes { get; protected set; } = new List<T>();

		// Token: 0x060010D8 RID: 4312 RVA: 0x000433A8 File Offset: 0x000415A8
		[Description("Internal, do not use. Used to link nodes to path entities. Finds a specific path entity and returns it.")]
		public static Entity FindPathEntity(int uniqueID)
		{
			foreach (BasePathEntity<BasePathNode> path in Entity.All.OfType<BasePathEntity<BasePathNode>>())
			{
				if (path.HammerID == uniqueID)
				{
					return path;
				}
			}
			return null;
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00043404 File Offset: 0x00041604
		private void LoadNodes()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PathNodes = JsonSerializer.Deserialize<List<T>>(this.pathNodesJSON, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});
			foreach (T t in this.PathNodes)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				t.PathEntity = this;
			}
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00043484 File Offset: 0x00041684
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LoadNodes();
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0004349C File Offset: 0x0004169C
		public override void ClientSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ClientSpawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LoadNodes();
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x000434B4 File Offset: 0x000416B4
		[Description("Visualizes the path for debugging purposes")]
		public virtual void DrawPath(int segments, bool drawTangents = false)
		{
			for (int nodeid = 0; nodeid < this.PathNodes.Count; nodeid++)
			{
				BasePathNode node = this.PathNodes[nodeid];
				Vector3 nodePos = node.WorldPosition;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.DebugOverlay.Sphere(nodePos, 4f, Color.White, 0f, true);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				DebugOverlay debugOverlay = GlobalGameNamespace.DebugOverlay;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
				defaultInterpolatedStringHandler.AppendFormatted<int>(nodeid + 1);
				debugOverlay.Text(defaultInterpolatedStringHandler.ToStringAndClear(), nodePos + Vector3.Up * 6f, Color.White, 0f, 2500f);
				if (drawTangents)
				{
					Vector3 nodeTanIn = node.WorldTangentIn;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					GlobalGameNamespace.DebugOverlay.Sphere(nodeTanIn, 2f, Color.Yellow, 0f, true);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					GlobalGameNamespace.DebugOverlay.Line(nodePos, nodeTanIn, Color.Yellow, 0f, true);
					Vector3 nodeTanOut = node.WorldTangentOut;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					GlobalGameNamespace.DebugOverlay.Sphere(nodeTanOut, 6f, Color.Orange, 0f, true);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					GlobalGameNamespace.DebugOverlay.Line(nodePos, nodeTanOut, Color.Orange, 0f, true);
				}
				BasePathNode nodeNext = (nodeid + 1 < this.PathNodes.Count) ? this.PathNodes[nodeid + 1] : default(T);
				if (nodeNext != null)
				{
					for (int i = 1; i <= segments; i++)
					{
						Vector3 lerpPos = this.GetPointBetweenNodes(node, nodeNext, (float)i / (float)segments, false);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						GlobalGameNamespace.DebugOverlay.Line(nodePos, lerpPos, Color.Green, 0f, true);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						nodePos = lerpPos;
					}
				}
			}
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x00043670 File Offset: 0x00041870
		[Description("Returns a point in world space on the cubic beizer curve between 2 given nodes")]
		public Vector3 GetPointBetweenNodes(IBasePathNode start, IBasePathNode end, float t, bool reverse = false)
		{
			Vector3 tanSrc = reverse ? start.WorldTangentIn : start.WorldTangentOut;
			Vector3 tanTgt = reverse ? end.WorldTangentOut : end.WorldTangentIn;
			return Vector3.CubicBeizer(start.WorldPosition, end.WorldPosition, tanSrc, tanTgt, t);
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x000436B8 File Offset: 0x000418B8
		[Description("Returns the approximate length of a curve between 2 nodes.")]
		public float GetCurveLength(IBasePathNode start, IBasePathNode end, int segments, bool reverse = false)
		{
			Vector3 lastPos = start.WorldPosition;
			float length = 0f;
			for (int i = 1; i <= segments; i++)
			{
				Vector3 lerpPos = this.GetPointBetweenNodes(start, end, (float)i / (float)segments, reverse);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				length += (lerpPos - lastPos).Length;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				lastPos = lerpPos;
			}
			return length;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x0004370E File Offset: 0x0004190E
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarGeneric<string>>(ref this._repback__pathNodesJSON, "pathNodesJSON", false, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x04000542 RID: 1346
		private VarGeneric<string> _repback__pathNodesJSON = new VarGeneric<string>();
	}
}
