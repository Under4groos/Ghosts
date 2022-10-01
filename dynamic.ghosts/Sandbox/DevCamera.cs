using System;
using System.Runtime.CompilerServices;
using Sandbox.UI;

namespace Sandbox
{
	// Token: 0x02000140 RID: 320
	public class DevCamera : CameraMode
	{
		// Token: 0x06000EA2 RID: 3746 RVA: 0x0003B1E4 File Offset: 0x000393E4
		[Description("On the camera becoming activated, snap to the current view position")]
		public override void Activated()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Activated();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetPos = CurrentView.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetRot = CurrentView.Rotation;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Position = this.TargetPos;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Rotation = this.TargetRot;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LookAngles = base.Rotation.Angles();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FovOverride = 80f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			RootPanel hud = Local.Hud;
			if (hud != null)
			{
				hud.SetClass("devcamera", true);
			}
			if (DevCamera.devRoot == null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				DevCamera.devRoot = new RootPanel();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				DevCamera.dofSettings = DevCamera.devRoot.AddChild<DevCamSettings>(null);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			DevCamSettings devCamSettings = DevCamera.dofSettings;
			if (devCamSettings == null)
			{
				return;
			}
			devCamSettings.Activated();
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0003B2C1 File Offset: 0x000394C1
		public override void Deactivated()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Deactivated();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			RootPanel hud = Local.Hud;
			if (hud != null)
			{
				hud.SetClass("devcamera", false);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			DevCamSettings devCamSettings = DevCamera.dofSettings;
			if (devCamSettings == null)
			{
				return;
			}
			devCamSettings.Deactivated();
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0003B300 File Offset: 0x00039500
		public override void Update()
		{
			if (Local.Client == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.FieldOfView = this.FovOverride;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Viewer = null;
			if (this.PivotEnabled)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.PivotMove();
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FreeMove();
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0003B354 File Offset: 0x00039554
		public override void BuildInput(InputBuilder input)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MoveInput = input.AnalogMove;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MoveSpeed = 1f;
			if (input.Down(InputButton.Run))
			{
				this.MoveSpeed = 5f;
			}
			if (input.Down(InputButton.Duck))
			{
				this.MoveSpeed = 0.2f;
			}
			if (input.Down(InputButton.Slot1))
			{
				this.LerpMode = 0f;
			}
			if (input.Down(InputButton.Slot2))
			{
				this.LerpMode = 0.5f;
			}
			if (input.Down(InputButton.Slot3))
			{
				this.LerpMode = 0.9f;
			}
			if (input.Pressed(InputButton.Walk))
			{
				Vector3 position = base.Position;
				Vector3 vector = base.Position + base.Rotation.Forward * 4096f;
				TraceResult tr = Trace.Ray(position, vector).Run();
				if (tr.Hit)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.PivotPos = tr.EndPosition;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.PivotDist = Vector3.DistanceBetween(tr.EndPosition, base.Position);
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.PivotEnabled = true;
				}
			}
			if (input.Down(InputButton.SecondaryAttack))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.FovOverride += input.AnalogLook.pitch * (this.FovOverride / 30f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.FovOverride = this.FovOverride.Clamp(5f, 150f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				input.AnalogLook = default(Angles);
			}
			if (input.Pressed(InputButton.Score))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				DevCamSettings devCamSettings = DevCamera.dofSettings;
				if (devCamSettings != null)
				{
					devCamSettings.Show();
				}
			}
			if (input.Released(InputButton.Score))
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				DevCamSettings devCamSettings2 = DevCamera.dofSettings;
				if (devCamSettings2 != null)
				{
					devCamSettings2.Hide();
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LookAngles += input.AnalogLook * (this.FovOverride / 80f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.LookAngles.roll = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PivotEnabled = (this.PivotEnabled && input.Down(InputButton.Walk));
			if (this.PivotEnabled)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.MoveInput.x = this.MoveInput.x + (float)input.MouseWheel * 10f;
			}
			else
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.BaseMoveSpeed += (float)input.MouseWheel * 10f;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.BaseMoveSpeed = this.BaseMoveSpeed.Clamp(10f, 1000f);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			input.Clear();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			input.StopProcessing = true;
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0003B620 File Offset: 0x00039820
		private void FreeMove()
		{
			Vector3 mv = this.MoveInput.Normal * this.BaseMoveSpeed * RealTime.Delta * base.Rotation * this.MoveSpeed;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetRot = Rotation.From(this.LookAngles);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetPos += mv;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Position = Vector3.Lerp(base.Position, this.TargetPos, 10f * RealTime.Delta * (1f - this.LerpMode), true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Rotation = Rotation.Slerp(base.Rotation, this.TargetRot, 10f * RealTime.Delta * (1f - this.LerpMode), true);
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0003B6FC File Offset: 0x000398FC
		private void PivotMove()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PivotDist -= this.MoveInput.x * RealTime.Delta * 100f * (this.PivotDist / 50f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PivotDist = this.PivotDist.Clamp(1f, 1000f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetRot = Rotation.From(this.LookAngles);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Rotation = Rotation.Slerp(base.Rotation, this.TargetRot, 10f * RealTime.Delta * (1f - this.LerpMode), true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetPos = this.PivotPos + base.Rotation.Forward * -this.PivotDist;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Position = this.TargetPos;
		}

		// Token: 0x0400048E RID: 1166
		private Angles LookAngles;

		// Token: 0x0400048F RID: 1167
		private Vector3 MoveInput;

		// Token: 0x04000490 RID: 1168
		private Vector3 TargetPos;

		// Token: 0x04000491 RID: 1169
		private Rotation TargetRot;

		// Token: 0x04000492 RID: 1170
		private bool PivotEnabled;

		// Token: 0x04000493 RID: 1171
		private Vector3 PivotPos;

		// Token: 0x04000494 RID: 1172
		private float PivotDist;

		// Token: 0x04000495 RID: 1173
		private float MoveSpeed;

		// Token: 0x04000496 RID: 1174
		private float BaseMoveSpeed = 300f;

		// Token: 0x04000497 RID: 1175
		private float FovOverride;

		// Token: 0x04000498 RID: 1176
		private float LerpMode;

		// Token: 0x04000499 RID: 1177
		private static RootPanel devRoot;

		// Token: 0x0400049A RID: 1178
		private static DevCamSettings dofSettings;
	}
}
