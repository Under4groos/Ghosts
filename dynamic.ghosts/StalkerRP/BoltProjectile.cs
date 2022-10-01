using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x02000055 RID: 85
	[Library("ent_bolt_projectile", Title = "Bolt Projectile")]
	[Spawnable]
	public class BoltProjectile : StalkerProjectileBase
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0001334B File Offset: 0x0001154B
		protected override string ModelPath
		{
			get
			{
				return "models/stalker/weapons/wpn_bolt/bolt_world2.vmdl";
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00013354 File Offset: 0x00011554
		public override void StartTouch(Entity other)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StartTouch(other);
			IBoltable boltable = other as IBoltable;
			if (boltable != null)
			{
				boltable.OnHitByBolt(this);
			}
		}
	}
}
