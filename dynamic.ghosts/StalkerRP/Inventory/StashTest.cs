using System;
using System.Runtime.CompilerServices;

namespace StalkerRP.Inventory
{
	// Token: 0x02000105 RID: 261
	[Title("Box")]
	[Spawnable]
	public class StashTest : StashEntity
	{
		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002FBE3 File Offset: 0x0002DDE3
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.InitialiseFromAsset("stash_box");
		}
	}
}
