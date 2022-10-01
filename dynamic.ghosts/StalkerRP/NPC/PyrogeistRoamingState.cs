using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000BB RID: 187
	public class PyrogeistRoamingState : PoltergeistRoamingState
	{
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x00024148 File Offset: 0x00022348
		protected override float attackDelay
		{
			get
			{
				return 5f;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x0002414F File Offset: 0x0002234F
		protected override float attackSearchRadius
		{
			get
			{
				return 1000f;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x00024156 File Offset: 0x00022356
		protected override float attackHoldTime
		{
			get
			{
				return 2.5f;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x0002415D File Offset: 0x0002235D
		private float liveTime
		{
			get
			{
				return 7.7f;
			}
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00024164 File Offset: 0x00022364
		public PyrogeistRoamingState(PoltergeistNPC host) : base(host)
		{
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00024170 File Offset: 0x00022370
		protected override void TryAttack()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastAttack = 0f;
			if (!this.Host.TargetingComponent.ValidateTarget())
			{
				return;
			}
			Vector3 center = base.Target.WorldSpaceBounds.Center;
			Vector3 c = center;
			Vector3 vector = Vector3.Random;
			vector = vector.WithZ(Rand.Float(0.05f, 0.1f));
			vector = c + vector.Normal * Rand.Float(150f, 250f);
			Vector3 pos = Trace.Ray(center, vector).WorldOnly().Radius(1f).Run().EndPosition;
			vector = center - pos;
			Vector3 dir = vector.Normal;
			PyrogeistBurnerEntity pyrogeistBurnerEntity = new PyrogeistBurnerEntity();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pyrogeistBurnerEntity.Setup(pos, dir, this.liveTime);
		}
	}
}
