using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x02000043 RID: 67
	public class StalkerWalkController : WalkController
	{
		// Token: 0x060002E8 RID: 744 RVA: 0x00011774 File Offset: 0x0000F974
		protected override void CheckFalling()
		{
			if (this.oldGroundEntity != base.GroundEntity && base.GroundEntity != null)
			{
				this.OnHitGround(this.oldVelocity);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.oldVelocity = base.Velocity;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.oldGroundEntity = base.GroundEntity;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000117C8 File Offset: 0x0000F9C8
		protected void OnHitGround(Vector3 fallVelocity)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			StalkerBaseCamera instance = StalkerBaseCamera.Instance;
			if (instance != null)
			{
				instance.SnapToEyePosition();
			}
			float fallSpeed = MathF.Abs(fallVelocity.z);
			StalkerPlayer player = base.Pawn as StalkerPlayer;
			if (player != null)
			{
				player.DoFallDamage(fallSpeed);
			}
		}

		// Token: 0x040000E9 RID: 233
		private Vector3 oldVelocity;

		// Token: 0x040000EA RID: 234
		private Entity oldGroundEntity;
	}
}
