using System;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x0200004B RID: 75
	public abstract class State
	{
		// Token: 0x0600030C RID: 780 RVA: 0x00012114 File Offset: 0x00010314
		public virtual void OnStateEntered()
		{
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00012116 File Offset: 0x00010316
		public virtual void OnStateExited()
		{
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00012118 File Offset: 0x00010318
		public virtual void Update(float deltaTime)
		{
		}

		// Token: 0x040000F1 RID: 241
		public TimeSince TimeSinceEntered = 0f;

		// Token: 0x040000F2 RID: 242
		public TimeSince TimeSinceExited = 0f;

		// Token: 0x040000F3 RID: 243
		public bool HasEntered;
	}
}
