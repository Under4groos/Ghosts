using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x0200008C RID: 140
	public class CatGroup : NPCGroup
	{
		// Token: 0x06000654 RID: 1620 RVA: 0x0001D814 File Offset: 0x0001BA14
		protected override void OnLeaderSet()
		{
			foreach (NPCBase npcbase in this.Members)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				npcbase.RequestFollowTarget(this.Leader);
			}
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001D870 File Offset: 0x0001BA70
		protected override void OnMemberAdded(NPCBase npc)
		{
			if (this.Leader.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				npc.RequestFollowTarget(this.Leader);
			}
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001D890 File Offset: 0x0001BA90
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

		// Token: 0x06000657 RID: 1623 RVA: 0x0001D8F4 File Offset: 0x0001BAF4
		public override void OnLeaderDeath()
		{
			foreach (NPCBase npc in this.Members)
			{
				if (npc.IsValid())
				{
					CatNPC cat = npc as CatNPC;
					if (cat != null)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						cat.SM.SetState(cat.FleeState);
					}
				}
			}
		}
	}
}
