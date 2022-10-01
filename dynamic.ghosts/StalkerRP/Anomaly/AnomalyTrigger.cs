using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.Anomaly
{
	// Token: 0x02000135 RID: 309
	public class AnomalyTrigger : EntityTrigger<Entity>, IBoltable
	{
		// Token: 0x06000D7E RID: 3454 RVA: 0x00036997 File Offset: 0x00034B97
		public void SetAnomalyParent(AnomalyBase anomalyBase)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AnomalyParent = anomalyBase;
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x000369A5 File Offset: 0x00034BA5
		protected override bool CanTouchTrigger(Entity other)
		{
			return !(other is ITrigger) && !(other is AnomalyBase) && !(other is PickupTrigger) && this.AnomalyParent.IsValidTriggerEnt(other);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x000369CD File Offset: 0x00034BCD
		protected override void OnTriggerTouched(Entity other)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AnomalyParent.OnValidTriggerTouch(other);
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x000369E0 File Offset: 0x00034BE0
		protected override void OnTriggerLeft(Entity other)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AnomalyParent.OnValidTriggerEndTouch(other);
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x000369F3 File Offset: 0x00034BF3
		public void OnHitByBolt(BoltProjectile bolt)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AnomalyParent.OnHitByBolt(bolt);
		}

		// Token: 0x04000470 RID: 1136
		public AnomalyBase AnomalyParent;
	}
}
