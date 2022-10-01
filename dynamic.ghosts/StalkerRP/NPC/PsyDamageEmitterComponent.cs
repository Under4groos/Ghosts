using System;
using System.Runtime.CompilerServices;
using Sandbox;
using StalkerRP.Debug;

namespace StalkerRP.NPC
{
	// Token: 0x0200006C RID: 108
	public class PsyDamageEmitterComponent : EntityComponent
	{
		// Token: 0x06000503 RID: 1283 RVA: 0x00018298 File Offset: 0x00016498
		public void Initialise(float _damagePerSecond, float _range)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.damagePerSecond = _damagePerSecond;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.range = _range;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.initialised = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.psyTrigger = new PsyDamageEmitterTrigger();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.psyTrigger.SetTriggerSize(this.range);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.psyTrigger.SetParent(base.Entity, null, null);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00018310 File Offset: 0x00016510
		[Event.Tick.ServerAttribute]
		private void Update()
		{
			if (DebugCommands.stalker_debug_npc_disable_thinking)
			{
				return;
			}
			if (!this.Enabled || !this.initialised || this.timeSinceLastTick < 0.5f)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DealDamageInTrigger();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceLastTick = 0f;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00018368 File Offset: 0x00016568
		private void DealDamageInTrigger()
		{
			foreach (Entity entity in this.psyTrigger.TriggeringEntities)
			{
				PsyHealthComponent psyHealthComponent;
				if (!entity.Equals(base.Entity) && base.Entity.CanSee(entity) && entity.Components.TryGet<PsyHealthComponent>(out psyHealthComponent, false))
				{
					float distance = entity.Position.Distance(base.Entity.Position);
					float weight = 1f - distance / this.range;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					psyHealthComponent.TakeDamage(this.damagePerSecond * 0.5f * weight);
				}
			}
		}

		// Token: 0x040001B9 RID: 441
		private const float tickDelay = 0.5f;

		// Token: 0x040001BA RID: 442
		private float damagePerSecond;

		// Token: 0x040001BB RID: 443
		private float range;

		// Token: 0x040001BC RID: 444
		private bool initialised;

		// Token: 0x040001BD RID: 445
		private TimeSince timeSinceLastTick = 0f;

		// Token: 0x040001BE RID: 446
		private PsyDamageEmitterTrigger psyTrigger;
	}
}
