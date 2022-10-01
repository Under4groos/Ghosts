using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000E3 RID: 227
	public class TushkanoGroup : NPCGroup
	{
		// Token: 0x060009CD RID: 2509 RVA: 0x0002A590 File Offset: 0x00028790
		protected override void OnLeaderSet()
		{
			if (!this.Members.Remove(this.Leader))
			{
				this.MaxMembers++;
			}
			foreach (NPCBase npcbase in this.Members)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				npcbase.RequestFollowTarget(this.Leader);
			}
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0002A60C File Offset: 0x0002880C
		protected override void OnMemberAdded(NPCBase npc)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MaxMembers++;
			if (this.Leader == null || !this.Leader.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.SetLeader(npc);
			}
			if (this.Leader.IsValid())
			{
				if (this.Leader.Target.IsValid())
				{
					npc.TargetingComponent.TrySetTarget(this.Leader.Target);
					return;
				}
				npc.RequestFollowTarget(this.Leader);
			}
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0002A690 File Offset: 0x00028890
		public void OnMemberDied(NPCBase npc)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Members.Remove(npc);
			if (this.Members.Count <= this.FleeThreshold)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Flee();
			}
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0002A6C4 File Offset: 0x000288C4
		private void Flee()
		{
			foreach (TushkanoNPC rat in this.Members.OfType<TushkanoNPC>())
			{
				if (rat.IsValid())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					rat.SM.SetState(rat.FleeState);
				}
			}
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0002A730 File Offset: 0x00028930
		public void OnMemberAcquireTarget(Entity target)
		{
			foreach (NPCBase npc in this.Members)
			{
				if (!npc.TargetingComponent.ValidateTarget())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					npc.TargetingComponent.TrySetTarget(target);
				}
			}
			if (!this.Leader.TargetingComponent.ValidateTarget())
			{
				this.Leader.TargetingComponent.TrySetTarget(target);
			}
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0002A7C0 File Offset: 0x000289C0
		public override void OnLeaderDeath()
		{
			foreach (NPCBase npc in this.Members)
			{
				if (npc.IsValid() && npc.LifeState == LifeState.Alive)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					base.SetLeader(npc);
					break;
				}
			}
		}

		// Token: 0x04000324 RID: 804
		private int MaxMembers;

		// Token: 0x04000325 RID: 805
		private int FleeThreshold = 2;
	}
}
