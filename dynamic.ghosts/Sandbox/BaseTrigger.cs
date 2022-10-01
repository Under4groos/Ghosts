using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x02000175 RID: 373
	[AutoApplyMaterial("materials/tools/toolstrigger.vmat")]
	[Solid]
	[VisGroup(VisGroup.Trigger)]
	[HideProperty("enable_shadows")]
	[Title("Base Trigger")]
	[Icon("select_all")]
	public class BaseTrigger : ModelEntity
	{
		// Token: 0x06001107 RID: 4359 RVA: 0x00043CC8 File Offset: 0x00041EC8
		[ConCmd.AdminAttribute("drawtriggers_toggle")]
		internal static void ToggleDrawTriggers()
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("drawtriggers_toggle");
				return;
			}
			foreach (Entity ent in Entity.All)
			{
				ModelEntity modelEnt = ent as ModelEntity;
				if (modelEnt != null && (ent is BaseTrigger || ent.Tags.Has("trigger")))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					modelEnt.DebugFlags ^= EntityDebugFlags.TriggerBounds;
				}
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001108 RID: 4360 RVA: 0x00043D5C File Offset: 0x00041F5C
		// (set) Token: 0x06001109 RID: 4361 RVA: 0x00043D64 File Offset: 0x00041F64
		[Property]
		[Title("Activation Tags")]
		[global::DefaultValue("player")]
		[Description("Entities with these tags can activate this trigger.")]
		public TagList ActivationTags { get; set; } = "player";

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x00043D6D File Offset: 0x00041F6D
		// (set) Token: 0x0600110B RID: 4363 RVA: 0x00043D75 File Offset: 0x00041F75
		[Property]
		[global::DefaultValue(true)]
		[Description("Whether this entity is enabled or not.")]
		public bool Enabled { get; protected set; } = true;

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x0600110C RID: 4364 RVA: 0x00043D7E File Offset: 0x00041F7E
		public IEnumerable<Entity> TouchingEntities
		{
			get
			{
				return this.touchingEntities;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x00043D86 File Offset: 0x00041F86
		public int TouchingEntityCount
		{
			get
			{
				return this.touchingEntities.Count;
			}
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00043D94 File Offset: 0x00041F94
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("trigger");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetupPhysicsFromModel(PhysicsMotionType.Static, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableAllCollisions = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableTouch = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Transmit = TransmitType.Never;
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00043DF3 File Offset: 0x00041FF3
		public override void StartTouch(Entity other)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.StartTouch(other);
			if (other.IsWorld)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AddToucher(other);
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00043E16 File Offset: 0x00042016
		public override void Touch(Entity other)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Touch(other);
			if (other.IsWorld)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AddToucher(other);
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x00043E3C File Offset: 0x0004203C
		public override void EndTouch(Entity other)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EndTouch(other);
			if (other.IsWorld)
			{
				return;
			}
			if (this.touchingEntitiesWhileDisabled.Contains(other))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.touchingEntitiesWhileDisabled.Remove(other);
			}
			if (this.touchingEntities.Contains(other))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.touchingEntities.Remove(other);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.OnTouchEnd(other);
				if (this.touchingEntities.Count < 1)
				{
					this.OnTouchEndAll(other);
				}
			}
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00043EC0 File Offset: 0x000420C0
		protected void AddToucher(Entity toucher)
		{
			if (!toucher.IsValid())
			{
				return;
			}
			if (!this.Enabled)
			{
				if (!this.touchingEntitiesWhileDisabled.Contains(toucher))
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.touchingEntitiesWhileDisabled.Add(toucher);
				}
				return;
			}
			if (this.touchingEntities.Contains(toucher))
			{
				return;
			}
			if (!this.PassesTriggerFilters(toucher))
			{
				return;
			}
			bool flag = this.touchingEntities.Count > 0;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.touchingEntities.Add(toucher);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnTouchStart(toucher);
			if (!flag)
			{
				this.OnTouchStartAll(toucher);
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x00043F4B File Offset: 0x0004214B
		// (set) Token: 0x06001114 RID: 4372 RVA: 0x00043F53 File Offset: 0x00042153
		[Description("Fired when an entity starts touching this trigger. The touching entity must pass this trigger's filters to cause this output to fire.")]
		protected Entity.Output OnStartTouch { get; set; }

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x00043F5C File Offset: 0x0004215C
		// (set) Token: 0x06001116 RID: 4374 RVA: 0x00043F64 File Offset: 0x00042164
		[Description("Fired when an entity stops touching this trigger. Only entities that passed this trigger's filters will cause this output to fire.")]
		protected Entity.Output OnEndTouch { get; set; }

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x00043F6D File Offset: 0x0004216D
		// (set) Token: 0x06001118 RID: 4376 RVA: 0x00043F75 File Offset: 0x00042175
		[Description("Fired when an entity starts touching this trigger while no other passing entities are touching it.")]
		protected Entity.Output OnStartTouchAll { get; set; }

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x00043F7E File Offset: 0x0004217E
		// (set) Token: 0x0600111A RID: 4378 RVA: 0x00043F86 File Offset: 0x00042186
		[Description("Fired when all entities touching this trigger have stopped touching it.")]
		protected Entity.Output OnEndTouchAll { get; set; }

		// Token: 0x0600111B RID: 4379 RVA: 0x00043F90 File Offset: 0x00042190
		[Input]
		[Description("Enables this trigger")]
		public void Enable()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Enabled = true;
			foreach (Entity entity in this.touchingEntitiesWhileDisabled.ToArray())
			{
				if (entity.IsValid())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.StartTouch(entity);
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.touchingEntitiesWhileDisabled.Clear();
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00043FEC File Offset: 0x000421EC
		[Input]
		[Description("Disables this trigger")]
		public void Disable()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Enabled = false;
			foreach (Entity entity in this.TouchingEntities.ToList<Entity>())
			{
				if (entity.IsValid())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.EndTouch(entity);
					if (!this.touchingEntitiesWhileDisabled.Contains(entity))
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.touchingEntitiesWhileDisabled.Add(entity);
					}
				}
			}
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0004407C File Offset: 0x0004227C
		[Input]
		[Description("Toggles this trigger between enabled and disabled states")]
		public void Toggle()
		{
			if (this.Enabled)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Disable();
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Enable();
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x000440A0 File Offset: 0x000422A0
		[Description("An entity that passes PassesTriggerFilters has started touching the trigger")]
		public virtual void OnTouchStart(Entity toucher)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnStartTouch.Fire(toucher, 0f);
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x000440C8 File Offset: 0x000422C8
		[Description("An entity that started touching this trigger has stopped touching")]
		public virtual void OnTouchEnd(Entity toucher)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnEndTouch.Fire(toucher, 0f);
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x000440F0 File Offset: 0x000422F0
		[Description("Called when an entity starts touching this trigger while no other passing entities are touching it")]
		public virtual void OnTouchStartAll(Entity toucher)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnStartTouchAll.Fire(toucher, 0f);
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00044118 File Offset: 0x00042318
		[Description("Called when all entities touching this trigger have stopped touching it.")]
		public virtual void OnTouchEndAll(Entity toucher)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnEndTouchAll.Fire(toucher, 0f);
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0004413F File Offset: 0x0004233F
		[Description("Determine if an entity should be allowed to touch this trigger")]
		public virtual bool PassesTriggerFilters(Entity other)
		{
			return other is ModelEntity && (other.Tags.HasAny(this.ActivationTags) || this.ActivationTags.Contains("*"));
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00044174 File Offset: 0x00042374
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void CreateHammerOutputs()
		{
			this.OnStartTouch = new Entity.Output(this, "OnStartTouch");
			this.OnEndTouch = new Entity.Output(this, "OnEndTouch");
			this.OnStartTouchAll = new Entity.Output(this, "OnStartTouchAll");
			this.OnEndTouchAll = new Entity.Output(this, "OnEndTouchAll");
			base.CreateHammerOutputs();
		}

		// Token: 0x0400054F RID: 1359
		private readonly List<Entity> touchingEntities = new List<Entity>();

		// Token: 0x04000550 RID: 1360
		private readonly List<Entity> touchingEntitiesWhileDisabled = new List<Entity>();
	}
}
