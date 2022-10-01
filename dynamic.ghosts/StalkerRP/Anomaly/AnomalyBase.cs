using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;
using StalkerRP.NPC;

namespace StalkerRP.Anomaly
{
	// Token: 0x02000133 RID: 307
	public abstract class AnomalyBase : ModelEntity
	{
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x0003651B File Offset: 0x0003471B
		// (set) Token: 0x06000D5B RID: 3419 RVA: 0x000364F3 File Offset: 0x000346F3
		[ConVar.ReplicatedAttribute(null)]
		public static int stalker_debug_anomaly_draw
		{
			get
			{
				return AnomalyBase._repback__stalker_debug_anomaly_draw;
			}
			set
			{
				if (AnomalyBase._repback__stalker_debug_anomaly_draw == value)
				{
					return;
				}
				AnomalyBase._repback__stalker_debug_anomaly_draw = value;
				if (Host.IsServer)
				{
					ConsoleSystem.SetValue("stalker_debug_anomaly_draw", value);
				}
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x00036522 File Offset: 0x00034722
		// (set) Token: 0x06000D5E RID: 3422 RVA: 0x0003652A File Offset: 0x0003472A
		[Description("Determines if the anomaly is active and applying its effects.")]
		public bool IsActive { get; private set; }

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x00036533 File Offset: 0x00034733
		// (set) Token: 0x06000D60 RID: 3424 RVA: 0x0003653B File Offset: 0x0003473B
		[Description("Determines if the anomaly has been triggered.")]
		public bool IsTriggered { get; private set; }

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x00036544 File Offset: 0x00034744
		public virtual float TriggerSize
		{
			get
			{
				return 60f;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x0003654B File Offset: 0x0003474B
		[Description("How long the anomaly remains active. This is only used in the default implementation, but I'm leaving it here for convenience and clarity.")]
		public virtual float ActiveTime
		{
			get
			{
				return 5f;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x00036552 File Offset: 0x00034752
		[Description("How long after the initial trigger the anomaly becomes active.")]
		public virtual float ActivationDelay
		{
			get
			{
				return 0.5f;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x00036559 File Offset: 0x00034759
		[Description("How long the cooldown")]
		public virtual float ActiveCooldown
		{
			get
			{
				return 2f;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x00036560 File Offset: 0x00034760
		[Description("Determines if the anomaly is being touched by something could activate it.")]
		protected bool IsTriggerBeingTouched
		{
			get
			{
				return this.Trigger.IsTriggerBeingTouch;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x0003656D File Offset: 0x0003476D
		protected HashSet<Entity> TouchingEntities
		{
			get
			{
				return this.Trigger.TriggeringEntities;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x0003657A File Offset: 0x0003477A
		private Vector3 HullSize
		{
			get
			{
				return 10f;
			}
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x00036588 File Offset: 0x00034788
		[ConCmd.ServerAttribute("stalker_debug_anomaly_remove_all")]
		public static void RemoveAllAnomalies()
		{
			if (!Host.IsServer)
			{
				ConsoleSystem.Run("stalker_debug_anomaly_remove_all");
				return;
			}
			foreach (Entity entity in Entity.All)
			{
				if (entity is AnomalyBase)
				{
					entity.Delete();
				}
			}
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x000365F0 File Offset: 0x000347F0
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("weapon");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetupPhysicsFromOBB(PhysicsMotionType.Dynamic, -this.HullSize, this.HullSize);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableShadowReceive = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableShadowCasting = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Trigger = new AnomalyTrigger();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Trigger.SetTriggerSize(this.TriggerSize);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Trigger.SetParent(this, null, null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Trigger.SetAnomalyParent(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Trigger.Position = this.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsActive = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsTriggered = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Transmit = TransmitType.Pvs;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("ENT_ANOMALY");
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x000366EC File Offset: 0x000348EC
		[Event.Tick.ServerAttribute]
		protected virtual void AnomalyTick()
		{
			float delta = Time.Delta;
			if (this.IsTriggered && !this.IsActive && this.TimeSinceTriggered > this.ActivationDelay)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.ActivateAnomaly();
				return;
			}
			if (this.IsActive)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.IsActive = this.AnomalyActiveTick(delta);
				if (!this.IsActive)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.AnomalyFinishActive();
				}
			}
			else if (this.CanTriggerAnomaly())
			{
				this.TriggerAnomaly();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DebugDraw();
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00036778 File Offset: 0x00034978
		public virtual void DebugDraw()
		{
			if (AnomalyBase.stalker_debug_anomaly_draw > 0)
			{
				Vector3 pos = this.Position;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.DebugOverlay.Box(pos - this.HullSize, pos + this.HullSize, Color.Orange, 0f, true);
			}
			if (AnomalyBase.stalker_debug_anomaly_draw > 1)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.DebugOverlay.Sphere(this.Position, this.TriggerSize, Color.Red, 0f, true);
			}
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x000367F4 File Offset: 0x000349F4
		public virtual bool IsValidTriggerEnt(Entity ent)
		{
			return ent is NPCBase || ent is StalkerPlayer || ent is DeathRagdoll;
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00036811 File Offset: 0x00034A11
		public void OnValidTriggerTouch(Entity entity)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnEntityBeginTouch(entity);
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0003681F File Offset: 0x00034A1F
		protected virtual void OnEntityBeginTouch(Entity entity)
		{
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00036821 File Offset: 0x00034A21
		public void OnValidTriggerEndTouch(Entity entity)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnEntityEndTouch(entity);
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0003682F File Offset: 0x00034A2F
		protected virtual void OnEntityEndTouch(Entity entity)
		{
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00036831 File Offset: 0x00034A31
		protected virtual bool CanTriggerAnomaly()
		{
			return !this.IsTriggered && this.IsTriggerBeingTouched && this.TimeSinceFinished > this.ActiveCooldown;
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00036858 File Offset: 0x00034A58
		protected void TriggerAnomaly()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsTriggered = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceTriggered = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnAnomalyInitialTrigger();
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00036886 File Offset: 0x00034A86
		public bool TryTriggerAnomaly()
		{
			if (!this.IsTriggered && !this.IsActive && this.TimeSinceFinished > this.ActiveCooldown)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TriggerAnomaly();
				return true;
			}
			return false;
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x000368B9 File Offset: 0x00034AB9
		protected void AnomalyFinishActive()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceFinished = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnAnomalyActiveFinished();
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x000368DB File Offset: 0x00034ADB
		protected void ActivateAnomaly()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsActive = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsTriggered = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceActivated = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.OnAnomalyActivated();
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00036915 File Offset: 0x00034B15
		[Description("Called when the anomaly is triggered.")]
		protected virtual void OnAnomalyActivated()
		{
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x00036917 File Offset: 0x00034B17
		[Description("Called when the anomaly is initially triggered. After the activation delay, OnAnomalyActivated will be called.")]
		protected virtual void OnAnomalyInitialTrigger()
		{
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00036919 File Offset: 0x00034B19
		[Description("Called when the anomaly is finished being active.")]
		protected virtual void OnAnomalyActiveFinished()
		{
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0003691B File Offset: 0x00034B1B
		[Description("Called when the anomaly's trigger is hit with a bolt.")]
		public virtual void OnHitByBolt(BoltProjectile bolt)
		{
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x0003691D File Offset: 0x00034B1D
		[Description("Called each frame when the anomaly has been triggered. Return value is used to set IsActive. Wont be called if IsActive is set to false.")]
		protected virtual bool AnomalyActiveTick(float deltaTime)
		{
			return this.TimeSinceActivated <= this.ActiveTime;
		}

		// Token: 0x04000468 RID: 1128
		public const string ANOMALY_TAG = "ENT_ANOMALY";

		// Token: 0x0400046B RID: 1131
		protected AnomalyTrigger Trigger;

		// Token: 0x0400046C RID: 1132
		protected TimeSince TimeSinceActivated = 0f;

		// Token: 0x0400046D RID: 1133
		protected TimeSince TimeSinceTriggered = 0f;

		// Token: 0x0400046E RID: 1134
		protected TimeSince TimeSinceFinished = 0f;

		// Token: 0x0400046F RID: 1135
		public static int _repback__stalker_debug_anomaly_draw;
	}
}
