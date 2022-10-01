using System;
using System.Runtime.CompilerServices;

namespace Sandbox
{
	// Token: 0x02000142 RID: 322
	public class FixedCamera : CameraMode
	{
		// Token: 0x06000EAC RID: 3756 RVA: 0x0003B8F4 File Offset: 0x00039AF4
		public FixedCamera()
		{
			Client client = Local.Client;
			Entity player = (client != null) ? client.Pawn : null;
			if (player != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Position = player.Position;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TargetPos = base.Position;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Rotation = player.Rotation;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.TargetRot = base.Rotation;
			}
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0003B960 File Offset: 0x00039B60
		public override void Update()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.FieldOfView = 70f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Position = this.TargetPos;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Rotation = this.TargetRot;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Viewer = null;
		}

		// Token: 0x0400049C RID: 1180
		private Vector3 TargetPos;

		// Token: 0x0400049D RID: 1181
		private Rotation TargetRot;
	}
}
