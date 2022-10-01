using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x020001AB RID: 427
	[Library("trigger_hurt")]
	[HammerEntity]
	[Solid]
	[Title("Hurt Volume")]
	[Category("Triggers")]
	[Icon("personal_injury")]
	[Description("A trigger volume that damages entities that touch it.")]
	public class HurtVolumeEntity : BaseTrigger
	{
		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x0005681C File Offset: 0x00054A1C
		// (set) Token: 0x060015B6 RID: 5558 RVA: 0x00056824 File Offset: 0x00054A24
		[Property("damage", null, Title = "Damage")]
		[global::DefaultValue(10f)]
		[Description("Amount of damage to deal to touching entities per second.")]
		public float Damage { get; set; } = 10f;

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x0005682D File Offset: 0x00054A2D
		// (set) Token: 0x060015B8 RID: 5560 RVA: 0x00056835 File Offset: 0x00054A35
		[Description("Fired when a player gets hurt by this trigger")]
		protected Entity.Output OnHurtPlayer { get; set; }

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x0005683E File Offset: 0x00054A3E
		// (set) Token: 0x060015BA RID: 5562 RVA: 0x00056846 File Offset: 0x00054A46
		[Description("Fired when anything BUT a player gets hurt by this trigger")]
		protected Entity.Output OnHurt { get; set; }

		// Token: 0x060015BB RID: 5563 RVA: 0x00056850 File Offset: 0x00054A50
		[Event.Tick.ServerAttribute]
		protected virtual void DealDamagePerTick()
		{
			if (!base.Enabled)
			{
				return;
			}
			foreach (Entity entity in base.TouchingEntities)
			{
				if (entity.IsValid())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					entity.TakeDamage(DamageInfo.Generic(this.Damage * Time.Delta).WithAttacker(this, null));
					if (entity.Tags.Has("player"))
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.OnHurtPlayer.Fire(entity, 0f);
					}
					else
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.OnHurt.Fire(entity, 0f);
					}
				}
			}
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x00056918 File Offset: 0x00054B18
		[Input]
		[Description("Sets the damage per second for this trigger_hurt")]
		protected void SetDamage(float damage)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Damage = damage;
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x00056926 File Offset: 0x00054B26
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void CreateHammerOutputs()
		{
			this.OnHurtPlayer = new Entity.Output(this, "OnHurtPlayer");
			this.OnHurt = new Entity.Output(this, "OnHurt");
			base.CreateHammerOutputs();
		}
	}
}
