using System;
using System.Runtime.CompilerServices;
using Sandbox;
using StalkerRP.Debug;

namespace StalkerRP.NPC
{
	// Token: 0x020000EF RID: 239
	public class NPCMovementSlowTurn : NPCMovementBase
	{
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x0002C4EB File Offset: 0x0002A6EB
		[Description("If the dot product of the targets new direction withs its current forward direction is less than this value than it will begin a stationary turn. Lower value = stationary turns at larger angles.")]
		protected virtual float StationaryTurnDotThreshold
		{
			get
			{
				return 0.35f;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x0002C4F2 File Offset: 0x0002A6F2
		[Description("If the dot product of the targets new direction withs its current forward direction is greater than this value it will end its stationary turn and begin accelerating.")]
		protected virtual float StationaryTurnEndDotThreshold
		{
			get
			{
				return 0.7f;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x0002C4F9 File Offset: 0x0002A6F9
		protected virtual float TurnSpeedMin
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x0002C500 File Offset: 0x0002A700
		protected virtual float TurnSpeedMax
		{
			get
			{
				return 100f;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x0002C507 File Offset: 0x0002A707
		protected virtual float Acceleration
		{
			get
			{
				return 5f;
			}
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0002C50E File Offset: 0x0002A70E
		public NPCMovementSlowTurn(NPCBase npc) : base(npc)
		{
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0002C518 File Offset: 0x0002A718
		public override void Update(float delta)
		{
			if (this.Host.Steer != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.Steer.Tick(this.Host.Position);
				if (!this.Host.Steer.Output.Finished)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Host.InputVelocity = this.Host.Steer.Output.Direction.Normal;
					if (this.Host.GroundEntity != null)
					{
						Vector3 dir = this.Host.Rotation.Forward;
						float dot = this.Host.InputVelocity.Dot(dir);
						if (dot < this.StationaryTurnDotThreshold)
						{
							RuntimeHelpers.EnsureSufficientExecutionStack();
							this.IsStationaryTurning = true;
							RuntimeHelpers.EnsureSufficientExecutionStack();
							this.Host.Velocity = this.Host.Velocity.LerpTo(0f, 3f * delta);
						}
						else if (dot > this.StationaryTurnEndDotThreshold || !this.IsStationaryTurning)
						{
							RuntimeHelpers.EnsureSufficientExecutionStack();
							this.Host.Velocity = this.Host.Velocity.LerpTo(dir * this.Host.Speed, this.Acceleration * delta);
							RuntimeHelpers.EnsureSufficientExecutionStack();
							this.IsStationaryTurning = false;
						}
						else
						{
							RuntimeHelpers.EnsureSufficientExecutionStack();
							this.Host.Velocity = this.Host.Velocity.LerpTo(0f, 3f * delta);
						}
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
				this.Host.IsStuck = (this.Host.TimeSinceLastStuck > 1.5f);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Move(delta);
			if (this.Host.Steer != null)
			{
				float turnSpeed = this.Host.Velocity.WithZ(0f).Length.LerpInverse(0f, 100f, true).Clamp(this.TurnSpeedMin, this.TurnSpeedMax);
				Rotation targetRotation = Rotation.LookAt(this.Host.InputVelocity, Vector3.Up);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.Rotation = Rotation.Lerp(this.Host.Rotation, targetRotation, turnSpeed * delta * 2f, true);
			}
		}

		// Token: 0x0400033E RID: 830
		public bool IsStationaryTurning;
	}
}
