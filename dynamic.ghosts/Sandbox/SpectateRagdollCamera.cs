using System;
using System.Runtime.CompilerServices;

namespace Sandbox
{
	// Token: 0x02000164 RID: 356
	public class SpectateRagdollCamera : CameraMode
	{
		// Token: 0x06001046 RID: 4166 RVA: 0x000403FF File Offset: 0x0003E5FF
		public override void Activated()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Activated();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FocusPoint = CurrentView.Position - this.GetViewOffset();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.FieldOfView = CurrentView.FieldOfView;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x00040438 File Offset: 0x0003E638
		public override void Update()
		{
			if (Local.Client == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FocusPoint = Vector3.Lerp(this.FocusPoint, this.GetSpectatePoint(), Time.Delta * 5f, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Position = this.FocusPoint + this.GetViewOffset();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Rotation = Input.Rotation;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.FieldOfView = base.FieldOfView.LerpTo(50f, Time.Delta * 3f, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Viewer = null;
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x000404D4 File Offset: 0x0003E6D4
		public virtual Vector3 GetSpectatePoint()
		{
			Player player = Local.Pawn as Player;
			if (player != null && player.Corpse.IsValid())
			{
				return player.Corpse.PhysicsGroup.MassCenter;
			}
			return Local.Pawn.Position;
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x00040518 File Offset: 0x0003E718
		public virtual Vector3 GetViewOffset()
		{
			if (Local.Client == null)
			{
				return Vector3.Zero;
			}
			return Input.Rotation.Forward * -130f + Vector3.Up * 20f;
		}

		// Token: 0x04000514 RID: 1300
		private Vector3 FocusPoint;
	}
}
