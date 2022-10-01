using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.Anomaly
{
	// Token: 0x02000134 RID: 308
	public class AnomalyMoving : PathPlatformEntity
	{
		// Token: 0x06000D7C RID: 3452 RVA: 0x0003696D File Offset: 0x00034B6D
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("ENT_ANOMALY");
		}
	}
}
