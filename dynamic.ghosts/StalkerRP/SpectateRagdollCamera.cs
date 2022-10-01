using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x02000016 RID: 22
	public class SpectateRagdollCamera : StalkerBaseCamera
	{
		// Token: 0x060000CE RID: 206 RVA: 0x000084AA File Offset: 0x000066AA
		public override void Activated()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Activated();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FocusPoint = CurrentView.Position - this.GetViewOffset();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.FieldOfView = CurrentView.FieldOfView;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000084E4 File Offset: 0x000066E4
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

		// Token: 0x060000D0 RID: 208 RVA: 0x00008580 File Offset: 0x00006780
		public virtual Vector3 GetSpectatePoint()
		{
			Player player = Local.Pawn as Player;
			if (player != null && player.Corpse.IsValid())
			{
				return player.Corpse.PhysicsGroup.MassCenter;
			}
			return Local.Pawn.Position;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000085C4 File Offset: 0x000067C4
		public virtual Vector3 GetViewOffset()
		{
			if (Local.Client == null)
			{
				return Vector3.Zero;
			}
			return Input.Rotation.Forward * -130f + Vector3.Up * 20f;
		}

		// Token: 0x0400003C RID: 60
		private Vector3 FocusPoint;
	}
}
