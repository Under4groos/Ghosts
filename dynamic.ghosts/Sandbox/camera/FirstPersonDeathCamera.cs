using System;
using System.Runtime.CompilerServices;
using StalkerRP;

namespace Sandbox.camera
{
	// Token: 0x020001B8 RID: 440
	public class FirstPersonDeathCamera : StalkerBaseCamera
	{
		// Token: 0x0600161D RID: 5661 RVA: 0x000573E4 File Offset: 0x000555E4
		public override void Activated()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Activated();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Position = CurrentView.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.FieldOfView = CurrentView.FieldOfView;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ZNear = 1f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ShrinkHead();
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x00057437 File Offset: 0x00055637
		public override void Deactivated()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Deactivated();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ResetHead();
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x00057450 File Offset: 0x00055650
		private void ShrinkHead()
		{
			StalkerPlayer player = Local.Pawn as StalkerPlayer;
			if (player == null || !player.Corpse.IsValid())
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			player.Corpse.ShrinkHead();
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x0005748C File Offset: 0x0005568C
		private void ResetHead()
		{
			StalkerPlayer player = Local.Pawn as StalkerPlayer;
			if (player == null || !player.Corpse.IsValid())
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			player.Corpse.ResetHead();
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x000574C8 File Offset: 0x000556C8
		public override void Update()
		{
			if (Local.Client == null)
			{
				return;
			}
			ValueTuple<Vector3, Rotation> transform = this.GetCamTransform();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Position = transform.Item1 + transform.Item2.Up * 4f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Rotation = transform.Item2;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Viewer = null;
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x00057530 File Offset: 0x00055730
		public virtual ValueTuple<Vector3, Rotation> GetCamTransform()
		{
			StalkerPlayer player = Local.Pawn as StalkerPlayer;
			if (player != null && player.Corpse.IsValid())
			{
				Transform? attachment = player.Corpse.GetAttachment("hat", true);
				if (attachment != null)
				{
					return new ValueTuple<Vector3, Rotation>(attachment.Value.Position, attachment.Value.Rotation);
				}
			}
			return new ValueTuple<Vector3, Rotation>(Local.Pawn.Position, Input.Rotation);
		}
	}
}
