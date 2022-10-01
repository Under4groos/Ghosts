using System;
using Sandbox;
using StalkerRP.NPC;

namespace StalkerRP.Anomaly
{
	// Token: 0x0200013B RID: 315
	public class BurnerEntityTrigger : EntityTrigger<Entity>
	{
		// Token: 0x06000E05 RID: 3589 RVA: 0x0003859E File Offset: 0x0003679E
		protected override bool CanTouchTrigger(Entity entity)
		{
			return entity is StalkerPlayer || entity is NPCBase || entity is Prop || entity is StalkerProjectileBase;
		}
	}
}
