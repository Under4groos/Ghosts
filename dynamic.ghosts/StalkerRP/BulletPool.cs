using System;
using System.Runtime.CompilerServices;

namespace StalkerRP
{
	// Token: 0x02000052 RID: 82
	public class BulletPool : EntityPool<BulletPhysBase>
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00013157 File Offset: 0x00011357
		public override int MaxActiveEntities
		{
			get
			{
				return 2000;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0001315E File Offset: 0x0001135E
		public override bool CreateEntitiesOnCacheEmpty
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00013161 File Offset: 0x00011361
		protected override void OnRequested(BulletPhysBase bullet)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			bullet.IsRetired = false;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0001316F File Offset: 0x0001136F
		protected override void OnRetired(BulletPhysBase bullet)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			bullet.IsRetired = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			bullet.Position = Vector3.Zero;
		}
	}
}
