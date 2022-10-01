using System;
using System.Runtime.CompilerServices;
using Sandbox;
using StalkerRP.Debug;

namespace StalkerRP.NPC
{
	// Token: 0x020000ED RID: 237
	public class NPCMovementBase
	{
		// Token: 0x06000A3F RID: 2623 RVA: 0x0002BCF1 File Offset: 0x00029EF1
		public NPCMovementBase(NPCBase npc)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host = npc;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0002BD08 File Offset: 0x00029F08
		public virtual void Update(float delta)
		{
			if (this.Host.Steer != null && !DebugCommands.stalker_debug_npc_disable_thinking)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.Steer.Tick(this.Host.Position);
				if (!this.Host.Steer.Output.Finished)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.InputVelocity = this.Host.Steer.Output.Direction.Normal;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.Velocity = this.Host.Velocity.AddClamped(this.Host.InputVelocity * delta * this.Host.Resource.Acceleration, this.Host.Speed);
					if (this.Host.GroundEntity != null)
					{
						float oldZ = this.Host.Velocity.z;
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.Host.Velocity = this.Host.Velocity.ClampLength(this.Host.Speed);
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.Host.Velocity = this.Host.Velocity.WithZ(oldZ);
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
				float turnSpeed = walkVelocity.Length.LerpInverse(0f, 100f, true);
				Rotation targetRotation = Rotation.LookAt(walkVelocity.Normal, Vector3.Up);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.Rotation = Rotation.Lerp(this.Host.Rotation, targetRotation, turnSpeed * delta * 15f, true);
			}
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0002BF58 File Offset: 0x0002A158
		public virtual void Move(float timeDelta)
		{
			BBox bbox = BBox.FromHeightAndRadius(64f, 4f);
			MoveHelper move = new MoveHelper(this.Host.Position, this.Host.Velocity);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			move.MaxStandableAngle = 50f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Entity host = this.Host;
			bool flag = true;
			move.Trace = move.Trace.Ignore(host, flag).Size(bbox);
			if (!this.Host.Velocity.IsNearlyZero(0.001f))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				move.TryUnstuck();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				move.TryMoveWithStep(timeDelta, 30f);
			}
			TraceResult tr = move.TraceDirection(Vector3.Down * 4f);
			if (move.IsFloor(tr))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.GroundEntity = tr.Entity;
				if (!tr.StartedSolid)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					move.Position = tr.EndPosition;
				}
				if (this.Host.InputVelocity.Length > 0f)
				{
					float movement = move.Velocity.Dot(this.Host.InputVelocity.Normal);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					move.Velocity -= movement * this.Host.InputVelocity.Normal;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					move.ApplyFriction(tr.Surface.Friction * 10f, timeDelta);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					move.Velocity += movement * this.Host.InputVelocity.Normal;
				}
				else
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					move.ApplyFriction(tr.Surface.Friction * 10f, timeDelta);
				}
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.GroundEntity = null;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				move.Velocity += Vector3.Down * 900f * timeDelta;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Position = move.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Velocity = move.Velocity;
		}

		// Token: 0x0400033C RID: 828
		public NPCBase Host;
	}
}
