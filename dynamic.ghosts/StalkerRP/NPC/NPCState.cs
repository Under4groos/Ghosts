using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x0200006D RID: 109
	public abstract class NPCState<T> : BaseNPCState where T : NPCBase
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00018448 File Offset: 0x00016648
		protected Entity Target
		{
			get
			{
				return this.Host.Target;
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001845A File Offset: 0x0001665A
		protected NPCState(T host)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host = host;
		}

		// Token: 0x040001BF RID: 447
		protected readonly T Host;
	}
}
