using System;
using System.Runtime.CompilerServices;

namespace Sandbox
{
	// Token: 0x0200018B RID: 395
	[Library]
	public class FlyingController : BasePlayerController
	{
		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x0004A157 File Offset: 0x00048357
		// (set) Token: 0x06001317 RID: 4887 RVA: 0x0004A15F File Offset: 0x0004835F
		[DefaultValue(0.25f)]
		public float Bounce { get; set; } = 0.25f;

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x0004A168 File Offset: 0x00048368
		// (set) Token: 0x06001319 RID: 4889 RVA: 0x0004A170 File Offset: 0x00048370
		[DefaultValue(20f)]
		public float Size { get; set; } = 20f;

		// Token: 0x0600131A RID: 4890 RVA: 0x0004A17C File Offset: 0x0004837C
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

		// Token: 0x0600131B RID: 4891 RVA: 0x0004A2D8 File Offset: 0x000484D8
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
