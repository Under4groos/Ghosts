using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x02000020 RID: 32
	public class DamageEffectBase
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00009565 File Offset: 0x00007765
		public float LifeTime
		{
			get
			{
				return 4f;
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000956C File Offset: 0x0000776C
		public virtual void OnEffectAdded()
		{
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000956E File Offset: 0x0000776E
		public virtual void EffectTick()
		{
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00009570 File Offset: 0x00007770
		public virtual void DoTransfer(Entity newHost)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host = newHost;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceCreated = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnEffectAdded();
		}

		// Token: 0x0400006B RID: 107
		public Entity Host;

		// Token: 0x0400006C RID: 108
		public TimeSince TimeSinceCreated = 0f;
	}
}
