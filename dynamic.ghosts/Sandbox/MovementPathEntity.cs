using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox.Internal;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x02000180 RID: 384
	[Library("movement_path")]
	[HammerEntity]
	[Path("movement_path_node", true)]
	[Title("Movement Path")]
	[Category("Gameplay")]
	[Icon("moving")]
	[Description("A movement path. Compiles each node as its own entity, allowing usage of inputs and outputs such as OnPassed.<br /> This entity can be used with entities like ent_path_platform.")]
	public class MovementPathEntity : GenericPathEntity
	{
		// Token: 0x0600123A RID: 4666 RVA: 0x00046B14 File Offset: 0x00044D14
		public override void DrawPath(int segments, bool drawTangents = false)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.DrawPath(segments, drawTangents);
			if (this.Looped)
			{
				BasePathNode start = base.PathNodes.Last<BasePathNode>();
				BasePathNode end = base.PathNodes.First<BasePathNode>();
				Vector3 nodePos = start.WorldPosition;
				for (int i = 1; i <= segments; i++)
				{
					Vector3 lerpPos = base.GetPointBetweenNodes(start, end, (float)i / (float)segments, false);
					if (i % 2 == 0)
					{
						GlobalGameNamespace.DebugOverlay.Line(nodePos, lerpPos, Color.Green.Darken(0.5f), 0f, true);
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					nodePos = lerpPos;
				}
			}
			foreach (BasePathNode node in base.PathNodes)
			{
				MovementPathNodeEntity mpNode = node.Entity as MovementPathNodeEntity;
				if (mpNode != null)
				{
					float darkenAmt = mpNode.AlternativePathEnabled ? 0f : 0.5f;
					BasePathNodeEntity nodeNext = mpNode.AlternativeNodeForwards.GetTargets(this).FirstOrDefault<Entity>() as BasePathNodeEntity;
					if (nodeNext != null)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						GlobalGameNamespace.DebugOverlay.Sphere(nodeNext.Position, 4f, Color.Orange.Darken(darkenAmt), 0f, true);
						Vector3 nodePos2 = node.WorldPosition;
						for (int j = 1; j <= segments; j++)
						{
							Vector3 lerpPos2 = base.GetPointBetweenNodes(node, nodeNext, (float)j / (float)segments, false);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							GlobalGameNamespace.DebugOverlay.Line(nodePos2, lerpPos2, Color.Yellow.Darken(darkenAmt), 0f, true);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							nodePos2 = lerpPos2;
						}
					}
					BasePathNodeEntity nodePrev = mpNode.AlternativeNodeBackwards.GetTargets(this).FirstOrDefault<Entity>() as BasePathNodeEntity;
					if (nodePrev != null)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						GlobalGameNamespace.DebugOverlay.Sphere(nodePrev.Position, 4f, Color.Orange.Darken(darkenAmt), 0f, true);
						Vector3 nodePos3 = node.WorldPosition;
						for (int k = 1; k <= segments; k++)
						{
							Vector3 lerpPos3 = base.GetPointBetweenNodes(node, nodePrev, (float)k / (float)segments, true);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							GlobalGameNamespace.DebugOverlay.Line(nodePos3, lerpPos3, Color.Cyan.Darken(darkenAmt), 0f, true);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							nodePos3 = lerpPos3;
						}
					}
				}
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x00046D78 File Offset: 0x00044F78
		// (set) Token: 0x0600123C RID: 4668 RVA: 0x00046D80 File Offset: 0x00044F80
		[Property]
		[DefaultValue(false)]
		[Description("Whether the path is looped or not.")]
		public bool Looped { get; set; }
	}
}
