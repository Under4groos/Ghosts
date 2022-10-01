using System;
using System.Runtime.CompilerServices;

namespace Sandbox
{
	// Token: 0x02000141 RID: 321
	public class FirstPersonCamera : CameraMode
	{
		// Token: 0x06000EA9 RID: 3753 RVA: 0x0003B800 File Offset: 0x00039A00
		public override void Activated()
		{
			Entity pawn = Local.Pawn;
			if (pawn == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Position = pawn.EyePosition;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Rotation = pawn.EyeRotation;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.lastPos = base.Position;
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0003B84C File Offset: 0x00039A4C
		public override void Update()
		{
			Entity pawn = Local.Pawn;
			if (pawn == null)
			{
				return;
			}
			Vector3 eyePos = pawn.EyePosition;
			if (eyePos.Distance(this.lastPos) < 300f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Position = Vector3.Lerp(eyePos.WithZ(this.lastPos.z), eyePos, 20f * Time.Delta, true);
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Position = eyePos;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Rotation = pawn.EyeRotation;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Viewer = pawn;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.lastPos = base.Position;
		}

		// Token: 0x0400049B RID: 1179
		private Vector3 lastPos;
	}
}
