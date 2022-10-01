using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox.Internal;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x020001AA RID: 426
	[Library("push_volume")]
	[HammerEntity]
	[Solid]
	[DrawAngles("forcedirection", null)]
	[Title("Push Volume")]
	[Category("Triggers")]
	[Icon("deblur")]
	[Description("A volume that pushes entities that are touching it.")]
	public class PushVolumeEntity : BaseTrigger
	{
		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x000565AB File Offset: 0x000547AB
		// (set) Token: 0x060015AB RID: 5547 RVA: 0x000565B3 File Offset: 0x000547B3
		[Property]
		[DefaultValue(500)]
		[Description("How strong should we be pushing other entities")]
		public float Force { get; set; } = 500f;

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x060015AC RID: 5548 RVA: 0x000565BC File Offset: 0x000547BC
		// (set) Token: 0x060015AD RID: 5549 RVA: 0x000565C4 File Offset: 0x000547C4
		[Property]
		[Description("Direction of the force.")]
		public Angles ForceDirection { get; set; }

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x060015AE RID: 5550 RVA: 0x000565CD File Offset: 0x000547CD
		// (set) Token: 0x060015AF RID: 5551 RVA: 0x000565D5 File Offset: 0x000547D5
		[Property]
		[DefaultValue(false)]
		[Description("If set, applies 1 second worth of force only when an entity enters the trigger")]
		public bool OnlyPushOnEnter { get; set; }

		// Token: 0x060015B0 RID: 5552 RVA: 0x000565E0 File Offset: 0x000547E0
		protected void PushObject(Entity entity, float time)
		{
			Vector3 force = Rotation.From(this.ForceDirection).Forward * this.Force * time;
			bool isPhysics = false;
			if (entity.PhysicsGroup != null && entity.PhysicsGroup.BodyCount > 0)
			{
				using (IEnumerator<PhysicsBody> enumerator = entity.PhysicsGroup.Bodies.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.BodyType == PhysicsBodyType.Dynamic)
						{
							RuntimeHelpers.EnsureSufficientExecutionStack();
							isPhysics = true;
							break;
						}
					}
				}
			}
			if (isPhysics)
			{
				using (IEnumerator<PhysicsBody> enumerator = entity.PhysicsGroup.Bodies.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PhysicsBody body = enumerator.Current;
						RuntimeHelpers.EnsureSufficientExecutionStack();
						body.ApplyImpulse(force * body.Mass);
					}
					return;
				}
			}
			if (force.z > 1f && entity.GroundEntity != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				entity.GroundEntity = null;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			entity.Velocity += force;
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x00056708 File Offset: 0x00054908
		[Event.Tick.ServerAttribute]
		protected virtual void PushObjectsAway()
		{
			if (!base.Enabled)
			{
				return;
			}
			if (!this.OnlyPushOnEnter)
			{
				foreach (Entity entity in base.TouchingEntities)
				{
					if (entity.IsValid())
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.PushObject(entity, Time.Delta);
					}
				}
			}
			if (base.DebugFlags.HasFlag(EntityDebugFlags.Text))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				GlobalGameNamespace.DebugOverlay.Line(this.Position, this.Position + Rotation.From(this.ForceDirection).Forward * 100f, 0f, true);
			}
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x000567D4 File Offset: 0x000549D4
		public override void OnTouchStart(Entity toucher)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnTouchStart(toucher);
			if (this.OnlyPushOnEnter)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.PushObject(toucher, 1f);
			}
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x000567FB File Offset: 0x000549FB
		[Input]
		[Description("Sets the force per second for this push volume")]
		protected void SetForce(float force)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Force = force;
		}
	}
}
