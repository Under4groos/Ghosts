using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000A8 RID: 168
	public class FleshGroup : NPCGroup
	{
		// Token: 0x06000768 RID: 1896 RVA: 0x00021C6C File Offset: 0x0001FE6C
		protected override void OnLeaderSet()
		{
			foreach (NPCBase npcbase in this.Members)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				npcbase.RequestFollowTarget(this.Leader);
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00021CC8 File Offset: 0x0001FEC8
		protected override void OnMemberAdded(NPCBase npc)
		{
			if (this.Leader.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				npc.RequestFollowTarget(this.Leader);
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00021CE8 File Offset: 0x0001FEE8
		public override void OnLeaderAcquiredTarget(Entity target)
		{
			foreach (NPCBase npc in this.Members)
			{
				if (npc.IsValid())
				{
					npc.TargetingComponent.TrySetTarget(target);
				}
			}
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00021D4C File Offset: 0x0001FF4C
		public override void OnLeaderDeath()
		{
			foreach (NPCBase npc in this.Members)
			{
				if (npc.IsValid())
				{
					FleshNPC flesh = npc as FleshNPC;
					if (flesh != null)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						flesh.SM.SetState(flesh.FleeState);
					}
				}
			}
		}
	}
}
