using System;
using System.Runtime.CompilerServices;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x02000173 RID: 371
	[PathNode]
	[Description("A basic node entity for the <see cref=\"T:Sandbox.BasePathEntity`1\">BasePathEntity</see>. These can be used as alternatives to <see cref=\"T:Sandbox.BasePathNode\">BasePathNode</see> data structures with <see cref=\"T:SandboxEditor.PathAttribute\">[Path]'s</see> 2nd argument.")]
	public class BasePathNodeEntity : Entity, IBasePathNode
	{
		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x00043955 File Offset: 0x00041B55
		// (set) Token: 0x060010F4 RID: 4340 RVA: 0x0004394C File Offset: 0x00041B4C
		[Property("tangent_in", null)]
		[HideInEditor]
		[Description("Position of the incoming tangent relative to node's own position. Does NOT include node's rotation/scale.")]
		public Vector3 TangentIn { get; set; }

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x00043966 File Offset: 0x00041B66
		// (set) Token: 0x060010F6 RID: 4342 RVA: 0x0004395D File Offset: 0x00041B5D
		[Property("tangent_out", null)]
		[HideInEditor]
		[Description("Position of the outgoing tangent relative to node's own position. Does NOT include node's rotation/scale.")]
		public Vector3 TangentOut { get; set; }

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x00043977 File Offset: 0x00041B77
		// (set) Token: 0x060010F8 RID: 4344 RVA: 0x0004396E File Offset: 0x00041B6E
		[HideInEditor]
		[Description("The Path entity this node belongs to.")]
		public Entity PathEntity { get; protected set; }

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x00043988 File Offset: 0x00041B88
		// (set) Token: 0x060010FA RID: 4346 RVA: 0x0004397F File Offset: 0x00041B7F
		[Property("hammerUniqueIdPath", null)]
		[HideInEditor]
		internal int PathEntityID { get; set; }

		// Token: 0x060010FC RID: 4348 RVA: 0x00043990 File Offset: 0x00041B90
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PathEntity = BasePathEntity<BasePathNode>.FindPathEntity(this.PathEntityID);
			foreach (BasePathNode node in (this.PathEntity as BasePathEntity<BasePathNode>).PathNodes)
			{
				if (node.HammerUniqueId.ToInt(0) == this.HammerID)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					node.Entity = this;
				}
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x00043A28 File Offset: 0x00041C28
		public Vector3 WorldTangentIn
		{
			get
			{
				return base.Transform.PointToWorld(this.TangentIn);
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x00043A4C File Offset: 0x00041C4C
		public Vector3 WorldTangentOut
		{
			get
			{
				return base.Transform.PointToWorld(this.TangentOut);
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x00043A6D File Offset: 0x00041C6D
		public Vector3 WorldPosition
		{
			get
			{
				return this.Position;
			}
		}
	}
}
