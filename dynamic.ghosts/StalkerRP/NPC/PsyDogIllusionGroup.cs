using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000D3 RID: 211
	public class PsyDogIllusionGroup : NPCGroup
	{
		// Token: 0x06000915 RID: 2325 RVA: 0x00027B01 File Offset: 0x00025D01
		private Entity GetLeaderTarget()
		{
			if (!this.Leader.IsValid())
			{
				return null;
			}
			return this.Leader.Target;
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00027B1D File Offset: 0x00025D1D
		protected override void OnLeaderSet()
		{
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00027B20 File Offset: 0x00025D20
		protected override void OnMemberAdded(NPCBase npc)
		{
			Entity target = this.GetLeaderTarget();
			if (target.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				npc.TargetingComponent.TrySetTarget(target);
			}
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00027B4E File Offset: 0x00025D4E
		public void OnMemberDied(NPCBase npc)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Members.Remove(npc);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00027B64 File Offset: 0x00025D64
		public override void OnLeaderAcquiredTarget(Entity target)
		{
			foreach (NPCBase npcbase in this.Members)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				npcbase.TargetingComponent.TrySetTarget(target);
			}
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00027BC0 File Offset: 0x00025DC0
		public override void OnLeaderDeath()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DissipateIllusions();
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00027BD0 File Offset: 0x00025DD0
		public void DissipateIllusions()
		{
			foreach (PsyDogIllusionNPC psyDogIllusionNPC in this.Members.OfType<PsyDogIllusionNPC>())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				psyDogIllusionNPC.DissipateIllusion();
			}
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00027C24 File Offset: 0x00025E24
		public bool IsEmpty()
		{
			return this.NumIllusions() == 0;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00027C2F File Offset: 0x00025E2F
		public int NumIllusions()
		{
			return this.Members.Count;
		}
	}
}
