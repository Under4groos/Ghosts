using System;
using System.Runtime.CompilerServices;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x02000172 RID: 370
	[Library("path_generic_node")]
	[PathNode]
	[Description("A basic node description for the <see cref=\"T:Sandbox.BasePathEntity`1\">BasePathEntity</see>. Please note that <see cref=\"T:Sandbox.BasePathNode\">BasePathNodes</see> are NOT actual entities, therefore cannot support inputs and outputs. See <see cref=\"T:Sandbox.BasePathNodeEntity\">BasePathNodeEntity</see>.")]
	public class BasePathNode : IBasePathNode
	{
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x00043751 File Offset: 0x00041951
		// (set) Token: 0x060010E4 RID: 4324 RVA: 0x00043748 File Offset: 0x00041948
		[HideInEditor]
		[Description("Position of the node relative to the path entity.")]
		public Vector3 Position { get; set; } = Vector3.Zero;

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x00043762 File Offset: 0x00041962
		// (set) Token: 0x060010E6 RID: 4326 RVA: 0x00043759 File Offset: 0x00041959
		[HideInEditor]
		[Description("Position of the incoming tangent relative to the node's position. Includes rotation/scale of the node.")]
		public Vector3 TangentIn { get; set; } = Vector3.Zero;

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x00043773 File Offset: 0x00041973
		// (set) Token: 0x060010E8 RID: 4328 RVA: 0x0004376A File Offset: 0x0004196A
		[HideInEditor]
		[Description("Position of the outgoing tangent relative to the node's position. Includes rotation/scale of the node.")]
		public Vector3 TangentOut { get; set; } = Vector3.Zero;

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x00043784 File Offset: 0x00041984
		// (set) Token: 0x060010EA RID: 4330 RVA: 0x0004377B File Offset: 0x0004197B
		[HideInEditor]
		[Description("The entity associated with this path node, if they were set to spawn via <see cref=\"T:SandboxEditor.PathAttribute\">[Path]</see> This will be set as soon as the node entity spawns, which will be after Path entity's Spawn() function.")]
		public Entity Entity { get; set; }

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x00043795 File Offset: 0x00041995
		// (set) Token: 0x060010EC RID: 4332 RVA: 0x0004378C File Offset: 0x0004198C
		[HideInEditor]
		[Description("The path entity this node is associated with.")]
		public Entity PathEntity { get; internal set; }

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x000437A6 File Offset: 0x000419A6
		// (set) Token: 0x060010EE RID: 4334 RVA: 0x0004379D File Offset: 0x0004199D
		[HideInEditor]
		[Description("Used to set the Entity property. Couldn't find a way to hide it. Do not use.")]
		public string HammerUniqueId { get; set; }

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x000437B0 File Offset: 0x000419B0
		public Vector3 WorldTangentIn
		{
			get
			{
				if (this.Entity.IsValid())
				{
					BasePathNodeEntity node = this.Entity as BasePathNodeEntity;
					if (node != null)
					{
						return node.Transform.PointToWorld(node.TangentIn);
					}
				}
				if (this.PathEntity.IsValid())
				{
					return this.PathEntity.Transform.PointToWorld(this.Position + this.TangentIn);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Assert.True(false, "Should never get here!");
				return this.TangentIn;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x00043838 File Offset: 0x00041A38
		public Vector3 WorldTangentOut
		{
			get
			{
				if (this.Entity.IsValid())
				{
					BasePathNodeEntity node = this.Entity as BasePathNodeEntity;
					if (node != null)
					{
						return node.Transform.PointToWorld(node.TangentOut);
					}
				}
				if (this.PathEntity.IsValid())
				{
					return this.PathEntity.Transform.PointToWorld(this.Position + this.TangentOut);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Assert.True(false, "Should never get here!");
				return this.TangentOut;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x000438C0 File Offset: 0x00041AC0
		public Vector3 WorldPosition
		{
			get
			{
				if (this.Entity.IsValid())
				{
					return this.Entity.Position;
				}
				if (this.PathEntity.IsValid())
				{
					return this.PathEntity.Transform.PointToWorld(this.Position);
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Assert.True(false, "Should never get here!");
				return this.Position;
			}
		}
	}
}
