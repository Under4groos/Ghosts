using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;
using SandboxEditor;

namespace StalkerRP
{
	// Token: 0x02000027 RID: 39
	[Title("NPC Spawn Point")]
	[Category("Map Tools")]
	[HammerEntity]
	public class NPCSpawnPoint : Entity
	{
		// Token: 0x0600013E RID: 318 RVA: 0x00009F56 File Offset: 0x00008156
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			NPCSpawnPoint.SpawnPoints.Add(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Transmit = TransmitType.Never;
		}

		// Token: 0x0400007A RID: 122
		public static readonly List<Entity> SpawnPoints = new List<Entity>();
	}
}
