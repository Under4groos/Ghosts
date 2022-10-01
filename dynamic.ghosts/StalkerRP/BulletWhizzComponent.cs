using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x02000035 RID: 53
	public class BulletWhizzComponent : EntityComponent<StalkerPlayer>
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000D7A5 File Offset: 0x0000B9A5
		private float cooldown
		{
			get
			{
				return 0.25f;
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000D7AC File Offset: 0x0000B9AC
		public void Trigger(Vector3 position, float speed, string sound)
		{
			if (this.TimeSinceLastTrigger < this.cooldown)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Entity.DoBulletWhizz(To.Single(base.Entity.Client), position, speed, sound);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceLastTrigger = 0f;
		}

		// Token: 0x040000AD RID: 173
		private TimeSince TimeSinceLastTrigger = 0f;
	}
}
