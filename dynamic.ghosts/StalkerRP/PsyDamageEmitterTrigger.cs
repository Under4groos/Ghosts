using System;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x0200001C RID: 28
	public class PsyDamageEmitterTrigger : EntityTrigger<Entity>
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00008E78 File Offset: 0x00007078
		protected override bool CanTouchTrigger(Entity entity)
		{
			PsyHealthComponent psyHealthComponent;
			return entity.Components.TryGet<PsyHealthComponent>(out psyHealthComponent, false);
		}
	}
}
