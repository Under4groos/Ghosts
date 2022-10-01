using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x0200002E RID: 46
	public class NoclipController : BasePlayerController
	{
		// Token: 0x06000162 RID: 354 RVA: 0x0000A620 File Offset: 0x00008820
		public override void Simulate()
		{
			Vector3 vel = Input.Rotation.Forward * Input.Forward + Input.Rotation.Left * Input.Left;
			if (Input.Down(InputButton.Jump))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				vel += Vector3.Up * 1f;
			}
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
				base.Position += base.Velocity * Time.Delta;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Velocity = base.Velocity.Approach(0f, base.Velocity.Length * Time.Delta * 5f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EyeRotation = Input.Rotation;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.WishVelocity = base.Velocity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.GroundEntity = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.BaseVelocity = Vector3.Zero;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetTag("noclip");
		}
	}
}
