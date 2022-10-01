using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000EC RID: 236
	public class NPCGroup
	{
		// Token: 0x06000A33 RID: 2611 RVA: 0x0002BC08 File Offset: 0x00029E08
		public void AddMember(NPCBase npc)
		{
			if (this.Members.Add(npc))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnMemberAdded(npc);
			}
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0002BC24 File Offset: 0x00029E24
		public void RemoveMember(NPCBase npc)
		{
			if (this.Members.Remove(npc))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnMemberRemoved(npc);
			}
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0002BC40 File Offset: 0x00029E40
		public void RemoveAllMembers()
		{
			foreach (NPCBase npc in this.Members.ToList<NPCBase>())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.RemoveMember(npc);
			}
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0002BCA0 File Offset: 0x00029EA0
		public void SetLeader(NPCBase leader)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Leader = leader;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnLeaderSet();
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0002BCB9 File Offset: 0x00029EB9
		public void RemoveLeader()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Leader = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnLeaderRemoved();
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0002BCD2 File Offset: 0x00029ED2
		protected virtual void OnMemberAdded(NPCBase npc)
		{
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0002BCD4 File Offset: 0x00029ED4
		protected virtual void OnMemberRemoved(NPCBase npc)
		{
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0002BCD6 File Offset: 0x00029ED6
		protected virtual void OnLeaderSet()
		{
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0002BCD8 File Offset: 0x00029ED8
		protected virtual void OnLeaderRemoved()
		{
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0002BCDA File Offset: 0x00029EDA
		public virtual void OnLeaderAcquiredTarget(Entity target)
		{
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0002BCDC File Offset: 0x00029EDC
		public virtual void OnLeaderDeath()
		{
		}

		// Token: 0x0400033A RID: 826
		public readonly HashSet<NPCBase> Members = new HashSet<NPCBase>();

		// Token: 0x0400033B RID: 827
		public NPCBase Leader;
	}
}
