using System;
using System.Runtime.CompilerServices;
using Sandbox;
using StalkerRP.Debug;

namespace StalkerRP.NPC
{
	// Token: 0x020000EE RID: 238
	public class NPCMovementDragTurn : NPCMovementBase
	{
		// Token: 0x06000A42 RID: 2626 RVA: 0x0002C1AA File Offset: 0x0002A3AA
		public NPCMovementDragTurn(NPCBase npc) : base(npc)
		{
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0002C1B4 File Offset: 0x0002A3B4
		public override void Update(float delta)
		{
			if (this.Host.Steer != null && !DebugCommands.stalker_debug_npc_disable_thinking)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.Steer.Tick(this.Host.Position);
				if (!this.Host.Steer.Output.Finished)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.InputVelocity = this.Host.Steer.Output.Direction.Normal;
					Rotation rot = this.Host.Rotation;
					Rotation target = Rotation.LookAt(this.Host.InputVelocity);
					Rotation diff = Rotation.Difference(rot, target);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.speed = (this.speed + 500f * delta).Clamp(0f, this.Host.Speed);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.InputVelocity = this.Host.Steer.Output.Direction.Normal;
					float oldZ = this.Host.Velocity.z;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.Velocity = rot.Forward * this.speed;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.Velocity = this.Host.Velocity * (diff * delta * 5f);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.Velocity = this.Host.Velocity.WithZ(oldZ);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.speed = this.Host.Velocity.Length;
					if (this.Host.GroundEntity != null)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.Host.Velocity = this.Host.Velocity.ClampLength(this.Host.Speed);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.Host.Velocity = this.Host.Velocity.WithZ(oldZ);
					}
					else
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.Host.Velocity = this.Host.Velocity.ClampLength(this.Host.Speed);
					}
				}
				if (DebugCommands.stalker_debug_npc_nav_drawpath)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.Steer.DebugDrawPath();
				}
				if (!this.Host.Velocity.IsNearlyZero(10f))
				{
					this.Host.TimeSinceLastStuck = 0f;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.IsStuck = (this.Host.TimeSinceLastStuck > 0.5f);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Move(delta);
			Vector3 walkVelocity = this.Host.Velocity.WithZ(0f);
			if (walkVelocity.Length > 0.5f)
			{
				float turnSpeed = 1.5f;
				Rotation targetRotation = Rotation.LookAt(walkVelocity.Normal, Vector3.Up);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.Rotation = Rotation.Lerp(this.Host.Rotation, targetRotation, turnSpeed * delta * 15f, true);
			}
		}

		// Token: 0x0400033D RID: 829
		private float speed;
	}
}
