using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.Inventory
{
	// Token: 0x02000106 RID: 262
	public class StashTrigger : EntityTrigger<StalkerPlayer>
	{
		// Token: 0x06000BAA RID: 2986 RVA: 0x0002FC08 File Offset: 0x0002DE08
		public void SetStashParent(StashEntity stashEntity)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StashParent = stashEntity;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0002FC16 File Offset: 0x0002DE16
		protected override bool CanTouchTrigger(StalkerPlayer player)
		{
			return player.LifeState == LifeState.Alive && base.CanTouchTrigger(player);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002FC29 File Offset: 0x0002DE29
		protected override void OnTriggerLeft(StalkerPlayer player)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StashParent.OnPlayerLeft(player);
		}

		// Token: 0x040003B1 RID: 945
		public StashEntity StashParent;
	}
}
