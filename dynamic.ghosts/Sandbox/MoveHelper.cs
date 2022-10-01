using System;
using System.Runtime.CompilerServices;

namespace Sandbox
{
	// Token: 0x0200015B RID: 347
	public struct MoveHelper
	{
		// Token: 0x06000FB0 RID: 4016 RVA: 0x0003E618 File Offset: 0x0003C818
		public MoveHelper(Vector3 position, Vector3 velocity, params string[] solidTags)
		{
			this = default(MoveHelper);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Velocity = velocity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Position = position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.GroundBounce = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.WallBounce = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MaxStandableAngle = 10f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Vector3 vector = 0f;
			Vector3 vector2 = 0f;
			this.Trace = Trace.Ray(vector, vector2).WorldAndEntities().WithAnyTags(solidTags);
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0003E6AF File Offset: 0x0003C8AF
		public MoveHelper(Vector3 position, Vector3 velocity)
		{
			this = new MoveHelper(position, velocity, new string[]
			{
				"solid",
				"playerclip",
				"passbullets",
				"player"
			});
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0003E6E0 File Offset: 0x0003C8E0
		[Description("Trace this from one position to another")]
		public TraceResult TraceFromTo(Vector3 start, Vector3 end)
		{
			return this.Trace.FromTo(start, end).Run();
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0003E704 File Offset: 0x0003C904
		[Description("Trace this from its current Position to a delta")]
		public TraceResult TraceDirection(Vector3 down)
		{
			return this.TraceFromTo(this.Position, this.Position + down);
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0003E720 File Offset: 0x0003C920
		[Description("Try to move to the position. Will return the fraction of the desired velocity that we traveled. Position and Velocity will be what we recommend using.")]
		public float TryMove(float timestep)
		{
			float timeLeft = timestep;
			float travelFraction = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HitWall = false;
			float result;
			using (VelocityClipPlanes moveplanes = new VelocityClipPlanes(this.Velocity, 5))
			{
				int bump = 0;
				while (bump < moveplanes.Max && !this.Velocity.Length.AlmostEqual(0f, 0.0001f))
				{
					TraceResult pm = this.TraceFromTo(this.Position, this.Position + this.Velocity * timeLeft);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					travelFraction += pm.Fraction;
					if (!pm.Hit)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.Position = pm.EndPosition;
						break;
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Position = pm.EndPosition + pm.Normal * 0.03125f;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					moveplanes.StartBump(this.Velocity);
					if (bump == 0 && pm.Hit && pm.Normal.Angle(Vector3.Up) >= this.MaxStandableAngle)
					{
						RuntimeHelpers.EnsureSufficientExecutionStack();
						this.HitWall = true;
					}
					RuntimeHelpers.EnsureSufficientExecutionStack();
					timeLeft -= timeLeft * pm.Fraction;
					if (!moveplanes.TryAdd(pm.Normal, ref this.Velocity, this.IsFloor(pm) ? this.GroundBounce : this.WallBounce))
					{
						break;
					}
					bump++;
				}
				if (travelFraction == 0f)
				{
					this.Velocity = 0f;
				}
				result = travelFraction;
			}
			return result;
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0003E8D0 File Offset: 0x0003CAD0
		[Description("Return true if this is the trace is a floor. Checks hit and normal angle.")]
		public bool IsFloor(TraceResult tr)
		{
			return tr.Hit && tr.Normal.Angle(Vector3.Up) < this.MaxStandableAngle;
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0003E8F8 File Offset: 0x0003CAF8
		[Description("Apply an amount of friction to the velocity")]
		public void ApplyFriction(float frictionAmount, float delta)
		{
			float StopSpeed = 100f;
			float speed = this.Velocity.Length;
			if (speed < 0.1f)
			{
				return;
			}
			float drop = ((speed < StopSpeed) ? StopSpeed : speed) * delta * frictionAmount;
			float newspeed = speed - drop;
			if (newspeed < 0f)
			{
				newspeed = 0f;
			}
			if (newspeed == speed)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			newspeed /= speed;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Velocity *= newspeed;
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0003E964 File Offset: 0x0003CB64
		[Description("Move our position by this delta using trace. If we hit something we'll stop, we won't slide across it nicely like TryMove does.")]
		public TraceResult TraceMove(Vector3 delta)
		{
			TraceResult tr = this.TraceFromTo(this.Position, this.Position + delta);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Position = tr.EndPosition;
			return tr;
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0003E99C File Offset: 0x0003CB9C
		[Description("Like TryMove but will also try to step up if it hits a wall")]
		public float TryMoveWithStep(float timeDelta, float stepsize)
		{
			Vector3 startPosition = this.Position;
			MoveHelper stepMove = this;
			float fraction = this.TryMove(timeDelta);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			stepMove.TraceMove(Vector3.Up * stepsize);
			float stepFraction = stepMove.TryMove(timeDelta);
			TraceResult tr = stepMove.TraceMove(Vector3.Down * stepsize);
			if (!tr.Hit)
			{
				return fraction;
			}
			if (tr.Normal.Angle(Vector3.Up) > this.MaxStandableAngle)
			{
				return fraction;
			}
			if (startPosition.Distance(this.Position.WithZ(startPosition.z)) > startPosition.Distance(stepMove.Position.WithZ(startPosition.z)))
			{
				return fraction;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Position = stepMove.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Velocity = stepMove.Velocity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HitWall = stepMove.HitWall;
			return stepFraction;
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0003EA85 File Offset: 0x0003CC85
		[Description("Test whether we're stuck, and if we are then unstuck us")]
		public bool TryUnstuck()
		{
			return !this.TraceFromTo(this.Position, this.Position).StartedSolid || this.Unstuck();
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0003EAA8 File Offset: 0x0003CCA8
		[Description("We're inside something solid, lets try to get out of it.")]
		private bool Unstuck()
		{
			for (int i = 1; i < 20; i++)
			{
				Vector3 tryPos = this.Position + Vector3.Up * (float)i;
				TraceResult tr = this.TraceFromTo(tryPos, this.Position);
				if (!tr.StartedSolid)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Position = tryPos + tr.Direction.Normal * (tr.Distance - 0.5f);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Velocity = 0f;
					return true;
				}
			}
			for (int j = 1; j < 100; j++)
			{
				Vector3 tryPos2 = this.Position + Vector3.Random * (float)j;
				TraceResult tr2 = this.TraceFromTo(tryPos2, this.Position);
				if (!tr2.StartedSolid)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Position = tryPos2 + tr2.Direction.Normal * (tr2.Distance - 0.5f);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.Velocity = 0f;
					return true;
				}
			}
			return false;
		}

		// Token: 0x040004F7 RID: 1271
		public Vector3 Position;

		// Token: 0x040004F8 RID: 1272
		public Vector3 Velocity;

		// Token: 0x040004F9 RID: 1273
		public bool HitWall;

		// Token: 0x040004FA RID: 1274
		public float GroundBounce;

		// Token: 0x040004FB RID: 1275
		public float WallBounce;

		// Token: 0x040004FC RID: 1276
		public float MaxStandableAngle;

		// Token: 0x040004FD RID: 1277
		public Trace Trace;
	}
}
