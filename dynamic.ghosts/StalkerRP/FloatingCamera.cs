using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x02000015 RID: 21
	public class FloatingCamera : StalkerBaseCamera
	{
		// Token: 0x060000CB RID: 203 RVA: 0x00008434 File Offset: 0x00006634
		public override void Activated()
		{
			Entity pawn = Local.Pawn;
			if (pawn == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Position = CurrentView.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Rotation = pawn.EyeRotation;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ZNear = 5f;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000847C File Offset: 0x0000667C
		public override void Update()
		{
			if (Local.Pawn == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Rotation = Input.Rotation;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Viewer = null;
		}
	}
}
