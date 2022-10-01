using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x0200002D RID: 45
	public class FlyingController : BasePlayerController
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000A35C File Offset: 0x0000855C
		// (set) Token: 0x0600015C RID: 348 RVA: 0x0000A364 File Offset: 0x00008564
		[DefaultValue(0.25f)]
		public float Bounce { get; set; } = 0.25f;

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000A36D File Offset: 0x0000856D
		// (set) Token: 0x0600015E RID: 350 RVA: 0x0000A375 File Offset: 0x00008575
		[DefaultValue(20f)]
		public float Size { get; set; } = 20f;

		// Token: 0x0600015F RID: 351 RVA: 0x0000A380 File Offset: 0x00008580
		public override void Simulate()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Rotation = Input.Rotation;
			Vector3 vel = Input.Rotation.Forward * Input.Forward + Input.Rotation.Left * Input.Left;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			vel = vel.Normal * 2000f;
			if (Input.Down(InputButton.Run))
			{
				vel *= 5f;
			}
			if (Input.Down(InputButton.Duck))
			{
				vel *= 0.2f;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Velocity += vel * Time.Delta;
			if (base.Velocity.LengthSquared > 0.01f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Move(base.Velocity * Time.Delta, 0);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Velocity = base.Velocity.Approach(0f, base.Velocity.Length * Time.Delta * 5f);
			if (Input.Down(InputButton.Jump))
			{
				base.Velocity = base.Velocity.Approach(0f, base.Velocity.Length * Time.Delta * 5f);
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000A4DC File Offset: 0x000086DC
		public void Move(Vector3 delta, int a = 0)
		{
			if (a > 1)
			{
				return;
			}
			float length = delta.Length;
			Vector3 targetPos = base.Position + delta;
			Vector3 vector = base.Position;
			Trace trace = Trace.Ray(vector, targetPos).WorldOnly();
			Vector3 vector2 = this.Size;
			TraceResult tr = trace.Size(vector2).Run();
			if (tr.StartedSolid)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Position = targetPos;
				return;
			}
			if (tr.Hit)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Position = tr.EndPosition + tr.Normal;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vector = base.Velocity;
				base.Velocity = vector.SubtractDirection(tr.Normal * (1f + this.Bounce), 1f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vector = base.Velocity;
				delta = vector.Normal * delta.Length * (1f - tr.Fraction);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Move(delta, ++a);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Position = tr.EndPosition;
		}
	}
}
