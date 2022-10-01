using System;
using System.Runtime.CompilerServices;

namespace Sandbox
{
	// Token: 0x0200018C RID: 396
	[Library]
	public class NoclipController : BasePlayerController
	{
		// Token: 0x0600131D RID: 4893 RVA: 0x0004A41C File Offset: 0x0004861C
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
