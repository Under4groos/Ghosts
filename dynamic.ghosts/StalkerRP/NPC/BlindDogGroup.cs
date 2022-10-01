using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x0200009D RID: 157
	public class BlindDogGroup : NPCGroup
	{
		// Token: 0x06000707 RID: 1799 RVA: 0x00020448 File Offset: 0x0001E648
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

		// Token: 0x06000708 RID: 1800 RVA: 0x000204C4 File Offset: 0x0001E6C4
		protected override void OnMemberAdded(NPCBase npc)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MaxMembers++;
			if (npc is PseudoDogNPC && (this.Leader == null || !this.Leader.IsValid() || !(this.Leader is PseudoDogNPC)))
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

		// Token: 0x06000709 RID: 1801 RVA: 0x0002055D File Offset: 0x0001E75D
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

		// Token: 0x0600070A RID: 1802 RVA: 0x00020590 File Offset: 0x0001E790
		private void Flee()
		{
			foreach (BlindDogNPC dog in this.Members.OfType<BlindDogNPC>())
			{
				if (dog.IsValid())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					dog.SM.SetState(dog.FleeState);
				}
			}
			foreach (PseudoDogNPC dog2 in this.Members.OfType<PseudoDogNPC>())
			{
				if (dog2.IsValid())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					dog2.SM.SetState(dog2.FleeState);
				}
			}
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00020654 File Offset: 0x0001E854
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
			if (this.Leader.IsValid() && !this.Leader.TargetingComponent.ValidateTarget())
			{
				this.Leader.TargetingComponent.TrySetTarget(target);
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x000206F0 File Offset: 0x0001E8F0
		public override void OnLeaderDeath()
		{
			if (this.Leader is PseudoDogNPC)
			{
				NPCBase dog = this.Members.FirstOrDefault((NPCBase x) => x is PseudoDogNPC);
				if (dog != null && dog.IsValid())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					base.SetLeader(dog);
				}
				else
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Flee();
				}
			}
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

		// Token: 0x04000262 RID: 610
		private int MaxMembers;

		// Token: 0x04000263 RID: 611
		private int FleeThreshold = 2;
	}
}
