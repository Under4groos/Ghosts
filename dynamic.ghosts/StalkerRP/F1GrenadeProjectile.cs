using System;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x02000056 RID: 86
	[Library("ent_f1_grenade_projectile")]
	public class F1GrenadeProjectile : StalkerGrenadeProjectileBase
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00013386 File Offset: 0x00011586
		protected override string ModelPath
		{
			get
			{
				return "models/stalker/weapons/wpn_f1_grenade/f1_grenade_world.vmdl";
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0001338D File Offset: 0x0001158D
		protected override string BlastSound
		{
			get
			{
				return "f1.explode";
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00013394 File Offset: 0x00011594
		protected override string BlastEffect
		{
			get
			{
				return "particles/stalker/weapons/f1_grenade/f1_explosion.vpcf";
			}
		}
	}
}
